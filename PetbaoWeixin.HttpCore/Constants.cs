namespace PetbaoWeixin.HttpCore
{
    /// <summary>
    /// 内部常量定义
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// 请求应答上下文实例中实例缓存名称
        /// </summary>
        internal const string ContextKey = "CONTEXT_KEY";

        /// <summary>
        /// 请求应答上下文实例中计时器实例名称
        /// </summary>
        internal const string StopwatchKey = "STOPWATCH_KEY";

        /// <summary>
        /// 请求响应执行的驱动实例名
        /// </summary>
        internal const string CmdProviderName = "PetbaoWeixin.Command.{0}, PetbaoWeixin.Command";

        /// <summary>
        /// 当前HTTP请求上下文实例驱动实例名
        /// </summary>
        internal const string ContextProviderName = "PetbaoWeixin.HttpCore.{0}Context, PetbaoWeixin.HttpCore";

        /// <summary>
        /// 数据链路请求重定向URL
        /// </summary>
        internal const string ContextRedirectUrl = "/Service/Gate.ashx";
    }
}