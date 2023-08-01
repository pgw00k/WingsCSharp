using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace System.Net
{
    public class HttpDownloader
    {
        /// <summary>
        /// 默认流大小
        /// </summary>
        public static readonly int DefaultCacheSize = 4096;

        /// <summary>
        /// http下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="path">文件存放地址，包含文件名</param>
        /// <returns></returns>
        public static bool DownloadFile(string url, string path)
        {
            string tempFile = Path.GetFullPath(path) + ".temp"; //临时文件
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);    //存在则删除
            }
            try
            {
                FileStream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                //Stream stream = new FileStream(tempFile, FileMode.Create);
                byte[] bArr = new byte[DefaultCacheSize];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    //stream.Write(bArr, 0, size);
                    fs.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                //stream.Close();
                fs.Close();
                responseStream.Close();
                File.Move(tempFile, path);
                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(string.Format("download {0} fail.{1}", url, ex.Message));
#endif
                return false;
            }
        }
    }
}
