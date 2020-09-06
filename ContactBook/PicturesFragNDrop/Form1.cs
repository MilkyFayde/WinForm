using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturesFragNDrop
{
    public partial class Form1 : Form
    {
        List<string> picPaths = new List<string>();
        int index;
        Form currentChildForm;
        public Form1()
        {
            InitializeComponent();
            PictureFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
        }

        void OpenPictureInNewForm(string path)
        {
            PictureForm form = new PictureForm(path);
            form.MdiParent = this;
            form.Show();
        } // OpenPictureInNewForm

        private void openPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PictureFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                OpenPictureInNewForm(PictureFileDialog.FileName);
        } // openPictureToolStripMenuItem_Click

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (picPaths.Count > 0)
                foreach (var path in picPaths)
                    OpenPictureInNewForm(path);

            picPaths.Clear();
        } // Form1_DragDrop

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            ///////// e.Data.GetDataPresent(DataFormats.Bitmap) - Is not working \\\\\\\\\\\\\\\
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
            {
                var path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                if (Directory.Exists(path))
                    ScanForPictures(path); // scan folder for pictures and add path to the list
                else
                    IsPicture(path); // if file is picture Add to list
                e.Effect = DragDropEffects.Copy;
            }
        } // Form1_DragEnter

        private void timer1_Tick(object sender, EventArgs e)
        {
            currentChildForm.Hide();
            currentChildForm = this.MdiChildren[index++];
            currentChildForm.Show();
            if (index >= this.MdiChildren.Length) index = 0;
        } // timer1_Tick

        private void slideShowStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                foreach (var child in this.MdiChildren)
                    child.Hide();
                timer1.Start();
                index = 0;
                currentChildForm = this.MdiChildren[index];
                this.AllowDrop = false;
            }
        } // slideShowStartToolStripMenuItem_Click

        private void slideShowStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                foreach (var child in this.MdiChildren)
                    child.Show();
                this.AllowDrop = true;
            }
        } // slideShowStopToolStripMenuItem_Click

        void IsPicture(string path)
        {
            string ext = Path.GetExtension(path).ToLower();
            if ((ext == ".jpg") || (ext == ".png") || (ext == ".bmp") || (ext == ".gif"))
                picPaths.Add(path);
        } // GetFilename

        void ScanForPictures(string path)
        {
            DirectoryInfo dinfo = new DirectoryInfo(path);
            FileInfo[] files = dinfo.GetFiles();
            foreach (FileInfo current in files)
                IsPicture(current.FullName); // if file is picture Add to list

            DirectoryInfo[] dirs = dinfo.GetDirectories();
            foreach (DirectoryInfo current in dirs)
                ScanForPictures(current.FullName);
        } // ScanForPictures
    } // class Form1
}
