using System;
using System.Xml;
using PetbaoWeixin.Utility;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.Message
{
    /// <summary>
    /// 微信服务器转发过来的关注事件消息类
    /// </summary>
    public class ReceiveEventUnsubscribeMessage : MessageBase
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 事件标示码
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReceiveEventUnsubscribeMessage()
            :base("event")
        {
            this.Event = "unsubscribe";
            this.EventKey = "";
        }

        /// <summary>
        /// 执行消息后续逻辑动作
        /// </summary>
        public override void ExecuteLogic()
        {
            //从业务系统中取消当前用户关注记录
            ApiSupportBiz.RemoveWxUserSubscribeLog(base.FromUserName);
        }
    }
}