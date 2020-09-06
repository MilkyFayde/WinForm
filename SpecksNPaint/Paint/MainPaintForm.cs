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
        Painter painter = new Painter();
        Pen pen = new Pen(Color.Black, 4);

        public MainPaintForm()
        {
            InitializeComponent();
            pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            PanelForm form = new PanelForm(pen, painter);
            form.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PaintForm form = new PaintForm(pen, painter);
            form.MdiParent = this;
            form.Show();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PaintForm form = new PaintForm(openFileDialog1.FileName, pen, painter);
                form.MdiParent = this;
                form.Show();
            }
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (PaintForm child in this.MdiChildren)
                child.Save();
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (PaintForm child in this.MdiChildren)
                child.Close();
        }
    }
}
