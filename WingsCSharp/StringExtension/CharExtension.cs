using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class CharExtension
    {
        /// <summary>
        /// 逐个字符判断 str1 是否以 str2 结尾
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static bool CharsEndWith(this string str1, string str2)
        {
            if (str2 == null) return false;
            if (str1.Length < str2.Length) return false;
            for (int i = 1; i <= str2.Length; i++)
            {
                if (str1[str1.Length - i] != str2[str2.Length - i]) return false;
            }
            return true;
        }

        /// <summary>
        /// 逐个字符判断 str1 是否以 str2 开始
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static bool CharsStartWith(this string str1, string str2)
        {
            if (str2 == null) return false;
            if (str1.Length < str2.Length) return false;
            for (int i = 0; i < str2.Length; i++)
            {
                if (str1[i] != str2[i]) return false;
            }
            return true;
        }
    }
}
