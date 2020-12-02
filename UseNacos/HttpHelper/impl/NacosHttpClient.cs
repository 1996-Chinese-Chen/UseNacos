using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace UseNacos
{
    public  class NacosHttpClient:IHttpClient
    {
        public NacosHttpClient(IHttpClientFactory clientFactory,BaseLoadBalance baseLoadBalance, IOptionsSnapshot<NacosConfig> nacosConfig)
        {
            ClientFactory = clientFactory;
            BaseLoadBalance = baseLoadBalance;
            Nacos = nacosConfig;
        }
        private IHttpClientFactory ClientFactory { get; }
        public BaseLoadBalance BaseLoadBalance { get; }
        public IOptionsSnapshot<NacosConfig> Nacos { get; }

        public async virtual Task<T> DeleteAsync<T,TR>(string Url,TR tParameter, QueryInstance query) where T:class where TR : class
        {
            try
            {
                if (query.CheckParameter())
                {
                    return null;
                }
                var host = await GetHostsAsync(query);
                var client = ExtendClass.GetHttpClient(ClientFactory, nameof(NacosHttpClient), host, Nacos.Value.EnableSSL);
                Url = Url.GetUrl(ExtendClass.SetMethodParameter(tParameter));
                var httpResponse = await client.DeleteAsync(Url);
                var resultStr = await httpResponse.Content.ReadAsStringAsync();
                var result = resultStr.ToJson<T>();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(MethodInfo.GetCurrentMethod().Name + ":" + ex.Message);
                return null;
            }
        }
        public async virtual Task<T> GetAsync<T, TR>(string Url, TR tParameter, QueryInstance query) where T:class where TR : class
        {
            try
            {
                if (query.CheckParameter())
                {
                    return null;
                }
                var host = await GetHostsAsync(query);
                var client = ExtendClass.GetHttpClient(ClientFactory, nameof(NacosHttpClient), host, Nacos.Value.EnableSSL);
                Url = Url.GetUrl(ExtendClass.SetMethodParameter(tParameter));
                var httpResponse = await client.GetAsync(Url);
                var resultStr = await httpResponse.Content.ReadAsStringAsync();
                var result = resultStr.ToJson<T>();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(MethodInfo.GetCurrentMethod().Name + ":" + ex.Message);
                return null;
            }
        }
        public async virtual Task<T> PostAsync<T,TR>(string Url,TR tParameter, QueryInstance query) where T : class where TR:class
        {
            try
            {
                if (query.CheckParameter())
                {
                    return null;
                }
                var host = await GetHostsAsync(query);
                var client = ExtendClass.GetHttpClient(ClientFactory, nameof(NacosHttpClient), host, Nacos.Value.EnableSSL);
                var content = ExtendClass.SetMethodJsonParameter(tParameter);
                content.Headers.Clear();
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json") { CharSet = "utf-8" };
                var httpResponse = await client.PostAsync(Url, content);
                var resultStr = httpResponse.Content.ReadAsStringAsync().Result;
                var result = resultStr.ToJson<T>();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(MethodInfo.GetCurrentMethod().Name + ":" + ex.Message);
                return null;
            }
            
        }
        public async virtual Task<T> PutAsync<T,TR>(string Url,TR tParameter, QueryInstance query) where T : class where TR : class
        {
            try {
                if (query.CheckParameter())
                {
                    return null;
                }
                var host = await GetHostsAsync(query);
                var client = ExtendClass.GetHttpClient(ClientFactory, nameof(NacosHttpClient), host, Nacos.Value.EnableSSL);
                var content = ExtendClass.SetMethodJsonParameter(tParameter);
                content.Headers.Clear();
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json") { CharSet = "utf-8" };
                var httpResponse = await client.PutAsync(Url, content);
                var resultStr = httpResponse.Content.ReadAsStringAsync().Result;
                var result = resultStr.ToJson<T>();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(MethodInfo.GetCurrentMethod().Name + ":" + ex.Message);
                return null;
            }
        }
        private async Task<Hosts> GetHostsAsync(QueryInstance query)
        {
            try
            {
                var host = await BaseLoadBalance.GetUrl(query);
                return host;
            }
            catch (Exception ex)
            {
                Console.WriteLine(MethodInfo.GetCurrentMethod().Name + ":" + ex.Message);
                return null;
            }
        }
    }
}
