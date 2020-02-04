using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;
using System.IO;


namespace Logic_Navigator
{
    public class frmMChild_Visualiser : System.Windows.Forms.Form
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

        private Point redOffset = new Point(0, 0);//700);
        private Point blueOffset = new Point(0, 0);//700);
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
        
        StringComparison sc = StringComparison.OrdinalIgnoreCase;

        string typedinput = "";

        public ArrayList interlockingtyped = new ArrayList();
        bool gotsomething = false;
        CultureInfo ci = new CultureInfo("en-US");
        private System.IO.StreamReader SR;
        private int counter = 0;
        private Color attentionColor = Color.HotPink;
        SolidBrush attBrush = new SolidBrush(Color.HotPink);

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
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private Timer timer4;
        private Timer timer5;
        private ContextMenuStrip contextMenuStrip1;
        private RichTextBox code;
        private OpenFileDialog openFileDialog1;
        private SplitContainer splitContainer1;
        private TextBox codeout;
        private ToolStrip toolStrip1;
        private ToolStripButton OpenFile;
        private ToolStripButton toolStripButton1;
        private PictureBox rungrendition;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem goToSelectedRungToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem1;
        private ToolStripMenuItem pasteToolStripMenuItem1;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem fontToolStripMenuItem;
        private FontDialog fontDialog1;
        private System.ComponentModel.IContainer components;

        public frmMChild_Visualiser(ArrayList interlockingOld, ArrayList interlockingNew, ArrayList timersOldPointer, ArrayList timersNewPointer, int imageOldIndex, int imageNewIndex,
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMChild_Visualiser));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.timer5 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.code = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rungrendition = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.OpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.codeout = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToSelectedRungToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.contextMenuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rungrendition)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.goToSelectedRungToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(178, 70);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // code
            // 
            this.code.AcceptsTab = true;
            this.code.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.code.AutoWordSelection = true;
            this.code.BackColor = System.Drawing.Color.WhiteSmoke;
            this.code.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.code.Location = new System.Drawing.Point(27, 1);
            this.code.Name = "code";
            this.code.Size = new System.Drawing.Size(1243, 264);
            this.code.TabIndex = 4;
            this.code.Text = "{Write/Paste logic in here, e.g ASSIGN   A + B TO C;  or open a file}";
            this.code.SelectionChanged += new System.EventHandler(this.code_SelectionChanged);
            this.code.Click += new System.EventHandler(this.code_Click);
            this.code.TextChanged += new System.EventHandler(this.code_TextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Logic file|*.ml2;*.gn2;*.mlk;*.vtl;*.lsv;*.ini|ML2 file|*.ml2|GN2 file|*.gn2|MLK " +
    "file|*.mlk|VTL file|*.vtl|LSV file|*.lsv|FEP file|*.ini|All Files|*.*";
            this.openFileDialog1.Title = "Open ML2/GN2/MLK/VTL/LSV/TXT file";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rungrendition);
            this.splitContainer1.Panel1.Margin = new System.Windows.Forms.Padding(100);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.code);
            this.splitContainer1.Panel2.Margin = new System.Windows.Forms.Padding(100, 0, 0, 0);
            this.splitContainer1.Size = new System.Drawing.Size(1272, 618);
            this.splitContainer1.SplitterDistance = 345;
            this.splitContainer1.TabIndex = 11;
            // 
            // rungrendition
            // 
            this.rungrendition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rungrendition.Location = new System.Drawing.Point(-2, -2);
            this.rungrendition.Name = "rungrendition";
            this.rungrendition.Size = new System.Drawing.Size(1272, 347);
            this.rungrendition.TabIndex = 1;
            this.rungrendition.TabStop = false;
            this.rungrendition.Paint += new System.Windows.Forms.PaintEventHandler(this.rungrendition_Paint);
            this.rungrendition.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rungrendition_MouseDown);
            this.rungrendition.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rungrendition_MouseMove);
            this.rungrendition.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rungrendition_MouseUp);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFile,
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(24, 265);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            // 
            // OpenFile
            // 
            this.OpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenFile.Image = ((System.Drawing.Image)(resources.GetObject("OpenFile.Image")));
            this.OpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(23, 20);
            this.OpenFile.Text = "Open File";
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click_1);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 20);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // codeout
            // 
            this.codeout.AcceptsReturn = true;
            this.codeout.AcceptsTab = true;
            this.codeout.Location = new System.Drawing.Point(556, 282);
            this.codeout.Multiline = true;
            this.codeout.Name = "codeout";
            this.codeout.Size = new System.Drawing.Size(255, 44);
            this.codeout.TabIndex = 5;
            this.codeout.Visible = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "ml2";
            this.saveFileDialog1.Filter = "Logic file|*.ml2;*.gn2;*.mlk;*.vtl;*.lsv;*.ini|ML2 file|*.ml2|GN2 file|*.gn2|MLK " +
    "file|*.mlk|VTL file|*.vtl|LSV file|*.lsv|FEP file|*.ini|All Files|*.*";
            this.saveFileDialog1.Title = "Save ML2 File";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // goToSelectedRungToolStripMenuItem
            // 
            this.goToSelectedRungToolStripMenuItem.Name = "goToSelectedRungToolStripMenuItem";
            this.goToSelectedRungToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.goToSelectedRungToolStripMenuItem.Text = "&Go to selected rung";
            this.goToSelectedRungToolStripMenuItem.Click += new System.EventHandler(this.goToSelectedRungToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1272, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem1,
            this.pasteToolStripMenuItem1});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // copyToolStripMenuItem1
            // 
            this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            this.copyToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.copyToolStripMenuItem1.Text = "&Copy";
            this.copyToolStripMenuItem1.Click += new System.EventHandler(this.copyToolStripMenuItem1_Click);
            // 
            // pasteToolStripMenuItem1
            // 
            this.pasteToolStripMenuItem1.Name = "pasteToolStripMenuItem1";
            this.pasteToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.pasteToolStripMenuItem1.Text = "&Paste";
            this.pasteToolStripMenuItem1.Click += new System.EventHandler(this.pasteToolStripMenuItem1_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // fontToolStripMenuItem
            // 
            this.fontToolStripMenuItem.Name = "fontToolStripMenuItem";
            this.fontToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fontToolStripMenuItem.Text = "Font";
            this.fontToolStripMenuItem.Click += new System.EventHandler(this.fontToolStripMenuItem_Click);
            // 
            // frmMChild_Visualiser
            // 
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1272, 617);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.codeout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMChild_Visualiser";
            this.Text = "Logic Visualiser";
            this.Load += new System.EventHandler(this.frmMChild_Load);
            this.SizeChanged += new System.EventHandler(this.frmMChild_Visualiser_SizeChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmMChild_Visualiser_DragDrop);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMChild_Paint);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.frmMDIMain_MouseWheel);
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rungrendition)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void frmMChild_Load(object sender, System.EventArgs e)
        {
            // Magic code that allows fast screen drawing
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            code.SelectionTabs = new int[] { 100, 200, 300, 400 };

            /*textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            col.Add("Foo");
            col.Add("Bar");
            textBox1.AutoCompleteCustomSource = col;*/


            //eGraphics = rungrendition.CreateGraphics();

        }

        private void frmMChild_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //paintRungs();
        }

        private void paintRungs(Graphics eGraphics)
        {
            if (interlockingtyped.Count > 0)
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
                //eGraphics.Clear(Color.White);
                //if (drawMode == "Normal")
                    drawRungs(eGraphics, BlueBrush, RedBrush, CommonBrush, WhiteBrush, BluePen, RedPen, BlackPen, myWhitePen, reddiff /*redOffset*/, bluediff /*blueOffset*/);
                //if (drawMode == "All New")
                  //  drawRungs(eGraphics, RedBrush, RedBrush, RedBrush, WhiteBrush, RedPen, RedPen, RedPen, myWhitePen, reddiff /*redOffset*/, bluediff /*blueOffset*/);
                //if (drawMode == "All Old")
                  //  drawRungs(eGraphics, BlueBrush, BlueBrush, BlueBrush, WhiteBrush, BluePen, BluePen, BluePen, myWhitePen, reddiff /*redOffset*/, bluediff /*blueOffset*/);
                if ((preview && previewTimed) && (findRung(interlockingtyped, previewRungName) != -1) && (findRung(interlockingtyped, previewRungName) != -1))
                {// Draw a preview of 'previewRungName'             
                    showingPreview = true;
                    tempNewCell = CurrentNewCell;
                    tempOldCell = CurrentOldCell;
                    tempScaleFactor = scaleFactor;
                    //scaleFactor = scaleFactor;// *0.8f;				
                    CurrentNewCell = findRung(interlockingtyped, previewRungName) - 1;
                    CurrentOldCell = findRung(interlockingtyped, previewRungName) - 1;
                    int previewOffset = getHeight(interlockingtyped, this.Text.ToString());
                    if (previewOffset < getHeight(interlockingtyped, this.Text.ToString())) previewOffset = getHeight(interlockingtyped, this.Text.ToString());
                    Point previewRung = new Point(15 + (int)((redOffset.X + (previewOffset * cellHeightStatic)) * tempScaleFactor / scaleFactor),
                                                 -10 + (int)((redOffset.Y + 10) * tempScaleFactor / scaleFactor) + previewEntrance.Y);
                    showgridlines = false;
                    drawMat(eGraphics, previewRung, previewRungName);   // Draw a Post it note for the preview
                    drawRungs(eGraphics, BlueBrush, RedBrush, CommonBrush, WhiteBrush, BluePen, RedPen, BlackPen, myWhitePen, previewRung, previewRung);
                    showgridlines = true;
                    scaleFactor = tempScaleFactor;
                    CurrentNewCell = tempNewCell;
                    CurrentOldCell = tempOldCell;
                    showingPreview = false;
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

        private void drawMat(Graphics grfx, Point offset, string previewRungName)
        {

            SolidBrush PostItBrush = new SolidBrush(Color.Cornsilk);
            Pen BlackPen = new Pen(Color.Black);
            Pen GreyPen = new Pen(Color.DarkGray);
            Point CellPt = new Point(40 - 5 + (int)(offset.X * scaleFactor),
                40 - 5 + (int)(offset.Y * scaleFactor));
            int oldCellsHigh = getHeight(interlockingtyped, previewRungName);
            int newCellsHigh = getHeight(interlockingtyped, previewRungName);
            int cellsHigh = 0;
            if (oldCellsHigh > newCellsHigh) cellsHigh = oldCellsHigh; else cellsHigh = newCellsHigh;
            int oldCellsWidth = getWidth(interlockingtyped, previewRungName);
            int newCellsWidth = getWidth(interlockingtyped, previewRungName);
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
                                                                				
            SolidBrush LinkBoardBrush = new SolidBrush(Color.Cornsilk);
            SolidBrush GreyBrush = new SolidBrush(Color.Gray);
            SolidBrush GreenBrush = new SolidBrush(Color.Green);
            SolidBrush linksHighlighted;
            linksHighlighted = GreenBrush;
            myGreyPen.DashStyle = DashStyle.Dot;

            Font drawF = new Font(drawFnt.Name, drawFnt.Size * scaleFactor, drawFnt.Style);
            Font smallFont = new Font(drawFnt.Name, 8 * scaleFactor, drawFnt.Style);
            Font underlineFont = new Font(drawFnt.Name, drawFnt.Size * scaleFactor, drawFnt.Style | FontStyle.Underline);

            Font drawFont = drawF;
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;

            ArrayList rungNewPointer = (ArrayList)interlockingtyped[0];
            ArrayList rungOldPointer = (ArrayList)interlockingtyped[0];

            //statusBar.Text = CellName;

            previewRungName = "";
            //statusBar.Text = redOffset.X + ", " + redOffset.Y;
            //if ((timer2.Enabled == false) && showgridlines && grid)
              //  DrawGridLines(grfx, myGreyPen); // Draw Grid Lines when the the rung is in place
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
                        //if (!showingPreview) DrawLinks(contactPointer, grfx, HighlightPen, drawF, drawFormat, HighlightBrush, redOffsetPaint);//RedOffset
                    }
                    else
                    {
                        //preview = false;
                        DrawCell(contactPointer, grfx, RedPen, drawFont, drawFormat, RedBrush, redOffsetPaint);//RedOffset // Rungs are not aligned
                    }
                }
                else
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
                                
                                previewRungName = contactPointer.name.ToString();
                                if (!showingPreview)
                                    DrawMergedContacts(contactOldPointer, contactPointer, grfx, HighlightPen, HighlightPen, HighlightPen,
                                        underlineFont, drawFormat, HighlightBrush, HighlightBrush, HighlightBrush, redOffsetPaint, blueOffsetPaint);
                                else
                                    DrawMergedContacts(contactOldPointer, contactPointer, grfx, BluePen, RedPen, BlackPen,
                                       drawFont, drawFormat, BlueBrush, RedBrush, CommonBrush, redOffsetPaint, blueOffsetPaint);
                            }
                            //if (!showingPreview) DrawLinks(contactPointer, grfx, HighlightPen, drawF, drawFormat, HighlightBrush, blueOffsetPaint);
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
        
        private void DrawCoil(Contact contactPointer, Font drawFont, Graphics grfx, Pen myPen, Point offset)
        {
            Pen Drawpen = myPen;

            SolidBrush DrawBrush = SimBrush;

            string titlebar = "";
            if (SeqList.Count > 0)
                if (contactPointer.name == SeqList[seqcounter].ToString())
                    Drawpen = GoldPen;

            else { titlebar = contactPointer.name; }
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
                if (Drawpen == SimPenUp)
                {
                    DrawLineInCell(grfx, CellPt, 87, -30, 87, 20, SimPenFlow);
                    DrawLineInCell(grfx, CellPt, 75, -10, 87, -30, SimPenFlow);
                    DrawLineInCell(grfx, CellPt, 99, -10, 87, -30, SimPenFlow);
                }
                if (Drawpen == SimPenDn)
                {

                    DrawLineInCell(grfx, CellPt, 87, -30, 87, 20, SimPenNoFlow);
                    DrawLineInCell(grfx, CellPt, 75, 0, 87, 20, SimPenNoFlow);
                    DrawLineInCell(grfx, CellPt, 99, 0, 87, 20, SimPenNoFlow);
                }
            }
        }

        private void DrawContact(Contact contact, Graphics grfx, Pen myPen, Point offset, bool drawlegs)
        {
            Pen Drawpen = myPen;
            if (SeqList.Count > 0)
                if (contact.name == SeqList[seqcounter].ToString())
                    Drawpen = GoldPen;
            Point CellPt = new Point(40 + (contact.x - 1) * cellHeight + (int)(offset.X * scaleFactor),
                40 + (contact.y - 1) * cellWidth + (int)(offset.Y * scaleFactor));
            if (Drawpen == SimPenUp)
            {
                DrawLineInCell(grfx, CellPt, 20, 25, 100, 25, SimPenFlow);
                DrawLineInCell(grfx, CellPt, 70, 13, 100, 25, SimPenFlow);
                DrawLineInCell(grfx, CellPt, 70, 37, 100, 25, SimPenFlow);
            }
            if (Drawpen == SimPenDn)
            {
                DrawLineInCell(grfx, CellPt, 70, -17, 95, 17, SimPenNoFlow);
                DrawLineInCell(grfx, CellPt, 70, 17, 95, -17, SimPenNoFlow);
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
                RectangleF SimTimeRect = new RectangleF(CellPt.Y + nameLocation.Y + 120 * scaleFactor, CellPt.X + nameLocation.X, 114 * scaleFactor, 25 * scaleFactor);
                grfxDrawString(grfx, contactPointer.name, drawFont, CommonRedBrush, drawRect, drawFormat);
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
                grfxDrawString(grfx, contactPointer.name, drawFont, CommonRedBrush, drawRect, drawFormat);
            }
            if (contactPointer.typeOfCell == "Horizontal Shunt") DrawLineInCell(grfx, CellPt, 0, 0, 110, 0, myPen);
            if (contactPointer.topLink == true) DrawLineInCell(grfx, CellPt, 0, -40, 0, 0, myPen);
            if (contactPointer.bottomLink == true) DrawLineInCell(grfx, CellPt, 0, 50, 0, 0, myPen);
            if (contactPointer.leftLink == true) DrawLineInCell(grfx, CellPt, 0, 0, -10, 0, myPen);
        }

        private void grfxDrawString(Graphics grfx, string name, Font drawFont, SolidBrush CommonRedBrush, RectangleF drawRect, StringFormat drawFormat)
        {
            if ((name.IndexOf('{') != -1) || (name.IndexOf('}') != -1))
                grfx.DrawString(name, drawFont, attBrush, drawRect, drawFormat);
            else
                grfx.DrawString(name, drawFont, CommonRedBrush, drawRect, drawFormat);
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
                        grfxDrawString(grfx, contactPointer.name, drawFont, RedBrush, drawNewRect, drawFormat);
                        grfxDrawString(grfx, contactOldPointer.name, drawFont, BlueBrush, drawOldRect, drawFormat);
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
                        if (contactPointer.NormallyClosed) DrawLineInCell(grfx, CellPt, 49, -(-14 + 11), 57, -14 - 11, RedPen);
                        if (contactOldPointer.NormallyClosed) DrawLineInCell(grfx, CellPt, 49, 34 - (-14), 57, -14 + 34, BluePen);
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
                        grfxDrawString(grfx, rungnumber + contactPointer.name, drawFont, CommonBrush, drawRect, drawFormat);
                        DrawContact(contactPointer, grfx, BlackPen, redOffsetPaint, true);
                        if (SimMode) DrawSimTimer(contactPointer.name, SimTimeRect, drawFont, drawFormat, grfx);
                        if ((contactPointer.NormallyClosed == true) && (contactOldPointer.NormallyClosed == true))
                            linkPen = BlackPen;
                        if ((contactPointer.NormallyClosed == false) && (contactOldPointer.NormallyClosed == true))
                            linkPen = BluePen;
                        if ((contactPointer.NormallyClosed == true) && (contactOldPointer.NormallyClosed == false))
                            linkPen = RedPen;
                        if (!((contactPointer.NormallyClosed == false) && (contactOldPointer.NormallyClosed == false)))
                            DrawLineInCell(grfx, CellPt, 49, 14, 57, -14, linkPen);
                    }
                }
                if (contactOldPointer.typeOfCell == "Coil")
                {
                    RectangleF drawNewRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + (int)(-4 * scaleFactor),
                        114 * scaleFactor, 25 * scaleFactor);
                    RectangleF drawOldRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + (int)(42 * scaleFactor),
                        114 * scaleFactor, 25 * scaleFactor);
                    grfxDrawString(grfx, contactPointer.name, drawFont, RedBrush, drawNewRect, drawFormat);
                    grfxDrawString(grfx, contactOldPointer.name, drawFont, BlueBrush, drawOldRect, drawFormat);
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
                        DrawLineInCell(grfx, CellPt, 49, -(-14 + 11), 57, -14 - 11, RedPen);

                }
                if (contactOldPointer.typeOfCell == "Empty")
                {
                    RectangleF drawRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X,
                        114 * scaleFactor, 25 * scaleFactor);
                    grfxDrawString(grfx, contactPointer.name, drawFont, RedBrush, drawRect, drawFormat);
                    DrawContact(contactPointer, grfx, RedPen, redOffsetPaint, true);
                    if (contactPointer.NormallyClosed)
                        DrawLineInCell(grfx, CellPt, 49, 14, 57, -14, RedPen);
                }
                if (contactOldPointer.typeOfCell == "Horizontal Shunt")
                {
                    RectangleF drawNewRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + (int)(-4 * scaleFactor),
                        114 * scaleFactor, 25 * scaleFactor);
                    grfxDrawString(grfx, contactPointer.name, drawFont, RedBrush, drawNewRect, drawFormat);
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
                    if (contactPointer.NormallyClosed) DrawLineInCell(grfx, CellPt, 49, -(-14 + 11), 57, -14 - 11, RedPen);
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
                    grfxDrawString(grfx, contactPointer.name, drawFont, RedBrush, drawNewRect, drawFormat);
                    grfxDrawString(grfx, contactOldPointer.name, drawFont, BlueBrush, drawOldRect, drawFormat);
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
                        DrawLineInCell(grfx, CellPt, 49, -(-14 - 34), 57, -14 + 34, BluePen);

                }
                if (contactOldPointer.typeOfCell == "Empty")
                {
                    RectangleF drawRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X,
                        cellWidthStatic * scaleFactor, 25 * scaleFactor);
                    grfxDrawString(grfx, contactPointer.name, drawFont, RedBrush, drawRect, drawFormat);
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
                        grfxDrawString(grfx, contactPointer.name, drawFont, RedBrush, drawNewRect, drawFormat);
                        grfxDrawString(grfx, contactOldPointer.name, drawFont, BlueBrush, drawOldRect, drawFormat);
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
                        grfxDrawString(grfx, rungnumber + contactPointer.name, drawFont, CommonBrush, drawRect, drawFormat);
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
                    grfxDrawString(grfx, contactPointer.name, drawFont, RedBrush, drawNewRect, drawFormat);
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
                    grfxDrawString(grfx, contactOldPointer.name, drawFont, BlueBrush, drawOldRect, drawFormat);
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
                    grfxDrawString(grfx, contactOldPointer.name, drawFont, BlueBrush, drawOldRect, drawFormat);

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
                    grfxDrawString(grfx, contactOldPointer.name, drawFont, BlueBrush, drawRect, drawFormat);
                    DrawContact(contactPointer, grfx, BluePen, redOffsetPaint, true);
                    if (contactOldPointer.NormallyClosed == true)//false)
                        DrawLineInCell(grfx, CellPt, 49, 14, 57, -14, BluePen);
                }
                if (contactOldPointer.typeOfCell == "Coil")
                {
                    RectangleF drawRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X,
                        114 * scaleFactor, 25 * scaleFactor);
                    grfxDrawString(grfx, contactOldPointer.name, drawFont, BlueBrush, drawRect, drawFormat);
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
                            RectangleF SimTimeRect = new RectangleF(CellPt.Y + nameLocation.Y, CellPt.X + nameLocation.X + 55 * scaleFactor, 114 * scaleFactor, 50 * scaleFactor);

                            ML2Timer timerOldElement = (ML2Timer)timersOld[timerOldnumber];
                            ML2Timer timerNewElement = (ML2Timer)timersNew[timerNewnumber];


                            if (SimMode) DrawSimTimer(contactPointer.name, SimTimeRect, drawFont, drawFormat, grfx);
                            grfx.DrawString("Slow to go High: ", drawFont, CommonBrush, drawsetlabeltimerRect, drawFormat);
                            if (timerOldElement.setTime.ToString() == timerNewElement.setTime.ToString())
                                grfx.DrawString(timerOldElement.setTime.ToString(), drawFont, CommonBrush, setTimeRect, drawFormat);
                            else
                            {
                                grfx.DrawString(timerOldElement.setTime.ToString(), drawFont, BlueBrush, setTimeRect, drawFormat);
                                grfx.DrawString("                          " + timerNewElement.setTime.ToString(), drawFont, RedBrush, setTimeRect, drawFormat);
                            }
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
                rungrendition.Invalidate();
            }
            else
            {
                float oldscalefactor = scaleFactor;
                scaleFactor = scaleFactor * scalefactormultiplyer;
                endScaleFactor = scaleFactor;
                redOffset.X -= (int)((originy / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                redOffset.Y -= (int)((originx / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                blueOffset.X -= (int)((originy / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                blueOffset.Y -= (int)((originx / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                rungrendition.Invalidate();
            }
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {   // Time delay before links are displayed
            if (linkSelected != 0) lastLinkHover = DateTime.Now;
            if (DateTime.Compare(lastLinkHover.AddMilliseconds(700), DateTime.Now) < 0)
                if (showLinks == true)
                {
                    showLinks = false; //Invalidate();
                }
        }

        private void timer2_Tick(object sender, System.EventArgs e)
        {   // Sliding entrance of rungs
            TimeSpan difference = DateTime.Now - LoadTime;
            int windowWidth = this.Size.Width;
            int windowHeight = this.Size.Height;
            float diff = ((float)difference.Milliseconds) / 300;
            scaleFactor = endScaleFactor;
            redEntrance.X = 0;
            redEntrance.Y = (int)(-windowWidth * ((1 - diff) * (1 - diff)));
            //statusBar.Text = windowWidth.ToString();

            if (difference.Milliseconds > 300)
            {
                redEntrance.X = 0;
                redEntrance.Y = 0;

                timer2.Enabled = false;
            }
            //Invalidate();
        }

        private void timer3_Tick(object sender, System.EventArgs e)
        {   // Preview note entrance
            TimeSpan difference = DateTime.Now - PreviewRequestTime;
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
                //rungrendition.Invalidate();
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            TimeSpan difference = DateTime.Now - LoadTimeTransition;
            int windowWidth = this.Size.Width;
            int windowHeight = this.Size.Height;
            float diff = ((float)difference.Milliseconds) / 250;
            scaleFactor = endScaleFactor;

            redEntrance.X = 0;
            redEntrance.Y = (int)(-windowWidth * (diff));
            //statusBar.Text = windowWidth.ToString();


            if (difference.Milliseconds > 250)
            {
                redEntrance.X = 0;
                redEntrance.Y = 0;
                timer4.Enabled = false; showgridlines = true;
            }
            //rungrendition.Invalidate();
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
                //Invalidate();
            }
            else seqcounter = 0;
            animate++;
            if (animate > 5) animate = 0;
        }
        
        private void frmMDIMain_MouseWheel(object sender, MouseEventArgs e)
        {
            //statusBar.Text = e.Delta.ToString();
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
        }

        private void code_TextChanged(object sender, EventArgs e)
        {
            processkey();
        }

        private void code_SelectionChanged(object sender, EventArgs e)
        {
            processkey();
        }

        private void code_Click(object sender, EventArgs e)
        {
            processkey();
        }

        private void processkey()
        {
            string ncode = code.Text + "ASSIGN";     
            int starts = code.SelectionStart;
            if (starts < ncode.Length - 6)
                if (ncode.Substring(starts, 6) == "ASSIGN")
                  starts += 6;
            string first = ncode.ToUpper().Substring(0, starts);            
            int st = first.LastIndexOf("ASSIGN");
            if (st != -1)
            {
                string last = ncode.ToUpper().Substring(st+1);
                int length = last.IndexOf("ASSIGN")+1;
                if (length != -1)                
                    codeout.Text = ncode.Substring(st, length);                
                else codeout.Text = "";                
            }
            else codeout.Text = "";
            typedinput = codeout.Text;
            if(gotsomething)
                interlockingtyped.Clear();
            ParseML2Rungs(typedinput, interlockingtyped);
            gotsomething = true;
            //Invalidate();
            rungrendition.Invalidate();
            counter++;
            //debug.Text = counter.ToString();
        }


        private void ParseML2Rungs(string typedinput, ArrayList interlocking)
        {
            string line = "", rungnamedebug = "";
            try
            {
                string code = ""; string frag = "";  string lineofcode = "";
                string rungName; int RungNumber; int coils = 0; string rungNameExtended = ""; bool dumpline = false;
                string charlookahead = ""; string charlookahead2 = "";
                bool commentMode = false; bool commentLineMode = false;
                string[] lines = typedinput.Split('\n');
                foreach (string lin in lines)
                {
                    commentLineMode = false;
                    line = TranslateMLline(lin); // Convert the and's, or's, to's to *'s +'s and ='s

                    dumpline = false;
                    lineofcode = "";
                    for (int index = 0; index < line.Length; index++)
                    {
                        if (index < line.Length - 1)
                        {
                            charlookahead2 = line.Substring(index, 2);
                            if (charlookahead2 == "/*") commentMode = true; // Find start of comments
                            if (charlookahead2 == "//") commentLineMode = true;
                        }
                        if ((commentMode == true) || (commentLineMode == true))
                        {
                            if (line.IndexOf("*/") == -1)
                            {
                                dumpline = true; //In Comment mode and not end comment in the line
                                index = line.Length;
                            }
                        }
                        if (!dumpline)
                        {
                            charlookahead = line.Substring(index, 1);
                            if ((charlookahead != " ") && (charlookahead != "\r\n") && (charlookahead != "\t"))
                                //Remove carraige returns, spaces & tabs
                                if (!commentMode)
                                    lineofcode += charlookahead;
                            if (index > 0)
                                if (line.Substring(index - 1, 2) == "*/") commentMode = false;
                        }
                    }
                    code += lineofcode;
                }
                
                code = balancebrackets(code);                
                code = adddummycoil(code);
                code = addoperator(code);
                code = doubleoperator(code);
                int logicBeginIndex = 0;// code.LastIndexOf("LOGICBEGIN", sc) + 10;//snip off anything before logic begin
                string logic = code.Substring(logicBeginIndex, code.Length - logicBeginIndex);
                int logicEndIndex = code.Length;//logic.LastIndexOf("ENDLOGIC", sc);//snip off anything after end logic
                logic = logic.Substring(0, logicEndIndex);
                string RungLogic = "";
                RungNumber = 1;
                while ((logic.IndexOf("ASSIGN", sc) != -1) || (logic.IndexOf("NV.ASSIGN", sc) != -1))
                {
                    if ((logic.IndexOf("ASSIGN", sc) < logic.IndexOf("NV.ASSIGN", sc)) || (logic.IndexOf("NV.ASSIGN", sc) == -1))
                        RungLogic = logic.Substring(logic.IndexOf("ASSIGN", sc) + 6, logic.IndexOf(";") - (6 + logic.IndexOf("ASSIGN", sc)));
                    else
                        RungLogic = logic.Substring(logic.IndexOf("NV.ASSIGN", sc) + 9, logic.IndexOf(";") - (9 + logic.IndexOf("NV.ASSIGN", sc)));
                    logicBeginIndex = logic.IndexOf(";") + 1;//snip off rung just scanned, until they are all scanned in
                    logic = logic.Substring(logicBeginIndex, logic.Length - logicBeginIndex);
                    rungName = RungLogic.Substring(RungLogic.IndexOf("=") + 1, RungLogic.Length - (1 + RungLogic.IndexOf("=")));

                    while (rungName.LastIndexOf("{StartBracket}", sc) != -1)
                        rungName = rungName.Substring(0, rungName.LastIndexOf("{StartBracket}", sc)) + "(" + rungName.Substring(rungName.LastIndexOf("{StartBracket}", sc) + 14);
                    while (rungName.LastIndexOf("{EndBracket}", sc) != -1)
                        rungName = rungName.Substring(0, rungName.LastIndexOf("{EndBracket}", sc)) + ")" + rungName.Substring(rungName.LastIndexOf("{EndBracket}", sc) + 12);

                    RungLogic = RungLogic.Substring(0, RungLogic.IndexOf("="));

                    coils = CountCommas(rungName);
                    rungNameExtended = rungName;
                    rungnamedebug = rungName;

                    for (int i = 0; i < coils; i++)
                    {
                        ArrayList runglist = new ArrayList();

                        RungLogic = ProcessBrackets(RungLogic);

                        ParseML2Rung(runglist, RungLogic, i);
                        ArrayList rung = new ArrayList();
                        rung.Add(RungNumber); RungNumber++;
                        if (rungNameExtended.IndexOf(",") == -1)
                        {
                            ArrayList coil = new ArrayList();
                            coil.Add(MakeContact(rungNameExtended, 1, 1, false, false, true, "Coil", false));
                            AndRungs(runglist, coil);
                            AddRungs(rung, runglist);
                            rung.Add(rungNameExtended);
                        }
                        else
                        {

                            ArrayList coil = new ArrayList();
                            frag = rungNameExtended.IndexOf(",").ToString();
                            coil.Add(MakeContact(rungNameExtended.Substring(rungNameExtended.LastIndexOf(",") + 1)
                                , 1, 1, false, false, true, "Coil", false));
                            AndRungs(runglist, coil);
                            AddRungs(rung, runglist);
                            rung.Add(rungNameExtended.Substring(rungNameExtended.LastIndexOf(",") + 1));
                            rungNameExtended = rungNameExtended.Substring(0, rungNameExtended.LastIndexOf(","));
                        }
                        interlocking.Add(TakeOutBrackets(rung));
                    }
                }
            }
            catch {
            //    MessageBox.Show("Problem parsing ML2 file, line: " + line.ToString() + ", " + rungnamedebug.ToString() + ", try compiling the ML2 for a diagnosis of errors", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private string addoperator(string code)
        {
            for (int i = 0; i < code.Length; i++)
                if (code[i] == '=')              
                    if (i > 7)                    
                        if ((code[i - 1] == '+') || (code[i - 1] == '*'))
                        {
                            string outcode = code.Substring(0, i);
                            outcode += "{contact}";
                            outcode += code.Substring(i);
                            return outcode;
                        }
            return code;                                              
        }

        private string doubleoperator(string code)
        {
            for (int i = 0; i < code.Length-1; i++)
            {
                if (((code[i] == '+') || (code[i] == '*')) &&
                    ((code[i+1] == '+') || (code[i+1] == '*')))
                {
                    string outcode = code.Substring(0, i+1);
                    outcode += "{contact}";
                    outcode += code.Substring(i+1);
                    return outcode;
                }
            }
            return code;
        }

        private string adddummycoil(string code)
        {
            char ch = 'v';
            for (int i = 0; i < code.Length; i++)
            {
                if ((code[i] == '=') && (i == code.Length - 1))//all good                    
                    return code + "{rung Name};";
                if ((code[i] == '=') && (i != code.Length - 1) && (code[code.Length-1] != ';'))//all good                  
                    return code + ";";
                if (code[i] == '=') return code;
            }
            return code + "={rung Name};";
        }

        private string balancebrackets(string code)
        {
            int startbracket = 0;
            int endbracket = 0;
            int equals = 0;
            char ch = 'v';
            for (int i = 0; i < code.Length;i++)                
            {
                ch = code[i];
                if (ch == '=')
                {
                    equals = i;
                    break;
                }
                if (ch == '(') startbracket++;
                if (ch == ')') endbracket++;
            }
            string outcode = code.Substring(0, equals);
            if (startbracket != endbracket)
                outcode += "*{bracket}";
            for (int i = 0; i < startbracket - endbracket; i++) outcode += ')';
            outcode += code.Substring(equals); 
            return (outcode);
        }

        private ArrayList TakeOutBrackets(ArrayList original)
        {
            ArrayList duplicate = new ArrayList();
            duplicate.Add(original[0]);
            for (int i = 1; i < original.Count - 1; i++)
            {
                Contact newcontact = (Contact)original[i];
                string oldname = newcontact.name;
                while (oldname.LastIndexOf("{StartBracket}", sc) != -1)
                    oldname = oldname.Substring(0, oldname.LastIndexOf("{StartBracket}", sc)) + "(" + oldname.Substring(oldname.LastIndexOf("{StartBracket}", sc) + 14);
                while (oldname.LastIndexOf("{EndBracket}", sc) != -1)
                    oldname = oldname.Substring(0, oldname.LastIndexOf("{EndBracket}", sc)) + ")" + oldname.Substring(oldname.LastIndexOf("{EndBracket}", sc) + 12);
                newcontact.name = oldname;
                duplicate.Add(newcontact);
            }
            duplicate.Add(original[original.Count - 1]);
            return (duplicate);
        }


        private string ProcessBrackets(string line)
        {
            while ((line.IndexOf("\"", sc) != -1))
            {
                int startapostrophe = line.IndexOf('\"');
                //int endapostrophe = line.IndexOf("\"", 0, 2, sc);
                int endapostrophe = line.Substring(startapostrophe + 1).IndexOf('\"');
                endapostrophe += startapostrophe;
                line = line.Substring(0, startapostrophe) + line.Substring(startapostrophe + 1);
                line = line.Substring(0, endapostrophe) + line.Substring(endapostrophe + 1);
                endapostrophe--;
                string testline = line.Substring(startapostrophe, 1 + endapostrophe - startapostrophe);
                if (endapostrophe != -1)
                {

                    while ((line.Substring(startapostrophe, 1 + endapostrophe - startapostrophe).IndexOf("(", sc) != -1))
                    {
                        int startbracket = line.Substring(startapostrophe, 1 + endapostrophe - startapostrophe).IndexOf("(");
                        if (startbracket != -1)
                        {
                            line = line.Substring(0, startapostrophe + startbracket) + "{StartBracket}" + line.Substring(startapostrophe + startbracket + 1);
                            endapostrophe += 13;
                        }
                    }
                    while ((line.Substring(startapostrophe, 1 + endapostrophe - startapostrophe).IndexOf(")", sc) != -1))
                    {
                        int startbracket = line.Substring(startapostrophe, 1 + endapostrophe - startapostrophe).IndexOf(")");
                        if (startbracket != -1)
                        {
                            line = line.Substring(0, startapostrophe + startbracket) + "{EndBracket}" + line.Substring(startapostrophe + startbracket + 1);
                            endapostrophe += 11;
                        }
                    }
                }
            }
            return line;
        }



        public string TranslateMLline(string lineread)
        {
                     string line = " " + lineread + " "; //Pad the line with space on either side
            while (line.LastIndexOf("\t", sc) != -1) //Replace the tabs with spaces
                line = line.Substring(0, line.LastIndexOf("\t", sc)) + " " +
                    line.Substring(line.LastIndexOf("\t", sc) + 1);

            while (line.LastIndexOf("//", sc) != -1) //Pad the comment with spaces
                line = line.Substring(0, line.LastIndexOf("//", sc)) + "{PH}" +
                    line.Substring(line.LastIndexOf("//", sc) + 2);
            while (line.LastIndexOf("{PH}", sc) != -1) //Pad the comment with spaces
                line = line.Substring(0, line.LastIndexOf("{PH}", sc)) + " // " +
                    line.Substring(line.LastIndexOf("{PH}", sc) + 4);

            while (line.LastIndexOf("/*", sc) != -1) //Pad the comment with spaces
                line = line.Substring(0, line.LastIndexOf("/*", sc)) + "{PH}" +
                    line.Substring(line.LastIndexOf("/*", sc) + 2);
            while (line.LastIndexOf("{PH}", sc) != -1) //Pad the comment with spaces
                line = line.Substring(0, line.LastIndexOf("{PH}", sc)) + " /* " +
                    line.Substring(line.LastIndexOf("{PH}", sc) + 4);

            while (line.LastIndexOf("*/", sc) != -1) //Pad the comment with spaces
                line = line.Substring(0, line.LastIndexOf("*/", sc)) + "{PH}" +
                    line.Substring(line.LastIndexOf("*/", sc) + 2);
            while (line.LastIndexOf("{PH}", sc) != -1) //Pad the comment with spaces
                line = line.Substring(0, line.LastIndexOf("{PH}", sc)) + " */ " +
                    line.Substring(line.LastIndexOf("{PH}", sc) + 4);
            
            while (line.LastIndexOf(")", sc) != -1) //Pad the comment with spaces
                line = line.Substring(0, line.LastIndexOf(")", sc)) + "{PH}" +
                    line.Substring(line.LastIndexOf(")", sc) + 1);
            while (line.LastIndexOf("{PH}", sc) != -1) //Pad the comment with spaces
                line = line.Substring(0, line.LastIndexOf("{PH}", sc)) + " ) " +
                    line.Substring(line.LastIndexOf("{PH}", sc) + 4);

            while (line.LastIndexOf("(", sc) != -1) //Pad the comment with spaces
                line = line.Substring(0, line.LastIndexOf("(", sc)) + "{PH}" +
                    line.Substring(line.LastIndexOf("(", sc) + 1);
            while (line.LastIndexOf("{PH}", sc) != -1) //Pad the comment with spaces
                line = line.Substring(0, line.LastIndexOf("{PH}", sc)) + " ( " +
                    line.Substring(line.LastIndexOf("{PH}", sc) + 4);

            while (line.LastIndexOf(" TO ", sc) != -1)
                line = line.Substring(0, line.LastIndexOf(" TO ", sc)) + "=" +
                     line.Substring(line.LastIndexOf(" TO ", sc) + 4);
            while (line.LastIndexOf(" AND ", sc) != -1)
                line = line.Substring(0, line.LastIndexOf(" AND ", sc)) + " * " +
                     line.Substring(line.LastIndexOf(" AND ", sc) + 5);

            while (line.LastIndexOf(" OR ", sc) != -1)
                line = line.Substring(0, line.LastIndexOf(" OR ", sc)) + " + " +
                     line.Substring(line.LastIndexOf(" OR ", sc) + 4);

            while (line.LastIndexOf(" NOT ", sc) != -1)
                line = line.Substring(0, line.LastIndexOf(" NOT ", sc)) + "~" +
                     line.Substring(line.LastIndexOf(" NOT ", sc) + 5);

            while (line.LastIndexOf("{RealStartBracket}", sc) != -1) //Pad the comment with spaces
                line = line.Substring(0, line.LastIndexOf("{RealStartBracket}", sc)) + " ( " +
                    line.Substring(line.LastIndexOf("{RealStartBracket}", sc) + 18);

            while (line.LastIndexOf("{RealEndBracket}", sc) != -1) //Pad the comment with spaces
                line = line.Substring(0, line.LastIndexOf("{RealEndBracket}", sc)) + " ) " +
                    line.Substring(line.LastIndexOf("{RealEndBracket}", sc) + 16);

            line.Replace("STATUS", "Status");
            return (line);
        }



        private int CountCommas(string rungName)
        {
            string name = rungName;
            int counter = 1;
            while (name != "")
            {
                if (name.IndexOf(",") == -1) return counter;
                name = name.Substring(name.IndexOf(",") + 1);
                counter++;
            }
            return counter;
        }

        private void AddRungs(ArrayList rung1, ArrayList rung2)
        {
            for (int j = 0; j < rung2.Count; j++)
            {
                Contact contactPointer = (Contact)rung2[j];
                if (contactPointer.y == 1) //Draw positive busbar
                { contactPointer.topLink = true; contactPointer.bottomLink = true; }
                rung1.Add(MakeContact(contactPointer.name, contactPointer.x, contactPointer.y, contactPointer.topLink,
                    contactPointer.bottomLink, contactPointer.leftLink, contactPointer.typeOfCell, contactPointer.NormallyClosed));
            }
        }

        private void ParseML2Rung(ArrayList rung, string RungLogic, int linenum)
        {
            Errorcheck(RungLogic, linenum);
            GetOredfactors(rung, RungLogic);
        }

        private void Errorcheck(string rung, int linenum)
        {
            int countst = 0;
            int countend = 0;
            foreach (char c in rung)
            {
                if (c == '(') countst++;
                if (c == ')') countend++;
            }
            //if (countst != countend)
                //statusBar.Text = "Error reading rung number: "
                 //    + (linenum + 1).ToString() + ", brackets are unmatched, there are " + countst.ToString() + " start brackets but "
                   //  + countend.ToString() + " end brackets. \r\n" + rung;            
        }

        private Contact MakeContact(string name, int x, int y, bool toplink, bool bottomLink, bool leftlink, string typeCell, bool normallyClosed)
        {
            Contact contact = new Contact();
            contact.NormallyClosed = normallyClosed;
            if (name.Length < 1) contact.name = "";
            else
            {
                contact.NormallyClosed = normallyClosed;
                if (name.Substring(0, 1) == "~") contact.name = name.Substring(1);
                else contact.name = name;
                if (name.Substring(0, 1) == "~") contact.NormallyClosed = true;

            }
            contact.x = x;
            contact.y = y;

            contact.typeOfCell = typeCell;//Contact, Coil, Horizontal Shunt, Vertical Shunt, Empty cell, End Contact
            contact.inputindex = -1;
            contact.rungindex = -1;
            contact.topLinkindex = -1;
            contact.bottomLinkindex = -1;
            contact.leftLinkindex = -1;
            contact.rightLinkindex = -1;
            contact.leftLink = leftlink;
            contact.topLink = toplink;
            contact.bottomLink = bottomLink;
            return contact;
        }

        private void GetOredfactors(ArrayList rung1, string RungLogic)
        {
            int factors;
            ArrayList rung2 = new ArrayList();
            factors = GetFactors(RungLogic, "+");
            for (int i = 1; i < factors + 1; i++)
            {
                GetAndedTokens(rung2, GetFactorString(RungLogic, i, "+"));
                GlueLogic(rung1, "+", rung2);
                rung2.Clear();
            }
        }

        private int GetFactors(string RungLogic, string operand)
        {
            int counter = 0; int startbrackets = 0; int endbrackets = 0; int factors = 0;
            while (RungLogic.Length > counter)
            {
                if (RungLogic.Substring(counter, 1) == "(") startbrackets++;
                if (RungLogic.Substring(counter, 1) == ")") endbrackets++;
                if ((startbrackets == endbrackets) && (RungLogic.Substring(counter, 1) == operand))
                    factors++;
                counter++;
            }
            return factors + 1;
        }

        private string GetFactorString(string RungLogic, int i, string operand)
        {
            int counter = 0; int startbrackets = 0; int endbrackets = 0; int operandsFound = 0;
            int startpoint = 0; int endpoint = 0;
            while (RungLogic.Length > counter)
            {
                if (RungLogic.Substring(counter, 1) == "(") startbrackets++;
                if (RungLogic.Substring(counter, 1) == ")") endbrackets++;
                if ((startbrackets == endbrackets) && (RungLogic.Substring(counter, 1) == operand))
                {
                    operandsFound++;
                    if (operandsFound == i - 1) startpoint = counter + 1;
                    if (operandsFound == i) endpoint = counter;
                }
                counter++;
            }

            string outputstring = "";
            if (endpoint == 0) outputstring = RungLogic.Substring(startpoint);
            else outputstring = RungLogic.Substring(startpoint, endpoint - startpoint);
            //                  if (outputstring.IndexOf("GG-YY") != -1)
            //outputstring = outputstring;
            return outputstring;
        }

        private string FindAndedTokens(string RungLogic)
        {
            int counter = 0; int startbrackets = 0; int endbrackets = 0; bool found = false;
            while ((!found) && (RungLogic.Length > counter))
            {
                if (RungLogic.Substring(counter, 1) == "(") startbrackets++;
                if (RungLogic.Substring(counter, 1) == ")") endbrackets++;
                if ((startbrackets == endbrackets) && (RungLogic.Substring(counter, 1) == "+"))
                {
                    found = true;
                    counter--;
                }
                counter++;
            }
            return RungLogic.Substring(0, counter);
        }

        private void GetAndedTokens(ArrayList rung1, string RungLogic)//Assumes no or's in the string
        {
            int factors = 0; string factorString = "";
            ArrayList rung2 = new ArrayList();
            factors = GetFactors(RungLogic, "*");
            for (int i = 1; i < factors + 1; i++)
            {
                factorString = GetFactorString(RungLogic, i, "*");
                if (factorString.Substring(0, 1) == "(")
                {
                    GetOredfactors(rung2, factorString.Substring(1, factorString.Length - 2));
                    GlueLogic(rung1, "*", rung2);
                    rung2.Clear();
                }
                else
                {
                    rung2.Add(MakeContact(GetTokens(factorString), 1, 1, false, false, false, "Contact", false));
                    GlueLogic(rung1, "*", rung2);
                    rung2.Clear();
                }
            }
        }

        private int FindEndBracket(string RungLogic)
        {
            int counter = 0; int startbrackets = 0; int endbrackets = 0; bool found = false;
            while (!found)
            {
                if (RungLogic.Substring(counter, 1) == "(") startbrackets++;
                if (RungLogic.Substring(counter, 1) == ")") endbrackets++;
                if ((startbrackets > 0) && (startbrackets == endbrackets)) found = true;
                counter++;
            }
            return counter;
        }

        private string GetTokens(string RungLogic)
        {
            char element;
            bool foundOperator = false;
            int index = 0;
            string token = "";
            while ((foundOperator == false) && (index < RungLogic.Length))
            {
                element = RungLogic.Substring(index, 1)[0];
                index++;
                if ((element >= '0') &&
                   (element <= '9') ||
                   (element >= 'a') &&
                   (element <= 'z') ||
                   (element >= 'A') &&
                   (element <= 'Z') ||
                   (element == '$') ||
                   (element == '-') ||
                   (element == '{') ||
                   (element == '}') ||
                   (element == '_') ||
                   (element == '/') ||
                   (element == '~') ||
                   //(element == '*') ||
                   (element == '.'))
                    token += element.ToString();
                else
                    foundOperator = true;
            }
            return token;

            /*
        Operators
        ---------
    
        *, AND : Logic and
        +, OR : Logic or
        ~, !, NOT : not inverter
        1-9, a-z, A-Z, _,.,-,$  : function element
         * */
        }

        private void GlueLogic(ArrayList rung1, string operand, ArrayList rung2)
        {
            if (operand == "+")
                OrRungs(rung1, rung2);
            if (operand == "*")
                AndRungs(rung1, rung2);
        }

        private void OrRungs(ArrayList rung1, ArrayList rung2)
        {
            int sizediff = 0;
            int rung1height = GetHeight(rung1);
            int rung2width = GetWidth(rung2); int rung1width = GetWidth(rung1);
            int rung1heightFirstRow = GetHeightFirstRow(rung1);
            int lastContact = GetLastContact(rung1);
            sizediff = rung1width - rung2width;
            if ((-sizediff > 0) && (rung1height > 0))//rung2 bigger than rung1
                for (int i = 0; i < -sizediff; i++) // join rung1 to rung2
                {
                    ArrayList rungtemp = new ArrayList();
                    rungtemp.Add(MakeContact("", 1, 1, false, false, true,
                        "Horizontal Shunt", false));
                    AndRungs(rung1, rungtemp);
                }
            if ((sizediff > 0) && (rung1height > 0))//rung1 bigger than rung2
                for (int i = 0; i < sizediff; i++) // join rung1 to rung2
                {
                    ArrayList rungtemp = new ArrayList();
                    rungtemp.Add(MakeContact("", 1, 1, false, false, true,
                        "Horizontal Shunt", false));
                    //rung1.Add(MakeContact("", rung1height + 1, rung2width + i + 1, false, false, true,
                    //    "Horizontal Shunt", false));
                    AndRungs(rung2, rungtemp);
                }
            for (int j = 0; j < rung2.Count; j++)
            {
                Contact contactPointer = (Contact)rung2[j];//bool toplink, bool bottomLink, bool leftlink,
                if ((rung1height > 0) && (contactPointer.x == 1) && (contactPointer.y == 1))
                {
                    rung1.Add(MakeContact(contactPointer.name, contactPointer.x + rung1height, contactPointer.y,
                        true, contactPointer.bottomLink, contactPointer.leftLink, contactPointer.typeOfCell, contactPointer.NormallyClosed));
                }
                else
                    rung1.Add(MakeContact(contactPointer.name, contactPointer.x + rung1height, contactPointer.y,
                        contactPointer.topLink, contactPointer.bottomLink, contactPointer.leftLink, contactPointer.typeOfCell, contactPointer.NormallyClosed));
            }

            if (rung1heightFirstRow != rung1height)
                for (int i = 0; i < rung1height - rung1heightFirstRow; i++)
                    rung1.Add(MakeContact("", rung1heightFirstRow + 1 + i, 1, true, true, false,
                        "Empty", false));
        }

        private void AndRungs(ArrayList rung1, ArrayList rung2)
        {
            int rung1width = GetWidth(rung1); int rung1Height = GetHeight(rung1);
            int rung1heightLastRow = GetHeightLastRow(rung1); int rung2heightFirstRow = GetHeightFirstRow(rung2);
            ArrayList rungtemp = new ArrayList();
            AddRungs(rungtemp, rung1);//Save a copy of rung1
            for (int j = 0; j < rung2.Count; j++)
            {
                Contact contactPointer = (Contact)rung2[j];
                if (rung1Height > 0)
                {
                    if (contactPointer.y == 1)
                    {
                        if (rung2heightFirstRow < rung1heightLastRow)
                            contactPointer.bottomLink = true;
                        if (contactPointer.x < rung2heightFirstRow)
                            contactPointer.bottomLink = true;
                        if ((contactPointer.x == 1) || CheckForLeftLink(rungtemp, contactPointer.x))
                            contactPointer.leftLink = true;
                        if (contactPointer.x > 1)
                            contactPointer.topLink = true;
                    }
                }
                rung1.Add(MakeContact(contactPointer.name, contactPointer.x, contactPointer.y + rung1width,
                        contactPointer.topLink, contactPointer.bottomLink, contactPointer.leftLink, contactPointer.typeOfCell, contactPointer.NormallyClosed));
            }
            if ((rung1Height > 0) && (rung1heightLastRow > rung2heightFirstRow))
            {
                for (int i = 1; i < rung1heightLastRow - rung2heightFirstRow; i++)
                {
                    rung1.Add(MakeContact("", i + rung2heightFirstRow, rung1width + 1,
                        true, true, CheckForLeftLink(rungtemp, i + rung2heightFirstRow), "Empty", false));
                }
                rung1.Add(MakeContact("", rung1heightLastRow, rung1width + 1,
                        true, false, true, "Empty", false));
            }
        }


        private bool CheckForLeftLink(ArrayList rung, int x)// Check to see if rung has unconnected rung, y deep.
        {
            int width = GetWidth(rung);
            for (int j = 0; j < rung.Count; j++)
            {
                Contact contactPointer = (Contact)rung[j];
                if ((contactPointer.x == x) && (contactPointer.y == width) && (contactPointer.typeOfCell != "Empty") &&
                    (contactPointer.typeOfCell != "Vertical Shunt"))
                    return true;
            }
            return false;
        }


        private int GetLastContact(ArrayList rung)//Find the position of the last contact on the bottom row
        {
            int height = GetHeight(rung);
            int lastContact = 0;
            for (int j = 0; j < rung.Count; j++)
            {
                Contact contactPointer = (Contact)rung[j];
                if ((contactPointer.x == height) && (contactPointer.typeOfCell == "Contact"))
                    if (lastContact < contactPointer.y)
                        lastContact = contactPointer.y;
            }
            return lastContact;
        }

        private int GetHeightFirstRow(ArrayList rung)
        {
            int maxHeight = 0;
            for (int j = 0; j < rung.Count; j++)
            {
                Contact contactPointer = (Contact)rung[j];
                if (contactPointer.y == 1)
                    if (maxHeight < contactPointer.x)
                        maxHeight = contactPointer.x;
            }
            return maxHeight;
        }

        private int GetHeightLastRow(ArrayList rung)
        {
            int maxHeight = 0; int rungWidth = GetWidth(rung);
            for (int j = 0; j < rung.Count; j++)
            {
                Contact contactPointer = (Contact)rung[j];
                if ((contactPointer.y == rungWidth) && (contactPointer.typeOfCell != "Empty") &&
                    (contactPointer.typeOfCell != "Vertical Shunt"))
                    if (maxHeight < contactPointer.x)
                        maxHeight = contactPointer.x;
            }
            return maxHeight;
        }

        private int GetWidth(ArrayList rung)
        {
            int maxWidth = 0;
            for (int j = 0; j < rung.Count; j++)
            {
                Contact contactPointer = (Contact)rung[j];
                if (maxWidth < contactPointer.y)
                    maxWidth = contactPointer.y;
            }
            return maxWidth;
        }

        private int GetHeight(ArrayList rung)
        {
            int maxHeight = 0;
            for (int j = 0; j < rung.Count; j++)
            {
                Contact contactPointer = (Contact)rung[j];
                if (maxHeight < contactPointer.x)
                    maxHeight = contactPointer.x;
            }
            return maxHeight;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            //Loadfile();
        }


        private void LoadFile()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                OpenMapFile(openFileDialog1.FileName);
        }

        private void OpenMapFile(string filenameString)
        {
            try
            {
                if (filenameString.EndsWith(".ml2", true, ci))                
                    ParseFile(filenameString, 0, 0);                
            }
            catch { MessageBox.Show("Error opening data file", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void ParseFile(string filenameString, int top, int left)
        {
            string line = "";            
            SR = File.OpenText(filenameString);
            code.Text = "";
            while ((line = SR.ReadLine()) != null)
                code.Text += line + "\n";                           
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            LoadFile();
        }

        private void frmMChild_Visualiser_SizeChanged(object sender, EventArgs e)
        {
            int sizey = this.Height;
        } 

        private void frmMChild_Visualiser_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {   
                OpenMapFile(file);
            }
        }

        private void OpenFile_Click_1(object sender, EventArgs e)
        {
            LoadFile();
        }       
        
        private void rungrendition_Paint(object sender, PaintEventArgs e)
        {
            eGraphics = e.Graphics;            
            paintRungs(eGraphics);
        }

        private void rungrendition_MouseMove(object sender, MouseEventArgs e)
        {
            if (true)
            {
                Point ScaledCursor = new Point((int)(Cursor.Position.X / scaleFactor), (int)(Cursor.Position.Y / scaleFactor));
                if (interlockingtyped.Count > 0)
                {
                    try
                    {
                        if (!rightMouseDown && !middleMouseDown && !leftMouseDown)
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
                            rungrendition.Invalidate();
                        }
                        if (leftMouseDown == true)
                        {
                            redOffset.X = redOffset.X - (blueOffset.X + click.X - ScaledCursor.Y);
                            redOffset.Y = redOffset.Y - (blueOffset.Y + click.Y - ScaledCursor.X);
                            blueOffset.X = -click.X + ScaledCursor.Y;
                            blueOffset.Y = -click.Y + ScaledCursor.X;

                            gridOffset.Y = (int)(cellWidthStatic * (blueOffset.Y / cellWidthStatic - Math.Round(blueOffset.Y / cellWidthStatic)));
                            gridOffset.X = (int)(cellHeightStatic * (blueOffset.X / cellHeightStatic - Math.Round(blueOffset.X / cellHeightStatic)));

                            rungrendition.Invalidate();
                        }
                        if (middleMouseDown == true)
                        {
                            scaleOffset.X = -scaleClick.X + Cursor.Position.Y;
                            scaleOffset.Y = -scaleClick.Y + Cursor.Position.X;
                            scaleFactor = scaleFactorClick + (((float)scaleOffset.X) / 100);
                            if (scaleFactor < 0.3) scaleFactor = 0.3F;
                            rungrendition.Invalidate();
                        }

                        /*

                        CellCoord = new Point((int)(1 + (e.Y - (40 + redOffset.X * scaleFactor)) / cellHeight),
                                (int)(1 + (e.X - (40 + redOffset.Y * scaleFactor)) / cellWidth)); // Get the current cell coordinates of mouse pointer.			
                        ArrayList rungOldPointer = (ArrayList)interlockingtyped[CurrentNewCell];
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
                            for (int i = 0; i < interlockingtyped.Count; i++)
                            {
                                ArrayList rungPointer = (ArrayList)interlockingtyped[i];
                                if ((string)rungPointer[rungPointer.Count - 1] == CellName)
                                    rungNumber = (int)rungPointer[0];
                            }
                            if (rungNumber == -1)
                            {

                                HighlightBrush = new SolidBrush(Color.LightGray);
                                HighlightPen = new Pen(Color.LightGray);
                                if (SimMode)
                                {
                                    //statusBar.Text = "Click to toggle input";
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
                            for (int c = 0; c < interlockingtyped.Count; c++)
                                if (e.X > linksPoint.Y + 130 * scaleFactor)
                                    if (e.X < linksPoint.Y + 130 * scaleFactor + cellWidthStatic * scaleFactor)
                                        if (e.Y > linksPoint.X + c * 25 * scaleFactor)
                                            if (e.Y < linksPoint.X + c * 25 * scaleFactor + 25 * scaleFactor)
                                            {
                                                linkSelected = c;
                                                lastLinkHover = DateTime.Now;
                                            }
                            if (oldLinkSelected != linkSelected) Invalidate();
                        }*/
                    }
                    catch (InvalidCastException ex)
                    {
                        // Perform some action here, and then throw a new exception.
                        MessageBox.Show(ex.Source.ToString(), "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }                
                    //catch { MessageBox.Show("Error drawing timer", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                }
            }
        }

        private void rungrendition_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftMouseDown == true) leftMouseDown = false;
            if (rightMouseDown == true) rightMouseDown = false;
            if (middleMouseDown == true)
            {
                scaleOffset.Y = -scaleClick.Y + Cursor.Position.X;
                middleMouseDown = false;
            }
        }

        private void rungrendition_MouseDown(object sender, MouseEventArgs e)
        {
            if (true)
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
                        for (int i = 0; i < interlockingtyped.Count; i++)
                        {
                            ArrayList rungPointer = (ArrayList)interlockingtyped[i];
                            if ((string)rungPointer[rungPointer.Count - 1] == rungName) oldIndex = i;
                        }
                        for (int j = 0; j < interlockingtyped.Count; j++)
                        {
                            ArrayList rungPointer = (ArrayList)interlockingtyped[j];
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
                        }
                        if ((oldIndex == -1) && (newIndex != -1))
                        {
                            oldindexTransition = newIndex;
                            newindexTransition = newIndex;
                            windowTypeTransition = "All New";
                            timer4.Enabled = true; LoadTimeTransition = DateTime.Now; showgridlines = false;
                        }
                        if ((oldIndex != -1) && (newIndex != -1))
                        {
                            oldindexTransition = oldIndex;
                            newindexTransition = newIndex;
                            windowTypeTransition = "Normal";
                            timer4.Enabled = true; LoadTimeTransition = DateTime.Now; showgridlines = false;
                        }
                    }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string savecode = code.Text.Substring(0, code.Text.Length - 1);
            string prevcode = code.Text;
            code.Text = savecode;
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                try
                {
                    code.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                }
                catch
                { MessageBox.Show("Error Saving File", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            code.Text = prevcode;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            code.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            code.Paste();
        }

        private void goToSelectedRungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string rung = code.SelectedText;
            code.Find(rung + ';');

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string savecode = code.Text.Substring(0, code.Text.Length - 1);
            string prevcode = code.Text;
            code.Text = savecode;
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                try
                {
                    code.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                }
                catch
                { MessageBox.Show("Error Saving File", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            code.Text = prevcode;
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            code.Copy();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            code.Paste();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                Font drawFont = fontDialog1.Font;
                code.Font = drawFont;                
            }
        }

        /****************************************************************/

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


        }
    
    /*
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
    }*/
}
