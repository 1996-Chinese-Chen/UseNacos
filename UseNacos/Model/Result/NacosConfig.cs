using System;
using System.Collections.Generic;
using System.Text;

namespace UseNacos
{
   public class NacosConfig
    {
        /// <summary>
        /// 服务实例IP
        /// </summary>
        public String Ip { get; set; }
        /// <summary>
        /// 服务实例port
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 命名空间ID
        /// </summary>
        public String NamespaceId { get; set; }
        /// <summary>
        /// 权重
        /// </summary>
        public Double Weight { get; set; }
        /// <summary>
        /// 是否上线
        /// </summary>
        public Boolean Enabled { get; set; } = true;
        /// <summary>
        /// 是否健康
        /// </summary>
        public Boolean Healthy { get; set; } = true;
        /// <summary>
        /// 集群名称
        /// </summary>
        public String ClusterName { get; set; }
        /// <summary>
        /// 服务名
        /// </summary>
        public String ServiceName { get; set; }
        /// <summary>
        /// 分组名
        /// </summary>
        public String GroupName { get; set; } = "DEFAULT_GROUP";
        /// <summary>
        /// 是否临时实例
        /// </summary>
        public Boolean Ephemeral { get; set; } = false;
        /// <summary>
        /// 是否启用https
        /// </summary>
        public Boolean EnableSSL { get; set; } = false;
        /// <summary>
        /// 注册中心地址
        /// </summary>
        public String RegisterUrl { get; set; }
        /// <summary>
        /// 负载均衡
        /// </summary>
        public String LoadBalance { get; set; } 
    }
}
