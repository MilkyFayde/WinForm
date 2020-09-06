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
    public partial class SearchForm : Form
    {
        Group group; // list of persons
        Category categories; // list of category names
        List<Person> persons; // list of found persons
        public bool IsDataChanged { get; private set; } = false;
        public SearchForm(Group group, Category categories)
        {
            InitializeComponent();
            this.group = group;
            this.categories = categories;
            persons = group.Persons;
            SearchListView.Columns.Add("Last Name", 150, HorizontalAlignment.Left);
            SearchListView.Columns.Add("First Name", 150, HorizontalAlignment.Left);
            SearchListView.Columns.Add("Address", 200, HorizontalAlignment.Left);
            SearchListView.Columns.Add("Phone Number", 150, HorizontalAlignment.Left);
            SearchListView.View = View.Details;

            ListViewUpdate(); // update view list
        } // SearchForm

        public void ListViewUpdate()
        {
            SearchListView.Groups.Clear();
            SearchListView.Items.Clear();

            foreach (var category in categories.Categories) // load categories to listView
                SearchListView.Groups.Add(category, category);
            foreach (var person in persons)
            {
                ListViewItem item = new ListViewItem(person.LName);
                item.SubItems.Add(person.FName);
                item.SubItems.Add(person.Address);
                item.SubItems.Add(person.PhoneNumber);
                item.Group = SearchListView.Groups[person.Category];
                SearchListView.Items.Add(item);
            } // foreach
        } // ListViewUpdate

        private void TextBoxesChanged(object sender, EventArgs e)
        {
            persons = group.FindPersons(LNameTextBox.Text, PhoneTextBox.Text);
            ListViewUpdate();
        } // TextBoxesChanged

        private void PhoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (PhoneTextBox.SelectionStart == 0)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '+'))
                    e.Handled = true;
            }
            else
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        } // PhoneTextBox_KeyPress

        private void editPersonToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SearchListView.SelectedItems.Count == 0) return;

            int index = SearchListView.Items.IndexOf(SearchListView.SelectedItems[0]);
            Person editingPerson = persons[index];
            EditPersonForm form = new EditPersonForm(editingPerson, group, categories);
            form.Owner = this;
            form.ShowDialog();

            if (form.IsDataChanged)
            {
                ListViewUpdate();
                IsDataChanged = form.IsDataChanged;
            }
        } // editPersonToolStripMenuItem1_Click

        private void deletePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SearchListView.SelectedItems.Count == 0) return;
            int index = SearchListView.Items.IndexOf(SearchListView.SelectedItems[0]);

            Person deletingPerson = persons[index];
            DialogResult dialog = MessageBox.Show($"Person \"{deletingPerson.LName} {deletingPerson.FName}\" will be deleted", "Deleting Person", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                group.Persons.Remove(deletingPerson);
                persons.Remove(deletingPerson);
                ListViewUpdate();
                IsDataChanged = true;
            } // if
        } // deletePersonToolStripMenuItem_Click
    } // class SearchForm
}
