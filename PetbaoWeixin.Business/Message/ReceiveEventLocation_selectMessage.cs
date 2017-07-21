using System;

namespace PetbaoWeixin.Business.Message
{
    /// <summary>
    /// 微信服务器转发过来的弹出地理位置选择器的事件推送消息类
    /// </summary>
    public class ReceiveEventLocation_selectMessage : MessageBase
    {
        /*
            本类型事件暂不处理，如果后续需要处理，需自行补充完整相关字段和方法内容
         */

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReceiveEventLocation_selectMessage()
            : base("event")
        {
        }
    }
}