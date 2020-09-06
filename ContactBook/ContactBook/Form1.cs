using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactBook
{
    public partial class Form1 : Form
    {
        Group group = new Group(); // list of persons
        Category categories = new Category(); // list of category names
        bool IsDataChanged = false; // flag if any data was changed
        public Form1()
        {
            InitializeComponent();

            MainListWindow.Columns.Add("Last Name", 150, HorizontalAlignment.Left);
            MainListWindow.Columns.Add("First Name", 150, HorizontalAlignment.Left);
            MainListWindow.Columns.Add("Address", 230, HorizontalAlignment.Left);
            MainListWindow.Columns.Add("Phone Number", 150, HorizontalAlignment.Left);
            MainListWindow.View = View.Details;
            Load(); // load saved categories and persons from xml
            group.DeleteUnusedPhotos(); // delete from Photo folder all unused photos
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        } // ProcessDialogKey

        private void addPersonToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddPerson();
        } // addPersonToolStripMenuItem1_Click

        void AddPerson()
        {
            AddPersonForm form = new AddPersonForm(group, categories);
            form.Owner = this;
            form.ShowDialog();

            if (form.IsDataChanged)
            {
                if (form.person != null) group.Add(form.person);
                ListViewUpdate(); //
                IsDataChanged = form.IsDataChanged;
            }
        } // AddPerson

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)=>Save();

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)=>Load();

        void Save()
        {
            categories.Save();
            group.Save();
            IsDataChanged = false;
        } // Save
        new void Load()
        {
            categories.Load();
            group.Load();
            ListViewUpdate();
            IsDataChanged = false;
        } // Load

        public void ListViewUpdate()
        {
            MainListWindow.Groups.Clear();
            MainListWindow.Items.Clear();

            foreach (var category in categories.Categories) // load categories to listView
                MainListWindow.Groups.Add(category, category);

            foreach (var person in group.Persons)
            {
                ListViewItem item = new ListViewItem(person.LName);

                item.SubItems.Add(person.FName);
                item.SubItems.Add(person.Address);
                item.SubItems.Add(person.PhoneNumber);
                item.Group = MainListWindow.Groups[person.Category];
                MainListWindow.Items.Add(item);
            } // foreach
        } // ListViewUpdate

        private void editPersonToolStripMenuItem_Click(object sender, EventArgs e)=>EditPerson();

        void EditPerson()
        {
            if (MainListWindow.SelectedItems.Count == 0) return;

            int index = MainListWindow.Items.IndexOf(MainListWindow.SelectedItems[0]);
            Person editingPerson = group.Persons[index];
            EditPersonForm form = new EditPersonForm(editingPerson, group, categories);
            form.Owner = this;
            form.ShowDialog();

            if (form.IsDataChanged)
            {
                ListViewUpdate();
                IsDataChanged = form.IsDataChanged;
            }
        } // EditPerson

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsDataChanged)
            {
                DialogResult dialog = MessageBox.Show("Data changed, do you want to save before exiting?", "Closing Contact Book", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                    Save();
            } // if
        } // Form1_FormClosing

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e) => MainListWindow.View = View.Details;
        private void smallIconToolStripMenuItem_Click(object sender, EventArgs e) => MainListWindow.View = View.SmallIcon;
        private void largeIconToolStripMenuItem_Click(object sender, EventArgs e) => MainListWindow.View = View.LargeIcon;
        private void listToolStripMenuItem_Click(object sender, EventArgs e) => MainListWindow.View = View.List;
        private void tileToolStripMenuItem_Click(object sender, EventArgs e) => MainListWindow.View = View.Tile;

        private void deletePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainListWindow.SelectedItems.Count == 0) return;
            int index = MainListWindow.Items.IndexOf(MainListWindow.SelectedItems[0]);
            Person deletingPerson = group.Persons[index];

            DialogResult dialog = MessageBox.Show($"Person \"{deletingPerson.LName} {deletingPerson.FName}\" will be deleted", "Deleting Person", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.No)
                return;

            group.Persons.Remove(deletingPerson);
            MainListWindow.Items[index].Remove();
            ListViewUpdate();
            IsDataChanged = true;
        } // deletePersonToolStripMenuItem_Click

        private void addEditCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCategoryForm form = new AddCategoryForm(group, categories);
            form.Owner = this;
            form.ShowDialog();

            if (form.IsDataChanged)
            {
                ListViewUpdate();
                IsDataChanged = form.IsDataChanged;
            }
        } // addEditCategoryToolStripMenuItem_Click

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchForm form = new SearchForm(group, categories);
            form.Owner = this;
            form.ShowDialog();
            if (form.IsDataChanged)
            {
                ListViewUpdate();
                IsDataChanged = form.IsDataChanged;
            } // if
        } // searchToolStripMenuItem_Click
    } // class Form1
}
