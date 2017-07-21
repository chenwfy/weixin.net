using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using PetbaoWeixin.Entities;
using CSharpLib.DataAccess;
using CSharpLib.Common;

namespace PetbaoWeixin.DataAccess
{
    /// <summary>
    /// 自定义菜单部分数据库操作类
    /// </summary>
    public static class MenuData
    {
        /// <summary>
        /// 获取指定父级编号的子级菜单数据列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static IEnumerable<WxMenu> GetMenus(int parentId = 0)
        {
            string querySql = @"SELECT  MenuId ,
                                        MenuName ,
                                        MenuType ,
                                        MenuKey ,
                                        MenuUrl
                                FROM    Weixin_Menus
                                WHERE   ParentId = @ParentId
                                ORDER BY Sort DESC ,
                                        CreateDate DESC ,
                                        MenuId DESC ";
            using (DbCommander cmd = new DbCommander(querySql))
            {
                cmd.AddInputParameters("ParentId", parentId);
                List<WxMenu> resultData = new List<WxMenu>(0);
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resultData.Add(new WxMenu
                        {
                            MenuId = (int)reader["MenuId"],
                            MenuName = (string)reader["MenuName"],
                            MenuType = (string)reader["MenuType"],
                            MenuKey = (string)reader["MenuKey"],
                            MenuUrl = (string)reader["MenuUrl"]
                        });
                    }
                    reader.Close();
                }
                return resultData;
            }
        }
    }
}