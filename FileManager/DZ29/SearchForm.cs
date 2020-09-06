using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DZ29
{
    public partial class SearchForm : Form
    {
        SearchList list; // list of found files
        public string SelectedName { get; private set; } // returning name of selected file
        public string[] SelectedPath { get; private set; }// returning splited path of selected file
        public SearchForm(string searchPath)
        {
            InitializeComponent();
            list = new SearchList(searchPath);
            searchView.View = View.Details;
            searchView.SmallImageList = new ImageList();
            searchView.LargeImageList = new ImageList();

            searchView.Columns.Add("Name", 150, HorizontalAlignment.Left);
            searchView.Columns.Add("Creation Date", 100, HorizontalAlignment.Left);
            searchView.Columns.Add("Size", 100, HorizontalAlignment.Left);
            searchView.Columns.Add("Path", 250, HorizontalAlignment.Left);
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0) return;
            list.Search(textBox1.Text);
            if (list.Lenght == 0) MessageBox.Show($"Any files with name \"{textBox1.Text}\" wasnt found");
            else
            {
                searchView.Items.Clear();
                searchView.LargeImageList.Images.Clear();
                searchView.SmallImageList.Images.Clear();
                searchView.LargeImageList.Images.Add(Bitmap.FromFile("CLSDFOLD.ICO"));
                searchView.SmallImageList.Images.Add(Bitmap.FromFile("CLSDFOLD.ICO"));

                foreach (var file in list)
                {
                    ListViewItem item = new ListViewItem(file.Name);
                    item.SubItems.Add(file.CreationDate.ToString());
                    item.SubItems.Add(file.Size);
                    item.SubItems.Add(file.Path);

                    if (file.IsFolder)
                    {
                        item.ImageIndex = 0;
                    }
                    else
                    {
                        Icon icon = Icon.ExtractAssociatedIcon(file.Path + "\\" + file.Name);
                        searchView.LargeImageList.Images.Add(icon);
                        searchView.SmallImageList.Images.Add(icon);
                        item.ImageIndex = searchView.LargeImageList.Images.Count - 1;
                    }
                    searchView.Items.Add(item);
                } // foreach

            } // else

        } // SearchButton_Click

        private void viewInMainFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (searchView.SelectedItems.Count == 0) return;
            int index = searchView.Items.IndexOf(searchView.SelectedItems[0]);

            SelectedPath = list[index].SplitPath();
            SelectedName = list[index].Name;
            this.Close();
        }
    }
}
