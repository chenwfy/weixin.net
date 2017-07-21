using System;
using System.Web;
using System.IO;
using System.Text;
using PetbaoWeixin.Utility;
using CSharpLib.Common;

namespace PetbaoWeixin.HttpCore
{
    /// <summary>
    /// 微信服务器HTTP请求上下文处理类
    /// </summary>
    public class DataContext : IContext
    {
        #region 成员

        /// <summary>
        /// 获取或设置当前HTTP请求上下文应答对象
        /// </summary>
        public HttpContext Context { get; private set; }

        /// <summary>
        /// 获取或设置当前HTTP请求BODY内容
        /// </summary>
        public string QueryBody { get; private set; }

        /// <summary>
        /// 获取或设置请求命令
        /// </summary>
        public string Command { get; private set; }

        /// <summary>
        /// 当前HTTP请求的URL附带请求参数
        /// </summary>
        private string urlQuery = string.Empty;

        #endregion

        #region 接口实现

        /// <summary>
        /// 是否发生意外错误
        /// </summary>
        public bool HasError { get; private set; }

        /// <summary>
        /// 请求数据初始化
        /// </summary>
        /// <returns></returns>
        public bool Build()
        {
            try
            {
                //远程请求IP，可用于进行微信服务器IP校验，暂不处理
                //string remoteIp = Context.Request.UserHostAddress;

                //URL附带参数
                this.urlQuery = Context.Request.Url.Query;

                //微信服务器请求过来时，URL必带的三个参数
                string signature = "signature".Request<string>(Context) ?? string.Empty;
                string timestamp = "timestamp".Request<string>(Context) ?? string.Empty;
                string nonce = "nonce".Request<string>(Context) ?? string.Empty;
                
                //请求签名校验
                if (!signature.CheckSignature(timestamp, nonce))
                    return false;

                this.QueryBody = string.Empty;
                string method = Context.Request.HttpMethod.ToUpper();
                
                //POST
                if (method.Equals("POST"))
                {
                    using (Stream stream = Context.Request.InputStream)
                    {
                        if (stream != null && stream.Length > 0)
                        {
                            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                this.QueryBody = reader.ReadToEnd();
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(this.QueryBody))
                    {
                        this.Command = "WeixinReq.Forward";
                        return true;
                    }
                }

                //GET
                if (method.Equals("GET"))
                {
                    this.Command = "WeixinReq.Verify";
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                ex.Error();
                return false;
            }
        }

        /// <summary>
        /// 开始处理请求
        /// </summary>
        public void BeginExecute()
        {
            "[WeiXinServerRequest - BeginProcess] - Command: {0}".Info(this.Command);
            Context.RewritePath(Constants.ContextRedirectUrl + this.urlQuery);
        }

        /// <summary>
        /// 执行请求
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IContext context)
        {
            IExecute<DataContext> instance = string.Format(Constants.CmdProviderName, Command).CreateInstance<IExecute<DataContext>>();
            if (null != instance)
                instance.Execute(context as DataContext);
            else
                Close();
        }

        /// <summary>
        /// 请求处理结束
        /// </summary>
        /// <param name="context"></param>
        /// <param name="status"></param>
        /// <param name="watchTime"></param>
        public void EndExecute(IContext context, string status, long watchTime)
        {
            "[WeiXinServerRequest - EndProcess] Command : {0} [{1}] - {2}ms".Info(this.Command, status, watchTime);
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            Context.Response.Close();
        }

        #endregion

        #region 公开方法

        /// <summary>
        /// 构造应用请求上下文应答类
        /// </summary>
        public DataContext(HttpContext context)
        {
            Context = context;
            HasError = true;
        }

        /// <summary>
        /// 立即刷新服务器输出流数据
        /// </summary>
        /// <param name="responseData">输出的数据</param>
        public void Flush(string responseData)
        {
            HasError = false;
            Context.Response.Clear();
            Context.Response.Charset = "UTF-8";
            Context.Response.Write(responseData);
            Context.Response.End();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
        }

        #endregion
    }
}