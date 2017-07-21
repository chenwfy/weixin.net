using System;
using ProtoBuf;

namespace PetbaoWeixin.Entities
{
    /// <summary>
    /// 用户基本信息
    /// </summary>
    [ProtoContract]
    public class UserBaseInfo
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [ProtoMember(1, IsRequired = true)]
        public int UserId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [ProtoMember(2, IsRequired = true)]
        public string UserName { get; set; }

        /// <summary>
        /// 用户头像URL
        /// </summary>
        [ProtoMember(3, IsRequired = true)]
        public string HeadIcon { get; set; }

        /// <summary>
        /// OpenId
        /// </summary>
        [ProtoMember(4, IsRequired = false)]
        public string OpenId { get; set; }
    }
}