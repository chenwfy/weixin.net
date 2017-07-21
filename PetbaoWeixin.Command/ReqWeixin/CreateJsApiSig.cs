using System;
using PetbaoWeixin.HttpCore;
using PetbaoWeixin.Business;
using PetbaoWeixin.Entities;

namespace PetbaoWeixin.Command.ReqWeixin
{
    /// <summary>
    /// 获取JSSKD签名命令执行处理类
    /// </summary>
    public class CreateJsApiSig : IExecute<PushContext>
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
                string pageUrl = System.Text.Encoding.UTF8.GetString(cmdData);
                JsApiSig sig = ApiSupportBiz.CreateJsApiSig(pageUrl);
                if (null != sig)
                {
                    context.Flush<JsApiSig>(sig);
                    return;
                }
            }

            context.Flush("Failed!");
        }
    }
}