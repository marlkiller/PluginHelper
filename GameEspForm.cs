using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PluginHelper
{
    public partial class GameEspForm : Form
    {
        public GameEspForm()
        {
            InitializeComponent();
            
        }
        
        protected override CreateParams CreateParams
        {
            get
            {
                int WS_EX_TOOLWINDOW = 0x80;
                CreateParams CP = base.CreateParams;

                CP.ExStyle = CP.ExStyle | WS_EX_TOOLWINDOW;
                return CP;
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