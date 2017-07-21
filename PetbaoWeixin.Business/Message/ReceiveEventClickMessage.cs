using System;
using System.Xml;
using PetbaoWeixin.Utility;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.Message
{
    /// <summary>
    /// 微信服务器转发过来的点击菜单拉取消息时的事件推送消息类
    /// </summary>
    public class ReceiveEventClickMessage : MessageBase
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应 
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReceiveEventClickMessage()
            : base("event")
        {
            this.Event = "CLICK";
        }

        /// <summary>
        /// 构建接收的消息结构
        /// </summary>
        /// <param name="xmlRoot">原始消息XML根节点对象</param>
        public override void Build(XmlNode xmlRoot)
        {
            base.Build(xmlRoot);
            EventKey = xmlRoot["EventKey"].InnerText;
        }

        /// <summary>
        /// 获取回复消息内容
        /// </summary>
        /// <returns></returns>
        public override string GetReply()
        {
            string reply = string.Empty;

            switch (EventKey)
            {
                case "FindByPosition":
                    reply = new ReplyTextMessage(this.FromUserName, this.ToUserName, "直接发送“位置”消息给我，即可查看您当前位置的周边信息！").ToString();
                    break;
                case "FeedBack":
                    reply = new ReplyTextMessage(this.FromUserName, this.ToUserName, "如果您对我们有任何疑问或建议，请直接发送“反馈+反馈建议”给我即可！").ToString();
                    break;
                case "FavoritedOrShare":
                    reply = "";
                    break;
                default:
                    reply = new ReplyTextMessage(this.FromUserName, this.ToUserName, "谢谢！").ToString();
                    break;
            }

            return reply;
        }
    }
}