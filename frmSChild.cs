using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Logic_Navigator
{
	/// <summary>
	/// Summary description for frmChild.
	/// </summary>
	public class frmSChild : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public ArrayList interlockingNew;
		public ArrayList interlockingOld;
        private ArrayList timersOld;
        private ArrayList timersNew;	
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private string rungNameOld = "";
		private string rungNameNew = "";		
		private bool dragging = false;
		private Font drawFnt;

		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.ListBox listBoxNew;
		private System.Windows.Forms.ListView listViewNew;
		private System.Windows.Forms.ListView listViewOld;


        public frmSChild(ArrayList interlockingOldPointer, ArrayList interlockingNewPointer, ArrayList timersOldPointer, ArrayList timersNewPointer, Font drawFont)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//			
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
            int timerNewnumber = findTimer(timersNew, rung2[rung2.Count - 1].ToString());
            int timerOldnumber = findTimer(timersOld, rung1[rung1.Count - 1].ToString());
            if ((timerNewnumber == -1) && (timerNewnumber == -1)) return true;
            if (timerNewnumber == -1) return false;
            if (timerOldnumber == -1) return false;
            ML2Timer timerOldElement = (ML2Timer)timersOld[timerOldnumber];
            ML2Timer timerNewElement = (ML2Timer)timersNew[timerNewnumber];
            if ((timerOldElement.clearTime != timerNewElement.clearTime) ||
                (timerOldElement.setTime != timerNewElement.setTime))
                return false;
            return true;
		}


        private int findTimer(ArrayList timer, string name)
        {
            if (string.Compare(name, "") == 0)
                return -1;
            for (int i = 0; i < timer.Count; i++)
            {
                ML2Timer timerElement = (ML2Timer)timer[i];
                if (string.Compare((string)timerElement.timerName, name) == 0)
                    return i;
            }
            return -1;
        }

		private void ShowRungWindow()
		{
			//rungName = GetRungName(treeView.Nodes[treeView.SelectedNode.Index].ToString());					
			int newIndex = findRung(interlockingNew, rungNameNew);
			int oldIndex = findRung(interlockingOld, rungNameOld);
			{
                frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, oldIndex - 1,
                    newIndex - 1, 0.75F, "Normal", drawFnt, "", true, true, Color.Blue, Color.Red);
				objfrmMChild.Size = new Size(700, 400);
                objfrmMChild.Location = new System.Drawing.Point(1, 1);
				objfrmMChild.Text = rungNameOld + " vs " + rungNameNew;	
				objfrmMChild.MdiParent = this.MdiParent;
				objfrmMChild.Show();
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		private static frmSChild m_Childform;
        public static frmSChild GetChildInstance(ArrayList interlockingOldPointer, ArrayList interlockingNewPointer, ArrayList timersOld, ArrayList timersNew, Font drawFont)
		{
			if (m_Childform ==null) //if not created yet, Create an instance
				m_Childform = new frmSChild(interlockingOldPointer, interlockingNewPointer, timersOld, timersNew,  drawFont);
			return m_Childform;  //just created or created earlier.Return it*/
		}
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.listBoxNew = new System.Windows.Forms.ListBox();
			this.listViewNew = new System.Windows.Forms.ListView();
			this.listViewOld = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Old Rungs";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(496, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "New Rungs";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Location = new System.Drawing.Point(424, 568);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(128, 24);
			this.button1.TabIndex = 4;
			this.button1.Text = "Compare";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 600);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(960, 22);
			this.statusBar1.TabIndex = 5;
			this.statusBar1.Text = "statusBar1";
			// 
			// listBoxNew
			// 
			this.listBoxNew.Location = new System.Drawing.Point(192, 24);
			this.listBoxNew.Name = "listBoxNew";
			this.listBoxNew.Size = new System.Drawing.Size(168, 511);
			this.listBoxNew.TabIndex = 7;
			// 
			// listViewNew
			// 
			this.listViewNew.AllowDrop = true;
			this.listViewNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.listViewNew.BackColor = System.Drawing.Color.White;
			this.listViewNew.ForeColor = System.Drawing.Color.Red;
			this.listViewNew.HideSelection = false;
			this.listViewNew.Location = new System.Drawing.Point(496, 24);
			this.listViewNew.MultiSelect = false;
			this.listViewNew.Name = "listViewNew";
			this.listViewNew.Size = new System.Drawing.Size(432, 520);
			this.listViewNew.TabIndex = 7;
			this.listViewNew.View = System.Windows.Forms.View.List;
			this.listViewNew.SelectedIndexChanged += new System.EventHandler(this.listViewNew_SelectedIndexChanged);
			// 
			// listViewOld
			// 
			this.listViewOld.AllowDrop = true;
			this.listViewOld.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.listViewOld.BackColor = System.Drawing.Color.White;
			this.listViewOld.ForeColor = System.Drawing.Color.Blue;
			this.listViewOld.HideSelection = false;
			this.listViewOld.Location = new System.Drawing.Point(16, 24);
			this.listViewOld.MultiSelect = false;
			this.listViewOld.Name = "listViewOld";
			this.listViewOld.Size = new System.Drawing.Size(456, 520);
			this.listViewOld.TabIndex = 8;
			this.listViewOld.View = System.Windows.Forms.View.List;
			this.listViewOld.SelectedIndexChanged += new System.EventHandler(this.listViewOld_SelectedIndexChanged);
			// 
			// frmSChild
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(960, 622);
			this.Controls.Add(this.listViewOld);
			this.Controls.Add(this.listViewNew);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "frmSChild";
			this.Text = "Compare Rungs";
			this.ResumeLayout(false);

		}
		#endregion

		private void frmSChild_Load(object sender, System.EventArgs e)
		{
			
		}

		public string GetRungName(string treeViewString)
		{
			return  treeViewString.Substring(treeViewString.LastIndexOf(":") + 2);
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if((rungNameNew != "") && (rungNameOld != ""))
				ShowRungWindow();
		}

		private void listViewNew_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(listViewNew.SelectedItems.Count > 0)
				rungNameNew = GetRungName(listViewNew.SelectedItems[0].Text);
			statusBar1.Text = rungNameNew;
		}

		private void listViewOld_SelectedIndexChanged(object sender, System.EventArgs e)
		{
					if(listViewOld.SelectedItems.Count > 0)
				rungNameOld = GetRungName(listViewOld.SelectedItems[0].Text);
			statusBar1.Text = rungNameOld;
		}
	}
}
