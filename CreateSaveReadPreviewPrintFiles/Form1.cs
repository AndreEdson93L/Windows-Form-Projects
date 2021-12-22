using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CreateSaveReadPreviewPrintFiles
{
    public partial class Form1 : Form
    {
        private int contatoreErrori = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //save file to the hard drive (disattivare l' antivirus per la sovrascrittura di file).
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    richTextBox1.SaveFile(saveFileDialog1.FileName);
                }
                catch (IOException ex)
                {
                    StringBuilder error = new StringBuilder();
                    error.Append($"Disattivare l' antivirus qualora si stia provando a sovrascivere un file e neghi l' accesso.\n");
                    error.Append($"Error: {ex.Message}\n");
                    error.Append($"StackTrace: {ex.StackTrace}");
                    MessageBox.Show(Convert.ToString(error));
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    richTextBox1.Text = $"Error: {ex.Message}\n";
                    richTextBox1.Text += $"StackTrace: {ex.StackTrace}";
                }
            }
        }
        //edit/underline
        private void txtActual_DoubleClick(object sender, EventArgs e)
        {
            var random = new Random();
            
            try
            {
                if (txtActual.Text.Trim().Length > 0)
                {
                    int startIndex = richTextBox1.Find(txtActual.Text);
                    richTextBox1.Select(startIndex, txtActual.Text.Length);
                    richTextBox1.SelectionBackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                    txtActual.Text = "";
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("You can't underline words that aren't in the document.");
                contatoreErrori++;
                if (contatoreErrori > 10)
                {
                    richTextBox1.Text += "\nIn case you didn't get the memo\n";
                    richTextBox1.Text += ex.Message;
                }
            }
        }
        //edit/raplace
        private void txtNew_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtNew.Text != "" && txtActual.Text.Trim().Length > 0)
                {
                    richTextBox1.Text = richTextBox1.Text.Replace(txtActual.Text, txtNew.Text);
                    txtNew.Text = "";
                }
            }
            catch 
            {
                MessageBox.Show("Be sure that you type both the words correctly.");
            }
        }
        //change font
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            richTextBox1.Font = fontDialog1.Font;
        }
        //draw the page for the print preview
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, fontDialog1.Font, Brushes.Black, e.MarginBounds, StringFormat.GenericTypographic);
        }
        //print the document + (it calls the printDocument1_PrintPage method)
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }
    }
}
