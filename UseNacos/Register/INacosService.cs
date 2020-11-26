using System;
using System.Threading.Tasks;

namespace UseNacos
{
  public  interface INacosService
    {
        /// <summary>
        /// 获取Nacos配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<BaseModel> Getconfigurations(ConfigrationModel model,Type type);
        /// <summary>
        /// 注册实例
        /// </summary>
        /// <param name="nacos"></param>
        /// <returns></returns>
        Task<Result> Registerinstance(Registerinstance nacos);
        /// <summary>
        /// 注销实例
        /// </summary>
        /// <param name="nacos"></param>
        /// <returns></returns>
        Task<Result> Deregisterinstance(Registerinstance nacos);
        /// <summary>
        /// 修改实例
        /// </summary>
        /// <param name="nacos"></param>
        /// <returns></returns>
        Task<Result> Modifyinstance(Registerinstance nacos);
        /// <summary>
        /// 查询实例列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ServiceList> Queryinstances(QueryInstance query);
        /// <summary>
        /// 查询实例详情
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<QueryinstancedetailsModel> Queryinstancedetails(QueryInstanceDetail query);
        /// <summary>
        /// 发送实例心跳
        /// </summary>
        /// <returns></returns>
        Task<Result> Sendinstancebeat(SendInstabceBeat send);
        /// <summary>
        /// 创建服务
        /// </summary>
        /// <returns></returns>
        Task<Result> Createservice(Createservice info);
        /// <summary>
        /// 删除服务
        /// </summary>
        /// <returns></returns>
        Task<Result> Deleteservice(Deleteservice info);
        /// <summary>
        /// 修改服务
        /// </summary>
        /// <returns></returns>
        Task<Result> Updateservice(Updateservice info);
        /// <summary>
        /// 查询服务
        /// </summary>
        /// <returns></returns>
        Task<QueryServiceModel> Queryservice(Queryservice info);
        /// <summary>
        /// 查询服务列表
        /// </summary>
        /// <returns></returns>
        Task<QueryServiceListModel> Queryservicelist(QueryServiceList query);
        /// <summary>
        /// 更新实例的健康状态
        /// </summary>
        /// <returns></returns>
        Task<Result> Updateinstancehealthstatus(UpdateinstancehealthstatusQuery nacos);
    }
}
