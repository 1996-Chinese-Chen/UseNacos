using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UseNacos
{
  public  class QueryServiceList:BaseRequest
    {
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
        /// 分页大小
        /// </summary>
        [Description("pageSize")]
        public int PageSize { get; set; } = 15;
        /// <summary>
        /// 页码
        /// </summary>
        [Description("pageNo")]
        public int PageNo { get; set; } = 0;

        public override bool CheckParameter()
        {
            return false;
        }
    }
}
