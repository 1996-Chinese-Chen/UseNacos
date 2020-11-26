using System;
using System.Collections.Generic;
using System.Text;

namespace UseNacos
{
   public class Hosts
    {
        public String Ip{ get; set; }
        public int Port { get; set; }
        public bool Valid { get; set; }
        public bool Marked { get; set; }
        public String InstanceId { get; set; }
        public Double Weight { get; set; }
        public object Metadata { get; set; }
    }
}
