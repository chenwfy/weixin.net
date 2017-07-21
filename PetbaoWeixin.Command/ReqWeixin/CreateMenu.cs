using System;
using PetbaoWeixin.HttpCore;
using PetbaoWeixin.Business;

namespace PetbaoWeixin.Command.ReqWeixin
{
    /// <summary>
    /// 设置或创建自定义菜单命令执行处理类
    /// </summary>
    public class CreateMenu : IExecute<PushContext>
    {
        /// <summary>
        /// 命令执行
        /// </summary>
        /// <param name="context"></param>
        public void Execute(PushContext context)
        {
            bool result = ApiSupportBiz.CreateMenu();
            if (result)
                context.Flush();
            else
                context.Flush("Failed!");
        }
    }
}