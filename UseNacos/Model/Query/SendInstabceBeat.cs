using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UseNacos
{
   public class SendInstabceBeat:BaseRequest
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
        /// 是否临时实例
        /// </summary>
        [Description("ephemeral")]
        public Boolean Ephemeral { get; set; } = false;
        /// <summary>
        /// 心跳内容
        /// </summary>
        [Description("beat")]
        public String Beat { get; set; }

        public override bool CheckParameter()
        {
            if (string.IsNullOrEmpty(ServiceName))
            {
                Console.WriteLine(nameof(SendInstabceBeat) + "：ServiceName不能为空！");
                return true;
            }
            if (string.IsNullOrEmpty(Beat))
            {
                Console.WriteLine(nameof(SendInstabceBeat) + "：Beat不能为空！");
                return true;
            }
            return false;
        }
    }
}
