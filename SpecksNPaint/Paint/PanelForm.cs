using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class PanelForm : Form
    {
        Pen pen;
        Painter painter;
        Button disabledFormBotton;
        Button disabledFillBotton;

        public PanelForm(Pen pen, Painter painter)
        {
            InitializeComponent();
            disabledFormBotton = Penbutton;
            disabledFillBotton = Nonebutton1;
            this.pen = pen;
            this.painter = painter;
            openFileDialog1.Filter = "Image Files (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png";
        }

        private void PanelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void Penbutton_Click(object sender, EventArgs e)
        {
            disabledFormBotton.Enabled = true;
            disabledFormBotton = (Button)sender;
            disabledFormBotton.Enabled = false;
            painter.ChangeToPenMode();
        }

        private void Ellipsebutton_Click(object sender, EventArgs e)
        {
            disabledFormBotton.Enabled = true;
            disabledFormBotton = (Button)sender;
            disabledFormBotton.Enabled = false;
            painter.ChangeToCircleMode();
        }

        private void Rectbutton_Click(object sender, EventArgs e)
        {
            disabledFormBotton.Enabled = true;
            disabledFormBotton = (Button)sender;
            disabledFormBotton.Enabled = false;
            painter.ChangeToRectangleMode();
        }

        private void Linebutton_Click(object sender, EventArgs e)
        {
            disabledFormBotton.Enabled = true;
            disabledFormBotton = (Button)sender;
            disabledFormBotton.Enabled = false;
            painter.ChangeToLineMode();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }

        private void Colorbutton_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
                pen.Color = color.Color;
        }

        private void Nonebutton1_Click(object sender, EventArgs e)
        {
            disabledFillBotton.Enabled = true;
            disabledFillBotton = (Button)sender;
            disabledFillBotton.Enabled = false;
            painter.BrushOn = false;
        }

        private void SolidButton1_Click(object sender, EventArgs e)
        {
            disabledFillBotton.Enabled = true;
            disabledFillBotton = (Button)sender;
            disabledFillBotton.Enabled = false;
            painter.BrushOn = true;
            painter.ChangeBrushToSolid();
         }

        private void HatchButton1_Click(object sender, EventArgs e)
        {
            disabledFillBotton.Enabled = true;
            disabledFillBotton = (Button)sender;
            disabledFillBotton.Enabled = false;
            painter.BrushOn = true;
            painter.ChangeBrushToHatch();
        }

        private void GradientButton_Click(object sender, EventArgs e)
        {
            disabledFillBotton.Enabled = true;
            disabledFillBotton = (Button)sender;
            disabledFillBotton.Enabled = false;
            painter.BrushOn = true;
            painter.ChangeBrushToGradient();
        }

        private void TextureButton_Click(object sender, EventArgs e)
        {
            disabledFillBotton.Enabled = true;
            disabledFillBotton = (Button)sender;
            disabledFillBotton.Enabled = false;
            painter.BrushOn = true;
            painter.ChangeBrushToTexture();
        }

        private void BrushColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
                painter.brush.ChangeColor(color.Color);
        }

        private void TexturePicturebutton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                painter.ChangeImage(openFileDialog1.FileName);
        }

        private void Trianglebutton_Click(object sender, EventArgs e)
        {
            disabledFormBotton.Enabled = true;
            disabledFormBotton = (Button)sender;
            disabledFormBotton.Enabled = false;
            painter.ChangeToTriangleMode();
        }
    }
}
