using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using PluginHelper.Native;

namespace PluginHelper.Service
{
    public class GdiGraphWithBufferImpl : IGraph
    {
        IntPtr GameHWND;
        private NativeMethods.RECT WBounds;
        public IntPtr EspHWND;

        private IntPtr movThread;
        private IntPtr workLoopThread;
        string className = "MainWindow";

        public void init(IntPtr processId)
        {
            unsafe
            {
                GameHWND = NativeMethods.GetWindowHandle(processId);
                NativeMethods.GetClientRect(GameHWND, ref WBounds);
            
                NativeMethods.WNDCLASSEX_D WClass = new NativeMethods.WNDCLASSEX_D();
                WClass.cbSize =   Marshal.SizeOf(typeof(NativeMethods.WNDCLASSEX_D));
                WClass.style = 0;
                WClass.lpfnWndProc =  lpfnWndProc;
                WClass.hInstance = NativeMethods.GetModuleHandle(null);
                WClass.cbClsExtra = 0;
                WClass.cbWndExtra = 0;
                WClass.hIcon = IntPtr.Zero;
                WClass.hCursor = new IntPtr(0);
                WClass.lpszMenuName = "this is lpszMenuName";
                WClass.lpszClassName = className;
                WClass.hIconSm = new IntPtr(0);
                // WClass.hbrBackground = NativeMethods.GetStockObject(NativeMethods.Brush.Black);

                ushort registerClassEx = NativeMethods.RegisterClassEx(WClass);
                EspHWND = NativeMethods.CreateWindowEx(NativeMethods.WS_EX.TRANSPARENT | NativeMethods.WS_EX.TOPMOST | NativeMethods.WS_EX.LAYERED, 
                    className, "this is lpWindowsName", NativeMethods.WS.POPUP, 0, 0, WBounds.right, WBounds.bottom,
                    GameHWND, new IntPtr(null), WClass.hInstance, new IntPtr(null));
                NativeMethods.SetLayeredWindowAttributes(EspHWND,  Color.FromArgb(255, 255, 255).ToArgb(), 255, NativeMethods.LWA_COLORKEY);
                
                // EspHWND = NativeMethods.CreateWindowExW(0, 
                //     className, "this is lpWindowsName", NativeMethods.WS.OVERLAPPEDWINDOW, 0, 0, WBounds.right, WBounds.bottom,
                //     new IntPtr(0), new IntPtr(null), WClass.hInstance, new IntPtr(null));
                // NativeMethods.SetLayeredWindowAttributes(EspHWND,  Color.FromArgb(255, 255, 255).ToArgb(), 255, NativeMethods.LWA_ALPHA);
            }
        }

        public void destory()
        {

            NativeMethods.UnregisterClass(className, NativeMethods.GetModuleHandle(null));
            NativeMethods.CloseHandle(GameHWND);
            NativeMethods.DestroyWindow(EspHWND);
            NativeMethods.TerminateThread(movThread,0);
            NativeMethods.TerminateThread(workLoopThread,0);
        }

        
        public void start()
        {
            NativeMethods.ShowWindow(EspHWND, 1);
            NativeMethods.UpdateWindow(EspHWND);
            
            NativeMethods.MSG Msg = default;
            NativeMethods.CloseHandle(
                NativeMethods.CreateThread(IntPtr.Zero, 0, MovWindow, IntPtr.Zero, 0, movThread));
            NativeMethods.CloseHandle(
                NativeMethods.CreateThread(IntPtr.Zero, 0, WorkLoop, IntPtr.Zero, 0,workLoopThread));
            
            
            while (NativeMethods.GetMessageW(ref Msg, IntPtr.Zero, 0, 0)) {
                NativeMethods.TranslateMessage(ref Msg);
                NativeMethods.DispatchMessageW(ref Msg);
            }
            
        }
        
        
        private IntPtr WorkLoop()
        {
            while (true)
            {
                unsafe
                {
                    fixed (NativeMethods.RECT* dev = &WBounds)
                    {
                        // 该函数向指定的窗体更新区域添加一个矩形，然后窗体跟新区域的这一部分将被重新绘制
                        NativeMethods.InvalidateRect(EspHWND, dev, true);
                        Thread.Sleep(16);
                    }
                }
            }
            return IntPtr.Zero;
        }

        private IntPtr MovWindow()
        {
            while (true)
            {
                
                NativeMethods.GetClientRect(GameHWND, ref WBounds);
                Point point = new Point ();
                point.X = WBounds.left;
                point.Y = WBounds.top;
                NativeMethods.ClientToScreen(GameHWND, ref point);
                NativeMethods.MoveWindow(EspHWND, point.X, point.Y, WBounds.right, WBounds.bottom, true);
                Thread.Sleep(1000);
            }
        }

        private  int lpfnWndProc(IntPtr hwnd, NativeMethods.WindowsMessage msg, IntPtr wParam, IntPtr lParam)
        {
              
            IntPtr Memhdc;
            IntPtr hdc;
            IntPtr Membitmap;
            switch (msg)
            {
                case NativeMethods.WindowsMessage.WM_PAINT : // WM_PAINT
                {

                    // https://www.cnblogs.com/zhoug2020/p/5622136.html
                    try
                    {
                        unsafe
                        {
                            IntPtr BoxPen = NativeMethods.CreatePen(NativeMethods.PenStyle.PS_SOLID, 1, (uint)ColorTranslator.ToWin32(Color.Red));

                            NativeMethods.PAINTSTRUCT ps;
                            hdc = NativeMethods.BeginPaint(hwnd, out ps);
                          
                            int win_width = WBounds.right - WBounds.left;
                            int win_height = WBounds.bottom + WBounds.left;
                            
                            Memhdc = NativeMethods.CreateCompatibleDC(hdc);
                            Membitmap = NativeMethods.CreateCompatibleBitmap(hdc, win_width, win_height);
                            NativeMethods.SelectObject(Memhdc, Membitmap);
                            NativeMethods.FillRect(Memhdc, ref WBounds, new IntPtr(0x0));
                            NativeMethods.TextOut(Memhdc, 0, 0, ("这是一个简单的绘图例子"), ("这是一个简单的绘图例子").Length*2);

                            var oldPen = NativeMethods.SelectObject(Memhdc, BoxPen);
                            Draw(Memhdc, 200, 100,400,200);

                            Int32 SRCCOPY = 0x00CC0020;
                            // Bitblt作用将某一内存块的数据传送到另一内存块
                            NativeMethods.BitBlt(hdc, 0, 0, win_width, win_height, Memhdc, 0, 0, SRCCOPY);
                            
                            NativeMethods.DeleteObject(NativeMethods.SelectObject(Memhdc, oldPen));
                            NativeMethods.DeleteObject(Membitmap);
                            NativeMethods.DeleteDC(Memhdc);
                            NativeMethods.DeleteDC(hdc);
                            NativeMethods.EndPaint(hwnd, ref ps);
                            fixed (NativeMethods.RECT* dev = &WBounds)
                            {
                                // 函数来强制使客户区的一个矩阵失效
                                NativeMethods.ValidateRect(hwnd, dev);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    return 0;
                }
                
                case NativeMethods.WindowsMessage.WM_ERASEBKGND: // WM_ERASEBKGND
                    return 1;
                case NativeMethods.WindowsMessage.WM_CLOSE: // WM_CLOSE
                    NativeMethods.DestroyWindow(hwnd);
                    break;
                case NativeMethods.WindowsMessage.WM_DESTROY: // WM_DESTROY
                    NativeMethods.PostQuitMessage(0);
                    break;
                default:
                    return NativeMethods.DefWindowProc(hwnd, msg, wParam, lParam);
            }

            return 0;
        }
        
        void Draw(IntPtr hdc,int x,int y,int w,int h)
        {
            NativeMethods.Rectangle(hdc, x, y, w, h);
        }
    }
    
    
}