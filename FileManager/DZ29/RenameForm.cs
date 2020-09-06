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
    public partial class RenameForm : Form
    {
        public string FileName { get; private set; }
        public RenameForm(string renamingFile)
        {
            InitializeComponent();
            textBox1.Text = renamingFile;
            textBox1.SelectAll();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^a-zA-Z0-9\s]\.");
            if (regex.IsMatch(e.KeyChar.ToString()))
                e.Handled = true;
        } // textBox1_KeyPress

        private void RenameButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Name cant be empty");
                return;
            }

            FileName = textBox1.Text;
            this.Close();
        } // RenameButton_Click
    }
}
