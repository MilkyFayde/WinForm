namespace Paint
{
    partial class PanelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PanelForm));
            this.Penbutton = new System.Windows.Forms.Button();
            this.Ellipsebutton = new System.Windows.Forms.Button();
            this.Linebutton = new System.Windows.Forms.Button();
            this.Rectbutton = new System.Windows.Forms.Button();
            this.PenColorbutton = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.SolidButton1 = new System.Windows.Forms.Button();
            this.HatchButton1 = new System.Windows.Forms.Button();
            this.GradientButton = new System.Windows.Forms.Button();
            this.TextureButton = new System.Windows.Forms.Button();
            this.Nonebutton1 = new System.Windows.Forms.Button();
            this.BrushColorButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.TexturePicturebutton = new System.Windows.Forms.Button();
            this.Trianglebutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // Penbutton
            // 
            this.Penbutton.Enabled = false;
            this.Penbutton.Image = ((System.Drawing.Image)(resources.GetObject("Penbutton.Image")));
            this.Penbutton.Location = new System.Drawing.Point(4, 12);
            this.Penbutton.Name = "Penbutton";
            this.Penbutton.Size = new System.Drawing.Size(30, 30);
            this.Penbutton.TabIndex = 0;
            this.Penbutton.UseVisualStyleBackColor = true;
            this.Penbutton.Click += new System.EventHandler(this.Penbutton_Click);
            // 
            // Ellipsebutton
            // 
            this.Ellipsebutton.Image = ((System.Drawing.Image)(resources.GetObject("Ellipsebutton.Image")));
            this.Ellipsebutton.Location = new System.Drawing.Point(40, 12);
            this.Ellipsebutton.Name = "Ellipsebutton";
            this.Ellipsebutton.Size = new System.Drawing.Size(30, 30);
            this.Ellipsebutton.TabIndex = 1;
            this.Ellipsebutton.UseVisualStyleBackColor = true;
            this.Ellipsebutton.Click += new System.EventHandler(this.Ellipsebutton_Click);
            // 
            // Linebutton
            // 
            this.Linebutton.Image = ((System.Drawing.Image)(resources.GetObject("Linebutton.Image")));
            this.Linebutton.Location = new System.Drawing.Point(112, 12);
            this.Linebutton.Name = "Linebutton";
            this.Linebutton.Size = new System.Drawing.Size(30, 30);
            this.Linebutton.TabIndex = 2;
            this.Linebutton.UseVisualStyleBackColor = true;
            this.Linebutton.Click += new System.EventHandler(this.Linebutton_Click);
            // 
            // Rectbutton
            // 
            this.Rectbutton.Image = ((System.Drawing.Image)(resources.GetObject("Rectbutton.Image")));
            this.Rectbutton.Location = new System.Drawing.Point(76, 12);
            this.Rectbutton.Name = "Rectbutton";
            this.Rectbutton.Size = new System.Drawing.Size(30, 30);
            this.Rectbutton.TabIndex = 3;
            this.Rectbutton.UseVisualStyleBackColor = true;
            this.Rectbutton.Click += new System.EventHandler(this.Rectbutton_Click);
            // 
            // PenColorbutton
            // 
            this.PenColorbutton.BackColor = System.Drawing.Color.Plum;
            this.PenColorbutton.Location = new System.Drawing.Point(4, 99);
            this.PenColorbutton.Name = "PenColorbutton";
            this.PenColorbutton.Size = new System.Drawing.Size(75, 30);
            this.PenColorbutton.TabIndex = 4;
            this.PenColorbutton.Text = "Pen Color";
            this.PenColorbutton.UseVisualStyleBackColor = false;
            this.PenColorbutton.Click += new System.EventHandler(this.Colorbutton_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(4, 48);
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(174, 45);
            this.trackBar1.TabIndex = 5;
            this.trackBar1.Value = 4;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // SolidButton1
            // 
            this.SolidButton1.Location = new System.Drawing.Point(54, 139);
            this.SolidButton1.Name = "SolidButton1";
            this.SolidButton1.Size = new System.Drawing.Size(42, 30);
            this.SolidButton1.TabIndex = 6;
            this.SolidButton1.Text = "Solid";
            this.SolidButton1.UseVisualStyleBackColor = true;
            this.SolidButton1.Click += new System.EventHandler(this.SolidButton1_Click);
            // 
            // HatchButton1
            // 
            this.HatchButton1.Location = new System.Drawing.Point(102, 139);
            this.HatchButton1.Name = "HatchButton1";
            this.HatchButton1.Size = new System.Drawing.Size(47, 30);
            this.HatchButton1.TabIndex = 7;
            this.HatchButton1.Text = "Hatch";
            this.HatchButton1.UseVisualStyleBackColor = true;
            this.HatchButton1.Click += new System.EventHandler(this.HatchButton1_Click);
            // 
            // GradientButton
            // 
            this.GradientButton.Location = new System.Drawing.Point(6, 175);
            this.GradientButton.Name = "GradientButton";
            this.GradientButton.Size = new System.Drawing.Size(42, 30);
            this.GradientButton.TabIndex = 8;
            this.GradientButton.Text = "Grad";
            this.GradientButton.UseVisualStyleBackColor = true;
            this.GradientButton.Click += new System.EventHandler(this.GradientButton_Click);
            // 
            // TextureButton
            // 
            this.TextureButton.Location = new System.Drawing.Point(54, 175);
            this.TextureButton.Name = "TextureButton";
            this.TextureButton.Size = new System.Drawing.Size(42, 30);
            this.TextureButton.TabIndex = 9;
            this.TextureButton.Text = "Textu";
            this.TextureButton.UseVisualStyleBackColor = true;
            this.TextureButton.Click += new System.EventHandler(this.TextureButton_Click);
            // 
            // Nonebutton1
            // 
            this.Nonebutton1.Enabled = false;
            this.Nonebutton1.Location = new System.Drawing.Point(6, 139);
            this.Nonebutton1.Name = "Nonebutton1";
            this.Nonebutton1.Size = new System.Drawing.Size(42, 30);
            this.Nonebutton1.TabIndex = 10;
            this.Nonebutton1.Text = "None";
            this.Nonebutton1.UseVisualStyleBackColor = true;
            this.Nonebutton1.Click += new System.EventHandler(this.Nonebutton1_Click);
            // 
            // BrushColorButton
            // 
            this.BrushColorButton.BackColor = System.Drawing.Color.Plum;
            this.BrushColorButton.Location = new System.Drawing.Point(6, 211);
            this.BrushColorButton.Name = "BrushColorButton";
            this.BrushColorButton.Size = new System.Drawing.Size(75, 30);
            this.BrushColorButton.TabIndex = 11;
            this.BrushColorButton.Text = "Brush Color";
            this.BrushColorButton.UseVisualStyleBackColor = false;
            this.BrushColorButton.Click += new System.EventHandler(this.BrushColorButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // TexturePicturebutton
            // 
            this.TexturePicturebutton.BackColor = System.Drawing.Color.LightSteelBlue;
            this.TexturePicturebutton.Location = new System.Drawing.Point(87, 211);
            this.TexturePicturebutton.Name = "TexturePicturebutton";
            this.TexturePicturebutton.Size = new System.Drawing.Size(75, 30);
            this.TexturePicturebutton.TabIndex = 12;
            this.TexturePicturebutton.Text = "Texture Pic";
            this.TexturePicturebutton.UseVisualStyleBackColor = false;
            this.TexturePicturebutton.Click += new System.EventHandler(this.TexturePicturebutton_Click);
            // 
            // Trianglebutton
            // 
            this.Trianglebutton.Image = ((System.Drawing.Image)(resources.GetObject("Trianglebutton.Image")));
            this.Trianglebutton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Trianglebutton.Location = new System.Drawing.Point(148, 12);
            this.Trianglebutton.Name = "Trianglebutton";
            this.Trianglebutton.Size = new System.Drawing.Size(30, 30);
            this.Trianglebutton.TabIndex = 13;
            this.Trianglebutton.UseVisualStyleBackColor = true;
            this.Trianglebutton.Click += new System.EventHandler(this.Trianglebutton_Click);
            // 
            // PanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 253);
            this.Controls.Add(this.Trianglebutton);
            this.Controls.Add(this.TexturePicturebutton);
            this.Controls.Add(this.BrushColorButton);
            this.Controls.Add(this.Nonebutton1);
            this.Controls.Add(this.TextureButton);
            this.Controls.Add(this.GradientButton);
            this.Controls.Add(this.HatchButton1);
            this.Controls.Add(this.SolidButton1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.PenColorbutton);
            this.Controls.Add(this.Rectbutton);
            this.Controls.Add(this.Linebutton);
            this.Controls.Add(this.Ellipsebutton);
            this.Controls.Add(this.Penbutton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PanelForm";
            this.Text = "Tools";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PanelForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Penbutton;
        private System.Windows.Forms.Button Ellipsebutton;
        private System.Windows.Forms.Button Linebutton;
        private System.Windows.Forms.Button Rectbutton;
        private System.Windows.Forms.Button PenColorbutton;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button SolidButton1;
        private System.Windows.Forms.Button HatchButton1;
        private System.Windows.Forms.Button GradientButton;
        private System.Windows.Forms.Button TextureButton;
        private System.Windows.Forms.Button Nonebutton1;
        private System.Windows.Forms.Button BrushColorButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button TexturePicturebutton;
        private System.Windows.Forms.Button Trianglebutton;
    }
}