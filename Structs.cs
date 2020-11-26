using System;
using System.Runtime.InteropServices;

namespace CustomOpenFileDialog
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowInfo
    {
        public UInt32 cbSize;
        public Rect rcWindow;
        public Rect rcClient;
        public UInt32 dwStyle;
        public UInt32 dwExStyle;
        public UInt32 dwWindowStatus;
        public UInt32 cxWindowBorders;
        public UInt32 cyWindowBorders;
        public UInt16 atomWindowType;
        public UInt16 wCreatorVersion;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(System.Drawing.Point point)
        {
            x = point.X;
            y = point.Y;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public uint left;
        public uint top;
        public uint right;
        public uint bottom;

        public Point Location
        {
            get { return new Point((int)left, (int)top); }
            set
            {
                right -= (left - (uint)value.x);
                bottom -= (bottom - (uint)value.y);
                left = (uint)value.x;
                top = (uint)value.y;
            }
        }

        public uint Width
        {
            get { return right - left; }
            set { right = left + value; }
        }

        public uint Height
        {
            get { return bottom - top; }
            set { bottom = top + value; }
        }

        public override string ToString()
        {
            return left + ":" + top + ":" + right + ":" + bottom;
        }
    }
}
