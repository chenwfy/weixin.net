using System;
using PetbaoWeixin.Entities.WeixinApi;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.WeixinApi
{
    /// <summary>
    /// 删除自定义菜单API访问类
    /// </summary>
    public class DeleteMenuApi : ApiBase
    {
        private const string apiUrl = "https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}";

        /// <summary>
        /// 构造删除自定义菜单API访问实例
        /// </summary>
        public DeleteMenuApi()
            : base("GET")
        {
            base.Url = string.Format(apiUrl, AccessToken.Value);
            base.PostData = string.Empty;
        }

        /// <summary>
        /// 执行接口访问
        /// </summary>
        public void Execute()
        {
            string resultTxt = this.HttpRequestString();
            resultTxt.Debug("=== DeleteMenuApi.Result ===");
        }
    }
}