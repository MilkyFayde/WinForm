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
    public partial class MainPaintForm : Form
    {
        public MainPaintForm()
        {
            InitializeComponent();

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PaintForm form = new PaintForm();
            form.MdiParent = this;
            form.Show();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PaintForm form = new PaintForm(openFileDialog1.FileName);
                form.MdiParent = this;
                form.Show();
            }

        }
    }
}
