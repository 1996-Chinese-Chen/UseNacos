using System;
using System.Collections.Generic;
using System.Text;

namespace UseNacos
{
   public class ServiceList
    {
        public String Dom { get; set; }
        public String Name { get; set; }
        public Double CacheMillis { get; set; }
        public Double LastRefTime { get; set; }
        public String Checksum { get; set; }
        public String Clusters { get; set; }
        public String Env { get; set; }
        public Boolean UseSpecifiedURL { get; set; }
        //public String Metadata { get; set; }
        public List<Hosts> Hosts { get; set; }
    }
}
