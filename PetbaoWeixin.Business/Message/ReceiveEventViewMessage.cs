using System;
using System.Xml;
using PetbaoWeixin.Utility;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.Message
{
    /// <summary>
    /// 微信服务器转发过来的点击菜单跳转链接时的事件推送消息类
    /// </summary>
    public class ReceiveEventViewMessage : MessageBase
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 事件KEY值，设置的跳转URL
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReceiveEventViewMessage()
            : base("event")
        {
            this.Event = "VIEW";
            this.EventKey = "";
        }
    }
}