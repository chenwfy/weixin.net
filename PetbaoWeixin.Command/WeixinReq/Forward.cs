using System;
using PetbaoWeixin.HttpCore;
using PetbaoWeixin.Business;
using CSharpLib.Common;

namespace PetbaoWeixin.Command.WeixinReq
{
    /// <summary>
    /// 接收并处理微信服务器转发的消息命令请求执行类
    /// </summary>
    public class Forward : IExecute<DataContext>
    {
        /// <summary>
        /// 命令执行
        /// </summary>
        /// <param name="context"></param>
        public void Execute(DataContext context)
        {
            if (Compiled.Debug)
            {
                context.QueryBody.Debug("===WeixinReq.Forward.QueryBody===");
            }

            if (string.IsNullOrEmpty(context.QueryBody))
            {
                context.Flush("ERROR");
                return;
            }

            //消息加密与签名暂不处理

            context.Flush(context.QueryBody.ReceiveForwardMessageAndReply());
        }
    }
}