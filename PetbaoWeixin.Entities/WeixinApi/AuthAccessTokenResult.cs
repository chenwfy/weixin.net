using System;
using Newtonsoft.Json;

namespace PetbaoWeixin.Entities.WeixinApi
{
    /// <summary>
    /// 获取网页用户信息授权认证AccessToken接口结果信息描述类
    /// </summary>
    public class AuthAccessTokenResult
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

        /// <summary>
        /// 用户刷新access_token 
        /// </summary>
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// 用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID 
        /// </summary>
        [JsonProperty(PropertyName = "openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔 
        /// </summary>
        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }
    }
}