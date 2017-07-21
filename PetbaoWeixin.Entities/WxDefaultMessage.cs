using ProtoBuf;

namespace PetbaoWeixin.Entities
{
    /// <summary>
    /// 微信默认消息配置
    /// </summary>
    [ProtoContract]
    public class WxDefaultMessage
    {
        /// <summary>
        /// 关注微信账号时，默认发送的欢迎消息内容
        /// </summary>
        [ProtoMember(1, IsRequired = true)]
        public string WelComeMessage { get; set; }

        /// <summary>
        /// 用户给微信账号发送消息时，默认回复消息内容（为空则表示不回复）
        /// </summary>
        [ProtoMember(2, IsRequired = true)]
        public string DefaultReplyMessage { get; set; }
    }
}