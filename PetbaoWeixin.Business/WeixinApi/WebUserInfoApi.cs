using System;
using PetbaoWeixin.Entities.WeixinApi;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.WeixinApi
{
    /// <summary>
    /// 网页SDK获取用户信息接口
    /// </summary>
    public class WebUserInfoApi : ApiBase
    {
        private const string apiUrl = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN";

        /// <summary>
        /// 网页SDK获取用户信息接口访问
        /// </summary>
        public WebUserInfoApi(string token, string openId)
            : base("GET")
        {
            base.Url = string.Format(apiUrl, token, openId);
            base.PostData = "";
        }

        /// <summary>
        /// 访问接口
        /// </summary>
        /// <returns></returns>
        public WebUserInfoResult Execute()
        {
            string resultTxt = this.HttpRequestString();
            resultTxt.Debug("=== WebUserInfoApi.Result ===");

            if (!string.IsNullOrEmpty(resultTxt) && resultTxt.Contains("openid"))
                return resultTxt.JsonDeserialize<WebUserInfoResult>();

            return null;
        }
    }
}