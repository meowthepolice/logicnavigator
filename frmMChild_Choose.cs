using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace Logic_Navigator
{
	public class frmMChild_Choose : System.Windows.Forms.Form
	{	

		string rungNameOld = "";
		string rungNameNew = "";
		public ArrayList interlockingNew;
		public ArrayList interlockingOld;
		private Font drawFnt;

		private SolidBrush HighlightBrush = new SolidBrush(Color.HotPink);
		private Pen HighlightPen = new Pen(Color.HotPink);

		public ArrayList interlockingNewPointer;
		public ArrayList interlockingOldPointer;
        private ArrayList timersOld;
        private ArrayList timersNew;	
		private System.Windows.Forms.ListView listViewOld;
		private System.Windows.Forms.ListView listViewNew;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.StatusBar statusBar1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public frmMChild_Choose(ArrayList interlockingOldPointer, ArrayList interlockingNewPointer, ArrayList timersOldPointer, ArrayList timersNewPointer, Font drawFont)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			
			interlockingOld = interlockingOldPointer;
			interlockingNew = interlockingNewPointer;

            timersOld = timersOldPointer;
            timersNew = timersNewPointer;
			drawFnt = drawFont;
			
			for(int i=0; i<interlockingOld.Count-1; i++)
			{
				ArrayList rungPointer = (ArrayList) interlockingOld[i];		
				listViewOld.Items.Add(rungPointer[0].ToString() + ": " + (string) rungPointer[rungPointer.Count-1]);
			}	

			for(int i=0; i<interlockingNew.Count-1; i++)
			{
				ArrayList rungPointer = (ArrayList) interlockingNew[i];		
				listViewNew.Items.Add(rungPointer[0].ToString() + ": " + (string) rungPointer[rungPointer.Count-1]);				
			}			
		}

		private int findRung(ArrayList interlocking, string rungName)
		{
			int rungNumber = -1;
			for(int i=0; i<interlocking.Count; i++)
			{
				ArrayList rungPointer = (ArrayList) interlocking[i];
				if(string.Compare((string) rungPointer[rungPointer.Count-1], rungName) == 0) 
					rungNumber = (int) rungPointer[0];
			}
			return rungNumber;
		}

		private bool RungsSame(ArrayList rung1, ArrayList rung2)
		{
			if(rung1.Count != rung2.Count) return false;
			for(int i=1; i < rung1.Count - 1; i++)		
			{
				Contact contact1 = (Contact) rung1[i];	
				Contact contact2 = (Contact) rung2[i];	
				if(contact1.leftLink != contact2.leftLink) return false;
				if(contact1.bottomLink != contact2.bottomLink) return false;
				if(contact1.topLink != contact2.topLink) return false;
				if(contact1.NormallyClosed != contact2.NormallyClosed) return false;
				if(contact1.x != contact2.x) return false;
				if(contact1.y != contact2.y) return false;
				if(string.Compare(contact1.typeOfCell, contact2.typeOfCell) != 0) return false;
				if(string.Compare(contact1.name, contact2.name) != 0) return false;
			}
			return true;
		}

		private void ShowRungWindow()
		{
			//rungName = GetRungName(treeView.Nodes[treeView.SelectedNode.Index].ToString());					
			int newIndex = findRung(interlockingNew, rungNameNew);
			int oldIndex = findRung(interlockingOld, rungNameOld);
		    {		
			    frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, oldIndex - 1,
                    newIndex - 1, 0.75F, "Normal", drawFnt, "", true, true, Color.Blue, Color.Red);
                objfrmMChild.Size = new Size(objfrmMChild.RecommendedWidthofWindow(newIndex-1), objfrmMChild.RecommendedHeightofWindow(newIndex-1));
                objfrmMChild.Location = new System.Drawing.Point(1, 1);
			    objfrmMChild.Text = rungNameOld + " vs " + rungNameNew;	
			    objfrmMChild.MdiParent = this.MdiParent;
			    objfrmMChild.Show();
		    }
		}

		public string GetRungName(string treeViewString)
		{
			return  treeViewString.Substring(treeViewString.LastIndexOf(":") + 2);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMChild_Choose));
            this.listViewOld = new System.Windows.Forms.ListView();
            this.listViewNew = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.SuspendLayout();
            // 
            // listViewOld
            // 
            this.listViewOld.AllowDrop = true;
            this.listViewOld.BackColor = System.Drawing.Color.White;
            this.listViewOld.ForeColor = System.Drawing.Color.Blue;
            this.listViewOld.HideSelection = false;
            this.listViewOld.HoverSelection = true;
            this.listViewOld.Location = new System.Drawing.Point(8, 24);
            this.listViewOld.MultiSelect = false;
            this.listViewOld.Name = "listViewOld";
            this.listViewOld.Size = new System.Drawing.Size(456, 514);
            this.listViewOld.TabIndex = 13;
            this.listViewOld.UseCompatibleStateImageBehavior = false;
            this.listViewOld.View = System.Windows.Forms.View.List;
            this.listViewOld.SelectedIndexChanged += new System.EventHandler(this.listViewOld_SelectedIndexChanged);
            // 
            // listViewNew
            // 
            this.listViewNew.AllowDrop = true;
            this.listViewNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewNew.BackColor = System.Drawing.Color.White;
            this.listViewNew.ForeColor = System.Drawing.Color.Red;
            this.listViewNew.HideSelection = false;
            this.listViewNew.Location = new System.Drawing.Point(491, 27);
            this.listViewNew.MultiSelect = false;
            this.listViewNew.Name = "listViewNew";
            this.listViewNew.Size = new System.Drawing.Size(432, 514);
            this.listViewNew.TabIndex = 12;
            this.listViewNew.UseCompatibleStateImageBehavior = false;
            this.listViewNew.View = System.Windows.Forms.View.List;
            this.listViewNew.SelectedIndexChanged += new System.EventHandler(this.listViewNew_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.Location = new System.Drawing.Point(0, 566);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(944, 24);
            this.button1.TabIndex = 11;
            this.button1.Text = "Compare";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(488, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "New Rungs";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Old Rungs";
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 534);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(944, 32);
            this.statusBar1.TabIndex = 14;
            this.statusBar1.Text = "Select a rung from \'Old Rungs\' and one from \'New Rungs\', -----> then click on \'Co" +
    "mpare\' button";
            // 
            // frmMChild_Choose
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(944, 590);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.listViewOld);
            this.Controls.Add(this.listViewNew);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMChild_Choose";
            this.Text = "Rung";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

		}
		#endregion

		private void listViewOld_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(listViewOld.SelectedItems.Count > 0)
				rungNameOld = GetRungName(listViewOld.SelectedItems[0].Text);
			statusBar1.Text = rungNameOld;
		}

		private void listViewNew_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(listViewNew.SelectedItems.Count > 0)
				rungNameNew = GetRungName(listViewNew.SelectedItems[0].Text);
			statusBar1.Text = rungNameNew;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if((rungNameNew != "") && (rungNameOld != ""))
				ShowRungWindow();
		}
	}

}
