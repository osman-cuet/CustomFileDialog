using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CustomOpenFileDialog
{
    public static class Win32
    {
        public delegate bool EnumWindowsCallBack(IntPtr hWnd, int lParam);
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);
        [DllImport("User32.Dll")]
        public static extern int GetDlgCtrlID(IntPtr hWndCtl);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int MapWindowPoints(IntPtr hWnd, IntPtr hWndTo, ref Point pt, int cPoints);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowInfo(IntPtr hwnd, out WindowInfo pwi);
        [DllImport("User32.Dll")]
        public static extern void GetWindowText(IntPtr hWnd, StringBuilder param, int length);
        [DllImport("User32.Dll")]
        public static extern void GetClassName(IntPtr hWnd, StringBuilder param, int length);
        [DllImport("user32.Dll")]
        public static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsCallBack lpEnumFunc, int lParam);
        [DllImport("user32.Dll")]
        public static extern bool EnumWindows(EnumWindowsCallBack lpEnumFunc, int lParam);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetCapture(IntPtr hWnd);
        [DllImport("user32.dll", EntryPoint = "FindWindowExA", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, StringBuilder param);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, char[] chars);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr BeginDeferWindowPos(int nNumWindows);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr DeferWindowPos(IntPtr hWinPosInfo, IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int Width, int Height, SetWindowPosFlags flags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool EndDeferWindowPos(IntPtr hWinPosInfo);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int Width, int Height, SetWindowPosFlags flags);
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rect);
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hwnd, ref Rect rect);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowText(HandleRef hWnd, string lpString);
    }
}
