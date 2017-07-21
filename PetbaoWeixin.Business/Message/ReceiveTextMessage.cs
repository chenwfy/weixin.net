using System;
using System.Xml;
using PetbaoWeixin.Utility;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.Message
{
    /// <summary>
    /// 微信服务器转发过来的文本消息类
    /// </summary>
    public class ReceiveTextMessage : MessageBase
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息编号
        /// </summary>
        public string MsgId { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReceiveTextMessage()
            : base("text")
        {
        }

        /// <summary>
        /// 构建接收的消息结构
        /// </summary>
        /// <param name="xmlRoot">原始消息XML根节点对象</param>
        public override void Build(XmlNode xmlRoot)
        {
            base.Build(xmlRoot);
            Content = xmlRoot["Content"].InnerText;
            MsgId = xmlRoot["MsgId"].InnerText;
        }

        /// <summary>
        /// 获取回复消息内容
        /// </summary>
        /// <returns></returns>
        public override string GetReply()
        {
            if (Content.StartsWith("反馈"))
                return new ReplyTextMessage(this.FromUserName, this.ToUserName, "谢谢您的反馈！").ToString();
            return base.GetReply();
        }
    }
}