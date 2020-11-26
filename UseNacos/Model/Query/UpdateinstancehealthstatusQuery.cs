using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UseNacos
{
    public class UpdateinstancehealthstatusQuery:BaseRequest
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
        /// 是否健康
        /// </summary>
        [Description("healthy")]
        public Boolean Healthy { get; set; } = true;
        public override bool CheckParameter()
        {
            if (string.IsNullOrEmpty(ServiceName))
            {
                Console.WriteLine(nameof(UpdateinstancehealthstatusQuery) + "：ServiceName不能为空！");
                return true;
            }
            if (string.IsNullOrEmpty(Ip))
            {
                Console.WriteLine(nameof(UpdateinstancehealthstatusQuery) + "：Ip不能为空！");
                return true;
            }
            if (Port <= 0)
            {
                Console.WriteLine(nameof(UpdateinstancehealthstatusQuery) + "：Port不能小于等于0！");
                return true;
            }
            return false;
        }
    }
}
