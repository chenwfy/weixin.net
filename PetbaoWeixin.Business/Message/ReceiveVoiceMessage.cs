using System;
using System.Xml;
using PetbaoWeixin.Utility;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.Message
{
    /// <summary>
    /// 微信服务器转发过来的语音消息类
    /// </summary>
    public class ReceiveVoiceMessage : MessageBase
    {
        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 消息编号
        /// </summary>
        public string MsgId { get; set; }

        /// <summary>
        /// 语音消息声音识别结果文字
        /// </summary>
        public string Recognition { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReceiveVoiceMessage()
            : base("voice")
        {
            this.Recognition = "";
        }
    }
}