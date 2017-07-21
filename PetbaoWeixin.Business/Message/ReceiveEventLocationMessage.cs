using System;
using System.Xml;
using PetbaoWeixin.Utility;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.Message
{
    /// <summary>
    /// 微信服务器转发过来的上报地理位置事件消息类
    /// </summary>
    public class ReceiveEventLocationMessage : MessageBase
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// 地理位置精度
        /// </summary>
        public decimal Precision { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReceiveEventLocationMessage()
            :base("event")
        {
            this.Event = "LOCATION";
            this.Latitude = 0;
            this.Longitude = 0;
            this.Precision = 0;
        }
    }
}