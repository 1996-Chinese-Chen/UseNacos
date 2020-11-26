using System;
using System.Collections.Generic;
using System.Text;

namespace UseNacos
{
   public static class InterfaceConst
    {
        /// <summary>
        /// 注册地址
        /// </summary>
        public const string InstanceAddRess = "/nacos/v1/ns/instance";
        /// <summary>
        /// 配置地址
        /// </summary>
        public const string configurations = "/nacos/v1/cs/configs";
        /// <summary>
        /// 查询实例列表
        /// </summary>
        public const string Queryinstances = "/nacos/v1/ns/instance/list";
        /// <summary>
        /// 发送心跳
        /// </summary>
        public const string Sendinstancebeat = "/nacos/v1/ns/instance/beat";
        /// <summary>
        /// 服务地址
        /// </summary>
        public const string ServiceAddRess = "/nacos/v1/ns/service";
        /// <summary>
        /// 查询服务列表
        /// </summary>
        public const string Queryservicelist = "/nacos/v1/ns/service/list";
        /// <summary>
        /// 更新实例健康状态
        /// </summary>
        public const string UpdateServiceHelath = "/nacos/v1/ns/health/instance";
    }
}
