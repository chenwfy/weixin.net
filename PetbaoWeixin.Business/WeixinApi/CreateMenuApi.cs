using System;
using PetbaoWeixin.Entities.WeixinApi;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.WeixinApi
{
    /// <summary>
    /// 创建自定义菜单API访问类
    /// </summary>
    public class CreateMenuApi : ApiBase
    {
        private const string apiUrl = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}";

        /// <summary>
        /// 构造创建自定义菜单API访问实例
        /// </summary>
        /// <param name="menuJson">菜单数据JSON串</param>
        public CreateMenuApi(string menuJson)
            : base("POST")
        {
            base.Url = string.Format(apiUrl, AccessToken.Value);
            base.PostData = menuJson;
        }

        /// <summary>
        /// 执行接口访问
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {
            //先删除原有菜单
            new DeleteMenuApi().Execute();

            //创建新的菜单
            string resultTxt = this.HttpRequestString();
            resultTxt.Debug("=== CreateMenuApi.Result ===");

            if (!string.IsNullOrEmpty(resultTxt) && resultTxt.Contains("errcode"))
            {
                ErrorResult result = resultTxt.JsonDeserialize<ErrorResult>();
                return result.Code == 0;
            }

            return false;
        }
    }
}