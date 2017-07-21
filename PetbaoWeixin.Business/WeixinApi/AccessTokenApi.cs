using System;
using PetbaoWeixin.Entities.WeixinApi;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.WeixinApi
{
    /// <summary>
    /// 获取AccessToken接口访问类
    /// </summary>
    public class AccessTokenApi : ApiBase
    {
        private const string apiUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        /// <summary>
        /// 获取AccessToken接口访问
        /// </summary>
        public AccessTokenApi()
            : base("GET")
        {
            base.Url = string.Format(apiUrl, Constants.PetbaoWeixinAppId, Constants.PetbaoWeixinAppSecret);
            base.PostData = "";
        }

        /// <summary>
        /// 访问接口
        /// </summary>
        /// <returns></returns>
        public AccessTokenResult Execute()
        {
            string resultTxt = this.HttpRequestString();
            resultTxt.Debug("=== AccessTokenApi.Result ===");

            if (!string.IsNullOrEmpty(resultTxt) && resultTxt.Contains("access_token"))
                return resultTxt.JsonDeserialize<AccessTokenResult>();

            return null;
        }
    }
}