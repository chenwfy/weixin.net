using System;
using PetbaoWeixin.Utility;
using CSharpLib.Common;

namespace PetbaoWeixin.Business.WeixinApi
{
    /// <summary>
    /// 访问微信服务器接口
    /// </summary>
    public static class ApiRequest
    {
        /// <summary>
        /// 以HTTP请求访问微信接口，响应结果为字节流
        /// </summary>
        /// <param name="api"></param>
        /// <returns>响应字节流</returns>
        public static byte[] HttpRequestData(this ApiBase api)
        {
            try
            {
                HttpClient client = new HttpClient();
                
                byte[] buffer;
                if (api.Method.ToUpper().Equals("GET"))
                    buffer = client.DownloadData(api.Url);
                else
                    buffer = client.UploadData(api.Url, api.Method, System.Text.Encoding.UTF8.GetBytes(api.PostData));

                client.Dispose();
                client = null;

                return buffer;
            }
            catch (Exception ex)
            {
                ex.Error();
                return null;
            }
        }

        /// <summary>
        /// 以HTTP请求访问微信接口，响应结果为字符串
        /// </summary>
        /// <param name="api"></param>
        /// <returns>响应字符串</returns>
        public static string HttpRequestString(this ApiBase api)
        {
            byte[] buffer = api.HttpRequestData();
            if (null != buffer && buffer.Length > 0)
                return System.Text.Encoding.UTF8.GetString(buffer);
            return string.Empty;
        }
    }
}