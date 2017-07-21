using System;
using System.Web;
using System.Diagnostics;
using CSharpLib.Common;

namespace PetbaoWeixin.HttpCore
{
    /// <summary>
    /// 自定义HTTP请求处理模块接口实现类
    /// </summary>
    public class HttpMoudle : IHttpModule
    {
        /// <summary>
        /// 处置由实现 System.Web.IHttpModule 的模块使用的资源（内存除外）。
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// 初始化模块，并使其为处理请求做好准备。
        /// </summary>
        /// <param name="context">一个 System.Web.HttpApplication，它提供对 ASP.NET 应用程序内所有应用程序对象的公用的方法、属性和事件的访问</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(HttpContextBeginRequest);
            context.EndRequest += new EventHandler(HttpContextEndRequest);
            context.Error += new EventHandler(HttpContextError);
            context.PreSendRequestHeaders += new EventHandler(HttpContextPreSendRequestHeaders);
        }

        /// <summary>
        /// 请求开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HttpContextBeginRequest(object sender, EventArgs e)
        {
            HttpContext httpContext = (sender as HttpApplication).Context;
            Stopwatch watch = new Stopwatch();
            httpContext.Items.Add(Constants.StopwatchKey, watch);
            watch.Start();

            IContext context = CreateContextInstance(httpContext.Request.Path ?? string.Empty, httpContext);
            if (null != context)
            {
                httpContext.Items.Add(Constants.ContextKey, context);
                if (context.Build())
                    context.BeginExecute();
                else
                    context.Close();
            }
            else
            {
                httpContext.Response.Close();
            }
        }

        /// <summary>
        /// 请求结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HttpContextEndRequest(object sender, EventArgs e)
        {
            HttpContext httpContext = (sender as HttpApplication).Context;
            Stopwatch watch = httpContext.Items[Constants.StopwatchKey] as Stopwatch;
            watch.Stop();

            IContext context = httpContext.Items[Constants.ContextKey] as IContext;
            if (null != context)
            {
                string status = context.HasError ? "ERROR" : "OK";
                context.EndExecute(context, status, watch.ElapsedMilliseconds);
            }
        }

        /// <summary>
        /// 请求错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HttpContextError(object sender, EventArgs e)
        {
            HttpContext httpContext = (sender as HttpApplication).Context;
            httpContext.Server.GetLastError().Error();
        }

        /// <summary>
        /// 响应标头处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HttpContextPreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext httpContext = (sender as HttpApplication).Context;
            httpContext.Response.Headers.Remove("Server");
            httpContext.Response.Headers.Remove("X-AspNet-Version");
            httpContext.Response.Headers.Remove("X-AspNetMvc-Version");
            httpContext.Response.Headers.Remove("X-Powered-By");
            httpContext.Response.Headers.Remove("Cache-Control");
            httpContext.Response.Headers.Remove("Date");
        }

        /// <summary>
        /// 当前请求处理实例路由配置
        /// </summary>
        /// <param name="vPath"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private static IContext CreateContextInstance(string vPath, HttpContext context)
        {
            string path = vPath + "";
            if (path.Equals("/Push"))
                return new PushContext(context);
            if (path.Equals("/Data"))
                return new DataContext(context);            
            return null;
        }
    }
}