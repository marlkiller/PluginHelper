using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using PluginHelper.Native;

namespace PluginHelper
{
    public partial class GameEspForm : Form
    {
        public IntPtr prosessId;
        public Thread thread;
        
        public GameEspForm()
        {
            InitializeComponent();
            Show();
            
            TopMost = true;
            BackColor = Color.FromArgb(0, 0, 1);
            TransparencyKey = BackColor;
            if (thread == null)
            {
                thread = new Thread(MovWindow);
            }  
        }
        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams CP = base.CreateParams;
                CP.ExStyle = (int) (NativeMethods.WS_EX.TRANSPARENT | NativeMethods.WS_EX.TOPMOST | NativeMethods.WS_EX.LAYERED);
                CP.Style = unchecked((int) NativeMethods.WS.POPUP);
                return CP;
            }
        }
        
        
        private void MovWindow(Object o)
        {
            while (true)
            {
                
                Random rd = new Random();
                Drawing(new Rectangle(10 ,10,200 + rd.Next(1,100),200 + rd.Next(1,100)), Color.Red);
                
                NativeMethods.RECT WBounds = default;
                IntPtr GameHWND = NativeMethods.GetWindowHandle(prosessId);
                NativeMethods.GetClientRect(GameHWND, ref WBounds);
                Point point = new Point ();
                point.X = WBounds.left;
                point.Y = WBounds.top;
                NativeMethods.ClientToScreen(GameHWND, ref point);
                Left = point.X;
                Top = point.Y;
                Width = WBounds.right;
                Height = WBounds.bottom;
                Thread.Sleep(1000);
            }
        }
        public void Drawing(Rectangle rectangle,Color line_color) {
            try
            {
                var MyBuff = new BufferedGraphicsContext().Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));
                var e = MyBuff.Graphics;
                e.Clear(BackColor);
                var pen = new Pen(line_color, 3);
                e.DrawRectangle(pen, rectangle);
                MyBuff.Render();
                MyBuff.Dispose();
            }
            catch { 
            
            }
        }
    }
}