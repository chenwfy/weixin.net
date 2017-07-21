using System;
using ProtoBuf;

namespace PetbaoWeixin.Entities.Push
{
    /// <summary>
    /// 服务器主动推送HTTP请求响应状态枚举
    /// </summary>
    [ProtoContract]
    public enum Status
    {
        /// <summary>
        /// 失败
        /// </summary>
        [ProtoEnum(Name = @"Failed", Value = 0)]
        Failed = 0,

        /// <summary>
        /// 成功
        /// </summary>
        [ProtoEnum(Name = @"Succeed", Value = 1)]
        Succeed = 1
    }
}