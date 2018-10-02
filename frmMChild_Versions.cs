using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;




namespace Logic_Navigator
{
	public class frmMChild_Versions : System.Windows.Forms.Form
	{	
		private SolidBrush HighlightBrush = new SolidBrush(Color.HotPink);
		private Pen HighlightPen = new Pen(Color.HotPink);

		public ArrayList interlockingNewPointer;
		public ArrayList interlockingOldPointer;
		
		private ArrayList versionRecOld;
		private ArrayList versionRecNew;

		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.ListView Rungs;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.ColumnHeader Date;
		private System.Windows.Forms.ColumnHeader VersionNumber;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ColumnHeader Type;
		private System.Windows.Forms.ListView OldVersions;
		private System.Windows.Forms.ColumnHeader Name_test;
		private System.Windows.Forms.ColumnHeader typeOfVersion;
		private System.Windows.Forms.ColumnHeader nameOfPerson;
		private System.Windows.Forms.ColumnHeader VersionNo;
		private System.Windows.Forms.ColumnHeader DateString;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListView NewVersions;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMChild_Versions(ArrayList interlockingOld, ArrayList interlockingNew, int imageOldIndex, int imageNewIndex, 
			float scaleF, string drawMde, ArrayList versionRecordOld, ArrayList versionRecordNew)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
						
			
			interlockingNewPointer = interlockingNew;			
			interlockingOldPointer = interlockingOld;

			//============================
			versionRecOld = versionRecordOld;
			versionRecNew = versionRecordNew;
			//============================

			ArrayList OldSortedList = SortVersionRecordsByDate(versionRecOld);
			ArrayList NewSortedList = SortVersionRecordsByDate(versionRecNew);

			for(int l=0; l < OldSortedList.Count; l++)	
			{				
				VersionRecord oldVPtr = (VersionRecord) OldSortedList[l];
                double Major_Minor = oldVPtr.majorVer;// + (((float) oldVPtr.minorVer)/10000);
                string verstring = "";
                verstring = oldVPtr.minorVer.ToString();
                if (oldVPtr.minorVer < 1000) verstring = "0" + verstring;
                if (oldVPtr.minorVer < 100) verstring = "0" + verstring;
                if (oldVPtr.minorVer < 10) verstring = "0" + verstring;

                this.OldVersions.Items.Add(oldVPtr.typeOfVersion);
				this.OldVersions.Items[l].SubItems.Add(oldVPtr.name_Person);
				//if(oldVPtr.minorVer == 0) this.OldVersions.Items[l].SubItems.Add(Major_Minor.ToString() + ".0000");
				//else
                this.OldVersions.Items[l].SubItems.Add(Major_Minor.ToString() + "." + verstring);
				this.OldVersions.Items[l].SubItems.Add(oldVPtr.date_time.ToString());
				if(!Find_in_list(OldSortedList, NewSortedList, l)) this.OldVersions.Items[l].ForeColor = Color.Blue;
			}	
			for(int l=0; l < NewSortedList.Count; l++)	
				if(!Find_in_list(NewSortedList, OldSortedList, l))
				{			
					VersionRecord newVPtr = (VersionRecord) NewSortedList[l];
                    double Major_Minor = newVPtr.majorVer;// + (((float) newVPtr.minorVer)/10000);
                    string verstring = "";
                    verstring = newVPtr.minorVer.ToString();
                    if (newVPtr.minorVer < 1000) verstring = "0" + verstring;
                    if (newVPtr.minorVer < 100) verstring = "0" + verstring;
                    if (newVPtr.minorVer < 10) verstring = "0" + verstring;

                    this.OldVersions.Items.Add(newVPtr.typeOfVersion);
					this.OldVersions.Items[l+OldSortedList.Count].SubItems.Add(newVPtr.name_Person);
					//if(newVPtr.minorVer == 0) this.OldVersions.Items[l+OldSortedList.Count].SubItems.Add(Major_Minor.ToString() + ".0000");
					//else 
                    this.OldVersions.Items[l+OldSortedList.Count].SubItems.Add(Major_Minor.ToString() + "." + verstring);
					this.OldVersions.Items[l+OldSortedList.Count].SubItems.Add(newVPtr.date_time.ToString());															
					this.OldVersions.Items[l+OldSortedList.Count].ForeColor = Color.Red;
				}
		}

		public ArrayList SortVersionRecordsByDate(ArrayList versRec)
		{
			ArrayList outVersRec = new ArrayList();
			ArrayList sortedlist = new ArrayList();
			for(int i = 0; i < versRec.Count; i++)
			{
				VersionRecord VPtr = (VersionRecord) versRec[i];
				DateTime dateAndTime = VPtr.date_time;
				sortedlist.Add(dateAndTime);
			}
			sortedlist.Sort();
			for(int i = 0; i < versRec.Count; i++)
				for(int j = 0; j < versRec.Count; j++)
				{
					VersionRecord VPtr = (VersionRecord) versRec[j];
					if(DateTime.Compare((DateTime) sortedlist[versRec.Count - i - 1],VPtr.date_time) == 0)
						outVersRec.Add(VPtr);
				}
			return outVersRec;
		}

		private bool Find_in_list(ArrayList OldSortedList, ArrayList NewSortedList, int l)
		{
			VersionRecord OldVPtr = (VersionRecord) OldSortedList[l];
			for(int i = 0; i < NewSortedList.Count; i++)
			{					
				VersionRecord NewVPtr = (VersionRecord) NewSortedList[i];
				if(CompareVersions(NewVPtr, OldVPtr)) return true;
			}
			return false;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMChild_Versions));
            this.treeView = new System.Windows.Forms.TreeView();
            this.Rungs = new System.Windows.Forms.ListView();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.OldVersions = new System.Windows.Forms.ListView();
            this.VersionNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nameOfPerson = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typeOfVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DateString = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Name_test = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VersionNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.NewVersions = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView.Location = new System.Drawing.Point(992, 216);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(16, 0);
            this.treeView.TabIndex = 0;
            this.treeView.Visible = false;
            // 
            // Rungs
            // 
            this.Rungs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Rungs.Location = new System.Drawing.Point(880, 16);
            this.Rungs.Name = "Rungs";
            this.Rungs.Size = new System.Drawing.Size(272, 0);
            this.Rungs.TabIndex = 1;
            this.Rungs.UseCompatibleStateImageBehavior = false;
            this.Rungs.View = System.Windows.Forms.View.List;
            this.Rungs.Visible = false;
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 439);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(984, 22);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "statusBar1";
            // 
            // OldVersions
            // 
            this.OldVersions.AllowColumnReorder = true;
            this.OldVersions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.OldVersions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.VersionNo,
            this.nameOfPerson,
            this.typeOfVersion,
            this.DateString});
            this.OldVersions.GridLines = true;
            this.OldVersions.Location = new System.Drawing.Point(8, 16);
            this.OldVersions.MultiSelect = false;
            this.OldVersions.Name = "OldVersions";
            this.OldVersions.Size = new System.Drawing.Size(480, 416);
            this.OldVersions.TabIndex = 3;
            this.OldVersions.UseCompatibleStateImageBehavior = false;
            this.OldVersions.View = System.Windows.Forms.View.Details;
            // 
            // VersionNo
            // 
            this.VersionNo.Text = "Type";
            this.VersionNo.Width = 70;
            // 
            // nameOfPerson
            // 
            this.nameOfPerson.Text = "Name";
            this.nameOfPerson.Width = 186;
            // 
            // typeOfVersion
            // 
            this.typeOfVersion.Text = "Version";
            this.typeOfVersion.Width = 58;
            // 
            // DateString
            // 
            this.DateString.Text = "Date";
            this.DateString.Width = 149;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            // 
            // Name_test
            // 
            this.Name_test.Text = "name";
            // 
            // Date
            // 
            this.Date.Text = "Date";
            this.Date.Width = 129;
            // 
            // VersionNumber
            // 
            this.VersionNumber.Text = "VersionNumber";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Versions";
            // 
            // NewVersions
            // 
            this.NewVersions.AllowColumnReorder = true;
            this.NewVersions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.NewVersions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.NewVersions.GridLines = true;
            this.NewVersions.Location = new System.Drawing.Point(496, 16);
            this.NewVersions.MultiSelect = false;
            this.NewVersions.Name = "NewVersions";
            this.NewVersions.Size = new System.Drawing.Size(480, 416);
            this.NewVersions.TabIndex = 5;
            this.NewVersions.UseCompatibleStateImageBehavior = false;
            this.NewVersions.View = System.Windows.Forms.View.Details;
            this.NewVersions.Visible = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Version";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 153;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            this.columnHeader3.Width = 107;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Date";
            this.columnHeader4.Width = 146;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(496, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "New File";
            this.label2.Visible = false;
            // 
            // frmMChild_Versions
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NewVersions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OldVersions);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.Rungs);
            this.Controls.Add(this.treeView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMChild_Versions";
            this.Text = "Version History";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

		}
		#endregion

		private void frmMChild_Load(object sender, System.EventArgs e)
		{				
		}

		private void frmMChild_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{		
			Pen BluePen = new Pen(Color.Blue);
			Pen RedPen = new Pen(Color.Red);
			Pen BlackPen = new Pen(Color.Black);						
			Pen myWhitePen = new Pen(Color.White);	
			SolidBrush BlueBrush = new SolidBrush(Color.Blue);
			SolidBrush RedBrush = new SolidBrush(Color.Red);
			SolidBrush CommonBrush = new SolidBrush(Color.Black);
			SolidBrush WhiteBrush = new SolidBrush(Color.White);
		}



		private bool CompareVersions(VersionRecord oldVerRecordPointer, VersionRecord newVerRecordPointer)
		{
			if(string.Compare(oldVerRecordPointer.name_Person, newVerRecordPointer.name_Person) != 0) return false;			
			if(string.Compare(oldVerRecordPointer.typeOfVersion, newVerRecordPointer.typeOfVersion) != 0) return false;
			if(oldVerRecordPointer.majorVer != newVerRecordPointer.majorVer) return false;
			if(oldVerRecordPointer.minorVer != newVerRecordPointer.minorVer) return false;
			if(DateTime.Compare(oldVerRecordPointer.date_time, newVerRecordPointer.date_time) != 0) return false;
			return true;
		}
	}

	public struct VersionRecord
	{		
		public int majorVer;
		public int minorVer;		
		public string name_Person;
		public DateTime date_time;
		public string typeOfVersion; //Modification, Check, Approval
	}
}
