using System;
using System.Runtime.InteropServices;

namespace WINAPI
{
	#region RECT
	[StructLayout(LayoutKind.Sequential)]
	public struct Rect
	{
		public int left;
		public int top;
		public int right;
		public int bottom;

        public override string ToString()
        {
			string r = $"({top},{bottom},{left},{right})";
			return r;
        }
    }
	#endregion

	public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

	[StructLayout(LayoutKind.Sequential)]
	public struct KeyboardHookStruct
	{
		public int vkCode;  //定一个虚拟键码。该代码必须有一个价值的范围1至254
		public int scanCode; // 指定的硬件扫描码的关键
		public int flags;  // 键标志
		public int time; // 指定的时间戳记的这个讯息
		public int dwExtraInfo; // 指定额外信息相关的信息
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct WinMessage
	{
		public IntPtr hwnd;
		public IntPtr lParam;
		public int message;
		public int pt_x;
		public int pt_y;
		public int time;
		public IntPtr wParam;
	}
}
