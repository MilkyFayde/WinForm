using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class PaintForm : Form
    {
        public Point end = new Point();
        public Point start = new Point();
        public Pen pen = new Pen(Color.Black, 4);
        public Graphics graphics;
        MyFigures figures; // list of figures
        Action BrushType; // delegate for currenly selected brush

        public PaintForm()
        {
            Start();
        }

        public PaintForm(string path)
        {
            Start();
            LoadFigures(path);
        }

        void Start()
        {
            InitializeComponent();
            figures = new MyFigures(this.Width, this.Height);
            graphics = panel1.CreateGraphics();
            pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            BrushType = PenMode;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            start = e.Location;
        } // panel1_MouseDown

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (BrushType != PenMode)
            {
                end = e.Location;
                BrushType.Invoke();
            }
        } // panel1_MouseUp

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = $"X = {e.X}, Y = {e.Y}";
            if (BrushType != PenMode) return;
            if (e.Button == MouseButtons.Left)
            {
                end = e.Location;
                BrushType.Invoke();
            } // if
        } // panel1_MouseMove

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;

            foreach (var figure in figures.figures)
                figure.Draw(gr);
        } // panel1_Paint
        private void PaintForm_ResizeEnd(object sender, EventArgs e)
        {
            graphics = panel1.CreateGraphics();
            figures.ChangeBorderSize(this.Width, this.Height);
        } // PaintForm_ResizeEnd
        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
                pen.Color = color.Color;
        } // colorToolStripMenuItem_Click
        private void SizeSubMenu_Click(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripMenuItem;
            if (currentItem != null)
            {
                ((ToolStripMenuItem)currentItem.OwnerItem).DropDownItems
                    .OfType<ToolStripMenuItem>().ToList()
                    .ForEach(item =>
                    {
                        item.Checked = false;
                    });

                currentItem.Checked = true;
                pen.Width = int.Parse(currentItem.Text);
            }
        } // SizeSubMenu_Click
        private void TypeSubMenu_Click(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripMenuItem;
            if (currentItem != null)
            {
                ((ToolStripMenuItem)currentItem.OwnerItem).DropDownItems
                    .OfType<ToolStripMenuItem>().ToList()
                    .ForEach(item =>
                    {
                        item.Checked = false;
                    });

                currentItem.Checked = true;
                switch (currentItem.Text)
                {
                    case "Pen":
                        BrushType = PenMode;
                        break;
                    case "Line":
                        BrushType = LineMode;
                        break;
                    case "Circle":
                        BrushType = CircleMode;
                        break;
                    case "Rectangle":
                        BrushType = RectangleMode;
                        break;
                    default:
                        break;
                }

            }
        } // SizeSubMenu_Click

        void PenMode()
        {
            graphics.DrawLine(pen, start, end);
            figures.Add(new MyPencil(start, end, pen.Color, pen.Width));
            start = end;
        } // PenMode
        void LineMode()
        {
            graphics.DrawLine(pen, start, end);
            figures.Add(new MyPencil(start, end, pen.Color, pen.Width));
        } // LineMode
        void RectangleMode()
        {
            if (start.X > end.X) // if start point > end point swap
            {
                int temp = start.X;
                start.X = end.X;
                end.X = temp;
            }

            if (start.Y > end.Y) // if start point > end point swap
            {
                int temp = start.Y;
                start.Y = end.Y;
                end.Y = temp;
            }
            int width = end.X - start.X;
            int height = end.Y - start.Y;
            Rectangle rect = new Rectangle(start.X, start.Y, width, height);
            graphics.DrawRectangle(pen, rect);
            figures.Add(new MyRectangle(start, width, height, pen.Color, pen.Width));
        } // RectangleMode

        void CircleMode()
        {
            if (start.X > end.X) // if start point > end point swap
            {
                int temp = start.X;
                start.X = end.X;
                end.X = temp;
            }

            if (start.Y > end.Y) // if start point > end point swap
            {
                int temp = start.Y;
                start.Y = end.Y;
                end.Y = temp;
            }

            int width = end.X - start.X;
            int height = end.Y - start.Y;
            Rectangle rect = new Rectangle(start.X, start.Y, width, height);
            graphics.DrawEllipse(pen, rect);
            figures.Add(new MyCircle(start, width, height, pen.Color, pen.Width));
        } // LineMode

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                figures.Save(saveFileDialog1.FileName);
        } //saveToolStripMenuItem_Click

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figures.Clear();
            graphics.Clear(Color.White);
        } // clearToolStripMenuItem_Click

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                LoadFigures(openFileDialog1.FileName);
        } // openToolStripMenuItem_Click

        void LoadFigures(string path)
        {
            figures.Load(path);

            this.Width = figures.borderWidth;
            this.Height = figures.borderHeight;
            graphics = panel1.CreateGraphics();

            foreach (var figure in figures.figures)
                figure.Draw(graphics);
        } // Load
    } // class PaintForm
}
