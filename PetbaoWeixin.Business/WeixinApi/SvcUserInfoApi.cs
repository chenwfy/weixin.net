using System;
using PetbaoWeixin.Entities.WeixinApi;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.WeixinApi
{
    /// <summary>
    /// 服务器端接口获取用户信息接口
    /// </summary>
    public class SvcUserInfoApi : ApiBase
    {
        private const string apiUrl = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN";

        /// <summary>
        /// 服务器端接口获取用户信息接口访问
        /// </summary>
        public SvcUserInfoApi(string openId)
            : base("GET")
        {
            base.Url = string.Format(apiUrl, AccessToken.Value, openId);
            base.PostData = "";
        }

        /// <summary>
        /// 访问接口
        /// </summary>
        /// <returns></returns>
        public SvcUserInfoResult Execute()
        {
            string resultTxt = this.HttpRequestString();
            resultTxt.Debug("=== SvcUserInfoApi.Result ===");

            if (!string.IsNullOrEmpty(resultTxt) && !resultTxt.Contains("errcode"))
                return resultTxt.JsonDeserialize<SvcUserInfoResult>();

            return null;
        }
    }
}