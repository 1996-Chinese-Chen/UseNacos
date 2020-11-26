using System;
using System.Collections.Generic;
using System.Text;

namespace UseNacos
{
  public  class QueryinstancedetailsModel
    {
        public Object MetaData { get; set; }
        public string InstanceId { get; set; }
        public int Port { get; set; }
        public String Service { get; set; }
        public String ClusterName { get; set; }
        public String Ip { get; set; }
        public Double Weight { get; set; }
        public Boolean Healthy { get; set; }
    }
}
