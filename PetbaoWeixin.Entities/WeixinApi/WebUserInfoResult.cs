using System;
using Newtonsoft.Json;

namespace PetbaoWeixin.Entities.WeixinApi
{
    /// <summary>
    /// 网页SDK获取用户信息结果描述类
    /// </summary>
    public class WebUserInfoResult
    {
        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        [JsonProperty(PropertyName = "openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 用户的昵称
        /// </summary>
        [JsonProperty(PropertyName = "nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知 
        /// </summary>
        [JsonProperty(PropertyName = "sex")]
        public int Sex { get; set; }

        /// <summary>
        /// 用户所在城市
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        /// <summary>
        /// 用户所在省份
        /// </summary>
        [JsonProperty(PropertyName = "province")]
        public string Province { get; set; }

        /// <summary>
        /// 用户所在国家
        /// </summary>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
        /// </summary>
        [JsonProperty(PropertyName = "headimgurl")]
        public string HeadImgageUrl { get; set; }

        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。详见：获取用户个人信息（UnionID机制）
        /// </summary>
        [JsonProperty(PropertyName = "unionid")]
        public string UnionId { get; set; }

        /// <summary>
        /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom） 
        /// </summary>
        [JsonProperty(PropertyName = "privilege")]
        public string[] Privileges { get; set; }
    }
}