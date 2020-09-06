using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Specks
{
    public partial class SpecksForm : Form
    {
        int size = 4;
        Bitmap[,] imagePieces;
        Button[,] buttons;
        DateTime starTime;

        public SpecksForm()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Image Files (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png";
            imagePieces = new Bitmap[size, size];
            buttons = new Button[size, size];
        } // SpecksForm

        private void SpecksForm_Load(object sender, EventArgs e)
        {
            AddButtonsToArray(); // adding buttons from form to array
            SplitImage(@"D:\111.jpg"); // split image to pieces
            AddImagesToButtons(); // add image pieces to buttons
        } // SpecksForm_Load

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SplitImage(openFileDialog1.FileName);  // split image to pieces
                AddImagesToButtons(); // add image pieces to buttons
            }

            foreach (var button in buttons)
                button.Enabled = false;
        } // openImageToolStripMenuItem_Click

        void AddButtonsToArray()
        {
            buttons[0, 0] = button1;
            buttons[0, 1] = button2;
            buttons[0, 2] = button3;
            buttons[0, 3] = button4;

            buttons[1, 0] = button5;
            buttons[1, 1] = button6;
            buttons[1, 2] = button7;
            buttons[1, 3] = button8;

            buttons[2, 0] = button9;
            buttons[2, 1] = button10;
            buttons[2, 2] = button11;
            buttons[2, 3] = button12;

            buttons[3, 0] = button13;
            buttons[3, 1] = button14;
            buttons[3, 2] = button15;
            buttons[3, 3] = button16;
        } // AddButtonsToList

        void SplitImage(string imgPath)
        {
            Image img = Image.FromFile(imgPath);
            int pieceWidth = (int)((double)img.Width / (double)imagePieces.GetLength(0));
            int pieceHeight = (int)((double)img.Height / (double)imagePieces.GetLength(1));

            Rectangle sourceRect = new Rectangle(0, 0, pieceWidth, pieceHeight);
            Rectangle destRect = new Rectangle(0, 0, pieceWidth, pieceHeight);
            Graphics gr;

            for (int i = 0; i < imagePieces.GetLength(0); i++)
            {
                sourceRect.X = 0;
                for (int j = 0; j < imagePieces.GetLength(1); j++)
                {
                    imagePieces[i, j] = new Bitmap(pieceWidth, pieceHeight);
                    gr = Graphics.FromImage(imagePieces[i, j]);
                    gr.DrawImage(img, destRect, sourceRect, GraphicsUnit.Pixel);
                    gr.Dispose();
                    sourceRect.X += pieceWidth;
                } // for j
                sourceRect.Y += pieceHeight;
            } // for i
        } // SplitImage

        void AddImagesToButtons()
        {
            for (int i = 0; i < imagePieces.GetLength(0); i++)
                for (int j = 0; j < imagePieces.GetLength(1); j++)
                    buttons[i, j].BackgroundImage = imagePieces[i, j];
        } // AddImagesToButtons

        private void startNewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            starTime = DateTime.Now;
            Random random = new Random();

            List<Bitmap> tempPieces = new List<Bitmap>();

            foreach (var piece in imagePieces)
                tempPieces.Add(piece);

            tempPieces.Remove(tempPieces.Last());

            for (int i = 0; i < imagePieces.GetLength(0); i++)
                for (int j = 0; j < imagePieces.GetLength(1); j++)
                {
                    if (tempPieces.Count == 0) break;
                    int randomIndex = random.Next(0, tempPieces.Count);
                    buttons[i, j].BackgroundImage = tempPieces[randomIndex];
                    tempPieces.RemoveAt(randomIndex);
                }

            buttons[size - 1, size - 1].BackgroundImage = null;

            foreach (var button in buttons)
                button.Enabled = true;
        } // startNewGameToolStripMenuItem_Click

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.BackgroundImage == null) return;

            for (int i = 0; i < buttons.GetLength(0); i++)
                for (int j = 0; j < buttons.GetLength(1); j++)
                    if (buttons[i, j] == button)
                        if(!IsMoved(i, j)) return; // check near buttons images for null and move image

            if(!IsVictory()) return; // check each button image == imagePieces

            VictoryScreen(); // victroy message + picture
        } // button_Click

        private bool IsVictory()
        {
            for (int i = 0; i < buttons.GetLength(0); i++)
                for (int j = 0; j < buttons.GetLength(1); j++)
                    if (i == buttons.GetLength(0) - 1 && j == buttons.GetLength(1) - 1) break;
                    else if (buttons[i, j].BackgroundImage != imagePieces[i, j]) return false;

            return true;
        } // IsVictory

        private void VictoryScreen()
        {
            buttons[size - 1, size - 1].BackgroundImage = imagePieces[size - 1, size - 1];

            TimeSpan ts = DateTime.Now - starTime;
            MessageBox.Show($"Victory! Solved by {ts.TotalSeconds} seconds.");

            foreach (var button in buttons)
                button.Enabled = false;
        } // VictoryScreen

        private bool IsMoved(int x, int y)
        {
            for (int i = x - 1; i <= x + 1; i++)
                for (int j = y - 1; j <= y + 1; j++)
                    if (i >= 0 && i < buttons.GetLength(0) && j >= 0 && j < buttons.GetLength(1) && (x != i || y != j))
                        if (buttons[i, j].BackgroundImage == null)
                        {
                            SwapButtonImages(buttons[x, y], buttons[i, j]); // swap images on buttons 
                            return true;
                        }

            return false;
        } // IsMoved

        void SwapButtonImages(Button buttonFrom, Button buttonTo)
        {
            buttonTo.BackgroundImage = buttonFrom.BackgroundImage;
            buttonFrom.BackgroundImage = null;
        } // SwapButtonImages

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();
    }
}
