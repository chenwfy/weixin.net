using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using PetbaoWeixin.Utility;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.Message
{
    public class ReplyNewsMessage : MessageBase
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
<ArticleCount>{4}</ArticleCount>
<Articles>{5}</Articles>
</xml>";

        /// <summary>
        /// 内容条目子模板内容
        /// </summary>
        private const string ItemTemplate = @"<item>
<Title><![CDATA[{0}]]></Title> 
<Description><![CDATA[{1}]]></Description>
<PicUrl><![CDATA[{2}]]></PicUrl>
<Url><![CDATA[{3}]]></Url>
</item>";

        /// <summary>
        /// 创建内容
        /// </summary>
        /// <param name="newsItem"></param>
        /// <returns></returns>
        private static string BuildContent(ICollection<NewsMessageItem> newsItem)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in newsItem)
            {
                builder.AppendFormat(ItemTemplate, item.Title, item.Description, item.PicUrl, item.Url); 
            }
            return builder.ToString();
        }

        #endregion

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 内容条总数
        /// </summary>
        public int ItemCount { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReplyNewsMessage()
            : base("news")
        {
        }

        /// <summary>
        /// 实例化一个文本消息回复内容结构
        /// </summary>
        /// <param name="toUserName">消息接收人标示</param>
        /// <param name="fromUserName">消息发送人标示</param>
        /// <param name="newsItem">消息内容</param>
        public ReplyNewsMessage(string toUserName, string fromUserName, ICollection<NewsMessageItem> newsItem)
            : this()
        {
            this.ToUserName = toUserName;
            this.FromUserName = fromUserName;
            this.Content = BuildContent(newsItem);
            this.ItemCount = newsItem.Count;
            this.CreateTime = Utilities.GetNowSeconds();
        }

        /// <summary>
        /// 重写ToString方法，返回消息内容组装完成后的XML内容
        /// </summary>
        /// <returns>消息内容组装完成后的XML内容</returns>
        public override string ToString()
        {
            return string.Format(Template, this.ToUserName, this.FromUserName, this.CreateTime, this.MsgType, this.ItemCount, this.Content);
        }
    }

    /// <summary>
    /// 新闻（图文）消息条目信息描述
    /// </summary>
    public class NewsMessageItem
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图片URL
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 内容链接URL
        /// </summary>
        public string Url { get; set; }
    }
}