using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace UseNacos
{
    public class NacosService : INacosService
    {
        public NacosService(IHttpClientFactory httpClient)
        {
            HttpClient = httpClient;
        }
        //注入的IHttpClientFactory
        public IHttpClientFactory HttpClient { get; }
        public async Task<Result> Createservice(Createservice info)
        {
            if (info.CheckParameter()) return null;
            var url = InterfaceConst.ServiceAddRess;
            return await PostAsync(url, info, MethodBase.GetCurrentMethod().Name);
        }

        public async Task<Result> Deleteservice(Deleteservice info)
        {
            if (info.CheckParameter()) return null;
            var url = InterfaceConst.ServiceAddRess;
            return await DeleteAsync(url, info, MethodBase.GetCurrentMethod().Name);
        }

        public async Task<Result> Deregisterinstance(Registerinstance nacos)
        {
            if (nacos.CheckParameter()) return null;
            var url = InterfaceConst.InstanceAddRess;
            return await DeleteAsync(url, nacos, MethodBase.GetCurrentMethod().Name);
        }

        public async Task<BaseModel> Getconfigurations(ConfigrationModel model,Type type)
        {
            if (model.CheckParameter())
            {
                return null;
            } 
           return await GetAsync<ConfigrationModel, BaseModel>(InterfaceConst.configurations, model, MethodBase.GetCurrentMethod().Name, type);
        }

        public async Task<Result> Modifyinstance(Registerinstance nacos)
        {
            if (nacos.CheckParameter()) return null;
            var url = InterfaceConst.InstanceAddRess;
            return await PutAsync(url, nacos, MethodBase.GetCurrentMethod().Name);
        }

        public async Task<QueryinstancedetailsModel> Queryinstancedetails(QueryInstanceDetail query)
        {
            if (query.CheckParameter()) return null;
            return await GetAsync<QueryInstanceDetail, QueryinstancedetailsModel>(InterfaceConst.InstanceAddRess, query, MethodBase.GetCurrentMethod().Name,typeof(QueryinstancedetailsModel));
        }

        public async Task<ServiceList> Queryinstances(QueryInstance query)
        {
            if (query.CheckParameter()) return null;
            var url = InterfaceConst.Queryinstances;
            var model = await GetAsync<QueryInstance, ServiceList>(url, query, MethodBase.GetCurrentMethod().Name, typeof(ServiceList));
            return model;
        }

        public async Task<QueryServiceModel> Queryservice(Queryservice info)
        {
            if (info.CheckParameter()) return null;
            return await GetAsync<Queryservice, QueryServiceModel>(InterfaceConst.ServiceAddRess, info, MethodBase.GetCurrentMethod().Name, typeof(QueryServiceModel));
        }

        public async Task<QueryServiceListModel> Queryservicelist(QueryServiceList query)
        {
            if (query.CheckParameter()) return null;
            return await GetAsync<QueryServiceList, QueryServiceListModel>(InterfaceConst.Queryservicelist, query, MethodBase.GetCurrentMethod().Name, typeof(QueryServiceListModel)); 
        }

        public  async Task<Result> Registerinstance(Registerinstance nacos)
        {
            if (nacos.CheckParameter()) return null;
            var url =InterfaceConst.InstanceAddRess;
           return await  PostAsync(url, nacos,MethodBase.GetCurrentMethod().Name);
        }

        public async Task<Result> Sendinstancebeat(SendInstabceBeat send)
        {
            if (send.CheckParameter()) return null;
            var url = InterfaceConst.Sendinstancebeat;
            return await PutAsync(url, send, MethodBase.GetCurrentMethod().Name);
        }

        public async Task<Result> Updateinstancehealthstatus(UpdateinstancehealthstatusQuery nacos)
        {
            if (nacos.CheckParameter()) return null;
            var url = InterfaceConst.UpdateServiceHelath;
            return await PutAsync(url, nacos, MethodBase.GetCurrentMethod().Name);
        }

        public async Task<Result> Updateservice(Updateservice info)
        {
            if (info.CheckParameter()) return null;
            var url = InterfaceConst.ServiceAddRess;
            return await PutAsync(url, info, MethodBase.GetCurrentMethod().Name);
        }
        private async Task<Result> PostAsync<T>(string sUrl, T nacos,string MethodName) where T:class
        {
            Result result = new Result { StatusCode=System.Net.HttpStatusCode.BadRequest,Message= "客户端请求中的语法错误" };
            try
            {
                var client = HttpClient.CreateClient("NacosConfig");
                var message = await client.PostAsync(sUrl, ExtendClass.SetMethodParameter(nacos));
                result.StatusCode = message.StatusCode;
                result.Message = await message.Content.ReadAsStringAsync();
                return result;
            }
            catch (HttpRequestException ex)
            {
                result.Message = ex.Message;
                Console.WriteLine(MethodName+"执行PostAsync请求出错"+":"+ ex.Message);
                return result;
            }
        }
        private async Task<TR> GetAsync<T,TR>(string sUrl, T nacos, string MethodName,Type type) where TR:class
        {
            try
            {
                var client = HttpClient.CreateClient("NacosConfig");
                var url = sUrl.GetUrl(ExtendClass.SetMethodParameter(nacos));
                var httpResponse = await client.GetAsync(url);
                var resultStr =await httpResponse.Content.ReadAsStringAsync();
                var result = resultStr.ToJson(type) as TR;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(MethodName + "执行GetAsync请求出错" + ":" + ex.Message);
                return null;
            }
        }
        private async Task<Result> DeleteAsync<T>(string sUrl, T nacos, string MethodName) where T : class
        {
            Result result = new Result { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "客户端请求中的语法错误" };
            try
            {
                var client = HttpClient.CreateClient("NacosConfig");
                var url = sUrl.GetUrl(ExtendClass.SetMethodParameter(nacos));
                var message = await client.DeleteAsync( url);
                result.StatusCode = message.StatusCode;
                result.Message = await message.Content.ReadAsStringAsync();
                return result;
            }
            catch (HttpRequestException ex)
            {
                result.Message = ex.Message;
                Console.WriteLine(MethodName + "执行DeleteAsync请求出错"+ ":" + ex.Message);
                return result;
            }
        }
        private async Task<Result> PutAsync<T>(string sUrl, T nacos, string MethodName) where T : class
        {
            Result result = new Result { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "客户端请求中的语法错误" };
            try
            {
                var client = HttpClient.CreateClient("NacosConfig");
                var message = await client.PutAsync(sUrl,ExtendClass.SetMethodParameter(nacos));
                result.StatusCode = message.StatusCode;
                result.Message = await message.Content.ReadAsStringAsync();
                return result;
            }
            catch (HttpRequestException ex)
            {
                result.Message = ex.Message;
                Console.WriteLine(MethodName + "执行PutAsync请求出错" + ":" + ex.Message);
                return result;
            }
        }
        //private HttpContent SetMethodParameter<T>(T nacos)
        //{
        //    Dictionary<string, string> dicParameter = new Dictionary<string, string>();
        //    foreach (var propertes in nacos.GetType().GetProperties())
        //    {
        //        if (propertes.GetValue(nacos) == null) continue;
        //        dicParameter.Add((propertes.GetCustomAttributes(true).FirstOrDefault() as DescriptionAttribute).Description, propertes.GetValue(nacos).ToString());
        //    }
        //    FormUrlEncodedContent form = new FormUrlEncodedContent(dicParameter);
        //    return form;
        //}
    }
}
