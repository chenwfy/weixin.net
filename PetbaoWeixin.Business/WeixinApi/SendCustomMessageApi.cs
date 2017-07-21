using System;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.WeixinApi
{
    /// <summary>
    /// 发送客服消息接口
    /// </summary>
    public class SendCustomMessageApi : ApiBase
    {
        private const string apiUrl = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";

        /// <summary>
        /// 获取网页用户信息授权认证AccessToken接口
        /// </summary>
        /// <param name="messageData">用户授权临时CODE</param>
        public SendCustomMessageApi(string messageData)
            : base("POST")
        {
            base.Url = string.Format(apiUrl, AccessToken.Value);
            base.PostData = messageData;
        }

        /// <summary>
        /// 访问接口
        /// </summary>
        /// <returns></returns>
        public void Execute()
        {
            string resultTxt = this.HttpRequestString();
            resultTxt.Debug("=== SendCustomMessageApi.Result ===");
        }
    }
}