using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
        public Pen pen;
        public Graphics graphics;
        Painter painter;
        MyFigures figures;

        public PaintForm(Pen pen, Painter painter)
        {
            this.pen = pen;
            this.painter = painter;
            Start();
        }

        public PaintForm(string path, Pen pen, Painter painter)
        {
            this.pen = pen;
            this.painter = painter;
            Start();
            LoadFigures(path);
        }

        void Start()
        {
            InitializeComponent();
            figures = new MyFigures(this.Width, this.Width);
            //openFileDialog1.Filter = "Image Files (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png";
            //saveFileDialog1.Filter = "Images|*.bmp;*.jpg;*.png";
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            start = e.Location;
        } // panel1_MouseDown

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!painter.IsPenMode())
            {
                end = e.Location;
                graphics = CreateGraphics();
                painter.action.Invoke(graphics, pen, start, end, figures);
                graphics.Dispose();
            }
        } // panel1_MouseUp

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = $"X = {e.X}, Y = {e.Y}";
            if (!painter.IsPenMode()) return;
            if (e.Button == MouseButtons.Left)
            {
                end = e.Location;
                graphics = CreateGraphics();
                painter.action.Invoke(graphics, pen, start, end, figures);
                start = end;
                graphics.Dispose();
            } // if
        } // panel1_MouseMove

        private void PaintForm_ResizeEnd(object sender, EventArgs e)
        {
            figures.borderWidth = this.Width;
            figures.borderHeight = this.Height;
        } // PaintForm_ResizeEnd

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        } //saveToolStripMenuItem_Click

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figures.Clear();
            graphics = CreateGraphics();
            graphics.Clear(Color.White);
            graphics.Dispose();
        } // clearToolStripMenuItem_Click

        private void PaintForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            foreach (var figure in figures.figures)
                figure.Draw(gr);
        } // PaintForm_Paint

        void LoadFigures(string path)
        {
            figures.Load(path);
            this.Width = figures.borderWidth;
            this.Height = figures.borderHeight;
        } // LoadFigures

        public void Save()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                figures.Save(saveFileDialog1.FileName);
        } // Save
    } // class PaintForm
}
