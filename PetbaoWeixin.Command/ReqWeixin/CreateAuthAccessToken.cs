using System;
using PetbaoWeixin.HttpCore;
using PetbaoWeixin.Business;
using PetbaoWeixin.Entities;

namespace PetbaoWeixin.Command.ReqWeixin
{
    /// <summary>
    /// 获取网页用户信息授权认证AccessToken命令请求处理类
    /// </summary>
    public class CreateAuthAccessToken : IExecute<PushContext>
    {
        /// <summary>
        /// 命令执行
        /// </summary>
        /// <param name="context"></param>
        public void Execute(PushContext context)
        {
            byte[] cmdData = context.CmdData;
            if (cmdData != null && cmdData.Length > 0)
            {
                string code = System.Text.Encoding.UTF8.GetString(cmdData);
                AuthToken token = ApiSupportBiz.GetAuthAccessToken(code);
                if (token != null)
                {
                    context.Flush<AuthToken>(token);
                    return;
                }
            }
            context.Flush("Failed!");
        }
    }
}