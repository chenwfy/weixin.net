using System;
using System.Xml;
using PetbaoWeixin.Utility;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.Message
{
    /// <summary>
    /// 回复文本消息类
    /// </summary>
    public class ReplyTextMessage : MessageBase
    {
        #region 消息内容XML模板

        /// <summary>
        /// 回复文本消息内容模板
        /// </summary>
        private const string Template = @"<xml>
<ToUserName><![CDATA[{0}]]></ToUserName>
<FromUserName><![CDATA[{1}]]></FromUserName>
<CreateTime>{2}</CreateTime>
<MsgType><![CDATA[{3}]]></MsgType>
<Content><![CDATA[{4}]]></Content>
</xml>";

        #endregion

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReplyTextMessage()
            : base("text")
        {
        }

        /// <summary>
        /// 实例化一个文本消息回复内容结构
        /// </summary>
        /// <param name="toUserName">消息接收人标示</param>
        /// <param name="fromUserName">消息发送人标示</param>
        /// <param name="content">消息内容</param>
        public ReplyTextMessage(string toUserName, string fromUserName, string content)
            : this()
        {
            this.ToUserName = toUserName;
            this.FromUserName = fromUserName;
            this.Content = content;
            this.CreateTime = Utilities.GetNowSeconds();
        }

        /// <summary>
        /// 重写ToString方法，返回消息内容组装完成后的XML内容
        /// </summary>
        /// <returns>消息内容组装完成后的XML内容</returns>
        public override string ToString()
        {
            return string.Format(Template, this.ToUserName, this.FromUserName, this.CreateTime, this.MsgType, this.Content);
        }
    }
}