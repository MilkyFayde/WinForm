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
    public partial class AddCategoryForm : Form
    {
        Category categories;
        Group group;
        public bool IsDataChanged { get; private set; } = false;

        public string category { get; private set; }
        public AddCategoryForm(Group group, Category categories)
        {
            InitializeComponent();
            this.group = group;
            this.categories = categories;

            ComboBoxesUpdate(0); // fill combo boxes with data souce
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

        void ComboBoxesUpdate(int index)
        {
            BindingSource binding = new BindingSource();
            binding.DataSource = categories.Categories;
            CategoryComboBox.DataSource = null; // clear datasource
            CategoryComboBox2.DataSource = null;
            CategoryComboBox.DataSource = binding.DataSource; // bind changed data source
            CategoryComboBox2.DataSource = binding.DataSource;
            CategoryComboBox.SelectedIndex = index;
            CategoryComboBox2.SelectedIndex = index;
        } // ComboBoxesUpdate

        private void CategoryTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        } // CategoryTextBox_KeyPress

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(CategoryTextBox.Text))
            {
                MessageBox.Show("Category field is empty");
                return;
            }
            if (categories.Exists(CategoryTextBox.Text))
            {
                MessageBox.Show("Entered Category name already exists");
                return;
            }
            category = CategoryTextBox.Text;
            categories.Add(category);
            IsDataChanged = true;
            this.Close();
        } // OkButton_Click

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        } // Cancelbutton_Click

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            int index = CategoryComboBox.SelectedIndex;
            if (index == 0)
            {
                MessageBox.Show("You cant remove \"None\" category");
                return;
            } // if

            group.RenameCategory(CategoryComboBox.Items[index].ToString(), "None");
            categories.Categories.Remove(CategoryComboBox.Items[index].ToString());

            ComboBoxesUpdate(0);
            CategoryComboBox.SelectedIndex = 0;
            IsDataChanged = true;
        } // RemoveButton_Click

        private void RenameBotton_Click(object sender, EventArgs e)
        {
            int index = CategoryComboBox2.SelectedIndex;

            if (index == 0)
            {
                MessageBox.Show("You cant rename \"None\" category");
                return;
            } // if
            if (String.IsNullOrEmpty(RenameTextBox.Text))
            {
                MessageBox.Show("Category field is empty");
                return;
            } // if
            if (categories.Exists(RenameTextBox.Text))
            {
                MessageBox.Show("Entered Category name already exists");
                return;
            } // if

            group.RenameCategory(CategoryComboBox2.Items[index].ToString(), RenameTextBox.Text);
            categories.RenameCategory(CategoryComboBox2.Items[index].ToString(), RenameTextBox.Text);
            RenameTextBox.Text = "";
            ComboBoxesUpdate(index);
            IsDataChanged = true;
        } // RenameBotton_Click
    } // class AddCategoryForm
}
