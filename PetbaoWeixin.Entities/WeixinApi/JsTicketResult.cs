using System;
using Newtonsoft.Json;

namespace PetbaoWeixin.Entities.WeixinApi
{
    /// <summary>
    /// JS票据获取接口返回信息类
    /// </summary>
    public class JsTicketResult
    {
        /// <summary>
        /// 响应状态码
        /// </summary>
        [JsonProperty(PropertyName = "errcode")]
        public int Code { get; set; }

        /// <summary>
        /// 响应消息
        /// </summary>
        [JsonProperty(PropertyName = "errmsg")]
        public string Message { get; set; }

        /// <summary>
        /// 票据值
        /// </summary>
        [JsonProperty(PropertyName = "ticket")]
        public string Ticket { get; set; }

        /// <summary>
        /// 票据过期时间，单位：秒
        /// </summary>
        [JsonProperty(PropertyName = "expires_in")]
        public int Expired { get; set; }
    }
}
