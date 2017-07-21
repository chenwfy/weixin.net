using System;
using PetbaoWeixin.HttpCore;
using PetbaoWeixin.Business;
using CSharpLib.Common;

namespace PetbaoWeixin.Command.WeixinReq
{
    /// <summary>
    /// 微信服务器第一次接入验证命令请求执行类
    /// </summary>
    public class Verify : IExecute<DataContext>
    {
        /// <summary>
        /// 命令执行
        /// </summary>
        /// <param name="context"></param>
        public void Execute(DataContext context)
        {
            string echostr = "echostr".Request<string>(context.Context) ?? string.Empty;
            context.Flush(echostr);
        }
    }
}