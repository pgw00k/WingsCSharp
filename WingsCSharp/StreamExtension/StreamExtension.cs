using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.IO
{
    public static class StreamExtension
    {
        /// <summary>
        /// 一个简单的拓展，可以从流中读取字符串，直到读取数小于 buffSize 为止
        /// <para>默认使用UTF8来进行编码</para>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="buffSize"></param>
        /// <returns></returns>
        public static string ReadAllToString(this Stream s,int buffSize = 65535)
        {
            byte[] buff = new byte[buffSize];
            int bytesRead;
            string raw = "";
            while ((bytesRead = s.Read(buff, 0, buffSize)) > 0)
            {
                raw += Encoding.UTF8.GetString(buff, 0, bytesRead);
                if (bytesRead < buffSize)
                {
                    break;
                }
            }
            return raw;
        }

        public static void Pipe(this Stream s,Stream d,int buffSize = 65535)
        {
            var BufferSize = buffSize;
            byte[] Buffer = new byte[BufferSize];
            int ReadCount;

            while ((ReadCount = s.Read(Buffer, 0, BufferSize)) > 0)
            {
                d.Write(Buffer, 0, ReadCount);
                d.Flush();
            }
        }

        public static void WriteUTF8String(this Stream s, string utfString)
        {
            byte[] data = Encoding.UTF8.GetBytes(utfString);
            s.Write(data, 0, data.Length);
        }
    }
}
