using System;
using System.Web;

namespace PetbaoWeixin.HttpCore
{
    /// <summary>
    /// 定义 ASP.NET 为使用自定义 HTTP 处理程序同步处理 HTTP Web 请求而实现的协定。
    /// </summary>
    public class Httphandler : IHttpHandler
    {
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="httpContext"></param>
        public void ProcessRequest(HttpContext httpContext)
        {
            IContext context = httpContext.Items[Constants.ContextKey] as IContext;
            if (null != context)
                context.Execute(context);
            else
                httpContext.Response.Close();
        }

        /// <summary>
        /// 获取一个值，该值指示其他请求是否可以使用 System.Web.IHttpHandler 实例。
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}