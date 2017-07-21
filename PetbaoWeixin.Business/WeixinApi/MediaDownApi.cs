using System;
using PetbaoWeixin.Entities.WeixinApi;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.WeixinApi
{
    /// <summary>
    /// 下载媒体文件接口
    /// </summary>
    public class MediaDownApi : ApiBase
    {
        private const string apiUrl = "http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}";

        /// <summary>
        /// 获取AccessToken接口访问
        /// </summary>
        /// <param name="mediaId">媒体文件编号</param>
        public MediaDownApi(string mediaId)
            : base("GET")
        {
            base.Url = string.Format(apiUrl, AccessToken.Value, mediaId);
            base.PostData = "";
        }

        /// <summary>
        /// 访问接口
        /// </summary>
        /// <returns></returns>
        public byte[] Execute()
        {
            byte[] buffer = this.HttpRequestData();
            if (buffer.Length < 100)
            {
                System.Text.Encoding.UTF8.GetString(buffer).Debug();
                return null;
            }
            return buffer;
        }
    }
}