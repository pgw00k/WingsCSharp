using System;
using System.Drawing;

namespace WINAPI
{
    /// <summary>
    /// 用以持续截图的类型
    /// </summary>
    public class CaptureHolder : IDisposable
    {
        protected Bitmap _Bitmap;
        protected Graphics _MemGs;
        protected IntPtr _Gdc;
        protected IntPtr _WinHandle;

        /// <summary>
        /// 窗口的截图
        /// </summary>
        public Bitmap MainBitMap
        {
            get
            {
                return _Bitmap;
            }

            set
            {
                _Bitmap = value;
            }
        }

        public CaptureHolder(IntPtr hWnd, int width,int height)
        {
            _WinHandle = hWnd;
            _Bitmap = new Bitmap(width, height);
            _MemGs = Graphics.FromImage(_Bitmap);          
        }

        public virtual void Cap()
        {
            _Gdc = _MemGs.GetHdc();
            Win32.PrintWindow(_WinHandle, _Gdc, 0);
            _MemGs.ReleaseHdc(_Gdc);
        }

        ~CaptureHolder()
        {
            Dispose();
        }

        public void Dispose()
        {
            _MemGs.Dispose();
            _Bitmap.Dispose();
        }
    }
}
