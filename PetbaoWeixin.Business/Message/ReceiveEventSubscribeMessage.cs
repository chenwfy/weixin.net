using System;
using System.Xml;
using PetbaoWeixin.Entities;
using PetbaoWeixin.Utility;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.Message
{
    /// <summary>
    /// 微信服务器转发过来的关注事件消息（包括扫描带场景的二维码进行关注事件消息）类
    /// </summary>
    public class ReceiveEventSubscribeMessage : MessageBase
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
        public ReceiveEventSubscribeMessage()
            :base("event")
        {
            this.Event = "subscribe";
            this.EventKey = "";
            this.Ticket = "";
        }

        /// <summary>
        /// 构建接收的消息结构
        /// </summary>
        /// <param name="xmlRoot">原始消息XML根节点对象</param>
        public override void Build(XmlNode xmlRoot)
        {
            base.Build(xmlRoot);
            EventKey = xmlRoot["EventKey"].InnerText;
            if (xmlRoot["Ticket"] != null)
            {
                Ticket = xmlRoot["Ticket"].InnerText;
            }
        }

        /// <summary>
        /// 执行消息后续逻辑动作
        /// </summary>
        public override void ExecuteLogic()
        {
            //将当前关注公众号的用户注册到业务系统中
            ApiSupportBiz.RegisterNewUserForBusiness(base.FromUserName);
            ApiSupportBiz.CreateWxUserSubscribeLog(base.FromUserName);
        }

        /// <summary>
        /// 获取回复消息内容
        /// </summary>
        /// <returns></returns>
        public override string GetReply()
        {
            WxDefaultMessage defaultMessage = MessageBiz.GetWxDefaultMessage();
            if (null != defaultMessage && !string.IsNullOrEmpty(defaultMessage.WelComeMessage))
            {
                ReplyTextMessage replyTextMessage = new ReplyTextMessage(this.FromUserName, this.ToUserName, defaultMessage.WelComeMessage);
                return replyTextMessage.ToString();
            }
            return base.GetReply();
        }        
    }
}