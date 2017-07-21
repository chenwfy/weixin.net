using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using PetbaoWeixin.Entities;
using PetbaoWeixin.Entities.WeixinApi;
using PetbaoWeixin.Business.WeixinApi;
using PetbaoWeixin.Utility;
using PetbaoWeixin.DataAccess;
using CSharpLib.Common;

namespace PetbaoWeixin.Business
{
    /// <summary>
    /// 业务相关支撑内容逻辑处理类
    /// </summary>
    public static class ApiSupportBiz
    {
        #region 自定义菜单

        /// <summary>
        /// 创建自定义菜单
        /// </summary>
        /// <returns></returns>
        public static bool CreateMenu()
        {
            string menuJson = GetMenuJson();
            if (string.IsNullOrEmpty(menuJson))
            {
                new Exception("No Menus Data Or Menus Data Read Failed!").Error();
                return false;
            }

            CreateMenuApi api = new CreateMenuApi(menuJson);
            return api.Execute();
        }

        /// <summary>
        /// 获取菜单JSON串
        /// </summary>
        /// <returns></returns>
        private static string GetMenuJson()
        {
            List<WxMenu> topMenuList = MenuData.GetMenus().ToList();
            if (topMenuList.Count == 0)
                return string.Empty;

            string menuJson = "{\"button\": [";

            foreach (var topMenu in topMenuList)
            {
                List<WxMenu> subMenuList = MenuData.GetMenus(topMenu.MenuId).ToList();
                int subCount = subMenuList.Count;

                if (subCount > 0)
                {
                    menuJson += "{ ";
                    menuJson += "\"name\": \"" + topMenu.MenuName + "\",";
                    menuJson += "\"sub_button\": [";

                    foreach (var subMenu in subMenuList)
                    {
                        menuJson += "{ ";
                        menuJson += "\"type\": \"" + subMenu.MenuType + "\",";
                        menuJson += "\"name\": \"" + subMenu.MenuName + "\",";
                        menuJson += "\"key\": \"" + subMenu.MenuKey + "\",";
                        menuJson += "\"url\": \"" + subMenu.MenuUrl + "\"";
                        menuJson += " },";
                    }

                    if (menuJson.EndsWith(","))
                        menuJson = menuJson.TrimEnd(',');

                    menuJson += "]},";
                }
                else
                {
                    menuJson += "{ ";
                    menuJson += "\"type\": \"" + topMenu.MenuType + "\",";
                    menuJson += "\"name\": \"" + topMenu.MenuName + "\",";
                    menuJson += "\"key\": \"" + topMenu.MenuKey + "\",";
                    menuJson += "\"url\": \"" + topMenu.MenuUrl + "\"";
                    menuJson += " },";
                }
            }

            if (menuJson.EndsWith(","))
                menuJson = menuJson.TrimEnd(',');

            menuJson += "]}";

            return menuJson;
        }

        #endregion

        #region 网页JS SDK相关

        /// <summary>
        /// 创建JS接口签名
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static JsApiSig CreateJsApiSig(string url)
        {
            JsApiSig sig = new JsApiSig
            {
                AppId = Constants.PetbaoWeixinAppId,
                Timestamp = Utilities.GetNowSeconds(),
                NonceStr = EncryptHelper.CreateSalt(8).Replace("&", "").Replace("=", ""),
                Signature = ""
            };

            if (!string.IsNullOrEmpty(JsApiTicket.Value))
            {
                List<string> sourceList = new List<string>(4);
                sourceList.Add(string.Format("noncestr={0}", sig.NonceStr));
                sourceList.Add(string.Format("jsapi_ticket={0}", JsApiTicket.Value));
                sourceList.Add(string.Format("timestamp={0}", sig.Timestamp));
                sourceList.Add(string.Format("url={0}", url));
                sourceList.Sort();

                sig.Signature = string.Join("&", sourceList).SHA1Encrypt();
                return sig;
            }

            return null;
        }

        #endregion

        #region 多媒体文件相关

        /// <summary>
        /// 下载指定的媒体文件
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public static byte[] DownloadMediaFile(string mediaId)
        {
            MediaDownApi api = new MediaDownApi(mediaId);
            return api.Execute();
        }

        #endregion

        #region 网页授权认证相关

        private const string BaseWxAuthUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect";

        /// <summary>
        /// 获取页面授权访问URL
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        public static string GetAuthUrl(string redirectUrl)
        {
            string randomCode = Guid.NewGuid().ToString().Replace("-", string.Empty);

            if (redirectUrl.Contains("?"))
                redirectUrl += "&r=" + randomCode;
            else
                redirectUrl += "?r=" + randomCode;

            return string.Format(BaseWxAuthUrl, Constants.PetbaoWeixinAppId, System.Web.HttpUtility.UrlEncode(redirectUrl), randomCode);
        }

        /// <summary>
        /// 获取网页认证授权ACCESSTOKEN
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static AuthToken GetAuthAccessToken(string code)
        {
            AuthAccessTokenApi api = new AuthAccessTokenApi(code);
            AuthAccessTokenResult result = api.Execute();
            if (null != result)
                return new AuthToken { AccessToken = result.AccessToken, OpenId = result.OpenId };
            return null;
        }

        /// <summary>
        /// 获取网页授权的用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static UserBaseInfo GetUserInfoByTokenAndOpenId(string token, string openId)
        {
            WebUserInfoApi api = new WebUserInfoApi(token, openId);
            WebUserInfoResult webUser = api.Execute();
            MIAP.ProtoBuf.User.UserBase cacheUser = GetUserBaseFromCache(openId);

            if (null != webUser)
            {
                if (null == cacheUser)
                {
                    PetbaoWeixinWeb.RemoteService.DataService.WxUserInfo wxUser = new PetbaoWeixinWeb.RemoteService.DataService.WxUserInfo
                    {
                        OpenId = webUser.OpenId,
                        Nickname = webUser.Nickname,
                        HeadImgageUrl = webUser.HeadImgageUrl,
                        Province = webUser.Province,
                        City = webUser.City,
                        Country = webUser.Country
                    };

                    int userId = PetbaoWeixinWeb.RemoteService.DataService.DataQuery.WxUserLogin(wxUser);
                    if (userId > 0)
                        return new UserBaseInfo { UserId = userId, UserName = webUser.Nickname, HeadIcon = webUser.HeadImgageUrl };
                }
                else
                {
                    string nickName = webUser.Nickname, headIcon = webUser.HeadImgageUrl;
                    if ((!string.IsNullOrEmpty(nickName) && !nickName.Equals(cacheUser.NiceName)) || (!string.IsNullOrEmpty(headIcon) && !headIcon.Equals(cacheUser.HeadIcon)))
                    {
                        PetbaoWeixinWeb.RemoteService.DataService.WxUserInfo wxUser = new PetbaoWeixinWeb.RemoteService.DataService.WxUserInfo
                        {
                            OpenId = webUser.OpenId,
                            Nickname = nickName,
                            HeadImgageUrl = headIcon,
                            Province = webUser.Province,
                            City = webUser.City,
                            Country = webUser.Country
                        };

                        PetbaoWeixinWeb.RemoteService.DataService.DataQuery.WxUserLogin(wxUser);
                        return new UserBaseInfo { UserId = (int)cacheUser.UserId, UserName = nickName, HeadIcon = headIcon };
                    }
                    return new UserBaseInfo { UserId = (int)cacheUser.UserId, UserName = cacheUser.NiceName, HeadIcon = cacheUser.HeadIcon };
                }
            }
            else
            {
                if (null != cacheUser)
                    return new UserBaseInfo { UserId = (int)cacheUser.UserId, UserName = cacheUser.NiceName, HeadIcon = cacheUser.HeadIcon };
            }
            return null;
        }

        /// <summary>
        /// 将当前关注公众号的用户注册到业务系统中
        /// </summary>
        /// <param name="openId"></param>
        public static void RegisterNewUserForBusiness(string openId)
        {
            SvcUserInfoApi api = new SvcUserInfoApi(openId);
            SvcUserInfoResult svcUser = api.Execute();
            MIAP.ProtoBuf.User.UserBase cacheUser = GetUserBaseFromCache(openId);

            if (null != svcUser)
            {
                string nickName = svcUser.Nickname, headIcon = svcUser.HeadImgageUrl;
                if (null == cacheUser || (null != cacheUser && ((!string.IsNullOrEmpty(nickName) && !nickName.Equals(cacheUser.NiceName)) || (!string.IsNullOrEmpty(headIcon) && !headIcon.Equals(cacheUser.HeadIcon)))))
                {
                    PetbaoWeixinWeb.RemoteService.DataService.WxUserInfo wxUser = new PetbaoWeixinWeb.RemoteService.DataService.WxUserInfo
                    {
                        OpenId = svcUser.OpenId,
                        Nickname = nickName,
                        HeadImgageUrl = headIcon,
                        Province = svcUser.Province,
                        City = svcUser.City,
                        Country = svcUser.Country
                    };
                    PetbaoWeixinWeb.RemoteService.DataService.DataQuery.WxUserLogin(wxUser);
                }
            }
        }

        /// <summary>
        /// 创建微信用户关注记录
        /// </summary>
        /// <param name="openId"></param>
        public static void CreateWxUserSubscribeLog(string openId)
        {
            PetbaoWeixinWeb.RemoteService.DataService.DataQuery.CreateWxUserSubscribeLog(openId);
        }

        /// <summary>
        /// 移除微信用户关注记录
        /// </summary>
        /// <param name="openId"></param>
        public static void RemoveWxUserSubscribeLog(string openId)
        {
            PetbaoWeixinWeb.RemoteService.DataService.DataQuery.RemoveWxUserSubscribeLog(openId);
        }

        #endregion

        #region 用户信息相关

        /// <summary>
        /// 获取微信用户信息缓存文件路径
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        private static string GetCacheFilePathForWx(string openId)
        {
            return Path.Combine("MIAP.CacheFileRoot".GetAppSetting().MapPath(), "WxUser", string.Format("{0}.bin", openId));
        }

        /// <summary>
        /// 从缓存读取用户基本信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        private static MIAP.ProtoBuf.User.UserBase GetUserBaseFromCache(string openId)
        {
            string filePath = GetCacheFilePathForWx(openId);
            if (File.Exists(filePath))
            {
                byte[] buffer = filePath.ReadFile();
                if (null != buffer && buffer.Length > 0)
                    return buffer.ProtoBufDeserialize<MIAP.ProtoBuf.User.UserBase>();
            }
            return null;
        }

        #endregion

        #region 发送客服消息相关

        /// <summary>
        /// 发送客服消息
        /// </summary>
        /// <param name="message"></param>
        public static void SendCustomMessage(string message)
        {
            SendCustomMessageApi api = new SendCustomMessageApi(message);
            api.Execute();
        }

        #endregion
    }
}