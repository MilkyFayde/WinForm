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
    public partial class AddPersonForm : Form
    {
        Group group;
        Category categories;
        public bool IsDataChanged { get; private set; } = false;
        public Person person { get; private set; } // created person
        string currentPhotoPath; // current selected photo
        string newPhotoPath; // path + name of copied photo

        public AddPersonForm(Group group, Category categories)
        {
            InitializeComponent();
            if (!Directory.Exists("Photos")) Directory.CreateDirectory("Photos");
            PhotoOpenFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            this.group = group;
            this.categories = categories;
            foreach (var category in categories.Categories)
                CategoryComboBox.Items.Add(category);
            CategoryComboBox.SelectedIndex = 0;
        }

        private void PhotoPictureBox_Click(object sender, EventArgs e)
        {
            if (PhotoOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PhotoPictureBox.Image = new Bitmap(PhotoOpenFileDialog.FileName);
                PhotoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                currentPhotoPath = PhotoOpenFileDialog.FileName;
            }
        } // PhotoPictureBox_Click

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
            if (group.PhoneExists(PhoneTextBox.Text))
            {
                MessageBox.Show("Entered Phone Number already exists");
                return;
            }

            CreatePerson(); // create new person
            this.Close();
        } // OkButton_Click

        void CreatePerson()
        {
            if (currentPhotoPath != null)
            {
                string extension = Path.GetExtension(currentPhotoPath);
                newPhotoPath = string.Format($"Photos\\{Guid.NewGuid()}{extension}"); // generate name for photo file
                File.Copy(currentPhotoPath, newPhotoPath); // copy photo with new name to Photo folder
            }

            person = new Person(FirstNameTextBox.Text, LastNameTextBox.Text, AddressTextBox.Text, PhoneTextBox.Text,CategoryComboBox.SelectedItem.ToString(), newPhotoPath);
            IsDataChanged = true;
        } // CreatePerson

        private void PhoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(PhoneTextBox.SelectionStart == 0)
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
    } // class AddPersonForm
}
