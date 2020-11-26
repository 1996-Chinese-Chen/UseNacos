using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UseNacos
{
   public class ConfigrationModel: BaseRequest
    {
        [Description("tenant")]
        public string Tenant { get; set; }
        [Description("dataId")]
        public string DataId { get; set; }
        [Description("group")]
        public string Group { get; set; }

        public override bool CheckParameter()
        {
            if (string.IsNullOrEmpty(DataId))
            {
                Console.WriteLine(nameof(ConfigrationModel) + "：DataId不能为空！");
                return true;
            }
            if (string.IsNullOrEmpty(Group))
            {
                Console.WriteLine(nameof(ConfigrationModel) + "：Group不能为空！");
                return true;
            }
            return false;
        }
    }
}
