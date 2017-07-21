using System;
using System.Net;

namespace PetbaoWeixin.Utility
{
    /// <summary>
    /// HTTP访问客户端
    /// </summary>
    public class HttpClient : WebClient
    {
        /// <summary>
        /// 获取或设置Cookie容器
        /// </summary>
        public CookieContainer Cookies { get; set; }
        
        /// <summary>
        /// 获取或设置访问超时时间（单位：毫秒）
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// 构造访问实例
        /// </summary>
        public HttpClient()
            :this(new CookieContainer())
        {
        }

        /// <summary>
        /// 构造访问实例
        /// </summary>
        /// <param name="cookieContainer">Cookie容器</param>
        public HttpClient(CookieContainer cookieContainer)
        {
            this.Cookies = cookieContainer;
            this.Timeout = 5000;
        }

        /// <summary>
        /// 返回带有 Cookie 的 HttpWebRequest。
        /// </summary>
        /// <param name="address">一个 <see cref="T:System.Uri"/>，用于标识要请求的资源。</param>
        /// <returns>
        /// 一个新的 <see cref="T:System.Net.WebRequest"/> 对象，用于指定的资源。
        /// </returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                HttpWebRequest httpRequest = request as HttpWebRequest;
                httpRequest.Proxy = null;
                httpRequest.CookieContainer = this.Cookies;
                httpRequest.Timeout = this.Timeout;
            }
            return request;
        }

        /// <summary>
        /// 返回对指定 <see cref="T:System.Net.WebRequest"/> 的 <see cref="T:System.Net.WebResponse"/>。
        /// </summary>
        /// <param name="request">用于获取响应的 <see cref="T:System.Net.WebRequest"/>。</param>
        /// <returns>
        /// 	<see cref="T:System.Net.WebResponse"/> 包含对指定 <see cref="T:System.Net.WebRequest"/> 的响应。
        /// </returns>
        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            if (response is HttpWebResponse)
            {
                HttpWebResponse httpResponse = response as HttpWebResponse;
                if (httpResponse.Cookies.Count > 0)
                {
                    Cookies.Add(httpResponse.Cookies);
                }
            }
            return response;
        }
    }
}