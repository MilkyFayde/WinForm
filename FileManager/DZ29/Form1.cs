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

namespace DZ29
{
    public partial class MainForm : Form
    {
        private ImageList imglist = new ImageList();
        string currentPath; // path of current open directory
        string currentFile; // path of current open directory + selected file name
        string copyFrom; // path of copying file
        TreeNode cuttingNode;
        bool IsCut = false; // if cutting file

        public MainForm()
        {
            InitializeComponent();
            InfoTextBox.Hide();
            fileView.AllowDrop = true;

            fileView.View = View.Details;
            fileView.SmallImageList = new ImageList();
            fileView.LargeImageList = new ImageList();

            fileView.Columns.Add("Name", 150, HorizontalAlignment.Left);
            fileView.Columns.Add("Creation Date", 100, HorizontalAlignment.Center);
            fileView.Columns.Add("Size", 120, HorizontalAlignment.Center);

            imglist.Images.Add(Bitmap.FromFile("CLSDFOLD.ICO"));
            imglist.Images.Add(Bitmap.FromFile("OPENFOLD.ICO"));
            imglist.Images.Add(Bitmap.FromFile("Drive01.ico"));
            treeView1.ImageList = imglist;

            string[] drives = Directory.GetLogicalDrives();
            foreach (string drive in drives)
            {
                TreeNode node = new TreeNode(drive, 2, 2);
                treeView1.Nodes.Add(node);
                FillByDirectories(node);
            } // foreach
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            else if (Form.ModifierKeys == Keys.None && keyData == Keys.Delete)
            {
                Delete();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        } // ProcessDialogKey

        private void FillByDirectories(TreeNode node)
        {
            try
            {
                DirectoryInfo dirinfo = new DirectoryInfo(node.FullPath);
                DirectoryInfo[] dirs = dirinfo.GetDirectories();

                foreach (DirectoryInfo dir in dirs)
                {
                    TreeNode tree = new TreeNode(dir.Name, 0, 1);
                    node.Nodes.Add(tree);
                }
            }
            catch { }
        } // FillByDirectories

        private void FillByFiles()
        {
            fileView.BeginUpdate();
            fileView.Items.Clear();
            DirectoryInfo dirinfo = new DirectoryInfo(currentPath);
            toolStripStatusLabel1.Text = currentPath;

            FileInfo[] files = dirinfo.GetFiles();

            fileView.LargeImageList.Images.Clear();
            fileView.SmallImageList.Images.Clear();

            fileView.LargeImageList.Images.Add(imglist.Images[0]);
            fileView.SmallImageList.Images.Add(imglist.Images[0]);
            int iconindex = 1;
            fileView.LargeImageList.Images.Add(Bitmap.FromFile("note11.ico"));
            fileView.SmallImageList.Images.Add(Bitmap.FromFile("note11.ico"));

            DirectoryInfo[] dirs = dirinfo.GetDirectories();
            foreach (var dir in dirs)
            {
                ListViewItem item = new ListViewItem(dir.Name);

                item.SubItems.Add(dir.CreationTime.ToString());
                item.SubItems.Add("");
                item.ImageIndex = 0;

                fileView.Items.Add(item);
            } // foreach

            foreach (FileInfo file in files)
            {
                ListViewItem item = new ListViewItem(file.Name);
                Icon icon = Icon.ExtractAssociatedIcon(file.FullName);

                fileView.LargeImageList.Images.Add(icon);
                fileView.SmallImageList.Images.Add(icon);
                iconindex++;

                item.ImageIndex = iconindex;

                item.SubItems.Add(file.CreationTime.ToString());
                item.SubItems.Add(file.Length.ToString());
                fileView.Items.Add(item);
            } // foreach

            fileView.EndUpdate();
        } // FillByFiles

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            treeView1.BeginUpdate();
            try
            {
                foreach (TreeNode node in e.Node.Nodes)
                    FillByDirectories(node);
            }
            catch { }
            treeView1.EndUpdate();
        } // treeView1_BeforeExpand

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                currentPath = e.Node.FullPath;
                FillByFiles();
            }
            catch { }
        } // treeView1_AfterSelect

        private void fileView_MouseClick(object sender, MouseEventArgs e)
        {
            if (fileView.SelectedItems.Count == 0) return;
            currentFile = currentPath + "\\" + fileView.SelectedItems[0].Text;
            string ext = Path.GetExtension(currentFile).ToLower();

            if (Directory.Exists(currentFile) || new FileInfo(currentFile).Length == 0) return;

            if ((ext == ".jpg") || (ext == ".png") || (ext == ".bmp") || (ext == ".gif"))
            {
                InfoPictureBox.Image = new Bitmap(currentFile);
                InfoPictureBox.Show();
                InfoTextBox.Hide();
            }
            else if (ext == ".txt")
            {
                InfoTextBox.Text = File.ReadAllLines(currentFile).First();
                InfoTextBox.Show();
                InfoPictureBox.Hide();
            }
            else
            {
                InfoPictureBox.Hide();
                InfoTextBox.Hide();
            }
        } // fileView_MouseClick

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateFolderForm form = new CreateFolderForm();
            form.Owner = this;
            form.ShowDialog();

            if (form.FileName == null) return;
            string fileName = currentPath + "\\" + form.FileName;

            if (form.IsFolder)
            {
                if (Directory.Exists(fileName))
                {
                    MessageBox.Show($"Folder with name \"{form.FileName}\" already exists");
                    return;
                }
                Directory.CreateDirectory(fileName);
                treeView1.SelectedNode.Nodes.Add(form.FileName);
            } // if
            else
            {
                if (File.Exists(fileName))
                {
                    MessageBox.Show($"File with name \"{form.FileName}\" already exists");
                    return;
                }
                // File.Create(fileName);
                using (FileStream fs = File.Create(fileName)) { }
            } // else

            FillByFiles();
        } // createFolderToolStripMenuItem_Click

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        } // deleteToolStripMenuItem_Click

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileView.SelectedItems.Count == 0) return;

            RenameForm form = new RenameForm(fileView.SelectedItems[0].Text);
            form.Owner = this;
            form.ShowDialog();

            if (form.FileName == null) return;
            string newFileName = currentPath + "\\" + form.FileName;
            currentFile = currentPath + "\\" + fileView.SelectedItems[0].Text;

            if (Directory.Exists(currentFile))
            {
                if (Directory.Exists(newFileName))
                {
                    MessageBox.Show($"Folder with name \"{form.FileName}\" already exists");
                    return;
                }
                Directory.Move(currentFile, newFileName);

                GetFolderNode(fileView.SelectedItems[0].Text).Text = form.FileName;  // get TreeNode by name in selected Node
            } // if folder
            else
            {
                if (File.Exists(newFileName))
                {
                    MessageBox.Show($"File with name \"{form.FileName}\" already exists");
                    return;
                }
                File.Move(currentFile, newFileName);
            } // else if file

            FillByFiles();
        } // renameToolStripMenuItem_Click

        TreeNode GetFolderNode(string name) => treeView1.SelectedNode.Nodes
                    .Cast<TreeNode>()
                    .Where(r => r.Text == name)
                    .FirstOrDefault(); // get TreeNode by name in selected Node
        void Delete()
        {
            if (fileView.SelectedItems.Count == 0) return;
            currentFile = currentPath + "\\" + fileView.SelectedItems[0].Text;

            DialogResult dialog = MessageBox.Show($"You want to delete folder \"{currentFile}\"?", "Deleting", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.No)
                return;

            if (Directory.Exists(currentFile))
            {
                Directory.Delete(currentFile, true);
                treeView1.SelectedNode.Nodes.Remove(GetFolderNode(fileView.SelectedItems[0].Text));  // get TreeNode by name in selected Node
            }
            else
                File.Delete(currentFile);

            FillByFiles();
        } // Delete

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileView.SelectedItems.Count == 0) return;
            copyFrom = currentPath + "\\" + fileView.SelectedItems[0].Text;
            pasteToolStripMenuItem.Enabled = true;
            IsCut = false;
        } // copyToolStripMenuItem_Click

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileView.SelectedItems.Count == 0) return;
            copyFrom = currentPath + "\\" + fileView.SelectedItems[0].Text;
            cuttingNode = GetFolderNode(fileView.SelectedItems[0].Text);  // get TreeNode by name in selected Node
            pasteToolStripMenuItem.Enabled = true;
            IsCut = true;
        } // cutToolStripMenuItem_Click

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsCut) // if selected file for cutting
            {
                if (Directory.Exists(copyFrom))
                {
                    MoveFolder(); // move folder with files to new location
                    FillByDirectories(treeView1.SelectedNode.LastNode);
                    treeView1.Nodes.Remove(cuttingNode);
                }
                else MoveFile(); // move file to new location
                pasteToolStripMenuItem.Enabled = false;
                IsCut = false;
            } // if cutting file
            else
            {
                if (Directory.Exists(copyFrom))
                {
                    CopyFolder(); // copy folder to new location
                    treeView1.SelectedNode.LastNode.Nodes.Clear();
                    FillByDirectories(treeView1.SelectedNode.LastNode);
                }
                else CopyFile(); // // copy file to new location
            }
            FillByFiles();
        } // pasteToolStripMenuItem_Click

        void CopyFolder()
        {
            int pos = copyFrom.LastIndexOf("\\") + 1;
            string oldFileName = copyFrom.Substring(pos, copyFrom.Length - pos);
            string copyTo = currentPath + "\\" + oldFileName;

            if (!Directory.Exists(copyTo)) // if folder with current name not exist
            {
                CopyEntireFolder(copyTo); // copy all dirs and files
                pos = copyTo.LastIndexOf("\\") + 1;
                treeView1.SelectedNode.Nodes.Add(copyTo.Substring(pos, copyTo.Length - pos));
                return;
            }
            copyTo = currentPath + "\\Copy - " + oldFileName;

            int i = 0;
            while (Directory.Exists(copyTo)) copyTo = currentPath + $"\\Copy({++i}) - " + oldFileName;  // while folder with current name not exist

            CopyEntireFolder(copyTo); // copy all dirs and files
            pos = copyTo.LastIndexOf("\\") + 1;

            treeView1.SelectedNode.Nodes.Add(copyTo.Substring(pos, copyTo.Length - pos));
        } // CopyFolder

        void CopyFile()
        {
            int pos = copyFrom.LastIndexOf("\\") + 1;
            string oldFileName = copyFrom.Substring(pos, copyFrom.Length - pos);
            string copyTo = currentPath + "\\" + oldFileName;

            if (!File.Exists(copyTo))  // if file with current name not exist
            {
                File.Copy(copyFrom, copyTo);
                return;
            }
            copyTo = currentPath + "\\Copy - " + oldFileName;

            int i = 0;
            while (File.Exists(copyTo)) copyTo = currentPath + $"\\Copy({++i}) - " + oldFileName;  // while file with current name not exist
            File.Copy(copyFrom, copyTo);
        } // CoplyFile

        void MoveFolder()
        {
            int pos = copyFrom.LastIndexOf("\\") + 1;
            if (currentPath == copyFrom.Substring(0, pos)) return; // if same folder

            string oldFileName = copyFrom.Substring(pos, copyFrom.Length - pos);
            string moveTo = currentPath + "\\" + oldFileName;

            if (!Directory.Exists(moveTo))  // if folder with current name not exist
            {
                Directory.Move(copyFrom, moveTo);
                pos = moveTo.LastIndexOf("\\") + 1;
                treeView1.SelectedNode.Nodes.Add(moveTo.Substring(pos, moveTo.Length - pos));
                return;
            }
            moveTo = currentPath + "\\Copy - " + oldFileName;

            int i = 0;
            while (Directory.Exists(moveTo)) moveTo = currentPath + $"\\Copy({++i}) - " + oldFileName;  // while file with current name not exist

            CopyEntireFolder(moveTo); // copy all dirs and files
            pos = moveTo.LastIndexOf("\\") + 1;

            treeView1.SelectedNode.Nodes.Add(moveTo.Substring(pos, moveTo.Length - pos));
        } // CopyFolder

        void MoveFile()
        {
            int pos = copyFrom.LastIndexOf("\\") + 1;
            if (currentPath == copyFrom.Substring(0, pos)) return; // if old and new paths same

            string oldFileName = copyFrom.Substring(pos, copyFrom.Length - pos);
            string moveTo = currentPath + "\\" + oldFileName;

            if (!File.Exists(moveTo))  // if file with current name not exist
            {
                File.Move(copyFrom, moveTo);
                return;
            }
            moveTo = currentPath + "\\Copy - " + oldFileName;

            int i = 0;
            while (File.Exists(moveTo)) moveTo = currentPath + $"\\Copy({++i}) - " + oldFileName;  // while file with current name not exist
            File.Move(copyFrom, moveTo);
        } // CoplyFile

        void CopyEntireFolder(string copyTo)
        {
            foreach (string dirPath in Directory.GetDirectories(copyFrom, "*", // creating folders
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(copyFrom, copyTo));

            foreach (string newPath in Directory.GetFiles(copyFrom, "*.*", // copying files
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(copyFrom, copyTo), true);
        } // CopyEntireFolder  copy all dirs and files

        private void fileView_DragEnter(object sender, DragEventArgs e)
        {
            copyFrom = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            pasteToolStripMenuItem.Enabled = true;
            IsCut = false;
            e.Effect = DragDropEffects.Copy;
        } // fileView_DragEnter

        private void fileView_DragDrop(object sender, DragEventArgs e)
        {
            if (Directory.Exists(copyFrom))
            {
                CopyFolder();
                treeView1.SelectedNode.LastNode.Nodes.Clear();
                FillByDirectories(treeView1.SelectedNode.LastNode);
            }
            else CopyFile();
            FillByFiles();
        } // fileView_DragDrop

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchForm form = new SearchForm(currentPath);
            form.Owner = this;
            form.ShowDialog();
            if (form.SelectedName == null) return; // if searching name in form is empy

            int index = currentPath.Split('\\').Count() - 1;

            for (int i = index; i < form.SelectedPath.Count(); i++)
            {
                TreeNode node = GetFolderNode(form.SelectedPath[i]);  // get TreeNode by name in selected Node
                treeView1.SelectedNode = node;
                node.Expand();
            }
        } // searchToolStripMenuItem_Click
    } // class MainForm
}
