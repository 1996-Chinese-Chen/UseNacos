using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UseNacos
{
   public interface IHttpClient
    {
       /// <summary>
       /// Get请求
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <typeparam name="TR"></typeparam>
       /// <param name="Url"></param>
       /// <param name="tParameter"></param>
       /// <returns></returns>
        Task<T> GetAsync<T,TR>(string Url,TR tParameter,QueryInstance query) where T:class where TR:class;
        /// <summary>
        /// Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="Url"></param>
        /// <param name="tR"></param>
        /// <returns></returns>
        Task<T> PostAsync<T,TR>(string Url,TR tR, QueryInstance query) where T: class where TR:class;
        /// <summary>
        /// Delete请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="Url"></param>
        /// <param name="tParameter"></param>
        /// <returns></returns>
        Task<T> DeleteAsync<T,TR>(string Url,TR tParameter, QueryInstance query) where T : class where TR : class;
        /// <summary>
        /// Put请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="Url"></param>
        /// <param name="tParameter"></param>
        /// <returns></returns>
        Task<T> PutAsync<T, TR>(string Url , TR tParameter, QueryInstance query) where T : class where TR : class;
    }
}
