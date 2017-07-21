using System;

namespace PetbaoWeixin.Utility
{
    /// <summary>
    /// 通用辅助方法类
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// 字符串首字母转大写（注意：该方法首输入参数首字符必须为字母，未做异常处理）
        /// </summary>
        /// <param name="sourceText">待处理的原始字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string FirstCharToUpper(this string sourceText)
        {
            if (string.IsNullOrEmpty(sourceText))
                return string.Empty;

            sourceText = sourceText.ToLower();
            if (sourceText.Length > 1)
                return sourceText.Substring(0, 1).ToUpper() + sourceText.Substring(1);
            
            return sourceText.ToUpper();
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        public static long GetNowSeconds()
        {
            return DateTime.UtcNow.ToTimeSeconds();
        }

        /// <summary>
        /// 获取指定时间距离1970-01-01 0:0:0的相差秒数
        /// </summary>
        /// <param name="time">待计算的时间</param>
        /// <returns>相差秒数</returns>
        public static long ToTimeSeconds(this DateTime time)
        {
            return (long)time.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }
    }
}