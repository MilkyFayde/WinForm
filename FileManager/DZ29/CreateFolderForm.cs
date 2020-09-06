using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DZ29
{
    public partial class CreateFolderForm : Form
    {
        public string FileName { get; private set; }
        public bool IsFolder { get; private set; } = true;
        public CreateFolderForm()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length == 0)
            {
                MessageBox.Show("Folder name cant be empty");
                return;
            }

            FileName = textBox1.Text;
            this.Close();
        } // CreateButton_Click

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^a-zA-Z0-9\s]\.");
            if (regex.IsMatch(e.KeyChar.ToString()))
                e.Handled = true;
        } // textBox1_KeyPress

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateFileButton_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("File name cant be empty");
                return;
            }

            FileName = textBox2.Text;
            IsFolder = false;
            this.Close();
        }
    }
}
