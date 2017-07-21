using System;
using PetbaoWeixin.HttpCore;
using PetbaoWeixin.Business;
using PetbaoWeixin.Entities;

namespace PetbaoWeixin.Command.ReqWeixin
{
    /// <summary>
    /// 创建授权认证URL命令处理执行类
    /// </summary>
    public class CreateAuthUrl : IExecute<PushContext>
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
                string org = System.Text.Encoding.UTF8.GetString(cmdData);
                string authUrl = ApiSupportBiz.GetAuthUrl(org);
                context.Flush(System.Text.Encoding.UTF8.GetBytes(authUrl));
            }
            else
            {
                context.Flush("Failed!");
            }
        }
    }
}