using System;

namespace PetbaoWeixin.Business.Message
{
    /// <summary>
    /// 微信服务器转发过来的扫码推事件且弹出“消息接收中”提示框的事件推送消息类
    /// </summary>
    public class ReceiveEventScancode_waitmsgMessage : MessageBase
    {
        /*
            本类型事件暂不处理，如果后续需要处理，需自行补充完整相关字段和方法内容
         */

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReceiveEventScancode_waitmsgMessage()
            : base("event")
        {
        }
    }
}