using System;
using PetbaoWeixin.HttpCore;
using PetbaoWeixin.Business;
using PetbaoWeixin.Entities;
using CSharpLib.Common;

namespace PetbaoWeixin.Command.ReqWeixin
{
    /// <summary>
    /// 下载媒体文件命令
    /// </summary>
    public class DownloadMedia : IExecute<PushContext>
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
                string mediaId = System.Text.Encoding.UTF8.GetString(cmdData);
                mediaId.Debug("===ReqWeixin.DownloadMedia 请求数据===");

                byte[] data = ApiSupportBiz.DownloadMediaFile(mediaId);
                context.Flush(data);
            }

            context.Flush("Failed!");
        }
    }
}