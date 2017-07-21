namespace PetbaoWeixin.Entities.Push
{
    /// <summary>
    /// 响应结果状态码定义类
    /// </summary>
    public static class RespondCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        public const string Success = "SUCCESS";

        /// <summary>
        /// 请求命令错误
        /// </summary>
        public const string CmdInvalid = "CMD_INVALID";

        /// <summary>
        /// 请求缺少必须的数据
        /// </summary>
        public const string CmdDataLack = "CD_LACK";

        /// <summary>
        /// 请求数据错误
        /// </summary>
        public const string DataInvalid = "CD_INVALID";

        /// <summary>
        /// 命令执行错误
        /// </summary>
        public const string ExecError = "EXEC_ERROR";

        /// <summary>
        /// 未知错误
        /// </summary>
        public const string CustomError = "UNKNOWN_ERROR";
    }
}