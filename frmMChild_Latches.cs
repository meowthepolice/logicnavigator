using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Globalization;


namespace Logic_Navigator
{
	public class frmMChild_Latches : System.Windows.Forms.Form
	{	
		private ArrayList HousingsNew;
		private ArrayList HousingsOld;		
		private System.Data.DataTable points;

		private SolidBrush HighlightBrush = new SolidBrush(Color.HotPink);
		private Pen HighlightPen = new Pen(Color.HotPink);
		private Font drawFnt;
		private string rungName = "";
		
		private ArrayList interlockingNew;
        private ArrayList interlockingOld;
        private ArrayList timersOld;
        private ArrayList timersNew;	

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DataGrid RungGrid;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox searchString1;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public frmMChild_Latches(ArrayList Housings_Old, ArrayList Housings_New, ArrayList interlockingOldPointer, ArrayList interlockingNewPointer,
             ArrayList timersOldPointer, ArrayList timersNewPointer, Font drawFont)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			
			HousingsOld = Housings_Old;
			HousingsNew = Housings_New;
			interlockingNew = interlockingNewPointer;
			interlockingOld = interlockingOldPointer;
            timersOld = timersOldPointer;
            timersNew = timersNewPointer;
			formatRungList();
			populateRungList("");

			drawFnt = drawFont;
			
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
					//statusBar1.Text = message + ", " + RungGrid[hti.Row,2].ToString();	
					rungName = RungGrid[hti.Row,2].ToString();	
					//if(!reloading) 
					if(string.Compare(rungName,"") != 0)
                        ShowRungWindow();	
					break;
				}/*
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
					break;*/
			}
		}
/*
		public string GetRungName(string treeViewString)
		{
			return  treeViewString.Substring(treeViewString.LastIndexOf(":") + 2);
		}*/


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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMChild_Latches));
            this.searchString1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RungGrid = new System.Windows.Forms.DataGrid();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
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
            this.searchString1.Visible = false;
            this.searchString1.TextChanged += new System.EventHandler(this.searchString1_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Search:";
            this.label3.Visible = false;
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
            this.RungGrid.Size = new System.Drawing.Size(526, 726);
            this.RungGrid.TabIndex = 23;
            this.RungGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RungGrid_MouseDown);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 760);
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
            this.button1.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(264, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 16);
            this.label1.TabIndex = 27;
            this.label1.Text = "Form under construction";
            // 
            // frmMChild_Latches
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(520, 782);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.searchString1);
            this.Controls.Add(this.RungGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMChild_Latches";
            this.Text = "Latches";
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

			NewSeqStyle = new DataGridTextBoxColumn();
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
			points = new System.Data.DataTable("Points");
			points.Columns.Add(new DataColumn("Key", typeof(int)));
			points.Columns.Add(new DataColumn("Name", typeof(string)));
			points.Columns.Add(new DataColumn("Old", typeof(string)));
			points.Columns.Add(new DataColumn("New", typeof(string)));			
			points.Columns.Add(new DataColumn("Status", typeof(string)));

			for(int i=0; i<HousingsNew.Count-1; i++)
			{			
				ArrayList house = (ArrayList) HousingsNew[i];	
				for(int j=0; j<house.Count-1; j++)
				{					
					ArrayList card = (ArrayList) house[j];
					if(card.Count > 0)
						if((string.Compare(card[0].ToString(),"VLM6") == 0) || (string.Compare(card[0].ToString(),"HVLM128") == 0))
						{
							ArrayList latches = (ArrayList) card[6];
							for(int k=0; k<latches.Count-1; k++)
							{
								UserLatches latch = (UserLatches) latches[k];
								points.Rows.Add(new object[]{(int) k, latch.latchName.ToString(), latch.latchNumber.ToString(), FindLatchNumber(latch.latchName.ToString(),HousingsOld)});
								
							}
						}
				}
			}
			for(int i=0; i<HousingsOld.Count-1; i++)
			{			
				ArrayList house = (ArrayList) HousingsOld[i];	
				for(int j=0; j<house.Count-1; j++)
				{					
					ArrayList card = (ArrayList) house[j];
					if(card.Count > 0)
						if((string.Compare(card[0].ToString(),"VLM6") == 0) || (string.Compare(card[0].ToString(),"HVLM128") == 0))
						{
							ArrayList latches = (ArrayList) card[6];
							for(int k=0; k<latches.Count-1; k++)
							{
								UserLatches latch = (UserLatches) latches[k];
								if(string.Compare(FindLatchNumber(latch.latchName.ToString(),HousingsNew),"-") == 0)
									points.Rows.Add(new object[]{(int) k, latch.latchName.ToString(), "-", latch.latchNumber.ToString()});
							}
						}
				}
			}
			/*	string name = (string) rungPointer[rungPointer.Count - 1];
				string name_upper = name.ToUpper();
				if(name_upper.LastIndexOf(searchString) != -1)
				{					
					rungNumber = findRung(interlockingNew, (string) rungPointer[rungPointer.Count - 1]);
					dataRungCount++;
					if((int) rungNumber != -1)
					{						
						if(!RungsSame(rungPointer, (ArrayList) interlockingNew[rungNumber-1])) //Same Name, but differing contacts
							statusInfo = "Changed";//newNode.ForeColor = Color.Lime;
						if(rungNumber != (int) rungPointer[0])
							if(RungsSame(rungPointer, (ArrayList) interlockingNew[rungNumber-1]))
								if((int) rungNumber != -1) statusInfo = "";//"Moved";//newNode.ForeColor = Color.MediumSeaGreen;	
						points.Rows.Add(new object[]{(int) i, rungPointer[0].ToString(), rungNumber.ToString(),
														(string) rungPointer[rungPointer.Count-1], statusInfo.ToString()});
					}
					if((int) rungNumber == -1) //rung removed, (rung not found in new data)
					{						
						statusInfo = "Old"; //newNode.ForeColor = Color.Blue;
						points.Rows.Add(new object[]{(int) i, rungPointer[0].ToString(), "-",
														(string) rungPointer[rungPointer.Count-1], statusInfo.ToString()});
					}
				}
			}
			for(int i=0; i<interlockingNew.Count-1; i++)
			{
				
				ArrayList rungPointer = (ArrayList) interlockingNew[i];		
				rungNumber = findRung(interlockingOld, (string) rungPointer[rungPointer.Count - 1]);					
				string name = (string) rungPointer[rungPointer.Count - 1];				
				string name_upper = name.ToUpper();
				if(name_upper.LastIndexOf(searchString) != -1)
					if(rungNumber == -1)
					{
						points.Rows.Add(new object[]{ (int) i, "-", rungPointer[0].ToString(), (string) rungPointer[rungPointer.Count-1], "New"});				
						dataRungCount++;
					}
			}	*/
			RungGrid.DataSource = points;
		}

		private string FindLatchNumber(string newLatchName, ArrayList Housings)
		{
			for(int i=0; i<Housings.Count-1; i++)
			{			
				ArrayList house = (ArrayList) Housings[i];	
				for(int j=0; j<house.Count-1; j++)
				{					
					ArrayList card = (ArrayList) house[j];
					if(card.Count > 0)
						if((string.Compare(card[0].ToString(),"VLM6") == 0) || (string.Compare(card[0].ToString(),"HVLM128") == 0))
						{
							ArrayList latches = (ArrayList) card[6];
							for(int k=0; k<latches.Count-1; k++)
							{
								UserLatches latch = (UserLatches) latches[k];
								if(string.Compare(latch.latchName.ToString(),newLatchName) == 0) return(latch.latchNumber.ToString());
							}
						}
				}
			}
			return("-");
		}

		private void ShowRungWindow()
		{			
			int newIndex = findRung(interlockingNew, rungName);
			int oldIndex = findRung(interlockingOld, rungName);
			if(newIndex == -1)
			{		
				if(oldIndex != -1)
				{
                    frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, oldIndex - 1,
                        oldIndex - 1, 0.75F, "All Old", drawFnt, "", true, true, Color.Blue, Color.Red);
                    objfrmMChild.Size = new Size(objfrmMChild.RecommendedWidthofWindow(oldIndex-1), objfrmMChild.RecommendedHeightofWindow(oldIndex-1));
                    objfrmMChild.Location = new System.Drawing.Point(1, 1);
					objfrmMChild.Text = rungName;	
					objfrmMChild.MdiParent = this.MdiParent;
					objfrmMChild.Show();		
				}
			}
			else
			{
				if(oldIndex == -1)
				{
                    frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, newIndex - 1,
                        newIndex - 1, 0.75F, "All New", drawFnt, "", true, true, Color.Blue, Color.Red);
                    objfrmMChild.Size = new Size(objfrmMChild.RecommendedWidthofWindow(newIndex-1), objfrmMChild.RecommendedHeightofWindow(newIndex-1));
                    objfrmMChild.Location = new System.Drawing.Point(1, 1);
					objfrmMChild.Text = rungName;	
					objfrmMChild.MdiParent = this.MdiParent;
					objfrmMChild.Show();
				}
				else
				{
                    frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, oldIndex - 1,
                        newIndex - 1, 0.75F, "Normal", drawFnt, "", true, true, Color.Blue, Color.Red);
					objfrmMChild.Size = new Size(700, 400);
                    objfrmMChild.Location = new System.Drawing.Point(1, 1);
					objfrmMChild.Text = rungName;	
					objfrmMChild.MdiParent = this.MdiParent;
					objfrmMChild.Show();
				}
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

		private void searchString1_TextChanged(object sender, System.EventArgs e)
		{	/*		
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
			
			//for(int i = 0; i < RungGrid.row)*/
		}
/*

		private void ShowRungWindow()
		{			
			int newIndex = findRung(interlockingNew, rungName);
			int oldIndex = findRung(interlockingOld, rungName);
			if(newIndex == -1)
			{		
				frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, oldIndex - 1, 
					oldIndex - 1, 0.75F, "All Old", drawFnt, "");
				objfrmMChild.Size = new Size(700, 400);	
				objfrmMChild.Text = rungName;	
				objfrmMChild.MdiParent = this.MdiParent;
				objfrmMChild.Show();
			}
			else
			{
				if(oldIndex == -1)
				{
					frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, newIndex - 1, 
						newIndex - 1, 0.75F, "All New", drawFnt, "");
					objfrmMChild.Size = new Size(700, 400);	
					objfrmMChild.Text = rungName;	
					objfrmMChild.MdiParent = this.MdiParent;
					objfrmMChild.Show();
				}
				else
				{	
					frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, oldIndex - 1, 
						newIndex - 1 , 0.75F, "Normal", drawFnt, "");
					objfrmMChild.Size = new Size(700, 400);	
					objfrmMChild.Text = rungName;	
					objfrmMChild.MdiParent = this.MdiParent;
					objfrmMChild.Show();
				}
			}
		}

		private void searchString1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			statusBar.Text = currentCount.ToString();
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
				ShowRungWindow();
			}
		}

		private void RungGrid_Navigate(object sender, System.Windows.Forms.NavigateEventArgs ne)
		{
			statusBar.Text = ne.ToString();
		}*/


	}
}
