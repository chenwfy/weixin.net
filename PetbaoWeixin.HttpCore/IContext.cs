namespace PetbaoWeixin.HttpCore
{
    /// <summary>
    /// 当前HTTP请求上下文处理接口
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// 获取当前请求是否发生意外错误
        /// </summary>
        bool HasError { get; }

        /// <summary>
        /// 解析并构建请求数据
        /// </summary>
        /// <returns></returns>
        bool Build();

        /// <summary>
        /// 开始处理请求
        /// </summary>
        void BeginExecute();

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context"></param>
        void Execute(IContext context);

        /// <summary>
        /// 请求处理结束
        /// </summary>
        /// <param name="context"></param>
        /// <param name="status"></param>
        /// <param name="watchTime"></param>
        void EndExecute(IContext context, string status, long watchTime);

        /// <summary>
        /// 关闭连接
        /// </summary>
        void Close();
    }
}