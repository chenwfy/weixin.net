using System;
using System.Xml;
using PetbaoWeixin.Entities;

namespace PetbaoWeixin.Business.Message
{
    /// <summary>
    /// 消息基类
    /// </summary>
    public abstract class MessageBase
    {
        /// <summary>
        /// 消息接收人标示（接收消息时为 开发者微信号；回复或主动发送消息时为用户OpenID）
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 消息发送人标示（接收消息时为发送人的OpenID；回复或主动发送消息时为开发者微信号） 
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息发送时间
        /// </summary>
        public long CreateTime { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MessageBase()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msgType">消息类型</param>
        public MessageBase(string msgType)
        {
            this.MsgType = msgType;
        }

        /// <summary>
        /// 构建接收的消息结构
        /// </summary>
        /// <param name="xmlRoot">原始消息XML根节点对象</param>
        public virtual void Build(XmlNode xmlRoot)
        {
            ToUserName = xmlRoot["ToUserName"].InnerText;
            FromUserName = xmlRoot["FromUserName"].InnerText;
            CreateTime = Convert.ToInt64(xmlRoot["CreateTime"].InnerText);
            MsgType = xmlRoot["MsgType"].InnerText;
        }

        /// <summary>
        /// 执行消息后续逻辑动作
        /// </summary>
        public virtual void ExecuteLogic()
        { 
        }

        /// <summary>
        /// 获取回复消息内容
        /// </summary>
        /// <returns></returns>
        public virtual string GetReply()
        {
            WxDefaultMessage defaultMessage = MessageBiz.GetWxDefaultMessage();
            if (null != defaultMessage && !string.IsNullOrEmpty(defaultMessage.DefaultReplyMessage))
            {
                ReplyTextMessage replyTextMessage = new ReplyTextMessage(this.FromUserName, this.ToUserName, defaultMessage.DefaultReplyMessage);
                return replyTextMessage.ToString();
            }
            return string.Empty;
        }
    }
}