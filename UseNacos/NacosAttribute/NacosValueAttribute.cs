using System;
using System.Collections.Generic;
using System.Text;

namespace UseNacos
{
    [AttributeUsage(AttributeTargets.Class)]
  public  class NacosValueAttribute:Attribute
    {
        /// <summary>
        /// DataId
        /// </summary>
        public String DataId { get; set; }
        /// <summary>
        /// 租户
        /// </summary>
        public string Tenant { get; }

        public NacosValueAttribute(String DataIdValue, string telent="")
        {
            this.DataId = DataIdValue;
            this.Tenant = telent;
        }
    }
}
