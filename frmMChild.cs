using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace Logic_Navigator
{
    public class frmMChild : System.Windows.Forms.Form
    {
        Graphics eGraphics;
        public float scaleFactor = 1;
        private DateTime LoadTime;
        private DateTime PreviewRequestTime;
        private float endScaleFactor = 1;
        private float scaleFactorClick;
        public int upcontacts = 0;        
        int animate = 0;

        private Point unscaledlocation = new Point(0, 0);

        private float cellWidthStatic = 120; //Y
        private float cellHeightStatic = 90; //X
        private int cellWidth; //Y
        private int cellHeight; //X
        private float snapDistX;
        private float snapDistY;

        private Point nameLocation = new Point(0, 5);
        private Point contactLocation = new Point(40, 10);

        private Point redOffset = new Point(0, 0);
        private Point blueOffset = new Point(0, 0);
        private Point OldRedOffset = new Point(0, 0);
        private Point OldBlueOffset = new Point(0, 0);
        private Point redEntrance = new Point(0, 0);
        private Point blueEntrance = new Point(0, 0);
        private Point previewEntrance = new Point(0, 0);

        private Point gridOffset = new Point(0, 0);
        private bool grid = true;
        private bool showtimers = true;

        private Point scaleOffset = new Point(0, 0);
        private Point click = new Point(0, 0);
        private Point scaleClick = new Point(0, 0);

        private Point CellCoord = new Point(0, 0);
        private Point LinkCoord = new Point(0, 0);
        private Point CellCoordPrev = new Point(0, 0);

        private string drawMode = "";
        private Point linksPoint = new Point(0, 0);
        private int linkSelected = 0;
        private int oldLinkSelected = 0;
        private DateTime lastLinkHover = new DateTime(0);
        private string linkName = "";
        private string contactHighlight = "";
        private string previewRungName = "";
        public Font drawFnt;
        private Font smallFont;
        private Font boldFont;
        private Font italicsFont;
        private Font underlineFont;
        public string inputToggle = "";

        Pen myGreyPen = new Pen(Color.Silver);
        public ArrayList SeqList = new ArrayList();
        public int seqcounter = 0;

        Pen BluePeng = new Pen(Color.Blue);
        Pen RedPeng = new Pen(Color.Red);
        Pen GreenPeng = new Pen(Color.Green);
        Pen GreyPeng = new Pen(Color.Gray);
        Pen BlackPeng = new Pen(Color.Black);
        Pen GoldPen = new Pen(Color.Gold);
        //Pen SimPenUp = new Pen(Color.Red);
        //Pen SimPenDn = new Pen(Color.DeepSkyBlue);        
        Pen SimPenUp = new Pen(Color.DeepSkyBlue);
        Pen SimPenDn = new Pen(Color.Red);
        Pen SimPenFlow = new Pen(Color.LightGreen, 3);
        Pen SimPenNoFlow = new Pen(Color.Pink, 4);
        Pen myWhitePeng = new Pen(Color.White);
        SolidBrush BlueBrushg = new SolidBrush(Color.Blue);
        SolidBrush RedBrushg = new SolidBrush(Color.Red);
        SolidBrush CommonBrushg = new SolidBrush(Color.Black);
        SolidBrush WhiteBrushg = new SolidBrush(Color.White);
        SolidBrush SimBrush = new SolidBrush(Color.Red);
        SolidBrush SimBrushUp = new SolidBrush(Color.DeepSkyBlue);
        SolidBrush SimBrushDown = new SolidBrush(Color.Red);

        ///Open new window////////////////////////////////////
        int oldindexTransition = 0;
        int newindexTransition = 0;
        string windowTypeTransition = "";
        string rungNameTransition = "";
        private DateTime LoadTimeTransition;
        /////////////////////////////////       

        private bool rightMouseDown = false;
        private bool leftMouseDown = false;
        private bool middleMouseDown = false;
        private bool showLinks = false;
        private string CellName = "";
        private string OldCellName = "";

        //Preview variables
        public int CurrentOldCell;
        public int CurrentNewCell;
        private bool preview = false;
        private bool previewTimed = true;
        private bool showingPreview = false;
        private bool showgridlines = true;
        private int tempNewCell;
        private int tempOldCell;
        private float tempScaleFactor;


        private SolidBrush HighlightBrush = new SolidBrush(Color.HotPink);
        private Pen HighlightPen = new Pen(Color.HotPink);
        private Pen contactHighlightPen = new Pen(Color.Blue);
        private SolidBrush contactHighlightBrush = new SolidBrush(Color.Yellow);

        public ArrayList interlockingNewPointer;
        public ArrayList interlockingOldPointer;
        private ArrayList timersOld;
        private ArrayList timersNew;
        public ArrayList trueInterlockingNewPointer;
        public ArrayList trueInterlockingOldPointer;

        public ArrayList SimInputs = new ArrayList();
        public ArrayList SimRungs = new ArrayList();
        public ArrayList SimS2DTimers = new ArrayList();
        public ArrayList SimS2PTimers = new ArrayList();

        Color HighColor, LowColor;

        public bool SimMode = false;

        public int ScrollCommand = 0;

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ListView Rungs;
        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private Timer timer4;
        private Timer timer5;
        private ContextMenuStrip contextMenuStrip1;
        private System.ComponentModel.IContainer components;

        public frmMChild(ArrayList interlockingOld, ArrayList interlockingNew, ArrayList timersOldPointer, ArrayList timersNewPointer, int imageOldIndex, int imageNewIndex,
            float scaleF, string drawMde, Font drawFt, string highlightContact, bool gridlines, bool showTimers, Color HighColorInput, Color LowColorInput)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            this.MouseWheel += pictureBox1_MouseWheel;

            //
            // TODO: Add any constructor code after InitializeComponent call

            //scaleFactor = scaleF;	

            endScaleFactor = scaleF;
            scaleFactor = 0.1f;
            tempScaleFactor = scaleFactor;

            timer2.Enabled = true;
            LoadTime = DateTime.Now;

            HighColor = HighColorInput;
            LowColor = LowColorInput;


            SimPenUp = new Pen(HighColor);
            SimPenDn = new Pen(LowColor);

            SimBrushUp = new SolidBrush(HighColor);
            SimBrushDown = new SolidBrush(LowColor);
            LowColor = LowColorInput;

            grid = gridlines;
            showtimers = showTimers;
            drawMode = drawMde;
            drawFnt = drawFt;
            smallFont = new Font(drawFnt.Name, 8 * scaleFactor, drawFnt.Style);
            underlineFont = new Font(drawFnt.Name, drawFnt.Size, drawFnt.Style | FontStyle.Underline);

            if (drawMde == "Normal")
            {
                interlockingNewPointer = interlockingNew;
                interlockingOldPointer = interlockingOld;
                timersOld = timersOldPointer;
                timersNew = timersNewPointer;
            }
            if (drawMde == "All New")
            {
                interlockingNewPointer = interlockingNew;
                interlockingOldPointer = interlockingNew;
                timersOld = timersOldPointer;
                timersNew = timersNewPointer;
            }
            if (drawMde == "All Old")
            {
                interlockingNewPointer = interlockingOld;
                interlockingOldPointer = interlockingOld;
                timersOld = timersOldPointer;
                timersNew = timersNewPointer;
            }
            CurrentOldCell = imageOldIndex;
            CurrentNewCell = imageNewIndex;

            trueInterlockingNewPointer = interlockingNew;
            trueInterlockingOldPointer = interlockingOld;


        }


        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (((Control.ModifierKeys & Keys.Control) != 0) &&
                ((Control.ModifierKeys & Keys.Shift) == 0))
            {
                if (e.Delta > 0)
                    applyzoom(1.25f, unscaledlocation.X, unscaledlocation.Y);// mouselocation.X, mouselocation.Y);
                else
                    applyzoom(0.8f, unscaledlocation.X, unscaledlocation.Y);// mouselocation.X, mouselocation.Y);
            }

            if (((Control.ModifierKeys & Keys.Control) != 0) &&
                ((Control.ModifierKeys & Keys.Shift) != 0))
            {
                if (e.Delta > 0)
                    applyzoom(1.05f, unscaledlocation.X, unscaledlocation.Y);
                else
                    applyzoom(1 / 1.05f, unscaledlocation.X, unscaledlocation.Y);
            }

            /*if ((Control.ModifierKeys & Keys.Shift) != 0)
            {
                if (e.Delta > 0)
                    totalpan.X += 10;
                else
                    totalpan.X -= 10;
                pan.Y = totalpan.X;
            }
            if (((Control.ModifierKeys & Keys.Control) == 0) &&
                (Control.ModifierKeys & Keys.Shift) == 0)
            {
                if (e.Delta > 0)
                    totalpan.Y += 10;
                else
                    totalpan.Y -= 10;
                pan.Y = totalpan.Y;
            }*/
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMChild));
            this.treeView = new System.Windows.Forms.TreeView();
            this.Rungs = new System.Windows.Forms.ListView();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.timer5 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
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
            this.Rungs.Size = new System.Drawing.Size(88, 0);
            this.Rungs.TabIndex = 1;
            this.Rungs.UseCompatibleStateImageBehavior = false;
            this.Rungs.View = System.Windows.Forms.View.List;
            this.Rungs.Visible = false;
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 439);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(800, 22);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "Logic Navigator";
            this.statusBar.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 15;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 15;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer4
            // 
            this.timer4.Interval = 15;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // timer5
            // 
            this.timer5.Enabled = true;
            this.timer5.Interval = 250;
            this.timer5.Tick += new System.EventHandler(this.timer5_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // frmMChild
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(800, 461);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.Rungs);
            this.Controls.Add(this.treeView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMChild";
            this.Text = "Rung";
            this.Load += new System.EventHandler(this.frmMChild_Load);
            this.Click += new System.EventHandler(this.frmMChild_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMChild_Paint);
            this.DoubleClick += new System.EventHandler(this.frmMChild_DoubleClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMChild_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMChild_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMChild_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmMChild_MouseUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.frmMDIMain_MouseWheel);
            this.ResumeLayout(false);

        }
        #endregion

        private void frmMChild_Load(object sender, System.EventArgs e)
        {
            // Magic code that allows fast screen drawing
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            CompileSequenceList();
        }

        private void frmMChild_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            eGraphics = e.Graphics;
            paintRungs();
        }

        private void paintRungs()
        {
            Point reddiff = new Point(redOffset.X + redEntrance.X, redOffset.Y + redEntrance.Y);
            Point bluediff = new Point(blueOffset.X + redEntrance.X, blueOffset.Y + redEntrance.Y);

            Pen BluePen = new Pen(Color.Blue);
            Pen RedPen = new Pen(Color.Red);
            Pen BlackPen = new Pen(Color.Black);
            Pen myWhitePen = new Pen(Color.White);
            SolidBrush BlueBrush = new SolidBrush(Color.Blue);
            SolidBrush RedBrush = new SolidBrush(Color.Red);
            SolidBrush CommonBrush = new SolidBrush(Color.Black);
            SolidBrush WhiteBrush = new SolidBrush(Color.White);
            if (ScrollCommand == -120)
            {
                statusBar.Text = ScrollCommand.ToString();
                ScrollCommand = 0;
                if (interlockingNewPointer.Count - 1 > CurrentNewCell)
                {
                    CurrentNewCell++;
                    ArrayList rungNewPointer = (ArrayList)interlockingNewPointer[CurrentNewCell - 1];
                    string contactname = (string)rungNewPointer[rungNewPointer.Count - 1];
                    if (findRung(interlockingOldPointer, contactname) != -1)
                        CurrentOldCell = findRung(interlockingOldPointer, contactname);
                }
            }
            if (ScrollCommand == 120)
            {
                statusBar.Text = ScrollCommand.ToString();
                ScrollCommand = 0;
                if (CurrentNewCell > 0)
                {
                    CurrentNewCell--;
                    ArrayList rungNewPointer = (ArrayList)interlockingNewPointer[CurrentNewCell];
                    string contactname = (string)rungNewPointer[rungNewPointer.Count - 1];
                    if (findRung(interlockingOldPointer, contactname) != -1)
                        CurrentOldCell = findRung(interlockingOldPointer, contactname) - 1;
                }
            }
            if (SimMode)
                this.BackColor = Color.MintCream;
            else this.BackColor = Color.White;

            if (drawMode == "Normal")
                drawRungs(eGraphics, BlueBrush, RedBrush, CommonBrush, WhiteBrush, BluePen, RedPen, BlackPen, myWhitePen, reddiff /*redOffset*/, bluediff /*blueOffset*/);
            if (drawMode == "All New")
                drawRungs(eGraphics, RedBrush, RedBrush, RedBrush, WhiteBrush, RedPen, RedPen, RedPen, myWhitePen, reddiff /*redOffset*/, bluediff /*blueOffset*/);
            if (drawMode == "All Old")
                drawRungs(eGraphics, BlueBrush, BlueBrush, BlueBrush, WhiteBrush, BluePen, BluePen, BluePen, myWhitePen, reddiff /*redOffset*/, bluediff /*blueOffset*/);
            if ((preview && previewTimed) && (findRung(trueInterlockingNewPointer, previewRungName) != -1) && (findRung(trueInterlockingOldPointer, previewRungName) != -1))
            {// Draw a preview of 'previewRungName'
             //statusBar.Text = "Drawing preview of: " + previewRungName;
                showingPreview = true;
                tempNewCell = CurrentNewCell;
                tempOldCell = CurrentOldCell;
                tempScaleFactor = scaleFactor;
                //scaleFactor = scaleFactor;// *0.8f;				
                CurrentNewCell = findRung(trueInterlockingNewPointer, previewRungName) - 1;
                CurrentOldCell = findRung(trueInterlockingOldPointer, previewRungName) - 1;
                int previewOffset = getHeight(trueInterlockingNewPointer, this.Text.ToString());
                if (previewOffset < getHeight(trueInterlockingOldPointer, this.Text.ToString())) previewOffset = getHeight(trueInterlockingOldPointer, this.Text.ToString());
                Point previewRung = new Point(15 + (int)((redOffset.X + (previewOffset * cellHeightStatic)) * tempScaleFactor / scaleFactor),
                                             -10 + (int)((redOffset.Y + 10) * tempScaleFactor / scaleFactor) + previewEntrance.Y);
                showgridlines = false;
                //statusBar.Text = scaleFactor.ToString() + ", " + previewOffset.ToString() + ", " + redOffset.X.ToString();
                drawMat(eGraphics, previewRung, previewRungName);   // Draw a Post it note for the preview
                drawRungs(eGraphics, BlueBrush, RedBrush, CommonBrush, WhiteBrush, BluePen, RedPen, BlackPen, myWhitePen, previewRung, previewRung);
                showgridlines = true;
                scaleFactor = tempScaleFactor;
                CurrentNewCell = tempNewCell;
                CurrentOldCell = tempOldCell;
                showingPreview = false;
            }
        }

        private void CompileSequenceList()
        {
            SeqList.Add("");
            ArrayList rungPointer3 = (ArrayList)interlockingNewPointer[0];
            for (int r = 0; r < interlockingNewPointer.Count; r++)
            {
                ArrayList rungPointer2 = (ArrayList)interlockingNewPointer[r];
                if (rungPointer2[rungPointer2.Count - 1].ToString() == this.Text.ToString())
                    rungPointer3 = (ArrayList)rungPointer2;
            }
            for (int i = 0; i < interlockingNewPointer.Count; i++)
            {
                ArrayList rungPointer = (ArrayList)interlockingNewPointer[i];
                string rungname = rungPointer[rungPointer.Count - 1].ToString();
                for (int k = 1; k < rungPointer3.Count - 1; k++)
                {
                    Contact contact = (Contact)rungPointer3[k];
                    if (rungname == contact.name)
                        SeqList.Add(rungname);
                }
            }
        }

        private int findRung(ArrayList interlocking, string rungName)
        {
            int rungNumber = -1;
            if (rungName == "")
                return -1;
            for (int i = 0; i < interlocking.Count; i++)
            {
                ArrayList rungPointer = (ArrayList)interlocking[i];
                if ((string)rungPointer[rungPointer.Count - 1] == rungName)
                    rungNumber = (int)rungPointer[0];
            }
            return rungNumber;
        }

        private int findTimer(ArrayList timer, string name)
        {
            if (name == "")
                return -1;
            for (int i = 0; i < timer.Count; i++)
            {
                ML2Timer timerElement = (ML2Timer)timer[i];
                if ((string)timerElement.timerName == name)
                    return i;
            }
            return -1;
        }

        private int getHeight(ArrayList interlocking, string rungName)
        {
            int maxHeight = -1;
            if (rungName == "")
                return -1;
            for (int i = 0; i < interlocking.Count; i++)
            {
                ArrayList rungPointer = (ArrayList)interlocking[i];
                if ((string)rungPointer[rungPointer.Count - 1] == rungName)
                {
                    for (int j = 1; j < rungPointer.Count - 1; j++)
                    {
                        Contact contactPointer = (Contact)rungPointer[j];
                        if (maxHeight < contactPointer.x)
                            maxHeight = contactPointer.x;
                    }
                }
            }
            return maxHeight;
        }

        public int getHeight(ArrayList interlocking, int rungNumber)
        {
            int maxHeight = -1;
            ArrayList rungPointer = (ArrayList)interlocking[rungNumber];
            for (int j = 1; j < rungPointer.Count - 1; j++)
            {
                Contact contactPointer = (Contact)rungPointer[j];
                if (maxHeight < contactPointer.x)
                    maxHeight = contactPointer.x;
            }
            return maxHeight;
        }


        public int getWidth(ArrayList interlocking, string rungName)
        {
            int maxWidth = -1;
            if (rungName == "")
                return -1;
            for (int i = 0; i < interlocking.Count; i++)
            {
                ArrayList rungPointer = (ArrayList)interlocking[i];
                if ((string)rungPointer[rungPointer.Count - 1] == rungName)
                {
                    for (int j = 1; j < rungPointer.Count - 1; j++)
                    {
                        Contact contactPointer = (Contact)rungPointer[j];
                        if (maxWidth < contactPointer.y)
                            maxWidth = contactPointer.y;
                    }
                }
            }
            return maxWidth;
        }

        public int getWidth(ArrayList interlocking, int rungNumber)
        {
            int maxWidth = -1;
            ArrayList rungPointer = (ArrayList)interlocking[rungNumber];
            for (int j = 1; j < rungPointer.Count - 1; j++)
            {
                Contact contactPointer = (Contact)rungPointer[j];
                if (maxWidth < contactPointer.y)
                    maxWidth = contactPointer.y;
            }
            return maxWidth;
        }

        public int RecommendedWidthofWindow(int RungNumber)
        {
            int oldCellsWidth = getWidth(trueInterlockingOldPointer, RungNumber);
            int newCellsWidth = getWidth(trueInterlockingNewPointer, RungNumber);
            int cellsWidth = 0;
            if (oldCellsWidth > newCellsWidth) cellsWidth = oldCellsWidth; else cellsWidth = newCellsWidth;
            return ((int)(100 + cellsWidth * cellWidthStatic * endScaleFactor));
        }

        public int RecommendedHeightofWindow(int RungNumber)
        {
            int oldCellsHigh = getHeight(trueInterlockingOldPointer, RungNumber);
            int newCellsHigh = getHeight(trueInterlockingNewPointer, RungNumber);
            int cellsHigh = 0;
            if (oldCellsHigh > newCellsHigh) cellsHigh = oldCellsHigh; else cellsHigh = newCellsHigh;
            return ((int)(100 + cellsHigh * cellHeightStatic * endScaleFactor));
        }

        private void drawMat(Graphics grfx, Point offset, string previewRungName)
        {
            //grfx.DrawLine(myPen, 0, CellPt.X + i * cellHeight + gridOffset.X * scaleFactor,						
            //	this.Size.Width, CellPt.X + i * cellHeight + gridOffset.X * scaleFactor);	
            SolidBrush PostItBrush = new SolidBrush(Color.Cornsilk);
            Pen BlackPen = new Pen(Color.Black);
            Pen GreyPen = new Pen(Color.DarkGray);
            Point CellPt = new Point(40 - 5 + (int)(offset.X * scaleFactor),
                40 - 5 + (int)(offset.Y * scaleFactor));
            int oldCellsHigh = getHeight(trueInterlockingOldPointer, previewRungName);
            int newCellsHigh = getHeight(trueInterlockingNewPointer, previewRungName);
            int cellsHigh = 0;
            if (oldCellsHigh > newCellsHigh) cellsHigh = oldCellsHigh; else cellsHigh = newCellsHigh;
            int oldCellsWidth = getWidth(trueInterlockingOldPointer, previewRungName);
            int newCellsWidth = getWidth(trueInterlockingNewPointer, previewRungName);
            int cellsWidth = 0;
            if (oldCellsWidth > newCellsWidth) cellsWidth = oldCellsWidth; else cellsWidth = newCellsWidth;
            //draw Post it note
            grfx.FillRectangle(PostItBrush, CellPt.Y, CellPt.X, 10 + cellsWidth * cellWidthStatic * scaleFactor, 10 + cellsHigh * cellHeightStatic * scaleFactor);
            //draw Outline
            grfx.DrawLine(GreyPen, CellPt.Y, CellPt.X, CellPt.Y + 10 + cellsWidth * cellWidthStatic * scaleFactor, CellPt.X);
            grfx.DrawLine(GreyPen, CellPt.Y, CellPt.X, CellPt.Y, CellPt.X + 10 + cellsHigh * cellHeightStatic * scaleFactor);
            grfx.DrawLine(BlackPen, CellPt.Y + 10 + cellsWidth * cellWidthStatic * scaleFactor, CellPt.X,
                                    CellPt.Y + 10 + cellsWidth * cellWidthStatic * scaleFactor, CellPt.X + 10 + cellsHigh * cellHeightStatic * scaleFactor);
            grfx.DrawLine(BlackPen, CellPt.Y, CellPt.X + 10 + cellsHigh * cellHeightStatic * scaleFactor,
                                    CellPt.Y + 10 + cellsWidth * cellWidthStatic * scaleFactor, CellPt.X + 10 + cellsHigh * cellHeightStatic * scaleFactor);

        }

        private void drawRungs(Graphics grfx,
            SolidBrush BlueBrush,
            SolidBrush RedBrush,
            SolidBrush CommonBrush,
            SolidBrush WhiteBrush,
            Pen BluePen,
            Pen RedPen,
            Pen BlackPen,
            Pen myWhitePen,
            Point redOffsetPaint,
            Point blueOffsetPaint)
        {
            int contactCoord = -1;
            // Create a new pen that we shall use for drawing the line
            cellWidth = (int)(cellWidthStatic * scaleFactor); //Y
            cellHeight = (int)(cellHeightStatic * scaleFactor); //X
                                                                //Pen myGreyPen = new Pen(Color.Silver);					
            SolidBrush LinkBoardBrush = new SolidBrush(Color.Cornsilk);
            SolidBrush GreyBrush = new SolidBrush(Color.Gray);
            SolidBrush GreenBrush = new SolidBrush(Color.Green);
            SolidBrush linksHighlighted;
            //if(showLinks) linksHighlighted = GreenBrush; else linksHighlighted = GreyBrush;
            linksHighlighted = GreenBrush;
            myGreyPen.DashStyle = DashStyle.Dot;

            Font drawF = new Font(drawFnt.Name, drawFnt.Size * scaleFactor, drawFnt.Style);
            Font smallFont = new Font(drawFnt.Name, 8 * scaleFactor, drawFnt.Style);
            Font underlineFont = new Font(drawFnt.Name, drawFnt.Size * scaleFactor, drawFnt.Style | FontStyle.Underline);
            // Font highlightFont = new Font(drawFnt.Name, drawFnt.Size * scaleFactor, drawFnt.Underline);
            //Font boldFont = new Font(drawFnt.Name, 8 * scaleFactor, drawFnt.Style);
            //Font drawFont = new Font("Tahoma", (float) cellWidth/9);
            Font drawFont = drawF;
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;

            ArrayList rungNewPointer = (ArrayList)interlockingNewPointer[CurrentNewCell];
            ArrayList rungOldPointer = (ArrayList)interlockingOldPointer[CurrentOldCell];

            statusBar.Text = CellName;

            previewRungName = "";
            //statusBar.Text = redOffset.X + ", " + redOffset.Y;
            if ((timer2.Enabled == false) && showgridlines && grid)
                DrawGridLines(grfx, myGreyPen); // Draw Grid Lines when the the rung is in place
            for (int l = 1; l < rungOldPointer.Count - 1; l++)  // Old rungs
            {
                Contact ctOldPointer = (Contact)rungOldPointer[l];
                contactCoord = -1;
                for (int m = 1; m < rungNewPointer.Count - 1; m++)
                {
                    Contact contactPointer = (Contact)rungNewPointer[m];
                    if (0.2 > Math.Abs((contactPointer.x - blueOffsetPaint.X / cellHeightStatic) - (ctOldPointer.x - redOffsetPaint.X / cellHeightStatic)))
                        if (0.1 > Math.Abs((contactPointer.y - blueOffsetPaint.Y / cellWidthStatic) - (ctOldPointer.y - redOffsetPaint.Y / cellWidthStatic)))
                            contactCoord = m; // Contacts have the same cell coordinates, need to draw merged contacts
                }
                if (contactCoord == -1) // No match between cell coordinates so draw old contact in blue
                    DrawCell(ctOldPointer, grfx, BluePen, drawFont, drawFormat, BlueBrush, blueOffsetPaint);
            }
            for (int j = 1; j < rungNewPointer.Count - 1; j++)  // New Rung
            {
                Contact contactPointer = (Contact)rungNewPointer[j];
                contactCoord = -1;
                for (int k = 1; k < rungOldPointer.Count - 1; k++)
                {
                    Contact ctOldPointer = (Contact)rungOldPointer[k];
                    if (0.2 > Math.Abs((contactPointer.x - blueOffsetPaint.X / cellHeightStatic) - (ctOldPointer.x - redOffsetPaint.X / cellHeightStatic)))
                        if (0.1 > Math.Abs((contactPointer.y - blueOffsetPaint.Y / cellWidthStatic) - (ctOldPointer.y - redOffsetPaint.Y / cellWidthStatic)))
                            contactCoord = k; // Contacts have the same cell coordinates						
                }
                if (contactPointer.name.Length > 12)
                    drawFont = smallFont;
                else drawFont = drawF;
                if (contactCoord == -1) //Draw contact as a new contact 		
                {
                    if (((CellCoord.X == contactPointer.x) && (CellCoord.Y == contactPointer.y)) || showLinks)
                    { // If the mouse is on the current cell draw it highlighted	
                        if (showLinks) DrawCell(contactPointer, grfx, RedPen, drawFont, drawFormat, RedBrush, redOffsetPaint);//RedOffset
                        else
                        {
                            if (preview == false)
                            {
                                PreviewRequestTime = DateTime.Now;
                                preview = true;
                                //previewTimed = false;
                                timer3.Enabled = true;
                            } // Show preview of the rung
                            //if (previewRungName != contactPointer.name.ToString()) preview = false;
                            previewRungName = contactPointer.name.ToString();
                            if (!showingPreview)
                                DrawCell(contactPointer, grfx, HighlightPen, drawFont, drawFormat, HighlightBrush, redOffsetPaint);//RedOffset
                            else
                                DrawCell(contactPointer, grfx, RedPen, drawFont, drawFormat, RedBrush, redOffsetPaint);//RedOffset // Rungs are not aligned
                        }
                        if (!showingPreview) DrawLinks(contactPointer, grfx, HighlightPen, drawF, drawFormat, HighlightBrush, redOffsetPaint);//RedOffset
                    }
                    else
                    {
                        //preview = false;
                        DrawCell(contactPointer, grfx, RedPen, drawFont, drawFormat, RedBrush, redOffsetPaint);//RedOffset // Rungs are not aligned
                    }
                }
                else //Draw contact as a merged contact
                {
                    Contact contactOldPointer = (Contact)rungOldPointer[contactCoord];
                    if ((contactOldPointer.name.Length > 12) || (contactPointer.name.Length > 12))
                        drawFont = smallFont;
                    else drawFont = drawF;
                    if ((0.1 > (blueOffsetPaint.X - redOffsetPaint.X) / cellHeightStatic - Math.Round((blueOffsetPaint.X - redOffsetPaint.X) / cellHeightStatic)) &&
                        (0.1 > (blueOffsetPaint.Y - redOffsetPaint.Y) / cellWidthStatic - Math.Round((blueOffsetPaint.Y - redOffsetPaint.Y) / cellWidthStatic)))
                    {   // Draw the contacts merged	

                        if (((CellCoord.X == contactPointer.x) && (CellCoord.Y == contactPointer.y)) || showLinks)
                        {
                            if (showLinks)
                            {
                                if (CellName == contactPointer.name) // Highlight stick paths
                                    DrawMergedContacts(contactOldPointer, contactPointer, grfx, HighlightPen, HighlightPen, HighlightPen,
                                        drawFont, drawFormat, HighlightBrush, HighlightBrush, HighlightBrush, redOffsetPaint, blueOffsetPaint);
                                else
                                    DrawMergedContacts(contactOldPointer, contactPointer, grfx, BluePen, RedPen, BlackPen,
                                                  drawFont, drawFormat, BlueBrush, RedBrush, CommonBrush, redOffsetPaint, blueOffsetPaint);
                            }
                            else
                            {
                                if (preview == false)
                                {
                                    PreviewRequestTime = DateTime.Now;
                                    preview = true;
                                    //previewTimed = false;
                                    timer3.Enabled = true;
                                } // Show preview of the rung
                                //if (previewRungName != contactPointer.name.ToString()) preview = false;
                                previewRungName = contactPointer.name.ToString();
                                if (!showingPreview)
                                    DrawMergedContacts(contactOldPointer, contactPointer, grfx, HighlightPen, HighlightPen, HighlightPen,
                                        underlineFont, drawFormat, HighlightBrush, HighlightBrush, HighlightBrush, redOffsetPaint, blueOffsetPaint);
                                else
                                    DrawMergedContacts(contactOldPointer, contactPointer, grfx, BluePen, RedPen, BlackPen,
                                       drawFont, drawFormat, BlueBrush, RedBrush, CommonBrush, redOffsetPaint, blueOffsetPaint);
                            }
                            if (!showingPreview) DrawLinks(contactPointer, grfx, HighlightPen, drawF, drawFormat, HighlightBrush, blueOffsetPaint);
                        }
                        else
                        {
                            DrawMergedContacts(contactOldPointer, contactPointer, grfx, BluePen, RedPen, BlackPen,
                                drawFont, drawFormat, BlueBrush, RedBrush, CommonBrush, redOffsetPaint, blueOffsetPaint);
                        }
                    }
                    else
                    {
                        if ((CellCoord.X == contactOldPointer.x) && (CellCoord.Y == contactOldPointer.y))
                        {
                            DrawCell(contactOldPointer, grfx, HighlightPen, drawFont, drawFormat, HighlightBrush, blueOffsetPaint);
                            DrawCell(contactPointer, grfx, HighlightPen, drawFont, drawFormat, HighlightBrush, redOffsetPaint);
                        }
                        else
                        {
                            DrawCell(contactOldPointer, grfx, BluePen, drawFont, drawFormat, BlueBrush, blueOffsetPaint);
                            DrawCell(contactPointer, grfx, RedPen, drawFont, drawFormat, RedBrush, redOffsetPaint);
                        }
                    }
                }
                drawFont = drawF;
            }
        }

        private void DrawLinks(Contact ctOldPointer, Graphics grfx, Pen myPen,
            Font drawFont, StringFormat drawFormat, SolidBrush HighlightBrush, Point blueOffsetPaint)
        {

            SolidBrush GreenBrush = new SolidBrush(Color.Green);
            SolidBrush linksHighlighted = GreenBrush;
            bool backContact = false;
            bool ascoil = false;

            bool cutline = false;
            Pen myGreyPen = new Pen(Color.Silver);
            SolidBrush LinkBoardBrush = new SolidBrush(Color.Cornsilk);
            if (ctOldPointer.typeOfCell == "Coil")
            {
                //statusBar.Text = "CellName: " + CellName.ToString() + "  ---> ShowLinks";
                int hitNumber = 1;
                DrawReference(ctOldPointer, "Contact used in:", 0, grfx,
                    HighlightPen, drawFont, drawFormat, linksHighlighted, LinkBoardBrush, blueOffsetPaint, false, false, false, 0);
                for (int r = 0; r < interlockingNewPointer.Count; r++)
                {
                    ascoil = false;
                    ArrayList rungPointer = (ArrayList)interlockingNewPointer[r];
                    //preview = false;
                    if (rungPointer[rungPointer.Count - 1].ToString() == ctOldPointer.name)
                        cutline = false;
                    if (rungPointer[rungPointer.Count - 1].ToString() == ctOldPointer.name)
                        ascoil = true;
                    for (int k = 1; k < rungPointer.Count - 1; k++)
                    {
                        Contact contact = (Contact)rungPointer[k];
                        if (ctOldPointer.name == contact.name)
                        {
                            if (contact.NormallyClosed == true) backContact = true; else backContact = false;
                            if (contact.typeOfCell == "Coil") ascoil = true; else ascoil = false;
                            if (linkSelected == hitNumber)
                            {
                                linkName = rungPointer[rungPointer.Count - 1].ToString();
                                if (preview == false)
                                {
                                    PreviewRequestTime = DateTime.Now;
                                    preview = true;
                                    //previewTimed = false;
                                    timer3.Enabled = true;
                                } // Show preview of the rung
                                //if (previewRungName != linkName) preview = false;
                                previewRungName = linkName;
                                DrawReference(ctOldPointer, rungPointer[rungPointer.Count - 1].ToString(), hitNumber++, grfx,
                                    HighlightPen, drawFont, drawFormat, HighlightBrush, LinkBoardBrush, blueOffsetPaint, backContact, ascoil, cutline, r + 1);
                            }
                            else
                            {
                                DrawReference(ctOldPointer, rungPointer[rungPointer.Count - 1].ToString(), hitNumber++, grfx,
                                    myGreyPen, drawFont, drawFormat, linksHighlighted, LinkBoardBrush, blueOffsetPaint, backContact, ascoil, cutline, r + 1);
                            }
                        }
                    }

                    cutline = false;
                }
            }
        }

        private void DrawGridLines(Graphics grfx, Pen myPen)
        {
            Point CellPt = new Point(40, 40);
            for (int i = 0; i < this.Size.Height / cellHeight; i++)
                grfx.DrawLine(myPen, 0, CellPt.X + i * cellHeight + gridOffset.X * scaleFactor,
                    this.Size.Width, CellPt.X + i * cellHeight + gridOffset.X * scaleFactor);
            for (int j = 0; j < (1 + this.Size.Width / cellWidth); j++)
                grfx.DrawLine(myPen,
                    CellPt.Y + j * cellWidth + gridOffset.Y * scaleFactor, 0,
                    CellPt.Y + j * cellWidth + gridOffset.Y * scaleFactor, this.Size.Height);
        }

        private void DrawCoil(Contact contactPointer, Font drawFont, Graphics grfx, Pen myPen, Point offset)
        {
            Pen Drawpen = myPen;

            SolidBrush DrawBrush = SimBrush;

            string titlebar = "";
            if (SeqList.Count > 0)
                if (contactPointer.name == SeqList[seqcounter].ToString())
                    Drawpen = GoldPen;
            //Simulation Code
            if (SimMode)
            {
                Drawpen = SimPenDn;
                DrawBrush = SimBrushDown;
                titlebar = contactPointer.name + " - Low";
                //for (int i = 0; i < SimInputs.Count; i++)
                //    if (contactPointer.name == SimInputs[i].ToString())
                //    {
                //        Drawpen = SimPenUp;
                //        DrawBrush = SimBrushUp;
                //    }
                for (int i = 0; i < SimRungs.Count; i++)

                    if (contactPointer.name == SimRungs[i].ToString())
                    {
                        Drawpen = SimPenUp;
                        DrawBrush = SimBrushUp;
                        titlebar = contactPointer.name + " - High";
                    }

            }
            else { titlebar = contactPointer.name; }
            //this.Text = titlebar;
            Point CellPt = new Point(40 + (contactPointer.x - 1) * cellHeight + (int)(offset.X * scaleFactor),
                40 + (contactPointer.y - 1) * cellWidth + (int)(offset.Y * scaleFactor));
            grfx.DrawArc(Drawpen,
                CellPt.Y + (contactLocation.Y + 46) * scaleFactor,
                CellPt.X + (contactLocation.X - 15) * scaleFactor,
                (30) * scaleFactor, (30) * scaleFactor,
                280.0F, 160.0F);
            grfx.DrawArc(Drawpen,
                CellPt.Y + (contactLocation.Y + 35) * scaleFactor,
                CellPt.X + (contactLocation.X - 15) * scaleFactor,
                (30) * scaleFactor, (30) * scaleFactor,
                100.0F, 160.0F);
            if (SimMode)
            {
                grfx.FillEllipse(DrawBrush,
                    CellPt.Y + (contactLocation.Y + 43) * scaleFactor,
                    CellPt.X + (contactLocation.X - 12) * scaleFactor,
                    (25) * scaleFactor, (25) * scaleFactor);

                //grfx.DrawString(state, drawFont, DrawBrush,
                //  CellPt.Y + (contactLocation.Y + 37) * scaleFactor,
                //CellPt.X + (contactLocation.X + -29) * scaleFactor);

                if (Drawpen == SimPenUp)
                {
                    DrawLineInCell(grfx, CellPt, 87, -30, 87, 20, SimPenFlow); //Drawpen);
                    DrawLineInCell(grfx, CellPt, 75, -10, 87, -30, SimPenFlow); //Drawpen);
                    DrawLineInCell(grfx, CellPt, 99, -10, 87, -30, SimPenFlow); //Drawpen);
                }
                if (Drawpen == SimPenDn)
                {

                    DrawLineInCell(grfx, CellPt, 87, -30, 87, 20, SimPenNoFlow); //Drawpen);
                    DrawLineInCell(grfx, CellPt, 75, 0, 87, 20, SimPenNoFlow); //Drawpen);
                    DrawLineInCell(grfx, CellPt, 99, 0, 87, 20, SimPenNoFlow); //Drawpen);

                    //DrawLineInCell(grfx, CellPt, 80, -17, 105, 17, SimPenNoFlow); //Drawpen);
                    //DrawLineInCell(grfx, CellPt, 80,  17, 105,-17, SimPenNoFlow); //Drawpen);
                }

            }
        }

        private void DrawContact(Contact contact, Graphics grfx, Pen myPen, Point offset, bool drawlegs)
        {
            Pen Drawpen = myPen;
            if (SeqList.Count > 0)
                if (contact.name == SeqList[seqcounter].ToString())
                    Drawpen = GoldPen;
            //Simulation Code
            if (SimMode)
            {
                Drawpen = SimPenDn;
                if ((
                                    !inList(contact.name, SimRungs) && // 
                                    !inList(contact.name, SimInputs) &&
                                    contact.NormallyClosed //Backcontact
                                )
                                ||
                                (
                                    (
                                        inList(contact.name, SimRungs) ||
                                        inList(contact.name, SimInputs)
                                    )
                                    &&
                                    !contact.NormallyClosed //Front Contact
                                ))
                    Drawpen = SimPenUp;

                //Drawpen.DashCap = System.Drawing.Drawing2D.DashCap.Round;
                //Drawpen.DashPattern = new float[] { 6 - (float) animate, (float)animate };  
            }
            Point CellPt = new Point(40 + (contact.x - 1) * cellHeight + (int)(offset.X * scaleFactor),
                40 + (contact.y - 1) * cellWidth + (int)(offset.Y * scaleFactor));
            if (Drawpen == SimPenUp)
            {
                //DrawLineInCell(grfx, CellPt, 46, 0, 60, 0, SimPenFlow); //Drawpen);
                //DrawLineInCell(grfx, CellPt, 56, -4, 60, 0, SimPenFlow); //Drawpen);
                //DrawLineInCell(grfx, CellPt, 56, 4, 60, 0, SimPenFlow); //Drawpen);
                DrawLineInCell(grfx, CellPt, 20, 25, 100, 25, SimPenFlow); //Drawpen);
                DrawLineInCell(grfx, CellPt, 70, 13, 100, 25, SimPenFlow); //Drawpen);
                DrawLineInCell(grfx, CellPt, 70, 37, 100, 25, SimPenFlow); //Drawpen);
            }
            if (Drawpen == SimPenDn)
            {
                //DrawLineInCell(grfx, CellPt, 46, 0, 60, 0, SimPenFlow); //Drawpen);
                DrawLineInCell(grfx, CellPt, 70, -17, 95, 17, SimPenNoFlow); //Drawpen);
                DrawLineInCell(grfx, CellPt, 70, 17, 95, -17, SimPenNoFlow); //Drawpen);
            }
            if (drawlegs)
            {
                DrawLineInCell(grfx, CellPt, 0, 0, 46, 0, myPen);
                DrawLineInCell(grfx, CellPt, 60, 0, 110, 0, myPen);
            }
            DrawLineInCell(grfx, CellPt, 46, -14, 46, 14, Drawpen);
            DrawLineInCell(grfx, CellPt, 60, -14, 60, 14, Drawpen);
            DrawLineInCell(grfx, CellPt, 46, -14, 43, -14, Drawpen);
            DrawLineInCell(grfx, CellPt, 46, 14, 43, 14, Drawpen);
            DrawLineInCell(grfx, CellPt, 60, -14, 63, -14, Drawpen);
            DrawLineInCell(grfx, CellPt, 60, 14, 63, 14, Drawpen);
        }

        private TimersTimingStruct getTimerElement(string timernamestring, ArrayList timers)
        {
            for (int i = 0; i < timers.Count; i++)
            {
                TimersTimingStruct timerElement = (TimersTimingStruct)timers[i];
                if (timerElement.timername == timernamestring)
                    return timerElement;
            }
            TimersTimingStruct dummyElement = new TimersTimingStruct();
            dummyElement.timername = "notimerfound";
            return dummyElement;
        }

        private bool inList(string name, ArrayList list)
        {
            for (int i = 0; i < list.Count; i++)
                if (name == (string)list[i]) return true;
            return false;
        }

        private void DrawReference(Contact contactPointer, string rungName, int hitNumber, Graphics grfx, Pen myPen,
            Font drawFont, StringFormat drawFormat, SolidBrush CommonRedBrush, SolidBrush myBrush, Point offset, bool Back, bool ascoil, bool cutline, int rungnumber)
        {
            Point CellPt = new Point(40 + (contactPointer.x - 1) * cellHeight + (int)(offset.X * scaleFactor),
                    40 + (contactPointer.y - 1) * cellWidth + (int)(offset.Y * scaleFactor));
            SolidBrush ShadowBrush = new SolidBrush(Color.Gray);
            Font refFont;
            int position = 150;
            int height = 25;
            float widthFactor = 1.9f;
            if (hitNumber != 0) grfx.DrawLine(myPen, CellPt.Y + 104 * scaleFactor, CellPt.X + 12 * scaleFactor, CellPt.Y + (position - 4) * scaleFactor, CellPt.X + (hitNumber * 25 + 12) * scaleFactor);
            grfx.FillRectangle(ShadowBrush,
                CellPt.Y + (float)Math.Ceiling(1 * scaleFactor) + position * scaleFactor,
                CellPt.X + (float)Math.Ceiling(1 * scaleFactor) + hitNumber * height * scaleFactor,
                (int)(widthFactor * cellWidthStatic * scaleFactor),
                height * scaleFactor);
            grfx.FillRectangle(myBrush, CellPt.Y + position * scaleFactor, CellPt.X + hitNumber * height * scaleFactor,
                (int)(widthFactor * cellWidthStatic * scaleFactor), height * scaleFactor);
            //grfx.FillRectangle(ShadowBrush, CellPt.Y + position * scaleFactor, CellPt.X + hitNumber * height * scaleFactor,
            //    (int)(widthFactor * cellWidthStatic * scaleFactor), 1);
            RectangleF drawRect = new RectangleF(CellPt.Y + position * scaleFactor, CellPt.X + hitNumber * height * scaleFactor,
                (int)(widthFactor * cellWidthStatic * scaleFactor), height * scaleFactor);
            StringFormat refDrawFormat = new StringFormat();
            refDrawFormat.Alignment = StringAlignment.Near;
            string comment = "";
            refFont = drawFont;
            boldFont = new Font(refFont.Name, refFont.Size, refFont.Style | FontStyle.Bold);
            italicsFont = new Font(refFont.Name, refFont.Size, refFont.Style | FontStyle.Italic);
            if (Back)
            {
                comment = " (as back contact)";
                //refFont = italicsFont;
            }
            if (ascoil)
            {
                comment = " (as coil)";
                refFont = boldFont;
            }
            if (rungnumber == 0) grfx.DrawString(rungName + comment, refFont, CommonRedBrush, CellPt.Y + position * scaleFactor, CellPt.X + hitNumber * height * scaleFactor, refDrawFormat);
            else grfx.DrawString(rungnumber.ToString() + ": " + rungName + comment, refFont, CommonRedBrush, CellPt.Y + position * scaleFactor, CellPt.X + hitNumber * height * scaleFactor, refDrawFormat);
            linksPoint = CellPt;
        }

        private void DrawCell(Contact contactPointer, Graphics grfx, Pen myPen,
            Font drawFont, StringFormat drawFormat, SolidBrush CommonRedBrush, Point offset)
        {
            Point CellPt = new Point(40 + (contactPointer.x - 1) * cellHeight + (int)(offset.X * scaleFactor),
                40 + (contactPointer.y - 1) * cellWidth + (int)(offset.Y * scaleFactor));
            // Highlight contacts
            if ((contactHighlight == contactPointer.name) && (contactHighlight != ""))
            {
                Rectangle drawRect = new Rectangle(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X,
                    (int)(114 * scaleFactor), (int)(25 * scaleFactor));
                grfx.FillRectangle(contactHighlightBrush, drawRect);
                grfx.DrawRectangle(contactHighlightPen, drawRect);
            }
            if (contactPointer.typeOfCell == "Contact")
            {
                RectangleF drawRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X, 114 * scaleFactor, 25 * scaleFactor);
                /*RectangleF SimTimeRect = new RectangleF(CellPt.Y + nameLocation.Y + 120 * scaleFactor, CellPt.X + nameLocation.X + 40 * scaleFactor,
                                            214 * scaleFactor, 25 * scaleFactor);*/
                RectangleF SimTimeRect = new RectangleF(CellPt.Y + nameLocation.Y + 120 * scaleFactor, CellPt.X + nameLocation.X, 114 * scaleFactor, 25 * scaleFactor);
                grfx.DrawString(contactPointer.name, drawFont, CommonRedBrush, drawRect, drawFormat);
                DrawContact(contactPointer, grfx, myPen, offset, true);
                if (SimMode) DrawSimTimer(contactPointer.name, SimTimeRect, drawFont, drawFormat, grfx);
                if (contactPointer.NormallyClosed == true) DrawLineInCell(grfx, CellPt, 49, -14, 57, 14, myPen);
            }
            if (contactPointer.typeOfCell == "Coil")
            {
                RectangleF drawRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X,
                    cellWidthStatic * scaleFactor, 25 * scaleFactor);
                DrawLineInCell(grfx, CellPt, 0, 0, 34, 0, myPen);
                DrawCoil(contactPointer, drawFont, grfx, myPen, offset);


                Pen BluePeng = new Pen(Color.Blue);
                Pen RedPeng = new Pen(Color.Red);
                Pen BlackPeng = new Pen(Color.Black);
                Pen myWhitePeng = new Pen(Color.White);
                SolidBrush BlueBrushg = new SolidBrush(Color.Blue);
                SolidBrush RedBrushg = new SolidBrush(Color.Red);
                SolidBrush CommonBrushg = new SolidBrush(Color.Black);
                SolidBrush WhiteBrushg = new SolidBrush(Color.White);

                DrawTime(contactPointer, CellPt, grfx, BluePeng, RedPeng, BlackPeng, drawFont, drawFormat, BlueBrushg, RedBrushg, CommonBrushg);
                grfx.DrawString(contactPointer.name, drawFont, CommonRedBrush, drawRect, drawFormat);
                //DrawTime(contactPointer, CellPt, grfx, BluePen, RedPen, BlackPen, drawFont, drawFormat, BlueBrush, RedBrush, CommonBrush, redOffsetPaint, blueOffsetPaint);
            }
            if (contactPointer.typeOfCell == "Horizontal Shunt") DrawLineInCell(grfx, CellPt, 0, 0, 110, 0, myPen);
            if (contactPointer.topLink == true) DrawLineInCell(grfx, CellPt, 0, -40, 0, 0, myPen);
            if (contactPointer.bottomLink == true) DrawLineInCell(grfx, CellPt, 0, 50, 0, 0, myPen);
            if (contactPointer.leftLink == true) DrawLineInCell(grfx, CellPt, 0, 0, -10, 0, myPen);
        }

        private void DrawLineInCell(Graphics grfx, Point CellPt, int Y1, int X1, int Y2, int X2, Pen myPen)
        {
            grfx.DrawLine(myPen,
                CellPt.Y + (contactLocation.Y + Y1) * scaleFactor,
                CellPt.X + (contactLocation.X + X1) * scaleFactor,
                CellPt.Y + (contactLocation.Y + Y2) * scaleFactor,
                CellPt.X + (contactLocation.X + X2) * scaleFactor);
        }

        private void DrawMergedContacts(Contact contactOldPointer, Contact contactPointer, Graphics grfx, Pen BluePen,
            Pen RedPen, Pen BlackPen, Font drawFont, StringFormat drawFormat, SolidBrush BlueBrush,
            SolidBrush RedBrush, SolidBrush CommonBrush, Point redOffsetPaint, Point blueOffsetPaint)
        {

            string rungnumber = "";
            Pen linkPen = BlackPen;
            Point CellPt = new Point(40 + (contactPointer.x - 1) * cellHeight + (int)(redOffsetPaint.X * scaleFactor),
                40 + (contactPointer.y - 1) * cellWidth + (int)(redOffsetPaint.Y * scaleFactor));


            //Contacts
            if (contactPointer.typeOfCell == "Contact")
            {
                if (contactOldPointer.typeOfCell == "Contact")
                {
                    if (contactPointer.name != contactOldPointer.name)
                    {
                        // Highlight contacts
                        if ((contactHighlight == contactPointer.name) && (contactHighlight != ""))
                        {
                            RectangleF drawRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + (int)(-4 * scaleFactor),
                                114 * scaleFactor, 25 * scaleFactor);
                            Rectangle drawRecti = new Rectangle(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + (int)(-4 * scaleFactor),
                                (int)(114 * scaleFactor), (int)(25 * scaleFactor));
                            grfx.FillRectangle(contactHighlightBrush, drawRect);
                            grfx.DrawRectangle(contactHighlightPen, drawRecti);
                        }
                        //Labels
                        RectangleF drawNewRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + (int)(-4 * scaleFactor),
                            114 * scaleFactor, 25 * scaleFactor);
                        RectangleF drawOldRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + (int)(42 * scaleFactor),
                            114 * scaleFactor, 25 * scaleFactor);
                        grfx.DrawString(contactPointer.name, drawFont, RedBrush, drawNewRect, drawFormat);
                        grfx.DrawString(contactOldPointer.name, drawFont, BlueBrush, drawOldRect, drawFormat);
                        //Contacts						
                        Point newContactPos = new Point((int)(redOffsetPaint.X - 11), (int)(redOffsetPaint.Y));
                        Point oldContactPos = new Point((int)(redOffsetPaint.X + 34), (int)(redOffsetPaint.Y));
                        DrawContact(contactPointer, grfx, RedPen, newContactPos, false);
                        DrawContact(contactPointer, grfx, BluePen, oldContactPos, false);
                        DrawLineInCell(grfx, CellPt, 0, 0, 10, 0, BlackPen);
                        DrawLineInCell(grfx, CellPt, 100, 0, 110, 0, BlackPen);
                        //Right Legs
                        DrawLineInCell(grfx, CellPt, 10, 0, 10, -11, RedPen);
                        DrawLineInCell(grfx, CellPt, 10, -11, 46, -11, RedPen);
                        DrawLineInCell(grfx, CellPt, 10, 0, 10, 34, BluePen);
                        DrawLineInCell(grfx, CellPt, 10, 34, 46, 34, BluePen);
                        //Left Legs
                        DrawLineInCell(grfx, CellPt, 100, 0, 100, -11, RedPen);
                        DrawLineInCell(grfx, CellPt, 60, -11, 100, -11, RedPen);
                        DrawLineInCell(grfx, CellPt, 100, 0, 100, 34, BluePen);
                        DrawLineInCell(grfx, CellPt, 60, 34, 100, 34, BluePen);
                        //Back contacts
                        if (contactPointer.NormallyClosed) DrawLineInCell(grfx, CellPt, 49, -(14 + 11), 57, 14 - 11, RedPen);
                        if (contactOldPointer.NormallyClosed) DrawLineInCell(grfx, CellPt, 49, 34 - 14, 57, 14 + 34, BluePen);
                    }
                    else
                    {
                        RectangleF drawRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X,
                            114 * scaleFactor, 25 * scaleFactor);
                        RectangleF SimTimeRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + 55 * scaleFactor, 114 * scaleFactor, 50 * scaleFactor);
                        if ((contactHighlight == contactOldPointer.name) && (contactHighlight != ""))
                        {
                            Rectangle drawRecti = new Rectangle(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X,
                                (int)(114 * scaleFactor), (int)(25 * scaleFactor));
                            grfx.FillRectangle(contactHighlightBrush, drawRect);
                            grfx.DrawRectangle(contactHighlightPen, drawRecti);
                        }
                        if (grid && (findRung(interlockingNewPointer, contactPointer.name) != -1)) rungnumber = findRung(interlockingNewPointer, contactPointer.name).ToString() + ": ";
                        grfx.DrawString(rungnumber + contactPointer.name, drawFont, CommonBrush, drawRect, drawFormat);
                        DrawContact(contactPointer, grfx, BlackPen, redOffsetPaint, true);
                        if (SimMode) DrawSimTimer(contactPointer.name, SimTimeRect, drawFont, drawFormat, grfx);
                        if ((contactPointer.NormallyClosed == true) && (contactOldPointer.NormallyClosed == true))
                            linkPen = BlackPen;
                        if ((contactPointer.NormallyClosed == false) && (contactOldPointer.NormallyClosed == true))
                            linkPen = BluePen;
                        if ((contactPointer.NormallyClosed == true) && (contactOldPointer.NormallyClosed == false))
                            linkPen = RedPen;
                        if (!((contactPointer.NormallyClosed == false) && (contactOldPointer.NormallyClosed == false)))
                            DrawLineInCell(grfx, CellPt, 49, -14, 57, 14, linkPen);
                    }
                }
                if (contactOldPointer.typeOfCell == "Coil")
                {
                    RectangleF drawNewRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + (int)(-4 * scaleFactor),
                        114 * scaleFactor, 25 * scaleFactor);
                    RectangleF drawOldRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + (int)(42 * scaleFactor),
                        114 * scaleFactor, 25 * scaleFactor);
                    grfx.DrawString(contactPointer.name, drawFont, RedBrush, drawNewRect, drawFormat);
                    grfx.DrawString(contactOldPointer.name, drawFont, BlueBrush, drawOldRect, drawFormat);
                    Point newContactPos = new Point((int)(redOffsetPaint.X - 11), (int)(redOffsetPaint.Y));
                    Point oldContactPos = new Point((int)(redOffsetPaint.X + 34), (int)(redOffsetPaint.Y));
                    DrawContact(contactPointer, grfx, RedPen, newContactPos, false);
                    DrawLineInCell(grfx, CellPt, 0, 0, 10, 0, BlackPen);
                    DrawLineInCell(grfx, CellPt, 100, 0, 110, 0, RedPen);
                    //Right Legs
                    DrawLineInCell(grfx, CellPt, 10, 0, 10, -11, RedPen);
                    DrawLineInCell(grfx, CellPt, 10, -11, 46, -11, RedPen);
                    DrawLineInCell(grfx, CellPt, 10, 0, 10, 34, BluePen);
                    DrawLineInCell(grfx, CellPt, 10, 34, 34, 34, BluePen);
                    //Left leg
                    DrawLineInCell(grfx, CellPt, 100, 0, 100, -11, RedPen);
                    DrawLineInCell(grfx, CellPt, 60, -11, 100, -11, RedPen);
                    DrawCoil(contactPointer, drawFont, grfx, BluePen, oldContactPos);
                    if (contactPointer.NormallyClosed)
                        DrawLineInCell(grfx, CellPt, 49, -(14 + 11), 57, 14 - 11, RedPen);

                }
                if (contactOldPointer.typeOfCell == "Empty")
                {
                    RectangleF drawRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X,
                        114 * scaleFactor, 25 * scaleFactor);
                    grfx.DrawString(contactPointer.name, drawFont, RedBrush, drawRect, drawFormat);
                    DrawContact(contactPointer, grfx, RedPen, redOffsetPaint, true);
                    if (contactPointer.NormallyClosed)
                        DrawLineInCell(grfx, CellPt, 49, -14, 57, 14, RedPen);
                }
                if (contactOldPointer.typeOfCell == "Horizontal Shunt")
                {
                    RectangleF drawNewRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + (int)(-4 * scaleFactor),
                        114 * scaleFactor, 25 * scaleFactor);
                    grfx.DrawString(contactPointer.name, drawFont, RedBrush, drawNewRect, drawFormat);
                    //Contacts						
                    Point newContactPos = new Point((int)(redOffsetPaint.X - 11), (int)(redOffsetPaint.Y));
                    DrawContact(contactPointer, grfx, RedPen, newContactPos, false);
                    DrawLineInCell(grfx, CellPt, 0, 0, 10, 0, BlackPen);
                    DrawLineInCell(grfx, CellPt, 100, 0, 110, 0, BlackPen);
                    //Right Legs
                    DrawLineInCell(grfx, CellPt, 10, 0, 10, -11, RedPen);
                    DrawLineInCell(grfx, CellPt, 10, -11, 46, -11, RedPen);
                    DrawLineInCell(grfx, CellPt, 10, 0, 10, 15, BluePen);
                    DrawLineInCell(grfx, CellPt, 10, 15, 100, 15, BluePen);
                    //Left Legs
                    DrawLineInCell(grfx, CellPt, 100, 0, 100, -11, RedPen);
                    DrawLineInCell(grfx, CellPt, 60, -11, 100, -11, RedPen);
                    DrawLineInCell(grfx, CellPt, 100, 0, 100, 15, BluePen);
                    //back contacts
                    if (contactPointer.NormallyClosed) DrawLineInCell(grfx, CellPt, 49, -(14 + 11), 57, 14 - 11, RedPen);
                }
            }

            //Coils
            if (contactPointer.typeOfCell == "Coil")
            {
                if (contactOldPointer.typeOfCell == "Contact")
                {
                    RectangleF drawNewRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + (int)(-4 * scaleFactor),
                        114 * scaleFactor, 25 * scaleFactor);
                    RectangleF drawOldRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + (int)(42 * scaleFactor),
                        114 * scaleFactor, 25 * scaleFactor);
                    grfx.DrawString(contactPointer.name, drawFont, RedBrush, drawNewRect, drawFormat);
                    grfx.DrawString(contactOldPointer.name, drawFont, BlueBrush, drawOldRect, drawFormat);
                    Point newContactPos = new Point((int)(redOffsetPaint.X - 11), (int)(redOffsetPaint.Y));
                    Point oldContactPos = new Point((int)(redOffsetPaint.X + 34), (int)(redOffsetPaint.Y));
                    DrawLineInCell(grfx, CellPt, 0, 0, 10, 0, BlackPen);
                    DrawLineInCell(grfx, CellPt, 100, 0, 110, 0, BlackPen);
                    //Right Legs
                    DrawLineInCell(grfx, CellPt, 10, 0, 10, -11, RedPen);
                    DrawLineInCell(grfx, CellPt, 10, -11, 34, -11, RedPen);
                    DrawLineInCell(grfx, CellPt, 10, 0, 10, 34, BluePen);
                    DrawLineInCell(grfx, CellPt, 10, 34, 46, 34, BluePen);
                    //Left leg
                    DrawLineInCell(grfx, CellPt, 100, 0, 100, 34, BluePen);
                    DrawLineInCell(grfx, CellPt, 60, 34, 100, 34, BluePen);

                    DrawContact(contactPointer, grfx, BluePen, oldContactPos, false);
                    DrawCoil(contactPointer, drawFont, grfx, RedPen, newContactPos);
                    //DrawTime(contactPointer, CellPt, grfx, BluePen, RedPen, BlackPen, drawFont, drawFormat, BlueBrush, RedBrush, CommonBrush, redOffsetPaint, blueOffsetPaint);
                    if (contactOldPointer.NormallyClosed)
                        DrawLineInCell(grfx, CellPt, 49, -(14 - 34), 57, 14 + 34, BluePen);

                }
                if (contactOldPointer.typeOfCell == "Empty")
                {
                    RectangleF drawRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X,
                        cellWidthStatic * scaleFactor, 25 * scaleFactor);
                    grfx.DrawString(contactPointer.name, drawFont, RedBrush, drawRect, drawFormat);
                    DrawLineInCell(grfx, CellPt, 0, 0, 34, 0, RedPen);
                    DrawCoil(contactPointer, drawFont, grfx, RedPen, redOffsetPaint);
                    DrawTime(contactPointer, CellPt, grfx, BluePen, RedPen, BlackPen, drawFont, drawFormat, BlueBrush, RedBrush, CommonBrush);
                }
                if (contactOldPointer.typeOfCell == "Coil")
                {
                    if (contactPointer.name != contactOldPointer.name)
                    {
                        RectangleF drawNewRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + (int)(-4 * scaleFactor),
                            114 * scaleFactor, 25 * scaleFactor);
                        RectangleF drawOldRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + (int)(42 * scaleFactor),
                            114 * scaleFactor, 25 * scaleFactor);
                        Point newContactPos = new Point((int)(redOffsetPaint.X - 11), (int)(redOffsetPaint.Y));
                        Point oldContactPos = new Point((int)(redOffsetPaint.X + 34), (int)(redOffsetPaint.Y));
                        grfx.DrawString(contactPointer.name, drawFont, RedBrush, drawNewRect, drawFormat);
                        grfx.DrawString(contactOldPointer.name, drawFont, BlueBrush, drawOldRect, drawFormat);
                        DrawCoil(contactPointer, drawFont, grfx, BluePen, oldContactPos);
                        DrawCoil(contactPointer, drawFont, grfx, RedPen, newContactPos);
                        DrawTime(contactPointer, CellPt, grfx, BluePen, RedPen, BlackPen, drawFont, drawFormat, BlueBrush, RedBrush, CommonBrush);
                        DrawLineInCell(grfx, CellPt, 0, 0, 10, 0, BlackPen);
                        //Right Legs
                        DrawLineInCell(grfx, CellPt, 10, 0, 10, -11, RedPen);
                        DrawLineInCell(grfx, CellPt, 10, -11, 34, -11, RedPen);
                        DrawLineInCell(grfx, CellPt, 10, 0, 10, 34, BluePen);
                        DrawLineInCell(grfx, CellPt, 10, 34, 34, 34, BluePen);
                    }
                    else
                    {
                        RectangleF drawRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X,
                            114 * scaleFactor, 25 * scaleFactor);
                        if (grid && (findRung(interlockingNewPointer, contactPointer.name) != -1)) rungnumber = findRung(interlockingNewPointer, contactPointer.name).ToString() + ": ";
                        grfx.DrawString(rungnumber + contactPointer.name, drawFont, CommonBrush, drawRect, drawFormat);
                        DrawCoil(contactPointer, drawFont, grfx, BlackPen, redOffsetPaint);
                        DrawLineInCell(grfx, CellPt, 0, 0, 34, 0, BlackPen);
                        DrawTime(contactPointer, CellPt, grfx, BluePen, RedPen, BlackPen, drawFont, drawFormat, BlueBrush, RedBrush, CommonBrush);
                    }
                }
                if (contactOldPointer.typeOfCell == "Horizontal Shunt")
                {
                    RectangleF drawNewRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + (int)(-4 * scaleFactor),
                        114 * scaleFactor, 25 * scaleFactor);
                    Point newContactPos = new Point((int)(redOffsetPaint.X - 11), (int)(redOffsetPaint.Y));
                    grfx.DrawString(contactPointer.name, drawFont, RedBrush, drawNewRect, drawFormat);
                    DrawCoil(contactPointer, drawFont, grfx, RedPen, newContactPos);
                    DrawLineInCell(grfx, CellPt, 0, 0, 10, 0, BlackPen);
                    DrawLineInCell(grfx, CellPt, 100, 0, 110, 0, BluePen);
                    //Right Legs
                    DrawLineInCell(grfx, CellPt, 10, 0, 10, -11, RedPen);
                    DrawLineInCell(grfx, CellPt, 10, -11, 34, -11, RedPen);
                    //Right Legs
                    DrawLineInCell(grfx, CellPt, 10, 0, 10, 15, BluePen);
                    DrawLineInCell(grfx, CellPt, 10, 15, 100, 15, BluePen);
                    //Left Legs
                    DrawLineInCell(grfx, CellPt, 100, 0, 100, 15, BluePen);
                }
            }

            // Horizontal Shunts
            if (contactPointer.typeOfCell == "Horizontal Shunt")
            {
                if (contactOldPointer.typeOfCell == "Coil")
                {
                    RectangleF drawOldRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + (int)(42 * scaleFactor),
                        cellWidthStatic * scaleFactor, 25 * scaleFactor);
                    Point oldContactPos = new Point((int)(redOffsetPaint.X + 34), (int)(redOffsetPaint.Y));
                    grfx.DrawString(contactOldPointer.name, drawFont, BlueBrush, drawOldRect, drawFormat);
                    DrawCoil(contactPointer, drawFont, grfx, BluePen, oldContactPos);
                    DrawLineInCell(grfx, CellPt, 0, 0, 10, 0, BlackPen);
                    DrawLineInCell(grfx, CellPt, 100, 0, 110, 0, RedPen);
                    //Right Legs
                    DrawLineInCell(grfx, CellPt, 10, 0, 10, -11, RedPen);
                    DrawLineInCell(grfx, CellPt, 10, -11, 100, -11, RedPen);
                    DrawLineInCell(grfx, CellPt, 100, -11, 100, 0, RedPen);
                    DrawLineInCell(grfx, CellPt, 10, 0, 10, 34, BluePen);
                    DrawLineInCell(grfx, CellPt, 10, 34, 34, 34, BluePen);
                }
                if (contactOldPointer.typeOfCell == "Empty")
                    DrawLineInCell(grfx, CellPt, 0, 0, 110, 0, RedPen);
                if (contactOldPointer.typeOfCell == "Horizontal Shunt")
                    DrawLineInCell(grfx, CellPt, 0, 0, 110, 0, BlackPen);
                if (contactOldPointer.typeOfCell == "Contact")
                {
                    RectangleF drawOldRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + (int)(42 * scaleFactor),
                        114 * scaleFactor, 25 * scaleFactor);
                    grfx.DrawString(contactOldPointer.name, drawFont, BlueBrush, drawOldRect, drawFormat);

                    //Contacts						
                    Point oldContactPos = new Point((int)(redOffsetPaint.X + 34), (int)(redOffsetPaint.Y));
                    DrawContact(contactPointer, grfx, BluePen, oldContactPos, false);
                    DrawLineInCell(grfx, CellPt, 0, 0, 10, 0, BlackPen);
                    DrawLineInCell(grfx, CellPt, 100, 0, 110, 0, BlackPen);
                    //Right Legs
                    DrawLineInCell(grfx, CellPt, 10, 0, 10, -11, RedPen);
                    DrawLineInCell(grfx, CellPt, 10, -11, 100, -11, RedPen);
                    DrawLineInCell(grfx, CellPt, 10, 0, 10, 34, BluePen);
                    DrawLineInCell(grfx, CellPt, 10, 34, 46, 34, BluePen);
                    //Left Legs
                    DrawLineInCell(grfx, CellPt, 100, 0, 100, -11, RedPen);
                    DrawLineInCell(grfx, CellPt, 60, -11, 100, -11, RedPen);
                    DrawLineInCell(grfx, CellPt, 100, 0, 100, 34, BluePen);
                    DrawLineInCell(grfx, CellPt, 60, 34, 100, 34, BluePen);
                }
            }
            // Empty
            if (contactPointer.typeOfCell == "Empty")
            {
                if (contactOldPointer.typeOfCell == "Contact")
                {
                    RectangleF drawRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X,
                        114 * scaleFactor, 25 * scaleFactor);
                    grfx.DrawString(contactOldPointer.name, drawFont, BlueBrush, drawRect, drawFormat);
                    DrawContact(contactPointer, grfx, BluePen, redOffsetPaint, true);
                    if (contactOldPointer.NormallyClosed == true)//false)
                        DrawLineInCell(grfx, CellPt, 49, -14, 57, 14, BluePen);
                }
                if (contactOldPointer.typeOfCell == "Coil")
                {
                    RectangleF drawRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X,
                        114 * scaleFactor, 25 * scaleFactor);
                    grfx.DrawString(contactOldPointer.name, drawFont, BlueBrush, drawRect, drawFormat);
                    DrawCoil(contactPointer, drawFont, grfx, BluePen, redOffsetPaint);
                    DrawTime(contactOldPointer, CellPt, grfx, BluePen, RedPen, BlackPen, drawFont, drawFormat, BlueBrush, RedBrush, CommonBrush);
                    DrawLineInCell(grfx, CellPt, 0, 0, 34, 0, BluePen);
                }
                if (contactOldPointer.typeOfCell == "Horizontal Shunt")
                    DrawLineInCell(grfx, CellPt, 0, 0, 110, 0, BluePen);
            }

            // Top Links
            if ((contactPointer.topLink == true) && (contactOldPointer.topLink == true))
                linkPen = BlackPen;
            if ((contactPointer.topLink == false) && (contactOldPointer.topLink == true))
                linkPen = BluePen;
            if ((contactPointer.topLink == true) && (contactOldPointer.topLink == false))
                linkPen = RedPen;
            if ((contactPointer.topLink == true) || (contactOldPointer.topLink == true))
                DrawLineInCell(grfx, CellPt, 0, -40, 0, 0, linkPen);
            // Bottom Links
            if ((contactPointer.bottomLink == true) && (contactOldPointer.bottomLink == true))
                linkPen = BlackPen;
            if ((contactPointer.bottomLink == false) && (contactOldPointer.bottomLink == true))
                linkPen = BluePen;
            if ((contactPointer.bottomLink == true) && (contactOldPointer.bottomLink == false))
                linkPen = RedPen;
            if ((contactPointer.bottomLink == true) || (contactOldPointer.bottomLink == true))
                DrawLineInCell(grfx, CellPt, 0, 50, 0, 0, linkPen);
            // Left Links
            if ((contactPointer.leftLink == true) && (contactOldPointer.leftLink == true))
                linkPen = BlackPen;
            if ((contactPointer.leftLink == false) && (contactOldPointer.leftLink == true))
                linkPen = BluePen;
            if ((contactPointer.leftLink == true) && (contactOldPointer.leftLink == false))
                linkPen = RedPen;
            if ((contactPointer.leftLink == true) || (contactOldPointer.leftLink == true))
                DrawLineInCell(grfx, CellPt, 0, 0, -10, 0, linkPen);
        }

        private void DrawTime(Contact contactPointer, Point CellPt, Graphics grfx, Pen BluePen,
            Pen RedPen, Pen BlackPen, Font drawFont, StringFormat drawFormat, SolidBrush BlueBrush,
            SolidBrush RedBrush, SolidBrush CommonBrush)
        {
            try
            {
                if (showtimers)
                {

                    int timerNewnumber = findTimer(timersNew, contactPointer.name);
                    int timerOldnumber = findTimer(timersOld, contactPointer.name);
                    if ((timerNewnumber != -1) || (timerOldnumber != -1))
                    {
                        if ((timerNewnumber != -1) && (timerOldnumber != -1))
                        {
                            RectangleF drawsetlabeltimerRect = new RectangleF(CellPt.Y + nameLocation.Y + 60 * scaleFactor, CellPt.X + nameLocation.X,
                                                                        214 * scaleFactor, 25 * scaleFactor);
                            RectangleF setTimeRect = new RectangleF(CellPt.Y + nameLocation.Y + 180 * scaleFactor, CellPt.X + nameLocation.X,
                                                                        214 * scaleFactor, 25 * scaleFactor);
                            RectangleF drawclearlabeltimerRect = new RectangleF(CellPt.Y + nameLocation.Y + 60 * scaleFactor, CellPt.X + nameLocation.X + 20 * scaleFactor,
                                                                        214 * scaleFactor, 25 * scaleFactor);
                            RectangleF clearTimeRect = new RectangleF(CellPt.Y + nameLocation.Y + 180 * scaleFactor, CellPt.X + nameLocation.X + 20 * scaleFactor,
                                                                        214 * scaleFactor, 25 * scaleFactor);
                            /*RectangleF SimTimeRect =                new RectangleF(CellPt.Y + nameLocation.Y + 120 * scaleFactor, CellPt.X + nameLocation.X + 40 * scaleFactor,
                                                                        214 * scaleFactor, 25 * scaleFactor);*/

                            RectangleF SimTimeRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + 55 * scaleFactor, 114 * scaleFactor, 50 * scaleFactor);

                            ML2Timer timerOldElement = (ML2Timer)timersOld[timerOldnumber];
                            ML2Timer timerNewElement = (ML2Timer)timersNew[timerNewnumber];


                            if (SimMode) DrawSimTimer(contactPointer.name, SimTimeRect, drawFont, drawFormat, grfx);/*
                            string SimTimerText = "";
                            if (SimMode)
                            {
                                for (int i = 0; i < SimS2PTimers.Count; i++)
                                {
                                    TimersTimingStruct timerelement = getTimerElement(contactPointer.name, SimS2PTimers);
                                    if (timerelement.timername != "notimerfound")
                                    {
                                        if (timerelement.totaltime > timerelement.timeElapsed)
                                            SimTimerText = "Timing " + (timerelement.totaltime - timerelement.timeElapsed) +
                                                "s (" + timerelement.totaltime + "s timer)";
                                        grfx.DrawString(SimTimerText, drawFont, SimBrush, SimTimeRect, drawFormat);
                                    }
                                }
                                for (int i = 0; i < SimS2DTimers.Count; i++)
                                {
                                    TimersTimingStruct timerelement = getTimerElement(contactPointer.name, SimS2DTimers);
                                    if (timerelement.timername != "notimerfound")
                                    {
                                        if (timerelement.totaltime > timerelement.timeElapsed)
                                            SimTimerText = "Timing " + (timerelement.totaltime - timerelement.timeElapsed) +
                                                "s (" + timerelement.totaltime + "s timer)";
                                        grfx.DrawString(SimTimerText, drawFont, SimBrush, SimTimeRect, drawFormat);
                                    }
                                }
                            }*/

                            //grfx.DrawString("Slow to Pick: ", drawFont, CommonBrush, drawsetlabeltimerRect, drawFormat);
                            grfx.DrawString("Slow to go High: ", drawFont, CommonBrush, drawsetlabeltimerRect, drawFormat);
                            if (timerOldElement.setTime.ToString() == timerNewElement.setTime.ToString())
                                grfx.DrawString(timerOldElement.setTime.ToString(), drawFont, CommonBrush, setTimeRect, drawFormat);
                            else
                            {
                                grfx.DrawString(timerOldElement.setTime.ToString(), drawFont, BlueBrush, setTimeRect, drawFormat);
                                grfx.DrawString("                          " + timerNewElement.setTime.ToString(), drawFont, RedBrush, setTimeRect, drawFormat);
                            }
                            //grfx.DrawString("Slow to Drop: ", drawFont, CommonBrush, drawclearlabeltimerRect, drawFormat);
                            grfx.DrawString("Slow to go Low: ", drawFont, CommonBrush, drawclearlabeltimerRect, drawFormat);
                            if (timerOldElement.clearTime.ToString() == timerNewElement.clearTime.ToString())
                                grfx.DrawString(timerOldElement.clearTime.ToString(), drawFont, CommonBrush, clearTimeRect, drawFormat);
                            else
                            {
                                grfx.DrawString(timerOldElement.clearTime.ToString(), drawFont, BlueBrush, clearTimeRect, drawFormat);
                                grfx.DrawString("                          " + timerNewElement.clearTime.ToString(), drawFont, RedBrush, clearTimeRect, drawFormat);
                            }
                        }
                    }
                }
            }
            catch { MessageBox.Show("Error drawing timer", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void DrawSimTimer(string contactname, RectangleF SimTimeRect, Font drawFont, StringFormat drawFormat, Graphics grfx)
        {
            string SimTimerText = "";
            for (int i = 0; i < SimS2PTimers.Count; i++)
            {
                TimersTimingStruct timerelement = getTimerElement(contactname, SimS2PTimers);
                if (timerelement.timername != "notimerfound")
                {
                    if (timerelement.totaltime > timerelement.timeElapsed)
                        SimTimerText = "Timing " + (timerelement.totaltime - timerelement.timeElapsed) +
                            "s (" + timerelement.totaltime + "s timer)";
                    grfx.DrawString(SimTimerText, drawFont, SimBrush, SimTimeRect, drawFormat);
                    //grfx.DrawRectangle(GreenPeng, SimTimeRect.X, SimTimeRect.Y, SimTimeRect.X + 20,
                    //    SimTimeRect.Y + SimTimeRect.Height * (timerelement.timeElapsed/timerelement.totaltime));
                    //grfx.DrawLine(GreyPeng, SimTimeRect.Y, SimTimeRect.X + SimTimeRect.Width * (timerelement.timeElapsed/timerelement.totaltime),
                    // SimTimeRect.Y + SimTimeRect.Height, SimTimeRect.X + SimTimeRect.Width);                        
                }
            }
            for (int i = 0; i < SimS2DTimers.Count; i++)
            {
                TimersTimingStruct timerelement = getTimerElement(contactname, SimS2DTimers);
                if (timerelement.timername != "notimerfound")
                {
                    if (timerelement.totaltime > timerelement.timeElapsed)
                        SimTimerText = "Timing " + (timerelement.totaltime - timerelement.timeElapsed) +
                            "s (" + timerelement.totaltime + "s timer)";
                    grfx.DrawString(SimTimerText, drawFont, SimBrush, SimTimeRect, drawFormat);
                }
            }
        }

        private void frmMChild_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (leftMouseDown == true) leftMouseDown = false;
            if (rightMouseDown == true) rightMouseDown = false;
            if (middleMouseDown == true)
            {
                scaleOffset.Y = -scaleClick.Y + Cursor.Position.X;
                middleMouseDown = false;
            }
        }

        private void frmMChild_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                click.X = (int)(Cursor.Position.Y / scaleFactor) - blueOffset.X;
                click.Y = (int)(Cursor.Position.X / scaleFactor) - blueOffset.Y;
                leftMouseDown = true;
            }

            if (e.Button == MouseButtons.Right)
            {
                click.X = (int)(Cursor.Position.Y / scaleFactor) - redOffset.X;
                click.Y = (int)(Cursor.Position.X / scaleFactor) - redOffset.Y;
                rightMouseDown = true;
                contactHighlight = ""; Invalidate();
            }

            if (e.Button == MouseButtons.Middle)
            {
                
                scaleClick.X = Cursor.Position.Y;
                scaleClick.Y = Cursor.Position.X;
                OldBlueOffset = blueOffset;
                OldRedOffset = redOffset;
                scaleFactorClick = scaleFactor;
                middleMouseDown = true;
            }
            if ((e.Button == MouseButtons.Left) && (e.Clicks == 1))
            {
                int oldIndex = -1;
                int newIndex = -1;
                string rungName = "";
                if (CellName != "") rungName = CellName;
                if (linkSelected != 0) rungName = linkName;
                if (rungName != "")
                {
                    for (int i = 0; i < trueInterlockingOldPointer.Count; i++)
                    {
                        ArrayList rungPointer = (ArrayList)trueInterlockingOldPointer[i];
                        if ((string)rungPointer[rungPointer.Count - 1] == rungName) oldIndex = i;
                    }
                    for (int j = 0; j < trueInterlockingNewPointer.Count; j++)
                    {
                        ArrayList rungPointer = (ArrayList)trueInterlockingNewPointer[j];
                        if ((string)rungPointer[rungPointer.Count - 1] == rungName) newIndex = j;
                    }

                    rungNameTransition = rungName;
                    inputToggle = rungName;
                    if ((newIndex == -1) && (oldIndex != -1))
                    {
                        oldindexTransition = oldIndex;
                        newindexTransition = oldIndex;
                        windowTypeTransition = "All Old";
                        timer4.Enabled = true; LoadTimeTransition = DateTime.Now; showgridlines = false;
                        //LaunchChildWindow(trueInterlockingOldPointer, trueInterlockingOldPointer, oldIndex, oldIndex,
                        //       scaleFactor, "All Old", drawFnt, rungName);
                    }
                    if ((oldIndex == -1) && (newIndex != -1))
                    {
                        oldindexTransition = newIndex;
                        newindexTransition = newIndex;
                        windowTypeTransition = "All New";
                        timer4.Enabled = true; LoadTimeTransition = DateTime.Now; showgridlines = false;
                        //LaunchChildWindow(trueInterlockingOldPointer, trueInterlockingNewPointer, newIndex, newIndex,
                        //        scaleFactor, "All New", drawFnt, rungName);
                    }
                    if ((oldIndex != -1) && (newIndex != -1))
                    {
                        oldindexTransition = oldIndex;
                        newindexTransition = newIndex;
                        windowTypeTransition = "Normal";
                        timer4.Enabled = true; LoadTimeTransition = DateTime.Now; showgridlines = false;
                        //LaunchChildWindow(trueInterlockingOldPointer, trueInterlockingNewPointer, oldIndex, newIndex,
                        //        scaleFactor, "Normal", drawFnt, rungName);
                    }
                    //if((oldIndex == -1)&&(newIndex == -1)) ;//MessageBox.Show("This contact is an external input. Under construction", "Inputs under construction");
                }
            }
        }

        private void LaunchChildWindow(ArrayList interlockingOldPointer, ArrayList interlockingNewPointer, ArrayList timersOldPointer, ArrayList timersNewPointer, int oldIndex, int newIndex,
            float scaleF, string drawMde, Font drawFt, string name)
        {
            frmMChild objfrmMChild = new frmMChild(interlockingOldPointer, interlockingNewPointer, timersOldPointer, timersNewPointer, oldIndex, newIndex,
                scaleFactor, drawMde, drawFnt, this.Text, grid, showtimers, HighColor, LowColor);
            objfrmMChild.Size = new Size(objfrmMChild.RecommendedWidthofWindow(newIndex), objfrmMChild.RecommendedHeightofWindow(newIndex));
            objfrmMChild.Location = new System.Drawing.Point(1, 1);
            objfrmMChild.Text = name;
            objfrmMChild.MdiParent = this.MdiParent;
            objfrmMChild.Show();
        }

        public void InvalidateForm()
        {
            Invalidate();
        }

        private void frmMChild_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Point ScaledCursor = new Point((int)(Cursor.Position.X / scaleFactor), (int)(Cursor.Position.Y / scaleFactor));

            if(!rightMouseDown && !middleMouseDown && !leftMouseDown)
                unscaledlocation = e.Location;

            if (rightMouseDown == true)
            {
                redOffset.X = -click.X + ScaledCursor.Y;
                redOffset.Y = -click.Y + ScaledCursor.X;

                snapDistX = ((float)(redOffset.X - blueOffset.X)) / (cellHeightStatic)
                    - ((int)((redOffset.X - blueOffset.X) / (cellHeightStatic)));
                if (snapDistX > 0)
                {
                    if (snapDistX < 0.20)
                        redOffset.X = blueOffset.X + ((int)(cellHeightStatic) * (int)((redOffset.X - blueOffset.X) / (cellHeightStatic)));
                    if (snapDistX > 0.80)
                        redOffset.X = blueOffset.X + ((int)(cellHeightStatic) * (1 + (int)((redOffset.X - blueOffset.X) / (cellHeightStatic))));
                }
                else
                {
                    snapDistX = -snapDistX;
                    if (snapDistX < 0.20)
                        redOffset.X = blueOffset.X - ((int)(cellHeightStatic) * (int)(-(redOffset.X - blueOffset.X) / (cellHeightStatic)));
                    if (snapDistX > 0.80)
                        redOffset.X = blueOffset.X - ((int)(cellHeightStatic) * (1 + (int)(-(redOffset.X - blueOffset.X) / (cellHeightStatic))));
                }
                snapDistY = ((float)(redOffset.Y - blueOffset.Y)) / (cellWidthStatic)
                    - ((int)((redOffset.Y - blueOffset.Y) / (cellWidthStatic)));
                if (snapDistY > 0)
                {
                    if (snapDistY < 0.10)
                        redOffset.Y = blueOffset.Y + ((int)(cellWidthStatic) * (int)((redOffset.Y - blueOffset.Y) / (cellWidthStatic)));
                    if (snapDistY > 0.90)
                        redOffset.Y = blueOffset.Y + ((int)(cellWidthStatic) * (1 + (int)((redOffset.Y - blueOffset.Y) / (cellWidthStatic))));
                }
                else
                {
                    snapDistY = -snapDistY;
                    if (snapDistY < 0.10)
                        redOffset.Y = blueOffset.Y - ((int)(cellWidthStatic) * (int)(-(redOffset.Y - blueOffset.Y) / (cellWidthStatic)));
                    if (snapDistY > 0.90)
                        redOffset.Y = blueOffset.Y - ((int)(cellWidthStatic) * (1 + (int)(-(redOffset.Y - blueOffset.Y) / (cellWidthStatic))));
                }
                Invalidate();
            }
            if (leftMouseDown == true)
            {
                redOffset.X = redOffset.X - (blueOffset.X + click.X - ScaledCursor.Y);
                redOffset.Y = redOffset.Y - (blueOffset.Y + click.Y - ScaledCursor.X);
                blueOffset.X = -click.X + ScaledCursor.Y;
                blueOffset.Y = -click.Y + ScaledCursor.X;

                gridOffset.Y = (int)(cellWidthStatic * (blueOffset.Y / cellWidthStatic - Math.Round(blueOffset.Y / cellWidthStatic)));
                gridOffset.X = (int)(cellHeightStatic * (blueOffset.X / cellHeightStatic - Math.Round(blueOffset.X / cellHeightStatic)));

                Invalidate();
            }
            if (middleMouseDown == true)
            {
                scaleOffset.X = -scaleClick.X + Cursor.Position.Y;
                scaleOffset.Y = -scaleClick.Y + Cursor.Position.X;
                scaleFactor = scaleFactorClick + (((float)scaleOffset.X) / 100);
                if (scaleFactor < 0.3) scaleFactor = 0.3F;
                Invalidate();
            }

            CellCoord = new Point((int)(1 + (e.Y - (40 + redOffset.X * scaleFactor)) / cellHeight),
                    (int)(1 + (e.X - (40 + redOffset.Y * scaleFactor)) / cellWidth)); // Get the current cell coordinates of mouse pointer.			
            ArrayList rungOldPointer = (ArrayList)interlockingNewPointer[CurrentNewCell];
            OldCellName = CellName;
            CellName = "";
            for (int m = 1; m < rungOldPointer.Count - 1; m++)
            {
                Contact contactPointer = (Contact)rungOldPointer[m];
                if ((contactPointer.x == CellCoord.X) && (contactPointer.y == CellCoord.Y))
                    CellName = contactPointer.name;
            }

            int rungNumber = -1;
            if (((CellCoord != CellCoordPrev) && (CellName != "")) ||
                ((OldCellName != "") && (CellName == "")))
            {
                rungNumber = -1;
                for (int i = 0; i < interlockingOldPointer.Count; i++)
                {
                    ArrayList rungPointer = (ArrayList)interlockingOldPointer[i];
                    if ((string)rungPointer[rungPointer.Count - 1] == CellName)
                        rungNumber = (int)rungPointer[0];
                }
                if (rungNumber == -1)
                {

                    HighlightBrush = new SolidBrush(Color.LightGray);
                    HighlightPen = new Pen(Color.LightGray);
                    if (SimMode)
                    {
                        statusBar.Text = "Click to toggle input";
                        HighlightBrush = new SolidBrush(Color.DarkGray);
                        HighlightPen = new Pen(Color.DarkGray);
                    }
                }
                else
                {
                    HighlightBrush = new SolidBrush(Color.DarkOrchid);
                    HighlightPen = new Pen(Color.DarkOrchid);

                    int oldIndex = CurrentOldCell + 1;
                    int newIndex = CurrentNewCell + 1;
                    statusBar.Text = "rungNumber: {" + rungNumber.ToString() + ", " + oldIndex.ToString() + ", " + newIndex.ToString() + "}";
                    if (rungNumber > interlockingOldPointer.Count)
                        if (oldIndex == newIndex)
                            statusBar.Text = newIndex.ToString() + ": " + rungOldPointer[rungOldPointer.Count - 1].ToString();
                        else statusBar.Text = oldIndex.ToString() + " -> " + newIndex.ToString() + ": " + rungOldPointer[rungOldPointer.Count - 1].ToString();
                    else statusBar.Text = rungNumber.ToString() + ": " + CellName;
                }
                CellCoordPrev = CellCoord;
                Invalidate();
            }

            ////////// 	Link Hover
            if ((string)rungOldPointer[rungOldPointer.Count - 1] == CellName)
            {
                timer1.Enabled = true;
                lastLinkHover = DateTime.Now; // If Mouse is hovering over the rung show links
                showLinks = true;
            }
            else
            {
                if ((DateTime.Compare(lastLinkHover.AddMilliseconds(700), DateTime.Now) < 0) || ("" != CellName))
                    if (showLinks == true) // If the mouse has moved away from the rung, wait 700 msecs before removing links
                    {
                        showLinks = false; Invalidate();
                        timer1.Enabled = false;
                    }
            }
            if (showLinks)
            {
                oldLinkSelected = linkSelected;
                linkSelected = 0;
                for (int c = 0; c < interlockingOldPointer.Count; c++)
                    if (e.X > linksPoint.Y + 130 * scaleFactor)
                        if (e.X < linksPoint.Y + 130 * scaleFactor + cellWidthStatic * scaleFactor)
                            if (e.Y > linksPoint.X + c * 25 * scaleFactor)
                                if (e.Y < linksPoint.X + c * 25 * scaleFactor + 25 * scaleFactor)
                                {
                                    linkSelected = c;
                                    lastLinkHover = DateTime.Now;
                                }
                if (oldLinkSelected != linkSelected) Invalidate();

                /*statusBar.Text = "(" + CellCoord.X.ToString() + ", " + CellCoord.Y.ToString() + ") "  
                    + CellName + ", " + CellCoordPrev + ", RungNumber: " + rungNumber
                    + "scaleFactor: " + scaleFactor.ToString()
                    + "// linksPoint: {" + linksPoint.X.ToString()
                    + "," + linksPoint.Y.ToString()
                    + "}// e: {" + e.X.ToString()
                    + "," + e.Y.ToString()
                    + "} // selected: " + linkName.ToString()
                    ;*/
            }

            /*
                        statusBar.Text = "(" + CellCoord.X.ToString() + ", " + CellCoord.Y.ToString() + ") "  
                            + CellName + ", " + CellCoordPrev + ", RungNumber: " + rungNumber
                            //+ "..." + contactPointer.x + ", " + contactPointer.y + "..." 
                            //  + "(" + ScaledCursor.X.ToString() + ", " + ScaledCursor.Y.ToString() + ") " 
                            //  + "(" + e.X.ToString() + ", " + e.Y.ToString() + ") " 
                            //  + "blueOfst: " + blueOffset.ToString() + ", " 
                            //	+ "redOfst: " + redOffset.ToString() + ", " 
                            //	+ "cllHt: " + cellHeight.ToString() + ", "
                            //	+ "cllWth: " + cellWidth.ToString() + ", "
                            //	+ "cllHtStatic: " + cellHeightStatic.ToString() + ", "
                            //	+ "cllWthStatic: " + cellWidthStatic.ToString() + ", "
                            + "scaleFactor: " + scaleFactor.ToString() ;*/
        }

        public void applyzoom(float scalefactormultiplyer, float originx, float originy)
        {
            if (originx == -1)
            {
                float oldscalefactor = scaleFactor;
                scaleFactor = scaleFactor * scalefactormultiplyer;
                endScaleFactor = scaleFactor;
                redOffset.X -= (int)((ClientSize.Height * 0.5 / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                redOffset.Y -= (int)((ClientSize.Width * 0.5 / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                blueOffset.X -= (int)((ClientSize.Height * 0.5 / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                blueOffset.Y -= (int)((ClientSize.Width * 0.5 / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                Invalidate();
            }
            else
            {
                float oldscalefactor = scaleFactor;
                scaleFactor = scaleFactor * scalefactormultiplyer;
                endScaleFactor = scaleFactor;
                /*
                redOffset.X += (int)((ClientSize.Width * 0.5 / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                redOffset.Y += (int)((ClientSize.Height * 0.5 / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                blueOffset.X += (int)((ClientSize.Width * 0.5 / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                blueOffset.Y += (int)((ClientSize.Height * 0.5 / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));*/

                redOffset.X -= (int)((originy / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                redOffset.Y -= (int)((originx / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                blueOffset.X -= (int)((originy / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                blueOffset.Y -= (int)((originx / oldscalefactor) * (1 - (oldscalefactor / scaleFactor))); 

                Invalidate();

                /*
                 * 
                totalpan.X += (int)((originx / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                totalpan.Y += (int)((originy / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                pan.X = totalpan.X;
                pan.Y = totalpan.Y;
                panSinceClick.X = 0;
                panSinceClick.Y = 0;*/
            }
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {   // Time delay before links are displayed
            if (linkSelected != 0) lastLinkHover = DateTime.Now;
            if (DateTime.Compare(lastLinkHover.AddMilliseconds(700), DateTime.Now) < 0)
                if (showLinks == true)
                {
                    showLinks = false; Invalidate();
                }

        }

        private void timer2_Tick(object sender, System.EventArgs e)
        {   // Sliding entrance of rungs
            TimeSpan difference = DateTime.Now - LoadTime;
            int windowWidth = this.Size.Width;
            int windowHeight = this.Size.Height;
            float diff = ((float)difference.Milliseconds) / 300;
            scaleFactor = endScaleFactor;

            redEntrance.X = 0;// (int)(windowHeight * (1 - (diff * diff)) * CurrentOldCell / interlockingNewPointer.Count);
            redEntrance.Y = (int)(-windowWidth * ((1 - diff) * (1 - diff)));
            statusBar.Text = windowWidth.ToString();

            /* if((diff > 1) && (diff < 2)) myGreyPen = new Pen(Color.FromArgb(
                     (int) (Color.Silver.R/(2-diff)),
                     (int) (Color.Silver.G/(2-diff)),
                     (int) (Color.Silver.B/(2-diff))));*/
            if (difference.Milliseconds > 300)
            {
                redEntrance.X = 0;
                redEntrance.Y = 0;

                timer2.Enabled = false;
            }
            Invalidate();
        }

        private void timer3_Tick(object sender, System.EventArgs e)
        {   // Preview note entrance
            TimeSpan difference = DateTime.Now - PreviewRequestTime;
            /*if(difference.Milliseconds > 300)
			{
                previewEntrance.X = 0;
                previewEntrance.Y = 0;
                timer3.Enabled = false;	
				previewTimed = true;
			}*/
            int windowWidth = this.Size.Width;
            int windowHeight = this.Size.Height;
            float diff = ((float)difference.Milliseconds) / 300;
            if (previewTimed) //Not finished timing and preview has not made entrance yet
            {
                previewEntrance.X = (int)(windowHeight * (1 - (diff * diff)));
                previewEntrance.Y = (int)(-windowWidth * ((1 - diff) * (1 - diff)));
                if (DateTime.Now > PreviewRequestTime + TimeSpan.FromMilliseconds(300))
                {
                    previewEntrance.X = 0;
                    previewEntrance.Y = 0;
                    timer3.Enabled = false;
                }
                Invalidate();
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            TimeSpan difference = DateTime.Now - LoadTimeTransition;
            int windowWidth = this.Size.Width;
            int windowHeight = this.Size.Height;
            float diff = ((float)difference.Milliseconds) / 250;
            scaleFactor = endScaleFactor;

            redEntrance.X = 0;// (int)(windowHeight * (1 - ((1 - diff))) * CurrentOldCell / interlockingNewPointer.Count);
            redEntrance.Y = (int)(-windowWidth * (diff));
            statusBar.Text = windowWidth.ToString();


            if (difference.Milliseconds > 250)
            {
                redEntrance.X = 0;
                redEntrance.Y = 0;
                LaunchChildWindow(trueInterlockingOldPointer, trueInterlockingNewPointer, timersOld, timersNew, oldindexTransition, newindexTransition,
                                scaleFactor, windowTypeTransition, drawFnt, rungNameTransition);
                timer4.Enabled = false; showgridlines = true;
            }
            Invalidate();
        }

        private void frmMChild_DoubleClick(object sender, EventArgs e)
        {
            //rungNameTransition
            //if ( == MouseButtons.Left)
            //grid = !grid;
            //CellName
            int menulength = contextMenuStrip1.Items.Count;
            if (contextMenuStrip1.Items.Count > 1)
                for (int i = 0; i < menulength; i++)
                    contextMenuStrip1.Items.RemoveAt(0);
            string contactname = CellName;
            contextMenuStrip1.Items.Add(contactname + " used in:");
            ArrayList Coils = new ArrayList();
            try
            {
                getRungs(Coils, trueInterlockingNewPointer);
                for (int r = 0; r < trueInterlockingNewPointer.Count; r++)
                {
                    ArrayList rungPointer = (ArrayList)trueInterlockingNewPointer[r];
                    for (int k = 1; k < rungPointer.Count - 1; k++)
                    {
                        Contact contact = (Contact)rungPointer[k];
                        if (contact.name == contactname)
                        {
                            contextMenuStrip1.Items.Add(rungPointer[rungPointer.Count - 1].ToString());
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error populating context menu ", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            contextMenuStrip1.Show(Cursor.Position);


            //Invalidate();
        }

        private void getRungs(ArrayList Coils, ArrayList interlockingNewPointer)
        {
            string name = "";
            for (int i = 0; i < interlockingNewPointer.Count; i++)
            {
                ArrayList rungPointer = (ArrayList)interlockingNewPointer[i];
                name = (string)rungPointer[rungPointer.Count - 1];
                Coils.Add(name);
            }
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            if (grid)
            {
                seqcounter++;
                if (SeqList.Count - 1 < seqcounter) seqcounter = 0;
                Invalidate();
            }
            else seqcounter = 0;
            animate++;
            if (animate > 5) animate = 0;
        }

        private void frmMChild_Click(object sender, EventArgs e)
        {

        }


        private void frmMDIMain_MouseWheel(object sender, MouseEventArgs e)
        {
            statusBar.Text = e.Delta.ToString();
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            ShowRungWindow(item.ToString());
        }

        private void frmMChild_KeyDown(object sender, KeyEventArgs e)
        {
            if (((e.KeyCode == Keys.PageDown) || (e.KeyCode == Keys.Down)) && (e.Modifiers != Keys.Alt) && (e.Modifiers != Keys.Control) && (e.Modifiers != Keys.Shift))
            {
                if (interlockingNewPointer.Count - 1 > CurrentNewCell)
                {
                    CurrentNewCell++; //if (CurrentNewCell > interlockingNewPointer.Count) CurrentNewCell = interlockingNewPointer.Count;
                    ArrayList rungNewPointer = (ArrayList)interlockingNewPointer[CurrentNewCell - 1];
                    string contactname = (string)rungNewPointer[rungNewPointer.Count - 1];
                    if (findRung(interlockingOldPointer, contactname) != -1)
                        CurrentOldCell = findRung(interlockingOldPointer, contactname);
                }
            }
            if (((e.KeyCode == Keys.PageUp) || (e.KeyCode == Keys.Up)) && (e.Modifiers != Keys.Alt) && (e.Modifiers != Keys.Control) && (e.Modifiers != Keys.Shift))
            {

                if (interlockingNewPointer.Count - 1 > CurrentNewCell)
                {
                    CurrentNewCell--; if (CurrentNewCell < 0) CurrentNewCell = 0;
                    ArrayList rungNewPointer = (ArrayList)interlockingNewPointer[CurrentNewCell - 1];
                    string contactname = (string)rungNewPointer[rungNewPointer.Count - 1];
                    if (findRung(interlockingOldPointer, contactname) != -1)
                        CurrentOldCell = findRung(interlockingOldPointer, contactname);
                }
            }
            e.Handled = true;
        }

        private void ShowRungWindow(string rungname)
        {
            int newIndex = findRung(trueInterlockingNewPointer, rungname);
            int oldIndex = findRung(trueInterlockingOldPointer, rungname);
            if (newIndex == -1)
            {
                frmMChild objfrmMChild = new frmMChild(trueInterlockingOldPointer, trueInterlockingNewPointer, timersOld, timersNew, oldIndex - 1,
                    oldIndex - 1, scaleFactor/*0.75F*/, "All Old", drawFnt, "", false, true, HighColor, LowColor);
                objfrmMChild.Size = new Size(objfrmMChild.RecommendedWidthofWindow(oldIndex - 1), objfrmMChild.RecommendedHeightofWindow(oldIndex - 1));
                objfrmMChild.Location = new System.Drawing.Point(1, 1);
                objfrmMChild.Text = rungname;
                objfrmMChild.MdiParent = this.MdiParent;
                objfrmMChild.Show();
            }
            else
            {
                if (oldIndex == -1)
                {
                    frmMChild objfrmMChild = new frmMChild(trueInterlockingOldPointer, trueInterlockingNewPointer, timersOld, timersNew, newIndex - 1,
                        newIndex - 1, scaleFactor/*0.75F*/, "All New", drawFnt, "", false, true, HighColor, LowColor);
                    objfrmMChild.Size = new Size(objfrmMChild.RecommendedWidthofWindow(newIndex - 1), objfrmMChild.RecommendedHeightofWindow(newIndex - 1));
                    objfrmMChild.Location = new System.Drawing.Point(1, 1);
                    objfrmMChild.Text = rungname;
                    objfrmMChild.MdiParent = this.MdiParent;
                    objfrmMChild.Show();
                }
                else
                {
                    frmMChild objfrmMChild = new frmMChild(trueInterlockingOldPointer, trueInterlockingNewPointer, timersOld, timersNew, oldIndex - 1,
                        newIndex - 1, scaleFactor/*0.75F*/, "Normal", drawFnt, "", false, true, HighColor, LowColor);
                    objfrmMChild.Size = new Size(objfrmMChild.RecommendedWidthofWindow(newIndex - 1), objfrmMChild.RecommendedHeightofWindow(newIndex - 1));
                    objfrmMChild.Location = new System.Drawing.Point(1, 1);
                    objfrmMChild.Text = rungname;
                    objfrmMChild.MdiParent = this.MdiParent;
                    objfrmMChild.Show();
                }
            }
        }


    }

    public struct Contact
    {
        public string name;
        public int rungindex;
        public int inputindex;
        public int x;
        public int y;
        public string typeOfCell; //Contact, Coil, Horizontal Shunt, Vertical Shunt, Empty cell, End Contact
        public bool NormallyClosed;
        public bool topLink;
        public int topLinkindex;
        public bool bottomLink;
        public int bottomLinkindex;
        public bool leftLink;
        public int leftLinkindex;
        public int rightLinkindex;
        public bool live;//Used simulation
    }

    public struct Rung
    {
        public List<Contact> Contacts;
    }


    public struct CoilState
    {
        public string name;
        public bool state;
    }

    public struct ContactAnalysis
    {
        public string name;
        public int index;
        public ArrayList usedin;
    }

    public struct ML2Timer
    {
        public string timerName;
        public string setTime; // Delay to Pick
        public string clearTime; // Delay to Drop
        public DateTime setStartTime;
        public DateTime clearStartTime;
    }

    public struct ReserveTimer
    {
        public string triggerName;
        public string triggerAccess;
        public string outputName;
        public string outputAccess;
        public string duration;
        public string durationSetting;
    }

    public struct UserTimer
    {
        public string triggerName;
        public string outputName;
        public string duration;
    }

    public struct ReserveLatches
    {
        public string latchName;
        public string latchAccess;
    }

    public struct UserLatches
    {
        public string latchNumber;
        public string latchName;
    }

    public struct TimersTimingStruct
    {
        public string timername;
        public DateTime timerstarttime;
        public int timeElapsed;
        public int totaltime;
    }
}
