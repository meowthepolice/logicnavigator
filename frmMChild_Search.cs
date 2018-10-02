using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Globalization;


namespace Logic_Navigator
{
	public class frmMChild_Search : System.Windows.Forms.Form
	{	
		private string rungNameOld = "";
		private string rungNameNew = "";	
		private ArrayList interlockingNew;
		private ArrayList interlockingOld;
        private ArrayList timersOld;
        private ArrayList timersNew;	
		private System.Data.DataTable points;

		private SolidBrush HighlightBrush = new SolidBrush(Color.HotPink);
		private Pen HighlightPen = new Pen(Color.HotPink);
		private Font drawFnt;
		string rungName = "";
		private int dataRungCount = 0;
		private int currentCount = 0;
		private string currentRung = "";

		private ArrayList interlockingNewPointer;
		private ArrayList interlockingOldPointer;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DataGrid RungGrid;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox searchString1;

        Color HighColor, LowColor;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMChild_Search(ArrayList interlockingOldPointer, ArrayList interlockingNewPointer, ArrayList timersOldPointer, 
                        ArrayList timersNewPointer, Font drawFont, Color HighColorInput, Color LowColorInput)
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
			formatRungList();
            populateRungList("");
            HighColor = HighColorInput;
            LowColor = LowColorInput;

			drawFnt = drawFont;
			
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

		private void RungGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			DataGrid myGrid = (DataGrid) sender;

			System.Windows.Forms.DataGrid.HitTestInfo hti;
			hti = myGrid.HitTest(e.X, e.Y);
			string message = "You clicked ";

			switch (hti.Type) 
			{
				case System.Windows.Forms.DataGrid.HitTestType.None :
					message += "the background.";
					break;
				case System.Windows.Forms.DataGrid.HitTestType.Cell :
				{
					message += "cell at row " + hti.Row + ", col " + hti.Column;
                        //statusBar1.Text = message + ", " + RungGrid[hti.Row,2/*hti.Column*/].ToString();	
                    //if(hti.Row < RungGrid.)
					rungName = RungGrid[hti.Row,2].ToString();	
					//if(!reloading) 
					ShowRungWindow();	
					break;
				}
				case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader :
					message += "the column header for column " + hti.Column;
					break;
				case System.Windows.Forms.DataGrid.HitTestType.RowHeader :
					message += "the row header for row " + hti.Row;
					break;
				case System.Windows.Forms.DataGrid.HitTestType.ColumnResize :
					message += "the column resizer for column " + hti.Column;
					break;
				case System.Windows.Forms.DataGrid.HitTestType.RowResize :
					message += "the row resizer for row " + hti.Row;
					break;
				case System.Windows.Forms.DataGrid.HitTestType.Caption :
					message += "the caption";
					break;
				case System.Windows.Forms.DataGrid.HitTestType.ParentRows :
					message += "the parent row";
					break;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMChild_Search));
            this.searchString1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RungGrid = new System.Windows.Forms.DataGrid();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RungGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // searchString1
            // 
            this.searchString1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.searchString1.Location = new System.Drawing.Point(56, 8);
            this.searchString1.Name = "searchString1";
            this.searchString1.Size = new System.Drawing.Size(376, 20);
            this.searchString1.TabIndex = 15;
            this.searchString1.Text = "ENTER SEARCH STRING HERE";
            this.searchString1.TextChanged += new System.EventHandler(this.searchString1_TextChanged);
            this.searchString1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchString1_KeyDown);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Search:";
            // 
            // RungGrid
            // 
            this.RungGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RungGrid.CaptionVisible = false;
            this.RungGrid.DataMember = "";
            this.RungGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.RungGrid.Location = new System.Drawing.Point(0, 40);
            this.RungGrid.Name = "RungGrid";
            this.RungGrid.PreferredColumnWidth = 30;
            this.RungGrid.RowHeadersVisible = false;
            this.RungGrid.Size = new System.Drawing.Size(526, 685);
            this.RungGrid.TabIndex = 23;
            this.RungGrid.Navigate += new System.Windows.Forms.NavigateEventHandler(this.RungGrid_Navigate);
            this.RungGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RungGrid_KeyDown);
            this.RungGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RungGrid_MouseDown);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 719);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(520, 22);
            this.statusBar.TabIndex = 24;
            this.statusBar.Text = "statusBar";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(432, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 24);
            this.button1.TabIndex = 26;
            this.button1.Text = "Clear";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMChild_Search
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(520, 741);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.searchString1);
            this.Controls.Add(this.RungGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMChild_Search";
            this.Text = "Search";
            ((System.ComponentModel.ISupportInitialize)(this.RungGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void formatRungList()
		{
			
			/// Format the Rung Grid
			DataGridColumnStyle NameSeqStyle;			
			DataGridColumnStyle OldSeqStyle;		
			DataGridColumnStyle NewSeqStyle;		
			DataGridColumnStyle StatusSeqStyle;		
				
			DataGridTableStyle tableStyle = new DataGridTableStyle();
			tableStyle.MappingName = "Points";

			NameSeqStyle = new DataGridTextBoxColumn();
			NameSeqStyle.MappingName = "Name";
			NameSeqStyle.HeaderText = "Name";
			NameSeqStyle.Width = 130;

			OldSeqStyle = new DataGridTextBoxColumn();
			OldSeqStyle.MappingName = "Old";
			OldSeqStyle.HeaderText = "Old";
			OldSeqStyle.Width = 35;

			StatusSeqStyle = new DataGridTextBoxColumn();
			StatusSeqStyle.MappingName = "Status";
			StatusSeqStyle.HeaderText = "Status";
			StatusSeqStyle.Width = 75;

			//PropertyDescriptorCollection pcol; //= this.BindingContext[myDataSet, "Table1"].GetItemProperties();
			//pcol.Add(
			NewSeqStyle = new DataGridTextBoxColumn();
			//NewSeqStyle = new ColumnStyle();//pcol["Table1"]);
			NewSeqStyle.MappingName = "New";
			NewSeqStyle.HeaderText = "New";
			NewSeqStyle.Width = 35;			

			tableStyle.GridColumnStyles.Add(OldSeqStyle);		
			tableStyle.GridColumnStyles.Add(NewSeqStyle);
			tableStyle.GridColumnStyles.Add(NameSeqStyle);				
			tableStyle.GridColumnStyles.Add(StatusSeqStyle);	
			RungGrid.TableStyles.Add(tableStyle);			
			RungGrid.TableStyles["Points"].RowHeadersVisible = false;
		}

		private void populateRungList(string searchString)		
		{			
			string statusInfo = "";
			points = new System.Data.DataTable("Points");
			points.Columns.Add(new DataColumn("Key", typeof(int)));
			points.Columns.Add(new DataColumn("Old", typeof(string)));
			points.Columns.Add(new DataColumn("New", typeof(string)));
			points.Columns.Add(new DataColumn("Name", typeof(string)));
			points.Columns.Add(new DataColumn("Status", typeof(string)));

			int rungNumber = 0;
			dataRungCount = 0;
            string name = "";
            try
            {
                for (int i = 0; i < interlockingOld.Count - 1; i++)
                {
                    statusInfo = "";
                    ArrayList rungPointer = (ArrayList)interlockingOld[i];
                    name = (string)rungPointer[rungPointer.Count - 1];
                    string name_upper = name.ToUpper();
                    if (name_upper.LastIndexOf(searchString) != -1)
                    {
                        rungNumber = findRung(interlockingNew, (string)rungPointer[rungPointer.Count - 1]);
                        dataRungCount++;
                        if ((int)rungNumber != -1)
                        {                    
                            if (!RungsSame(rungPointer, (ArrayList)interlockingNew[rungNumber - 1])) //Same Name, but differing contacts
                                statusInfo = "Changed";//newNode.ForeColor = Color.Lime;
                            if (rungNumber != (int)rungPointer[0])
                                if (RungsSame(rungPointer, (ArrayList)interlockingNew[rungNumber - 1]))
                                    if ((int)rungNumber != -1) statusInfo = "";//"Moved";//newNode.ForeColor = Color.MediumSeaGreen;	
                            points.Rows.Add(new object[]{(int) i, rungPointer[0].ToString(), rungNumber.ToString(),
														    (string) rungPointer[rungPointer.Count-1], statusInfo.ToString()});
                        }
                        if ((int)rungNumber == -1) //rung removed, (rung not found in new data)
                        {
                            statusInfo = "Old"; //newNode.ForeColor = Color.Blue;
                            points.Rows.Add(new object[]{(int) i, rungPointer[0].ToString(), "-",
														    (string) rungPointer[rungPointer.Count-1], statusInfo.ToString()});
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error populating rung list (rung number:" + rungNumber.ToString() + ", " + name.ToString() + ")", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
            }

			for(int i=0; i<interlockingNew.Count-1; i++)
			{
				
				ArrayList rungPointer = (ArrayList) interlockingNew[i];		
				rungNumber = findRung(interlockingOld, (string) rungPointer[rungPointer.Count - 1]);					
				name = (string) rungPointer[rungPointer.Count - 1];				
				string name_upper = name.ToUpper();
				if(name_upper.LastIndexOf(searchString) != -1)
					if(rungNumber == -1)
					{
						points.Rows.Add(new object[]{ (int) i, "-", rungPointer[0].ToString(), (string) rungPointer[rungPointer.Count-1], "New"});				
						dataRungCount++;
					}
			}	
			RungGrid.DataSource = points;
		}

		private void searchString1_TextChanged(object sender, System.EventArgs e)
		{			
			string currentString = "";
			if(currentCount != -1) 
			{
				currentString = RungGrid[currentCount,2].ToString();
				RungGrid.UnSelect(currentCount);
			}
			string input = (string) searchString1.Text;
			points.Clear();
			populateRungList(input);
			statusBar.Text = dataRungCount.ToString();

			currentCount = -1;
			for(int i = 0; i < dataRungCount; i++)
				if(string.Compare(currentString, RungGrid[i,2].ToString()) != -1)
				{
					//RungGrid.Select(i);
					currentCount = i;
				}
			//if(currentCount == -1) 
				RungGrid.Select(0);
			//else RungGrid.Select(currentCount);
			
			//for(int i = 0; i < RungGrid.row)
		}


		private void ShowRungWindow()
		{			
			int newIndex = findRung(interlockingNew, rungName);
			int oldIndex = findRung(interlockingOld, rungName);
			if(newIndex == -1)
			{
                frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, oldIndex - 1,
                    oldIndex - 1, 0.75F, "All Old", drawFnt, "", false, true, HighColor, LowColor);
                objfrmMChild.Size = new Size(objfrmMChild.RecommendedWidthofWindow(oldIndex-1), objfrmMChild.RecommendedHeightofWindow(oldIndex-1));
                objfrmMChild.Location = new System.Drawing.Point(1, 1);
				objfrmMChild.Text = rungName;	
				objfrmMChild.MdiParent = this.MdiParent;
				objfrmMChild.Show();
			}
			else
			{
				if(oldIndex == -1)
				{
                    frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, newIndex - 1,
                        newIndex - 1, 0.75F, "All New", drawFnt, "", false, true, HighColor, LowColor);
                    objfrmMChild.Size = new Size(objfrmMChild.RecommendedWidthofWindow(newIndex-1), objfrmMChild.RecommendedHeightofWindow(newIndex-1));
                    objfrmMChild.Location = new System.Drawing.Point(1, 1);
					objfrmMChild.Text = rungName;	
					objfrmMChild.MdiParent = this.MdiParent;
					objfrmMChild.Show();
				}
				else
				{
                    frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, oldIndex - 1,
                        newIndex - 1, 0.75F, "Normal", drawFnt, "", false, true, HighColor, LowColor);
                    objfrmMChild.Size = new Size(objfrmMChild.RecommendedWidthofWindow(newIndex-1), objfrmMChild.RecommendedHeightofWindow(newIndex-1));
                    objfrmMChild.Location = new System.Drawing.Point(1, 1);
					objfrmMChild.Text = rungName;	
					objfrmMChild.MdiParent = this.MdiParent;
					objfrmMChild.Show();
				}
			}
		}

		private void searchString1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			statusBar.Text = currentCount.ToString();
			/*if((e.KeyCode == Keys.Down) )//|| (e.KeyCode == Keys.Tab))
			{
				RungGrid.UnSelect(currentCount);
				if(dataRungCount > currentCount)
                    RungGrid.Select(++currentCount);
			}						
			if(e.KeyCode == Keys.Up)
			{			
				RungGrid.UnSelect(currentCount);
				if(dataRungCount > currentCount)
					RungGrid.Select(--currentCount);			
			}

			if(e.KeyCode == Keys.Enter)
			{
				if(currentCount != -1)
				{
					rungName = RungGrid[currentCount,2].ToString();
					ShowRungWindow();
				}
			}*/
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			points.Clear();			
			populateRungList("");
		}

		private void RungGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{			
			if(e.KeyCode == Keys.Down)			
				currentCount++;			
			if(e.KeyCode == Keys.Up)			
				currentCount--;			
			if(e.KeyCode == Keys.Enter)
			{
				rungName = RungGrid[currentCount,2].ToString();
                if(rungName != "")
				    ShowRungWindow();
			}
		}

		private void RungGrid_Navigate(object sender, System.Windows.Forms.NavigateEventArgs ne)
		{
			statusBar.Text = ne.ToString();
		}
	}
}
