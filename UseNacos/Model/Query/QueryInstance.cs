using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UseNacos
{
   public class QueryInstance: BaseRequest
    {
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
        /// 命名空间ID
        /// </summary>
        [Description("namespaceId")]
        public String NamespaceId { get; set; }
        /// <summary>
        /// 集群名称(多个使用,分割)
        /// </summary>
        [Description("clusters")]
        public String Clusters { get; set; }
        /// <summary>
        /// 是否只查询健康实例
        /// </summary>
        [Description("healthyOnly")]
        public Boolean HealthyOnly { get; set; } = false;

        public override bool CheckParameter()
        {
            if (string.IsNullOrEmpty(ServiceName))
            {
                Console.WriteLine(nameof(QueryInstance) + "：ServiceName不能为空！");
                return true;
            }
            return false;
        }
    }
}
