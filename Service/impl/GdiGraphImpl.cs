using System;
using System.Drawing;
using System.Threading;
using PluginHelper.Native;

namespace PluginHelper.Service
{
    public class GdiGraphImpl : IGraph
    {
        private IntPtr solidBrush;
        public IntPtr hdc;
        IntPtr hNPen;
        IntPtr windowHandle;
        Thread thread;

        int ParseRGB(Color color)
        {
            return (color.B << 16) | (ushort)((color.G << 8) | color.R);
        }
        public void init(IntPtr processId)
        {
            // 0x000000FF
            var red = ParseRGB(Color.FromArgb(255, 0, 0));
            hNPen = NativeMethods.CreatePen(NativeMethods.PenStyle.PS_SOLID, 2, (uint) red);
            windowHandle = NativeMethods.GetWindowHandle(processId);
            hdc = NativeMethods.GetDC(windowHandle);
            solidBrush = NativeMethods.CreateSolidBrush(red);
        }
        
        public void start()
        {
            thread = new Thread(drawGDIStart);
            thread.Start(new object());

        }

        void drawGDIStart(object obj)
        {
            // 参数
            while (true)
            {
                Thread.Sleep(1000);

                var rect = new NativeMethods.RECT();
                NativeMethods.GetWindowRect(windowHandle, ref rect);

                // 整个窗口画大圈
                DrawBorderBox(0, 0, rect.Width - 50, rect.Height - 110, 5);
                // 画小圈
                DrawBorderBox(50, 50, 200, 100, 5);
                // 画一条线
                DrawLine(50, 50, 250, 150);
            }
        }

        void DrawLine(int StartX, int StartY, int EndX, int EndY)
        {
            var oldPen = NativeMethods.SelectObject(hdc, hNPen);
            NativeMethods.MoveToEx(hdc, StartX, StartY, IntPtr.Zero); //start
            NativeMethods.LineTo(hdc, EndX, EndY); //end
            NativeMethods.DeleteObject(NativeMethods.SelectObject(hdc, oldPen));
        }

        void DrawBorderBox(int x, int y, int w, int h, int thickness)
        {
            DrawFilledRect(x, y, w, thickness); //Top horiz line
            DrawFilledRect(x, y, thickness, h); //Left vertical line
            DrawFilledRect((x + w), y, thickness, h); //right vertical line
            DrawFilledRect(x, y + h, w + thickness, thickness); //bottom horiz line
        }

        void DrawFilledRect(int x, int y, int w, int h)
        {
            NativeMethods.RECT rect = new NativeMethods.RECT(x, y, x + w, y + h);
            NativeMethods.FillRect(hdc, ref rect, solidBrush);
        }

        public void destory()
        {
            
            if (thread.IsAlive)
            {
                thread.Abort();
            }
            NativeMethods.DeleteObject(hNPen);
            NativeMethods.CloseHandle(windowHandle);
            NativeMethods.DeleteDC(hdc);
            NativeMethods.DeleteObject(solidBrush);
        }
    }
}