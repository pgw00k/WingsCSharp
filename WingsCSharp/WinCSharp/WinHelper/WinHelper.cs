using System;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace WINAPI
{
    public class WinHelper
    {
        public static byte[] CaptureWindow(IntPtr hWnd, int width, int height)
        {
            using (var bmp = new Bitmap(width, height))
            {
                using (Graphics memoryGraphics = Graphics.FromImage(bmp))
                {
                    IntPtr dc = memoryGraphics.GetHdc();
                    Win32.PrintWindow(hWnd, dc, 0);
                    memoryGraphics.ReleaseHdc(dc);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        bmp.Save(ms, ImageFormat.Png);
                        ms.Seek(0, SeekOrigin.Begin);
                        return ms.ToArray();
                    }
                }
            }
        }
    }
}
