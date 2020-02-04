using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Logic_Navigator
{
	/// <summary>
	/// Summary description for frmAbout.
	/// </summary>
	public class frmAbout : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label7;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private PictureBox pictureBox1;
        private Label label20;
        private Label label21;
        private Label label16;
        private Label label17;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

		public frmAbout()
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
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(18, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(369, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "Software written by Ken Karrasch (Developer) and Dylan Willis (Housing view debug" +
    "ging, NCD file ip configuration error checking)";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(464, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Left Mouse:  Pan / Open current contact";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(20, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(464, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ctrl - Mouse Wheel : Zoom In/Out";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(20, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(464, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Right Button: Drag New rungs across Old rungs";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 32);
            this.label5.TabIndex = 6;
            this.label5.Text = "Logic Navigator";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(18, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(464, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "This program compares, explores, and simulates ladder logic.";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(18, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 29);
            this.label8.TabIndex = 9;
            this.label8.Text = "File Formats supported:";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(12, 215);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 16);
            this.label9.TabIndex = 10;
            this.label9.Text = "Controls";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(192, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 24);
            this.label7.TabIndex = 13;
            this.label7.Text = "(Build 0.0609)";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(131, 193);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(316, 29);
            this.label10.TabIndex = 14;
            this.label10.Text = "- Westrace (HVLM, VLM6, Westrace 2) - INS, WT2, and NCD";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(131, 204);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(316, 27);
            this.label11.TabIndex = 15;
            this.label11.Text = "- Microlok - MLK and ML2 (and reverse compiled ML2)";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(131, 215);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(141, 14);
            this.label12.TabIndex = 16;
            this.label12.Text = "- VPI - VTL, LSV and NV";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(18, 87);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(392, 24);
            this.label13.TabIndex = 17;
            this.label13.Text = "Contact: 0438 751 993, or logicnavigator@gmail.com";
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label14.Location = new System.Drawing.Point(18, 109);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(413, 69);
            this.label14.TabIndex = 18;
            this.label14.Text = resources.GetString("label14.Text");
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(20, 273);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(462, 16);
            this.label15.TabIndex = 19;
            this.label15.Text = "Hover over any contact to get preview of rung. Double Click on screen to enter an" +
    "alysis mode.";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(437, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(425, 222);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(12, 290);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(61, 15);
            this.label20.TabIndex = 26;
            this.label20.Text = "Warning";
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(20, 311);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(842, 47);
            this.label21.TabIndex = 27;
            this.label21.Text = resources.GetString("label21.Text");
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(131, 180);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(316, 29);
            this.label16.TabIndex = 28;
            this.label16.Text = "- S2 Scanner data - rung.txt outputfile(limited support for timers)";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(275, 216);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(141, 14);
            this.label17.TabIndex = 29;
            this.label17.Text = "- FEP - config.ini";
            // 
            // frmAbout
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(873, 367);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label16);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About Logic Navigator";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void frmAbout_Load(object sender, EventArgs e)
        {

        }
    }
}
