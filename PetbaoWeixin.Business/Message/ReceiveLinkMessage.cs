using System;
using System.Xml;
using PetbaoWeixin.Utility;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.Message
{
    /// <summary>
    /// 微信服务器转发过来的链接消息类
    /// </summary>
    public class ReceiveLinkMessage : MessageBase
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 消息编号
        /// </summary>
        public string MsgId { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReceiveLinkMessage()
            : base("link")
        { 
        }
    }
}