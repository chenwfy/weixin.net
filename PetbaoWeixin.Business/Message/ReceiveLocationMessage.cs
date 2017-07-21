using System;
using System.Xml;
using System.Collections.Generic;
using PetbaoWeixin.Utility;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.Message
{
    /// <summary>
    /// 微信服务器转发过来的地理位置消息类
    /// </summary>
    public class ReceiveLocationMessage : MessageBase
    {
        /// <summary>
        /// 地理位置维度
        /// </summary>
        public decimal Location_X { get; set; }
        
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public decimal Location_Y { get; set; }
        
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public double Scale { get; set; }
        
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 消息编号
        /// </summary>
        public string MsgId { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReceiveLocationMessage()
            : base("location")
        { 
        }

        /// <summary>
        /// 构建接收的消息结构
        /// </summary>
        /// <param name="xmlRoot">原始消息XML根节点对象</param>
        public override void Build(XmlNode xmlRoot)
        {
            base.Build(xmlRoot);
            Location_X = Convert.ToDecimal(xmlRoot["Location_X"].InnerText);
            Location_Y = Convert.ToDecimal(xmlRoot["Location_Y"].InnerText);
            Scale = Convert.ToDouble(xmlRoot["Scale"].InnerText);
            Label = xmlRoot["Label"].InnerText;
        }

        /// <summary>
        /// 获取回复消息内容
        /// </summary>
        /// <returns></returns>
        public override string GetReply()
        {
            PetbaoWeixinWeb.RemoteService.DataService.BaiduPlaceApiPetResult result = PetbaoWeixinWeb.RemoteService.DataService.DataQuery.GetRoundByPlaceHolder((double)this.Location_Y, (double)this.Location_X);
            if (null != result && result.Data.Length > 0)
            {
                List<NewsMessageItem> itemList = new List<NewsMessageItem>(0);
                PetbaoWeixinWeb.RemoteService.DataService.PetDetail detail = null;
                int idx = 0;
                foreach (var item in result.Data)
                {
                    if (!string.IsNullOrEmpty(item.Name))
                    {
                        detail = item.Detail;
                        string picUrl = idx == 0 ? "http://files.chongwubao.com.cn/Files/pos_320_200.jpg" : string.Empty;

                        NewsMessageItem newsItem = new NewsMessageItem
                        {
                            Title = item.Name + ((null != detail && detail.Distance > 0) ? string.Format("（约{0}米）", detail.Distance) : string.Empty),
                            Description = item.Address ?? string.Empty,
                            PicUrl = picUrl,
                            Url = null != detail ? detail.Url : string.Empty
                        };
                        itemList.Add(newsItem);
                        idx++;
                    }
                }

                if (itemList.Count > 0)
                    return new ReplyNewsMessage(this.FromUserName, this.ToUserName, itemList).ToString();
            }
            return base.GetReply();
        }
    }
}