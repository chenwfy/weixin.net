using System;
using PetbaoWeixin.Entities.WeixinApi;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.WeixinApi
{
    /// <summary>
    /// JS接口票据获取类
    /// </summary>
    public class JsTicketApi : ApiBase
    {
        private const string apiUrl = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi";

        /// <summary>
        /// 获取AccessToken接口访问
        /// </summary>
        public JsTicketApi()
            : base("GET")
        {
            base.Url = string.Format(apiUrl, AccessToken.Value);
            base.PostData = "";
        }

        /// <summary>
        /// 访问接口
        /// </summary>
        /// <returns></returns>
        public JsTicketResult Execute()
        {
            string resultTxt = this.HttpRequestString();
            resultTxt.Debug("=== JsTicketApi.Result ===");

            if (!string.IsNullOrEmpty(resultTxt) && resultTxt.Contains("ticket"))
                return resultTxt.JsonDeserialize<JsTicketResult>();
            return null;
        }
    }
}