using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;

namespace UseNacos
{
    public class BackgroundServices : IHostedService, IDisposable
    {
        private INacosService NacosService;
        public static  NacosConfig NacosConfig { get; set; }
        private List<Timer> Timer { get; set; } = new List<Timer>();
        private List<BaseModel> List { get; set; } = new List<BaseModel>();
        //定义一个定时器
        private Timer _timer;
        public IServiceCollection Services { get; }
        public BackgroundServices(IServiceCollection services,INacosService nacosService)
        {
            NacosService = nacosService;
            Services = services;
            var t = services.Where(s => s.ImplementationInstance != null && s.ImplementationInstance.GetType().BaseType == typeof(BaseModel)).ToList();
            if (t != null && t.Count > 0)
            {
                foreach (var yu in t)
                {
                    var y = yu.ImplementationInstance as BaseModel;
                    List.Add(y);
                }
            }
        }
        /// <summary>
        /// 启动任务绑定
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //绑定定时任务
            //设置延迟时间
            List.ForEach(s =>
            {
               _timer = new Timer(DoWork, s, TimeSpan.FromSeconds(0.2), TimeSpan.FromSeconds(1));
                Timer.Add(_timer);
            });
            return Task.CompletedTask;
        }

        /// <summary>
        /// 定时执行的操作，绑定到定时器上
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private  void DoWork(object state)
        {
            try
            {
                var basemodel = state as BaseModel;//对象转为BaseModel
                var vAttribute = basemodel.GetType().GetCustomAttributes(true).Where(a => a.GetType() == typeof(NacosValueAttribute)).ToList();//获取NacosValueAttribute
                if (vAttribute != null && vAttribute.Count > 0)
                {
                    var nacosValue = vAttribute.FirstOrDefault() as NacosValueAttribute;
                    var Config = NacosService.Getconfigurations(new ConfigrationModel { DataId = nacosValue.DataId,Group=NacosConfig.GroupName,  Tenant = nacosValue.Tenant }, basemodel.GetType()).Result;
                    if (Config != null)
                    {
                        foreach (var p in basemodel.GetType().GetProperties())
                        {
                            var value = Config.GetType().GetProperty(p.Name).GetValue(Config);
                            if (value != null)
                            {
                                p.SetValue(basemodel, value);
                            }
                        }
                        //获取配置的值，然后动态更新
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 任务关闭时执行
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 释放托管资源，释放时触发
        /// </summary>
        public void Dispose()
        {
            _timer?.Dispose();
            Thread.Sleep(5000);
        }
    }
}
