using System;
using System.Collections.Generic;
using PetbaoWeixin.HttpCore;
using PetbaoWeixin.Business;
using PetbaoWeixin.Entities;
using CSharpLib.Common;

namespace PetbaoWeixin.Command.ReqWeixin
{
    /// <summary>
    /// 根据授权码和OPENID获取网页授权用户信息
    /// </summary>
    public class QueryUserBase : IExecute<PushContext>
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
                List<string> dataList = cmdData.ProtoBufDeserialize<List<string>>();
                string token = dataList[0];
                string openId = dataList[1];

                UserBaseInfo userInfo = ApiSupportBiz.GetUserInfoByTokenAndOpenId(token, openId);
                if (null != userInfo)
                {
                    userInfo.OpenId = openId;
                    context.Flush<UserBaseInfo>(userInfo);
                }

            }
            context.Flush("Failed!");
        }
    }
}