using System;
using System.Xml;
using PetbaoWeixin.Utility;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.Message
{
    /// <summary>
    /// 微信服务器转发过来的扫描带参数二维码事件（用户已关注时的事件推送）消息类
    /// </summary>
    public class ReceiveEventScanMessage : MessageBase
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
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReceiveEventScanMessage()
            :base("event")
        {
            this.Event = "SCAN";
            this.EventKey = "";
            this.Ticket = "";
        }
    }
}