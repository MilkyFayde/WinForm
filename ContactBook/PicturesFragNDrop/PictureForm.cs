using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturesFragNDrop
{
    public partial class PictureForm : Form
    {
        public PictureForm(string path)
        {
            InitializeComponent();
            this.Text = path;
            pictureBox1.Image = new Bitmap(path);
            this.Size = pictureBox1.Image.Size;
            this.Text = path;
        }
    }
}
