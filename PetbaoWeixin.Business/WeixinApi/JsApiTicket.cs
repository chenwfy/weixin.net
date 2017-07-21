using System;
using PetbaoWeixin.Entities.WeixinApi;
using CSharpLib.Common;
using CSharpLib.Common.Cache;

namespace PetbaoWeixin.Business.WeixinApi
{
    /// <summary>
    /// JS接口票据获取和缓存类
    /// </summary>
    public static class JsApiTicket
    {
        private const string WxJsApiTicketCacheKey = "WEIXIN_API_JSAPITICKET";

        /// <summary>
        /// 当前可用ACCESSTOKEN值
        /// </summary>
        public static string Value
        {
            get
            {
                return GetJsApiTicketFromCache();
            }
        }

        /// <summary>
        /// 从本地缓存读取JsApiTicket值
        /// </summary>
        /// <returns></returns>
        private static string GetJsApiTicketFromCache()
        {
            object cacheValue = Constants.PetbaoWeixinCacheName.GetCache(WxJsApiTicketCacheKey);
            if (null == cacheValue)
            {
                GetAndCacheJsApiTicket();
                cacheValue = Constants.PetbaoWeixinCacheName.GetCache(WxJsApiTicketCacheKey);
            }
            if (null != cacheValue)
                return (string)cacheValue;

            return string.Empty;
        }

        /// <summary>
        /// 获取并缓存JsApiTicket值
        /// </summary>
        private static void GetAndCacheJsApiTicket()
        {
            JsTicketApi api = new JsTicketApi();
            JsTicketResult result = api.Execute();
            if (null != result && result.Code == 0)
            {
                string ticket = result.Ticket;
                int expired = result.Expired;

                //微信服务器默认AccessToken为2小时，即7200秒过期
                //本地缓存时，可将过期时间减少2分钟，以便提前更新

                IExpiration expiration = new RelativeTimeExpiration(new TimeSpan(0, 0, expired - 120));
                Constants.PetbaoWeixinCacheName.SetCache(WxJsApiTicketCacheKey, ticket, expiration);
            }
        }
    }
}
