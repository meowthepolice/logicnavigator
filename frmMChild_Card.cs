using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace Logic_Navigator
{
	public class frmMChild_Card : System.Windows.Forms.Form
	{	

		public ArrayList HousingsNew;
		public ArrayList HousingsOld;
		
		private int housingNumber;
		private int cardNumber;
	
		private Font drawFnt;	
		private string drawMode = "";
			
		private Font drawFont = new Font("Tahoma", (float) 9);		
		private Font smallFont = new Font("Tahoma", (float) 7);

		

		private SolidBrush HighlightBrush = new SolidBrush(Color.HotPink);
		private Pen HighlightPen = new Pen(Color.HotPink);		
		private Pen SelectedPen = new Pen(Color.HotPink);
		
		//private System.Windows.Forms.Button button1;
		//private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.StatusBar statusBar2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.statusBar2 = new System.Windows.Forms.StatusBar();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Yellow;
			this.label1.Location = new System.Drawing.Point(8, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Form under construction";
			// 
			// statusBar2
			// 
			this.statusBar2.Location = new System.Drawing.Point(0, 736);
			this.statusBar2.Name = "statusBar2";
			this.statusBar2.Size = new System.Drawing.Size(992, 22);
			this.statusBar2.TabIndex = 1;
			this.statusBar2.Text = "statusBar2";
			// 
			// frmMChild_Card
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Beige;
			this.ClientSize = new System.Drawing.Size(992, 758);
			this.Controls.Add(this.statusBar2);
			this.Controls.Add(this.label1);
			this.Name = "frmMChild_Card";
			this.Text = "Card";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMChild_Housings_Paint);
			this.ResumeLayout(false);

		}

		public frmMChild_Card(ArrayList housingsOldPointer, ArrayList housingsNewPointer, Font drawFont, string drawMde, int HousingNumber, int CardNumber)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call

			housingNumber = HousingNumber;
			cardNumber = CardNumber;
			HousingsOld = housingsOldPointer;
			HousingsNew = housingsNewPointer;
			drawFnt = drawFont;	
			drawMode = drawMde;	

		}

		private void frmMChild_Housings_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Pen BluePen = new Pen(Color.Blue);
			Pen RedPen = new Pen(Color.Red);
			Pen BlackPen = new Pen(Color.Black);						
			Pen WhitePen = new Pen(Color.White);	
			SolidBrush BlueBrush = new SolidBrush(Color.Blue);
			SolidBrush RedBrush = new SolidBrush(Color.Red);
			SolidBrush CommonBrush = new SolidBrush(Color.Black);
			SolidBrush WhiteBrush = new SolidBrush(Color.White);
			if(string.Compare(drawMode, "Normal") == 0)
				drawCard(e.Graphics, BlueBrush, RedBrush, CommonBrush, WhiteBrush, BluePen, RedPen, BlackPen, WhitePen);
			if(string.Compare(drawMode, "All New") == 0)
				drawCard(e.Graphics, RedBrush, RedBrush, RedBrush, WhiteBrush, RedPen, RedPen, RedPen, WhitePen);
			if(string.Compare(drawMode, "All Old") == 0)
				drawCard(e.Graphics, BlueBrush, BlueBrush, BlueBrush, WhiteBrush, BluePen, BluePen, BluePen, WhitePen);			
		}

		private void drawCard(Graphics grfx, 
			SolidBrush BlueBrush,
			SolidBrush RedBrush,
			SolidBrush CommonBrush,
			SolidBrush WhiteBrush,
			Pen BluePen,
			Pen RedPen,
			Pen BlackPen,
			Pen WhitePen)			
		{			
			ArrayList housingPointer = (ArrayList) HousingsNew[housingNumber];
			ArrayList cardPointer = (ArrayList) housingPointer[cardNumber];	
			
			grfx.DrawString(cardPointer[0].ToString(), drawFont, RedBrush, 
				20, 20);
			grfx.DrawString(cardPointer[1].ToString() + ", " + cardPointer[2].ToString() + ", " + cardPointer[3].ToString() 
				+ ", " + cardNumber.ToString() + ", " + housingNumber.ToString(), drawFont, RedBrush, 120, 20);

			//grfx.DrawRectangle(redPen, 10, 40, 100, 20);

			ArrayList IOPointer = (ArrayList) cardPointer[4];	
			int j = 0;
			for(int i=0; i < IOPointer.Count; i++)
			{
				j = i + 1;
				grfx.DrawString(j.ToString() + ":", drawFont, RedBrush, 
					15, 40 + i * 20);
				grfx.DrawString(IOPointer[i].ToString(), drawFont, RedBrush, 
					35, 40 + i * 20);
				grfx.DrawRectangle(RedPen, 10, 40 + i * 20, 140, 20);
			}
			for(int i = IOPointer.Count; i < 12; i++)
			{
				j = i + 1;
				grfx.DrawString(j.ToString() + ":", drawFont, RedBrush, 
					15, 40 + i * 20);
				grfx.DrawString("-", drawFont, RedBrush, 
					35, 40 + i * 20);
				grfx.DrawRectangle(RedPen, 10, 40 + i * 20, 140, 20);
			}
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

		#endregion


	}

}
