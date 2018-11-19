using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Media;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


namespace Logic_Navigator
{
    /// <summary>
    /// Summary description for frmAbout.
    /// </summary>
    public class frmMChild_Prefixmap : System.Windows.Forms.Form
    {
        private Button button1;
        private OpenFileDialog openFileDialog1;
        private System.IO.StreamReader SR;

        List<String> mapfile = new List<String>();
        List<String> mapfileout = new List<String>();
        List<byte> filebytes = new List<byte>();
        int rotater = 0;

        //CRC32 crc32 = new CRC32();
        String hash = String.Empty;

        const int SPIN = 0;
        const int EXCHANGE = 0;
        const int PARTNER = 0;
        private Label label1;
        private Label label3;
        private TextBox filename;
        private TextBox filenameout;
        private Label label2;
        private Button savebrowse;
        private RichTextBox originalmap;
        private SaveFileDialog saveFileDialog1;
        private OpenFileDialog openFileDialog2;
        private Button SaveButton;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label5;
        private RichTextBox newmap;
        private Button button2;
        private TextBox prefixtext;
        private Label label4;
        private Button button3;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public frmMChild_Prefixmap()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMChild_Prefixmap));
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.filename = new System.Windows.Forms.TextBox();
            this.filenameout = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.savebrowse = new System.Windows.Forms.Button();
            this.originalmap = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.SaveButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.newmap = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.prefixtext = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(783, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 26);
            this.button1.TabIndex = 2;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Input:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Prefix to remove";
            // 
            // filename
            // 
            this.filename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filename.Location = new System.Drawing.Point(64, 5);
            this.filename.Name = "filename";
            this.filename.Size = new System.Drawing.Size(713, 20);
            this.filename.TabIndex = 1;
            // 
            // filenameout
            // 
            this.filenameout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filenameout.Location = new System.Drawing.Point(64, 31);
            this.filenameout.Name = "filenameout";
            this.filenameout.Size = new System.Drawing.Size(713, 20);
            this.filenameout.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Output:";
            // 
            // savebrowse
            // 
            this.savebrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.savebrowse.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.savebrowse.Location = new System.Drawing.Point(783, 27);
            this.savebrowse.Name = "savebrowse";
            this.savebrowse.Size = new System.Drawing.Size(77, 26);
            this.savebrowse.TabIndex = 5;
            this.savebrowse.Text = "Browse";
            this.savebrowse.UseVisualStyleBackColor = false;
            this.savebrowse.Click += new System.EventHandler(this.savebrowse_Click);
            // 
            // originalmap
            // 
            this.originalmap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.originalmap.Location = new System.Drawing.Point(12, 134);
            this.originalmap.Name = "originalmap";
            this.originalmap.Size = new System.Drawing.Size(457, 299);
            this.originalmap.TabIndex = 24;
            this.originalmap.Text = "";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SaveButton.Location = new System.Drawing.Point(866, 8);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(77, 35);
            this.SaveButton.TabIndex = 3;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.Location = new System.Drawing.Point(132, 68);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(110, 20);
            this.textBox1.TabIndex = 46;
            // 
            // textBox2
            // 
            this.textBox2.AcceptsReturn = true;
            this.textBox2.Location = new System.Drawing.Point(132, 94);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(110, 20);
            this.textBox2.TabIndex = 48;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 47;
            this.label5.Text = "Prefix to add";
            // 
            // newmap
            // 
            this.newmap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newmap.Location = new System.Drawing.Point(475, 134);
            this.newmap.Name = "newmap";
            this.newmap.Size = new System.Drawing.Size(468, 299);
            this.newmap.TabIndex = 49;
            this.newmap.Text = "";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.Location = new System.Drawing.Point(577, 67);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 26);
            this.button2.TabIndex = 50;
            this.button2.Text = "Append prefix";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // prefixtext
            // 
            this.prefixtext.AcceptsReturn = true;
            this.prefixtext.Location = new System.Drawing.Point(461, 71);
            this.prefixtext.Name = "prefixtext";
            this.prefixtext.Size = new System.Drawing.Size(110, 20);
            this.prefixtext.TabIndex = 52;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(389, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "Prefix to add";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button3.Location = new System.Drawing.Point(248, 67);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 26);
            this.button3.TabIndex = 53;
            this.button3.Text = "Do prefixes";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // frmMChild_Prefixmap
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(949, 446);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.prefixtext);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.newmap);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.originalmap);
            this.Controls.Add(this.savebrowse);
            this.Controls.Add(this.filenameout);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.filename);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMChild_Prefixmap";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.Text = "Prefix Editor ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename.Text = openFileDialog1.FileName;
                filenameout.Text = filename.Text.Substring(0, filename.Text.Length - 4) + "_prefix.map";
                readfile();
                showmap();
            }
        }
        
        private void showmap()
        {
            int count = 0;
            string temp = "";
            foreach (string line in mapfile)
            {
                count++;
                temp += line;
                temp += "\r\n";
            }
            originalmap.Text = temp;
            newmap.Text = temp;

        }

        private void readfile()
        {
            string line; bool endOfInst = false;
            SR = File.OpenText(filename.Text);
            line = "";
            bool started = false;
            
            mapfile.Clear();

            while (((line = SR.ReadLine()) != null) && (endOfInst != true))
            {
                mapfile.Add(line);                                
            }
            SR.Close();
        }
        
        private bool ingrid(DataGridView grid, string word)
        {
            for (int i = 0; i < grid.RowCount; i++)
                if (grid[0, i].Value != null)
                    if (grid[0, i].Value.ToString() == word)
                        return (true);
            return (false);
        }
        
        public static bool IsLike(string pattern, string text, bool caseSensitive = false)
        {
            pattern = pattern.Replace(".", @"\.");
            pattern = pattern.Replace("?", ".");
            pattern = pattern.Replace("*", ".*?");
            pattern = pattern.Replace(@"\", @"\\");
            pattern = pattern.Replace(" ", @"\s");
            return new Regex(pattern, caseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase).IsMatch(text);
        }
        
        private void savebrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                filenameout.Text = openFileDialog2.FileName;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            string CRLF = "\r\n";

            bool dialog = false;
            if (filenameout.Text == "")
            {
                saveFileDialog1.FileName = "filename" + ".map";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        dialog = true;
                        filenameout.Text = saveFileDialog1.FileName.ToString();
                    }
            }
            else
            {
                File.Delete(filenameout.Text);
                myStream = File.OpenWrite(filenameout.Text);
                myStream.Flush();
                dialog = true;
            }
            string line = "";
            if (dialog == true)
                for (int i = 0; i < mapfileout.Count; i++)
                {
                    WriteString(myStream, mapfileout[i]);
                    WriteString(myStream, CRLF);
                }
            myStream.Close();
            MessageBox.Show("Prefixed File Saved, " + filenameout.Text, "File Saved");

        }

        private void WriteString(Stream file, string characters)
        {
            for (int i = 0; i < characters.Length; i++)
                file.WriteByte((byte)characters[i]);
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            // Specify a file to read from and to create.
            string pathSource = filename.Text;
            string pathNew = filename.Text + "out";
            rotater = 0;

            try
            {

                using (FileStream fsSource = new FileStream(pathSource,
                    FileMode.Open, FileAccess.Read))
                {

                    // Read the source file into a byte array.
                    byte[] bytes = new byte[fsSource.Length];
                    byte[] bytesrot = new byte[fsSource.Length];
                    int numBytesToRead = (int)fsSource.Length;
                    int numBytesRead = 0;
                    while (numBytesToRead > 0)
                    {
                        // Read may return anything from 0 to numBytesToRead.
                        int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                        // Break when the end of the file is reached.
                        if (n == 0)
                            break;

                        numBytesRead += n;
                        numBytesToRead -= n;
                    }
                    numBytesToRead = bytes.Length;
                    using (FileStream fsNew = new FileStream(pathNew,
                        FileMode.Create, FileAccess.Write))
                    {
                        fsNew.Write(bytesrot, 0, numBytesToRead);
                    }
                }
            }
            catch (FileNotFoundException ioEx)
            {
                Console.WriteLine(ioEx.Message);
            }
        }
        
        private void button2_Click_1(object sender, EventArgs e)
        {
            string prefixedstring = "";
            string prefixtxt = prefixtext.Text;
            mapfileout.Clear();
            foreach(string line in mapfile)
            {
                prefixedstring += doline(prefixtxt, line) + "\r\n";
                mapfileout.Add(doline(prefixtxt, line));
            }
            newmap.Text = prefixedstring;
        }

        private string doline(string prefixtxt, string line)
        {
            if(line.Length >= 10)
                if (line.Substring(0, 10) == "Indication")
                    if (line.Length > 12)
                        return line.Substring(0, 12) +
                            addprefix(prefixtxt, line.Substring(12, line.Length - 12));
            if(line.Length >= 11)
                if (line.Substring(0, 11) == "ControlName")
                    if (line.Length > 13)
                        return line.Substring(0, 13) +
                            addprefix(prefixtxt, line.Substring(13, line.Length - 13));
            return line;
        }

        private string addprefix(string prefix, string variable)
        {
            if (variable.Substring(0, 1) == "!")
                return "!" + prefix + variable.Substring(1, variable.Length - 1);
            if (variable.Substring(0, 1) == "~")
                return "~" + prefix + variable.Substring(1, variable.Length - 1);
            return prefix + variable;
        }
    }
}


