using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace UseNacos
{
    public static class ExtendClass
    {
        /// <summary>
        /// 字符串转实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T ToJson<T>(this string str) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch (Exception ex)
            {
                Console.WriteLine(MethodInfo.GetCurrentMethod().Name + ":" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 字符串转对应类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ToJson(this string str, Type type)
        {
            try
            {
                return JsonConvert.DeserializeObject(str, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(MethodInfo.GetCurrentMethod().Name + ":" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 设置Url参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nacos"></param>
        /// <returns></returns>
        public static HttpContent SetMethodParameter<T>(T nacos)
        {
            try
            {
                if (nacos == null) return null;
                Dictionary<string, string> dicParameter = new Dictionary<string, string>();
                foreach (var propertes in nacos.GetType().GetProperties())
                {
                    if (propertes.GetValue(nacos) == null) continue;
                    if (propertes.GetCustomAttributes(true).Length > 0)
                    {
                        dicParameter.Add((propertes.GetCustomAttributes(true).FirstOrDefault() as DescriptionAttribute).Description, propertes.GetValue(nacos).ToString());
                    }
                    else
                    {
                        dicParameter.Add(propertes.Name, propertes.GetValue(nacos).ToString());
                    }
                }
                FormUrlEncodedContent form = new FormUrlEncodedContent(dicParameter);
                return form;
            }
            catch (Exception ex)
            {
                Console.WriteLine(MethodInfo.GetCurrentMethod().Name + ":" + ex.Message);
                return null;
            }
        }
        public static HttpClient GetHttpClient(IHttpClientFactory httpClientFactory, String Name, Hosts hosts, bool bIsEnableSSL)
        {
            try
            {
                if (hosts == null)
                {
                    Console.WriteLine(MethodInfo.GetCurrentMethod().Name + ":Host不能为空，无法负载均衡");
                    throw new NullReferenceException("Service Is Null,Can't Use LoadBalances");
                }
                var client = httpClientFactory.CreateClient(Name);
                if (bIsEnableSSL)
                {
                    client.BaseAddress = new Uri("https://" + hosts.Ip + ":" + hosts.Port);
                }
                else
                {
                    client.BaseAddress = new Uri("http://" + hosts.Ip + ":" + hosts.Port);
                }
                return client;
            }
            catch (Exception ex)
            {
                Console.WriteLine(MethodInfo.GetCurrentMethod().Name + ":" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 设置url参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nacos"></param>
        /// <returns></returns>
        public static HttpContent SetMethodJsonParameter<T>(T nacos)
        {
            try
            {
                if (nacos == null) return null;
                StringContent form = new StringContent(JsonConvert.SerializeObject(nacos));
                return form;
            }
            catch (Exception ex)
            {
                Console.WriteLine(MethodInfo.GetCurrentMethod().Name + ":" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// Get请求Url转换
        /// </summary>
        /// <param name="sUrl"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetUrl(this string sUrl, HttpContent content)
        {
            try
            {
                if (content == null)
                {
                    return sUrl;
                }
                return sUrl + "?" + content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(MethodInfo.GetCurrentMethod().Name + ":" + ex.Message);
                return null;
            }
        }
    }
}
