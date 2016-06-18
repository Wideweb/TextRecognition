using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ViewComponents
{
    public partial class DrawBox : UserControl
    {
        private readonly Graphics graphics;
        private readonly Graphics visibleGraphics;
        private readonly Pen pen;
        private Bitmap bmp;

        private bool isMouseDown;
        private int prevMouseX;
        private int prevMouseY;

        public DrawBox()
        {
            InitializeComponent();

            bmp = new Bitmap(Width, Height);
            graphics = Graphics.FromImage(bmp);
            visibleGraphics = CreateGraphics();

            pen = new Pen(new SolidBrush(Color.Black), 20);
            pen.EndCap = LineCap.Round;
            pen.StartCap = LineCap.Round;
        }

        private void DrawBox_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            prevMouseX = e.X;
            prevMouseY = e.Y;
        }

        private void DrawBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouseDown) return;
            
            graphics.DrawLine(pen, prevMouseX, prevMouseY, e.X, e.Y);
            visibleGraphics.DrawLine(pen, prevMouseX, prevMouseY, e.X, e.Y);
            prevMouseX = e.X;
            prevMouseY = e.Y;
        }

        private void DrawBox_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        public void Clear()
        {
            visibleGraphics.Clear(Color.White);
            graphics.Clear(Color.White);
        }

        public Bitmap GetImage()
        {
            return bmp;
        }
    }
}
