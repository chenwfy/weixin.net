using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace PetbaoWeixin.Utility
{
    /// <summary>
    /// 加密解密辅助类
    /// </summary>
    public static class Encrypty
    {
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="source">待加密的原始明文字符串</param>
        /// <returns>加密后的字符串（全小写，无“-”符号）</returns>
        public static string SHA1Encrypt(this string source)
        {
            using (SHA1 sha1 = new SHA1CryptoServiceProvider())
            {
                return BitConverter.ToString(sha1.ComputeHash(Encoding.Default.GetBytes(source))).Replace("-", "").ToLower();
            }
        }
    }
}