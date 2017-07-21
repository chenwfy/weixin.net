using System;
using ProtoBuf;

namespace PetbaoWeixin.Entities
{
    /// <summary>
    /// JS接口签名信息类
    /// </summary>
    [ProtoContract]
    public class JsApiSig
    {
        /// <summary>
        /// 应用编号
        /// </summary>
        [ProtoMember(1, IsRequired = true)]
        public string AppId { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        [ProtoMember(2, IsRequired = true)]
        public long Timestamp{  get; set;}

        /// <summary>
        /// 随机码
        /// </summary>
        [ProtoMember(3, IsRequired = true)]
        public string NonceStr { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [ProtoMember(4, IsRequired = true)]
        public string Signature { get; set; }
    }
}