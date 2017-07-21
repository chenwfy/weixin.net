using System;
using PetbaoWeixin.Entities.WeixinApi;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.WeixinApi
{
    /// <summary>
    /// 获取网页用户信息授权认证AccessToken接口
    /// </summary>
    public class AuthAccessTokenApi : ApiBase
    {
        private const string apiUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";

        /// <summary>
        /// 获取网页用户信息授权认证AccessToken接口
        /// </summary>
        /// <param name="code">用户授权临时CODE</param>
        public AuthAccessTokenApi(string code)
            : base("GET")
        {
            base.Url = string.Format(apiUrl, Constants.PetbaoWeixinAppId, Constants.PetbaoWeixinAppSecret, code);
            base.PostData = "";
        }

        /// <summary>
        /// 访问接口
        /// </summary>
        /// <returns></returns>
        public AuthAccessTokenResult Execute()
        {
            string resultTxt = this.HttpRequestString();
            resultTxt.Debug("=== AuthAccessTokenApi.Result ===");

            if (!string.IsNullOrEmpty(resultTxt) && resultTxt.Contains("access_token"))
                return resultTxt.JsonDeserialize<AuthAccessTokenResult>();
            return null;
        }
    }
}