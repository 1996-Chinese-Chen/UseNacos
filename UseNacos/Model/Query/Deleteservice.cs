﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UseNacos
{
    public class Deleteservice : BaseRequest
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
        public override bool CheckParameter()
        {
            if (string.IsNullOrEmpty(ServiceName))
            {
                Console.WriteLine(nameof(Deleteservice) + "：ServiceName不能为空！");
                return true;
            }
            return false;
        }
    }
}
