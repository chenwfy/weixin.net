using System;
using ProtoBuf;

namespace PetbaoWeixin.Entities.Push
{
    /// <summary>
    /// 服务器主动推送HTTP请求上行数据结构描述类
    /// </summary>
    [ProtoContract]
    public class RequestBase
    {
        /// <summary>
        /// 获取或设置请求命令
        /// </summary>
        [ProtoMember(1, IsRequired = true)]
        public string Command { get; set; }

        /// <summary>
        /// 获取或设置请求数据
        /// </summary>
        [ProtoMember(2, IsRequired = false)]
        public byte[] Data { get; set; }
    }
}