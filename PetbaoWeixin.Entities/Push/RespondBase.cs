using System;
using ProtoBuf;

namespace PetbaoWeixin.Entities.Push
{
    /// <summary>
    /// 服务器主动推送HTTP请求响应结果数据结构描述类
    /// </summary>
    [ProtoContract]
    public class RespondBase
    {
        /// <summary>
        /// 获取或设置响应命令
        /// </summary>
        [ProtoMember(1, IsRequired = true)]
        public string Command { get; set; }

        /// <summary>
        /// 获取或设置响应状态
        /// </summary>
        [ProtoMember(2, IsRequired = true)]
        public Status Status { get; set; }

        /// <summary>
        /// 获取或设置响应状态码
        /// </summary>
        [ProtoMember(3, IsRequired = false)]
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置响应消息
        /// </summary>
        [ProtoMember(4, IsRequired = false)]
        public string Message { get; set; }

        /// <summary>
        /// 获取或设置响应数据
        /// </summary>
        [ProtoMember(5, IsRequired = false)]
        public byte[] Data { get; set; }
    }
}