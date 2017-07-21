using System;
using PetbaoWeixin.HttpCore;
using PetbaoWeixin.Business;
using CSharpLib.Common;

namespace PetbaoWeixin.Command.ReqWeixin
{
    /// <summary>
    /// 发送客服消息命令执行处理类
    /// </summary>
    public class SendCustomMessage : IExecute<PushContext>
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
                string message = System.Text.Encoding.UTF8.GetString(cmdData);
                message.Debug("=== ReqWeixin.SendCustomMessage 请求数据 ===");
                ApiSupportBiz.SendCustomMessage(message);
                context.Flush();
                return;
            }
            context.Flush("Failed!");
        }
    }
}