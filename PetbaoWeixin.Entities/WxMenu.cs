using System;
using System.Collections.Generic;

namespace PetbaoWeixin.Entities
{
    /// <summary>
    /// 微信公众账号菜单
    /// </summary>
    public class WxMenu
    {
        /// <summary>
        /// 菜单编号，用于数据库管理
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 菜单标题，不超过16个字节，子菜单不超过40个字节 
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单的响应动作类型 
        /// </summary>
        public string MenuType { get; set; }

        /// <summary>
        /// 菜单KEY值，用于消息接口推送，不超过128字节 
        /// </summary>
        public string MenuKey { get; set; }

        /// <summary>
        /// 网页链接，用户点击菜单可打开链接，不超过256字节
        /// </summary>
        public string MenuUrl { get; set; }
    }
}