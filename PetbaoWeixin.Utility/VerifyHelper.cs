using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetbaoWeixin.Utility
{
    /// <summary>
    /// 相关校验辅助类
    /// </summary>
    public static class VerifyHelper
    {
        /// <summary>
        /// TOKEN码
        /// </summary>
        private const string AppToken = "BIGFOOTPETBAOWEIXINTOKEN";

        /// <summary>
        /// 消息加密秘钥
        /// </summary>
        private const string EncodingAESKey = "S3ifdWEklke9DF3rt3Gldeip34DFdfeK5deDe3De4Sk";

        /// <summary>
        /// 微信服务器请求URL签名校验（用于初始化服务器和消息真实性校验）
        /// </summary>
        /// <param name="sourceSignature">原始请求签名内容</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <returns>签名是否合法</returns>
        public static bool CheckSignature(this string sourceSignature, string timestamp, string nonce)
        {
            if (string.IsNullOrEmpty(sourceSignature) || string.IsNullOrEmpty(timestamp) || string.IsNullOrEmpty(nonce))
                return false;

            List<string> sourceList = new List<string>(3) { AppToken, timestamp, nonce };
            sourceList.Sort();
            string encryptResult = string.Join(string.Empty, sourceList).SHA1Encrypt();
            return sourceSignature.Equals(encryptResult);
        }
    }
}