using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace UseNacos
{
    [NacosLoadBalance(nameof(RoundLoadBalance))]
    public class RoundLoadBalance : BaseLoadBalance
    {
        public RoundLoadBalance(INacosService nacosService)
        {
            NacosService = nacosService;
        }
        private int iHostIndex = 0;
        public INacosService NacosService { get; }

        public override async Task<Hosts> GetUrl(QueryInstance query)
        {
            var t= await NacosService.Queryinstances(query);
            if (t != null && t.Hosts != null && t.Hosts.Count > 0)
            {
                var host = t.Hosts[iHostIndex];
                if (iHostIndex + 1 == t.Hosts.Count)
                {
                    iHostIndex = 0;
                }
                else
                {
                    iHostIndex += 1;
                }
                return host;
            }
            return null;
        }
    }
}
