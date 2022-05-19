using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;

namespace WINAPI
{
    public class WinHooker
    {
        protected int HookPtr = 0;
        protected HookProc _Callback;

        public virtual void StartHook(WinHook hookType,HookProc proc)
        {
            _Callback = new HookProc(proc);
            string mName = Assembly.GetExecutingAssembly().GetName().Name;
            IntPtr thisPtr = Win32.GetModuleHandle(mName);
            HookPtr = Win32.SetWindowsHookEx((int)hookType, BaseHookProc, thisPtr, 0);
        }

        protected virtual int BaseHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if(_Callback!=null)
            {
                return _Callback(nCode, wParam, lParam);
            }

            //如果返回1，则结束消息，这个消息到此为止，不再传递。
            //如果返回0或调用CallNextHookEx函数则消息出了这个钩子继续往下传递，也就是传给消息真正的接受者
            return Win32.CallNextHookEx(HookPtr, nCode, wParam, lParam);
        }
    }
}
