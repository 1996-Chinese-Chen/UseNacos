using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace UseNacos
{
 public static  class NacosExtend
    {
        public static void AddNacosCore(this IServiceCollection services ,IConfiguration configuration)
        {
            #region 配置类的动态更新
            var nacos = configuration.GetSection(nameof(NacosConfig)).Get(typeof(NacosConfig)) as NacosConfig;
            configuration.Bind(nacos);
            services.AddOptions<NacosConfig>();
            ChangeToken.OnChange(() => configuration.GetReloadToken(), () => {
                if (nacos.GroupName == null || nacos.GroupName == "") nacos.GroupName = "DEFAULT_GROUP";
                nacos = configuration.GetSection(nameof(NacosConfig)).Get(typeof(NacosConfig)) as NacosConfig;
                BackgroundServices.NacosConfig = nacos;
                configuration.Bind(nacos);
            });
            if (nacos.GroupName == null || nacos.GroupName == "") nacos.GroupName = "DEFAULT_GROUP";
            BackgroundServices.NacosConfig = nacos;
            #endregion
            #region 服务依赖注入
            services.Configure<NacosConfig>(configuration.GetSection(nameof(NacosConfig)));
            services.AddScoped(typeof(IHttpClient), typeof(NacosHttpClient));
            services.AddSingleton(services); //注入IServiceCollection后续使用获取服务
            services.AddSingleton(typeof(INacosService), typeof(NacosService));//注册Nacos服务接口，
            services.AddHttpClient("NacosConfig",s=> {
                s.BaseAddress = new Uri(nacos.RegisterUrl);
            });//注入HttpFactory工厂，并设置baseurl为Nacos链接地址
           
            services.AddHttpClient(nameof(NacosHttpClient));//httphelper所需httpclient
            var nacosService = services.BuildServiceProvider().GetService(typeof(INacosService)) as INacosService;//获取上面注册的IServiceCollection服务，下面首次配置赋值
            services.AddHostedService<BackgroundServices>();//注入定时任务，定时获取最新配置
            #endregion
            #region 服务注册
            var vMetadata = configuration.GetSection((nameof(NacosConfig) + ":Metadata")).GetChildren().ToList();
            var dvMetadata = new Dictionary<string, string>();
            foreach (var iChildren in vMetadata)
            {
                dvMetadata.Add(iChildren.Key, iChildren.Value);
            }
            var metaData = JsonConvert.SerializeObject(dvMetadata);
            nacosService.Registerinstance(new Registerinstance
            {
                ServiceName = nacos.ServiceName,
                Ip = nacos.Ip,
                Port = nacos.Port,
                Enabled = nacos.Enabled,
                Healthy = nacos.Healthy,
                GroupName = nacos.GroupName,
                NamespaceId = nacos.NamespaceId,
                Metadata = metaData,
                Ephemeral=nacos.Ephemeral,
                ClusterName=nacos.ClusterName,
                Weight=nacos.Weight
            });
            #endregion
            #region 配置首次加载赋值，负载均衡注入
            AppDomain.CurrentDomain.GetAssemblies().Where(w => !w.FullName.Contains("System") && !w.FullName.Contains("Microsoft")&&!w.FullName.Contains("Newtonsoft")&&!w.FullName.Contains("Anonymously Hosted DynamicMethods")).ToList().ForEach(s =>
            {
                //获取当前类库下所有继承BaseModel的配置类
                var list = s?.GetExportedTypes().Where(w => w.BaseType == typeof(BaseModel)).ToList();
                var loadBalance = s.GetExportedTypes().Where(w => w.BaseType == typeof(BaseLoadBalance)).ToList();
                if (loadBalance.Count > 0)
                {
                    foreach (var balance in loadBalance)
                    {
                        var vAttribute = balance.GetCustomAttributes(true).Where(attri => attri.GetType() == typeof(NacosLoadBalance)).ToList();
                        if (vAttribute != null && vAttribute.Count > 0)
                        {
                            var nacosLoadBalance = vAttribute.FirstOrDefault() as NacosLoadBalance;
                            if (nacosLoadBalance.LoadBalanceValue != nacos.LoadBalance) continue;
                            var client = services.BuildServiceProvider().GetService(typeof(IHttpClientFactory)) as IHttpClientFactory;//获取上面注册的IServiceCollection服务，下面首次配置赋值
                            var vparameters = balance.GetConstructors().FirstOrDefault().GetParameters() ;
                            var tparameter = new List<object>();
                            foreach (var parameter in vparameters)
                            {
                                var vInstance = services.Where(p => p.ServiceType == parameter.ParameterType).FirstOrDefault();
                                if (vInstance != null)
                                {
                                    var tInstance = vInstance.ImplementationInstance;
                                    if (tInstance == null)
                                    {
                                        tInstance = services.BuildServiceProvider().GetService(parameter.ParameterType);
                                    }
                                    tparameter.Add(tInstance);
                                }
                            }
                            var baseLoadBalance = Activator.CreateInstance(balance, tparameter.ToArray()) as BaseLoadBalance;
                            services.AddSingleton(balance, baseLoadBalance);
                            services.AddSingleton(typeof(BaseLoadBalance), baseLoadBalance);
                        }
                    }
                }
                list.ForEach(sb =>
                {
                    //循环实体类，并创建实体类对象
                    var sbInstance= Activator.CreateInstance(sb);
                    var vAttribute = sb.GetCustomAttributes(true).Where(a=>a.GetType()==typeof(NacosValueAttribute)).ToList();//获取该实体类下是否使用NacosValueAttribute特性
                    if (vAttribute != null && vAttribute.Count > 0)
                    {
                        var nacosValue = vAttribute.FirstOrDefault() as NacosValueAttribute;
                        //使用了该特性，调用接口获取该特性的配置结果
                        var Config = nacosService.Getconfigurations(new ConfigrationModel { DataId = nacosValue.DataId, Tenant = nacosValue.Tenant,Group=nacos.GroupName },sb).Result;
                        foreach (var p in sbInstance.GetType().GetProperties())
                        {
                            //反射获取获取的配置结果中该p列的结果值，
                            var value = Config.GetType().GetProperty(p.Name).GetValue(Config);
                            if (value != null)
                            {
                                p.SetValue(sbInstance, value);//为创建的t对象反射赋值
                            }
                        }
                    }
                    //注入创建的该对象为单例
                    services.AddSingleton(sb, sbInstance);
                });
            });//获取所有本地类库，去除系统类库；
            #endregion
        }
    }
}
