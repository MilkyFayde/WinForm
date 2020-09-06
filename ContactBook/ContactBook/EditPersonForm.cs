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

namespace ContactBook
{
    public partial class EditPersonForm : Form
    {
        Group group;
        Category categories;
        public bool IsDataChanged { get; private set; } = false;
        public Person editingPerson { get; private set; }
        string phoneNumberBeforeChange; // phone number of editing person for no duplicates
        string currentPhotoPath; // current selected photo
        string newPhotoPath; // path + name of copied photo

        public EditPersonForm(Person editingPerson, Group group, Category categories)
        {
            InitializeComponent();
            this.Text = $"Editing {editingPerson.LName} {editingPerson.FName}";
            this.editingPerson = editingPerson;
            this.group = group;
            this.categories = categories;
            phoneNumberBeforeChange = editingPerson.PhoneNumber;
            currentPhotoPath = editingPerson.PicPath;

            FillEditForm(); // fill all fields in edit form
            PhotoOpenFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

            foreach (var category in categories.Categories)
                CategoryComboBox.Items.Add(category);
            CategoryComboBox.SelectedItem = editingPerson.Category;
        }
        private void PhotoPictureBox_Click(object sender, EventArgs e)
        {
            if (PhotoOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PhotoPictureBox.Image = new Bitmap(PhotoOpenFileDialog.FileName);
                currentPhotoPath = PhotoOpenFileDialog.FileName;
            }
        }// PhotoPictureBox_Click

        void ChangePerson()
        {
            if (FirstNameTextBox.Text != editingPerson.FName)
            {
                editingPerson.FName = FirstNameTextBox.Text;
                IsDataChanged = true;
            }
            if (LastNameTextBox.Text != editingPerson.FName)
            {
                editingPerson.LName = LastNameTextBox.Text;
                IsDataChanged = true;
            }
            if (AddressTextBox.Text != editingPerson.Address)
            {
                editingPerson.Address = AddressTextBox.Text;
                IsDataChanged = true;
            }
            if (PhoneTextBox.Text != editingPerson.PhoneNumber)
            {
                editingPerson.PhoneNumber = PhoneTextBox.Text;
                IsDataChanged = true;
            }
            if (CategoryComboBox.SelectedItem.ToString() != editingPerson.Category)
            {
                editingPerson.Category = CategoryComboBox.SelectedItem.ToString();
                IsDataChanged = true;
            }
            if (currentPhotoPath != editingPerson.PicPath) // if photo changed
            {
                string extension = Path.GetExtension(currentPhotoPath);
                newPhotoPath = string.Format($"Photos\\{Guid.NewGuid()}{extension}"); // generate name for photo file
                File.Copy(currentPhotoPath, newPhotoPath); // copy photo with new name to Photo folder
                editingPerson.PicPath = newPhotoPath;
                IsDataChanged = true;
            } // if
        } // CreatePerson

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

        private void AddCategoryButton_Click(object sender, EventArgs e)
        {
            AddCategoryForm form = new AddCategoryForm(group, categories);
            form.Owner = this;
            form.ShowDialog();

            CategoryComboBox.Items.Clear();
            foreach (var category in categories.Categories)
                CategoryComboBox.Items.Add(category);

            CategoryComboBox.SelectedIndex = CategoryComboBox.Items.Count - 1;
            if(form.IsDataChanged) IsDataChanged = form.IsDataChanged;
        } // AddCategoryButton_Click
        void FillEditForm()
        {
            if (editingPerson.PicPath != null) PhotoPictureBox.Image = new Bitmap(editingPerson.PicPath);
            PhotoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            FirstNameTextBox.Text = editingPerson.FName;
            LastNameTextBox.Text = editingPerson.LName;
            AddressTextBox.Text = editingPerson.Address;
            PhoneTextBox.Text = editingPerson.PhoneNumber;
        } // FillEditForm

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(FirstNameTextBox.Text))
            {
                MessageBox.Show("First Name field is empty");
                return;
            }

            if (String.IsNullOrEmpty(LastNameTextBox.Text))
            {
                MessageBox.Show("Last Name field is empty");
                return;
            }

            if (String.IsNullOrEmpty(AddressTextBox.Text))
            {
                MessageBox.Show("Address field is empty");
                return;
            }

            if (String.IsNullOrEmpty(PhoneTextBox.Text))
            {
                MessageBox.Show("Phone Number field is empty");
                return;
            }
            if (group.PhoneExists(PhoneTextBox.Text) && phoneNumberBeforeChange != PhoneTextBox.Text)
            {
                MessageBox.Show("Entered Phone Number already exists");
                return;
            }
            ChangePerson(); // apply changed fields to editing person
            this.Close();
        } // OkButton_Click

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show($"Person \"{editingPerson.LName} {editingPerson.FName}\" will be deleted", "Deleting Person", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                group.Persons.Remove(editingPerson);
                IsDataChanged = true;
                this.Close();
            } // if
        } // DeleteButton_Click

    } // class AddPersonForm
}

