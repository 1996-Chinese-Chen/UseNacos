using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UseNacos
{
   public class Updateservice:BaseRequest
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
        /// 元数据
        /// </summary>
        [Description("metadata")]
        public String Metadata { get; set; }
        /// <summary>
        /// 访问策略
        /// </summary>
        [Description("selector")]
        public String Selector { get; set; }
        /// <summary>
        /// 保护阈值,取值0到1,默认0
        /// </summary>
        [Description("protectThreshold")]
        public double ProtectThreshold { get; set; }
        public override bool CheckParameter()
        {
            if (string.IsNullOrEmpty(ServiceName))
            {
                Console.WriteLine(nameof(Updateservice) + "：ServiceName不能为空！");
                return true;
            }
            return false;
        }
    }
}
