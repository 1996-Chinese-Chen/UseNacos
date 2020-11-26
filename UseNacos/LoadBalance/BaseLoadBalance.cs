using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UseNacos
{
    public abstract  class BaseLoadBalance
    {
        public BaseLoadBalance() 
        {

        }
        public abstract Task<Hosts> GetUrl(QueryInstance query);
    }
}
