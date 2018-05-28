using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Notepad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string filename = "";
        private System.Drawing.Printing.PrintDocument docToPrint = new System.Drawing.Printing.PrintDocument();

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                DialogResult click = MessageBox.Show("The text in the Untitled has changed.\n\n Do you want to save the changes?", " My Notepad", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (click == DialogResult.Yes)
                {
                    if (filename == "")
                    {
                        saveFileDialog1.Filter = "Text Files|*.txt";
                        DialogResult result = saveFileDialog1.ShowDialog();
                        if (result == DialogResult.Cancel)
                        {
                            return;
                        }
                        filename = saveFileDialog1.FileName;
                        // MessageBox.Show(fname);
                    }
                    StreamWriter write = new StreamWriter(filename);
                    write.WriteLine(textBox1.Text);
                    write.Flush();
                    //  textBox1.Text = "";
                    write.Close();

                    textBox1.Text = "";
                    filename = "";
                    // bool flag = false;
                }
                if (click == DialogResult.No)
                {
                    textBox1.Clear();
                    this.Text = "Untitled-Notepad";
                }
            }
            else
            {
                textBox1.Clear();
                filename = "";
                this.Text = "Untitled-Notepad";
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = openFileDialog1.ShowDialog();
                if (res == DialogResult.OK)
                {
                    string str = openFileDialog1.FileName, str2;
                    FileStream fS = new FileStream(str, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fS);
                    str2 = sr.ReadToEnd();
                    textBox1.Text = str2;
                    sr.Close();
                    fS.Close();
                    this.Text = openFileDialog1.FileName ;
                }
                else
                {
                    MessageBox.Show(" Not Opened File");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filename == "")
            {
                saveFileDialog1.Filter = "Text Files|*.txt";
                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                filename = saveFileDialog1.FileName;
                StreamWriter s = new StreamWriter(filename);
                s.WriteLine(textBox1.Text);
                s.Flush();
                s.Close();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text Files|*.txt";
            saveFileDialog1.ShowDialog();
            filename = saveFileDialog1.FileName;
            if (filename == "")
            {
                saveFileDialog1.Filter = "Text Files|*.txt";
                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                filename = saveFileDialog1.FileName;
            }
            StreamWriter s = new StreamWriter(filename);
            s.WriteLine(textBox1.Text);
            s.Flush();
            s.Close();
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.PageSettings = new System.Drawing.Printing.PageSettings();
            pageSetupDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            pageSetupDialog1.ShowNetwork = false;
            DialogResult result = pageSetupDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                object[] results = new object[]{
                    pageSetupDialog1.PageSettings.Margins,
                    pageSetupDialog1.PageSettings.PaperSize,
                    pageSetupDialog1.PageSettings.Landscape,
                    pageSetupDialog1.PrinterSettings.PrinterName,
                    pageSetupDialog1.PrinterSettings.PrintRange};
                //richTextBox1.Text.LastIndexOf(results);
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*printDialog1.AllowSomePages = true;
            printDialog1.ShowHelp = true;
            printDialog1.Document = docToPrint;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                docToPrint.Print();
            }*/
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                DialogResult click = MessageBox.Show("The text in the Untitled has changed.\n\n Do you want to save the changes?", " My Notepad", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (click == DialogResult.Yes)
                {
                    if (filename == "")
                    {
                        saveFileDialog1.Filter = "Text Files|*.txt";
                        DialogResult result = saveFileDialog1.ShowDialog();
                        if (result == DialogResult.Cancel)
                        {
                            return;
                        }
                        filename = saveFileDialog1.FileName;
                        // MessageBox.Show(fname);
                    }
                    StreamWriter write = new StreamWriter(filename);
                    write.WriteLine(textBox1.Text);
                    write.Flush();
                    write.Close();
                    Application.Exit();
                }
                if (click == DialogResult.No)
                {
                    Application.Exit();
                }
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {           
               if (textBox1.CanUndo)
                    textBox1.Undo();          
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectedText != "")
            {
                cutToolStripMenuItem.Enabled = true;
                textBox1.Cut();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectedText != "")
            {
                textBox1.Copy();
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                textBox1.SelectedText = "";
            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FindTab tab = new FindTab(this);
            //tab.Show();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                textBox1.SelectAll();
            }
            else
            {
                MessageBox.Show("No Text is there");
            }
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string timeDate;
            timeDate = DateTime.Now.ToShortTimeString() + " " +
            DateTime.Now.ToShortDateString();
            int newSelectionStart = textBox1.SelectionStart + timeDate.Length;
            textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, timeDate);
            textBox1.SelectionStart = newSelectionStart;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;
            fontDialog1.ShowDialog();
            textBox1.Font = fontDialog1.Font;
            textBox1.ForeColor = fontDialog1.Color;
            Properties.Settings.Default.font = textBox1.Font;
            Properties.Settings.Default.color = textBox1.ForeColor;
            Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Font = Properties.Settings.Default.font;
            textBox1.ForeColor = Properties.Settings.Default.color;
            textBox1.Focus();
            this.Text = "Untitled-Notepad";
        }

        private void wordToolStripMenuItem_Click(object sender, EventArgs e)
        {         
           if( wordToolStripMenuItem.CheckState==CheckState.Checked)
            {                
                textBox1.Multiline = true;
                textBox1.ScrollBars = ScrollBars.Vertical;
                textBox1.AcceptsReturn = true;
                textBox1.AcceptsTab = true;
                textBox1.WordWrap = true;
            }
           else
            {             
                textBox1.Multiline = true;
                textBox1.ScrollBars = ScrollBars.Horizontal;
                textBox1.AcceptsReturn = false;
                textBox1.AcceptsTab = false;
                textBox1.WordWrap = false;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                undoToolStripMenuItem.Enabled = true;
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
            }
            else
            {
                undoToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
            }
            undoToolStripMenuItem.Enabled = true;
            status.Text = "Line: " + (textBox1.GetLineFromCharIndex(Int32.MaxValue) + 1) + "   Cols: " + textBox1.Text.Length;

        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://go.microsoft.com/fwlink/?LinkId=834783");
        }

        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.Show();
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show( "Line: " + (textBox1.GetLineFromCharIndex(Int32.MaxValue) + 1) + "   Cols: " + textBox1.Text.Length);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(textBox1.Text, new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new PointF(100, 100));
        }
    }

}
