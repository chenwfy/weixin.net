using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetbaoWeixin.Business.WeixinApi
{
    /// <summary>
    /// 微信接口基类
    /// </summary>
    public abstract class ApiBase
    {
        /// <summary>
        /// 接口URL
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// 访问方法：POST/GET/HEAD
        /// </summary>
        public string Method { get; private set; }

        /// <summary>
        /// 如果为POST访问，POST数据内容
        /// </summary>
        public string PostData { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public ApiBase()
            : this("GET")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        public ApiBase(string method)
        {
            this.Method = method;
        }
    }
}