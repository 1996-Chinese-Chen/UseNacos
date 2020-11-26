using System;
using System.Collections.Generic;
using System.Text;

namespace UseNacos
{
    [AttributeUsage(AttributeTargets.Class)]
   public class NacosLoadBalance:Attribute
    {
        public String LoadBalanceValue { get; set; }
        public NacosLoadBalance(String loadBalance)
        {
            LoadBalanceValue = loadBalance;
        }
    }
}
