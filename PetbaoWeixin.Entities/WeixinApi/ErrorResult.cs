using System;
using Newtonsoft.Json;

namespace PetbaoWeixin.Entities.WeixinApi
{
    /// <summary>
    /// 错误响应结果类
    /// </summary>
    public class ErrorResult
    {
        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty(PropertyName = "errcode")]
        public int Code { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        [JsonProperty(PropertyName = "errmsg")]
        public string Message { get; set; }
    }
}