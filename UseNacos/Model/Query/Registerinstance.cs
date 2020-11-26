using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UseNacos
{
   public class Registerinstance:BaseRequest
    {
        /// <summary>
        /// 服务实例IP
        /// </summary>
        [Description("ip")]
        public String Ip { get; set; }
        /// <summary>
        /// 服务实例port
        /// </summary>
        [Description("port")]
        public int Port { get; set; }
        /// <summary>
        /// 命名空间ID
        /// </summary>
        [Description("namespaceId")]
        public String NamespaceId { get; set; }
        /// <summary>
        /// 权重
        /// </summary>
        [Description("weight")]
        public Double Weight { get; set; }
        /// <summary>
        /// 是否上线
        /// </summary>
        [Description("enabled")]
        public Boolean Enabled { get; set; } = true;
        /// <summary>
        /// 是否健康
        /// </summary>
        [Description("healthy")]
        public Boolean Healthy { get; set; } = true;
        /// <summary>
        /// 元数据
        /// </summary>
        [Description("metadata")]
        public String Metadata { get; set; }
        /// <summary>
        /// 集群名称
        /// </summary>
        [Description("clusterName")]
        public String ClusterName { get; set; }
        /// <summary>
        /// 服务名
        /// </summary>
        [Description("serviceName")]
        public String ServiceName { get; set; }
        /// <summary>
        /// 分组名
        /// </summary>
        [Description("groupName")]
        public String GroupName { get; set; }
        /// <summary>
        /// 是否临时实例
        /// </summary>
        [Description("ephemeral")]
        public Boolean Ephemeral { get; set; } = false;

        public override bool CheckParameter()
        {
            if (string.IsNullOrEmpty(ServiceName))
            {
                Console.WriteLine(nameof(Registerinstance) + "：ServiceName不能为空！");
                return true;
            }
            if (string.IsNullOrEmpty(Ip))
            {
                Console.WriteLine(nameof(Registerinstance) + "：Ip不能为空！");
                return true;
            }
            if (Port<=0)
            {
                Console.WriteLine(nameof(Registerinstance) + "：Port不能小于等于0！");
                return true;
            }
            return false;
        }
    }
}
