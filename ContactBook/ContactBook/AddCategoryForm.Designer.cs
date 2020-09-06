namespace ContactBook
{
    partial class AddCategoryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TabControl = new System.Windows.Forms.TabControl();
            this.AddtabPage = new System.Windows.Forms.TabPage();
            this.CategoryTextBox = new System.Windows.Forms.TextBox();
            this.Cancelbutton = new System.Windows.Forms.Button();
            this.CategoryLabel = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.RemoveTabPage = new System.Windows.Forms.TabPage();
            this.CategoryComboBox = new System.Windows.Forms.ComboBox();
            this.CancelButton2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.EdotTabPage = new System.Windows.Forms.TabPage();
            this.CancelButton3 = new System.Windows.Forms.Button();
            this.RenameBotton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.RenameTextBox = new System.Windows.Forms.TextBox();
            this.CategoryComboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TabControl.SuspendLayout();
            this.AddtabPage.SuspendLayout();
            this.RemoveTabPage.SuspendLayout();
            this.EdotTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.AddtabPage);
            this.TabControl.Controls.Add(this.RemoveTabPage);
            this.TabControl.Controls.Add(this.EdotTabPage);
            this.TabControl.Location = new System.Drawing.Point(2, 1);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(277, 181);
            this.TabControl.TabIndex = 13;
            // 
            // AddtabPage
            // 
            this.AddtabPage.Controls.Add(this.CategoryTextBox);
            this.AddtabPage.Controls.Add(this.Cancelbutton);
            this.AddtabPage.Controls.Add(this.CategoryLabel);
            this.AddtabPage.Controls.Add(this.OkButton);
            this.AddtabPage.Location = new System.Drawing.Point(4, 22);
            this.AddtabPage.Name = "AddtabPage";
            this.AddtabPage.Padding = new System.Windows.Forms.Padding(3);
            this.AddtabPage.Size = new System.Drawing.Size(269, 155);
            this.AddtabPage.TabIndex = 0;
            this.AddtabPage.Text = "Add";
            this.AddtabPage.UseVisualStyleBackColor = true;
            // 
            // CategoryTextBox
            // 
            this.CategoryTextBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CategoryTextBox.Location = new System.Drawing.Point(6, 26);
            this.CategoryTextBox.MaxLength = 10;
            this.CategoryTextBox.Name = "CategoryTextBox";
            this.CategoryTextBox.Size = new System.Drawing.Size(257, 26);
            this.CategoryTextBox.TabIndex = 18;
            this.CategoryTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CategoryTextBox_KeyPress);
            // 
            // Cancelbutton
            // 
            this.Cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancelbutton.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Cancelbutton.Location = new System.Drawing.Point(188, 115);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(75, 33);
            this.Cancelbutton.TabIndex = 20;
            this.Cancelbutton.Text = "Cancel";
            this.Cancelbutton.UseVisualStyleBackColor = true;
            this.Cancelbutton.Click += new System.EventHandler(this.Cancelbutton_Click);
            // 
            // CategoryLabel
            // 
            this.CategoryLabel.AutoSize = true;
            this.CategoryLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CategoryLabel.Location = new System.Drawing.Point(3, 4);
            this.CategoryLabel.Name = "CategoryLabel";
            this.CategoryLabel.Size = new System.Drawing.Size(65, 19);
            this.CategoryLabel.TabIndex = 17;
            this.CategoryLabel.Text = "Category";
            // 
            // OkButton
            // 
            this.OkButton.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OkButton.Location = new System.Drawing.Point(97, 115);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 33);
            this.OkButton.TabIndex = 19;
            this.OkButton.Text = "Add";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // RemoveTabPage
            // 
            this.RemoveTabPage.Controls.Add(this.CategoryComboBox);
            this.RemoveTabPage.Controls.Add(this.CancelButton2);
            this.RemoveTabPage.Controls.Add(this.label1);
            this.RemoveTabPage.Controls.Add(this.RemoveButton);
            this.RemoveTabPage.Location = new System.Drawing.Point(4, 22);
            this.RemoveTabPage.Name = "RemoveTabPage";
            this.RemoveTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.RemoveTabPage.Size = new System.Drawing.Size(269, 155);
            this.RemoveTabPage.TabIndex = 1;
            this.RemoveTabPage.Text = "Remove";
            this.RemoveTabPage.UseVisualStyleBackColor = true;
            // 
            // CategoryComboBox
            // 
            this.CategoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CategoryComboBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CategoryComboBox.FormattingEnabled = true;
            this.CategoryComboBox.Location = new System.Drawing.Point(6, 26);
            this.CategoryComboBox.Name = "CategoryComboBox";
            this.CategoryComboBox.Size = new System.Drawing.Size(257, 27);
            this.CategoryComboBox.TabIndex = 26;
            // 
            // CancelButton2
            // 
            this.CancelButton2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CancelButton2.Location = new System.Drawing.Point(188, 115);
            this.CancelButton2.Name = "CancelButton2";
            this.CancelButton2.Size = new System.Drawing.Size(75, 33);
            this.CancelButton2.TabIndex = 24;
            this.CancelButton2.Text = "Cancel";
            this.CancelButton2.UseVisualStyleBackColor = true;
            this.CancelButton2.Click += new System.EventHandler(this.Cancelbutton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 19);
            this.label1.TabIndex = 21;
            this.label1.Text = "Category";
            // 
            // RemoveButton
            // 
            this.RemoveButton.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RemoveButton.Location = new System.Drawing.Point(97, 115);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(75, 33);
            this.RemoveButton.TabIndex = 23;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // EdotTabPage
            // 
            this.EdotTabPage.Controls.Add(this.CancelButton3);
            this.EdotTabPage.Controls.Add(this.RenameBotton);
            this.EdotTabPage.Controls.Add(this.label3);
            this.EdotTabPage.Controls.Add(this.RenameTextBox);
            this.EdotTabPage.Controls.Add(this.CategoryComboBox2);
            this.EdotTabPage.Controls.Add(this.label2);
            this.EdotTabPage.Location = new System.Drawing.Point(4, 22);
            this.EdotTabPage.Name = "EdotTabPage";
            this.EdotTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.EdotTabPage.Size = new System.Drawing.Size(269, 155);
            this.EdotTabPage.TabIndex = 2;
            this.EdotTabPage.Text = "Edit";
            this.EdotTabPage.UseVisualStyleBackColor = true;
            // 
            // CancelButton3
            // 
            this.CancelButton3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CancelButton3.Location = new System.Drawing.Point(188, 115);
            this.CancelButton3.Name = "CancelButton3";
            this.CancelButton3.Size = new System.Drawing.Size(75, 33);
            this.CancelButton3.TabIndex = 32;
            this.CancelButton3.Text = "Cancel";
            this.CancelButton3.UseVisualStyleBackColor = true;
            this.CancelButton3.Click += new System.EventHandler(this.Cancelbutton_Click);
            // 
            // RenameBotton
            // 
            this.RenameBotton.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RenameBotton.Location = new System.Drawing.Point(97, 115);
            this.RenameBotton.Name = "RenameBotton";
            this.RenameBotton.Size = new System.Drawing.Size(75, 33);
            this.RenameBotton.TabIndex = 31;
            this.RenameBotton.Text = "Rename";
            this.RenameBotton.UseVisualStyleBackColor = true;
            this.RenameBotton.Click += new System.EventHandler(this.RenameBotton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 19);
            this.label3.TabIndex = 30;
            this.label3.Text = "Rename To";
            // 
            // RenameTextBox
            // 
            this.RenameTextBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RenameTextBox.Location = new System.Drawing.Point(7, 83);
            this.RenameTextBox.MaxLength = 10;
            this.RenameTextBox.Name = "RenameTextBox";
            this.RenameTextBox.Size = new System.Drawing.Size(257, 26);
            this.RenameTextBox.TabIndex = 29;
            this.RenameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CategoryTextBox_KeyPress);
            // 
            // CategoryComboBox2
            // 
            this.CategoryComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CategoryComboBox2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CategoryComboBox2.FormattingEnabled = true;
            this.CategoryComboBox2.Location = new System.Drawing.Point(6, 26);
            this.CategoryComboBox2.Name = "CategoryComboBox2";
            this.CategoryComboBox2.Size = new System.Drawing.Size(257, 27);
            this.CategoryComboBox2.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 19);
            this.label2.TabIndex = 27;
            this.label2.Text = "Category";
            // 
            // AddCategoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancelbutton;
            this.ClientSize = new System.Drawing.Size(279, 183);
            this.Controls.Add(this.TabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddCategoryForm";
            this.Text = "Add Category";
            this.TabControl.ResumeLayout(false);
            this.AddtabPage.ResumeLayout(false);
            this.AddtabPage.PerformLayout();
            this.RemoveTabPage.ResumeLayout(false);
            this.RemoveTabPage.PerformLayout();
            this.EdotTabPage.ResumeLayout(false);
            this.EdotTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage AddtabPage;
        private System.Windows.Forms.TabPage RemoveTabPage;
        private System.Windows.Forms.TextBox CategoryTextBox;
        private System.Windows.Forms.Button Cancelbutton;
        private System.Windows.Forms.Label CategoryLabel;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.ComboBox CategoryComboBox;
        private System.Windows.Forms.TabPage EdotTabPage;
        private System.Windows.Forms.ComboBox CategoryComboBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox RenameTextBox;
        private System.Windows.Forms.Button CancelButton3;
        private System.Windows.Forms.Button RenameBotton;
    }
}