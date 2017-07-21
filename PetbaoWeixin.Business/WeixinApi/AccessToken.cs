using System;
using PetbaoWeixin.Entities.WeixinApi;
using CSharpLib.Common;
using CSharpLib.Common.Cache;

namespace PetbaoWeixin.Business.WeixinApi
{
    /// <summary>
    /// 微信服务器API请求必须的AccessToken操作辅助类
    /// </summary>
    public static class AccessToken
    {
        private const string WxAccessTokenCacheKey = "WEIXIN_API_ACCESSTOKEN";

        /// <summary>
        /// 当前可用ACCESSTOKEN值
        /// </summary>
        public static string Value
        {
            get
            {
                return GetAccessTokenFromCache();
            }
        }

        /// <summary>
        /// 从本地缓存读取AccessToken值
        /// </summary>
        /// <returns></returns>
        private static string GetAccessTokenFromCache()
        {
            object cacheValue = Constants.PetbaoWeixinCacheName.GetCache(WxAccessTokenCacheKey);
            if (null == cacheValue)
            {
                GetAndCacheAccessToken();
                cacheValue = Constants.PetbaoWeixinCacheName.GetCache(WxAccessTokenCacheKey);
            }
            if (null != cacheValue)
                return (string)cacheValue;

            return string.Empty;
        }

        /// <summary>
        /// 获取并缓存AccessToken值
        /// </summary>
        private static void GetAndCacheAccessToken()
        {
            AccessTokenApi api = new AccessTokenApi();
            AccessTokenResult result = api.Execute();
            if (null != result)
            {
                string token = result.AccessToken;
                int expired = result.Expired;

                //微信服务器默认AccessToken为2小时，即7200秒过期
                //本地缓存时，可将过期时间减少2分钟，以便提前更新

                IExpiration expiration = new RelativeTimeExpiration(new TimeSpan(0, 0, expired - 120));
                Constants.PetbaoWeixinCacheName.SetCache(WxAccessTokenCacheKey, token, expiration);
            }
        }
    }
}