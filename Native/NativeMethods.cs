using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace PluginHelper.Native
{
    public class NativeMethods

    {
    
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole(); 
            
        public enum WindowsMessage : uint
        {
            WM_NULL = 0x0000,
            WM_CREATE = 0x0001,
            WM_DESTROY = 0x0002,
            WM_MOVE = 0x0003,
            WM_SIZE = 0x0005,
            WM_ACTIVATE = 0x0006,
            WM_SETFOCUS = 0x0007,
            WM_KILLFOCUS = 0x0008,
            WM_ENABLE = 0x000A,
            WM_SETREDRAW = 0x000B,
            WM_SETTEXT = 0x000C,
            WM_GETTEXT = 0x000D,
            WM_GETTEXTLENGTH = 0x000E,
            WM_PAINT = 0x000F,
            WM_CLOSE = 0x0010,
            WM_QUERYENDSESSION = 0x0011,
            WM_QUERYOPEN = 0x0013,
            WM_ENDSESSION = 0x0016,
            WM_QUIT = 0x0012,
            WM_ERASEBKGND = 0x0014,
            WM_SYSCOLORCHANGE = 0x0015,
            WM_SHOWWINDOW = 0x0018,
            WM_WININICHANGE = 0x001A,
            WM_SETTINGCHANGE = WM_WININICHANGE,
            WM_DEVMODECHANGE = 0x001B,
            WM_ACTIVATEAPP = 0x001C,
            WM_FONTCHANGE = 0x001D,
            WM_TIMECHANGE = 0x001E,
            WM_CANCELMODE = 0x001F,
            WM_SETCURSOR = 0x0020,
            WM_MOUSEACTIVATE = 0x0021,
            WM_CHILDACTIVATE = 0x0022,
            WM_QUEUESYNC = 0x0023,
            WM_GETMINMAXINFO = 0x0024,
            WM_PAINTICON = 0x0026,
            WM_ICONERASEBKGND = 0x0027,
            WM_NEXTDLGCTL = 0x0028,
            WM_SPOOLERSTATUS = 0x002A,
            WM_DRAWITEM = 0x002B,
            WM_MEASUREITEM = 0x002C,
            WM_DELETEITEM = 0x002D,
            WM_VKEYTOITEM = 0x002E,
            WM_CHARTOITEM = 0x002F,
            WM_SETFONT = 0x0030,
            WM_GETFONT = 0x0031,
            WM_SETHOTKEY = 0x0032,
            WM_GETHOTKEY = 0x0033,
            WM_QUERYDRAGICON = 0x0037,
            WM_COMPAREITEM = 0x0039,
            WM_GETOBJECT = 0x003D,
            WM_COMPACTING = 0x0041,
            WM_WINDOWPOSCHANGING = 0x0046,
            WM_WINDOWPOSCHANGED = 0x0047,
            WM_COPYDATA = 0x004A,
            WM_CANCELJOURNAL = 0x004B,
            WM_NOTIFY = 0x004E,
            WM_INPUTLANGCHANGEREQUEST = 0x0050,
            WM_INPUTLANGCHANGE = 0x0051,
            WM_TCARD = 0x0052,
            WM_HELP = 0x0053,
            WM_USERCHANGED = 0x0054,
            WM_NOTIFYFORMAT = 0x0055,
            WM_CONTEXTMENU = 0x007B,
            WM_STYLECHANGING = 0x007C,
            WM_STYLECHANGED = 0x007D,
            WM_DISPLAYCHANGE = 0x007E,
            WM_GETICON = 0x007F,
            WM_SETICON = 0x0080,
            WM_NCCREATE = 0x0081,
            WM_NCDESTROY = 0x0082,
            WM_NCCALCSIZE = 0x0083,
            WM_NCHITTEST = 0x0084,
            WM_NCPAINT = 0x0085,
            WM_NCACTIVATE = 0x0086,
            WM_GETDLGCODE = 0x0087,
            WM_SYNCPAINT = 0x0088,
            WM_NCMOUSEMOVE = 0x00A0,
            WM_NCLBUTTONDOWN = 0x00A1,
            WM_NCLBUTTONUP = 0x00A2,
            WM_NCLBUTTONDBLCLK = 0x00A3,
            WM_NCRBUTTONDOWN = 0x00A4,
            WM_NCRBUTTONUP = 0x00A5,
            WM_NCRBUTTONDBLCLK = 0x00A6,
            WM_NCMBUTTONDOWN = 0x00A7,
            WM_NCMBUTTONUP = 0x00A8,
            WM_NCMBUTTONDBLCLK = 0x00A9,
            WM_NCXBUTTONDOWN = 0x00AB,
            WM_NCXBUTTONUP = 0x00AC,
            WM_NCXBUTTONDBLCLK = 0x00AD,
            WM_INPUT_DEVICE_CHANGE = 0x00FE,
            WM_INPUT = 0x00FF,
            WM_KEYFIRST = 0x0100,
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101,
            WM_CHAR = 0x0102,
            WM_DEADCHAR = 0x0103,
            WM_SYSKEYDOWN = 0x0104,
            WM_SYSKEYUP = 0x0105,
            WM_SYSCHAR = 0x0106,
            WM_SYSDEADCHAR = 0x0107,
            WM_UNICHAR = 0x0109,
            WM_KEYLAST = 0x0109,
            WM_IME_STARTCOMPOSITION = 0x010D,
            WM_IME_ENDCOMPOSITION = 0x010E,
            WM_IME_COMPOSITION = 0x010F,
            WM_IME_KEYLAST = 0x010F,
            WM_INITDIALOG = 0x0110,
            WM_COMMAND = 0x0111,
            WM_SYSCOMMAND = 0x0112,
            WM_TIMER = 0x0113,
            WM_HSCROLL = 0x0114,
            WM_VSCROLL = 0x0115,
            WM_INITMENU = 0x0116,
            WM_INITMENUPOPUP = 0x0117,
            WM_MENUSELECT = 0x011F,
            WM_MENUCHAR = 0x0120,
            WM_ENTERIDLE = 0x0121,
            WM_MENURBUTTONUP = 0x0122,
            WM_MENUDRAG = 0x0123,
            WM_MENUGETOBJECT = 0x0124,
            WM_UNINITMENUPOPUP = 0x0125,
            WM_MENUCOMMAND = 0x0126,
            WM_CHANGEUISTATE = 0x0127,
            WM_UPDATEUISTATE = 0x0128,
            WM_QUERYUISTATE = 0x0129,
            WM_CTLCOLORMSGBOX = 0x0132,
            WM_CTLCOLOREDIT = 0x0133,
            WM_CTLCOLORLISTBOX = 0x0134,
            WM_CTLCOLORBTN = 0x0135,
            WM_CTLCOLORDLG = 0x0136,
            WM_CTLCOLORSCROLLBAR = 0x0137,
            WM_CTLCOLORSTATIC = 0x0138,
            WM_MOUSEFIRST = 0x0200,
            WM_MOUSEMOVE = 0x0200,
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_LBUTTONDBLCLK = 0x0203,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_RBUTTONDBLCLK = 0x0206,
            WM_MBUTTONDOWN = 0x0207,
            WM_MBUTTONUP = 0x0208,
            WM_MBUTTONDBLCLK = 0x0209,
            WM_MOUSEWHEEL = 0x020A,
            WM_XBUTTONDOWN = 0x020B,
            WM_XBUTTONUP = 0x020C,
            WM_XBUTTONDBLCLK = 0x020D,
            WM_MOUSEHWHEEL = 0x020E,
            WM_MOUSELAST = 0x020E,
            WM_PARENTNOTIFY = 0x0210,
            WM_ENTERMENULOOP = 0x0211,
            WM_EXITMENULOOP = 0x0212,
            WM_NEXTMENU = 0x0213,
            WM_SIZING = 0x0214,
            WM_CAPTURECHANGED = 0x0215,
            WM_MOVING = 0x0216,
            WM_POWERBROADCAST = 0x0218,
            WM_DEVICECHANGE = 0x0219,
            WM_MDICREATE = 0x0220,
            WM_MDIDESTROY = 0x0221,
            WM_MDIACTIVATE = 0x0222,
            WM_MDIRESTORE = 0x0223,
            WM_MDINEXT = 0x0224,
            WM_MDIMAXIMIZE = 0x0225,
            WM_MDITILE = 0x0226,
            WM_MDICASCADE = 0x0227,
            WM_MDIICONARRANGE = 0x0228,
            WM_MDIGETACTIVE = 0x0229,
            WM_MDISETMENU = 0x0230,
            WM_ENTERSIZEMOVE = 0x0231,
            WM_EXITSIZEMOVE = 0x0232,
            WM_DROPFILES = 0x0233,
            WM_MDIREFRESHMENU = 0x0234,
            WM_IME_SETCONTEXT = 0x0281,
            WM_IME_NOTIFY = 0x0282,
            WM_IME_CONTROL = 0x0283,
            WM_IME_COMPOSITIONFULL = 0x0284,
            WM_IME_SELECT = 0x0285,
            WM_IME_CHAR = 0x0286,
            WM_IME_REQUEST = 0x0288,
            WM_IME_KEYDOWN = 0x0290,
            WM_IME_KEYUP = 0x0291,
            WM_MOUSEHOVER = 0x02A1,
            WM_MOUSELEAVE = 0x02A3,
            WM_NCMOUSEHOVER = 0x02A0,
            WM_NCMOUSELEAVE = 0x02A2,
            WM_WTSSESSION_CHANGE = 0x02B1,
            WM_TABLET_FIRST = 0x02c0,
            WM_TABLET_LAST = 0x02df,
            WM_DPICHANGED = 0x02E0,
            WM_CUT = 0x0300,
            WM_COPY = 0x0301,
            WM_PASTE = 0x0302,
            WM_CLEAR = 0x0303,
            WM_UNDO = 0x0304,
            WM_RENDERFORMAT = 0x0305,
            WM_RENDERALLFORMATS = 0x0306,
            WM_DESTROYCLIPBOARD = 0x0307,
            WM_DRAWCLIPBOARD = 0x0308,
            WM_PAINTCLIPBOARD = 0x0309,
            WM_VSCROLLCLIPBOARD = 0x030A,
            WM_SIZECLIPBOARD = 0x030B,
            WM_ASKCBFORMATNAME = 0x030C,
            WM_CHANGECBCHAIN = 0x030D,
            WM_HSCROLLCLIPBOARD = 0x030E,
            WM_QUERYNEWPALETTE = 0x030F,
            WM_PALETTEISCHANGING = 0x0310,
            WM_PALETTECHANGED = 0x0311,
            WM_HOTKEY = 0x0312,
            WM_PRINT = 0x0317,
            WM_PRINTCLIENT = 0x0318,
            WM_APPCOMMAND = 0x0319,
            WM_THEMECHANGED = 0x031A,
            WM_CLIPBOARDUPDATE = 0x031D,
            WM_DWMCOMPOSITIONCHANGED = 0x031E,
            WM_DWMNCRENDERINGCHANGED = 0x031F,
            WM_DWMCOLORIZATIONCOLORCHANGED = 0x0320,
            WM_DWMWINDOWMAXIMIZEDCHANGE = 0x0321,
            WM_GETTITLEBARINFOEX = 0x033F,
            WM_HANDHELDFIRST = 0x0358,
            WM_HANDHELDLAST = 0x035F,
            WM_AFXFIRST = 0x0360,
            WM_AFXLAST = 0x037F,
            WM_PENWINFIRST = 0x0380,
            WM_PENWINLAST = 0x038F,
            WM_TOUCH = 0x0240,
            WM_APP = 0x8000,
            WM_USER = 0x0400,
        }

        public delegate int WndProc(IntPtr hWnd, WindowsMessage msg, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class WNDCLASSEX_D
        {
            public int cbSize = 0;
            public int style = 0;
            public WndProc lpfnWndProc = null;
            public int cbClsExtra = 0;
            public int cbWndExtra = 0;
            public IntPtr hInstance = IntPtr.Zero;
            public IntPtr hIcon = IntPtr.Zero;
            public IntPtr hCursor = IntPtr.Zero;
            public IntPtr hbrBackground = IntPtr.Zero;
            public string lpszMenuName = null;
            public string lpszClassName = null;
            public IntPtr hIconSm = IntPtr.Zero;
        }


        [DllImport("user32", EntryPoint = "RegisterClassEx", CharSet = CharSet.Unicode, SetLastError = true,
            BestFitMapping = false)]
        internal static extern UInt16 RegisterClassEx(WNDCLASSEX_D wc_d);


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int WNDPROC(IntPtr hwnd, uint uMsg, IntPtr wParam, IntPtr lParam);


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct HINSTANCE
        {
            public IntPtr ptr;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct HICON
        {
            public IntPtr ptr;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct HBRUSH
        {
            public IntPtr ptr;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WNDCLASSEXW
        {
            public uint cbSize;
            public uint style;
            public WNDPROC lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public HINSTANCE hInstance;
            public HICON hIcon;
            public HICON hCursor;
            public HBRUSH hbrBackground;
            [MarshalAs(UnmanagedType.LPWStr)] public string lpszMenuName;
            [MarshalAs(UnmanagedType.LPWStr)] public string lpszClassName;
            public HICON hIconSm;
        }

        [DllImport("user32.dll")]
        public static extern ushort RegisterClassExW(
            ref WNDCLASSEXW __param__0
        );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr DefWindowProcW(IntPtr hWnd, UInt32 msg, UIntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "DefWindowProcW")]
        internal static extern int DefWindowProc(IntPtr hWnd, WindowsMessage msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern void PostQuitMessage(int nExitCode);
   

        public enum GWL
        {
            WNDPROC = (-4),
            HINSTANCE = (-6),
            HWNDPARENT = (-8),
            STYLE = (-16),
            EXSTYLE = (-20),
            USERDATA = (-21),
            ID = (-12)
        }

        // We only ever call this on 32 bit so IntPtr is correct
        [DllImport("User32", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr GetWindowLongW(IntPtr hWnd, GWL nIndex);

        [DllImport("User32", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetWindowLongPtrW(IntPtr hWnd, GWL nIndex);

        public static IntPtr GetWindowLong(IntPtr hWnd, GWL nIndex)
        {
            if (IntPtr.Size == 4)
            {
                return GetWindowLongW(hWnd, nIndex);
            }

            return GetWindowLongPtrW(hWnd, nIndex);
        }



        [Flags]
        public enum WS_EX : uint
        {
            DEFAULT = 0x00000000,
            DLGMODALFRAME = 0x00000001,
            NOPARENTNOTIFY = 0x00000004,
            TOPMOST = 0x00000008,
            ACCEPTFILES = 0x00000010,
            // 事件穿过窗口，操作后面的窗口
            TRANSPARENT = 0x00000020,
            MDICHILD = 0x00000040,
            TOOLWINDOW = 0x00000080,
            WINDOWEDGE = 0x00000100,
            CLIENTEDGE = 0x00000200,
            CONTEXTHELP = 0x00000400,
            RIGHT = 0x00001000,
            LEFT = 0x00000000,
            RTLREADING = 0x00002000,
            LTRREADING = 0x00000000,
            LEFTSCROLLBAR = 0x00004000,
            RIGHTSCROLLBAR = 0x00000000,
            CONTROLPARENT = 0x00010000,
            STATICEDGE = 0x00020000,
            APPWINDOW = 0x00040000,
            OVERLAPPEDWINDOW = WINDOWEDGE | CLIENTEDGE,
            PALETTEWINDOW = WINDOWEDGE | TOOLWINDOW | TOPMOST,
            LAYERED = 0x00080000,
            NOINHERITLAYOUT = 0x00100000,
            NOREDIRECTIONBITMAP = 0x00200000,
            LAYOUTRTL = 0x00400000,
            COMPOSITED = 0x02000000,
            NOACTIVATE = 0x08000000
        }

        public enum WS : uint
        {
            OVERLAPPED = 0x00000000,
            POPUP = 0x80000000,
            CHILD = 0x40000000,
            MINIMIZE = 0x20000000,
            VISIBLE = 0x10000000,
            DISABLED = 0x08000000,
            CLIPSIBLINGS = 0x04000000,
            CLIPCHILDREN = 0x02000000,
            MAXIMIZE = 0x01000000,
            CAPTION = 0x00C00000,
            BORDER = 0x00800000,
            DLGFRAME = 0x00400000,
            VSCROLL = 0x00200000,
            HSCROLL = 0x00100000,
            SYSMENU = 0x00080000,
            THICKFRAME = 0x00040000,
            TABSTOP = 0x00010000,
            MINIMIZEBOX = 0x00020000,
            MAXIMIZEBOX = 0x00010000,

            OVERLAPPEDWINDOW = (OVERLAPPED | CAPTION | SYSMENU |
                                THICKFRAME | MINIMIZEBOX | MAXIMIZEBOX)
        }

        [DllImport("User32", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CreateWindowExW(
            WS_EX dwExStyle,
            string lpClassName,
            string lpWindowName,
            WS dwStyle,
            int X,
            int Y,
            int nWidth,
            int nHeight,
            IntPtr hWndParent,
            IntPtr hMenu,
            IntPtr hInst,
            [MarshalAs(UnmanagedType.AsAny)] object lpParam);

        [DllImport("User32", SetLastError = true)]
        internal static extern IntPtr CreateWindowEx(
            WS_EX dwExStyle, string lpClassName, string lpWindowName, WS dwStyle,
            int X, int Y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu,
            IntPtr hInstance, IntPtr lpParam);


        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_LAYERED = 0x80000;
        public const int LWA_ALPHA = 0x2;
        public const int LWA_COLORKEY = 0x1;
        
        // hwnd
        //     指定分层窗口句柄。
        // crKey
        // 指定需要透明的背景颜色值，可用RGB()宏
        // bAlpha
        // 设置透明度，0表示完全透明，255表示不透明
        // dwFlags
        // 透明方式。可取以下值：
        // LWA_ALPHA时：crKey参数无效，bAlpha参数有效；
        // LWA_COLORKEY：窗体中的所有颜色为crKey的地方将变为透明，bAlpha参数无效。。
        // LWA_ALPHA | LWA_COLORKEY：crKey的地方将变为全透明，而其它地方根据bAlpha参数确定透明度。
        [DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")]
        public static extern int SetLayeredWindowAttributes(IntPtr Handle, int crKey, byte bAlpha, int dwFlags);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool UpdateWindow(IntPtr hWnd);

        [StructLayout(LayoutKind.Sequential)]
        public struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            public RECT rcPaint;
            public bool fRestore;
            public bool fIncUpdate;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }


        [DllImport("User32", ExactSpelling = true, EntryPoint = "BeginPaint", CharSet = CharSet.Auto)]
        public static extern IntPtr BeginPaint(IntPtr hWnd, out PAINTSTRUCT lpPaint);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "GetStockObject", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CriticalGetStockObject(int stockObject);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap([In] IntPtr hdc, int nWidth, int nHeight);


        [DllImport("user32", EntryPoint = "UnregisterClass")]
        public static extern int UnregisterClassA(string lpClassName, int hInstance);

        // GetWindowRect是取得窗口在屏幕坐标系下的RECT坐标（包括客户区和非客户区），这样可以得到窗口的大小和相对屏幕左上角(0,0)的位置。 
        // GetClientRect取得窗口客户区(不包括非客户区)在客户区坐标系下的RECT坐标,可以得到窗口的大小，而不能得到相对屏幕的位置，因为这个矩阵是在客户区坐标系下（相对于窗口客户区的左上角）的。　　
        // 用GetClientRect返回的RECT结构上左为零, 右下分别对应客户区的宽度和高度;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);


        [DllImport("user32", EntryPoint = "GetClientRect", ExactSpelling = true, CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern bool GetClientRect(IntPtr hWnd, [In, Out] ref NativeMethods.RECT rect);


        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public int Width
            {
                get { return right - left; }
            }

            public int Height
            {
                get { return bottom - top; }
            }

            public void Offset(int dx, int dy)
            {
                left += dx;
                top += dy;
                right += dx;
                bottom += dy;
            }

            public bool IsEmpty
            {
                get { return left >= right || top >= bottom; }
            }
        }

        [DllImport("gdi32.dll")]
        public static extern bool LineTo(IntPtr hDC, int xEnd, int yEnd);

        [DllImport("gdi32.dll")]
        public static extern bool MoveToEx(IntPtr hDC, int xStart, int yStart, IntPtr prevPoint);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);

        [DllImport("gdi32.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern Boolean BitBlt(IntPtr hDC, int x, int y, int w, int h, IntPtr srcDc, int xSrc, int ySrc,
            int dwRop);

        [DllImport("gdi32.dll")]
        public static extern IntPtr DeleteDC(IntPtr hDC);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern Boolean EndPaint(IntPtr hWnd, ref PAINTSTRUCT lpPaint);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern unsafe Boolean ValidateRect(IntPtr hWnd, RECT* lpPaint);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateSolidBrush(int crColor);


        public enum PenStyle : int
        {
            PS_SOLID = 0, //The pen is solid.
            PS_DASH = 1, //The pen is dashed.
            PS_DOT = 2, //The pen is dotted.
            PS_DASHDOT = 3, //The pen has alternating dashes and dots.
            PS_DASHDOTDOT = 4, //The pen has alternating dashes and double dots.
            PS_NULL = 5, //The pen is invisible.

            PS_INSIDEFRAME =6, // Normally when the edge is drawn, it’s centred on the outer edge meaning that half the width of the pen is drawn
            // outside the shape’s edge, half is inside the shape’s edge. When PS_INSIDEFRAME is specified the edge is drawn
            //completely inside the outer edge of the shape.
            PS_USERSTYLE = 7,
            PS_ALTERNATE = 8,
            PS_STYLE_MASK = 0x0000000F,
            PS_ENDCAP_ROUND = 0x00000000,
            PS_ENDCAP_SQUARE = 0x00000100,
            PS_ENDCAP_FLAT = 0x00000200,
            PS_ENDCAP_MASK = 0x00000F00,
            PS_JOIN_ROUND = 0x00000000,
            PS_JOIN_BEVEL = 0x00001000,
            PS_JOIN_MITER = 0x00002000,
            PS_JOIN_MASK = 0x0000F000,
            PS_COSMETIC = 0x00000000,
            PS_GEOMETRIC = 0x00010000,
            PS_TYPE_MASK = 0x000F0000
        };

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreatePen(PenStyle fnPenStyle, int nWidth, uint crColor);

        [DllImport("gdi32.dll")]
        public static extern bool Rectangle(IntPtr hdc, int a, int b, int c, int d);

        [DllImportAttribute("USER32.DLL")]
        public static extern long FillRect(IntPtr hDc, ref RECT lpRect, IntPtr hBrush);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool TextOut(IntPtr HDC, int X, int Y, string str, int cout);


        [DllImport("User32.dll")]
        public static extern IntPtr
            GetDC(IntPtr Hwnd); //其在MSDN中原型为HDC GetDC(HWND hWnd),HDC和HWND都是驱动器句柄（长指针），在C#中只能用IntPtr代替了

        [DllImport("User32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        public static bool IsWindowExist(IntPtr handle)
        {
            return (!(GetWindow(handle, 4) != IntPtr.Zero) && IsWindowVisible(handle));
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool IsWindowVisible(IntPtr hWnd);


        /// 获取应用程序窗口句柄
        public static IntPtr GetWindowHandle(IntPtr processId)
        {
            return Process.GetProcessById(processId.ToInt32()).MainWindowHandle;
        }

        public delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool EnumWindows(EnumThreadWindowsCallback callback, IntPtr extraData);


        [DllImportAttribute("user32.dll")]
        public static extern int MessageBoxA(int Modal, string Message, string Caption, int Options);


        // Wait functions
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        [Flags]
        public enum SnapshotFlags : uint
        {
            HeapList = 0x00000001,
            Process = 0x00000002,
            Thread = 0x00000004,
            Module = 0x00000008,
            Module32 = 0x00000010,
            Inherit = 0x80000000,
            All = 0x0000001F,
            NoHeaps = 0x40000000
        }

        [DllImport("KERNEL32.DLL ")]
        public static extern IntPtr CreateToolhelp32Snapshot(SnapshotFlags flags, IntPtr processid);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        [DllImport("kernel32.dll")]
        public static extern bool Module32First(IntPtr hSnapshot, ref MODULEENTRY32 lpme);

        [DllImport("kernel32.dll")]
        public static extern bool Module32Next(IntPtr hSnapshot, ref MODULEENTRY32 lpme);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct MODULEENTRY32
        {
            internal uint dwSize;
            internal uint th32ModuleID;
            internal uint th32ProcessID;
            internal uint GlblcntUsage;
            internal uint ProccntUsage;
            public IntPtr modBaseAddr;
            internal uint modBaseSize;
            IntPtr hModule;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            internal string szModule;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            internal string szExePath;
        };

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern short GetKeyState(int vKey);

        [DllImport("user32.dll")]
        internal static extern int ToAscii(uint uVirtKey, uint uScanCode, byte[] lpKeyState, [Out] StringBuilder lpChar,
            uint uFlags);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass,
            string lpszWindow);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentThreadId();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(IntPtr lpModule);

        [DllImport("kernel32", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out IntPtr lpdwProcessId);

        [StructLayout(LayoutKind.Sequential)]

        public struct MSG
        {
            IntPtr hwnd;
            uint message;
            UIntPtr wParam;
            IntPtr lParam;
            int time;
            POINT pt;
            int lPrivate;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }

        [DllImport("User32", SetLastError = true, EntryPoint = "GetMessageW", ExactSpelling = true,
            CharSet = CharSet.Unicode)]
        public static extern Boolean GetMessageW([In, Out] ref MSG msg, IntPtr hWnd, int uMsgFilterMin,
            int uMsgFilterMax);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMessage(ref MSG msg, IntPtr hwnd, int minFilter, int maxFilter);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool TranslateMessage([In, Out] ref MSG msg);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr DispatchMessageA(ref MSG msg);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr DispatchMessageW(ref MSG msg);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int DispatchMessage(ref MSG msg);

        // [1]. hProcess
        //     由OpenProcess返回的进程句柄。
        // 如参数传数据为 INVALID_HANDLE_VALUE 【即-1】目标进程为自身进程
        // [2]. lpBaseAddress
        //     要写的内存首地址
        // 在写入之前，此函数将先检查目标地址是否可用，并能容纳待写入的数据。
        // [3]. lpBuffer
        //     指向要写的数据的指针。
        // [4]. nSize
        //     要写入的字节数。
        // 返回值
        //     非零值代表成功。
        // 可用GetLastError获取更多的错误详细信息
        // void* buffer , [MarshalAs(UnmanagedType.AsAny)] object lpBuffer
        [DllImport("kernel32.dll")]
        // public static extern int WriteProcessMemory(IntPtr hwnd, IntPtr baseaddress, [MarshalAs(UnmanagedType.AsAny)] object lpBuffer, int nsize, out IntPtr lpNumberOfBytesWritten);
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize,
            out IntPtr lpNumberOfBytesWritten);


        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize,
            out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr CloseHandle(IntPtr hObject);


        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hwnd, string lpname);

        [DllImport("kernel32.dll")]
        public static extern int GetModuleHandleA(string name);


        // public delegate int ThreadProc(IntPtr param);
        public delegate IntPtr ThreadProc();

        [DllImport("kernel32")]
        public static extern IntPtr CreateThread(
            IntPtr lpThreadAttributes,
            uint dwStackSize,
            ThreadProc lpStartAddress, // ThreadProc as friendly delegate
            // IntPtr lpStartAddress, // ThreadProc as friendly delegate
            IntPtr lpParameter,
            uint dwCreationFlags,
            uint dwThreadId);

        [DllImport("user32", ExactSpelling = true)]
        public static extern unsafe Boolean InvalidateRect(IntPtr hWnd, RECT* lpRect, Boolean bErase);

        //     hProcess 
        //          [输入] 进程句柄
        //     lpThreadAttributes 
        //         [输入] 线程安全描述字，指向SECURITY_ATTRIBUTES结构的指针
        //     dwStackSize 
        //         [输入] 线程栈大小，以字节表示
        //     lpStartAddress 
        //         [输入] 一个LPTHREAD_START_ROUTINE类型的指针，指向在远程进程中执行的函数地址
        //     lpParameter 
        //         [输入] 传入参数
        //     dwCreationFlags 
        //         [输入] 创建线程的其它标志
        //
        //     lpThreadId 
        //         [输出] 线程身份标志，如果为NULL,则不返回
        //
        //     返回值
        // 成功返回新线程句柄，失败返回NULL，并且可调用GetLastError获得错误值。
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize,
            IntPtr lpStartAddress,
            IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        // pointer = Generic.GetLibraryAddress("kernel32.dll", "CreateRemoteThread");
        // var createRemoteThread = Marshal.GetDelegateForFunctionPointer(pointer, typeof(CreateRemoteThread)) as CreateRemoteThread;
        // createRemoteThread(hProcess, IntPtr.Zero, 0, alloc, IntPtr.Zero, 0, IntPtr.Zero);

        public static bool Is64BitProcess()
        {
            return IntPtr.Size == 8;
        }


        [DllImport("psapi.dll")]
        public static extern bool EnumProcessModulesEx(IntPtr hProcess,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)] [In] [Out]
            IntPtr[] lphModule,
            int cb, [MarshalAs(UnmanagedType.U4)] out int lpcbNeeded, uint dwFilterFlag);

        [DllImport("psapi.dll")]
        public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName,
            [In] [MarshalAs(UnmanagedType.U4)] int nSize);

        public static readonly int ExecuteReadWrite = 0x40;
        public static readonly int Commit = 0x1000;


        // 申请内存空间
        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualAllocEx(IntPtr hwnd, IntPtr lpaddress, IntPtr size, int type, int tect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetThreadContext(IntPtr hThread, ref ThreadHijack.CONTEXT64 lpContext);

        [DllImport("kernel32.dll")]
        public static extern int ResumeThread(IntPtr hThread);

        public static readonly int Release = 0x8000;


        // hProcess
        //     目标进程的句柄。该句柄必须拥有 PROCESS_VM_OPERATION 权限。
        // lpAddress
        //     指向要释放的虚拟内存空间首地址的指针。
        // 如果 dwFreeType 为 MEM_RELEASE, 则该参数必须为VirtualAllocEx的返回值.
        // dwSize
        //     虚拟内存空间的字节数。
        // 如果 dwFreeType 为 MEM_RELEASE，则 dwSize 必须为0 . 按 VirtualAllocEx申请时的大小全部释放。
        // 如果dwFreeType 为 MEM_DECOMMIT, 则释放从lpAddress 开始的一个或多个字节 ，即 lpAddress +dwSize。
        // dwFreeType
        // 释放内存空间
        [DllImport("kernel32.dll")]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, int dwFreeType);


        public static readonly int PROCESS_ALL_ACCESS = 0x1F0FFF;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenThread(ThreadHijack.ThreadAccess dwDesiredAccess, bool bInheritHandle,
            uint dwThreadId);

        [DllImport("kernel32.dll")]
        public static extern uint SuspendThread(IntPtr hThread);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetThreadContext(IntPtr hThread, ref ThreadHijack.CONTEXT64 lpContext);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, IntPtr dwProcessId);


        // SetWindowsHookEx(
        // idHook: Integer;   {钩子类型}
        // lpfn: TFNHookProc; {函数指针}
        // hmod: HINST;       {是模块的句柄，在本机代码中，对应 dll 的句柄（可在 dll 的入口函数中获取）; 一般是 HInstance; 如果是当前线程这里可以是 0}
        // dwThreadId: DWORD  {关联的线程; 可用 GetCurrentThreadId 获取当前线程; 0 表示是系统级钩子}
        // ): HHOOK;            {返回钩子的句柄; 0 表示失败}
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hMod, IntPtr dwThreadId);


        [DllImport("user32.dll")]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);


        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        internal static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc,
            WinEventDelegate lpfnWinEventProc, uint idProcess, IntPtr idThread, uint dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool UnhookWinEvent(IntPtr hWinEventHook);


        public delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

        internal delegate void WinEventDelegate(
            IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread,
            uint dwmsEventTime);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

    }
}