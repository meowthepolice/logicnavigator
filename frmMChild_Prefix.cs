using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;
using System.Media;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;



// Copyright (c) Damien Guard.  All rights reserved.
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Originally published at http://damieng.com/blog/2006/08/08/calculating_crc32_in_c_and_net

//using System;
//using System.Collections.Generic;
using System.Security.Cryptography;

namespace Logic_Navigator
{
    /// <summary>
    /// Summary description for frmAbout.
    /// </summary>
    public class frmMChild_Prefix : System.Windows.Forms.Form
    {
        private Button button1;
        private OpenFileDialog openFileDialog1;
        private System.IO.StreamReader SR;

        List<String> gpcfile = new List<String>();
        List<byte> filebytes = new List<byte>();
        int rotater = 0;

        //CRC32 crc32 = new CRC32();
        String hash = String.Empty;

        const int SPIN = 0;
        const int EXCHANGE = 0;
        const int PARTNER = 0;
        private Label label1;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Button MoveAllRight;
        private TextBox prefixtext;
        private Label label3;
        private Button ApplyPrefix;
        private TextBox filename;
        private Button MoveSelectedRight;
        private Button MoveAllLeft;
        private Button MoveSelectedLeft;
        private Button SelectAll1;
        private Button InvertSelection1;
        private Button DeselectAll1;
        private Button DeselectAll2;
        private Button InvertAll2;
        private Button SelectAll2;
        private Label diag1;
        private Label diag2;
        private TextBox filenameout;
        private Label label2;
        private Button savebrowse;
        private GroupBox groupBox1;
        private Button FilterLeft;
        private Button FilterRight;
        private CheckBox Inversecheck;
        private TextBox filterText;
        private Button RemovePrefix;
        private RichTextBox gpcview;
        private SaveFileDialog saveFileDialog1;
        private OpenFileDialog openFileDialog2;
        private Button SaveButton;
        private Label label4;
        private Button button2;
        private TextBox CRC32out;
        private Button button3;
        private TextBox AltText;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public frmMChild_Prefix()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMChild_Prefix));
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.MoveAllRight = new System.Windows.Forms.Button();
            this.prefixtext = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ApplyPrefix = new System.Windows.Forms.Button();
            this.filename = new System.Windows.Forms.TextBox();
            this.MoveSelectedRight = new System.Windows.Forms.Button();
            this.MoveAllLeft = new System.Windows.Forms.Button();
            this.MoveSelectedLeft = new System.Windows.Forms.Button();
            this.SelectAll1 = new System.Windows.Forms.Button();
            this.InvertSelection1 = new System.Windows.Forms.Button();
            this.DeselectAll1 = new System.Windows.Forms.Button();
            this.DeselectAll2 = new System.Windows.Forms.Button();
            this.InvertAll2 = new System.Windows.Forms.Button();
            this.SelectAll2 = new System.Windows.Forms.Button();
            this.diag1 = new System.Windows.Forms.Label();
            this.diag2 = new System.Windows.Forms.Label();
            this.filenameout = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.savebrowse = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FilterLeft = new System.Windows.Forms.Button();
            this.FilterRight = new System.Windows.Forms.Button();
            this.Inversecheck = new System.Windows.Forms.CheckBox();
            this.filterText = new System.Windows.Forms.TextBox();
            this.RemovePrefix = new System.Windows.Forms.Button();
            this.gpcview = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.SaveButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.CRC32out = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.AltText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Input:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 62);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 60;
            this.dataGridView1.RowTemplate.Height = 16;
            this.dataGridView1.Size = new System.Drawing.Size(163, 322);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(284, 62);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidth = 60;
            this.dataGridView2.RowTemplate.Height = 16;
            this.dataGridView2.Size = new System.Drawing.Size(163, 322);
            this.dataGridView2.TabIndex = 14;
            this.dataGridView2.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            // 
            // MoveAllRight
            // 
            this.MoveAllRight.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.MoveAllRight.Location = new System.Drawing.Point(202, 253);
            this.MoveAllRight.Name = "MoveAllRight";
            this.MoveAllRight.Size = new System.Drawing.Size(56, 25);
            this.MoveAllRight.TabIndex = 11;
            this.MoveAllRight.Text = ">>";
            this.MoveAllRight.UseVisualStyleBackColor = false;
            this.MoveAllRight.Click += new System.EventHandler(this.MoveAllRight_Click);
            // 
            // prefixtext
            // 
            this.prefixtext.AcceptsReturn = true;
            this.prefixtext.Location = new System.Drawing.Point(460, 86);
            this.prefixtext.Name = "prefixtext";
            this.prefixtext.Size = new System.Drawing.Size(56, 20);
            this.prefixtext.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(457, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Prefix";
            // 
            // ApplyPrefix
            // 
            this.ApplyPrefix.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ApplyPrefix.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ApplyPrefix.Location = new System.Drawing.Point(460, 113);
            this.ApplyPrefix.Name = "ApplyPrefix";
            this.ApplyPrefix.Size = new System.Drawing.Size(56, 25);
            this.ApplyPrefix.TabIndex = 16;
            this.ApplyPrefix.Text = "Apply";
            this.ApplyPrefix.UseVisualStyleBackColor = false;
            this.ApplyPrefix.Click += new System.EventHandler(this.ApplyPrefix_Click);
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
            // MoveSelectedRight
            // 
            this.MoveSelectedRight.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.MoveSelectedRight.Location = new System.Drawing.Point(202, 222);
            this.MoveSelectedRight.Name = "MoveSelectedRight";
            this.MoveSelectedRight.Size = new System.Drawing.Size(56, 25);
            this.MoveSelectedRight.TabIndex = 10;
            this.MoveSelectedRight.Text = ">";
            this.MoveSelectedRight.UseVisualStyleBackColor = false;
            this.MoveSelectedRight.Click += new System.EventHandler(this.MoveSelectedRight_Click);
            // 
            // MoveAllLeft
            // 
            this.MoveAllLeft.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.MoveAllLeft.Location = new System.Drawing.Point(202, 299);
            this.MoveAllLeft.Name = "MoveAllLeft";
            this.MoveAllLeft.Size = new System.Drawing.Size(56, 25);
            this.MoveAllLeft.TabIndex = 12;
            this.MoveAllLeft.Text = "<<";
            this.MoveAllLeft.UseVisualStyleBackColor = false;
            this.MoveAllLeft.Click += new System.EventHandler(this.MoveAllLeft_Click);
            // 
            // MoveSelectedLeft
            // 
            this.MoveSelectedLeft.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.MoveSelectedLeft.Location = new System.Drawing.Point(202, 330);
            this.MoveSelectedLeft.Name = "MoveSelectedLeft";
            this.MoveSelectedLeft.Size = new System.Drawing.Size(56, 25);
            this.MoveSelectedLeft.TabIndex = 13;
            this.MoveSelectedLeft.Text = "<";
            this.MoveSelectedLeft.UseVisualStyleBackColor = false;
            this.MoveSelectedLeft.Click += new System.EventHandler(this.MoveSelectedLeft_Click);
            // 
            // SelectAll1
            // 
            this.SelectAll1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SelectAll1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SelectAll1.Location = new System.Drawing.Point(8, 405);
            this.SelectAll1.Name = "SelectAll1";
            this.SelectAll1.Size = new System.Drawing.Size(56, 35);
            this.SelectAll1.TabIndex = 18;
            this.SelectAll1.Text = "Select All";
            this.SelectAll1.UseVisualStyleBackColor = false;
            this.SelectAll1.Click += new System.EventHandler(this.SelectAll1_Click);
            // 
            // InvertSelection1
            // 
            this.InvertSelection1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.InvertSelection1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.InvertSelection1.Location = new System.Drawing.Point(132, 405);
            this.InvertSelection1.Name = "InvertSelection1";
            this.InvertSelection1.Size = new System.Drawing.Size(68, 35);
            this.InvertSelection1.TabIndex = 20;
            this.InvertSelection1.Text = "Invert Selection";
            this.InvertSelection1.UseVisualStyleBackColor = false;
            this.InvertSelection1.Click += new System.EventHandler(this.InvertSelection1_Click);
            // 
            // DeselectAll1
            // 
            this.DeselectAll1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeselectAll1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.DeselectAll1.Location = new System.Drawing.Point(70, 405);
            this.DeselectAll1.Name = "DeselectAll1";
            this.DeselectAll1.Size = new System.Drawing.Size(56, 35);
            this.DeselectAll1.TabIndex = 19;
            this.DeselectAll1.Text = "Deselect All";
            this.DeselectAll1.UseVisualStyleBackColor = false;
            this.DeselectAll1.Click += new System.EventHandler(this.DeselectAll1_Click);
            // 
            // DeselectAll2
            // 
            this.DeselectAll2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeselectAll2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.DeselectAll2.Location = new System.Drawing.Point(342, 405);
            this.DeselectAll2.Name = "DeselectAll2";
            this.DeselectAll2.Size = new System.Drawing.Size(56, 35);
            this.DeselectAll2.TabIndex = 22;
            this.DeselectAll2.Text = "Deselect All";
            this.DeselectAll2.UseVisualStyleBackColor = false;
            this.DeselectAll2.Click += new System.EventHandler(this.DeselectAll2_Click);
            // 
            // InvertAll2
            // 
            this.InvertAll2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.InvertAll2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.InvertAll2.Location = new System.Drawing.Point(404, 405);
            this.InvertAll2.Name = "InvertAll2";
            this.InvertAll2.Size = new System.Drawing.Size(60, 35);
            this.InvertAll2.TabIndex = 23;
            this.InvertAll2.Text = "Invert Selection";
            this.InvertAll2.UseVisualStyleBackColor = false;
            this.InvertAll2.Click += new System.EventHandler(this.InvertAll2_Click);
            // 
            // SelectAll2
            // 
            this.SelectAll2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SelectAll2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SelectAll2.Location = new System.Drawing.Point(280, 405);
            this.SelectAll2.Name = "SelectAll2";
            this.SelectAll2.Size = new System.Drawing.Size(56, 35);
            this.SelectAll2.TabIndex = 21;
            this.SelectAll2.Text = "Select All";
            this.SelectAll2.UseVisualStyleBackColor = false;
            this.SelectAll2.Click += new System.EventHandler(this.SelectAll2_Click);
            // 
            // diag1
            // 
            this.diag1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.diag1.AutoSize = true;
            this.diag1.Location = new System.Drawing.Point(12, 387);
            this.diag1.Name = "diag1";
            this.diag1.Size = new System.Drawing.Size(40, 13);
            this.diag1.TabIndex = 30;
            this.diag1.Text = "0 items";
            // 
            // diag2
            // 
            this.diag2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.diag2.AutoSize = true;
            this.diag2.Location = new System.Drawing.Point(285, 387);
            this.diag2.Name = "diag2";
            this.diag2.Size = new System.Drawing.Size(40, 13);
            this.diag2.TabIndex = 31;
            this.diag2.Text = "0 items";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FilterLeft);
            this.groupBox1.Controls.Add(this.FilterRight);
            this.groupBox1.Controls.Add(this.Inversecheck);
            this.groupBox1.Controls.Add(this.filterText);
            this.groupBox1.Location = new System.Drawing.Point(181, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(96, 99);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // FilterLeft
            // 
            this.FilterLeft.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.FilterLeft.Location = new System.Drawing.Point(7, 68);
            this.FilterLeft.Name = "FilterLeft";
            this.FilterLeft.Size = new System.Drawing.Size(41, 25);
            this.FilterLeft.TabIndex = 9;
            this.FilterLeft.Text = "<";
            this.FilterLeft.UseVisualStyleBackColor = false;
            this.FilterLeft.Click += new System.EventHandler(this.FilterLeft_Click);
            // 
            // FilterRight
            // 
            this.FilterRight.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.FilterRight.Location = new System.Drawing.Point(49, 68);
            this.FilterRight.Name = "FilterRight";
            this.FilterRight.Size = new System.Drawing.Size(41, 25);
            this.FilterRight.TabIndex = 9;
            this.FilterRight.Text = ">";
            this.FilterRight.UseVisualStyleBackColor = false;
            this.FilterRight.Click += new System.EventHandler(this.FilterRight_Click);
            // 
            // Inversecheck
            // 
            this.Inversecheck.AutoSize = true;
            this.Inversecheck.Location = new System.Drawing.Point(18, 46);
            this.Inversecheck.Name = "Inversecheck";
            this.Inversecheck.Size = new System.Drawing.Size(61, 17);
            this.Inversecheck.TabIndex = 8;
            this.Inversecheck.Text = "Inverse";
            this.Inversecheck.UseVisualStyleBackColor = true;
            // 
            // filterText
            // 
            this.filterText.Location = new System.Drawing.Point(7, 20);
            this.filterText.Name = "filterText";
            this.filterText.Size = new System.Drawing.Size(83, 20);
            this.filterText.TabIndex = 7;
            // 
            // RemovePrefix
            // 
            this.RemovePrefix.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.RemovePrefix.Location = new System.Drawing.Point(460, 142);
            this.RemovePrefix.Name = "RemovePrefix";
            this.RemovePrefix.Size = new System.Drawing.Size(56, 25);
            this.RemovePrefix.TabIndex = 17;
            this.RemovePrefix.Text = "Remove";
            this.RemovePrefix.UseVisualStyleBackColor = false;
            this.RemovePrefix.Click += new System.EventHandler(this.RemovePrefix_Click);
            // 
            // gpcview
            // 
            this.gpcview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpcview.Location = new System.Drawing.Point(525, 113);
            this.gpcview.Name = "gpcview";
            this.gpcview.Size = new System.Drawing.Size(418, 327);
            this.gpcview.TabIndex = 24;
            this.gpcview.Text = "";
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(525, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(219, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "GPC File Preview (Insertions and labels only):";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(735, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 26);
            this.button2.TabIndex = 42;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_2);
            // 
            // CRC32out
            // 
            this.CRC32out.Location = new System.Drawing.Point(818, 57);
            this.CRC32out.Name = "CRC32out";
            this.CRC32out.Size = new System.Drawing.Size(125, 20);
            this.CRC32out.TabIndex = 43;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new System.Drawing.Point(656, 82);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 26);
            this.button3.TabIndex = 44;
            this.button3.Text = "Browse";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // AltText
            // 
            this.AltText.Location = new System.Drawing.Point(735, 85);
            this.AltText.Name = "AltText";
            this.AltText.Size = new System.Drawing.Size(125, 20);
            this.AltText.TabIndex = 45;
            // 
            // frmMChild_Prefix
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(949, 446);
            this.Controls.Add(this.AltText);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.CRC32out);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.gpcview);
            this.Controls.Add(this.RemovePrefix);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.savebrowse);
            this.Controls.Add(this.filenameout);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.diag2);
            this.Controls.Add(this.diag1);
            this.Controls.Add(this.DeselectAll2);
            this.Controls.Add(this.InvertAll2);
            this.Controls.Add(this.SelectAll2);
            this.Controls.Add(this.DeselectAll1);
            this.Controls.Add(this.InvertSelection1);
            this.Controls.Add(this.SelectAll1);
            this.Controls.Add(this.MoveSelectedLeft);
            this.Controls.Add(this.MoveAllLeft);
            this.Controls.Add(this.MoveSelectedRight);
            this.Controls.Add(this.filename);
            this.Controls.Add(this.ApplyPrefix);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.prefixtext);
            this.Controls.Add(this.MoveAllRight);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMChild_Prefix";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.Text = "Prefix Editor ";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename.Text = openFileDialog1.FileName;
                filenameout.Text = filename.Text.Substring(0, filename.Text.Length - 4) + "_prefix.gpc";
                readfile();
            }
        }


        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }


        private void readfile()
        {
            string line; bool endOfInst = false;
            SR = File.OpenText(filename.Text);
            line = "";
            bool started = false;

            this.dataGridView1.Columns.Clear();
            this.dataGridView2.Columns.Clear();
            this.dataGridView1.Rows.Clear();
            this.dataGridView2.Rows.Clear();



            this.dataGridView1.Columns.Add("Mnemonic", "Mnemonic");
            this.dataGridView2.Columns.Add("Mnemonic", "Mnemonic");
            this.dataGridView1.Columns[0].Width = 145;
            this.dataGridView2.Columns[0].Width = 145;
            gpcfile.Clear();

            while (((line = SR.ReadLine()) != null) && (endOfInst != true))
            {
                gpcfile.Add(line);
                addbytes(line + "\r\n");
                if (line.IndexOf("[Insertions]") != -1)
                    started = true;
                if ((line.IndexOf(",M") != -1) && started)
                {
                    string[] words = line.Split(' ');
                    for (int i = 0; i < words.Length; i++)
                    {
                        if ((words[i].IndexOf(",M") != -1) &&
                            (words[i + 1].IndexOf("NOT_ASSIGNED") == -1))
                        {
                            if (!ingrid(dataGridView1, words[i + 1]))
                                this.dataGridView1.Rows.Add(words[i + 1]);
                        }
                    }
                }
            }
            dataGridView1.Sort(this.dataGridView1.Columns["Mnemonic"], ListSortDirection.Ascending);
            dodiag();
            SR.Close();
        }

        private void addbytes(string line)
        {
            foreach (char ch in line)
            {
                filebytes.Add((byte)((byte)ch >> (byte)rotater)); //(original >> bits)
                //rotater++;
                //if (rotater < 7) rotater++;
                //else rotater = 0;
            }
        }

        private bool ingrid(DataGridView grid, string word)
        {
            for (int i = 0; i < grid.RowCount; i++)
                if (grid[0, i].Value != null)
                    if (grid[0, i].Value.ToString() == word)
                        return (true);
            return (false);
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MoveAllRight_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
                dataGridView2.Rows.Add(dataGridView1[0, i].Value);
            dataGridView1.Rows.Clear();
            sortandtrim();
        }

        private void MoveAllLeft_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
                dataGridView1.Rows.Add(dataGridView2[0, i].Value);
            dataGridView2.Rows.Clear();
            sortandtrim();
        }

        private void MoveSelectedRight_Click(object sender, EventArgs e)
        {
            List<int> selecteditems = new List<int>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
                if (dataGridView1[0, i].Selected)
                    selecteditems.Add(i);
            for (int i = 0; i < selecteditems.Count; i++)
                dataGridView2.Rows.Add(dataGridView1[0, selecteditems[i]].Value);
            for (int i = selecteditems.Count - 1; i >= 0; i--)
                dataGridView1.Rows.RemoveAt(selecteditems[i]);
            sortandtrim();

        }

        private void sortandtrim()
        {
            dataGridView1.Sort(dataGridView1.Columns["Mnemonic"], ListSortDirection.Ascending);
            removespaces(dataGridView1);
            dataGridView2.Sort(dataGridView2.Columns["Mnemonic"], ListSortDirection.Ascending);
            removespaces(dataGridView2);
            for (int i = 0; i < dataGridView1.RowCount; i++)
                dataGridView1[0, i].Selected = false;
            for (int i = 0; i < dataGridView2.RowCount; i++)
                dataGridView2[0, i].Selected = false;
            dodiag();
        }

        private void removespaces(DataGridView grid)
        {
            for (int i = 0; i < grid.RowCount - 1; i++)
                try
                {
                    if ((grid[0, i].Value == "") ||
                        (grid[0, i].Value == null))
                        try
                        {
                            grid.Rows.RemoveAt(i);
                        }
                        catch
                        { }
                }
                catch
                {
                    int j = i;
                }
        }

        private void MoveSelectedLeft_Click(object sender, EventArgs e)
        {
            List<int> selecteditems = new List<int>();
            for (int i = 0; i < dataGridView2.RowCount; i++)
                if (dataGridView2[0, i].Selected)
                    selecteditems.Add(i);
            for (int i = 0; i < selecteditems.Count; i++)
                dataGridView1.Rows.Add(dataGridView2[0, selecteditems[i]].Value);
            for (int i = selecteditems.Count - 1; i >= 0; i--)
                dataGridView2.Rows.RemoveAt(selecteditems[i]);
            sortandtrim();
        }

        private void InvertSelection1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
                if (dataGridView1[0, i].Selected)
                    dataGridView1[0, i].Selected = false;
                else
                    if (dataGridView1[0, i].Value != null)
                    dataGridView1[0, i].Selected = true;
            dodiag();
        }

        private void SelectAll1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
                if (dataGridView1[0, i].Value != null)
                    dataGridView1[0, i].Selected = true;
            dodiag();
        }

        private void DeselectAll1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
                dataGridView1[0, i].Selected = false;
            dodiag();
        }

        private void SelectAll2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
                if (dataGridView2[0, i].Value != null)
                    dataGridView2[0, i].Selected = true;
            dodiag();
        }

        private void DeselectAll2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
                dataGridView2[0, i].Selected = false;
            dodiag();
        }

        private void InvertAll2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
                if (dataGridView2[0, i].Selected)
                    dataGridView2[0, i].Selected = false;
                else
                    if (dataGridView2[0, i].Value != null)
                    dataGridView2[0, i].Selected = true;
            dodiag();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            dodiag();
        }

        private void dodiag()
        {
            diag(dataGridView1, 1);
            diag(dataGridView2, 2);
        }

        private void diag(DataGridView grid, int gridnum)
        {
            int selected = 0;
            for (int i = 0; i < grid.RowCount; i++)
                if (grid[0, i].Selected)
                    selected++;
            if (grid.RowCount != 0)
                if (selected != 0)
                    if (gridnum == 1)
                        diag1.Text = "Mnemonics: " + (grid.RowCount - 1).ToString() +
                            ", Selected: " + selected.ToString();
                    else
                        diag2.Text = "Mnemonics: " + (grid.RowCount - 1).ToString() +
                            ", Selected: " + selected.ToString();
                else
                    if (gridnum == 1)
                    diag1.Text = "Mnemonics: " + (grid.RowCount - 1).ToString();
                else
                    diag2.Text = "Mnemonics: " + (grid.RowCount - 1).ToString();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dodiag();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            dodiag();
        }

        private void FilterLeft_Click(object sender, EventArgs e)
        {
            filter(dataGridView2, dataGridView1);
        }

        private void FilterRight_Click(object sender, EventArgs e)
        {
            filter(dataGridView1, dataGridView2);
        }

        private void filter(DataGridView gridfrom, DataGridView gridto)
        {
            List<int> selecteditems = new List<int>();
            for (int i = 0; i < gridfrom.RowCount; i++)
                if (gridfrom[0, i].Value != null)
                {
                    if (Inversecheck.Checked)
                    {
                        if (!IsLike(filterText.Text, gridfrom[0, i].Value.ToString(), false))
                            selecteditems.Add(i);
                    }
                    else
                        if (IsLike(filterText.Text, gridfrom[0, i].Value.ToString(), false))
                        selecteditems.Add(i);
                }
            for (int i = 0; i < selecteditems.Count; i++)
                gridto.Rows.Add(gridfrom[0, selecteditems[i]].Value);
            for (int i = selecteditems.Count - 1; i >= 0; i--)
                gridfrom.Rows.RemoveAt(selecteditems[i]);
            sortandtrim();
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

        private void ApplyPrefix_Click(object sender, EventArgs e)
        {
            ApplyPrefixToData(dataGridView2);
        }

        private void ApplyPrefixToData(DataGridView grid)
        {
            for (int i = 0; i < grid.RowCount; i++)
                if (grid[0, i].Value != null)
                {
                    findandreplace(grid[0, i].Value.ToString(), prefixtext.Text + grid[0, i].Value);
                    grid[0, i].Value = prefixtext.Text + grid[0, i].Value;
                }
            dopreview();
        }

        private void findandreplace(string oldstr, string newstr)
        {
            bool started = false;
            for (int i = 0; i < gpcfile.Count; i++)
            {
                if (gpcfile[i].IndexOf("[Insertions]") != -1)
                    started = true;
                if (started)
                    if (gpcfile[i].IndexOf(" " + oldstr + " ") != -1)
                    {
                        string name = gpcfile[i];
                        name = gpcfile[i].Replace(" " + oldstr + " ", " " + newstr + " ");
                        gpcfile[i] = name;
                        name = "";
                    }
            }
        }

        private void dopreview()
        {
            bool start = false;
            gpcview.Text = "";
            for (int i = 0; i < gpcfile.Count; i++)
            {
                if (gpcfile[i].IndexOf("Insertions") != -1)
                    start = true;
                if (start)
                    gpcview.Text += gpcfile[i] + "\r\n";
            }
        }

        private void RemovePrefix_Click(object sender, EventArgs e)
        {
            ApplyPrefixRemToData(dataGridView2);
        }

        private void ApplyPrefixRemToData(DataGridView grid)
        {
            for (int i = 0; i < grid.RowCount; i++)
                if (grid[0, i].Value != null)
                    if (grid[0, i].Value.ToString().Substring(0, prefixtext.Text.Length) == prefixtext.Text)
                    {
                        findandreplace(grid[0, i].Value.ToString(), grid[0, i].Value.ToString().Substring(prefixtext.Text.Length));
                        grid[0, i].Value = grid[0, i].Value.ToString().Substring(prefixtext.Text.Length);
                    }
            dopreview();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
        }

        private void savefile()
        {

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
                saveFileDialog1.FileName = "filename" + ".gpc";
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
                for (int i = 0; i < gpcfile.Count; i++)
                {
                    WriteString(myStream, gpcfile[i]);
                    WriteString(myStream, CRLF);
                    if (i > gpcfile.Count - 3)
                        line = gpcfile[i];
                }
            myStream.Close();
            MessageBox.Show("Prefixed File Saved, " + filenameout.Text, "File Saved");

        }

        private void WriteString(Stream file, string characters)
        {
            for (int i = 0; i < characters.Length; i++)
                file.WriteByte((byte)characters[i]);
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            Crc322 crc32 = new Crc322();
            String hash = String.Empty;
            using (FileStream fs = File.Open(filename.Text, FileMode.Open)) //here you pass the file name 
            {

                StringBuilder hex = new StringBuilder(4 * 2);

                foreach (byte b in crc32.ComputeHash(fs))
                {
                    hex.AppendFormat("{0:x2}", b);
                    hash += ", "+ b.ToString("x2").ToLower();
                }

                foreach (byte ch in filebytes)
                {
                    //gpcview.Text += Convert.ToChar(ch);
                }

                //gpcview.Text += hash.ToString();

                CRC32out.Text = "CRC-32 is " + hash;
                //Console.WriteLine("CRC-32 is {0}", hash);
            }  
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

                    for (int i = 0; i < numBytesRead; i++)
                    {
                        byte ch = bytes[i];
                        if (rotater < 7) rotater++;
                        else rotater = 0;

                        for (int j = 0; j < rotater; j++)
                            ch = RotateRight(ch);
                        bytesrot[i] = ch; //(original >> bits)
                    }
                    Crc32 checksum = new Crc32();
                    string hex = "";
                    uint output = checksum.ComputeChecksum(bytesrot);

                    AltText.Text = output.ToString("X2");//  output//checksum.ComputeChecksum(bytesrot).ToString();

                    // Write the byte array to the other FileStream.
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

        private static byte RotateRight(byte b)
        {
            //Console.WriteLine("Rotating Right");
            byte mask = 0xff;
            return (byte)(((b >> 1) | (b << 7)) & mask);
        }


        public class Crc32
        {
            uint[] table;

            public uint ComputeChecksum(byte[] bytes)
            {
                uint crc = 0x00000000;
                for (int i = 0; i < bytes.Length; ++i)
                {
                    byte index = (byte)(((crc) & 0xff) ^ bytes[i]);
                    crc = (uint)((crc >> 8) ^ table[index]);
                }
                return ~crc;
            }

            public byte[] ComputeChecksumBytes(byte[] bytes)
            {
                return BitConverter.GetBytes(ComputeChecksum(bytes));
            }

            public Crc32()
            {
                uint poly = 0xedb88320;
                table = new uint[256];
                uint temp = 0;
                for (uint i = 0; i < table.Length; ++i)
                {
                    temp = i;
                    for (int j = 8; j > 0; --j)
                    {
                        if ((temp & 1) == 1)
                        {
                            temp = (uint)((temp >> 1) ^ poly);
                        }
                        else
                        {
                            temp >>= 1;
                        }
                    }
                    table[i] = temp;
                }
            }
        }

        public sealed class Crc322 : HashAlgorithm
        {
            public const UInt32 DefaultPolynomial = 0xedb88320u;
            public const UInt32 DefaultSeed = 0xffffffffu;

            static UInt32[] defaultTable;

            readonly UInt32 seed;
            readonly UInt32[] table;
            UInt32 hash;

            public Crc322()
                : this(DefaultPolynomial, DefaultSeed)
            {
            }

            public Crc322(UInt32 polynomial, UInt32 seed)
            {
                table = InitializeTable(polynomial);
                this.seed = hash = seed;
            }

            public override void Initialize()
            {
                hash = seed;
            }

            protected override void HashCore(byte[] array, int ibStart, int cbSize)
            {
                hash = CalculateHash(table, hash, array, ibStart, cbSize);
            }

            protected override byte[] HashFinal()
            {
                var hashBuffer = UInt32ToBigEndianBytes(~hash);
                HashValue = hashBuffer;
                return hashBuffer;
            }

            public override int HashSize { get { return 32; } }

            public static UInt32 Compute(byte[] buffer)
            {
                return Compute(DefaultSeed, buffer);
            }

            public static UInt32 Compute(UInt32 seed, byte[] buffer)
            {
                return Compute(DefaultPolynomial, seed, buffer);
            }

            public static UInt32 Compute(UInt32 polynomial, UInt32 seed, byte[] buffer)
            {
                return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
            }

            static UInt32[] InitializeTable(UInt32 polynomial)
            {
                if (polynomial == DefaultPolynomial && defaultTable != null)
                    return defaultTable;

                var createTable = new UInt32[256];
                for (var i = 0; i < 256; i++)
                {
                    var entry = (UInt32)i;
                    for (var j = 0; j < 8; j++)
                        if ((entry & 1) == 1)
                            entry = (entry >> 1) ^ polynomial;
                        else
                            entry = entry >> 1;
                    createTable[i] = entry;
                }

                if (polynomial == DefaultPolynomial)
                    defaultTable = createTable;

                return createTable;
            }

            static UInt32 CalculateHash(UInt32[] table, UInt32 seed, IList<byte> buffer, int start, int size)
            {
                var hash = seed;
                for (var i = start; i < start + size; i++)
                    hash = (hash >> 8) ^ table[buffer[i] ^ hash & 0xff];
                return hash;
            }

            static byte[] UInt32ToBigEndianBytes(UInt32 uint32)
            {
                var result = BitConverter.GetBytes(uint32);

                if (BitConverter.IsLittleEndian)
                    Array.Reverse(result);

                return result;
            }
        }
        

    }
}


