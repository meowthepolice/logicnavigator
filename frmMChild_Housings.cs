using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace Logic_Navigator
{
	public class frmMChild_Housings : System.Windows.Forms.Form
	{	

		Graphics eGraphics;		
		public ArrayList HousingsNew;
		public ArrayList HousingsOld;
		
		public ArrayList InterlockingOld;
		public ArrayList InterlockingNew;
        private ArrayList timersOld;
        private ArrayList timersNew;	
					
		private float cellWidthStatic = 120; //Y
		private float cellHeightStatic = 90; //X
		
		private float scaleFactor = 1.0F;
		private Font drawFnt;		
		private Font drawFont = new Font("Tahoma", (float) 9);		
		private Font smallFont = new Font("Tahoma", (float) 7);
		private string drawMode = "";
		private Point mousePointer = new Point(0,0);
		private bool mouseSettled = true;
		private int refboxI = -1;
		private int refboxJ = -1;
		private bool showRef = false;
		private int optionChosen = -1;
		private int oldoptionChosen = -1;
		private int rungChosen = -1;
		private string rungName = "";
		private string highlightName = "";
		DateTime lastRefHover;
		private int tickCounter = 0;

		private int CardWidth = 50;//30;
		private int CardSize = 0;
		private ArrayList cardList;
		private int housingHeight = 100;//60;
		private int CardsPerHousing = 16;
		private int top = 30;
		private int left = 30;
		private int newTop = 30;
		private int newLeft = 30;
		
		private SolidBrush BlueBrush = new SolidBrush(Color.Blue);
		private SolidBrush RedBrush = new SolidBrush(Color.Red);
		private SolidBrush CommonBrush = new SolidBrush(Color.Black);
		private SolidBrush WhiteBrush = new SolidBrush(Color.White);
		private SolidBrush PurpleBrush = new SolidBrush(Color.Purple);
		
		private Pen BluePen = new Pen(Color.Blue);
		private Pen RedPen = new Pen(Color.Red);
		private Pen BlackPen = new Pen(Color.Black);						
		private Pen WhitePen = new Pen(Color.White);

		private int localoffsetX = 0;//146;
		private int localoffsetY = 0;//48;

		private SolidBrush HighlightBrush = new SolidBrush(Color.HotPink);
		private Pen HighlightPen = new Pen(Color.HotPink);		
		private Pen SelectedPen = new Pen(Color.HotPink);
		private	StringFormat drawFormat = new StringFormat();
		

		private int leftBound = 0; 
		private int rightBound = 0;
		private int topBound = 0; 
		private int bottomBound = 0;
		
		private int currentleftBound = 0; 
		private int currenttopBound = 0; 

		private int currentI = 0;
		private int currentJ = 0;
		private int highestI = 0;
		private int highestJ = 0;		
		private int minI = 0;
		private int minJ = 0;
		
		private int hitNumber = -1;

		//private System.Windows.Forms.Button button1;
        //private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBar statusBar2;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMChild_Housings));
            this.statusBar2 = new System.Windows.Forms.StatusBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // statusBar2
            // 
            this.statusBar2.Location = new System.Drawing.Point(0, 736);
            this.statusBar2.Name = "statusBar2";
            this.statusBar2.Size = new System.Drawing.Size(992, 22);
            this.statusBar2.TabIndex = 1;
            this.statusBar2.Text = "statusBar2";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmMChild_Housings
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(992, 758);
            this.Controls.Add(this.statusBar2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMChild_Housings";
            this.Text = "Housings";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMChild_Housings_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMChild_Housings_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMChild_Housings_MouseMove);
            this.ResumeLayout(false);

		}

        public frmMChild_Housings(ArrayList housingsOldPointer, ArrayList housingsNewPointer, ArrayList interlockingOldPointer, ArrayList interlockingNewPointer, ArrayList timersOldPointer, ArrayList timersNewPointer, Font drawFont, string drawMde)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			
			HousingsOld = housingsOldPointer;
			HousingsNew = housingsNewPointer;
			InterlockingOld = interlockingOldPointer;
            InterlockingNew = interlockingNewPointer;
            timersOld = timersOldPointer;
            timersNew = timersNewPointer;
			drawFnt = drawFont;	
			drawFormat.Alignment = StringAlignment.Center;			
			drawMode = drawMde;	

			//Magic Code that stops flickering in c#

			////////////////////////////////////////////////////
			SetStyle(ControlStyles.DoubleBuffer, true);

			SetStyle(ControlStyles.ResizeRedraw, true);

			SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			SetStyle(ControlStyles.UserPaint, true);
			////////////////////////////////////////////////////
		}

		private void frmMChild_Housings_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{	// Bug not here
			eGraphics = e.Graphics;
			if(string.Compare(drawMode, "Normal") == 0)
				drawHousings(e.Graphics);
			if(string.Compare(drawMode, "All New") == 0)
				drawHousings(e.Graphics);
			if(string.Compare(drawMode, "All Old") == 0)
				drawHousings(e.Graphics);
			if(hitNumber == 1) DrawLinks(cardList, e.Graphics, BlackPen, drawFont, HighlightBrush, Cursor.Position);
		}

		private void drawHousings(Graphics grfx)			
		{         //Bug not here
            try
            {
                SolidBrush GreenBrush = new SolidBrush(Color.Green);
                DrawHousingsOutline(grfx, BlackPen);
                for (int i = 0; i < HousingsNew.Count; i++)
                {
                    ArrayList housingPointer = (ArrayList)HousingsNew[i];
                    for (int j = 0; j < housingPointer.Count; j++)
                    {
                        ArrayList cardPointer = (ArrayList)housingPointer[j];
                        if (string.Compare(cardPointer[0].ToString(), "DIAG") != 0)
                            DrawCard(grfx, (int)cardPointer[2], i, j, RedPen, RedBrush, cardPointer[0].ToString());
                        else
                            DrawCard(grfx, 1, i, j, RedPen, RedBrush, cardPointer[0].ToString());
                    }
                }
                try
                {
                    DrawSlotNumbers(grfx, BlackPen);
                }
                catch { }
                try
                {
                    DrawHousingNumbers(grfx, BlackPen);
                }
                catch { }
            }
            catch
            {
               // MessageBox.Show("Logic Navigator experienced difficulties drawing the housing", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

		private void DrawSlotNumbers(Graphics grfx, Pen SlotPen)
		{
			for(int i = 15; i > 0; i--)
				grfx.DrawString(i.ToString(), smallFont, CommonBrush, 
					left + (int) ((0.3 + CardsPerHousing - i) * CardWidth), 
					top + newTop - 10);
		}
		
		private void DrawHousingNumbers(Graphics grfx, Pen HousingPen)
		{
			for(int i = 4; i > 0; i--)
				grfx.DrawString(i.ToString(), smallFont, CommonBrush, 
					left + newLeft,  (int) (top + newTop + ((i - 0.5) * (int) housingHeight)));
		}

		private void DrawCard(Graphics grfx, int slotNumber, int housingNumber, int j, Pen pen, Brush brush, string cardType)
		{			// Bug not here
			int CardSize = GetCardSize(cardType);
			int NotchPosition = CardSize - GetNotchPosition(cardType);
			SolidBrush CardBrush = new SolidBrush(Color.WhiteSmoke);			
			grfx.FillRectangle(CardBrush, 2 + left + (CardsPerHousing - (slotNumber + CardSize - 1)) * CardWidth, 
				2 + top + newTop + 10+(housingNumber*housingHeight), (CardWidth * CardSize)-4, housingHeight-4);	
			grfx.DrawRectangle(pen, 2 + left + (CardsPerHousing - (slotNumber + CardSize - 1)) * CardWidth, 
				2 + top + newTop + 10+(housingNumber*housingHeight), (CardWidth * CardSize)-4, housingHeight-4);				
			grfx.DrawLine(pen, (int) (-2 + left + (CardsPerHousing - (slotNumber + NotchPosition - 1)) * CardWidth),
				(int) (-2 + top + housingHeight + newTop + 10+(housingNumber*housingHeight)),
				(int) ( left + (-0.5 + CardsPerHousing - (slotNumber + NotchPosition - 1)) * CardWidth),
				(int) ( top + (0.9*housingHeight) + newTop + 10+(housingNumber*housingHeight)));				
			grfx.DrawLine(pen, (int) (2 + left + (-1 + CardsPerHousing - (slotNumber + NotchPosition - 1)) * CardWidth),
				(int) (-2 + top + housingHeight + newTop + 10+(housingNumber*housingHeight)),
				(int) (left + (-0.5 + CardsPerHousing - (slotNumber + NotchPosition - 1)) * CardWidth),
				(int) (top + (0.9*housingHeight) + newTop + 10+(housingNumber*housingHeight)));				
			grfx.DrawString(cardType + ", " + housingNumber.ToString() + ", " + j.ToString(), smallFont,
				brush, 2 + left + (CardsPerHousing - (slotNumber + CardSize - 1)) * CardWidth, 
				2 + top+housingNumber*housingHeight + newTop + 10);
		}

		private void DrawHousingsOutline(Graphics grfx, Pen pen)
		{				
			int CardSize = 1;
			SolidBrush HousingBrush = new SolidBrush(Color.LightGray);			
			for(int i=0; i<4; i++)
				for(int j=1; j<16; j++)
				{
					grfx.FillRectangle(HousingBrush, left + (CardsPerHousing - (j + CardSize - 1)) * CardWidth, 
						top + newTop + 10+(i*housingHeight), CardWidth * CardSize, housingHeight);	
					grfx.DrawRectangle(pen, left + (CardsPerHousing - (j + CardSize - 1)) * CardWidth, 
						top + newTop + 10+(i*housingHeight), CardWidth * CardSize, housingHeight);	
				}
		}

		private int GetNotchPosition(string CardType)
		{
			if(string.Compare(CardType,"EVTC") == 0) return 1;
			if(string.Compare(CardType,"HVLM128") == 0) return 2;
			if(string.Compare(CardType,"VLM6") == 0) return 2;
			if(string.Compare(CardType,"NVC422") == 0) return 1;
			if(string.Compare(CardType,"VTC232") == 0) return 1;
			if(string.Compare(CardType,"VROM50") == 0) return 2;
			if(string.Compare(CardType,"VPIM50") == 0) return 2;
			if(string.Compare(CardType,"VLOMFT110") == 0) return 2;
			if(string.Compare(CardType,"NCDM") == 0) return 1;
			if(string.Compare(CardType,"DIAG") == 0) return 1;
			else return 1;
		}

		private int GetCardSize(string CardType)
		{
			if(string.Compare(CardType,"EVTC") == 0) return 1;
			if(string.Compare(CardType,"HVLM128") == 0) return 2;
			if(string.Compare(CardType,"VLM6") == 0) return 2;
			if(string.Compare(CardType,"NVC422") == 0) return 1;
			if(string.Compare(CardType,"VTC232") == 0) return 1;
			if(string.Compare(CardType,"VROM50") == 0) return 2;
			if(string.Compare(CardType,"VPIM50") == 0) return 2;
			if(string.Compare(CardType,"VLOMFT110") == 0) return 3;
			if(string.Compare(CardType,"NCDM") == 0) return 1;				
			if(string.Compare(CardType,"DIAG") == 0) return 1;
			else return 1;


			/*
						VPIM50, VROM50, NVC/DM - 2
			VLOMFT110 - 3
			VTC232, EVTC232, WCM, DM, DM128, NCDM, NVC232, NVC422 - 1
			 */
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

		private void frmMChild_Housings_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            try
            {                              
                Graphics grfx = eGraphics;
                oldoptionChosen = optionChosen;
                if (optionChosen != refOptionChosen((int)e.X, (int)e.Y)) Invalidate();
                if (rungChosen != refOptionChosen((int)e.X, (int)e.Y)) Invalidate();
                optionChosen = refOptionChosen((int)e.X, (int)e.Y);                                
                if ((oldoptionChosen != -1) && (currentI != -1)) rungChosen = rungOptionChosen((int)e.X, (int)e.Y);                
                if (rungChosen != -1) optionChosen = oldoptionChosen; 
                if ((oldoptionChosen == -1) && (rungChosen == -1))
                {
                    currentI = -1; currentJ = -1;
                    for (int i = 0; i < HousingsNew.Count; i++)
                    {
                        ArrayList housingPointer = (ArrayList)HousingsNew[i];
                        for (int j = 0; j < housingPointer.Count; j++)
                        {
                            ArrayList cardPointer = (ArrayList)housingPointer[j];
                            if ((string.Compare(cardPointer[0].ToString(), "VPIM50") == 0) ||
                                (string.Compare(cardPointer[0].ToString(), "NVC422") == 0) ||
                                (string.Compare(cardPointer[0].ToString(), "VROM50") == 0) ||
                                (string.Compare(cardPointer[0].ToString(), "VLOMFT110") == 0))
                            {
                                    int CardSize = GetCardSize(cardPointer[0].ToString());
                                    int leftBoundCard = localoffsetX + left + (CardsPerHousing - ((int)cardPointer[2] + CardSize - 1)) * CardWidth;
                                    int rightBoundCard = localoffsetX + (left + (CardsPerHousing - ((int)cardPointer[2] + CardSize - 1)) * CardWidth) + CardWidth * CardSize;
                                    int topBoundCard = localoffsetY + top + newTop + 10 + (i * housingHeight);
                                    int bottomBoundCard = localoffsetY + top + newTop + 10 + (i * housingHeight) + housingHeight;

                                    if ((e.Y > topBoundCard) &&
                                        (e.Y < bottomBoundCard) &&
                                        (e.X > leftBoundCard) &&
                                        (e.X < rightBoundCard))
                                    {
                                        hitNumber = -1;
                                        if ((currentI != i) && (currentJ != j))
                                        {
                                            hitNumber = 1;
                                            cardList = cardPointer;
                                        }
                                        currentI = i;
                                        currentJ = j;
                                        if (highestI < currentI) highestI = currentI;
                                        if (highestJ < currentJ) highestJ = currentJ;
                                        if (minI > currentI) highestI = currentI;
                                        if (minJ > currentJ) highestJ = currentJ;
                                        showRef = true;
                                        currentleftBound = leftBoundCard;
                                        currenttopBound = topBoundCard;
                                        topBoundCard = localoffsetY + top + (4 * housingHeight) + 10 + (i * housingHeight);
                                        bottomBoundCard = localoffsetY + (top + (4 * housingHeight) + 10 + (i * housingHeight)) + housingHeight;
                                        leftBound = leftBoundCard;
                                        rightBound = rightBoundCard;
                                        topBound = topBoundCard;
                                        bottomBound = bottomBoundCard;					
                                        Invalidate();
                                }
                            }
                        }
                    }
                }            
            }
            catch 
            { 
                MessageBox.Show("Failure in Procedure: frmMChild_Housings_MouseMove", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);           
            }
		}

		private void DrawLinks(ArrayList cardPointer, Graphics grfx, Pen myPen, 
			Font drawFont, SolidBrush HighlightBrush, Point blueOffsetPaint)
		{
            try
            {
                SolidBrush GreenBrush = new SolidBrush(Color.Green);
                SolidBrush linksHighlighted = GreenBrush;
                Pen myGreyPen = new Pen(Color.Silver);
                SolidBrush LinkBoardBrush = new SolidBrush(Color.Cornsilk);
                if (showRef == true)
                {
                    DrawReference(cardPointer, "IO", 0, grfx, HighlightPen, drawFont, drawFormat, linksHighlighted, LinkBoardBrush, blueOffsetPaint);
                    try
                    {                        
                        if ((optionChosen != -1) && (currentI != -1)) DrawRungLinks(grfx);
                    }
                    catch { MessageBox.Show("optionChosen: " + optionChosen.ToString(), "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                }
            }
            catch { MessageBox.Show( "Failure in Procedure: DrawLinks", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
		}

		private void DrawRungLinks(Graphics grfx)
		{	// Bug not here					
			int hitNumber = 1; 
            try
            {	
            ArrayList housingPointer = (ArrayList) HousingsNew[currentI];           
	        ArrayList cardPointer = (ArrayList) housingPointer[currentJ];
			ArrayList IOPointer = (ArrayList) cardPointer[4];
			string optionName;

                if ((optionChosen < IOPointer.Count) && (optionChosen != -1))
                {
                    optionName = IOPointer[optionChosen].ToString();
                    if (optionName != "")
                    {
                        for (int r = 0; r < InterlockingNew.Count - 1; r++)
                        {
                            ArrayList rungPointer = (ArrayList)InterlockingNew[r];
                            if (string.Compare(rungPointer[rungPointer.Count - 1].ToString(), optionName) != 0)
                                for (int k = 1; k < rungPointer.Count - 1; k++)
                                {
                                    Contact contact = (Contact)rungPointer[k];
                                    if (string.Compare(optionName, contact.name) == 0)
                                        //if(linkSelected == hitNumber)										
                                        //{
                                        //linkName = rungPointer[rungPointer.Count-1].ToString();	

                                        DrawRungReference(rungPointer[rungPointer.Count - 1].ToString(), hitNumber++, grfx,
                                            HighlightPen, drawFont, drawFormat, HighlightBrush);
                                    //}
                                    //else 
                                    //	DrawRungReference(ctOldPointer, rungPointer[rungPointer.Count-1].ToString(), hitNumber++, grfx, 
                                    //		myGreyPen, drawFont, drawFormat, linksHighlighted, LinkBoardBrush, blueOffsetPaint);	
                                }
                        }
                    }
                }
            }
            catch { 
                MessageBox.Show("Failure in Module: DrawRungLinks, currentI & HousingsNew: " + currentI.ToString() + ", " + HousingsNew.Count.ToString(), "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
		}


		private void DrawRungReference(string rungName, int hitNumber, Graphics grfx, Pen myPen, 
			Font drawFont, StringFormat drawFormat, SolidBrush CommonRedBrush)
		{  //Bug not here
					float x = currentleftBound - localoffsetX - 30;
					float y = currenttopBound - localoffsetY;
					SolidBrush ShadowBrush = new SolidBrush(Color.Gray);	
					SolidBrush LinkBoardBrush = new SolidBrush(Color.Cornsilk);	
					grfx.FillRectangle(ShadowBrush, x + 1 + (cellWidthStatic * scaleFactor) + CardSize * CardWidth * scaleFactor, y + 1 + (optionChosen + hitNumber) * 18 * scaleFactor, (float) (cellWidthStatic * scaleFactor), (float) (18 * scaleFactor));
					grfx.FillRectangle(LinkBoardBrush, x + (cellWidthStatic * scaleFactor) + CardSize * CardWidth * scaleFactor, y + (optionChosen + hitNumber) * 18 * scaleFactor, cellWidthStatic * scaleFactor, 18 * scaleFactor);
					StringFormat refDrawFormat = new StringFormat();
					refDrawFormat.Alignment = StringAlignment.Near;	
					//grfx.DrawString(j.ToString() + ":", drawFont, CommonRedBrush, x + CardSize * CardWidth * scaleFactor, y + j * 18 * scaleFactor, refDrawFormat);
					//if(i == optionChosen) 
						grfx.DrawString(rungName, drawFont, PurpleBrush, x + (cellWidthStatic * scaleFactor) + CardSize * CardWidth * scaleFactor, y + (optionChosen + hitNumber) * 18 * scaleFactor, refDrawFormat);
					//else
					//	grfx.DrawString(rungName, drawFont, CommonRedBrush, x + 100 + 20 + CardSize * CardWidth * scaleFactor, y + hitNumber * 18 * scaleFactor, refDrawFormat);
		}
		

		private void DrawReference(ArrayList contactPointer, string rungName, int hitNumber, Graphics grfx, Pen myPen, 
			Font drawFont, StringFormat drawFormat, SolidBrush CommonRedBrush, SolidBrush myBrush, Point offset)
		{	//Bug not here		
			SolidBrush ShadowBrush = new SolidBrush(Color.Gray);			

			float x = currentleftBound - localoffsetX - 30;
			float y = currenttopBound - localoffsetY;
            
			if((currentI != -1) && (currentJ != -1))
			{
				ArrayList housingPointer = (ArrayList) HousingsNew[currentI];
				ArrayList cardPointer = (ArrayList) housingPointer[currentJ];	

				ArrayList IOPointer = (ArrayList) cardPointer[4];	
				int j = 0;
				CardSize = GetCardSize(cardPointer[0].ToString());
				for(int i=0; i < IOPointer.Count; i++)
				{
					j = i + 1;
					grfx.FillRectangle(ShadowBrush, x + 1 + CardSize * CardWidth * scaleFactor, y + 1 + j * 18 * scaleFactor, (float) (cellWidthStatic * scaleFactor), (float) (18 * scaleFactor));
					grfx.FillRectangle(myBrush, x + CardSize * CardWidth * scaleFactor, y + j * 18 * scaleFactor, cellWidthStatic * scaleFactor, 18 * scaleFactor);
					StringFormat refDrawFormat = new StringFormat();
					refDrawFormat.Alignment = StringAlignment.Near;	
					grfx.DrawString(j.ToString() + ":", drawFont, CommonRedBrush, x + CardSize * CardWidth * scaleFactor, y + j * 18 * scaleFactor, refDrawFormat);
					if(i == optionChosen) 
						grfx.DrawString(IOPointer[i].ToString(), drawFont, PurpleBrush, x + 20 + CardSize * CardWidth * scaleFactor, y + j * 18 * scaleFactor, refDrawFormat);
					else
						grfx.DrawString(IOPointer[i].ToString(), drawFont, CommonRedBrush, x + 20 + CardSize * CardWidth * scaleFactor, y + j * 18 * scaleFactor, refDrawFormat);
				}
				for(int i = IOPointer.Count; i < 12; i++)
				{
					j = i + 1;
					grfx.FillRectangle(ShadowBrush, x + 1 + CardSize * CardWidth * scaleFactor, y + 1 + j * 18 * scaleFactor,
						(float) (cellWidthStatic * scaleFactor), (float) (18 * scaleFactor));
					grfx.FillRectangle(myBrush,  x + CardSize * CardWidth * scaleFactor, y + j * 18 * scaleFactor,	cellWidthStatic * scaleFactor, 18 * scaleFactor);	
					StringFormat refDrawFormat = new StringFormat();
					refDrawFormat.Alignment = StringAlignment.Near;	
					grfx.DrawString(j.ToString() + ":" + "-", drawFont, CommonRedBrush, x + CardSize * CardWidth * scaleFactor, y + j * 18 * scaleFactor, refDrawFormat);
				}
			}
		}
		
		private int refOptionChosen(int mouseX, int mouseY)
		{ //Bug not here
			float x = currentleftBound - localoffsetX - 30;
			float y = currenttopBound - localoffsetY;
			mousePointer.X = mouseX;
			mousePointer.Y = mouseY;

            if ((currentI != -1) && (currentJ != -1))
            {
                ArrayList housingPointer = (ArrayList)HousingsNew[currentI];
                ArrayList cardPointer = (ArrayList)housingPointer[currentJ];
                if (cardPointer.Count > 4)
                {
                    ArrayList IOPointer = (ArrayList)cardPointer[4];
                    int j = 0;
                    if (currentJ > housingPointer.Count)
                        statusBar2.Text = "bug found";
                    CardSize = GetCardSize(cardPointer[0].ToString());
                    for (int i = 0; i < IOPointer.Count; i++)
                    {
                        j = i + 1;
                        if (mousePointer.X > x + CardSize * CardWidth * scaleFactor)
                            if (mousePointer.X <= x + CardSize * CardWidth * scaleFactor + (float)(cellWidthStatic * scaleFactor))
                                if (mousePointer.Y > y + j * 18 * scaleFactor)
                                    if (mousePointer.Y <= y + j * 18 * scaleFactor + (18 * scaleFactor))
                                        return (i);
                    }
                    for (int i = IOPointer.Count; i < 12; i++)
                    {
                        j = i + 1;
                        if ((mousePointer.X > x + CardSize * CardWidth * scaleFactor) &&
                           (mousePointer.X <= x + CardSize * CardWidth * scaleFactor + (float)(cellWidthStatic * scaleFactor)) &&
                            (mousePointer.Y > y + j * 18 * scaleFactor) &&
                            (mousePointer.Y <= y + j * 18 * scaleFactor + (18 * scaleFactor)))
                            return (i);
                    }
                }
            }
			return(-1);
		}

		
		private int rungOptionChosen(int mouseX, int mouseY)
		{	//Bug not here		
		
			float x = currentleftBound - localoffsetX - 30;
			float y = currenttopBound - localoffsetY;
			int hitNumber = 0;
            try
            {
                ArrayList housingPointer = (ArrayList)HousingsNew[currentI];
                ArrayList cardPointer = (ArrayList)housingPointer[currentJ];
                ArrayList IOPointer = (ArrayList)cardPointer[4];
                string optionName;
                if ((oldoptionChosen < IOPointer.Count) && (oldoptionChosen != -1))
                {
                    optionName = IOPointer[oldoptionChosen].ToString();//optionChosen
                    if (optionName != "")
                    {
                        for (int r = 0; r < InterlockingNew.Count - 1; r++)
                        {
                            ArrayList rungPointer = (ArrayList)InterlockingNew[r];
                            if (string.Compare(rungPointer[rungPointer.Count - 1].ToString(), optionName) != 0)
                                for (int k = 1; k < rungPointer.Count - 1; k++)
                                {
                                    Contact contact = (Contact)rungPointer[k];
                                    if (string.Compare(optionName, contact.name) == 0)
                                    {
                                        hitNumber++;
                                        //DrawRungReference(rungPointer[rungPointer.Count-1].ToString(), hitNumber++, grfx, 
                                        //	HighlightPen, drawFont, drawFormat, HighlightBrush);

                                        //j = i + 1;

                                        int mousex = mousePointer.X;
                                        float mousex1 = x + (cellWidthStatic * scaleFactor) + CardSize * CardWidth * scaleFactor;
                                        float mousex2 = x + (2 * cellWidthStatic * scaleFactor) + CardSize * CardWidth * scaleFactor;

                                        int mousey = mousePointer.Y;
                                        float mousey1 = y + (oldoptionChosen + hitNumber) * 18 * scaleFactor;
                                        float mousey2 = y + (oldoptionChosen + hitNumber + 1) * 18 * scaleFactor;
                                        //					if(mousePointer.X <= x + (1 + cellWidthStatic * scaleFactor) + CardSize * CardWidth * scaleFactor) 
                                        if (hitNumber == 1)
                                            statusBar2.Text = //"rungChosen: " + rungChosen.ToString() + ", optionChosen: " + optionChosen.ToString() + ", currentI" + currentI.ToString()
                                                //+ ", currentJ" + currentJ.ToString() + ", hitNumber " + hitNumber.ToString() 
                                                 ", mousex " + mousex.ToString() + ", mousex1 " + mousex1.ToString() + ", mousex2 " + mousex2.ToString() +
                                                ", mousey " + mousey.ToString() + ", mousey1 " + mousey1.ToString() + ", mousey2 " + mousey2.ToString();
                                        //		if(mousePointer.X > x + (-1 + cellWidthStatic * scaleFactor) + CardSize * CardWidth * scaleFactor) 
                                        //				if(mousePointer.X <= x + (1 + cellWidthStatic * scaleFactor) + CardSize * CardWidth * scaleFactor) 


                                        if (mousePointer.X > x + (cellWidthStatic * scaleFactor) + CardSize * CardWidth * scaleFactor)
                                            if (mousePointer.X <= x + (2 * cellWidthStatic * scaleFactor) + CardSize * CardWidth * scaleFactor)
                                                if (mousePointer.Y > y + (oldoptionChosen + hitNumber) * 18 * scaleFactor)
                                                    if (mousePointer.Y <= y + (oldoptionChosen + hitNumber + 1) * 18 * scaleFactor)
                                                    {
                                                        rungName = rungPointer[rungPointer.Count - 1].ToString();
                                                        highlightName = contact.name;
                                                        return (hitNumber);
                                                    }
                                    }
                                }
                        }
                    }
                }
            }

            catch
            {
                MessageBox.Show("Failure in Procedure: rungOptionChosen, currentI & HousingsNew: " + currentI.ToString() + ", " + 
                    HousingsNew.Count.ToString(), "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
		
			return(-1);
		}


		private void frmMChild_Housings_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if((currentI != -1) && (currentJ != -1))
			{
				if(rungChosen == -1)
				{
					ArrayList housingPointer = (ArrayList) HousingsNew[currentI];
					ArrayList cardPointer = (ArrayList) housingPointer[currentJ];	
					frmMChild_Card objfrmMChild = new frmMChild_Card(HousingsOld, HousingsNew, drawFont, "Normal", currentI, currentJ);	
					objfrmMChild.Size = new Size(700, 400);	
					objfrmMChild.Text = "Card";	
					objfrmMChild.MdiParent = this.MdiParent;
					objfrmMChild.Show();
				}
				else if(rungName != "")
						{
							int oldIndex = -1;
							int newIndex = -1;
							for(int i=0; i<InterlockingOld.Count; i++)
							{
								ArrayList rungPointer = (ArrayList) InterlockingOld[i];
								if(string.Compare((string) rungPointer[rungPointer.Count-1], rungName) == 0) oldIndex = i;						
							}		
							for(int j=0; j<InterlockingNew.Count; j++)
							{
								ArrayList rungPointer = (ArrayList) InterlockingNew[j];
								if(string.Compare((string) rungPointer[rungPointer.Count-1], rungName) == 0) newIndex = j;
							}			
							if((newIndex == -1)&&(oldIndex != -1)) LaunchChildWindow(InterlockingOld, InterlockingNew, oldIndex, oldIndex, 
									0.75F/*scaleFactor*/, "All Old", drawFnt, rungName);										
							if((oldIndex == -1)&&(newIndex != -1)) LaunchChildWindow(InterlockingOld, InterlockingNew, newIndex, newIndex, 
									0.75F/*scaleFactor*/, "All New", drawFnt, rungName);
							if((oldIndex != -1)&&(newIndex != -1)) LaunchChildWindow(InterlockingOld, InterlockingNew, oldIndex, newIndex, 
									0.75F/*scaleFactor*/, "Normal", drawFnt, rungName);
						}
			}
		}
		
		private void LaunchChildWindow(ArrayList interlockingOldPointer, ArrayList interlockingNewPointer, int oldIndex, int newIndex, 
					float scaleF, string drawMde, Font drawFt, string name)
		{
            frmMChild objfrmMChild = new frmMChild(interlockingOldPointer, interlockingNewPointer, timersOld, timersNew, oldIndex, newIndex,
                        scaleF, drawMde, drawFnt, highlightName, true, true, Color.Blue, Color.Red);//this.Text);
					objfrmMChild.Size = new Size(500, 300);	
					objfrmMChild.Text = name;	
					objfrmMChild.MdiParent = this.MdiParent;
					objfrmMChild.Show();	
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{  //Bug not here

			if((mousePointer.X == Cursor.Position.X) && (mousePointer.Y == Cursor.Position.Y))
			{				
				tickCounter++;
				if(tickCounter > 0) 
				{
					mouseSettled = true;// showRef = true;
				}
			}
			else 
			{
				if(hitNumber != 1)
				{
					mouseSettled = false; 
					tickCounter = 0;
				}
			}
			mousePointer = Cursor.Position;
			
			/////////////////////////////			
			if((refboxI != currentI) && (refboxJ != currentJ))// && (optionChosen != -1))
			{
				lastRefHover = DateTime.Now;				
			}
			DateTime targetTime = lastRefHover.AddMilliseconds(700);
			DateTime NowTime = DateTime.Now;

			if((DateTime.Compare(lastRefHover.AddMilliseconds(700), DateTime.Now) < 0))
				if((currentI == -1) && (currentJ == -1))//& (optionChosen != -1))
					if(showRef == true) 
					{
						showRef = false; 			
						Invalidate();
					}
			//if(optionChosen == -1)
				refboxI = currentI;
				refboxJ = currentJ;
            
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>

		#endregion


	}

}
