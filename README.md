# 使用 Nacos 注册服务到阿里巴巴的 Nacos 服务中心

在 ConfigureServices 配置使用

```csharp
services.AddNacosCore(Configuration);
```

appSetting.Json 文件配置如下：

```json
{  
"NacosConfig": {
  "Ip": "localhost", //注册到Nacos的IP 
  "Port": 51711,//注册到Nacos的端口 
  "ServiceName": "Test", //注册到Nacos的服务名
  "Metadata": { "Name": "cxd", "Age": 18 }, //注册到Nacos的元数据,格式必须为Json字符串
  "Enabled": true, //服务是否上线 
  "Healthy": true, //服务是否健康 
  "RegisterUrl": "http://127.0.0.1:8848", //Nacos服务注册地址 
  "EnableSSL": false, //是否启动SSL,启用调用服务获取数据时使用的是HTTPS 
  "GroupName": "", //注册到Nacos的分组名 
  "Weight": 0, //权重 
  "ClusterName": "", //集群名称 
  "Ephemeral": false, // 是否临时实例 
  "NameSpaceId": "", //命名空间ID 
  "LoadBalance": "RoundLoadBalance" //默认RoundLoadBalance为轮询算法，值为特性的值 
}
}

```

动态配置类：
继承 `BaseModel` 使用 `NacosValueAttribute` 特性，标记配置类的 DataId 以及 Telent 租户信息

```csharp
public HelloController (
	INacosService nacos,
	IHttpClient http,
	IOptionsSnapshot nacosConfig,
	Config config) 
{ 
   Nacos = nacos;
   Class1 = http; 
   NacosConfig = nacosConfig; 
} 
```

使用配置如上，Config 继承 BaseModel [NacosValue("Config")]DataId为Config NacosConfig为appSetting.Json文件配置 INacosService为服务注册接口，包含配置获取，服务注册，注销，修改，删除，实例注册，修改，发送心跳等，IHttpClient为封装好的HTTP接口，内置负载均衡，默认为轮询， 自定义负载均衡，继承BaseLoadBalance实现GetUrl方法即可，返回Host类型，包含服务IP等信息，其中必须使用NacosLoadBalance特性传入自定义负载均衡标识，建议使用自定义负载均衡类名 需要注意的是，自定义负载均衡构造方法所需参数必须在services.AddNacosCore(Configuration);方法之前注入到容器内部，否则会报错

 QQ934550201
