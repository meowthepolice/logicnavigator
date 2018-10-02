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
	public class frmWeb : System.Windows.Forms.Form
    {
        private TabControl tabControl1;
        private Button Back;
        private Button Forward;
        private Button Refresh;
        private Button Search;
        private Button Home;
        private TextBox textBox1;
        private Button Newtab;
        private Button button1;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        String websiteurl = "";
        int counter = 0;

		public frmWeb(String website)
		{
            websiteurl = website;
              //
              // Required for Windows Form Designer support
              //
            InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}
        WebBrowser browser = new WebBrowser();
        int i = 0;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWeb));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Back = new System.Windows.Forms.Button();
            this.Forward = new System.Windows.Forms.Button();
            this.Refresh = new System.Windows.Forms.Button();
            this.Search = new System.Windows.Forms.Button();
            this.Home = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Newtab = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(2, 37);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1383, 708);
            this.tabControl1.TabIndex = 0;
            // 
            // Back
            // 
            this.Back.Image = ((System.Drawing.Image)(resources.GetObject("Back.Image")));
            this.Back.Location = new System.Drawing.Point(1, 1);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(40, 36);
            this.Back.TabIndex = 1;
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Forward
            // 
            this.Forward.Image = ((System.Drawing.Image)(resources.GetObject("Forward.Image")));
            this.Forward.Location = new System.Drawing.Point(42, 1);
            this.Forward.Name = "Forward";
            this.Forward.Size = new System.Drawing.Size(40, 36);
            this.Forward.TabIndex = 2;
            this.Forward.UseVisualStyleBackColor = true;
            this.Forward.Click += new System.EventHandler(this.Forward_Click);
            // 
            // Refresh
            // 
            this.Refresh.Image = ((System.Drawing.Image)(resources.GetObject("Refresh.Image")));
            this.Refresh.Location = new System.Drawing.Point(84, 1);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(40, 36);
            this.Refresh.TabIndex = 3;
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // Search
            // 
            this.Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Search.Image = ((System.Drawing.Image)(resources.GetObject("Search.Image")));
            this.Search.Location = new System.Drawing.Point(1339, 0);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(40, 36);
            this.Search.TabIndex = 4;
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // Home
            // 
            this.Home.Image = ((System.Drawing.Image)(resources.GetObject("Home.Image")));
            this.Home.Location = new System.Drawing.Point(127, 1);
            this.Home.Name = "Home";
            this.Home.Size = new System.Drawing.Size(40, 36);
            this.Home.TabIndex = 5;
            this.Home.UseVisualStyleBackColor = true;
            this.Home.Click += new System.EventHandler(this.Home_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(239, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1094, 26);
            this.textBox1.TabIndex = 6;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // Newtab
            // 
            this.Newtab.Image = ((System.Drawing.Image)(resources.GetObject("Newtab.Image")));
            this.Newtab.Location = new System.Drawing.Point(173, 6);
            this.Newtab.Name = "Newtab";
            this.Newtab.Size = new System.Drawing.Size(35, 25);
            this.Newtab.TabIndex = 7;
            this.Newtab.UseVisualStyleBackColor = true;
            this.Newtab.Click += new System.EventHandler(this.Newtab_Click);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(210, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 25);
            this.button1.TabIndex = 8;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmWeb
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1383, 746);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Newtab);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Home);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.Refresh);
            this.Controls.Add(this.Forward);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmWeb";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logic Navigator Web Browser";
            this.Load += new System.EventHandler(this.frmWeb_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
        #endregion

        private void frmWeb_Load(object sender, EventArgs e)
        {
            browser = new WebBrowser();
            browser.ScriptErrorsSuppressed = true;
            browser.Dock = DockStyle.Fill;
            browser.Visible = true;
            browser.DocumentCompleted += Browser_DocumentCompleted;
            browser.Navigate(websiteurl);
            //tabControl1.Anchor = AnchorStyles.Top & AnchorStyles.Bottom & AnchorStyles.Left & AnchorStyles.Right;
            tabControl1.TabPages.Add("New Tab");
            tabControl1.SelectTab(i);
            tabControl1.SelectedTab.Controls.Add(browser);
            i += 1;
        }

        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            tabControl1.SelectedTab.Text = ((WebBrowser)tabControl1.SelectedTab.Controls[0]).DocumentTitle;
            textBox1.Text = browser.Url.AbsoluteUri;
            //this.Text = counter.ToString(); counter++;
        }
        
        private void Navigate(String address)
        {
            if (String.IsNullOrEmpty(address)) return;
            if (address.Equals("about:blank")) return;
            if((!address.StartsWith("http://")) && 
               (!address.StartsWith("https://")))
            {
                address = "http://" + address;            
            }
            try
            {
                browser.Navigate(new Uri(address));
            }
            catch(System.UriFormatException)
            {
                return;
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).GoBack();
        }

        private void Forward_Click(object sender, EventArgs e)
        {
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).GoForward();
        }

        private void Home_Click(object sender, EventArgs e)
        {
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(websiteurl);
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(textBox1.Text);
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(textBox1.Text);
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {

            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(textBox1.Text);
        }

        private void Newtab_Click(object sender, EventArgs e)
        {
            browser = new WebBrowser();
            browser.ScriptErrorsSuppressed = true;
            browser.Dock = DockStyle.Fill;
            browser.Visible = true;
            browser.DocumentCompleted += Browser_DocumentCompleted;
            browser.Navigate("http://www.google.com");
            //tabControl1.Anchor = AnchorStyles.Top & AnchorStyles.Bottom & AnchorStyles.Left & AnchorStyles.Right;
            tabControl1.TabPages.Add("New Tab");
            tabControl1.SelectTab(i);
            tabControl1.SelectedTab.Controls.Add(browser);
            i += 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(tabControl1.TabPages.Count - 1 > 0)
            {
                tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
                tabControl1.SelectTab(tabControl1.TabPages.Count - 1);
                i -= 1;
            }
        }
    }
}
