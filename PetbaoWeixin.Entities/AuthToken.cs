using System;
using ProtoBuf;

namespace PetbaoWeixin.Entities
{
    /// <summary>
    /// 网页认证授权ACCESSTOKEN信息结构描述类
    /// </summary>
    [ProtoContract]
    public class AuthToken
    {
        /// <summary>
        /// AccessToken内容
        /// </summary>
        [ProtoMember(1, IsRequired = true)]
        public string AccessToken { get; set; }

        /// <summary>
        /// 用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID 
        /// </summary>
        [ProtoMember(2, IsRequired = true)]
        public string OpenId { get; set; }
    }
}