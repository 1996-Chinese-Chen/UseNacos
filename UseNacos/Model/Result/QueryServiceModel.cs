using System;
using System.Collections.Generic;
using System.Text;

namespace UseNacos
{
    public class QueryServiceModel
    {
        public Object MetaData { get; set; }
        public String GroupName { get; set; }
        public String NamespaceId { get; set; }
        public String Name { get; set; }
        public Selector Selector { get; set; }
        public double ProtectThreshold { get; set; }
        public List<Clusters> Clusters { get; set; }
    }
    public class Selector
    {

        public String Type { get; set; }
    }
    public class Clusters
    {
        public HealthChecker HealthChecker { get; set; }
        public Object MetaData { get; set; }
        public String Name { get; set; }
    }
    public class HealthChecker
    {
        public String Type { get; set; }
    }
}
