using System;
using Newtonsoft.Json;

namespace PetbaoWeixin.Entities.WeixinApi
{
    /// <summary>
    /// AccessToken接口响应结果
    /// </summary>
    public class AccessTokenResult
    {
        /// <summary>
        /// AccessToken内容
        /// </summary>
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// AccessToken过期时间，单位：秒
        /// </summary>
        [JsonProperty(PropertyName = "expires_in")]
        public int Expired { get; set; }
    }
}