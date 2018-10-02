 using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Globalization;


namespace Logic_Navigator
{
    public class frmMChild_SimMap : System.Windows.Forms.Form
    {
        private frmMDIMain parentform;

        private ArrayList interlockingNew;
        private ArrayList interlockingOld;
        private ArrayList timersOld;
        private ArrayList timersNew;
        private System.Data.DataTable points;

        private SolidBrush HighlightBrush = new SolidBrush(Color.HotPink);
        private Pen HighlightPen = new Pen(Color.HotPink);
        private string ImageDirectoryPath = "";
        public string Mapfilename = "";
        private Font drawFnt;
        string rungName = "";
        int paintCounter = 0;
        private Point mouselocation = new Point(0, 0);
        private Point unscaledlocation = new Point(0, 0);
        private Point mousehover = new Point(0, 0);
        
        AutoCompleteStringCollection RungAndInputAutoCompleteSource = new AutoCompleteStringCollection();
        AutoCompleteStringCollection InputAutCompleteSource = new AutoCompleteStringCollection();


        private Point scaleOffset = new Point(0, 0);
        private Point click = new Point(0, 0);
        private Point Start = new Point(0, 0);
        private Point End = new Point(0, 0);
        private Point Startadj = new Point(0, 0);
        private Point Endadj = new Point(0, 0);
        private Point Interim = new Point(0, 0);
        private Point scaleClick = new Point(0, 0);
        private Point pan = new Point(0, 0);
        private Point totalpan = new Point(0, 0);
        private Point panSinceClick = new Point(0, 0);
        private bool rubberband = false;
        private bool verticallock = false;
        private bool horizontallock = false;
        private bool cutting = false;

        int PORTRAIT = 1;
        int LANDSCAPE = 0;
        int PORTRAITLANDSCAPE = 2;

        private Point pointerLocation = new Point(0, 0);

        private PointF reversetrm = new PointF(0.0f, 0.0f);

        public string inputToggle = "";
        public string serialtogglebit = "";
        public string simspeedcommand = "";
        public string keypresschar = "";

        public List<MapObj> Indications = new List<MapObj>();
        public List<List<MapObj>> UndoList = new List<List<MapObj>>();
        int UndoIndex = 0;
        public List<int> HighlightedItems= new List<int>();
        Boolean panInArrowMode = false;
        Boolean panMovement = false;
        
        private List<int> tempHighlighted = new List<int>();
        public List<int> HighlightedItemsCopied = new List<int>();
        public List<int> HighlightedItemsDelete = new List<int>();
        private int Copyitem = -1;
        Point LocationOfMouse = new Point(0, 0);
        public List<Point> InterimOffsets = new List<Point>();
        private int highlighteditem = -1;
        private int lasthighlighteditem = -1;
        private bool supressCommit = false;
        private bool supressNewitem = false;
        private bool oncentre = false;
        private bool onleftedge = false;
        private bool onrightedge = false;
        private bool ontopedge = false;
        private bool onbottomedge = false;

        private bool rightMouseDown = false;
        private bool leftMouseDown = false;
        private bool middleMouseDown = false;

        private Color colorholder = Color.Pink;

        private Point redOffset = new Point(0, 0);
        private Point blueOffset = new Point(0, 0);
        private Point OldRedOffset = new Point(0, 0);
        private Point OldBlueOffset = new Point(0, 0);

        Graphics grfx;

        private double pi = 3.14159;

        public float scaleFactor = 1;

        public ArrayList SimRungs = new ArrayList();
        public ArrayList SimS2PTimers = new ArrayList();
        public ArrayList SimS2DTimers = new ArrayList();
        public ArrayList SimInputs = new ArrayList();
        public bool block = false;
        public string searchString = "";

        Color HighColor, LowColor;

        private bool setupmode = false;

        private int X;
        private int Y;

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox searchString1;

        public frmMChild_SimMap(ArrayList interlockingOldPointer, ArrayList interlockingNewPointer, ArrayList timersOldPointer, 
            ArrayList timersNewPointer, Font drawFont, Color HighColorInput, Color LowColorInput, string ImageDirectory, frmMDIMain parentforminput)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            this.MouseWheel += pictureBox1_MouseWheel;

            parentform = parentforminput;
            // TextBox tb = new TextBox();
            //this.Controls.Add(tb);
            //tb.KeyPress += new KeyPressEventHandler(keypressed);

            //
            // TODO: Add any constructor code after InitializeComponent call

            interlockingOld = interlockingOldPointer;
            interlockingNew = interlockingNewPointer;
            ImageDirectoryPath = ImageDirectory;
            timersOld = timersOldPointer;
            timersNew = timersNewPointer;
            populateInputList("");
            populateAutoComplete();
            //FindBeads();
            //DetermineBeadsLinked();
           // GetOccupiedBeans();
            Indication1.AutoCompleteCustomSource = RungAndInputAutoCompleteSource;
            Indication2.AutoCompleteCustomSource = RungAndInputAutoCompleteSource;
            Indication3.AutoCompleteCustomSource = RungAndInputAutoCompleteSource;
            Indication4.AutoCompleteCustomSource = RungAndInputAutoCompleteSource;
            ControlName.AutoCompleteCustomSource = InputAutCompleteSource;
            drawFnt = drawFont;
            HighColor = HighColorInput;
            LowColor = LowColorInput;

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
                    applyzoom(1/1.05f, unscaledlocation.X, unscaledlocation.Y);
            }


            if ((Control.ModifierKeys & Keys.Shift) != 0)
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
            }
        }

        private int findRung(ArrayList interlocking, string rungName)
        {
            int rungNumber = -1;
            for (int i = 0; i < interlocking.Count; i++)
            {
                ArrayList rungPointer = (ArrayList)interlocking[i];
                if (string.Compare((string)rungPointer[rungPointer.Count - 1], rungName) == 0)
                    rungNumber = (int)rungPointer[0];
            }
            return rungNumber;
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMChild_SimMap));
            this.searchString1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SimMode = new System.Windows.Forms.ToolStripButton();
            this.Design = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.HandButton = new System.Windows.Forms.ToolStripButton();
            this.ArrowButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.Thumbtack = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton18 = new System.Windows.Forms.ToolStripButton();
            this.ZoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton61 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.RectangleButton = new System.Windows.Forms.ToolStripButton();
            this.CircleButton = new System.Windows.Forms.ToolStripButton();
            this.LabelButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox3 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.UndoButton = new System.Windows.Forms.ToolStripButton();
            this.RedoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton62 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton64 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton63 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton58 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.Speed = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton17 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton16 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton13 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton15 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton14 = new System.Windows.Forms.ToolStripButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Indication1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.XCoord = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Height = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Width = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.YCoord = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.BitNumber = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.IndicationColour1 = new System.Windows.Forms.TextBox();
            this.LowColourString = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enlargeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shrinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendToBackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bringToFrontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.degClockwiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.degCounterClockwiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label10 = new System.Windows.Forms.Label();
            this.ShapecomboBox1 = new System.Windows.Forms.ComboBox();
            this.TransparentCheckbox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.IndicationColour4 = new System.Windows.Forms.TextBox();
            this.Indication4 = new System.Windows.Forms.TextBox();
            this.IndicationColour2 = new System.Windows.Forms.TextBox();
            this.Indication2 = new System.Windows.Forms.TextBox();
            this.IndicationColour3 = new System.Windows.Forms.TextBox();
            this.Indication3 = new System.Windows.Forms.TextBox();
            this.LabelText = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ControlName = new System.Windows.Forms.TextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.PointNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XCoordinate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YCoordinate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AngleBox = new System.Windows.Forms.TextBox();
            this.Angle = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.DummyTextBox = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.button13 = new System.Windows.Forms.Button();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Textsizetext = new System.Windows.Forms.TextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.Properties = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.SearchbyString = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton52 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton53 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton54 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton55 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton56 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton24 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton25 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton26 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton27 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton19 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton20 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton21 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton22 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton23 = new System.Windows.Forms.ToolStripButton();
            this.NudgeAmount = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton29 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton28 = new System.Windows.Forms.ToolStripButton();
            this.Enlarge = new System.Windows.Forms.ToolStripButton();
            this.Shrink = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton57 = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.IsTrackCheckbox = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem01 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem02 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem03 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem04 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton30 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton31 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton32 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton33 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton34 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton35 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton36 = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox4 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton37 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton38 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton39 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton40 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton41 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton42 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton43 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton44 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton45 = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton46 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton47 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton48 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton49 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton50 = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox5 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton51 = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox6 = new System.Windows.Forms.ToolStripTextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton59 = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox10 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox9 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton60 = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox12 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox11 = new System.Windows.Forms.ToolStripTextBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString1
            // 
            this.searchString1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.searchString1.Location = new System.Drawing.Point(788, 97);
            this.searchString1.Name = "searchString1";
            this.searchString1.Size = new System.Drawing.Size(52, 20);
            this.searchString1.TabIndex = 15;
            this.searchString1.Visible = false;
            this.searchString1.TextChanged += new System.EventHandler(this.searchString1_TextChanged);
            // 
            // label3
            // 
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(735, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "Search:";
            this.label3.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(839, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 24);
            this.button1.TabIndex = 26;
            this.button1.Text = "Clear";
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.MediumBlue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(324, 93);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.SeaGreen;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Size = new System.Drawing.Size(366, 106);
            this.dataGridView1.TabIndex = 27;
            this.dataGridView1.Visible = false;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1056, 54);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 23);
            this.button2.TabIndex = 28;
            this.button2.Text = "Show/Hide Toolbox";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(699, 238);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(98, 23);
            this.button3.TabIndex = 29;
            this.button3.Text = "Hide Properties";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SimMode,
            this.Design,
            this.toolStripSeparator4,
            this.HandButton,
            this.ArrowButton,
            this.toolStripSeparator6,
            this.Thumbtack,
            this.toolStripButton18,
            this.ZoomOutButton,
            this.toolStripButton61,
            this.toolStripSeparator1,
            this.RectangleButton,
            this.CircleButton,
            this.LabelButton,
            this.toolStripTextBox3,
            this.toolStripSeparator2,
            this.UndoButton,
            this.RedoButton,
            this.toolStripSeparator3,
            this.toolStripButton62,
            this.toolStripButton64,
            this.toolStripButton63,
            this.toolStripButton1,
            this.toolStripButton58,
            this.toolStripLabel1,
            this.Speed,
            this.toolStripButton17,
            this.toolStripButton16,
            this.toolStripButton11,
            this.toolStripButton12,
            this.toolStripButton3,
            this.toolStripButton13,
            this.toolStripButton15,
            this.toolStripButton14});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1527, 25);
            this.toolStrip1.TabIndex = 30;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SimMode
            // 
            this.SimMode.Checked = true;
            this.SimMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SimMode.Image = ((System.Drawing.Image)(resources.GetObject("SimMode.Image")));
            this.SimMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SimMode.Name = "SimMode";
            this.SimMode.Size = new System.Drawing.Size(73, 22);
            this.SimMode.Text = "Simulate";
            this.SimMode.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // Design
            // 
            this.Design.Image = ((System.Drawing.Image)(resources.GetObject("Design.Image")));
            this.Design.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Design.Name = "Design";
            this.Design.Size = new System.Drawing.Size(63, 22);
            this.Design.Text = "Design";
            this.Design.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // HandButton
            // 
            this.HandButton.Checked = true;
            this.HandButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HandButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.HandButton.Image = global::Logic_Navigator.Properties.Resources.Untitled;
            this.HandButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.HandButton.Name = "HandButton";
            this.HandButton.Size = new System.Drawing.Size(23, 22);
            this.HandButton.Text = "Pan";
            this.HandButton.Click += new System.EventHandler(this.toolStripButton1_Click_2);
            // 
            // ArrowButton
            // 
            this.ArrowButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ArrowButton.Image = global::Logic_Navigator.Properties.Resources.pixel_arrow_cursorbrooch;
            this.ArrowButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ArrowButton.Name = "ArrowButton";
            this.ArrowButton.Size = new System.Drawing.Size(23, 22);
            this.ArrowButton.Text = "Arrow";
            this.ArrowButton.Click += new System.EventHandler(this.ArrowButton_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // Thumbtack
            // 
            this.Thumbtack.CheckOnClick = true;
            this.Thumbtack.Image = ((System.Drawing.Image)(resources.GetObject("Thumbtack.Image")));
            this.Thumbtack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Thumbtack.Name = "Thumbtack";
            this.Thumbtack.Size = new System.Drawing.Size(23, 22);
            this.Thumbtack.ToolTipText = "Disable Pan - (Ctrl+T)";
            this.Thumbtack.Click += new System.EventHandler(this.Thumbtack_Click);
            // 
            // toolStripButton18
            // 
            this.toolStripButton18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton18.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton18.Image")));
            this.toolStripButton18.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton18.Name = "toolStripButton18";
            this.toolStripButton18.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton18.Text = "Save Map";
            this.toolStripButton18.ToolTipText = "Save the map - F2";
            this.toolStripButton18.Click += new System.EventHandler(this.toolStripButton18_Click_1);
            // 
            // ZoomOutButton
            // 
            this.ZoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomOutButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomOutButton.Image")));
            this.ZoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomOutButton.Name = "ZoomOutButton";
            this.ZoomOutButton.Size = new System.Drawing.Size(23, 22);
            this.ZoomOutButton.Text = "Save logic state";
            this.ZoomOutButton.ToolTipText = "Save the current logic state. (Ctrl+L)";
            this.ZoomOutButton.Click += new System.EventHandler(this.ZoomOutButton_Click);
            // 
            // toolStripButton61
            // 
            this.toolStripButton61.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton61.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton61.Image")));
            this.toolStripButton61.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton61.Name = "toolStripButton61";
            this.toolStripButton61.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton61.Text = "Load previous logic state file";
            this.toolStripButton61.ToolTipText = "Restore logic state to last saved state. (Ctrl+K)";
            this.toolStripButton61.Click += new System.EventHandler(this.toolStripButton61_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // RectangleButton
            // 
            this.RectangleButton.Image = ((System.Drawing.Image)(resources.GetObject("RectangleButton.Image")));
            this.RectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RectangleButton.Name = "RectangleButton";
            this.RectangleButton.Size = new System.Drawing.Size(79, 22);
            this.RectangleButton.Text = "Rectangle";
            this.RectangleButton.Visible = false;
            this.RectangleButton.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // CircleButton
            // 
            this.CircleButton.Image = ((System.Drawing.Image)(resources.GetObject("CircleButton.Image")));
            this.CircleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CircleButton.Name = "CircleButton";
            this.CircleButton.Size = new System.Drawing.Size(57, 22);
            this.CircleButton.Text = "Circle";
            this.CircleButton.Visible = false;
            this.CircleButton.Click += new System.EventHandler(this.SignalButton_Click);
            // 
            // LabelButton
            // 
            this.LabelButton.Image = ((System.Drawing.Image)(resources.GetObject("LabelButton.Image")));
            this.LabelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LabelButton.Name = "LabelButton";
            this.LabelButton.Size = new System.Drawing.Size(55, 22);
            this.LabelButton.Text = "Label";
            this.LabelButton.Visible = false;
            this.LabelButton.Click += new System.EventHandler(this.LabelButton_Click);
            // 
            // toolStripTextBox3
            // 
            this.toolStripTextBox3.Name = "toolStripTextBox3";
            this.toolStripTextBox3.Size = new System.Drawing.Size(75, 25);
            this.toolStripTextBox3.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator2.Visible = false;
            // 
            // UndoButton
            // 
            this.UndoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UndoButton.Image = global::Logic_Navigator.Properties.Resources.Undo_icon__1_;
            this.UndoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UndoButton.Name = "UndoButton";
            this.UndoButton.Size = new System.Drawing.Size(23, 22);
            this.UndoButton.Text = "Undo";
            this.UndoButton.ToolTipText = "Undo - Ctrl Z";
            this.UndoButton.Visible = false;
            this.UndoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // RedoButton
            // 
            this.RedoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RedoButton.Image = global::Logic_Navigator.Properties.Resources.Redo_icon;
            this.RedoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RedoButton.Name = "RedoButton";
            this.RedoButton.Size = new System.Drawing.Size(23, 22);
            this.RedoButton.Text = "Redo";
            this.RedoButton.ToolTipText = "Redo - Ctrl Y";
            this.RedoButton.Visible = false;
            this.RedoButton.Click += new System.EventHandler(this.RedoButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator3.Visible = false;
            // 
            // toolStripButton62
            // 
            this.toolStripButton62.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton62.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton62.Image")));
            this.toolStripButton62.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton62.Name = "toolStripButton62";
            this.toolStripButton62.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton62.Text = "Fill Picture";
            this.toolStripButton62.ToolTipText = "Fit Screen (Ctrl+W)";
            this.toolStripButton62.Click += new System.EventHandler(this.toolStripButton62_Click);
            // 
            // toolStripButton64
            // 
            this.toolStripButton64.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton64.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton64.Image")));
            this.toolStripButton64.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton64.Name = "toolStripButton64";
            this.toolStripButton64.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton64.Text = "Fill Picture";
            this.toolStripButton64.ToolTipText = "Fit Width";
            this.toolStripButton64.Click += new System.EventHandler(this.toolStripButton64_Click);
            // 
            // toolStripButton63
            // 
            this.toolStripButton63.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton63.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton63.Image")));
            this.toolStripButton63.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton63.Name = "toolStripButton63";
            this.toolStripButton63.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton63.Text = "Fill Picture";
            this.toolStripButton63.ToolTipText = "Fit Height";
            this.toolStripButton63.Click += new System.EventHandler(this.toolStripButton63_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Zoom In";
            this.toolStripButton1.ToolTipText = "Zoom In (Ctrl+\"+\")";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_3);
            // 
            // toolStripButton58
            // 
            this.toolStripButton58.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton58.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton58.Image")));
            this.toolStripButton58.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton58.Name = "toolStripButton58";
            this.toolStripButton58.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton58.Text = "Zoom Out";
            this.toolStripButton58.ToolTipText = "Zoom Out: (Ctrl+\"-\")";
            this.toolStripButton58.Click += new System.EventHandler(this.toolStripButton58_Click_1);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel1.Text = "Sim Speed:";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // Speed
            // 
            this.Speed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Speed.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Speed.Name = "Speed";
            this.Speed.Size = new System.Drawing.Size(30, 25);
            this.Speed.Text = "1";
            // 
            // toolStripButton17
            // 
            this.toolStripButton17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton17.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton17.Image")));
            this.toolStripButton17.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton17.Name = "toolStripButton17";
            this.toolStripButton17.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton17.Text = "÷100";
            this.toolStripButton17.ToolTipText = "Slow speed by factor of 100";
            this.toolStripButton17.Visible = false;
            this.toolStripButton17.Click += new System.EventHandler(this.toolStripButton17_Click_1);
            // 
            // toolStripButton16
            // 
            this.toolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton16.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton16.Image")));
            this.toolStripButton16.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton16.Name = "toolStripButton16";
            this.toolStripButton16.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton16.Text = "÷5";
            this.toolStripButton16.ToolTipText = "Slow speed by factor of 10";
            this.toolStripButton16.Click += new System.EventHandler(this.toolStripButton16_Click_1);
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton11.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton11.Image")));
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton11.Text = "÷2 (Ctrl Tab)";
            this.toolStripButton11.ToolTipText = "Halve the Speed - Left button";
            this.toolStripButton11.Click += new System.EventHandler(this.toolStripButton11_Click_1);
            // 
            // toolStripButton12
            // 
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton12.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton12.Image")));
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton12.Text = "Normal Speed";
            this.toolStripButton12.ToolTipText = "Run at standard speed";
            this.toolStripButton12.Click += new System.EventHandler(this.toolStripButton12_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "x2 - (Tab)";
            this.toolStripButton3.ToolTipText = "Double the Speed - Right button";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click_1);
            // 
            // toolStripButton13
            // 
            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton13.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton13.Image")));
            this.toolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton13.Name = "toolStripButton13";
            this.toolStripButton13.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton13.Text = "x5";
            this.toolStripButton13.ToolTipText = "Multiply speed by 10";
            this.toolStripButton13.Click += new System.EventHandler(this.toolStripButton13_Click);
            // 
            // toolStripButton15
            // 
            this.toolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton15.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton15.Image")));
            this.toolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton15.Name = "toolStripButton15";
            this.toolStripButton15.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton15.Text = "x100";
            this.toolStripButton15.ToolTipText = "Multiply speed by 100";
            this.toolStripButton15.Visible = false;
            this.toolStripButton15.Click += new System.EventHandler(this.toolStripButton15_Click_1);
            // 
            // toolStripButton14
            // 
            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton14.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton14.Image")));
            this.toolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton14.Name = "toolStripButton14";
            this.toolStripButton14.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton14.Text = "/2";
            this.toolStripButton14.ToolTipText = "Half the Speed";
            this.toolStripButton14.Visible = false;
            this.toolStripButton14.Click += new System.EventHandler(this.toolStripButton14_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1160, 90);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(56, 20);
            this.textBox1.TabIndex = 31;
            this.textBox1.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(1222, 90);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(53, 20);
            this.textBox2.TabIndex = 32;
            this.textBox2.Visible = false;
            // 
            // Indication1
            // 
            this.Indication1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Indication1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Indication1.Location = new System.Drawing.Point(9, 98);
            this.Indication1.Name = "Indication1";
            this.Indication1.Size = new System.Drawing.Size(199, 20);
            this.Indication1.TabIndex = 3;
            this.toolTip1.SetToolTip(this.Indication1, resources.GetString("Indication1.ToolTip"));
            this.Indication1.Visible = false;
            this.Indication1.TextChanged += new System.EventHandler(this.Indication_TextChanged);
            this.Indication1.DoubleClick += new System.EventHandler(this.Indication1_DoubleClick);
            // 
            // label1
            // 
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(11, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 15);
            this.label1.TabIndex = 34;
            this.label1.Text = "Indication:";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(6, 276);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 17);
            this.label2.TabIndex = 36;
            this.label2.Text = "X:";
            this.label2.Visible = false;
            // 
            // XCoord
            // 
            this.XCoord.Location = new System.Drawing.Point(40, 275);
            this.XCoord.Name = "XCoord";
            this.XCoord.Size = new System.Drawing.Size(52, 20);
            this.XCoord.TabIndex = 10;
            this.toolTip1.SetToolTip(this.XCoord, "Pixels away from left edge of the window.");
            this.XCoord.Visible = false;
            this.XCoord.TextChanged += new System.EventHandler(this.XCoord_TextChanged);
            // 
            // label4
            // 
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(101, 299);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 20);
            this.label4.TabIndex = 38;
            this.label4.Text = "Height:";
            this.label4.Visible = false;
            // 
            // Height
            // 
            this.Height.Location = new System.Drawing.Point(149, 299);
            this.Height.Name = "Height";
            this.Height.Size = new System.Drawing.Size(52, 20);
            this.Height.TabIndex = 13;
            this.toolTip1.SetToolTip(this.Height, "Height in pixels of the graphic.\r\n");
            this.Height.Visible = false;
            this.Height.TextChanged += new System.EventHandler(this.Height_TextChanged);
            // 
            // label5
            // 
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(101, 275);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 20);
            this.label5.TabIndex = 40;
            this.label5.Text = "Width:";
            this.label5.Visible = false;
            // 
            // Width
            // 
            this.Width.Location = new System.Drawing.Point(149, 276);
            this.Width.Name = "Width";
            this.Width.Size = new System.Drawing.Size(52, 20);
            this.Width.TabIndex = 12;
            this.toolTip1.SetToolTip(this.Width, "Width in pixels of the graphic.");
            this.Width.Visible = false;
            this.Width.TextChanged += new System.EventHandler(this.Width_TextChanged);
            // 
            // label6
            // 
            this.label6.Enabled = false;
            this.label6.Location = new System.Drawing.Point(6, 301);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 20);
            this.label6.TabIndex = 42;
            this.label6.Text = "Y:";
            this.label6.Visible = false;
            // 
            // YCoord
            // 
            this.YCoord.Location = new System.Drawing.Point(40, 301);
            this.YCoord.Name = "YCoord";
            this.YCoord.Size = new System.Drawing.Size(52, 20);
            this.YCoord.TabIndex = 11;
            this.toolTip1.SetToolTip(this.YCoord, "Pixels away from the top of the window");
            this.YCoord.Visible = false;
            this.YCoord.TextChanged += new System.EventHandler(this.YCoord_TextChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(27, 601);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(58, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "OK";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label7
            // 
            this.label7.Enabled = false;
            this.label7.Location = new System.Drawing.Point(23, 437);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 20);
            this.label7.TabIndex = 44;
            this.label7.Text = "Bit Number:";
            this.label7.Visible = false;
            // 
            // BitNumber
            // 
            this.BitNumber.Enabled = false;
            this.BitNumber.Location = new System.Drawing.Point(87, 437);
            this.BitNumber.Name = "BitNumber";
            this.BitNumber.Size = new System.Drawing.Size(52, 20);
            this.BitNumber.TabIndex = 9;
            this.BitNumber.Visible = false;
            // 
            // IndicationColour1
            // 
            this.IndicationColour1.Location = new System.Drawing.Point(214, 98);
            this.IndicationColour1.Name = "IndicationColour1";
            this.IndicationColour1.Size = new System.Drawing.Size(22, 20);
            this.IndicationColour1.TabIndex = 8;
            this.toolTip1.SetToolTip(this.IndicationColour1, "Click to invoke the color dialog box to choose the color of the graphic.");
            this.IndicationColour1.Visible = false;
            this.IndicationColour1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.HighColourString_MouseClick);
            this.IndicationColour1.TextChanged += new System.EventHandler(this.HighColourString_TextChanged);
            // 
            // LowColourString
            // 
            this.LowColourString.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.LowColourString.Location = new System.Drawing.Point(70, 205);
            this.LowColourString.Name = "LowColourString";
            this.LowColourString.Size = new System.Drawing.Size(22, 20);
            this.LowColourString.TabIndex = 16;
            this.toolTip1.SetToolTip(this.LowColourString, "The color of the graphic when there are no indications or none of the indications" +
        " are triggered.");
            this.LowColourString.Visible = false;
            this.LowColourString.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LowColourString_MouseClick);
            // 
            // label9
            // 
            this.label9.Enabled = false;
            this.label9.Location = new System.Drawing.Point(6, 205);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 18);
            this.label9.TabIndex = 51;
            this.label9.Text = "Low Color:";
            this.label9.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.enlargeToolStripMenuItem,
            this.shrinkToolStripMenuItem,
            this.sendToBackToolStripMenuItem,
            this.bringToFrontToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.propertiesToolStripMenuItem,
            this.toolStripMenuItem2,
            this.flipToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(188, 268);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.ShowShortcutKeys = false;
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.cutToolStripMenuItem.Text = "Cut      x";
            this.cutToolStripMenuItem.Visible = false;
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.ShowShortcutKeys = false;
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.copyToolStripMenuItem.Text = "Copy   c";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.ShowShortcutKeys = false;
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.pasteToolStripMenuItem.Text = "Paste   v";
            this.pasteToolStripMenuItem.ToolTipText = "Paste selection";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // enlargeToolStripMenuItem
            // 
            this.enlargeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("enlargeToolStripMenuItem.Image")));
            this.enlargeToolStripMenuItem.Name = "enlargeToolStripMenuItem";
            this.enlargeToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.enlargeToolStripMenuItem.Text = "Enlarge (by 25%)";
            this.enlargeToolStripMenuItem.Click += new System.EventHandler(this.enlargeToolStripMenuItem_Click);
            // 
            // shrinkToolStripMenuItem
            // 
            this.shrinkToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("shrinkToolStripMenuItem.Image")));
            this.shrinkToolStripMenuItem.Name = "shrinkToolStripMenuItem";
            this.shrinkToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.shrinkToolStripMenuItem.Text = "Shrink (by 25%)";
            this.shrinkToolStripMenuItem.Click += new System.EventHandler(this.shrinkToolStripMenuItem_Click);
            // 
            // sendToBackToolStripMenuItem
            // 
            this.sendToBackToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sendToBackToolStripMenuItem.Image")));
            this.sendToBackToolStripMenuItem.Name = "sendToBackToolStripMenuItem";
            this.sendToBackToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.sendToBackToolStripMenuItem.Text = "Send to Back - PgDn";
            this.sendToBackToolStripMenuItem.Click += new System.EventHandler(this.sendToBackToolStripMenuItem_Click);
            // 
            // bringToFrontToolStripMenuItem
            // 
            this.bringToFrontToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("bringToFrontToolStripMenuItem.Image")));
            this.bringToFrontToolStripMenuItem.Name = "bringToFrontToolStripMenuItem";
            this.bringToFrontToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.bringToFrontToolStripMenuItem.Text = "Bring to Front - PgUp";
            this.bringToFrontToolStripMenuItem.Click += new System.EventHandler(this.bringToFrontToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("selectAllToolStripMenuItem.Image")));
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("propertiesToolStripMenuItem.Image")));
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.degClockwiseToolStripMenuItem,
            this.degCounterClockwiseToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(187, 22);
            this.toolStripMenuItem2.Text = "Rotate";
            // 
            // degClockwiseToolStripMenuItem
            // 
            this.degClockwiseToolStripMenuItem.Name = "degClockwiseToolStripMenuItem";
            this.degClockwiseToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.degClockwiseToolStripMenuItem.Text = "90 deg Clockwise";
            this.degClockwiseToolStripMenuItem.Click += new System.EventHandler(this.degClockwiseToolStripMenuItem_Click);
            // 
            // degCounterClockwiseToolStripMenuItem
            // 
            this.degCounterClockwiseToolStripMenuItem.Name = "degCounterClockwiseToolStripMenuItem";
            this.degCounterClockwiseToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.degCounterClockwiseToolStripMenuItem.Text = "90 deg Counter Clockwise";
            this.degCounterClockwiseToolStripMenuItem.Click += new System.EventHandler(this.degCounterClockwiseToolStripMenuItem_Click);
            // 
            // flipToolStripMenuItem
            // 
            this.flipToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verticalToolStripMenuItem,
            this.horizontalToolStripMenuItem});
            this.flipToolStripMenuItem.Name = "flipToolStripMenuItem";
            this.flipToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.flipToolStripMenuItem.Text = "Flip";
            // 
            // verticalToolStripMenuItem
            // 
            this.verticalToolStripMenuItem.Name = "verticalToolStripMenuItem";
            this.verticalToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.verticalToolStripMenuItem.Text = "Vertical Axis";
            this.verticalToolStripMenuItem.Click += new System.EventHandler(this.verticalToolStripMenuItem_Click);
            // 
            // horizontalToolStripMenuItem
            // 
            this.horizontalToolStripMenuItem.Name = "horizontalToolStripMenuItem";
            this.horizontalToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.horizontalToolStripMenuItem.Text = "Horizontal Axis";
            this.horizontalToolStripMenuItem.Click += new System.EventHandler(this.horizontalToolStripMenuItem_Click);
            // 
            // label10
            // 
            this.label10.Enabled = false;
            this.label10.Location = new System.Drawing.Point(5, 242);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 20);
            this.label10.TabIndex = 55;
            this.label10.Text = "Shape:";
            this.label10.Visible = false;
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // ShapecomboBox1
            // 
            this.ShapecomboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ShapecomboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.ShapecomboBox1.FormattingEnabled = true;
            this.ShapecomboBox1.Items.AddRange(new object[] {
            "Rectangle",
            "Circle",
            "Timer",
            "Image",
            "?"});
            this.ShapecomboBox1.Location = new System.Drawing.Point(53, 239);
            this.ShapecomboBox1.Name = "ShapecomboBox1";
            this.ShapecomboBox1.Size = new System.Drawing.Size(105, 21);
            this.ShapecomboBox1.TabIndex = 8;
            this.toolTip1.SetToolTip(this.ShapecomboBox1, "Type of graphic, Rectangle, Circle, Label.  The label types can be changed.");
            this.ShapecomboBox1.Visible = false;
            this.ShapecomboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // TransparentCheckbox
            // 
            this.TransparentCheckbox.AutoSize = true;
            this.TransparentCheckbox.Location = new System.Drawing.Point(98, 207);
            this.TransparentCheckbox.Name = "TransparentCheckbox";
            this.TransparentCheckbox.Size = new System.Drawing.Size(131, 17);
            this.TransparentCheckbox.TabIndex = 7;
            this.TransparentCheckbox.Text = "Transparent when low";
            this.TransparentCheckbox.UseVisualStyleBackColor = true;
            this.TransparentCheckbox.Visible = false;
            this.TransparentCheckbox.CheckedChanged += new System.EventHandler(this.TransparentCheckbox_CheckedChanged);
            // 
            // label8
            // 
            this.label8.Enabled = false;
            this.label8.Location = new System.Drawing.Point(208, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 15);
            this.label8.TabIndex = 48;
            this.label8.Text = "Color:";
            this.label8.Visible = false;
            // 
            // IndicationColour4
            // 
            this.IndicationColour4.Location = new System.Drawing.Point(214, 176);
            this.IndicationColour4.Name = "IndicationColour4";
            this.IndicationColour4.Size = new System.Drawing.Size(22, 20);
            this.IndicationColour4.TabIndex = 14;
            this.IndicationColour4.Visible = false;
            this.IndicationColour4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.IndicationColour4_MouseClick);
            // 
            // Indication4
            // 
            this.Indication4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Indication4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Indication4.Location = new System.Drawing.Point(9, 176);
            this.Indication4.Name = "Indication4";
            this.Indication4.Size = new System.Drawing.Size(199, 20);
            this.Indication4.TabIndex = 6;
            this.Indication4.Visible = false;
            this.Indication4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            this.Indication4.DoubleClick += new System.EventHandler(this.Indication4_DoubleClick);
            // 
            // IndicationColour2
            // 
            this.IndicationColour2.Location = new System.Drawing.Point(214, 124);
            this.IndicationColour2.Name = "IndicationColour2";
            this.IndicationColour2.Size = new System.Drawing.Size(22, 20);
            this.IndicationColour2.TabIndex = 10;
            this.IndicationColour2.Visible = false;
            this.IndicationColour2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox5_MouseClick);
            // 
            // Indication2
            // 
            this.Indication2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Indication2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Indication2.Location = new System.Drawing.Point(9, 124);
            this.Indication2.Name = "Indication2";
            this.Indication2.Size = new System.Drawing.Size(199, 20);
            this.Indication2.TabIndex = 4;
            this.Indication2.Visible = false;
            this.Indication2.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            this.Indication2.DoubleClick += new System.EventHandler(this.Indication2_DoubleClick);
            // 
            // IndicationColour3
            // 
            this.IndicationColour3.Location = new System.Drawing.Point(214, 150);
            this.IndicationColour3.Name = "IndicationColour3";
            this.IndicationColour3.Size = new System.Drawing.Size(22, 20);
            this.IndicationColour3.TabIndex = 12;
            this.IndicationColour3.Visible = false;
            this.IndicationColour3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.IndicationColour3_MouseClick);
            // 
            // Indication3
            // 
            this.Indication3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Indication3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Indication3.Location = new System.Drawing.Point(9, 150);
            this.Indication3.Name = "Indication3";
            this.Indication3.Size = new System.Drawing.Size(199, 20);
            this.Indication3.TabIndex = 5;
            this.Indication3.Visible = false;
            this.Indication3.TextChanged += new System.EventHandler(this.textBox8_TextChanged);
            this.Indication3.DoubleClick += new System.EventHandler(this.Indication3_DoubleClick);
            // 
            // LabelText
            // 
            this.LabelText.Location = new System.Drawing.Point(35, 57);
            this.LabelText.Name = "LabelText";
            this.LabelText.Size = new System.Drawing.Size(130, 20);
            this.LabelText.TabIndex = 2;
            this.toolTip1.SetToolTip(this.LabelText, "Enter the text that will be shown in a Label Graphic");
            this.LabelText.Visible = false;
            this.LabelText.TextChanged += new System.EventHandler(this.LabelText_TextChanged);
            // 
            // label11
            // 
            this.label11.Enabled = false;
            this.label11.Location = new System.Drawing.Point(6, 59);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 18);
            this.label11.TabIndex = 64;
            this.label11.Text = "Text:";
            this.label11.Visible = false;
            // 
            // label12
            // 
            this.label12.Enabled = false;
            this.label12.Location = new System.Drawing.Point(7, 32);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 19);
            this.label12.TabIndex = 65;
            this.label12.Text = "Control:";
            this.label12.Visible = false;
            // 
            // ControlName
            // 
            this.ControlName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ControlName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.ControlName.Location = new System.Drawing.Point(49, 32);
            this.ControlName.Name = "ControlName";
            this.ControlName.Size = new System.Drawing.Size(187, 20);
            this.ControlName.TabIndex = 1;
            this.toolTip1.SetToolTip(this.ControlName, "Enter the name of the function that will toggle if the graphic is pressed. eg \"13" +
        "RR\" will mean the input 13RR will change its state.");
            this.ControlName.Visible = false;
            this.ControlName.TextChanged += new System.EventHandler(this.ControlName_TextChanged);
            this.ControlName.DoubleClick += new System.EventHandler(this.ControlName_DoubleClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PointNumber,
            this.XCoordinate,
            this.YCoordinate});
            this.dataGridView2.Location = new System.Drawing.Point(27, 460);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(240, 150);
            this.dataGridView2.TabIndex = 67;
            this.dataGridView2.Visible = false;
            // 
            // PointNumber
            // 
            this.PointNumber.HeaderText = "Polygon Point";
            this.PointNumber.Name = "PointNumber";
            this.PointNumber.Width = 60;
            // 
            // XCoordinate
            // 
            this.XCoordinate.HeaderText = "X Coordinate";
            this.XCoordinate.Name = "XCoordinate";
            this.XCoordinate.Width = 70;
            // 
            // YCoordinate
            // 
            this.YCoordinate.HeaderText = "Y Coordinate";
            this.YCoordinate.Name = "YCoordinate";
            this.YCoordinate.Width = 70;
            // 
            // AngleBox
            // 
            this.AngleBox.Location = new System.Drawing.Point(98, 321);
            this.AngleBox.Name = "AngleBox";
            this.AngleBox.Size = new System.Drawing.Size(52, 20);
            this.AngleBox.TabIndex = 14;
            this.toolTip1.SetToolTip(this.AngleBox, resources.GetString("AngleBox.ToolTip"));
            this.AngleBox.Visible = false;
            this.AngleBox.TextChanged += new System.EventHandler(this.AngleBox_TextChanged);
            // 
            // Angle
            // 
            this.Angle.Enabled = false;
            this.Angle.Location = new System.Drawing.Point(6, 324);
            this.Angle.Name = "Angle";
            this.Angle.Size = new System.Drawing.Size(86, 20);
            this.Angle.TabIndex = 69;
            this.Angle.Text = "Rotation Angle:";
            this.Angle.Visible = false;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(865, 238);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(56, 22);
            this.button5.TabIndex = 73;
            this.button5.Text = "Enlarge";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(927, 237);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(50, 23);
            this.button6.TabIndex = 74;
            this.button6.Text = "Shrink";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Visible = false;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(1116, 238);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(50, 23);
            this.button9.TabIndex = 78;
            this.button9.Text = "Delete";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Visible = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(981, 60);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(54, 20);
            this.textBox3.TabIndex = 79;
            this.textBox3.Visible = false;
            // 
            // DummyTextBox
            // 
            this.DummyTextBox.Location = new System.Drawing.Point(1405, 622);
            this.DummyTextBox.Name = "DummyTextBox";
            this.DummyTextBox.Size = new System.Drawing.Size(52, 20);
            this.DummyTextBox.TabIndex = 80;
            this.DummyTextBox.Visible = false;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(1041, 61);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(56, 20);
            this.textBox4.TabIndex = 81;
            this.textBox4.Visible = false;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(1222, 63);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(51, 20);
            this.textBox5.TabIndex = 82;
            this.textBox5.Visible = false;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(1279, 63);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(51, 20);
            this.textBox6.TabIndex = 83;
            this.textBox6.Visible = false;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(1259, 89);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 20);
            this.textBox7.TabIndex = 84;
            this.textBox7.Visible = false;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(1259, 115);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(100, 20);
            this.textBox8.TabIndex = 85;
            this.textBox8.Visible = false;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(1259, 141);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(100, 20);
            this.textBox9.TabIndex = 86;
            this.textBox9.Visible = false;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(1087, 89);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(67, 20);
            this.textBox10.TabIndex = 87;
            this.textBox10.Visible = false;
            // 
            // label13
            // 
            this.label13.Enabled = false;
            this.label13.Location = new System.Drawing.Point(983, 240);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 20);
            this.label13.TabIndex = 88;
            this.label13.Text = "Scale by:";
            this.label13.Visible = false;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(1037, 239);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(35, 20);
            this.textBox11.TabIndex = 89;
            this.textBox11.Text = "1.00";
            this.textBox11.Visible = false;
            this.textBox11.TextChanged += new System.EventHandler(this.textBox11_TextChanged);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(1078, 238);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(32, 22);
            this.button10.TabIndex = 90;
            this.button10.Text = "Go";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Visible = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(1172, 238);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(57, 23);
            this.button11.TabIndex = 91;
            this.button11.Text = "Flip Vert";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Visible = false;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(1232, 238);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(57, 23);
            this.button12.TabIndex = 92;
            this.button12.Text = "Flip Hor";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Visible = false;
            // 
            // label14
            // 
            this.label14.Enabled = false;
            this.label14.Location = new System.Drawing.Point(812, 240);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 20);
            this.label14.TabIndex = 93;
            this.label14.Text = "Selection:";
            this.label14.Visible = false;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(1414, 236);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(32, 22);
            this.button13.TabIndex = 96;
            this.button13.Text = "Go";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Visible = false;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(1349, 239);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(35, 20);
            this.textBox12.TabIndex = 95;
            this.textBox12.Text = "1.00";
            this.textBox12.Visible = false;
            // 
            // label15
            // 
            this.label15.Enabled = false;
            this.label15.Location = new System.Drawing.Point(1295, 240);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 20);
            this.label15.TabIndex = 94;
            this.label15.Text = "Rotate By:";
            this.label15.Visible = false;
            // 
            // label16
            // 
            this.label16.Enabled = false;
            this.label16.Location = new System.Drawing.Point(1390, 239);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 20);
            this.label16.TabIndex = 97;
            this.label16.Text = "deg";
            this.label16.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Rectangle",
            "Circle",
            "Timer",
            "?"});
            this.comboBox1.Location = new System.Drawing.Point(102, 616);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(105, 21);
            this.comboBox1.TabIndex = 100;
            this.comboBox1.Visible = false;
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(768, 55);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(207, 20);
            this.textBox13.TabIndex = 102;
            this.textBox13.Visible = false;
            // 
            // Textsizetext
            // 
            this.Textsizetext.Location = new System.Drawing.Point(196, 56);
            this.Textsizetext.Name = "Textsizetext";
            this.Textsizetext.Size = new System.Drawing.Size(40, 20);
            this.Textsizetext.TabIndex = 70;
            this.toolTip1.SetToolTip(this.Textsizetext, "Text size in percent");
            this.Textsizetext.Visible = false;
            this.Textsizetext.TextChanged += new System.EventHandler(this.Textsizetext_TextChanged);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Properties,
            this.toolStripButton4,
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolStripButton8,
            this.toolStripButton9,
            this.toolStripSeparator5,
            this.toolStripButton10,
            this.SearchbyString,
            this.toolStripButton52,
            this.toolStripButton53,
            this.toolStripButton54,
            this.toolStripButton55,
            this.toolStripButton56,
            this.toolStripLabel2,
            this.toolStripButton24,
            this.toolStripButton25,
            this.toolStripButton26,
            this.toolStripButton27,
            this.toolStripButton19,
            this.toolStripButton20,
            this.toolStripButton21,
            this.toolStripButton22,
            this.toolStripButton23,
            this.NudgeAmount,
            this.toolStripLabel3,
            this.toolStripButton29,
            this.toolStripButton28,
            this.Enlarge,
            this.Shrink,
            this.toolStripButton2,
            this.toolStripTextBox1,
            this.toolStripButton57,
            this.toolStripTextBox2,
            this.toolStripButton5});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1527, 25);
            this.toolStrip2.TabIndex = 103;
            this.toolStrip2.Text = "toolStrip2";
            this.toolStrip2.Visible = false;
            // 
            // Properties
            // 
            this.Properties.Image = ((System.Drawing.Image)(resources.GetObject("Properties.Image")));
            this.Properties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Properties.Name = "Properties";
            this.Properties.Size = new System.Drawing.Size(108, 22);
            this.Properties.Text = "Hide Properties";
            this.Properties.Click += new System.EventHandler(this.toolStripButton2_Click_1);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Delete";
            this.toolStripButton4.ToolTipText = "Delete - Del";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(75, 22);
            this.toolStripButton6.Text = "Select All";
            this.toolStripButton6.ToolTipText = "Select All (Ctrl+A)";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton7.Text = "Cut";
            this.toolStripButton7.ToolTipText = "Cut - x";
            this.toolStripButton7.Visible = false;
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton8.Text = "Copy";
            this.toolStripButton8.ToolTipText = "Copy - c";
            this.toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton9.Image")));
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton9.Text = "Paste";
            this.toolStripButton9.ToolTipText = "Paste - v";
            this.toolStripButton9.Click += new System.EventHandler(this.toolStripButton9_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton10.Image")));
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(122, 22);
            this.toolStripButton10.Text = "Select by keyword";
            this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // SearchbyString
            // 
            this.SearchbyString.Name = "SearchbyString";
            this.SearchbyString.Size = new System.Drawing.Size(70, 25);
            this.SearchbyString.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchbyString_KeyPress);
            this.SearchbyString.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchbyString_KeyUp);
            this.SearchbyString.Click += new System.EventHandler(this.SearchbyString_Click);
            // 
            // toolStripButton52
            // 
            this.toolStripButton52.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton52.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton52.Image")));
            this.toolStripButton52.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton52.Name = "toolStripButton52";
            this.toolStripButton52.Size = new System.Drawing.Size(50, 22);
            this.toolStripButton52.Text = "Nudge:";
            // 
            // toolStripButton53
            // 
            this.toolStripButton53.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton53.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton53.Image")));
            this.toolStripButton53.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton53.Name = "toolStripButton53";
            this.toolStripButton53.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton53.Text = "Up";
            this.toolStripButton53.Click += new System.EventHandler(this.toolStripButton53_Click);
            // 
            // toolStripButton54
            // 
            this.toolStripButton54.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton54.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton54.Image")));
            this.toolStripButton54.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton54.Name = "toolStripButton54";
            this.toolStripButton54.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton54.Text = "Down";
            this.toolStripButton54.Click += new System.EventHandler(this.toolStripButton54_Click);
            // 
            // toolStripButton55
            // 
            this.toolStripButton55.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton55.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton55.Image")));
            this.toolStripButton55.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton55.Name = "toolStripButton55";
            this.toolStripButton55.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton55.Text = "Left";
            this.toolStripButton55.Click += new System.EventHandler(this.toolStripButton55_Click);
            // 
            // toolStripButton56
            // 
            this.toolStripButton56.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton56.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton56.Image")));
            this.toolStripButton56.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton56.Name = "toolStripButton56";
            this.toolStripButton56.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton56.Text = "Right";
            this.toolStripButton56.Click += new System.EventHandler(this.toolStripButton56_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(30, 22);
            this.toolStripLabel2.Text = "Size:";
            // 
            // toolStripButton24
            // 
            this.toolStripButton24.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton24.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton24.Image")));
            this.toolStripButton24.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton24.Name = "toolStripButton24";
            this.toolStripButton24.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton24.Text = "Wider";
            this.toolStripButton24.ToolTipText = "Wider (Alt+Right)";
            this.toolStripButton24.Click += new System.EventHandler(this.toolStripButton24_Click);
            // 
            // toolStripButton25
            // 
            this.toolStripButton25.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton25.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton25.Image")));
            this.toolStripButton25.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton25.Name = "toolStripButton25";
            this.toolStripButton25.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton25.Text = "Thiner";
            this.toolStripButton25.ToolTipText = "Thiner (Alt +Left)";
            this.toolStripButton25.Click += new System.EventHandler(this.toolStripButton25_Click);
            // 
            // toolStripButton26
            // 
            this.toolStripButton26.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton26.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton26.Image")));
            this.toolStripButton26.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton26.Name = "toolStripButton26";
            this.toolStripButton26.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton26.Text = "Taller";
            this.toolStripButton26.ToolTipText = "Taller (Alt+Down)";
            this.toolStripButton26.Click += new System.EventHandler(this.toolStripButton26_Click);
            // 
            // toolStripButton27
            // 
            this.toolStripButton27.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton27.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton27.Image")));
            this.toolStripButton27.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton27.Name = "toolStripButton27";
            this.toolStripButton27.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton27.Text = "Shorter";
            this.toolStripButton27.ToolTipText = "Shorter (Alt+Up)";
            this.toolStripButton27.Click += new System.EventHandler(this.toolStripButton27_Click);
            // 
            // toolStripButton19
            // 
            this.toolStripButton19.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton19.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton19.Image")));
            this.toolStripButton19.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton19.Name = "toolStripButton19";
            this.toolStripButton19.Size = new System.Drawing.Size(40, 22);
            this.toolStripButton19.Text = "Push:";
            // 
            // toolStripButton20
            // 
            this.toolStripButton20.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton20.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton20.Image")));
            this.toolStripButton20.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton20.Name = "toolStripButton20";
            this.toolStripButton20.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton20.Text = "Up";
            this.toolStripButton20.ToolTipText = "Up (Ctrl+Up)";
            this.toolStripButton20.Click += new System.EventHandler(this.toolStripButton20_Click);
            // 
            // toolStripButton21
            // 
            this.toolStripButton21.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton21.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton21.Image")));
            this.toolStripButton21.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton21.Name = "toolStripButton21";
            this.toolStripButton21.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton21.Text = "Down";
            this.toolStripButton21.ToolTipText = "Down (Ctrl+Dn)";
            this.toolStripButton21.Click += new System.EventHandler(this.toolStripButton21_Click);
            // 
            // toolStripButton22
            // 
            this.toolStripButton22.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton22.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton22.Image")));
            this.toolStripButton22.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton22.Name = "toolStripButton22";
            this.toolStripButton22.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton22.Text = "Left";
            this.toolStripButton22.ToolTipText = "Left (Ctrl+Left)";
            this.toolStripButton22.Click += new System.EventHandler(this.toolStripButton22_Click);
            // 
            // toolStripButton23
            // 
            this.toolStripButton23.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton23.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton23.Image")));
            this.toolStripButton23.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton23.Name = "toolStripButton23";
            this.toolStripButton23.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton23.Text = "Right";
            this.toolStripButton23.ToolTipText = "Right (Ctrl+Right)";
            this.toolStripButton23.Click += new System.EventHandler(this.toolStripButton23_Click);
            // 
            // NudgeAmount
            // 
            this.NudgeAmount.AutoSize = false;
            this.NudgeAmount.DropDownWidth = 75;
            this.NudgeAmount.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "10",
            "20",
            "30",
            "50",
            "100",
            "1000"});
            this.NudgeAmount.Name = "NudgeAmount";
            this.NudgeAmount.Size = new System.Drawing.Size(75, 23);
            this.NudgeAmount.Text = "20";
            this.NudgeAmount.ToolTipText = "Nudge Amount";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(35, 22);
            this.toolStripLabel3.Text = "Lock:";
            // 
            // toolStripButton29
            // 
            this.toolStripButton29.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton29.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton29.Image")));
            this.toolStripButton29.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton29.Name = "toolStripButton29";
            this.toolStripButton29.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton29.Text = "Lock Horizontal";
            this.toolStripButton29.Click += new System.EventHandler(this.toolStripButton29_Click);
            // 
            // toolStripButton28
            // 
            this.toolStripButton28.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton28.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton28.Image")));
            this.toolStripButton28.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton28.Name = "toolStripButton28";
            this.toolStripButton28.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton28.Text = "Lock Vertical";
            this.toolStripButton28.Click += new System.EventHandler(this.toolStripButton28_Click);
            // 
            // Enlarge
            // 
            this.Enlarge.Image = ((System.Drawing.Image)(resources.GetObject("Enlarge.Image")));
            this.Enlarge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Enlarge.Name = "Enlarge";
            this.Enlarge.Size = new System.Drawing.Size(66, 22);
            this.Enlarge.Text = "Enlarge";
            this.Enlarge.Visible = false;
            this.Enlarge.Click += new System.EventHandler(this.Enlarge_Click);
            // 
            // Shrink
            // 
            this.Shrink.Image = ((System.Drawing.Image)(resources.GetObject("Shrink.Image")));
            this.Shrink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Shrink.Name = "Shrink";
            this.Shrink.Size = new System.Drawing.Size(60, 22);
            this.Shrink.Text = "Shrink";
            this.Shrink.Visible = false;
            this.Shrink.Click += new System.EventHandler(this.Shrink_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(73, 22);
            this.toolStripButton2.Text = "Scale By:";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click_2);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(40, 25);
            this.toolStripTextBox1.Text = "1.0";
            // 
            // toolStripButton57
            // 
            this.toolStripButton57.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton57.Image")));
            this.toolStripButton57.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton57.Name = "toolStripButton57";
            this.toolStripButton57.Size = new System.Drawing.Size(111, 22);
            this.toolStripButton57.Text = "Rotate By (deg):";
            this.toolStripButton57.Visible = false;
            this.toolStripButton57.Click += new System.EventHandler(this.toolStripButton57_Click);
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(40, 25);
            this.toolStripTextBox2.Text = "90";
            this.toolStripTextBox2.Visible = false;
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(126, 22);
            this.toolStripButton5.Text = "Show/Hide Special";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.LabelText);
            this.panel1.Controls.Add(this.ControlName);
            this.panel1.Controls.Add(this.Textsizetext);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.IsTrackCheckbox);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.Indication4);
            this.panel1.Controls.Add(this.IndicationColour4);
            this.panel1.Controls.Add(this.IndicationColour3);
            this.panel1.Controls.Add(this.IndicationColour2);
            this.panel1.Controls.Add(this.Indication3);
            this.panel1.Controls.Add(this.Indication2);
            this.panel1.Controls.Add(this.Indication1);
            this.panel1.Controls.Add(this.IndicationColour1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.Angle);
            this.panel1.Controls.Add(this.XCoord);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Height);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.Width);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.YCoord);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.AngleBox);
            this.panel1.Controls.Add(this.ShapecomboBox1);
            this.panel1.Controls.Add(this.LowColourString);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.TransparentCheckbox);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Location = new System.Drawing.Point(26, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 353);
            this.panel1.TabIndex = 105;
            this.panel1.Visible = false;
            // 
            // label21
            // 
            this.label21.Enabled = false;
            this.label21.Location = new System.Drawing.Point(169, 58);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(33, 18);
            this.label21.TabIndex = 71;
            this.label21.Text = "Size:";
            this.label21.Visible = false;
            // 
            // IsTrackCheckbox
            // 
            this.IsTrackCheckbox.AutoSize = true;
            this.IsTrackCheckbox.Location = new System.Drawing.Point(164, 239);
            this.IsTrackCheckbox.Name = "IsTrackCheckbox";
            this.IsTrackCheckbox.Size = new System.Drawing.Size(54, 17);
            this.IsTrackCheckbox.TabIndex = 9;
            this.IsTrackCheckbox.Text = "Track";
            this.IsTrackCheckbox.UseVisualStyleBackColor = true;
            this.IsTrackCheckbox.Visible = false;
            this.IsTrackCheckbox.CheckedChanged += new System.EventHandler(this.IsTrackCheckbox_CheckedChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(6, 6);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(81, 20);
            this.label17.TabIndex = 0;
            this.label17.Text = "Properties";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem01,
            this.toolStripMenuItem02,
            this.toolStripMenuItem03,
            this.toolStripMenuItem04});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(105, 92);
            this.contextMenuStrip2.Text = "Inspect";
            // 
            // toolStripMenuItem01
            // 
            this.toolStripMenuItem01.Name = "toolStripMenuItem01";
            this.toolStripMenuItem01.Size = new System.Drawing.Size(104, 22);
            this.toolStripMenuItem01.Text = "Item1";
            this.toolStripMenuItem01.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem02
            // 
            this.toolStripMenuItem02.Name = "toolStripMenuItem02";
            this.toolStripMenuItem02.Size = new System.Drawing.Size(104, 22);
            this.toolStripMenuItem02.Text = "Item2";
            this.toolStripMenuItem02.Click += new System.EventHandler(this.toolStripMenuItem02_Click);
            // 
            // toolStripMenuItem03
            // 
            this.toolStripMenuItem03.Name = "toolStripMenuItem03";
            this.toolStripMenuItem03.Size = new System.Drawing.Size(104, 22);
            this.toolStripMenuItem03.Text = "Item3";
            this.toolStripMenuItem03.Click += new System.EventHandler(this.toolStripMenuItem03_Click);
            // 
            // toolStripMenuItem04
            // 
            this.toolStripMenuItem04.Name = "toolStripMenuItem04";
            this.toolStripMenuItem04.Size = new System.Drawing.Size(104, 22);
            this.toolStripMenuItem04.Text = "Item4";
            this.toolStripMenuItem04.Click += new System.EventHandler(this.toolStripMenuItem04_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 694);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1527, 22);
            this.statusStrip1.TabIndex = 106;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip3.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip3_ItemClicked);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton30,
            this.toolStripButton31,
            this.toolStripButton32,
            this.toolStripButton33,
            this.toolStripButton34,
            this.toolStripButton35,
            this.toolStripButton36,
            this.toolStripTextBox4,
            this.toolStripButton37,
            this.toolStripButton38,
            this.toolStripButton39,
            this.toolStripButton40,
            this.toolStripButton41,
            this.toolStripLabel4,
            this.toolStripButton42,
            this.toolStripButton43,
            this.toolStripButton44,
            this.toolStripButton45,
            this.toolStripComboBox1,
            this.toolStripLabel5,
            this.toolStripButton46,
            this.toolStripButton47,
            this.toolStripButton48,
            this.toolStripButton49,
            this.toolStripButton50,
            this.toolStripTextBox5,
            this.toolStripButton51,
            this.toolStripTextBox6});
            this.toolStrip3.Location = new System.Drawing.Point(0, 25);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(1527, 25);
            this.toolStrip3.TabIndex = 107;
            this.toolStrip3.Text = "toolStrip3";
            this.toolStrip3.Visible = false;
            // 
            // toolStripButton30
            // 
            this.toolStripButton30.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton30.Image")));
            this.toolStripButton30.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton30.Name = "toolStripButton30";
            this.toolStripButton30.Size = new System.Drawing.Size(108, 22);
            this.toolStripButton30.Text = "Hide Properties";
            // 
            // toolStripButton31
            // 
            this.toolStripButton31.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton31.Image")));
            this.toolStripButton31.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton31.Name = "toolStripButton31";
            this.toolStripButton31.Size = new System.Drawing.Size(60, 22);
            this.toolStripButton31.Text = "Delete";
            // 
            // toolStripButton32
            // 
            this.toolStripButton32.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton32.Image")));
            this.toolStripButton32.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton32.Name = "toolStripButton32";
            this.toolStripButton32.Size = new System.Drawing.Size(75, 22);
            this.toolStripButton32.Text = "Select All";
            // 
            // toolStripButton33
            // 
            this.toolStripButton33.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton33.Image")));
            this.toolStripButton33.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton33.Name = "toolStripButton33";
            this.toolStripButton33.Size = new System.Drawing.Size(46, 22);
            this.toolStripButton33.Text = "Cut";
            this.toolStripButton33.Visible = false;
            // 
            // toolStripButton34
            // 
            this.toolStripButton34.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton34.Image")));
            this.toolStripButton34.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton34.Name = "toolStripButton34";
            this.toolStripButton34.Size = new System.Drawing.Size(55, 22);
            this.toolStripButton34.Text = "Copy";
            // 
            // toolStripButton35
            // 
            this.toolStripButton35.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton35.Image")));
            this.toolStripButton35.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton35.Name = "toolStripButton35";
            this.toolStripButton35.Size = new System.Drawing.Size(55, 22);
            this.toolStripButton35.Text = "Paste";
            this.toolStripButton35.Visible = false;
            // 
            // toolStripButton36
            // 
            this.toolStripButton36.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton36.Image")));
            this.toolStripButton36.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton36.Name = "toolStripButton36";
            this.toolStripButton36.Size = new System.Drawing.Size(122, 22);
            this.toolStripButton36.Text = "Select by keyword";
            // 
            // toolStripTextBox4
            // 
            this.toolStripTextBox4.Name = "toolStripTextBox4";
            this.toolStripTextBox4.Size = new System.Drawing.Size(70, 25);
            // 
            // toolStripButton37
            // 
            this.toolStripButton37.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton37.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton37.Image")));
            this.toolStripButton37.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton37.Name = "toolStripButton37";
            this.toolStripButton37.Size = new System.Drawing.Size(50, 22);
            this.toolStripButton37.Text = "Nudge:";
            // 
            // toolStripButton38
            // 
            this.toolStripButton38.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton38.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton38.Image")));
            this.toolStripButton38.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton38.Name = "toolStripButton38";
            this.toolStripButton38.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton38.Text = "Up";
            // 
            // toolStripButton39
            // 
            this.toolStripButton39.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton39.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton39.Image")));
            this.toolStripButton39.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton39.Name = "toolStripButton39";
            this.toolStripButton39.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton39.Text = "Down";
            // 
            // toolStripButton40
            // 
            this.toolStripButton40.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton40.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton40.Image")));
            this.toolStripButton40.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton40.Name = "toolStripButton40";
            this.toolStripButton40.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton40.Text = "Left";
            // 
            // toolStripButton41
            // 
            this.toolStripButton41.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton41.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton41.Image")));
            this.toolStripButton41.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton41.Name = "toolStripButton41";
            this.toolStripButton41.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton41.Text = "Right";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(30, 22);
            this.toolStripLabel4.Text = "Size:";
            // 
            // toolStripButton42
            // 
            this.toolStripButton42.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton42.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton42.Image")));
            this.toolStripButton42.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton42.Name = "toolStripButton42";
            this.toolStripButton42.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton42.Text = "Wider";
            // 
            // toolStripButton43
            // 
            this.toolStripButton43.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton43.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton43.Image")));
            this.toolStripButton43.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton43.Name = "toolStripButton43";
            this.toolStripButton43.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton43.Text = "Thiner";
            // 
            // toolStripButton44
            // 
            this.toolStripButton44.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton44.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton44.Image")));
            this.toolStripButton44.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton44.Name = "toolStripButton44";
            this.toolStripButton44.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton44.Text = "Taller";
            // 
            // toolStripButton45
            // 
            this.toolStripButton45.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton45.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton45.Image")));
            this.toolStripButton45.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton45.Name = "toolStripButton45";
            this.toolStripButton45.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton45.Text = "Shorter";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.AutoSize = false;
            this.toolStripComboBox1.DropDownWidth = 75;
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "10",
            "20",
            "30",
            "50",
            "100",
            "1000"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(75, 23);
            this.toolStripComboBox1.Text = "1";
            this.toolStripComboBox1.ToolTipText = "Simulation Speed";
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(35, 22);
            this.toolStripLabel5.Text = "Lock:";
            // 
            // toolStripButton46
            // 
            this.toolStripButton46.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton46.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton46.Image")));
            this.toolStripButton46.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton46.Name = "toolStripButton46";
            this.toolStripButton46.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton46.Text = "Lock Horizontal";
            // 
            // toolStripButton47
            // 
            this.toolStripButton47.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton47.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton47.Image")));
            this.toolStripButton47.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton47.Name = "toolStripButton47";
            this.toolStripButton47.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton47.Text = "Lock Vertical";
            // 
            // toolStripButton48
            // 
            this.toolStripButton48.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton48.Image")));
            this.toolStripButton48.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton48.Name = "toolStripButton48";
            this.toolStripButton48.Size = new System.Drawing.Size(66, 22);
            this.toolStripButton48.Text = "Enlarge";
            this.toolStripButton48.Visible = false;
            // 
            // toolStripButton49
            // 
            this.toolStripButton49.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton49.Image")));
            this.toolStripButton49.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton49.Name = "toolStripButton49";
            this.toolStripButton49.Size = new System.Drawing.Size(60, 22);
            this.toolStripButton49.Text = "Shrink";
            this.toolStripButton49.Visible = false;
            // 
            // toolStripButton50
            // 
            this.toolStripButton50.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton50.Image")));
            this.toolStripButton50.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton50.Name = "toolStripButton50";
            this.toolStripButton50.Size = new System.Drawing.Size(73, 22);
            this.toolStripButton50.Text = "Scale By:";
            // 
            // toolStripTextBox5
            // 
            this.toolStripTextBox5.Name = "toolStripTextBox5";
            this.toolStripTextBox5.Size = new System.Drawing.Size(40, 25);
            this.toolStripTextBox5.Text = "1.0";
            // 
            // toolStripButton51
            // 
            this.toolStripButton51.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton51.Image")));
            this.toolStripButton51.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton51.Name = "toolStripButton51";
            this.toolStripButton51.Size = new System.Drawing.Size(111, 22);
            this.toolStripButton51.Text = "Rotate By (deg):";
            // 
            // toolStripTextBox6
            // 
            this.toolStripTextBox6.Name = "toolStripTextBox6";
            this.toolStripTextBox6.Size = new System.Drawing.Size(40, 25);
            this.toolStripTextBox6.Text = "90";
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(1182, 28);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(23, 20);
            this.textBox14.TabIndex = 108;
            this.textBox14.Text = "inputtoggle";
            this.textBox14.Visible = false;
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(1211, 28);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(56, 20);
            this.textBox15.TabIndex = 109;
            this.textBox15.Text = "inputtoggle";
            this.textBox15.Visible = false;
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(1349, 0);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(178, 20);
            this.textBox16.TabIndex = 110;
            this.textBox16.Text = "inputtoggle";
            this.textBox16.Visible = false;
            // 
            // toolStrip4
            // 
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton59,
            this.toolStripTextBox10,
            this.toolStripTextBox9,
            this.toolStripButton60,
            this.toolStripTextBox12,
            this.toolStripTextBox11});
            this.toolStrip4.Location = new System.Drawing.Point(0, 25);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(1527, 25);
            this.toolStrip4.TabIndex = 111;
            this.toolStrip4.Text = "toolStrip4";
            this.toolStrip4.Visible = false;
            // 
            // toolStripButton59
            // 
            this.toolStripButton59.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton59.Image")));
            this.toolStripButton59.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton59.Name = "toolStripButton59";
            this.toolStripButton59.Size = new System.Drawing.Size(82, 22);
            this.toolStripButton59.Text = "Circularize";
            this.toolStripButton59.ToolTipText = "Type \'o\'";
            this.toolStripButton59.Click += new System.EventHandler(this.toolStripButton59_Click);
            // 
            // toolStripTextBox10
            // 
            this.toolStripTextBox10.Name = "toolStripTextBox10";
            this.toolStripTextBox10.Size = new System.Drawing.Size(40, 25);
            this.toolStripTextBox10.Text = "100";
            // 
            // toolStripTextBox9
            // 
            this.toolStripTextBox9.Name = "toolStripTextBox9";
            this.toolStripTextBox9.Size = new System.Drawing.Size(40, 25);
            this.toolStripTextBox9.Text = "5";
            // 
            // toolStripButton60
            // 
            this.toolStripButton60.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton60.Image")));
            this.toolStripButton60.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton60.Name = "toolStripButton60";
            this.toolStripButton60.Size = new System.Drawing.Size(58, 22);
            this.toolStripButton60.Text = "Angle";
            this.toolStripButton60.ToolTipText = "Type \'/\'";
            this.toolStripButton60.Click += new System.EventHandler(this.toolStripButton60_Click);
            // 
            // toolStripTextBox12
            // 
            this.toolStripTextBox12.Name = "toolStripTextBox12";
            this.toolStripTextBox12.Size = new System.Drawing.Size(40, 25);
            this.toolStripTextBox12.Text = "45";
            // 
            // toolStripTextBox11
            // 
            this.toolStripTextBox11.Name = "toolStripTextBox11";
            this.toolStripTextBox11.Size = new System.Drawing.Size(40, 25);
            this.toolStripTextBox11.Text = "100";
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(375, 42);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(54, 20);
            this.textBox17.TabIndex = 112;
            this.textBox17.Visible = false;
            this.textBox17.TextChanged += new System.EventHandler(this.textBox17_TextChanged);
            // 
            // textBox18
            // 
            this.textBox18.Location = new System.Drawing.Point(375, 65);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(54, 20);
            this.textBox18.TabIndex = 113;
            this.textBox18.Visible = false;
            // 
            // textBox19
            // 
            this.textBox19.Location = new System.Drawing.Point(375, 85);
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new System.Drawing.Size(54, 20);
            this.textBox19.TabIndex = 114;
            this.textBox19.Visible = false;
            // 
            // label18
            // 
            this.label18.Enabled = false;
            this.label18.Location = new System.Drawing.Point(307, 44);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 20);
            this.label18.TabIndex = 115;
            this.label18.Text = "totalpan";
            this.label18.Visible = false;
            // 
            // label19
            // 
            this.label19.Enabled = false;
            this.label19.Location = new System.Drawing.Point(306, 67);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 20);
            this.label19.TabIndex = 116;
            this.label19.Text = "scaleoffset";
            this.label19.Visible = false;
            // 
            // label20
            // 
            this.label20.Enabled = false;
            this.label20.Location = new System.Drawing.Point(288, 88);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(85, 20);
            this.label20.TabIndex = 117;
            this.label20.Text = "clientsize.width";
            this.label20.Visible = false;
            // 
            // frmMChild_SimMap
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1354, 749);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.textBox19);
            this.Controls.Add(this.textBox18);
            this.Controls.Add(this.textBox17);
            this.Controls.Add(this.toolStrip4);
            this.Controls.Add(this.textBox16);
            this.Controls.Add(this.textBox15);
            this.Controls.Add(this.textBox14);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip3);
            this.Controls.Add(this.textBox13);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.textBox12);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.textBox11);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.DummyTextBox);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.BitNumber);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.searchString1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMChild_SimMap";
            this.Text = "Map View";
            this.Load += new System.EventHandler(this.frmMChild_SimMap_Load);
            this.Click += new System.EventHandler(this.frmMChild_SimMap_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMChild_SimMap_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMChild_SimMap_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMChild_SimMap_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMChild_SimMap_KeyUp);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.frmMChild_SimMap_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMChild_SimMap_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMChild_SimMap_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmMChild_SimMap_MouseUp);
            this.Resize += new System.EventHandler(this.frmMChild_SimMap_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void populateInputList(string searchString)
        {
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Coil";
            dataGridView1.Columns[1].Name = "Status";
            dataGridView1.Columns[1].Width = 150;
            getInputs(searchString);
        }

        private void getInputs(string searchString)
        {
            int rungNumber = 0;
            string name = "";
            string[] row = new string[] { "1", "" };

            ArrayList Coils = new ArrayList();
            try
            {
                getRungs(Coils, interlockingNew);
                //getTimers(Coils, interlockingNew);
                for (int r = 0; r < Coils.Count; r++)
                {
                    if (Coils[r].ToString().LastIndexOf(searchString) != -1)
                        if (Coils[r].ToString() != "")
                        {
                            row = new string[] { Coils[r].ToString(), "Low" };//+ " - Timing (30/345s)"};
                            dataGridView1.Rows.Add(row);
                        }
                }
            }
            catch
            {
                MessageBox.Show("Error populating Coil list (rung number:" + rungNumber.ToString() + ", " + name.ToString() + ")", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void populateAutoComplete()
        {
            ArrayList Coils = new ArrayList();
            ArrayList Inputs = new ArrayList();

            try
            {
                getRungs(Coils, interlockingNew);
                //getTimers(Coils, interlockingNew);
                for (int r = 0; r < Coils.Count; r++)
                {
                    if (Coils[r].ToString().LastIndexOf(searchString) != -1)
                        if (Coils[r].ToString() != "")
                        {
                            RungAndInputAutoCompleteSource.Add(Coils[r].ToString());
                        }
                }
               // getRungs(Coils, interlockingNew);
                for (int r = 0; r < interlockingNew.Count; r++)
                {
                    ArrayList rungPointer = (ArrayList)interlockingNew[r];
                    for (int k = 1; k < rungPointer.Count - 1; k++)
                    {
                        Contact contact = (Contact)rungPointer[k];
                        if (!inList(contact.name, Inputs) && !inList(contact.name, Coils))
                            if (contact.name.LastIndexOf(searchString) != -1)
                                if (contact.name != "")
                                {
                                    InputAutCompleteSource.Add(contact.name);   
                                    RungAndInputAutoCompleteSource.Add(contact.name);                              
                                }
                    }
                }
            }            
            catch
            {
                MessageBox.Show("Error populating AutoComplete list)", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void getRungs(ArrayList Coils, ArrayList interlockingNewPointer)
        {
            string name = "";
            for (int i = 0; i < interlockingNewPointer.Count; i++)
            {
                ArrayList rungPointer = (ArrayList)interlockingNewPointer[i];
                name = (string)rungPointer[rungPointer.Count - 1];
                Coils.Add(name);
            }
        }

        private bool inList(string name, ArrayList list)
        {
            for (int i = 0; i < list.Count; i++)
                if (name == (string)list[i]) return true;
            return false;
        }

        private void searchString1_TextChanged(object sender, System.EventArgs e)
        {
            searchStringTextChanged();
        }

        public void searchStringTextChanged()
        {
            block = true;
            string input = (string)searchString1.Text;
            searchString = input;

            while (dataGridView1.Rows.Count > 1)
                if (!dataGridView1.Rows[0].IsNewRow)
                    dataGridView1.Rows.RemoveAt(0);
            getInputs(input);
            CopySimInputsToDataGrid();
            block = false;
        }

        public void CommitSearchString()
        {
            searchString1.Text = searchString;
        }

        private void ShowRungWindow()
        {
            int newIndex = findRung(interlockingNew, rungName);
            int oldIndex = findRung(interlockingOld, rungName);

            if ((newIndex != -1) && (newIndex != -1))
            {
                frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, oldIndex - 1,
                    newIndex - 1, 0.75F, "Normal", drawFnt, "", false, true, HighColor, LowColor);
                objfrmMChild.Size = new Size(objfrmMChild.RecommendedWidthofWindow(newIndex - 1), objfrmMChild.RecommendedHeightofWindow(newIndex - 1));
                objfrmMChild.Location = new System.Drawing.Point(1, 1);
                objfrmMChild.Text = rungName;
                objfrmMChild.MdiParent = this.MdiParent;
                objfrmMChild.Show();
            }

            findusages(rungName);
            
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            searchString1.Text = "";
            while (dataGridView1.Rows.Count > 1)
                if (!dataGridView1.Rows[0].IsNewRow)
                    dataGridView1.Rows.RemoveAt(0);
            getInputs("");
            CopySimInputsToDataGrid();
        }

        private DataGridView dataGridView1;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView1.CurrentCell.RowIndex;
            rungName = (string)dataGridView1.Rows[row].Cells[0].Value;
            ShowRungWindow();
        }

        private void UpdateSimInputs()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value == "High")
                    AddToSimInputs((string)dataGridView1.Rows[i].Cells[0].Value, true/*Is High*/);
                if (dataGridView1.Rows[i].Cells[1].Value == "Low")
                    AddToSimInputs((string)dataGridView1.Rows[i].Cells[0].Value, false/*Is Low*/);
            }
        }

        private void AddToSimInputs(string input, bool IsHigh)
        {
            bool found = false;
            for (int i = 0; i < SimRungs.Count; i++)
                if (SimRungs[i].ToString() == input)
                {
                    if (!IsHigh) //Input went low so take it out of the list
                        SimRungs.RemoveAt(i);
                    found = true;
                }
            if (IsHigh && !found) SimRungs.Add(input);
        }

        public void UpdateSimRungsList(ArrayList extSimRungs, ArrayList extS2PtimersTiming, ArrayList extS2DtimersTiming) //Reverse direction to UpdateSimInputs
        {
            DuplicateList(extSimRungs, SimRungs);
            DuplicateTimersList(extS2PtimersTiming, SimS2PTimers);
            DuplicateTimersList(extS2DtimersTiming, SimS2DTimers);
            Invalidate();
        }

        public void UpdateSimInputsList(ArrayList extSimInputs)
        {
            DuplicateList(extSimInputs, SimInputs);
            Invalidate();
        }

        private void CopySimInputsToDataGrid()
        {
            try
            {

                bool found = false;
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    found = false;
                    for (int i = 0; i < SimRungs.Count; i++)
                    {
                        if ((string)dataGridView1.Rows[j].Cells[0].Value == SimRungs[i].ToString())
                        {
                            found = true;
                            TimersTimingStruct timerelement = getTimerElement((string)dataGridView1.Rows[j].Cells[0].Value, SimS2DTimers);
                            if (timerelement.timername != "notimerfound")
                                dataGridView1.Rows[j].Cells[1].Value = "High " + "- Low in " +
                                    (timerelement.totaltime - timerelement.timeElapsed) + "s (" + timerelement.totaltime + "s timer)";
                            else
                                dataGridView1.Rows[j].Cells[1].Value = "High";
                            dataGridView1.Rows[j].DefaultCellStyle.ForeColor = Color.Red;
                        }
                    }
                    if (!found && (dataGridView1.Rows[j].Cells[0].Value != null))
                    {
                        TimersTimingStruct timerelement = getTimerElement((string)dataGridView1.Rows[j].Cells[0].Value, SimS2PTimers);
                        if (timerelement.timername != "notimerfound")
                            dataGridView1.Rows[j].Cells[1].Value = "Low " + "- High in " +
                                (timerelement.totaltime - timerelement.timeElapsed) + "s (" + timerelement.totaltime + "s timer)";
                        else
                            dataGridView1.Rows[j].Cells[1].Value = "Low";
                        dataGridView1.Rows[j].DefaultCellStyle.ForeColor = Color.Blue;
                    }
                }
            }
            catch { MessageBox.Show("Error Updating Simulation Rung Panel", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
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

        private void DuplicateList(ArrayList original, ArrayList duplicate)
        {
            duplicate.Clear();
            for (int i = 0; i < original.Count; i++)
                duplicate.Add(original[i].ToString());
        }

        private void DuplicateTimersList(ArrayList original, ArrayList duplicate)
        {
            duplicate.Clear();
            for (int i = 0; i < original.Count; i++)
                duplicate.Add(original[i]);
        }

        private Button button2;

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Visible)
            {
                dataGridView1.Visible = false;
                searchString1.Visible = false;
                label3.Visible = false;
                button1.Visible = false;
            }
            else
            {
                dataGridView1.Visible = true;
                searchString1.Visible = true;
                label3.Visible = true;
                button1.Visible = true;
            }
        }

        private Button button3;

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Hide Properties")
            {
                button3.Text = "Show Properties";
                SimMode.Checked = false;
                ShowHideToolbar("Hide");

            }
            else
            {
                button3.Text = "Hide Properties";
                SimMode.Checked = true;
                ShowHideToolbar("Show");
            }
        }

        private void ShowHideToolbar(string instruction)
        {
            if (instruction == "Show")
            {
                XCoord.Visible = true;
                YCoord.Visible = true;
                Textsizetext.Visible = true;
                label21.Visible = true;
                label2.Visible = true;
                label6.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                Width.Visible = true;
                Height.Visible = true;
                AngleBox.Visible = true;
                Angle.Visible = true;
                label1.Visible = true;
                label8.Visible = true;
                Indication1.Visible = true;
                Indication2.Visible = true;
                Indication3.Visible = true;
                Indication4.Visible = true;
                IndicationColour1.Visible = true;
                IndicationColour2.Visible = true;
                IndicationColour3.Visible = true;
                IndicationColour4.Visible = true;
                label12.Visible = true;
                label11.Visible = true;
                ControlName.Visible = true;
                LabelText.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                LowColourString.Visible = true;
                TransparentCheckbox.Visible = true;
                ShapecomboBox1.Visible = true;
                //label7.Visible = true;
                //BitNumber.Visible = true;
                label17.Visible = true;
                panel1.Visible = true;
                //panel2.Visible = true;
            }
            else
            {
                XCoord.Visible = false;
                YCoord.Visible = false;
                Textsizetext.Visible = false;
                label21.Visible = false;
                label2.Visible = false;
                label6.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                Width.Visible = false;
                Height.Visible = false;
                AngleBox.Visible = false;
                Angle.Visible = false;
                label1.Visible = false;
                label8.Visible = false;
                Indication1.Visible = false;
                Indication2.Visible = false;
                Indication3.Visible = false;
                Indication4.Visible = false;
                IndicationColour1.Visible = false;
                IndicationColour2.Visible = false;
                IndicationColour3.Visible = false;
                IndicationColour4.Visible = false;
                label12.Visible = false;
                label11.Visible = false;
                ControlName.Visible = false;
                LabelText.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                LowColourString.Visible = false;
                TransparentCheckbox.Visible = false;
                ShapecomboBox1.Visible = false;
                //label7.Visible = false;
                //BitNumber.Visible = false;
                label17.Visible = false;
                panel1.Visible = false;
                //panel2.Visible = true;
            }
        }      


        private ToolStrip toolStrip1;
        private ToolStripButton RectangleButton;


        private ToolStripButton CircleButton;
        private ToolStripButton ArrowButton;


        private void frmMChild_SimMap_MouseDown(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null;
            panInArrowMode = false; panMovement = false;
            try
            {
                Point eLocation = new Point(0, 0);
                eLocation.X = (int)(((float)e.Location.X)/ scaleFactor);
                eLocation.Y = (int)(((float)e.Location.Y)/ scaleFactor);

                if (e.Button == MouseButtons.Right)
                   rightMouseDown = true;
                if ((e.Button == MouseButtons.Right) && !SimMode.Checked)
                {
                    if (ArrowButton.Checked)
                    {
                        HandButton.Checked = true;
                        panInArrowMode = true;
                    }
                }
                
                if ((e.Button == MouseButtons.Left) || panInArrowMode)
                {
                    if (true)
                    {
                        click = eLocation;
                        Start = click;
                        End = click;
                        Boolean inHighlightedItemsList = false;
                        for(int i = 0; i < HighlightedItems.Count; i ++)
                            if(highlighteditem == HighlightedItems[i])
                                inHighlightedItemsList = true;                        
                        if(!inHighlightedItemsList)// (highlighteditem == -1)
                        {
                            rubberband = true;
                            if((Control.ModifierKeys & Keys.Control) == 0)
                                HighlightedItems.Clear();
                        }

                        leftMouseDown = true;
                    }

                    if(!setupmode && HandButton.Checked)
                    {

                    }
                    Invalidate();
                    
                }

                if (e.Button == MouseButtons.Middle)
                {
                    click.X = Cursor.Position.X;
                    click.Y = Cursor.Position.Y;
                    middleMouseDown = true;
                    if (highlighteditem != -1)
                        Indications.RemoveAt(highlighteditem);
                    Invalidate();
                }
                if ((e.Button == MouseButtons.Left) && (e.Clicks == 1))
                {
                }
            }
            catch { MessageBox.Show("Mouse Down" + highlighteditem.ToString() + " ", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void EditProperties()
        {
            try
            {
                supressCommit = true;
                MapObj item;
                TransparentCheckbox.BackColor = Color.White;
                if (highlighteditem != -1)
                {
                    item = Indications[highlighteditem];
                    Textsizetext.Text = item.Textsize.ToString();
                    XCoord.Text = item.StartLocation.X.ToString();
                    YCoord.Text = item.StartLocation.Y.ToString();
                    Width.Text = (item.EndLocation.X - item.StartLocation.X).ToString();
                    Height.Text = (item.EndLocation.Y - item.StartLocation.Y).ToString();
                    BitNumber.Text = highlighteditem.ToString();
                    //IndicationColour1.BackColor = item.HighColour;
                    LowColourString.BackColor = item.LowColour;
                    ShapecomboBox1.Text = item.Shape;
                    LabelText.Text = item.Text;
                    Indication1.Text = item.Indication1;
                    Indication2.Text = item.Indication2;
                    Indication3.Text = item.Indication3;
                    Indication4.Text = item.Indication4;
                    IndicationColour1.BackColor = item.IndColour1;
                    IndicationColour2.BackColor = item.IndColour2;
                    IndicationColour3.BackColor = item.IndColour3;
                    IndicationColour4.BackColor = item.IndColour4;
                    ControlName.Text = item.Control;        
                    AngleBox.Text = item.RotationAngle.ToString();
                    IsTrackCheckbox.Checked = item.IsTrack; 
                    TransparentCheckbox.Checked = item.Transparent;
                }
                if (HighlightedItems.Count > 0)
                {
                    if (Indications.Count > HighlightedItems[0])
                    {
                        item = Indications[HighlightedItems[0]];
                        Textsizetext.Text = item.Textsize.ToString();
                        XCoord.Text = item.StartLocation.X.ToString();
                        YCoord.Text = item.StartLocation.Y.ToString();
                        Width.Text = (item.EndLocation.X - item.StartLocation.X).ToString();
                        Height.Text = (item.EndLocation.Y - item.StartLocation.Y).ToString();
                        BitNumber.Text = highlighteditem.ToString();
                        //IndicationColour1.BackColor = item.HighColour;
                        LowColourString.BackColor = item.LowColour;
                        ShapecomboBox1.Text = item.Shape;
                        LabelText.Text = item.Text;
                        Indication1.Text = item.Indication1;
                        Indication2.Text = item.Indication2;
                        Indication3.Text = item.Indication3;
                        Indication4.Text = item.Indication4;
                        IndicationColour1.BackColor = item.IndColour1;
                        IndicationColour2.BackColor = item.IndColour2;
                        IndicationColour3.BackColor = item.IndColour3;
                        IndicationColour4.BackColor = item.IndColour4;
                        ControlName.Text = item.Control;        
                        AngleBox.Text = item.RotationAngle.ToString();
                        IsTrackCheckbox.Checked = item.IsTrack;
                        TransparentCheckbox.Checked = item.Transparent;
                        for (int i = 0; i < HighlightedItems.Count; i++)
                        {
                            if (Indications.Count > HighlightedItems[i])
                            {
                                item = Indications[HighlightedItems[i]];
                                if (Textsizetext.Text != item.Textsize.ToString()) Textsizetext.Text = "?";
                                if (XCoord.Text != item.StartLocation.X.ToString()) XCoord.Text = "?";
                                if (YCoord.Text != item.StartLocation.Y.ToString()) YCoord.Text = "?";
                                if (Width.Text != (item.EndLocation.X - item.StartLocation.X).ToString()) Width.Text = "?";
                                if (Height.Text != (item.EndLocation.Y - item.StartLocation.Y).ToString()) Height.Text = "?";
                                if (BitNumber.Text != i.ToString()) BitNumber.Text = "?";
                                if (ShapecomboBox1.Text != item.Shape) ShapecomboBox1.Text = "?";
                                if (LabelText.Text != item.Text) LabelText.Text = "?";
                                //if (IndicationColour1.BackColor != item.HighColour) IndicationColour1.BackColor = Color.Gray;
                                if (LowColourString.BackColor != item.LowColour) LowColourString.BackColor = Color.Silver;
                                if (Indication1.Text != item.Indication1) Indication1.Text = "?";
                                if (Indication2.Text != item.Indication2) Indication2.Text = "?";
                                if (Indication3.Text != item.Indication3) Indication3.Text = "?";
                                if (Indication4.Text != item.Indication4) Indication4.Text = "?";
                                if (IndicationColour1.BackColor != item.IndColour1) IndicationColour1.BackColor = Color.Silver;
                                if (IndicationColour2.BackColor != item.IndColour2) IndicationColour2.BackColor = Color.Silver;
                                if (IndicationColour3.BackColor != item.IndColour3) IndicationColour3.BackColor = Color.Silver;
                                if (IndicationColour4.BackColor != item.IndColour4) IndicationColour4.BackColor = Color.Silver;
                                if (ControlName.Text != item.Control) ControlName.Text = "?";                                
                                if (TransparentCheckbox.Checked != item.Transparent) TransparentCheckbox.BackColor = Color.Gray;
                                if (IsTrackCheckbox.Checked != item.Transparent) IsTrackCheckbox.BackColor = Color.Gray;
                                if (AngleBox.Text != item.RotationAngle.ToString()) AngleBox.Text = "?";
                            }
                        }
                    }
                }
                supressCommit = false;
            }
            catch { MessageBox.Show("Problem with Edit Properties" + highlighteditem.ToString() + " ", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            Invalidate();
        }

        private void frmMChild_SimMap_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {                
                rubberband = false;
                Point eLocation = new Point(0, 0);
                eLocation.X = (int)(((float)e.Location.X) / scaleFactor);
                eLocation.Y = (int)(((float)e.Location.Y) / scaleFactor);
                
                if ((leftMouseDown) && (!HandButton.Checked))
                {
                    leftMouseDown = false;
                    if (Start == End)
                    {
                        Point pointerLocation = new Point(0, 0);
                        pointerLocation.X = (int)((float)(e.Location.X / scaleFactor) + totalpan.X);
                        pointerLocation.Y = (int)((float)(e.Location.Y / scaleFactor) + totalpan.Y);
                        bool blank = true;
                        for (int i = 0; i < Indications.Count; i++)
                        {
                            MapObj item = Indications[Indications.Count - (i + 1)];
                            if (InBoundsRotated(item, pointerLocation) && blank && !InHighlightedList(Indications.Count - (i + 1)))
                            {
                                HighlightedItems.Add(Indications.Count - (i + 1));
                                InterimOffsets.Add(new Point(0, 0));
                                blank = false;
                            }
                            if (InBoundsRotated(item, pointerLocation) && blank && InHighlightedList(Indications.Count - (i + 1)))
                            {
                                removeItemFromHighlightedList(Indications.Count - (i + 1));
                                blank = false;
                            }
                            if ((blank) && (Control.ModifierKeys & Keys.Control) == 0)
                                HighlightedItems.Clear();
                            //  inputToggle = item.Control;
                        }
                        if(!setupmode)
                        {
                            for (int i = 0; i < Indications.Count; i++)
                            {
                                MapObj item = Indications[i];
                                if (InBoundsRotated(item, pointerLocation))                                 
                                    if(true)
                                        if (!item.IsTrack)
                                        {
                                            inputToggle = item.Control + ";";
                                            textBox14.Text = inputToggle;
                                            serialtogglebit = "";
                                            if (item.Name.IndexOf("CONTROLSWITCH#") != -1) serialtogglebit = item.Name;
                                            if (item.Indication1.IndexOf("CONTROLSWITCH#") != -1) serialtogglebit = item.Indication1;
                                            if (item.Indication2.IndexOf("CONTROLSWITCH#") != -1) serialtogglebit = item.Indication2;
                                            if (item.Indication3.IndexOf("CONTROLSWITCH#") != -1) serialtogglebit = item.Indication3;
                                            if (item.Indication4.IndexOf("CONTROLSWITCH#") != -1) serialtogglebit = item.Indication4;
                                            if (serialtogglebit != "") parentform.doserial(serialtogglebit);
                                        }
                            }
                        }                                    
                    }
                    if (!supressNewitem && (RectangleButton.Checked || CircleButton.Checked || LabelButton.Checked))
                    {
                        MapObj item = new MapObj();
                        item.HighColour = Color.LightSlateGray;
                        item.LowColour = Color.FromArgb(224, 224, 224);
                        item.Shape = "?";
                        item.Textsize = 12;
                        if (RectangleButton.Checked) item.Shape = "Rectangle";
                        if (CircleButton.Checked) item.Shape = "Circle";
                        if (LabelButton.Checked)  item.Shape = "Label"; 
                        Startadj.X = (int)((float)((Start.X) + totalpan.X));
                        Startadj.Y = (int)((float)((Start.Y) + totalpan.Y));
                        if((item.Shape == "Label") || (item.Shape == "Rectangle") || (item.Shape == "Circle"))
                        {
                            if ((Startadj.X == Endadj.X) && (Startadj.Y == Endadj.Y))
                            {
                                if(item.Shape == "Label")
                                {
                                    Endadj.X = (int)((float)((End.X) + totalpan.X + 127));
                                    Endadj.Y = (int)((float)((End.Y) + totalpan.Y + 20));
                                }
                                if (item.Shape == "Rectangle")
                                {
                                    Endadj.X = (int)((float)((End.X) + totalpan.X + 100));
                                    Endadj.Y = (int)((float)((End.Y) + totalpan.Y + 10));
                                }
                                if (item.Shape == "Circle")
                                {
                                    Endadj.X = (int)((float)((End.X) + totalpan.X + 25));
                                    Endadj.Y = (int)((float)((End.Y) + totalpan.Y + 25));
                                }
                            }
                            else
                            {
                                Endadj.X = (int)((float)((End.X) + totalpan.X));
                                Endadj.Y = (int)((float)((End.Y) + totalpan.Y));
                            }
                        }
                        else
                        {
                            Endadj.X = (int)((float)((End.X) + totalpan.X));
                            Endadj.Y = (int)((float)((End.Y) + totalpan.Y));
                        }
                        item.StartLocation = NormaliseStart(Startadj, Endadj);
                        item.EndLocation = NormaliseEnd(Startadj, Endadj);
                        item.IndColour1 = Color.Gray;
                        item.IndColour2 = Color.Gray;
                        item.IndColour3 = Color.Gray;
                        item.IndColour4 = Color.Gray;
                        item.Control = "";
                        item.Indication1 = "";
                        item.Indication2 = "";
                        item.Indication3 = "";
                        item.Indication4 = "";
                        item.Name = "InsertNameHere";
                        item.Text = "{Insert Text Here}";
                        item.TypeofObj = "Indication";
                        item.RotationAngle = 0;
                        item.IsTrack = false;
                        Indications.Add(item);
                    }
                    Start.X = 0; Start.Y = 0; 
                    End.X = 0; End.Y = 0;
                    Interim.X = 0; Interim.Y = 0;
                    Point zeropoint = new Point(0, 0);
                    for (int i = 0; i < InterimOffsets.Count; i++)
                        InterimOffsets[i] = zeropoint;
                    highlighteditem = Indications.Count - 1;
                    EditProperties();
                    PushIndications();
                    Invalidate();
                }
                if (leftMouseDown && HandButton.Checked)
                {
                    leftMouseDown = false;
                    //if (!Thumbtack.Checked)
                    {
                        pan.X += panSinceClick.X;
                        pan.Y += panSinceClick.Y;
                    }
                    panSinceClick.X = 0;
                    panSinceClick.Y = 0;
                    textBox4.Text = pan.ToString();
                    textBox5.Text = totalpan.ToString();
                    textBox6.Text = panSinceClick.ToString();
                    if (Start == End)
                    {
                        Point pointerLocation = new Point(0, 0);
                        pointerLocation.X = (int)((float)(e.Location.X / scaleFactor) + totalpan.X);
                        pointerLocation.Y = (int)((float)(e.Location.Y / scaleFactor) + totalpan.Y);
                        for (int i = 0; i < Indications.Count; i++)
                        {
                            MapObj item = Indications[i];
                            if (InBoundsRotated(item, pointerLocation))
                                if (true)
                                {
                                    //if(inputToggle != "")
                                    if (IsUp(item.Control))
                                        RemoveRungfromList(item.Control, SimInputs);
                                    else SimInputs.Add(item.Control);

                                    inputToggle = item.Control + ";" + inputToggle;                                    
                                    textBox14.Text = SimInputs.Count.ToString();
                                    serialtogglebit = "";
                                    if (item.Name.IndexOf("CONTROLSWITCH#") != -1) serialtogglebit = item.Name;
                                    if (item.Text.IndexOf("CONTROLSWITCH#") != -1) serialtogglebit = item.Text;
                                    if (item.Indication1.IndexOf("CONTROLSWITCH#") != -1) serialtogglebit = item.Indication1;
                                    if (item.Indication2.IndexOf("CONTROLSWITCH#") != -1) serialtogglebit = item.Indication2;
                                    if (item.Indication3.IndexOf("CONTROLSWITCH#") != -1) serialtogglebit = item.Indication3;
                                    if (item.Indication4.IndexOf("CONTROLSWITCH#") != -1) serialtogglebit = item.Indication4;
                                    if (serialtogglebit != "")
                                        parentform.doserial(serialtogglebit);
                                }
                        }
                    }
                }
                if (rightMouseDown == true)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        click = eLocation;
                        Start = click;
                        End = click;
                        //rightMouseDown = true;
                        LocationOfMouse = eLocation;
                        if(!HandButton.Checked || (panInArrowMode && !panMovement))
                            contextMenuStrip1.Show(Cursor.Position);
                        if(HandButton.Checked && !panInArrowMode)
                        {
                            //contextMenuStrip2.Items.Clear();
                            for (int i = 0; i < Indications.Count; i++)
                            {
                                MapObj item = Indications[i];
                                if (InBoundsRotated(item, pointerLocation))
                                {
                                    contextMenuStrip2.Items[0].Visible = false;
                                    contextMenuStrip2.Items[1].Visible = false;
                                    contextMenuStrip2.Items[2].Visible = false;
                                    contextMenuStrip2.Items[3].Visible = false; 
                                    if (item.Indication1 != "") { contextMenuStrip2.Items[0].Visible = true; contextMenuStrip2.Items[0].Text = Trimmed(item.Indication1);  }
                                    if (item.Indication2 != "") { contextMenuStrip2.Items[1].Visible = true; contextMenuStrip2.Items[1].Text = Trimmed(item.Indication2); }
                                    if (item.Indication3 != "") { contextMenuStrip2.Items[2].Visible = true; contextMenuStrip2.Items[2].Text = Trimmed(item.Indication3); }
                                    if (item.Indication4 != "") { contextMenuStrip2.Items[3].Visible = true; contextMenuStrip2.Items[3].Text = Trimmed(item.Indication4);  }
                                }
                                contextMenuStrip2.Show(Cursor.Position);
                            }
                        }

                    }
                    rightMouseDown = false;
                }
                if (middleMouseDown == true) middleMouseDown = false;
                if (panInArrowMode)
                {
                    HandButton.Checked = false; panInArrowMode = false; panMovement = false;
                }

            }
            catch { MessageBox.Show("Mouse Up" + highlighteditem.ToString() + "," + Indications.Count.ToString() + " ", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

        }

        private void RemoveRungfromList(string name, ArrayList list)
        {
            for (int i = 0; i < list.Count; i++)
                if (name == (string)list[i])
                    list.RemoveAt(i);
        }

        private string Trimmed(string ind)
        {
            if(ind.Substring(0,1) == "~")
                return(ind.Substring(1));
            return (ind);
        }

        private bool InHighlightedList(int itemnumber)
        {
            for (int i = 0; i < HighlightedItems.Count; i++)
                if(HighlightedItems[i] == itemnumber)
                    return(true);
            return(false);
        }

        private void frmMChild_SimMap_MouseMove(object sender, MouseEventArgs e)
        {
            
            try{
                //debug
                textBox17.Text = totalpan.X.ToString();
                textBox18.Text = scaleFactor.ToString();
                textBox19.Text = ClientSize.Width.ToString();



            Point eLocation = new Point(0, 0);
            unscaledlocation = e.Location;
            eLocation.X = (int) ((float) e.Location.X / scaleFactor);
            eLocation.Y = (int) ((float) e.Location.Y / scaleFactor);            
            toolStripTextBox3.Text = eLocation.ToString();
            supressNewitem = false;
            mouselocation = eLocation;
            if ((leftMouseDown == true) && (!HandButton.Checked))
            {
                try{
                End = eLocation;
                if (highlighteditem != -1)
                {
                    for (int i = 0; i < HighlightedItems.Count; i++)
                    {
                        try
                        {
                            if ((InterimOffsets.Count > i) && (Indications.Count > HighlightedItems[i]))
                            {
                                Point EndLocation = new Point(0, 0);
                                MapObj item = Indications[HighlightedItems[i]];
                                Point delta = new Point(Start.X - End.X, Start.Y - End.Y);
                                if (verticallock) delta = new Point(Start.X - End.X, 0);
                                if (horizontallock) delta = new Point(0, Start.Y - End.Y);
                                if ((onrightedge) || (!onleftedge && !onrightedge && !ontopedge && !onbottomedge))
                                    item.EndLocation.X -= delta.X - InterimOffsets[i].X;
                                if ((onbottomedge) || (!onleftedge && !onrightedge && !ontopedge && !onbottomedge))
                                    item.EndLocation.Y -= delta.Y - InterimOffsets[i].Y;
                                if ((onleftedge) || (!onleftedge && !onrightedge && !ontopedge && !onbottomedge))
                                    item.StartLocation.X -= delta.X - InterimOffsets[i].X;
                                if ((ontopedge) || (!onleftedge && !onrightedge && !ontopedge && !onbottomedge))
                                    item.StartLocation.Y -= delta.Y - InterimOffsets[i].Y;
                                Interim = delta;
                                InterimOffsets[i] = delta;
                                Indications[HighlightedItems[i]] = item;
                                supressNewitem = true;
                            }

                        }
                        catch { MessageBox.Show("Mouse Move, Highlighted Items failure", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    }
                    {
                        try
                        {
                            if (Indications.Count > highlighteditem)
                            {
                                Point EndLocation = new Point(0, 0);
                                MapObj item = Indications[highlighteditem];
                                Point delta = new Point(Start.X - End.X, Start.Y - End.Y);
                                if (verticallock) delta = new Point(Start.X - End.X, 0);
                                if (horizontallock) delta = new Point(0, Start.Y - End.Y);
                                if ((onrightedge) || (!onleftedge && !onrightedge && !ontopedge && !onbottomedge))
                                    item.EndLocation.X -= delta.X - Interim.X;
                                if ((onbottomedge) || (!onleftedge && !onrightedge && !ontopedge && !onbottomedge))
                                    item.EndLocation.Y -= delta.Y - Interim.Y;
                                if ((onleftedge) || (!onleftedge && !onrightedge && !ontopedge && !onbottomedge))
                                    item.StartLocation.X -= delta.X - Interim.X;
                                if ((ontopedge) || (!onleftedge && !onrightedge && !ontopedge && !onbottomedge))
                                    item.StartLocation.Y -= delta.Y - Interim.Y;
                                Interim = delta;
                                Indications[highlighteditem] = item;
                                supressNewitem = true;
                            }
                        }
                        catch { MessageBox.Show("Mouse Move, leftMouseDown:" + leftMouseDown.ToString() + ", Movement ", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    }
                }
                if ((ArrowButton.Checked) && (highlighteditem == -1))    
                {         
                    GetListOfItems();
                    //EditProperties();
                }
                if ((ArrowButton.Checked) && (highlighteditem != -1))   
                {
                    //EditProperties();
                }
                textBox3.Text = HighlightedItems.Count.ToString();
                Invalidate();    
                
                }
                catch { MessageBox.Show("Mouse Move, edit Properties:" + leftMouseDown.ToString() + " ", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }

           // Point mousehover = new Point(0, 0);
            Point currentlocationmouse = new Point(0, 0);
            currentlocationmouse = mouselocation;

            if (((leftMouseDown == true) && !Thumbtack.Checked && (HandButton.Checked)) || panInArrowMode)
            {
                if (panInArrowMode)
                    panMovement = true;
                try
                {
                        panSinceClick.X = Start.X - eLocation.X;
                        panSinceClick.Y = Start.Y - eLocation.Y;
                        totalpan.X = pan.X + panSinceClick.X;
                        totalpan.Y = pan.Y + panSinceClick.Y;
                        textBox4.Text = pan.ToString();
                        textBox5.Text = totalpan.ToString();
                        textBox6.Text = panSinceClick.ToString();
                        textBox7.Text = Start.ToString();
                        textBox8.Text = eLocation.ToString();

                    Invalidate();
                }
                catch { MessageBox.Show("Problem with moving of train", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
                
            pointerLocation.X = (int)((float) (e.Location.X / scaleFactor) + totalpan.X);
            pointerLocation.Y = (int)((float) (e.Location.Y / scaleFactor) + totalpan.Y);

            if ((leftMouseDown != true) && !panInArrowMode)
            {
                try
                {
                    if (highlighteditem != -1) lasthighlighteditem = highlighteditem;
                    highlighteditem = -1;
                    for (int i = 0; i < Indications.Count; i++)
                    {
                        MapObj item = Indications[i];
                        if (InBounds(item, pointerLocation))
                        {                                
                            highlighteditem = i;
                            if (setupmode)
                            {
                                this.Cursor = Cursors.SizeAll;
                                onleftedge = false;
                                onrightedge = false;
                                ontopedge = false;
                                onbottomedge = false;
                                oncentre = false;
                                int NSgranularity = 5; int EWgranularity = 5;
                                if (item.EndLocation.Y - item.StartLocation.Y < 13)
                                    NSgranularity = ((int)(item.EndLocation.Y - item.StartLocation.Y)) / 5;
                                if (item.EndLocation.X - item.StartLocation.X < 13)
                                    EWgranularity = ((int)(item.EndLocation.X - item.StartLocation.X)) / 3;
                                if (OnCentre(item, pointerLocation, NSgranularity)) oncentre = true;
                                if (OnLeftEdge(item, pointerLocation, EWgranularity)) onleftedge = true;
                                if (OnRightEdge(item, pointerLocation, EWgranularity)) onrightedge = true;
                                if (OnTopEdge(item, pointerLocation, NSgranularity)) ontopedge = true;
                                if (OnBottomEdge(item, pointerLocation, NSgranularity)) onbottomedge = true;
                                if (oncentre) this.Cursor = Cursors.Hand;
                                if ((onleftedge || onrightedge) && !(onbottomedge || ontopedge)) this.Cursor = Cursors.SizeWE;
                                if ((ontopedge || onbottomedge) && !(onleftedge || onrightedge)) this.Cursor = Cursors.SizeNS;
                                if (((onleftedge) && (ontopedge)) || ((onrightedge) && (onbottomedge))) this.Cursor = Cursors.SizeNWSE;
                                if (((onleftedge) && (onbottomedge)) || ((onrightedge) && (ontopedge))) this.Cursor = Cursors.SizeNESW;
                            }
                            else
                            {
                                if (item.Control != "") this.Cursor = Cursors.Arrow;
                                if (item.Control == "") this.Cursor = Cursors.Hand;
                            }
                        }
                        if (HighlightedItems.Count == 0)
                            EditProperties();
                        Invalidate();
                    }
                    if (highlighteditem == -1) this.Cursor = Cursors.Arrow;
                }
                catch { MessageBox.Show("Mouse Move, leftMouseDown:" + leftMouseDown.ToString() + " ", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            }
            catch { MessageBox.Show("Mouse Move" + leftMouseDown.ToString() + " ", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            textBox2.Text = supressNewitem.ToString();                    
        }

        private void GetListOfItems()
        {
            HighlightedItems.Clear(); InterimOffsets.Clear();
            for (int i = 0; i < Indications.Count; i++)
                if (IsItemInBounds(Indications[i]))
                {
                    HighlightedItems.Add(i);
                    InterimOffsets.Add(new Point(0,0));
                }
        }

        private void removeItemFromHighlightedList(int removenumber)
        {
            tempHighlighted.Clear();
            for(int i = 0; i < HighlightedItems.Count; i++)
                tempHighlighted.Add(HighlightedItems[i]);
            
            HighlightedItems.Clear();
            InterimOffsets.Clear();
            for (int i = 0; i < tempHighlighted.Count; i++)
                if (tempHighlighted[i] != removenumber)
                {
                    HighlightedItems.Add(tempHighlighted[i]);
                    InterimOffsets.Add(new Point(0, 0));
                }
        }

        private bool IsItemInBounds(MapObj item)
        {
            Startadj.X = (int) ((float)((Start.X) + totalpan.X));
            Startadj.Y = (int) ((float)((Start.Y) + totalpan.Y));
            Endadj.X = (int)((float)((End.X) + totalpan.X ));
            Endadj.Y = (int)((float)((End.Y) + totalpan.Y)); 
            Point StartNorm = NormaliseStart(Startadj, Endadj);
            Point EndNorm = NormaliseEnd(Startadj, Endadj);
            Point centre = new Point((item.StartLocation.X + item.EndLocation.X) / 2,
                                     (item.StartLocation.Y + item.EndLocation.Y) / 2);
            if ((StartNorm.X < centre.X) &&
                (StartNorm.Y < centre.Y) &&
                (EndNorm.X > centre.X) &&
                (EndNorm.Y > centre.Y))
                return (true);
            return (false);
        }

        private bool InBounds(MapObj item, Point location)
        {
            if ((item.EndLocation.X >= location.X) &&
                (item.EndLocation.Y >= location.Y) &&
                (item.StartLocation.X <= location.X) &&
                (item.StartLocation.Y <= location.Y))
                return (true);
            return (false);
        }

        private bool InBoundsRotated(MapObj item, Point location)
        {
            if (item.RotationAngle == 0)
            {
                if ((item.EndLocation.X >= location.X) &&
                    (item.EndLocation.Y >= location.Y) &&
                    (item.StartLocation.X <= location.X) &&
                    (item.StartLocation.Y <= location.Y))
                    return (true);
            }
            else
            {
                int width =  location.X - item.StartLocation.X;
                int height = location.Y - item.StartLocation.Y;
                reversetrm.X = (float) item.StartLocation.X + (width * (float) Math.Cos(DegtoRad(-item.RotationAngle)) + height * (float) Math.Sin(DegtoRad(-item.RotationAngle)));
                reversetrm.Y = (float) item.StartLocation.Y - width * (float) Math.Sin(DegtoRad(-item.RotationAngle)) + height * (float) Math.Cos(DegtoRad(-item.RotationAngle));
                
                if ((item.EndLocation.X >= reversetrm.X) &&
                    (item.EndLocation.Y >= reversetrm.Y) &&
                    (item.StartLocation.X <= reversetrm.X) &&
                    (item.StartLocation.Y <= reversetrm.Y))
                    return (true);
            }
            return (false);
        }

        private bool OnCentre(MapObj item, Point location, int granularity)
        {
            Point centre = new Point((item.StartLocation.X + item.EndLocation.X) / 2,
                                     (item.StartLocation.Y + item.EndLocation.Y) / 2);
            if ((centre.X - granularity < location.X) && (centre.X + granularity > location.X))
                if ((centre.Y - granularity < location.Y) && (centre.Y + granularity > location.Y))
                    return (true);
            return (false);
        }

        private bool OnLeftEdge(MapObj item, Point location, int granularity)
        {
            //if (item.RotationAngle == 0)
            {
                if ((item.StartLocation.X <= location.X) && (item.StartLocation.X + granularity > location.X))
                    if (item.StartLocation.Y < location.Y)
                        if (item.EndLocation.Y > location.Y)
                            return (true);
            }
            //else
            //{
            //    int width = location.X - item.StartLocation.X;
            //    int height = location.Y - item.StartLocation.Y;
            //    reversetrm.X = (float)item.StartLocation.X + (width * (float)Math.Cos(DegtoRad(-item.RotationAngle)) + height * (float)Math.Sin(DegtoRad(-item.RotationAngle)));
            //    reversetrm.Y = (float)item.StartLocation.Y - width * (float)Math.Sin(DegtoRad(-item.RotationAngle)) + height * (float)Math.Cos(DegtoRad(-item.RotationAngle));

            //    if ((item.StartLocation.X <= location.X) && (item.StartLocation.X + granularity > location.X))
            //        if (item.StartLocation.Y < location.Y)
            //            if (item.EndLocation.Y > location.Y)
            //                return (true);
            //}
            return (false);
        }

        private bool OnRightEdge(MapObj item, Point location, int granularity)
        {
            //if (item.RotationAngle == 0)
            {
                if ((item.EndLocation.X >= location.X) && (item.EndLocation.X - granularity < location.X))
                    if (item.StartLocation.Y < location.Y)
                        if (item.EndLocation.Y > location.Y)
                            return (true);
            }
            //else
            //{
            //    int width = location.X - item.StartLocation.X;
            //    int height = location.Y - item.StartLocation.Y;
            //    reversetrm.X = (float)item.StartLocation.X + (width * (float)Math.Cos(DegtoRad(-item.RotationAngle)) + height * (float)Math.Sin(DegtoRad(-item.RotationAngle)));
            //    reversetrm.Y = (float)item.StartLocation.Y - width * (float)Math.Sin(DegtoRad(-item.RotationAngle)) + height * (float)Math.Cos(DegtoRad(-item.RotationAngle));

            //    if ((item.StartLocation.X <= location.X) && (item.StartLocation.X + granularity > location.X))
            //        if (item.StartLocation.Y < location.Y)
            //            if (item.EndLocation.Y > location.Y)
            //                return (true);
            //}
            return (false);
        }

        private bool OnBottomEdge(MapObj item, Point location, int granularity)
        {
            //if (item.RotationAngle == 0)
            {
                if ((item.EndLocation.Y >= location.Y) && (item.EndLocation.Y - granularity < location.Y))
                    if (item.StartLocation.X < location.X)
                        if (item.EndLocation.X > location.X)
                            return (true);
            }
            //else
            //{
            //    int width = location.X - item.StartLocation.X;
            //    int height = location.Y - item.StartLocation.Y;
            //    reversetrm.X = (float)item.StartLocation.X + (width * (float)Math.Cos(DegtoRad(-item.RotationAngle)) + height * (float)Math.Sin(DegtoRad(-item.RotationAngle)));
            //    reversetrm.Y = (float)item.StartLocation.Y - width * (float)Math.Sin(DegtoRad(-item.RotationAngle)) + height * (float)Math.Cos(DegtoRad(-item.RotationAngle));

            //    if ((item.StartLocation.X <= location.X) && (item.StartLocation.X + granularity > location.X))
            //        if (item.StartLocation.Y < location.Y)
            //            if (item.EndLocation.Y > location.Y)
            //                return (true);
            //}
            return (false);
        }

        private bool OnTopEdge(MapObj item, Point location, int granularity)
        {

            if ((item.StartLocation.Y <= location.Y) && (item.StartLocation.Y + granularity > location.Y))
                if (item.StartLocation.X < location.X)
                    if (item.EndLocation.X > location.X)
                        return (true);
            return (false);
        }

        private void frmMChild_SimMap_Paint(object sender, PaintEventArgs e)
        {
            grfx = e.Graphics;
            paintMap();
        }

        private bool IsUp(string indication)
        {
            bool invert = false;
            string function = indication;
            if (indication == "") return (false);
            //else if(indication != "111RRTI1") return true;
            if (indication.Substring(0, 1) == "~")
            {
                invert = true;
                function = indication.Substring(1);
            }
            for (int i = 0; i < SimRungs.Count; i++)
                if (function == SimRungs[i].ToString())
                    return(invert != true);
            for (int i = 0; i < SimInputs.Count; i++)
                if (function == SimInputs[i].ToString())
                    return (invert != true);
            return (invert != false);
        }

        private void paintMap()
        {
            Matrix mx = new Matrix(1, 0, 0, 1, AutoScrollPosition.X, AutoScrollPosition.Y);
            grfx.Transform = mx;

            Point StartNormalised = new Point(0, 0);
            Point EndNormalised = new Point(0, 0);
            //textBox13.Text = paintCounter.ToString(); paintCounter++;

            Font scaleFont = new Font(drawFnt.Name, 12*scaleFactor, drawFnt.Style);
            Font scaleFontsized = new Font(drawFnt.Name, 12 * scaleFactor, drawFnt.Style);

            Pen GreyPen = new Pen(Color.Gray);

            Pen HighlightedAndControlPenColor = new Pen(Color.DarkTurquoise);
            HighlightedAndControlPenColor = new Pen(Color.DarkBlue, scaleFactor);
            HighlightedAndControlPenColor.DashCap = System.Drawing.Drawing2D.DashCap.Round;
            HighlightedAndControlPenColor.DashPattern = new float[] { 4.0f, 4.0f, 4.0f, 4.0f };

            SolidBrush Ind1Brush;
            SolidBrush Ind2Brush;
            SolidBrush Ind3Brush;
            SolidBrush Ind4Brush;
            Pen TextPenColor;
            SolidBrush HighBrush;
            SolidBrush DarkBrush;
            SolidBrush LowBrush;
            SolidBrush Brush;
            Pen PenColor;
            //Pen HighlightedPenColor = new Pen(Color.DarkTurquoise);
            Pen HighlightedPenColor;
            Pen HighlightedItemPenColor;
            Matrix m = new Matrix();
            //DrawStr(highlighteditem.ToString(), drawFnt, new SolidBrush(Color.Pink), 500, 500);            

            if (highlighteditem == -1)
            {
                SolidBrush GreyBrush = new SolidBrush(Color.Gray);
                Startadj.X = (int)((float)((Start.X) + totalpan.X));
                Startadj.Y = (int)((float)((Start.Y) + totalpan.Y));
                Endadj.X = (int)((float)((End.X) + totalpan.X));
                Endadj.Y = (int)((float)((End.Y) + totalpan.Y));
                StartNormalised = NormaliseStart(Startadj, Endadj);
                EndNormalised = NormaliseEnd(Startadj, Endadj);
                if (leftMouseDown)
                {
                    //Matrix m = new Matrix();
                    Rectangle r = new Rectangle(StartNormalised.X, StartNormalised.Y, EndNormalised.X - StartNormalised.X, EndNormalised.Y - StartNormalised.Y);
                    m.RotateAt(0, new PointF(r.Left + (r.Width / 2), r.Top + (r.Height / 2)));
                    grfx.Transform = m;
                    if (LabelButton.Checked)
                        DrawRect(GreyPen, StartNormalised, EndNormalised.X - StartNormalised.X, EndNormalised.Y - StartNormalised.Y);
                    if (RectangleButton.Checked)
                        DrawRect(GreyPen, StartNormalised, EndNormalised.X - StartNormalised.X, EndNormalised.Y - StartNormalised.Y);
                    if (CircleButton.Checked)
                        DrawEll(GreyPen, StartNormalised, EndNormalised.X - StartNormalised.X, EndNormalised.Y - StartNormalised.Y);
                }
            }
            if (rubberband)
            {
                Pen RubberbandPen = new Pen(Color.Gray, scaleFactor);
                RubberbandPen.DashCap = System.Drawing.Drawing2D.DashCap.Round;
                RubberbandPen.DashPattern = new float[] { 2.0F, 2.0f, 2.0f, 2.0f };
                DrawRect(RubberbandPen, StartNormalised, EndNormalised.X - StartNormalised.X, EndNormalised.Y - StartNormalised.Y);
            }                
            {
                 //scaleFactor * (startx - totalpan.X)
                textBox15.Text = ClientSize.Width.ToString();
                textBox16.Text = ClientSize.Height.ToString();
            }
            for (int i = 0; i < Indications.Count; i++)
            {
                MapObj item = Indications[i];

                if (item.Textsize != 12)
                {
                    if(scaleFactor * item.Textsize > 0) 
                        scaleFontsized = new Font(drawFnt.Name, scaleFactor * item.Textsize, drawFnt.Style);
                    else scaleFontsized = scaleFont;
                }
                else scaleFontsized = scaleFont;
                if ((scaleFactor * (item.StartLocation.X - totalpan.X) < ClientSize.Width) &&
                    (scaleFactor * (item.StartLocation.Y - totalpan.Y) < ClientSize.Height)&&
                    (scaleFactor * (item.EndLocation.X - totalpan.X) > 0) &&
                    (scaleFactor * (item.EndLocation.Y - totalpan.Y) > 0))
                {
                    PenColor = new Pen(item.HighColour);
                    HighlightedPenColor = new Pen(Color.DarkTurquoise, (int) scaleFactor);
                    HighlightedPenColor.DashCap = System.Drawing.Drawing2D.DashCap.Round;
                    HighlightedPenColor.DashPattern = new float[] { 2.0f, 2.0f, 2.0f, 2.0f };
                    
                    HighlightedItemPenColor = new Pen(Color.DarkBlue, (int) scaleFactor);
                    HighlightedItemPenColor.DashCap = System.Drawing.Drawing2D.DashCap.Round;
                    HighlightedItemPenColor.DashPattern = new float[] { 3.0f, 3.0f, 3.0f, 3.0f };

                    TextPenColor = new Pen(item.HighColour);
                    HighBrush = new SolidBrush(item.HighColour);
                    DarkBrush = new SolidBrush(Color.Black);
                    LowBrush = new SolidBrush(item.LowColour);
                    Brush = new SolidBrush(Color.SeaGreen);
                    StartNormalised = NormaliseStart(Start, End);
                    EndNormalised = NormaliseEnd(Start, End);
                    bool Ishighlighted = false;
                    bool Ishighlighteditem = false;
                    bool IsControlAndHighlighted = false;
                    string imagefilename = "";
                    Rectangle r = new Rectangle(StartNormalised.X, StartNormalised.Y, EndNormalised.X - StartNormalised.X, EndNormalised.Y - StartNormalised.Y);

                    Ind1Brush = new SolidBrush(item.IndColour1);
                    Ind2Brush = new SolidBrush(item.IndColour2);
                    Ind3Brush = new SolidBrush(item.IndColour3);
                    Ind4Brush = new SolidBrush(item.IndColour4);
                    Brush = LowBrush; TextPenColor = new Pen(item.LowColour);
                    if (!setupmode && IsUp(item.Indication1)) { Brush = Ind1Brush; TextPenColor = new Pen(item.IndColour1); imagefilename = item.Indication1; }
                    if (!setupmode && IsUp(item.Indication2)) { Brush = Ind2Brush; TextPenColor = new Pen(item.IndColour2); imagefilename = item.Indication2; }
                    if (!setupmode && IsUp(item.Indication3)) { Brush = Ind3Brush; TextPenColor = new Pen(item.IndColour3); imagefilename = item.Indication3; }
                    if (!setupmode && IsUp(item.Indication4)) { Brush = Ind4Brush; TextPenColor = new Pen(item.IndColour4); imagefilename = item.Indication4; }

                    if (setupmode)
                        TextPenColor = new Pen(item.IndColour1);
                    for (int j = 0; j < HighlightedItems.Count; j++)
                        if (HighlightedItems[j] == i)
                        {
                            Ishighlighted = true;
                            HighBrush = new SolidBrush(Color.DarkTurquoise);
                        }
                    if (highlighteditem == i)
                    {
                        Ishighlighteditem = true;
                        if (Indications[i].Control != "")
                            IsControlAndHighlighted = true;
                        HighBrush = new SolidBrush(Color.LightSkyBlue);
                        TextPenColor = new Pen(Color.LightSkyBlue, 2);
                        TextPenColor.DashCap = System.Drawing.Drawing2D.DashCap.Round;
                        TextPenColor.DashPattern = new float[] { 2.0f, 2.0f, 2.0f, 2.0f };
                    }
                    if (highlighteditem > 0)
                        if ((!setupmode) && (Indications[highlighteditem].Control == Indications[i].Control) && (Indications[i].Control != ""))
                        {
                            IsControlAndHighlighted = true;
                            int newr = Brush.Color.R + 0;
                            int newg = Brush.Color.G + 30;
                            int newb = Brush.Color.B + 0;
                            if (newr > 255) newr = 255; if (newg > 255) newg = 255; if (newb > 255) newb = 255;
                            HighBrush = new SolidBrush(Color.FromArgb(newr, newg, newb));
                        }
                    if (((Brush == LowBrush) && item.Transparent) && (!setupmode))
                    {
                    }
                    else
                    {
                        if (item.Shape == "Rectangle")
                        {
                            float width = item.EndLocation.X - item.StartLocation.X;
                            float height = item.EndLocation.Y - item.StartLocation.Y;
                            PointF point1 = new PointF(item.StartLocation.X, item.StartLocation.Y);
                            PointF point2 = new PointF((float)item.StartLocation.X + width * (float)Math.Cos(DegtoRad(item.RotationAngle)),
                                                       (float)item.StartLocation.Y - width * (float)Math.Sin(DegtoRad(item.RotationAngle)));
                            PointF point3 = new PointF((float)item.StartLocation.X + width * (float)Math.Cos(DegtoRad(item.RotationAngle)) + height * (float)Math.Sin(DegtoRad(item.RotationAngle)),
                                                       (float)item.StartLocation.Y - width * (float)Math.Sin(DegtoRad(item.RotationAngle)) + height * (float)Math.Cos(DegtoRad(item.RotationAngle)));
                            PointF point4 = new PointF((float)item.StartLocation.X + height * (float)Math.Sin(DegtoRad(item.RotationAngle)),
                                                       (float)item.StartLocation.Y + height * (float)Math.Cos(DegtoRad(item.RotationAngle)));
                            PointF[] curvePoints = { point1, point2, point3, point4 };

                            if (setupmode)
                            {
                                FillPoly(Brush, curvePoints);
                                if (Ishighlighted)
                                    if (Ishighlighteditem)
                                        DrawRect(HighlightedItemPenColor, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                                    else
                                        DrawRect(HighlightedPenColor, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                                else
                                    DrawRect(GreyPen, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                            }
                            else
                            {
                                if (IsControlAndHighlighted)
                                {
                                    FillPoly(HighBrush, curvePoints);
                                }

                                else
                                    FillPoly(Brush, curvePoints);
                            }
                        }
                        if ((item.Shape == "Image") && (item.Text != ""))
                        {
                            float width = item.EndLocation.X - item.StartLocation.X;
                            float height = item.EndLocation.Y - item.StartLocation.Y;


                            float rot = item.RotationAngle;// + paintCounter++;// DateTime.Now.Ticks;

                           /* Point[] destinationPoints = {
                                        new Point(item.StartLocation.X, item.StartLocation.Y),   // destination for upper-left point of original                                             
                                        new Point(item.StartLocation.X + (int) (width * Math.Cos(DegtoRad(item.RotationAngle))),
                                                  item.StartLocation.Y - (int) (width * Math.Sin(DegtoRad(item.RotationAngle)))),  // destination for upper-right point of original 
                                        //new Point(item.StartLocation.X + (int) (width * Math.Cos(DegtoRad(item.RotationAngle))) + (int) (height * Math.Sin(DegtoRad(item.RotationAngle))),
                                          //        item.StartLocation.Y - (int) (width * Math.Sin(DegtoRad(item.RotationAngle))) + (int) (height * Math.Cos(DegtoRad(item.RotationAngle)))),
                                        new Point(item.StartLocation.X + (int) (height * Math.Sin(DegtoRad(item.RotationAngle))),
                                                  item.StartLocation.Y + (int) (height * Math.Cos(DegtoRad(item.RotationAngle))))*/




                            Point[] destinationPoints = {
                                        new Point(item.StartLocation.X, item.StartLocation.Y),   // destination for upper-left point of original                                             
                                        new Point(item.StartLocation.X + (int) (width * Math.Cos(DegtoRad(rot))),
                                                  item.StartLocation.Y - (int) (width * Math.Sin(DegtoRad(rot)))),  // destination for upper-right point of original 
                                        new Point(item.StartLocation.X + (int) (height * Math.Sin(DegtoRad(rot))),
                                                  item.StartLocation.Y + (int) (height * Math.Cos(DegtoRad(rot))))


                                                    };  // destination for lower-left point of original
                            try
                            {
                                Image image = new Bitmap(ImageDirectoryPath + item.Text);//"grass_tile2.jpg");

                                // Draw the image unaltered with its upper-left corner at (0, 0).
                                // grfx.DrawImage(image, 0, 0);

                                // Draw the image mapped to the parallelogram.
                                //grfx.DrawImage(image, destinationPoints);

                                //float width = item.EndLocation.X - item.StartLocation.X;
                                //float height = item.EndLocation.Y - item.StartLocation.Y;
                                /*PointF point1 = new PointF(item.StartLocation.X, item.StartLocation.Y);
                                PointF point2 = new PointF((float)item.StartLocation.X + width * (float)Math.Cos(DegtoRad(item.RotationAngle)),
                                                           (float)item.StartLocation.Y - width * (float)Math.Sin(DegtoRad(item.RotationAngle)));
                                PointF point3 = new PointF((float)item.StartLocation.X + width * (float)Math.Cos(DegtoRad(item.RotationAngle)) + height * (float)Math.Sin(DegtoRad(item.RotationAngle)),
                                                           (float)item.StartLocation.Y - width * (float)Math.Sin(DegtoRad(item.RotationAngle)) + height * (float)Math.Cos(DegtoRad(item.RotationAngle)));
                                PointF point4 = new PointF((float)item.StartLocation.X + height * (float)Math.Sin(DegtoRad(item.RotationAngle)),
                                                           (float)item.StartLocation.Y + height * (float)Math.Cos(DegtoRad(item.RotationAngle)));
                                PointF[] curvePoints = { point1, point2, point3, point4 };*/
                                if (setupmode)
                                {
                                    PointF point1 = new PointF(item.StartLocation.X, item.StartLocation.Y);
                                    /*PointF point2 = new PointF((float)item.StartLocation.X + width * (float)Math.Cos(DegtoRad(item.RotationAngle)),
                                                           (float)item.StartLocation.Y - width * (float)Math.Sin(DegtoRad(item.RotationAngle)));
                                    PointF point3 = new PointF((float)item.StartLocation.X + width * (float)Math.Cos(DegtoRad(item.RotationAngle)) + height * (float)Math.Sin(DegtoRad(item.RotationAngle)),
                                                           (float)item.StartLocation.Y - width * (float)Math.Sin(DegtoRad(item.RotationAngle)) + height * (float)Math.Cos(DegtoRad(item.RotationAngle)));
                                    PointF point4 = new PointF((float)item.StartLocation.X + height * (float)Math.Sin(DegtoRad(item.RotationAngle)),
                                                           (float)item.StartLocation.Y + height * (float)Math.Cos(DegtoRad(item.RotationAngle)));*/
                                    rot = item.RotationAngle;// + 90;// + paintCounter++;// DateTime.Now.Ticks;
                                    PointF point2 = new PointF((float)item.StartLocation.X + width * (float)Math.Cos(DegtoRad(rot)),
                                                           (float)item.StartLocation.Y - width * (float)Math.Sin(DegtoRad(rot)));
                                    PointF point3 = new PointF((float)item.StartLocation.X + width * (float)Math.Cos(DegtoRad(rot)) + height * (float)Math.Sin(DegtoRad(rot)),
                                                           (float)item.StartLocation.Y - width * (float)Math.Sin(DegtoRad(rot)) + height * (float)Math.Cos(DegtoRad(rot)));
                                    PointF point4 = new PointF((float)item.StartLocation.X + height * (float)Math.Sin(DegtoRad(rot)),
                                                           (float)item.StartLocation.Y + height * (float)Math.Cos(DegtoRad(rot)));
                                    PointF[] curvePoints = { point1, point2, point3, point4 };
                                    FillPoly(Brush, curvePoints);
                                    if (Ishighlighted)
                                        if (Ishighlighteditem)
                                            DrawRect(HighlightedItemPenColor, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                                        else
                                            DrawRect(HighlightedPenColor, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                                    else
                                        DrawRect(GreyPen, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                                }
                                /*else
                                {
                                    if (IsControlAndHighlighted)                                
                                        FillPoly(HighBrush, curvePoints);                                
                                    else
                                        FillPoly(Brush, curvePoints);
                                }*/
                                DrawImage(image, destinationPoints);
                                //grfx.DrawImage(image, destinationPoints);
                            }
                            catch
                            {
                                DrawStr("file " + item.Text + " not found", scaleFontsized, HighBrush, item.StartLocation.X, item.StartLocation.Y);
                                //MessageBox.Show("Problem reading image: " + item.Indication1, "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        if (item.Shape == "Label")
                        {
                            m.RotateAt(-item.RotationAngle, new PointF(scaleFactor * ((item.StartLocation.X) - totalpan.X),
                                scaleFactor * ((item.StartLocation.Y) - totalpan.Y)));                            
                            grfx.Transform = m;
                            if (setupmode)
                            {
                                DrawStr(item.Text, scaleFontsized, HighBrush, item.StartLocation.X, item.StartLocation.Y);
                                if (Ishighlighted)
                                    if (Ishighlighteditem)
                                        DrawRect(HighlightedItemPenColor, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                                    else
                                        DrawRect(HighlightedPenColor, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                                else
                                    DrawRect(GreyPen, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                            }
                            else
                            {
                                if (IsControlAndHighlighted)
                                {
                                    DrawRect(HighlightedAndControlPenColor, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                                    DrawStr(item.Text, scaleFontsized, HighBrush, item.StartLocation.X, item.StartLocation.Y);
                                }
                                else
                                    DrawStr(item.Text, scaleFontsized, Brush, item.StartLocation.X, item.StartLocation.Y);
                            }
                            m.RotateAt(item.RotationAngle, new PointF(scaleFactor * ((item.StartLocation.X) - totalpan.X),
                               scaleFactor * ((item.StartLocation.Y) - totalpan.Y)));
                            //m.RotateAt(-item.RotationAngle, new PointF(item.StartLocation.X + width / 2, item.StartLocation.Y + height / 2));
                            grfx.Transform = m;
                        }
                        if (item.Shape == "Timer")
                        {
                            m.RotateAt(-item.RotationAngle, new PointF(scaleFactor * ((item.StartLocation.X) - totalpan.X),
                                scaleFactor * ((item.StartLocation.Y) - totalpan.Y)));
                            grfx.Transform = m;
                            string label = gettime(item.Text);
                            if (setupmode)
                            {
                                DrawStr(item.Text, scaleFontsized, HighBrush, item.StartLocation.X, item.StartLocation.Y);
                                DrawRect(PenColor, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                            }
                            else
                            {
                                //DrawRect(PenColor, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                                DrawStr(label, scaleFontsized, HighBrush, item.StartLocation.X, item.StartLocation.Y);
                            }
                            m.RotateAt(item.RotationAngle, new PointF(scaleFactor * ((item.StartLocation.X) - totalpan.X),
                                scaleFactor * ((item.StartLocation.Y) - totalpan.Y)));
                            grfx.Transform = m;
                        }
                        if (item.Shape == "Circle")
                        {
                            if (setupmode)
                            {

                                FillEll(Brush, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                                if (Ishighlighted)
                                    if (IsControlAndHighlighted)
                                        DrawEll(HighlightedItemPenColor, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                                    else
                                        DrawEll(HighlightedPenColor, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                                else
                                {

                                    TextPenColor = new Pen(item.IndColour1);
                                    DrawEll(TextPenColor, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                                    TextPenColor = new Pen(item.IndColour2);
                                    Point ind2 = new Point(item.StartLocation.X + 1, item.StartLocation.Y + 1);
                                    DrawEll(TextPenColor, ind2, -2 + item.EndLocation.X - item.StartLocation.X, -2 + item.EndLocation.Y - item.StartLocation.Y);
                                    TextPenColor = new Pen(item.IndColour3);
                                    Point ind3 = new Point(item.StartLocation.X + 2, item.StartLocation.Y + 2);
                                    DrawEll(TextPenColor, ind3, -4 + item.EndLocation.X - item.StartLocation.X, -4 + item.EndLocation.Y - item.StartLocation.Y);
                                    TextPenColor = new Pen(item.IndColour4);
                                    Point ind4 = new Point(item.StartLocation.X + 3, item.StartLocation.Y + 3);
                                    DrawEll(TextPenColor, ind4, -6 + item.EndLocation.X - item.StartLocation.X, -6 + item.EndLocation.Y - item.StartLocation.Y);
                                }
                            }
                            else
                            {
                                FillEll(Brush, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                                if (IsControlAndHighlighted)
                                    DrawEll(HighlightedPenColor, item.StartLocation, item.EndLocation.X - item.StartLocation.X, item.EndLocation.Y - item.StartLocation.Y);
                            }
                        }
                    }
                }
            }

        }
               

        private string gettime(string timercandidate)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string rowvalue = (string) dataGridView1.Rows[i].Cells[0].Value;
                if (timercandidate == rowvalue)
                    if ((dataGridView1.Rows[i].Cells[1].Value != "Low") && (dataGridView1.Rows[i].Cells[1].Value != "High"))
                        return ((string) dataGridView1.Rows[i].Cells[1].Value);
            }
            return ("");
        }

        private void DrawRect(Pen pen, Point TopLeft, int width, int height)
        {
            Matrix mx = new Matrix(1, 0, 0, 1, AutoScrollPosition.X, AutoScrollPosition.Y);
            grfx.Transform = mx;
            grfx.DrawRectangle(pen, scaleFactor * (TopLeft.X - totalpan.X), scaleFactor * (TopLeft.Y - totalpan.Y), scaleFactor * width, scaleFactor * height);
        }

        private void FillPoly(Brush brush, PointF[] curvepoints)
        {
            PointF[] curvetrans = new PointF[curvepoints.Length];// = { point1, point2, point3, point4 };
            for (int i = 0; i < curvepoints.Length; i++)
            {
                curvetrans[i].X = scaleFactor * (curvepoints[i].X - totalpan.X);
                curvetrans[i].Y = scaleFactor * (curvepoints[i].Y - totalpan.Y);
            }            
            grfx.FillPolygon(brush, curvetrans);
        }

        private void DrawImage(Image Imagename, Point[] curvepoints)
        {
            Point[] curvetrans = new Point[curvepoints.Length];// = { point1, point2, point3, point4 };
            for (int i = 0; i < curvepoints.Length; i++)
            {
                curvetrans[i].X = (int) (scaleFactor * (curvepoints[i].X - totalpan.X));
                curvetrans[i].Y = (int) (scaleFactor * (curvepoints[i].Y - totalpan.Y));
            }
            //grfx.DrawImage(Imagename, curvepoints);
            grfx.DrawImage(Imagename, curvetrans);
            //grfx.FillPolygon(brush, curvetrans);
        }

        private void FillEll(Brush brush, Point TopLeft, int width, int height)                            
        {
            grfx.FillEllipse(brush, scaleFactor * (TopLeft.X - totalpan.X), scaleFactor * (TopLeft.Y - totalpan.Y), scaleFactor * width, scaleFactor * height);            
        }

        private void DrawEll(Pen pen, Point TopLeft, int width, int height)
        {
            grfx.DrawEllipse(pen, scaleFactor * (TopLeft.X - totalpan.X), scaleFactor * (TopLeft.Y - totalpan.Y), scaleFactor * width, scaleFactor * height);
        }
                   
        private void DrawStr(string text, Font font, SolidBrush brush, int startx, int starty)
        {
            grfx.DrawString(text, font, brush, scaleFactor * (startx - totalpan.X), scaleFactor * (starty - totalpan.Y));
        }
        
        private float DegtoRad(float deg)
        {
            return (deg * (2 * (float) pi) /360 );
        }


        private Point NormaliseEnd(Point Start, Point End)
        {
            Point EndNormalised = new Point(0, 0);
            if (Start.X > End.X) EndNormalised.X = Start.X;
            else EndNormalised.X = End.X;
            if (Start.Y > End.Y) EndNormalised.Y = Start.Y; 
            else EndNormalised.Y = End.Y; 
            return (EndNormalised);
        }

        private Point NormaliseStart(Point Start, Point End)
        {
            Point StartNormalised = new Point(0, 0);
            if (Start.X > End.X) StartNormalised.X = End.X;
            else StartNormalised.X = Start.X;
            if (Start.Y > End.Y) StartNormalised.Y = End.Y;
            else StartNormalised.Y = Start.Y;
            return (StartNormalised);
        }



        private void frmMChild_SimMap_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            PushIndications();
        }

        private TextBox textBox1;
        private TextBox textBox2;

        private void frmMChild_SimMap_Click(object sender, EventArgs e)
        {         
        }

        private TextBox Indication1;
        private Label label1;
        private Label label21;
        private Label label2;
        private TextBox XCoord;
        private Label label4;
        private TextBox Height;
        private Label label5;
        private TextBox Width;
        private Label label6;
        private TextBox YCoord;
        private Button button4;

        private void button4_Click(object sender, EventArgs e)
        {
            CommitChange();
        }

        private void CommitChange()
        {

            if (!supressCommit)
            {
                try
                {
                    MapObj item = new MapObj();
                    Point End = new Point(0, 0);
                    int textsize = 100;
                    Point Start = new Point(0, 0);
                    Pen AquaPen = new Pen(Color.Aqua);
                    for (int i = 0; i < HighlightedItems.Count; i++)
                    {
                        item = Indications[HighlightedItems[i]];
                        End = item.EndLocation;
                        Start = item.StartLocation;
                        int width = item.EndLocation.X - item.StartLocation.X;
                        int height = item.EndLocation.Y - item.StartLocation.Y;
                        if (Textsizetext.Text.ToString() != "?")
                            textsize = parseint(Textsizetext.Text.ToString(), 100);
                        if (XCoord.Text.ToString() != "?")
                            Start.X = parseint(XCoord.Text.ToString(), 0);
                        if (YCoord.Text.ToString() != "?")
                            Start.Y = parseint(YCoord.Text.ToString(), 0);
                        if (Width.Text.ToString() != "?")
                        { 
                            int widthtext = parseint(Width.Text.ToString(), 7);

                            if (widthtext < 0) widthtext -= widthtext;
                            End.X = Start.X + widthtext;
                        }
                        else
                            End.X = Start.X + width;
                        if (Height.Text.ToString() != "?")
                        {
                            int heighttext = parseint(Height.Text.ToString(), 7);
                            if (heighttext < 0) heighttext -= heighttext;
                            End.Y = Start.Y + heighttext;
                        }
                        else
                            End.Y = Start.Y + height;
                        item.Textsize = textsize;
                        item.EndLocation = End;
                        item.StartLocation = Start;
                        item.Transparent = TransparentCheckbox.Checked;
                        item.IsTrack = IsTrackCheckbox.Checked;
                        if (AngleBox.Text.ToString() != "?")
                            item.RotationAngle = parseint(AngleBox.Text.ToString(), 0);
                        if (Indication1.Text.ToString() != "?") item.Name = Indication1.Text;
                        if (LabelText.Text.ToString() != "?") item.Text = LabelText.Text;
                        if (ShapecomboBox1.Text.ToString() != "?") item.Shape = ShapecomboBox1.Text.ToString();

                        if (Indication1.Text.ToString() != "?") item.Indication1 = Indication1.Text.ToString();
                        if (Indication2.Text.ToString() != "?") item.Indication2 = Indication2.Text.ToString();
                        if (Indication3.Text.ToString() != "?") item.Indication3 = Indication3.Text.ToString();
                        if (Indication4.Text.ToString() != "?") item.Indication4 = Indication4.Text.ToString();

                        if (LabelText.Text.ToString() != "?") item.Text = LabelText.Text;
                        if (item.HighColour != Color.Silver) item.HighColour = IndicationColour1.BackColor;
                        //if (item.LowColour != Color.Silver) 
                        if (LowColourString.BackColor != Color.Silver) 
                            item.LowColour = LowColourString.BackColor;
                        if (IndicationColour1.BackColor != Color.Silver) item.IndColour1 = IndicationColour1.BackColor;
                        if (IndicationColour2.BackColor != Color.Silver) item.IndColour2 = IndicationColour2.BackColor;
                        if (IndicationColour3.BackColor != Color.Silver) item.IndColour3 = IndicationColour3.BackColor;
                        if (IndicationColour4.BackColor != Color.Silver) item.IndColour4 = IndicationColour4.BackColor;
                        if (ControlName.Text != "?") item.Control = ControlName.Text;
                        if (TransparentCheckbox.BackColor != Color.Gray) item.Transparent = TransparentCheckbox.Checked;
                        if (IsTrackCheckbox.BackColor != Color.Gray) item.IsTrack = IsTrackCheckbox.Checked;     
                        Indications[HighlightedItems[i]] = item;
                        PushIndications();
                    }
                    if (BitNumber.Text.ToString() != "?")
                    {
                        textsize = Int32.Parse(Textsizetext.Text.ToString());
                        Start.X = Int32.Parse(XCoord.Text.ToString());
                        Start.Y = Int32.Parse(YCoord.Text.ToString());
                        End.X = Int32.Parse(XCoord.Text.ToString()) + Int32.Parse(Width.Text.ToString());
                        End.Y = Int32.Parse(YCoord.Text.ToString()) + Int32.Parse(Height.Text.ToString());
                        item.EndLocation = End;
                        item.StartLocation = Start;
                        item.RotationAngle = Int32.Parse(AngleBox.Text.ToString());
                        item.Name = Indication1.Text;
                        item.Text = LabelText.Text;
                        item.Shape = ShapecomboBox1.Text.ToString();
                        item.HighColour = IndicationColour1.BackColor;
                        item.LowColour = LowColourString.BackColor;
                        item.Transparent = TransparentCheckbox.Checked;
                        item.Indication1 = Indication1.Text.ToString();
                        item.Indication2 = Indication2.Text.ToString();
                        item.Indication3 = Indication3.Text.ToString();
                        item.Indication4 = Indication4.Text.ToString();                        
                        item.IndColour1 = IndicationColour1.BackColor;
                        item.IndColour2 = IndicationColour2.BackColor;
                        item.IndColour3 = IndicationColour3.BackColor;
                        item.IndColour4 = IndicationColour4.BackColor;
                        item.Control = ControlName.Text;
                        int bitNumber = Int32.Parse(BitNumber.Text.ToString());
                        Indications[bitNumber] = item;
                        PushIndications();
                    }
                    Invalidate();
                }
                catch
                {
                    MessageBox.Show("Problem with Properties Dialogs", "Logic Navigator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private int parseint(string text, int sanenumber)
        {
            try
            {
                return (Int32.Parse(text));
            }
            catch
            {
                return (sanenumber);
            }
            /*if ((text == "-") || (text == ""))
                return (sanenumber);
            else
                return (Int32.Parse(text));*/
        }

        private Label label7;
        private TextBox BitNumber;

        private void button5_Click(object sender, EventArgs e)
        {
            MapObj item = new MapObj();
            int textsize = 100;
            Point End = new Point(0, 0);
            Point Start = new Point(0, 0);
            Pen AquaPen = new Pen(Color.Aqua);
            textsize = Int32.Parse(Textsizetext.Text.ToString()) + 50;
            Start.X = Int32.Parse(XCoord.Text.ToString()) + 50;
            Start.Y = Int32.Parse(YCoord.Text.ToString()) + 50;
            End.X = Int32.Parse(XCoord.Text.ToString()) + Int32.Parse(Width.Text.ToString()) + 50;
            End.Y = Int32.Parse(YCoord.Text.ToString()) + Int32.Parse(Height.Text.ToString()) + 50;
            item.EndLocation = End;
            item.StartLocation = Start;
            item.Name = Indication1.Text;
            item.Shape = ShapecomboBox1.Text.ToString();
            item.HighColour = IndicationColour1.BackColor;
            item.LowColour = LowColourString.BackColor;
            int bitNumber = Int32.Parse(BitNumber.Text.ToString());
            Indications.Add(item);
            PushIndications();
            Invalidate();
        }

        private ToolStripButton LabelButton;

        private void SignalButton_Click(object sender, EventArgs e)
        {
            HandButton.Checked = false;
            RectangleButton.Checked = false;
            ArrowButton.Checked = false;
            CircleButton.Checked = true;
            LabelButton.Checked = false;
        }
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            HandButton.Checked = false;
            RectangleButton.Checked = true;
            ArrowButton.Checked = false;
            CircleButton.Checked = false;
            LabelButton.Checked = false;
        }

        private void PointButton_Click(object sender, EventArgs e)
        {
            HandButton.Checked = false;
            RectangleButton.Checked = false;
            ArrowButton.Checked = false;
            CircleButton.Checked = false;
            LabelButton.Checked = false;
        }

        private void LabelButton_Click(object sender, EventArgs e)
        {
            HandButton.Checked = false;
            RectangleButton.Checked = false;
            ArrowButton.Checked = false;
            CircleButton.Checked = false;
            LabelButton.Checked = true;
        }

        private void ArrowButton_Click(object sender, EventArgs e)
        {
            HandButton.Checked = false;
            RectangleButton.Checked = false;
            ArrowButton.Checked = true;
            CircleButton.Checked = false;
            LabelButton.Checked = false;
            //setupmode = true;
        }
        private ColorDialog colorDialog1;

        private void button6_Click(object sender, EventArgs e)
        {
            IndicationColour1.BackColor = Choose_Colour();            
        }

        private TextBox IndicationColour1;
        private TextBox LowColourString;
        private Label label9;

        private void button7_Click(object sender, EventArgs e)
        {
            LowColourString.BackColor = Choose_Colour();  
        }

        private Color Choose_Colour()
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                return(colorDialog1.Color);
            return (Color.Black);
        }

        private ContextMenuStrip contextMenuStrip1;
        private IContainer components;

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private ToolStripMenuItem propertiesToolStripMenuItem;

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Text == "Hide Properties")
            {
                Properties.Text = "Show Properties";
                ShowHideToolbar("Hide");

            }
            else
            {
                Properties.Text = "Hide Properties";
                ShowHideToolbar("Show");
            }


            //EditProperties();
        }

        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem sendToBackToolStripMenuItem;
        private ToolStripMenuItem bringToFrontToolStripMenuItem;

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteSelectedItems();
        }

        public void deleteSelectedItems()
        {
            HighlightedItemsDelete.Clear();
            if (HighlightedItems.Count > 0)
            {
                int highest = 0;
                int oldhighest = -1;
                for (int i = 0; i < HighlightedItems.Count; i++)
                {
                    highest = 0;
                    for (int j = 0; j < HighlightedItems.Count; j++)
                    {
                        if(!inList(HighlightedItems[j],HighlightedItemsDelete))
                            if (HighlightedItems[j] > highest)
                            {
                                if (highest != oldhighest)
                                    highest = HighlightedItems[j];
                            }
                    }
                    oldhighest = highest;
                    HighlightedItemsDelete.Add(highest);

                }
                for (int i = 0; i < HighlightedItemsDelete.Count; i++)
                    Indications.RemoveAt(HighlightedItemsDelete[i]);
            }
            else
                if (highlighteditem != -1)
                    Indications.RemoveAt(highlighteditem);
            Invalidate();
        }

        private bool inList(int number, List<int> itemslist)
        {
            for (int j = 0; j < itemslist.Count; j++)
            {
                if (itemslist[j] == number)
                    return (true);
            }
            return (false);
        }

        private ToolStripMenuItem pasteToolStripMenuItem;

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void Copy()
        {
            HighlightedItemsCopied.Clear();
            if (HighlightedItems.Count > 0)
            {
                for (int i = 0; i < HighlightedItems.Count; i++)
                {
                    int item = HighlightedItems[i];
                    HighlightedItemsCopied.Add(item);
                }
            }
            else
            {
                HighlightedItemsCopied.Clear();
                if (highlighteditem != -1)
                    Copyitem = highlighteditem;
                else Copyitem = -1;
            }
        }
                   
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste(0);
        }

        private void Cut()
        {
            cutting = true;
            Copy();
        }


        private void Paste(int typeofpaste)
        {
            try
            {                
                if (HighlightedItemsCopied.Count != 0)
                {
                    int startnumber = FindLeftMostObject(HighlightedItemsCopied);
                    MapObj item = Indications[HighlightedItemsCopied[startnumber]];
                    Point delta = new Point(0, 0);
                    if (typeofpaste == 0)
                    {
                        delta.X = mouselocation.X - item.StartLocation.X;
                        delta.Y = mouselocation.Y - item.StartLocation.Y;
                    }
                    else
                    {
                        delta.X = 100;
                        delta.Y = 100;
                    }
                    Startadj.X = (int)((float)((delta.X) + totalpan.X));
                    Startadj.Y = (int)((float)((delta.Y) + totalpan.Y));
                    if (cutting) deleteSelectedItems();
                    HighlightedItems.Clear();
                    for (int i = 0; i < HighlightedItemsCopied.Count; i++)
                    {
                        item = Indications[HighlightedItemsCopied[i]];
                        int width = item.EndLocation.X - item.StartLocation.X;
                        int height = item.EndLocation.Y - item.StartLocation.Y;
                        item.StartLocation.X += Startadj.X;
                        item.StartLocation.Y += Startadj.Y;
                        item.EndLocation.X = item.StartLocation.X + width;
                        item.EndLocation.Y = item.StartLocation.Y + height;
                        Indications.Add(item);
                        HighlightedItems.Add(Indications.Count - 1);
                    }
                    PushIndications();
                }
                else
                {
                    if (Copyitem != -1)
                    {
                        MapObj item = Indications[Copyitem];
                        if (cutting) deleteSelectedItems();
                        Point delta = new Point(0, 0);
                        if (typeofpaste == 0)
                        {
                            delta.X = mouselocation.X - item.StartLocation.X;
                            delta.Y = mouselocation.Y - item.StartLocation.Y;
                        }
                        else
                        {
                            delta.X = 100;
                            delta.Y = 100;
                        }
                        Startadj.X = (int)((float)((delta.X) + totalpan.X));
                        Startadj.Y = (int)((float)((delta.Y) + totalpan.Y));    
                        int width = item.EndLocation.X - item.StartLocation.X;
                        int height = item.EndLocation.Y - item.StartLocation.Y;
                        item.StartLocation.X += Startadj.X;//delta.X;
                        item.StartLocation.Y += Startadj.Y;// delta.Y;
                        item.EndLocation.X = item.StartLocation.X + width;
                        item.EndLocation.Y = item.StartLocation.Y + height;
                        Indications.Add(item);
                        PushIndications();
                    }
                }
                cutting = false;
            }
            catch
            {
                MessageBox.Show("Error pasting", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private int FindLeftMostObject(List<int> list)
        {
            int leftmost = 100000;
            int itemnumber = -1;
            for (int i = 0; i < list.Count; i++)
            {
                MapObj item = Indications[HighlightedItemsCopied[i]];
                if (item.StartLocation.X < leftmost) 
                {
                    leftmost = item.StartLocation.X;
                    itemnumber = i;
                }
            }
            if(leftmost == 100000) return(-1);
            return (itemnumber);
        }

        private int FindRightMostObject()
        {
            int rightmost = 0;
            int itemnumber = -1;
            for (int i = 0; i < Indications.Count; i++)
            {
                MapObj item = Indications[i];
                if (item.StartLocation.X > rightmost)
                {
                    rightmost = item.StartLocation.X;
                    itemnumber = i;
                }
            }
            if (rightmost == 0) return (-1);
            return (rightmost);
        }

        private int FindBottomMostObject()
        {
            int bottommost = 0;
            int itemnumber = -1;
            for (int i = 0; i < Indications.Count; i++)
            {
                MapObj item = Indications[i];
                if (item.EndLocation.Y > bottommost)
                {
                    bottommost = item.EndLocation.Y;
                    itemnumber = i;
                }
            }
            if (bottommost == 0) return (-1);
            return (bottommost);
        }

        private ToolStripMenuItem selectAllToolStripMenuItem;

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAll();
            //HighlightedItems.Clear(); InterimOffsets.Clear();
            //for (int i = 0; i < Indications.Count; i++)
            //{
            //    HighlightedItems.Add(i);
            //    InterimOffsets.Add(new Point(0, 0));
            //}
            //EditProperties();
        }

        private void sendToBackToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            
                SendObjectstoBack();
            /*if ((highlighteditem > -1) && (highlighteditem < Indications.Count))
            {
                MapObj item = Indications[highlighteditem];
                List<MapObj> temp = new List<MapObj>();
                for (int i = 0; i < Indications.Count; i++)
                    if (i != highlighteditem)
                    {
                        MapObj items = Indications[i];
                        temp.Add(items);
                    }
                Indications.Clear();
                Indications.Add(item);
                PushIndications();
                for (int i = 0; i < temp.Count; i++)
                {
                    MapObj items = temp[i];
                    Indications.Add(items);
                    PushIndications();
                }
            }*/
        }

        private void SendObjectstoBack()
        {
            List<int> newhighlighteditems = new List<int>();
            List<MapObj> tempnonhighlighteditems = new List<MapObj>();
            for (int i = 0; i < Indications.Count; i++)
                if (!inhighlighteditems(i))
                {
                    MapObj items = Indications[i];
                    tempnonhighlighteditems.Add(items);
                }
            List<MapObj> temphighlighteditems = new List<MapObj>();
            for (int i = 0; i < Indications.Count; i++)
                if (inhighlighteditems(i))
                {
                    MapObj items = Indications[i];
                    temphighlighteditems.Add(items);
                }
            PushIndications();
            Indications.Clear();
            HighlightedItems.Clear();
            for (int i = 0; i < temphighlighteditems.Count; i++)
            {
                HighlightedItems.Add(i);
                MapObj items = temphighlighteditems[i];
                Indications.Add(items);
            }
            for (int i = 0; i < tempnonhighlighteditems.Count; i++)
            {
                MapObj items = tempnonhighlighteditems[i];
                Indications.Add(items);
            }
        }

        private bool inhighlighteditems(int item)
        {
            for (int i = 0; i < HighlightedItems.Count; i++)
                if (item == HighlightedItems[i]) return (true);
            return(false);            
        }

        private void SendObjecttoBack()
        {
            if ((highlighteditem > -1) && (highlighteditem < Indications.Count))
            {
                MapObj item = Indications[highlighteditem];
                List<MapObj> temp = new List<MapObj>();
                for (int i = 0; i < Indications.Count; i++)
                    if (i != highlighteditem)
                    {
                        MapObj items = Indications[i];
                        temp.Add(items);
                    }
                Indications.Clear();
                Indications.Add(item);
                PushIndications();
                for (int i = 0; i < temp.Count; i++)
                {
                    MapObj items = temp[i];
                    Indications.Add(items);
                    PushIndications();
                }
            }

        }

        private void bringToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BringObjectstoFront();
            /*if ((highlighteditem > -1) && (highlighteditem < Indications.Count))
            {
                MapObj item = Indications[highlighteditem];
                Indications.RemoveAt(highlighteditem);
                Indications.Add(item);
                PushIndications();
            }*/
        }

        private void BringObjecttoFront()
        {
            if ((highlighteditem > -1) && (highlighteditem < Indications.Count))
            {
                MapObj item = Indications[highlighteditem];
                Indications.RemoveAt(highlighteditem);
                Indications.Add(item);
                PushIndications();
            }
        }

        private void BringObjectstoFront()
        {
            List<int> newhighlighteditems = new List<int>();
            List<MapObj> tempnonhighlighteditems = new List<MapObj>();
            for (int i = 0; i < Indications.Count; i++)
                if (!inhighlighteditems(i))
                {
                    MapObj items = Indications[i];
                    tempnonhighlighteditems.Add(items);
                }
            List<MapObj> temphighlighteditems = new List<MapObj>();
            for (int i = 0; i < Indications.Count; i++)
                if (inhighlighteditems(i))
                {
                    MapObj items = Indications[i];
                    temphighlighteditems.Add(items);
                }
            PushIndications();
            Indications.Clear();
            HighlightedItems.Clear();
            for (int i = 0; i < tempnonhighlighteditems.Count; i++)
            {
                MapObj items = tempnonhighlighteditems[i];
                Indications.Add(items);
            }
            int start = Indications.Count;
            for (int i = 0; i < temphighlighteditems.Count; i++)
            {
                HighlightedItems.Add(start + i);
                MapObj items = temphighlighteditems[i];
                Indications.Add(items);
            }
        }

        private Label label10;

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private ComboBox ShapecomboBox1;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommitChange();
        }

        private void HighColourString_TextChanged(object sender, EventArgs e)
        {

        }

        private void HighColourString_MouseClick(object sender, MouseEventArgs e)
        {
            IndicationColour1.BackColor = Choose_Colour();
            CommitChange();
        }

        private void LowColourString_MouseClick(object sender, MouseEventArgs e)
        {
            LowColourString.BackColor = Choose_Colour();
            CommitChange();
        }

        private ToolStripButton SimMode;

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            HandButton.Checked = true;
            ArrowButton.Checked = false;

            RectangleButton.Checked = false;
            CircleButton.Checked = false;
            LabelButton.Checked = false;

            RectangleButton.Visible = false;
            CircleButton.Visible = false;
            LabelButton.Visible = false;
            toolStripSeparator2.Visible = false;
            UndoButton.Visible = false;
            RedoButton.Visible = false;
            toolStripSeparator3.Visible = false;
            ArrowButton.Visible = true;

            ShowHideToolbar("Hide");
            toolStrip2.Visible = false;


            SimMode.Checked = true;
            Design.Checked = false;
            setupmode = false;

            Invalidate();            
        }
        
        private CheckBox TransparentCheckbox;
        private Label label8;
        private TextBox IndicationColour4;
        private TextBox Indication4;
        private TextBox IndicationColour2;
        private TextBox Indication2;
        private TextBox IndicationColour3;
        private TextBox Indication3;
        private TextBox LabelText;
        private Label label11;

        private void PolygonButton_Click(object sender, EventArgs e)
        {
            HandButton.Checked = false;
            RectangleButton.Checked = false;
            ArrowButton.Checked = false;
            CircleButton.Checked = false;
            LabelButton.Checked = false;
        }

        private Label label12;
        private TextBox ControlName;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn PointNumber;
        private DataGridViewTextBoxColumn XCoordinate;
        private DataGridViewTextBoxColumn YCoordinate;
        private TextBox AngleBox;
        private Label Angle;
        private ToolStripMenuItem enlargeToolStripMenuItem;
        private ToolStripMenuItem shrinkToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;

        private void enlargeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScaleHighlightedItems(1.25f);
        }

        private void shrinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScaleHighlightedItems(1/1.25f);
        }


        private void RotateC()
        {
            int refitem = 0;

            int MinX = 100000;
            int MinY;
            int temp;
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                MapObj item = Indications[HighlightedItems[i]];
                if (item.StartLocation.X < MinX) { MinX = item.StartLocation.X; refitem = i; }
            }
            if (HighlightedItems.Count > 0)
            {
                MapObj refitemdetails = Indications[HighlightedItems[refitem]];
                MinX = refitemdetails.StartLocation.X;
                MinY = refitemdetails.StartLocation.Y;
                for (int i = 0; i < HighlightedItems.Count; i++)
                {
                    MapObj item = Indications[HighlightedItems[i]];
                    Point newStartLocation = new Point(MinX - (MinY - item.EndLocation.Y), MinY - (item.StartLocation.X - MinX));
                    Point newEndLocation = new Point(MinX - (MinY - item.StartLocation.Y), MinY - (item.EndLocation.X - MinX));
                    if(newStartLocation.X > newEndLocation.X)
                    {
                        temp = newStartLocation.X;
                        newStartLocation.X = newEndLocation.X;
                        newEndLocation.X = temp;
                    }
                    if (newStartLocation.Y > newEndLocation.Y)
                    {
                        temp = newStartLocation.Y;
                        newStartLocation.Y = newEndLocation.Y;
                        newEndLocation.Y = temp;
                    }
                    item.StartLocation = newStartLocation;
                    item.EndLocation = newEndLocation;
                    Indications[HighlightedItems[i]] = item;
                    PushIndications();
                }
            }
            else
            {
                if (highlighteditem != -1)
                {
                    MapObj refitemdetails = Indications[highlighteditem];
                    MinX = refitemdetails.StartLocation.X;
                    MinY = refitemdetails.StartLocation.Y;
                    MapObj item = Indications[highlighteditem];
                    Point newStartLocation = new Point(MinX - (MinY - item.EndLocation.Y), MinY - (item.StartLocation.X - MinX));
                    Point newEndLocation = new Point(MinX - (MinY - item.StartLocation.Y), MinY - (item.EndLocation.X - MinX));
                    if (newStartLocation.X > newEndLocation.X)
                    {
                        temp = newStartLocation.X;
                        newStartLocation.X = newEndLocation.X;
                        newEndLocation.X = temp;
                    }
                    if (newStartLocation.Y > newEndLocation.Y)
                    {
                        temp = newStartLocation.Y;
                        newStartLocation.Y = newEndLocation.Y;
                        newEndLocation.Y = temp;
                    }
                    item.StartLocation = newStartLocation;
                    item.EndLocation = newEndLocation;
                    Indications[highlighteditem] = item;
                    PushIndications();
                }
            }
            Invalidate();
        }


        private void RotateAC()
        {
            int refitem = 0;

            int MinX = 100000;
            int MinY;
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                MapObj item = Indications[HighlightedItems[i]];
                if (item.StartLocation.X < MinX) { MinX = item.StartLocation.X; refitem = i; }
            }
            if (HighlightedItems.Count > 0)
            {
                MapObj refitemdetails = Indications[HighlightedItems[refitem]];
                MinX = refitemdetails.StartLocation.X;
                MinY = refitemdetails.StartLocation.Y;
                for (int i = 0; i < HighlightedItems.Count; i++)
                {
                    MapObj item = Indications[HighlightedItems[i]];
                    Point newStartLocation = new Point(MinX + MinY - item.EndLocation.Y, MinY + item.StartLocation.X - MinX);                                                      
                    Point newEndLocation = new Point(MinX + MinY - item.StartLocation.Y, MinY + item.EndLocation.X - MinX);
                    item.StartLocation = newStartLocation;
                    item.EndLocation = newEndLocation;
                    Indications[HighlightedItems[i]] = item;
                    PushIndications();
                }
            }
            else
            {
                if (highlighteditem != -1)
                {
                    MapObj refitemdetails = Indications[highlighteditem];
                    MinX = refitemdetails.StartLocation.X;
                    MinY = refitemdetails.StartLocation.Y;
                    MapObj item = Indications[highlighteditem];
                    Point newStartLocation = new Point(MinX + MinY - item.EndLocation.Y, MinY + item.StartLocation.X - MinX);
                    Point newEndLocation = new Point(MinX + MinY - item.StartLocation.Y, MinY + item.EndLocation.X - MinX);
                    item.StartLocation = newStartLocation;
                    item.EndLocation = newEndLocation;
                    Indications[highlighteditem] = item;
                    PushIndications();
                }
            }
            Invalidate();
        }

        private void ScaleHighlightedItems(float scale)
        {
            int refitem = 0;

            int MinX = 100000;
            int MinY;
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                MapObj item = Indications[HighlightedItems[i]];
                if (item.StartLocation.X < MinX) { MinX = item.StartLocation.X; refitem = i; }
            }
            if (HighlightedItems.Count > 0)
            {
                MapObj refitemdetails = Indications[HighlightedItems[refitem]];
                MinX = refitemdetails.StartLocation.X;
                MinY = refitemdetails.StartLocation.Y;
                for (int i = 0; i < HighlightedItems.Count; i++)
                {
                    MapObj item = Indications[HighlightedItems[i]];
                    Point newStartLocation = new Point(MinX + (int)((((float)(item.StartLocation.X - MinX)) * (scale))),
                                                       MinY + (int)((((float)(item.StartLocation.Y - MinY)) * (scale))));
                    Point newEndLocation = new Point(MinX + (int)((((float)(item.EndLocation.X - MinX)) * (scale))),
                                                     MinY + (int)((((float)(item.EndLocation.Y - MinY)) * (scale))));
                    item.StartLocation = newStartLocation;
                    item.EndLocation = newEndLocation;
                    Indications[HighlightedItems[i]] = item;
                    PushIndications();
                }
            }
            else
            {
                if (highlighteditem != -1)
                {
                    MapObj refitemdetails = Indications[highlighteditem];
                    MinX = refitemdetails.StartLocation.X;
                    MinY = refitemdetails.StartLocation.Y;
                    MapObj item = Indications[highlighteditem];
                    Point newStartLocation = new Point(MinX + (int)((((float)(item.StartLocation.X - MinX)) * (scale))),
                                                       MinY + (int)((((float)(item.StartLocation.Y - MinY)) * (scale))));
                    Point newEndLocation = new Point(MinX + (int)((((float)(item.EndLocation.X - MinX)) * (scale))),
                                                     MinY + (int)((((float)(item.EndLocation.Y - MinY)) * (scale))));
                    item.StartLocation = newStartLocation;
                    item.EndLocation = newEndLocation;
                    Indications[highlighteditem] = item;
                    PushIndications();
                }
            }
            Invalidate();
        }
        private Button button5;
        private Button button6;

        private void button5_Click_1(object sender, EventArgs e)
        {
            ScaleHighlightedItems(1.25f);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            ScaleHighlightedItems(1/1.25f);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            applyzoom(1.25f,-1,-1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            applyzoom(0.8f,-1,-1);
        }

        private void Height_TextChanged(object sender, EventArgs e)
        {
            CommitChange();
        }

        private void Width_TextChanged(object sender, EventArgs e)
        {
            CommitChange();
        }

        private void YCoord_TextChanged(object sender, EventArgs e)
        {
            CommitChange();
        }

        private void XCoord_TextChanged(object sender, EventArgs e)
        {
            CommitChange();
        }

        private void AngleBox_TextChanged(object sender, EventArgs e)
        {
            CommitChange();
        }

        private void Indication_TextChanged(object sender, EventArgs e)
        {
            CommitChange();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            CommitChange();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            CommitChange();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            CommitChange();
        }

        private void LabelText_TextChanged(object sender, EventArgs e)
        {
            CommitChange();
        }

        private Button button9;

        private void button9_Click(object sender, EventArgs e)
        {
            deleteSelectedItems();
        }

        //public struct MapObj
        //{
        //    public string TypeofObj; //Label, Indication, ActivateText
        //    public string Name;
        //    public string Text;
        //    public string Indication1;
        //    public string Indication2;
        //    public string Indication3;
        //    public string Indication4;
        //    public Point StartLocation;
        //    public Point EndLocation;
        //    public int RotationAngle;
        //    public String Shape;
        //    public Color HighColour;
        //    public Color IndColour1;
        //    public Color IndColour2;
        //    public Color IndColour3;
        //    public Color IndColour4;
        //    public Color LowColour;
        //    public List<Point> PolygonPoints;
        //    public bool Transparent;
        //}

        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            IndicationColour2.BackColor = Choose_Colour();
            CommitChange();
        }

        private void IndicationColour3_MouseClick(object sender, MouseEventArgs e)
        {
            IndicationColour3.BackColor = Choose_Colour();
            CommitChange();
        }

        private void IndicationColour4_MouseClick(object sender, MouseEventArgs e)
        {
            IndicationColour4.BackColor = Choose_Colour();
            CommitChange();
        }

        private TextBox textBox3;
        private TextBox DummyTextBox;
        private TextBox textBox4;
        private ToolStripButton HandButton;

        private void toolStripButton1_Click_2(object sender, EventArgs e)
        {
            HandButton.Checked = true;
            RectangleButton.Checked = false;
            ArrowButton.Checked = false;
            CircleButton.Checked = false;
            LabelButton.Checked = false;
            //setupmode = false;
        }

        private TextBox textBox5;
        private TextBox textBox6;
        private TextBox textBox7;
        private TextBox textBox8;
        private TextBox textBox9;
        private TextBox textBox10;

        private void TransparentCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            TransparentCheckbox.BackColor = Color.White;
            CommitChange();
        }

        private void ControlName_TextChanged(object sender, EventArgs e)
        {
            CommitChange();
        }

        private Label label13;
        private TextBox textBox11;

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            //ScaleHighlightedItems(1.25f);
        }

        private Button button10;

        private void button10_Click(object sender, EventArgs e)
        {            
            try
            {
                float amount = float.Parse(textBox11.Text);
                ScaleHighlightedItems(float.Parse(textBox11.Text));
            }
            catch
            {
                MessageBox.Show("Error reading Scale: " + textBox11.Text, "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private ToolStripMenuItem flipToolStripMenuItem;
        private ToolStripMenuItem verticalToolStripMenuItem;
        private ToolStripMenuItem horizontalToolStripMenuItem;

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HighlightedItemsDelete.Clear();
            if (HighlightedItems.Count > 0)
            {
                for (int i = 0; i < HighlightedItems.Count; i++)
                {
                    MapObj item = Indications[HighlightedItems[i]];
                    MapObj temp = item;
                    if (item.Shape == "Rectangle")
                    {
                        int height = item.EndLocation.Y - item.StartLocation.Y;
                        int width = item.EndLocation.X - item.StartLocation.X;

                        temp.StartLocation.X = (2 * pointerLocation.X - item.StartLocation.X) - (int)(height * Math.Sin(DegtoRad(item.RotationAngle)));
                        temp.StartLocation.Y = item.StartLocation.Y + (int)(height * Math.Cos(DegtoRad(item.RotationAngle)));
                        temp.EndLocation.Y = temp.StartLocation.Y + height;
                        temp.EndLocation.X = temp.StartLocation.X + width;

                        temp.RotationAngle = 180 - item.RotationAngle;
                        if (temp.RotationAngle < 0) temp.RotationAngle += 360;
                        if (temp.RotationAngle > 360) temp.RotationAngle -= 360;
                    }
                    if ((item.Shape == "Circle") || (item.Shape == "Label"))
                    {
                        int height = item.EndLocation.Y - item.StartLocation.Y;
                        int width = item.EndLocation.X - item.StartLocation.X;
                        Point centre = new Point(item.StartLocation.X + width / 2, item.StartLocation.Y + height / 2);
                        centre.X = (2 * pointerLocation.X) - centre.X;
                        temp.StartLocation.X = centre.X - (width/2);
                        temp.StartLocation.Y = centre.Y - (height/2);
                        temp.EndLocation.X = centre.X + (width / 2);
                        temp.EndLocation.Y = centre.Y + (height / 2); 
                    }
                    Indications[HighlightedItems[i]] = temp;
                    PushIndications();
                }
            }
            Invalidate();
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PushIndications();
            HighlightedItemsDelete.Clear();
            if (HighlightedItems.Count > 0)
            {
                for (int i = 0; i < HighlightedItems.Count; i++)
                {
                    MapObj item = Indications[HighlightedItems[i]];                    
                    MapObj temp = item;
                    if (item.Shape == "Rectangle")
                    {
                        int height = item.EndLocation.Y - item.StartLocation.Y;
                        int Width = item.EndLocation.X - item.StartLocation.X;

                        temp.StartLocation.Y = (2 * pointerLocation.Y - item.StartLocation.Y) - (int)(height * Math.Cos(DegtoRad(item.RotationAngle)));
                        temp.StartLocation.X = item.StartLocation.X + (int)(height * Math.Sin(DegtoRad(item.RotationAngle)));
                        temp.EndLocation.X = temp.StartLocation.X + Width;
                        temp.EndLocation.Y = temp.StartLocation.Y + height;

                        temp.RotationAngle = -item.RotationAngle;
                        if (temp.RotationAngle < 0) temp.RotationAngle += 360;
                        if (temp.RotationAngle > 360) temp.RotationAngle -= 360;
                    }
                    if ((item.Shape == "Circle") || (item.Shape == "Label"))
                    {
                        int height = item.EndLocation.Y - item.StartLocation.Y;
                        int width = item.EndLocation.X - item.StartLocation.X;
                        Point centre = new Point(item.StartLocation.X + width / 2, item.StartLocation.Y + height / 2);
                        centre.Y = (2 * pointerLocation.Y) - centre.Y;
                        temp.StartLocation.X = centre.X - (width / 2);
                        temp.StartLocation.Y = centre.Y - (height / 2);
                        temp.EndLocation.X = centre.X + (width / 2);
                        temp.EndLocation.Y = centre.Y + (height / 2);
                    }
                    Indications[HighlightedItems[i]] = temp;
                    PushIndications();
                }
            }
            Invalidate();
        }

        private void PushIndications()
        {
            if (NewIndicationUnique())
            {
                List<MapObj> NewIndications = new List<MapObj>();
                for (int i = 0; i < Indications.Count; i++)
                {
                    MapObj item = Indications[i];
                    NewIndications.Add(item);
                }
                if (UndoList.Count > UndoIndex + 1)
                    UndoList.RemoveRange(UndoIndex + 1, UndoList.Count - (UndoIndex+1));
                UndoList.Add(NewIndications);
                UndoIndex = UndoList.Count - 1;
            }
        }

        private bool NewIndicationUnique()
        {
            if (UndoList.Count == 0) return (true);
            else
            {
                List<MapObj> OldIndications = UndoList[UndoList.Count - 1];
                if (OldIndications.Count != Indications.Count) return (true);
                for (int i = 0; i < Indications.Count; i++)
                {
                    MapObj item = Indications[i];
                    MapObj OldObj = OldIndications[i];
                    if (item.HighColour != OldObj.HighColour) return (true);
                    if (item.LowColour != OldObj.LowColour) return (true);
                    if (item.Shape != OldObj.Shape) return (true);
                    if (item.StartLocation != OldObj.StartLocation) return (true);
                    if (item.EndLocation != OldObj.EndLocation) return (true);
                    if (item.IndColour1 != OldObj.IndColour1) return (true);
                    if (item.IndColour2 != OldObj.IndColour2) return (true);
                    if (item.IndColour3 != OldObj.IndColour3) return (true);
                    if (item.IndColour4 != OldObj.IndColour4) return (true);
                    if (item.Control != OldObj.Control) return (true);
                    if (item.Indication1 != OldObj.Indication1) return (true);
                    if (item.Indication2 != OldObj.Indication2) return (true);
                    if (item.Indication3 != OldObj.Indication3) return (true);
                    if (item.Indication4 != OldObj.Indication4) return (true);
                    if (item.Name != OldObj.Name) return (true);
                    if (item.Textsize != OldObj.Textsize) return (true);
                    if (item.Text != OldObj.Text) return (true);
                    if (item.TypeofObj != OldObj.TypeofObj) return (true);
                    if (item.IsTrack != OldObj.IsTrack) return (true);
                    if (item.Transparent != OldObj.Transparent) return (true);
                    if (item.RotationAngle != OldObj.RotationAngle) return (true);
                }
                return (false);
            }
        }

        private Button button11;
        private Button button12;
        private Label label14;
        private Button button13;
        private TextBox textBox12;
        private Label label15;
        private Label label16;

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                float amount = float.Parse(textBox11.Text);
                //RotateSelectionBy(float.Parse(textBox11.Text));
            }
            catch
            {
                MessageBox.Show("Error reading Scale: " + textBox11.Text, "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (UndoIndex > 0)
            {
                Indications.Clear();
                MapObj item;
                List<MapObj> NewIndications = UndoList[UndoIndex - 1];
                for (int i = 0; i < NewIndications.Count; i++)
                {
                    item = NewIndications[i];
                    Indications.Add(item);
                }
                UndoIndex--;
                //UndoList.RemoveAt(UndoIndex);
                if (UndoIndex == -1) UndoIndex = 0;
            }
            Invalidate();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if ((UndoIndex > 0) && (UndoList.Count > UndoIndex + 1))
            {
                Indications.Clear();
                MapObj item;
                List<MapObj> NewIndications = UndoList[UndoIndex + 1];
                for (int i = 0; i < NewIndications.Count; i++)
                {
                    item = NewIndications[i];
                    Indications.Add(item);
                }
                UndoIndex++;
                if (UndoIndex == -1) UndoIndex = 0;
            }
            Invalidate();
        }

        private ComboBox comboBox1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton UndoButton;
        private ToolStripButton RedoButton;

        private void UndoButton_Click(object sender, EventArgs e)
        {
            undo();
        }

        private void undo()
        {
            if (UndoIndex > 0)
            {
                Indications.Clear();
                MapObj item;
                List<MapObj> NewIndications = UndoList[UndoIndex - 1];
                for (int i = 0; i < NewIndications.Count; i++)
                {
                    item = NewIndications[i];
                    Indications.Add(item);
                }
                UndoIndex--;
                //UndoList.RemoveAt(UndoIndex);
                if (UndoIndex == -1) UndoIndex = 0;
            }
            Invalidate();

        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            redo();
        }

        private void redo()
        {
            if ((UndoIndex > 0) && (UndoList.Count > UndoIndex + 1))
            {
                Indications.Clear();
                MapObj item;
                List<MapObj> NewIndications = UndoList[UndoIndex + 1];
                for (int i = 0; i < NewIndications.Count; i++)
                {
                    item = NewIndications[i];
                    Indications.Add(item);
                }
                UndoIndex++;
                if (UndoIndex == -1) UndoIndex = 0;
            }
            Invalidate();
        }

        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton toolStripButton1;
        private ToolStripButton ZoomOutButton;

        private void toolStripButton1_Click_3(object sender, EventArgs e)
        {
            applyzoom(1.25f,-1,-1);
        }

        private void ZoomOutButton_Click(object sender, EventArgs e)
        {
            simspeedcommand = "SaveLogicState";
        }

        public void applyzoom(float scalefactormultiplyer, float originx, float originy)
        {
            if (originx == -1)
            {
                float oldscalefactor = scaleFactor;
                textBox10.Text = scaleFactor.ToString();
                scaleFactor = scaleFactor * scalefactormultiplyer;
                totalpan.X += (int)((ClientSize.Width * 0.5  / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                totalpan.Y += (int)((ClientSize.Height * 0.5  / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                pan.X = totalpan.X;
                pan.Y = totalpan.Y;
                panSinceClick.X = 0;
                panSinceClick.Y = 0;
                Invalidate();
            }
            else
            {
                float oldscalefactor = scaleFactor;
                textBox10.Text = scaleFactor.ToString();
                scaleFactor = scaleFactor * scalefactormultiplyer;
                totalpan.X += (int) ((originx / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                totalpan.Y += (int) ((originy / oldscalefactor) * (1 - (oldscalefactor / scaleFactor)));
                pan.X = totalpan.X;
                pan.Y = totalpan.Y;
                panSinceClick.X = 0;
                panSinceClick.Y = 0;
                Invalidate();
            }
        }
        
        public void fitscreen(int dimension)
        {
            int startx = 10000;
            int starty = 10000;
            int endx = 0;
            int endy = 0;
            float scalefactorx = 1;
            float scalefactory = 1;
            float margin = 0.02f;
            int panoffsetx = 0;
            int panoffsety = 0;

            for (int i = 0; i < Indications.Count; i++)
            {
                MapObj item = Indications[i];
                if (startx > item.StartLocation.X) startx = item.StartLocation.X;
                if (starty > item.StartLocation.Y) starty = item.StartLocation.Y;
                if (endx < item.EndLocation.X) endx = item.EndLocation.X;
                if (endy < item.EndLocation.Y) endy = item.EndLocation.Y;
            }

            scalefactorx =  ((1 - margin) * ClientSize.Width / (float) (endx - startx));
            scalefactory = ((1 - margin) * ClientSize.Height / (float) (endy - starty));

            if (dimension == PORTRAITLANDSCAPE)
            {
                if (scalefactorx > scalefactory)
                {
                    scaleFactor = scalefactory;
                    //panoffsetx = (int)((scalefactory / scalefactorx) * (endx - startx));
                }
                else
                {
                    scaleFactor = scalefactorx;
                    //panoffsety = (int) ((endy - starty)*0.5*ClientSize.Height / ClientSize.Width);
                }
            }
            if (dimension == PORTRAIT) scaleFactor = scalefactory;
            if (dimension == LANDSCAPE) scaleFactor = scalefactorx;

            totalpan.X = startx + (int) ((-margin/2) * (endx - startx)) + panoffsetx;
            totalpan.Y = starty + (int) (-30/scaleFactor) + (int)((-margin / 2) * (endy - starty)) - panoffsety;
            pan.X = totalpan.X;
            pan.Y = totalpan.Y;
            panSinceClick.X = 0;
            panSinceClick.Y = 0;
        }


        private TextBox textBox13;
        private ToolStripButton Design;

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            setupmode = true;

            HandButton.Checked = false;
            RectangleButton.Checked = false;
            ArrowButton.Checked = true;
            CircleButton.Checked = false;
            LabelButton.Checked = false;
                        
            RectangleButton.Visible = true;
            CircleButton.Visible = true;
            LabelButton.Visible = true;
            toolStripSeparator2.Visible = true;
            UndoButton.Visible = true;
            RedoButton.Visible = true;
            toolStripSeparator3.Visible = true;
            ArrowButton.Visible = true;


            if (button3.Text == "Hide Properties")
                ShowHideToolbar("Show");
            toolStrip2.Visible = true;
            

            SimMode.Checked = false;
            Design.Checked = true;            
            Invalidate();
        }

        private ToolStripSeparator toolStripSeparator4;

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //this.MdiParent.SaveMap();
        }

        private ToolTip toolTip1;

        private ToolStrip toolStrip2;
        private ToolStripButton Properties;

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            if (Properties.Text == "Hide Properties")
            {
                Properties.Text = "Show Properties";
                ShowHideToolbar("Hide");

            }
            else
            {
                Properties.Text = "Hide Properties";
                ShowHideToolbar("Show");
            }
        }
        private ToolStripButton Enlarge;
        private ToolStripButton Shrink;
        private Panel panel1;

        private void Enlarge_Click(object sender, EventArgs e)
        {
            ScaleHighlightedItems(1.25f);
        }

        private void Shrink_Click(object sender, EventArgs e)
        {

            ScaleHighlightedItems(1 / 1.25f);
        }
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton4;

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            deleteSelectedItems();
        }

        private ToolStripButton toolStripButton5;
        private ToolStripTextBox toolStripTextBox2;

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {
                if (toolStrip4.Visible) toolStrip4.Visible = false;
                else toolStrip4.Visible = true;
            }
            catch
            {
                MessageBox.Show("Error reading Scale: " + toolStripTextBox2.Text, "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        public void CircularizeBy(float radius, float offset)
        {
            try
            {
                if (HighlightedItemsCopied.Count != 0)
                {
                    int items = HighlightedItemsCopied.Count;
                    int startnumber = FindLeftMostObject(HighlightedItemsCopied);
                    MapObj item = Indications[HighlightedItemsCopied[startnumber]];
                    Point delta = new Point(0, 0);
                    delta.X = mouselocation.X;// -item.StartLocation.X;//LocationOfMouse.X - item.StartLocation.X;
                    delta.Y = mouselocation.Y;// -item.StartLocation.Y;//LocationOfMouse.Y - item.StartLocation.Y;
                    Startadj.X = (int)((float)((delta.X) + totalpan.X));
                    Startadj.Y = (int)((float)((delta.Y) + totalpan.Y));
                    if (cutting) deleteSelectedItems();
                    HighlightedItems.Clear();
                    for (int i = 0; i < HighlightedItemsCopied.Count; i++)
                    {
                        item = Indications[HighlightedItemsCopied[i]];
                        int width = item.EndLocation.X - item.StartLocation.X;
                        int height = item.EndLocation.Y - item.StartLocation.Y;
                        item.StartLocation.X = Startadj.X + (int)(radius * Math.Cos(i * 2 * 3.14159 / items));
                        item.StartLocation.Y = Startadj.Y + (int)(radius * Math.Sin(i * 2 * 3.14159 / items));
                        item.EndLocation.X = item.StartLocation.X + width;
                        item.EndLocation.Y = item.StartLocation.Y + height;
                        Indications.Add(item);
                        PushIndications();
                        HighlightedItems.Add(Indications.Count - 1);
                    }
                }
                else
                {
                    if (Copyitem != -1)
                    {
                        MapObj item = Indications[Copyitem];
                        if (cutting) deleteSelectedItems();
                        Point delta = new Point(0, 0);
                        delta.X = mouselocation.X - item.StartLocation.X;//LocationOfMouse.X - item.StartLocation.X;
                        delta.Y = mouselocation.X - item.StartLocation.X;//LocationOfMouse.Y - item.StartLocation.Y;
                        Startadj.X = (int)((float)((delta.X) + totalpan.X));
                        Startadj.Y = (int)((float)((delta.Y) + totalpan.Y));
                        int width = item.EndLocation.X - item.StartLocation.X;
                        int height = item.EndLocation.Y - item.StartLocation.Y;
                        item.StartLocation.X += Startadj.X;//delta.X;
                        item.StartLocation.Y += Startadj.Y;// delta.Y;
                        item.EndLocation.X = item.StartLocation.X + width;
                        item.EndLocation.Y = item.StartLocation.Y + height;
                        Indications.Add(item);
                        PushIndications();
                    }
                }
                cutting = false;
            }
            catch
            {
                MessageBox.Show("Error pasting", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        public void Spoke(float angle, float length)
        {
            try
            {
                if (HighlightedItemsCopied.Count != 0)
                {
                    int items = HighlightedItemsCopied.Count;
                    int startnumber = FindLeftMostObject(HighlightedItemsCopied);
                    MapObj item = Indications[HighlightedItemsCopied[startnumber]];
                    Point delta = new Point(0, 0);
                    delta.X = mouselocation.X;
                    delta.Y = mouselocation.Y;
                    Startadj.X = (int)((float)((delta.X) + totalpan.X));
                    Startadj.Y = (int)((float)((delta.Y) + totalpan.Y));
                    if (cutting) deleteSelectedItems();
                    HighlightedItems.Clear();
                    for (int i = 0; i < HighlightedItemsCopied.Count; i++)
                    {
                        item = Indications[HighlightedItemsCopied[i]];
                        int width = item.EndLocation.X - item.StartLocation.X;
                        int height = item.EndLocation.Y - item.StartLocation.Y;
                        item.StartLocation.X = Startadj.X + (int)(length * i / items * Math.Cos(2 * 3.14159 * angle / 360));
                        item.StartLocation.Y = Startadj.Y + (int)(length * i / items * Math.Sin(2 * 3.14159 * angle / 360));
                        item.EndLocation.X = item.StartLocation.X + width;
                        item.EndLocation.Y = item.StartLocation.Y + height;
                        Indications.Add(item);
                        PushIndications();
                        HighlightedItems.Add(Indications.Count - 1);
                    }
                }
                else
                {
                    if (Copyitem != -1)
                    {
                        MapObj item = Indications[Copyitem];
                        if (cutting) deleteSelectedItems();
                        Point delta = new Point(0, 0);
                        delta.X = mouselocation.X - item.StartLocation.X;//LocationOfMouse.X - item.StartLocation.X;
                        delta.Y = mouselocation.X - item.StartLocation.X;//LocationOfMouse.Y - item.StartLocation.Y;
                        Startadj.X = (int)((float)((delta.X) + totalpan.X));
                        Startadj.Y = (int)((float)((delta.Y) + totalpan.Y));
                        int width = item.EndLocation.X - item.StartLocation.X;
                        int height = item.EndLocation.Y - item.StartLocation.Y;
                        item.StartLocation.X += Startadj.X;//delta.X;
                        item.StartLocation.Y += Startadj.Y;// delta.Y;
                        item.EndLocation.X = item.StartLocation.X + width;
                        item.EndLocation.Y = item.StartLocation.Y + height;
                        Indications.Add(item);
                        PushIndications();
                    }
                }
                cutting = false;
            }
            catch
            {
                MessageBox.Show("Error pasting", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void toolStripButton2_Click_2(object sender, EventArgs e)
        {
            try
            {
                float amount = float.Parse(toolStripTextBox1.Text);
                ScaleHighlightedItems(float.Parse(toolStripTextBox1.Text));
            }
            catch
            {
                MessageBox.Show("Error reading Scale: " + toolStripTextBox1.Text, "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private Label label17;
        private ToolStripButton toolStripButton6;

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void SelectAll()
        {
            HighlightedItems.Clear(); InterimOffsets.Clear();
            for (int i = 0; i < Indications.Count; i++)
            {
                HighlightedItems.Add(i);
                InterimOffsets.Add(new Point(0, 0));
            }
            EditProperties();
        }

        private ToolStripButton toolStripButton7;
        private ToolStripButton toolStripButton8;
        private ToolStripButton toolStripButton9;

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            Paste(1);
        }

        private ToolStripButton toolStripButton10;
        private ToolStripTextBox SearchbyString;

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < Indications.Count; i++)
            {
                bool addtolist = false;
                if (Indications[i].Indication1.IndexOf(SearchbyString.Text) != -1) addtolist = true;
                if (Indications[i].Indication2.IndexOf(SearchbyString.Text) != -1) addtolist = true;
                if (Indications[i].Indication3.IndexOf(SearchbyString.Text) != -1) addtolist = true;
                if (Indications[i].Indication4.IndexOf(SearchbyString.Text) != -1) addtolist = true;
                if (Indications[i].Control.IndexOf(SearchbyString.Text) != -1) addtolist = true;
                if (Indications[i].Text.IndexOf(SearchbyString.Text) != -1) addtolist = true;
                if (addtolist)
                {
                    HighlightedItems.Add(i);//Indications.Count - (i + 1));
                    InterimOffsets.Add(new Point(0, 0));
                }
            }
            Invalidate();*/
            searchbystring();
        }

        private void searchbystring()
        {
            HighlightedItems.Clear();
            for (int i = 0; i < Indications.Count; i++)
            {
                bool addtolist = false;
                if (Indications[i].Indication1.IndexOf(SearchbyString.Text) != -1) addtolist = true;
                if (Indications[i].Indication2.IndexOf(SearchbyString.Text) != -1) addtolist = true;
                if (Indications[i].Indication3.IndexOf(SearchbyString.Text) != -1) addtolist = true;
                if (Indications[i].Indication4.IndexOf(SearchbyString.Text) != -1) addtolist = true;
                if (Indications[i].Control.IndexOf(SearchbyString.Text) != -1) addtolist = true;
                if (Indications[i].Text.IndexOf(SearchbyString.Text) != -1) addtolist = true;
                if (addtolist)
                {
                    HighlightedItems.Add(i);
                    InterimOffsets.Add(new Point(0, 0));
                }
            }
            Invalidate();
        }
        
        private ContextMenuStrip contextMenuStrip2;


        private ToolStripMenuItem toolStripMenuItem01;
        private ToolStripMenuItem toolStripMenuItem02;
        private ToolStripMenuItem toolStripMenuItem03;

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            rungName = contextMenuStrip2.Items[0].Text.ToString();
            //if(e.Button == MouseButtons.Left)
            ShowRungWindow();
        }

        private ToolStripMenuItem toolStripMenuItem04;

        private void toolStripMenuItem02_Click(object sender, EventArgs e)
        {
            rungName = contextMenuStrip2.Items[1].Text.ToString();
            ShowRungWindow();
        }

        private void toolStripMenuItem03_Click(object sender, EventArgs e)
        {
            rungName = contextMenuStrip2.Items[2].Text.ToString();
            ShowRungWindow();
        }

        private void toolStripMenuItem04_Click(object sender, EventArgs e)
        {
            rungName = contextMenuStrip2.Items[3].Text.ToString();
            ShowRungWindow();
        }

        private ToolStripTextBox toolStripTextBox3;

        private ToolStripButton toolStripButton12;
        private ToolStripButton toolStripButton13;
        private ToolStripButton toolStripButton14;
        private ToolStripButton toolStripButton15;

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            Speed.Text = "1";
            simspeedcommand = "Speed:" + Speed.Text;
            setsimspeedcolor((int)(float.Parse(Speed.Text)));
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            Speed.Text = ((int) (float.Parse(Speed.Text) * 10)).ToString();
            simspeedcommand = "Speed:" + Speed.Text;
            setsimspeedcolor((int)(float.Parse(Speed.Text)));
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {

            Speed.Text = ((int)(float.Parse(Speed.Text) / 2)).ToString();
            simspeedcommand = "Speed:" + Speed.Text;
            setsimspeedcolor((int)(float.Parse(Speed.Text)));
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            Speed.Text = "10000";
            simspeedcommand = "Speed:" + Speed.Text;
            setsimspeedcolor((int)(float.Parse(Speed.Text)));
        }

        private void setsimspeedcolor(int speed)
        {          
            if(speed < 255)  
                Speed.BackColor = Color.FromArgb(255, 247 - (speed-8), 247 - (speed-8));
            if(speed < 8)
                Speed.BackColor = Color.FromArgb(255-(8-speed)*10, 255, 255 - (8-speed) * 10);
            if(speed > 255)
                Speed.BackColor = Color.FromArgb(255, 0, 0);

        }

        private ToolStripLabel toolStripLabel1;
        private CheckBox IsTrackCheckbox;

        private void IsTrackCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            IsTrackCheckbox.BackColor = Color.White;
            CommitChange();
        }

        private StatusStrip statusStrip1;



        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                MapObj item = Indications[HighlightedItems[i]];
                item.EndLocation.Y--;
                item.StartLocation.Y--;
                Indications[HighlightedItems[i]] = item;
            }  
        }

        private ToolStripButton toolStripButton19;
        private ToolStripButton toolStripButton20;
        private ToolStripButton toolStripButton21;
        private ToolStripButton toolStripButton22;
        private ToolStripButton toolStripButton23;

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                MapObj item = Indications[HighlightedItems[i]];
                item.EndLocation.Y -= Int32.Parse(NudgeAmount.Text);
                item.StartLocation.Y -= Int32.Parse(NudgeAmount.Text); 
                Indications[HighlightedItems[i]] = item;
            }  
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                MapObj item = Indications[HighlightedItems[i]];
                item.EndLocation.Y += Int32.Parse(NudgeAmount.Text);
                item.StartLocation.Y += Int32.Parse(NudgeAmount.Text);
                Indications[HighlightedItems[i]] = item;
            }  
        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                MapObj item = Indications[HighlightedItems[i]];
                item.EndLocation.X -= Int32.Parse(NudgeAmount.Text);
                item.StartLocation.X -= Int32.Parse(NudgeAmount.Text);
                Indications[HighlightedItems[i]] = item;
            }  
        }

        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                MapObj item = Indications[HighlightedItems[i]];
                item.EndLocation.X += Int32.Parse(NudgeAmount.Text);
                item.StartLocation.X += Int32.Parse(NudgeAmount.Text);
                Indications[HighlightedItems[i]] = item;
            }  
        }

        private ToolStripLabel toolStripLabel2;
        private ToolStripButton toolStripButton24;
        private ToolStripButton toolStripButton25;
        private ToolStripButton toolStripButton26;
        private ToolStripButton toolStripButton27;

        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            ExtendRight(1);
        }

        private void ExtendRight(int amount)
        {
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                MapObj item = Indications[HighlightedItems[i]];
                item.EndLocation.X += amount;
                Indications[HighlightedItems[i]] = item;
            }
        }

        private void rotateparameter(int angle)
        {
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                MapObj item = Indications[HighlightedItems[i]];
                item.RotationAngle += angle;
                Indications[HighlightedItems[i]] = item;
            }
        }

        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            ContractRight(1);
        }

        private void ContractRight(int amount)
        {
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                MapObj item = Indications[HighlightedItems[i]];
                item.EndLocation.X -= amount;
                if(item.EndLocation.X < 1) item.EndLocation.X = 1;
                Indications[HighlightedItems[i]] = item;
            }
        }

        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            ExtendDown(1);
        }

        private void ExtendDown(int amount)
        {
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                MapObj item = Indications[HighlightedItems[i]];
                item.EndLocation.Y += amount;
                Indications[HighlightedItems[i]] = item;
            }  
        }

        private void toolStripButton27_Click(object sender, EventArgs e)
        {
            ContractDown(1);
        }

        private void ContractDown(int amount)
        {
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                MapObj item = Indications[HighlightedItems[i]];
                item.EndLocation.Y -= amount;
                if (item.EndLocation.Y < 1) item.EndLocation.Y = 1;
                Indications[HighlightedItems[i]] = item;
            }
        }

        private void Indication1_DoubleClick(object sender, EventArgs e)
        {
            findusages(Indication1.Text);
        }

        private void ControlName_DoubleClick(object sender, EventArgs e)
        {
            findusages(ControlName.Text);      
        }


        private void findusages(string name)
        {
            int menulength = contextMenuStrip3.Items.Count;
            if (contextMenuStrip3
                .Items.Count > 0)
                for (int i = 0; i < menulength; i++)
                    contextMenuStrip3.Items.RemoveAt(0);
            string contactname = name;
            contextMenuStrip3.Items.Add(contactname + " used in:");
            ArrayList Coils = new ArrayList();
            try
            {
                getRungs(Coils, interlockingNew);
                for (int r = 0; r < interlockingNew.Count; r++)
                {
                    ArrayList rungPointer = (ArrayList)interlockingNew[r];
                    for (int k = 1; k < rungPointer.Count - 1; k++)
                    {
                        Contact contact = (Contact)rungPointer[k];
                        if (contact.name == contactname)
                        {
                            contextMenuStrip3.Items.Add(rungPointer[rungPointer.Count - 1].ToString());
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error populating context menu ", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            contextMenuStrip3.Show(Cursor.Position);     
        }

        private ContextMenuStrip contextMenuStrip3;

        private void contextMenuStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            rungName = item.ToString();
            ShowRungWindow();
        }

        private void Indication2_DoubleClick(object sender, EventArgs e)
        {
            findusages(Indication2.Text);
        }

        private void Indication3_DoubleClick(object sender, EventArgs e)
        {
            findusages(Indication3.Text);
        }

        private void Indication4_DoubleClick(object sender, EventArgs e)
        {
            findusages(Indication4.Text);
        }

        private ToolStripButton toolStripButton29;
        private ToolStripButton toolStripButton28;

        private void toolStripButton29_Click(object sender, EventArgs e)
        {
            if(toolStripButton29.Checked)
            //if (toolStripButton29.Text == "Lock Horizontal")
            {                
                toolStripButton29.Checked = false;
                //toolStripButton29.Text = "Unlock Horizontal";
                verticallock = false;
            }
            else
            {
                toolStripButton29.Checked = true;
                //toolStripButton29.Text = "Lock Horizontal";
                verticallock = true;
            }
        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            if (toolStripButton28.Checked)
            //if (toolStripButton28.Text == "Lock Vertical")
            {
                toolStripButton28.Checked = false;
                //toolStripButton28.Text = "Unlock Vertical";
                horizontallock = false;
            }
            else
            {
                toolStripButton28.Checked = true;
                //toolStripButton28.Text = "Lock Vertical";
                horizontallock = true;
            }
        }

        private ToolStripLabel toolStripLabel3;


        public ToolStripComboBox NudgeAmount;



        private ToolStripButton toolStripButton52;
        private ToolStripButton toolStripButton53;
        private ToolStripButton toolStripButton54;
        private ToolStripButton toolStripButton55;
        private ToolStripButton toolStripButton56;
        private ToolStrip toolStrip3;
        private ToolStripButton toolStripButton30;
        private ToolStripButton toolStripButton31;
        private ToolStripButton toolStripButton32;
        private ToolStripButton toolStripButton33;
        private ToolStripButton toolStripButton34;
        private ToolStripButton toolStripButton35;
        private ToolStripButton toolStripButton36;
        private ToolStripTextBox toolStripTextBox4;
        private ToolStripButton toolStripButton37;
        private ToolStripButton toolStripButton38;
        private ToolStripButton toolStripButton39;
        private ToolStripButton toolStripButton40;
        private ToolStripButton toolStripButton41;
        private ToolStripLabel toolStripLabel4;
        private ToolStripButton toolStripButton42;
        private ToolStripButton toolStripButton43;
        private ToolStripButton toolStripButton44;
        private ToolStripButton toolStripButton45;
        public ToolStripComboBox toolStripComboBox1;
        private ToolStripLabel toolStripLabel5;
        private ToolStripButton toolStripButton46;
        private ToolStripButton toolStripButton47;
        private ToolStripButton toolStripButton48;
        private ToolStripButton toolStripButton49;
        private ToolStripButton toolStripButton50;
        private ToolStripTextBox toolStripTextBox5;
        private ToolStripButton toolStripButton51;
        private ToolStripTextBox toolStripTextBox6;

        private void toolStripButton53_Click(object sender, EventArgs e)
        {
            MoveUp(1);
        }

        private void MoveUp(int dist)
        {
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                MapObj item = Indications[HighlightedItems[i]];
                item.EndLocation.Y -= dist;
                item.StartLocation.Y -= dist;
                Indications[HighlightedItems[i]] = item;
            }
            Invalidate();
        }


        private void MoveNextUp(int dist)
        {
            int upmosty = 10000;
            int closest = -1;
            int y = 0;
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                y = Indications[HighlightedItems[i]].StartLocation.Y;
                if (upmosty > y)
                    upmosty = y;
            }
            for (int i = 0; i < Indications.Count; i++)
            {
                y = Indications[i].StartLocation.Y;
                if (y < upmosty)
                    if (y > closest)
                        closest = y;
            }
            if (closest != -1)
                for (int i = 0; i < HighlightedItems.Count; i++)
                {
                    MapObj item = Indications[HighlightedItems[i]];
                    item.EndLocation.Y -= upmosty - closest;
                    item.StartLocation.Y -= upmosty - closest;
                    Indications[HighlightedItems[i]] = item;
                }
            else
                for (int i = 0; i < HighlightedItems.Count; i++)
                {
                    MapObj item = Indications[HighlightedItems[i]];
                    item.EndLocation.Y -= dist;
                    item.StartLocation.Y -= dist;
                    Indications[HighlightedItems[i]] = item;
                }
            Invalidate();
        }

        private void toolStripButton54_Click(object sender, EventArgs e)
        {
            MoveDn(1);
        }

        private void MoveDn(int dist)
        {
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                MapObj item = Indications[HighlightedItems[i]];
                item.EndLocation.Y += dist;
                item.StartLocation.Y += dist;
                Indications[HighlightedItems[i]] = item;
            }
            Invalidate();
        }

        private void MoveNextDn(int dist)
        {
            int upmosty = 10000;
            int closest = 10000;
            int y = 0;
            bool highlightedlist = false;
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                y = Indications[HighlightedItems[i]].StartLocation.Y;
                if (upmosty > y)
                    upmosty = y;
            }
            for (int i = 0; i < Indications.Count; i++)
            {
                y = Indications[i].StartLocation.Y;
                highlightedlist = false;
                for (int j = 0; j < HighlightedItems.Count; j++)
                    if (HighlightedItems[j] == i)
                        highlightedlist = true;
                if (!highlightedlist)
                    if (y > upmosty)
                        if (y < closest)
                            closest = y;
            }
            if (closest != 10000)
                for (int i = 0; i < HighlightedItems.Count; i++)
                {
                    MapObj item = Indications[HighlightedItems[i]];
                    item.EndLocation.Y -= upmosty - closest;
                    item.StartLocation.Y -= upmosty - closest;
                    Indications[HighlightedItems[i]] = item;
                }
            else
                for (int i = 0; i < HighlightedItems.Count; i++)
                {
                    MapObj item = Indications[HighlightedItems[i]];
                    item.EndLocation.Y += dist;
                    item.StartLocation.Y += dist;
                    Indications[HighlightedItems[i]] = item;
                }
            Invalidate();
        }


        private void toolStripButton55_Click(object sender, EventArgs e)
        {
            MoveLeft(1);
        }

        private void MoveLeft(int dist)
        {
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                MapObj item = Indications[HighlightedItems[i]];
                item.EndLocation.X -= dist;
                item.StartLocation.X -= dist;
                Indications[HighlightedItems[i]] = item;
            }  
            Invalidate();
        }


        private void MoveNextLeft(int dist)
        {
            int leftmostx = 10000;
            int closest = -1;
            int x = 0;
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                x = Indications[HighlightedItems[i]].StartLocation.X;
                if (leftmostx > x)
                   leftmostx = x;
            }
            for (int i = 0; i < Indications.Count; i++)
            {
                x = Indications[i].StartLocation.X;
                if (x < leftmostx)
                   if (x > closest)
                        closest = x;
            }
            if (closest != -1)
                for (int i = 0; i < HighlightedItems.Count; i++)
                {
                    MapObj item = Indications[HighlightedItems[i]];
                    item.EndLocation.X -= leftmostx - closest;
                    item.StartLocation.X -= leftmostx - closest;
                    Indications[HighlightedItems[i]] = item;
                }
            else
                for (int i = 0; i < HighlightedItems.Count; i++)
                {
                    MapObj item = Indications[HighlightedItems[i]];
                    item.EndLocation.X -= dist;
                    item.StartLocation.X -= dist;
                    Indications[HighlightedItems[i]] = item;
                }
            Invalidate();
        }

        private void toolStripButton56_Click(object sender, EventArgs e)
        {
            MoveRight(1);
        }

        private void MoveRight(int dist)
        {
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                MapObj item = Indications[HighlightedItems[i]];
                item.EndLocation.X += dist;
                item.StartLocation.X += dist;
                Indications[HighlightedItems[i]] = item;
            }
            Invalidate();
        }

        private void MoveNextRight(int dist)
        {
            int leftmostx = 10000;
            int closest = 10000;
            int x = 0;
            bool highlightedlist = false;
            for (int i = 0; i < HighlightedItems.Count; i++)
            {
                x = Indications[HighlightedItems[i]].StartLocation.X;
                if (leftmostx > x)
                    leftmostx = x;
            }
            for (int i = 0; i < Indications.Count; i++)
            {
                x = Indications[i].StartLocation.X;
                highlightedlist = false;
                for (int j = 0; j < HighlightedItems.Count; j++)
                    if (HighlightedItems[j] == i)
                        highlightedlist = true; 
                if(!highlightedlist)
                    if (x > leftmostx)
                        if (x < closest)
                            closest = x;
            }
            if (closest != 10000)
                for (int i = 0; i < HighlightedItems.Count; i++)
                {
                    MapObj item = Indications[HighlightedItems[i]];
                    item.EndLocation.X -= leftmostx - closest;
                    item.StartLocation.X -= leftmostx - closest;
                    Indications[HighlightedItems[i]] = item;
                }
            else
                for (int i = 0; i < HighlightedItems.Count; i++)
                {
                    MapObj item = Indications[HighlightedItems[i]];
                    item.EndLocation.X += dist;
                    item.StartLocation.X += dist;
                    Indications[HighlightedItems[i]] = item;
                }
            Invalidate();
        }

        /*
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.A))
            {
                MessageBox.Show("You pressed Ctrl+A!");
            }
        }*/

        private void frmMChild_SimMap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 'c') || (e.KeyChar == 'C'))
            {
                Copy();
                e.Handled = true;
            }
            if ((e.KeyChar == 'x') || (e.KeyChar == 'X'))
            {                
                Cut();
                e.Handled = true;
            }
            if ((e.KeyChar == 'v') || (e.KeyChar == 'V'))
            {
                Paste(0);
                e.Handled = true;
            }
            if ((e.KeyChar == 'o') || (e.KeyChar == 'o'))
            {
                CircularizeBy(float.Parse(toolStripTextBox10.Text), float.Parse(toolStripTextBox9.Text));
                e.Handled = true;
            }
            if (e.KeyChar == '/')// && (e.Modifiers == Keys.Control))
            {
                Spoke(float.Parse(toolStripTextBox12.Text), float.Parse(toolStripTextBox11.Text));
                e.Handled = true;
            }
            if (e.KeyChar == 'd')// && (e.Modifiers == Keys.Control))
            {
                deleteSelectedItems();
                e.Handled = true;
            }
            /*if (e.KeyChar == '+')// && (e.Modifiers == Keys.Control))
            {
                applyzoom(1.25f, mouselocation.X, mouselocation.Y);
                e.Handled = true;
            }
            if (e.KeyChar == '-')// && (e.Modifiers == Keys.Control))
            {
                applyzoom(0.8f, mouselocation.X, mouselocation.Y);
                e.Handled = true;
            }*/
            if (e.KeyChar == '.')
            {
                rotateparameter(-1);
                e.Handled = true;
            }
            if (e.KeyChar == ',')
            {
                rotateparameter(1);
                e.Handled = true;
            }
  
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Left) return true;
            if (keyData == Keys.Right) return true;
            if (keyData == Keys.Down) return true;
            if (keyData == Keys.Up) return true;   
            else return base.IsInputKey(keyData);
            
        }

        private void frmMChild_SimMap_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control && e.KeyCode == Keys.A)
            {
                SelectAll();
            }
            if (e.Control && e.KeyCode == Keys.Y)
            {
                redo();
            }
            if (e.Control && e.KeyCode == Keys.Z)
            {
                undo();
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                Copy();
            }
            if (e.Control && e.KeyCode == Keys.X)
            {
                Cut();
            }
            if (e.Control && e.KeyCode == Keys.V)
            {
                Paste(0);
            }
            if (e.KeyCode == Keys.F2)
            {
                simspeedcommand = "SaveMap" + this.Text;
            }
            if (e.KeyCode == Keys.F5)
            {
                Invalidate();
            }
            if (e.Control && e.KeyCode == Keys.OemPeriod)
            {
                rotateparameter(-15);
            }
            if (e.Control && e.KeyCode == Keys.Oemcomma)
            {
                rotateparameter(15);
            }
            if (e.Control && e.KeyCode == Keys.Oemplus)
            {
                applyzoom(1.25f, -1, -1);
            }
            if (e.Control && e.KeyCode == Keys.OemMinus)
            {
                applyzoom(0.8f, -1, -1);
            }

            if (e.Control && e.KeyCode == Keys.T)
            {
                if (Thumbtack.Checked) Thumbtack.Checked = false;
                else Thumbtack.Checked = true;
            }

            if (e.KeyCode == Keys.Delete)
            {
                deleteSelectedItems();
                e.Handled = true;
            }


            if ((e.KeyCode == Keys.W) && (e.Modifiers == Keys.Control) && (e.Modifiers != Keys.Shift) && (e.Modifiers != Keys.Alt))
            {
                fitscreen(PORTRAITLANDSCAPE);
                e.Handled = true;
            }

            if (!SimMode.Checked)
            {
                // Move 1
                if ((e.KeyCode == Keys.Left) && (e.Modifiers != Keys.Control) && (e.Modifiers != Keys.Shift) && (e.Modifiers != Keys.Alt))
                {
                    MoveLeft(1);
                    e.Handled = true;
                }
                if ((e.KeyCode == Keys.Right) && (e.Modifiers != Keys.Control) && (e.Modifiers != Keys.Shift) && (e.Modifiers != Keys.Alt))
                {
                    MoveRight(1);
                    e.Handled = true;
                }
                if ((e.KeyCode == Keys.Down) && (e.Modifiers != Keys.Control) && (e.Modifiers != Keys.Shift) && (e.Modifiers != Keys.Alt))
                {
                    MoveDn(1);
                    e.Handled = true;
                }
                if ((e.KeyCode == Keys.Up) && (e.Modifiers != Keys.Control) && (e.Modifiers != Keys.Shift) && (e.Modifiers != Keys.Alt))
                {
                    MoveUp(1);
                    e.Handled = true;
                }

                if ((e.KeyCode == Keys.Right) && (e.Modifiers == Keys.Alt) && (e.Modifiers != Keys.Control) && (e.Modifiers != Keys.Shift))
                {
                    ExtendRight(1);
                    e.Handled = true;
                }
                if ((e.KeyCode == Keys.Left) && (e.Modifiers == Keys.Alt) && (e.Modifiers != Keys.Control) && (e.Modifiers != Keys.Shift))
                {
                    ContractRight(1);
                    e.Handled = true;
                }
                if ((e.KeyCode == Keys.Down) && (e.Modifiers == Keys.Alt) && (e.Modifiers != Keys.Control) && (e.Modifiers != Keys.Shift))
                {
                    ExtendDown(1);
                    e.Handled = true;
                }
                if ((e.KeyCode == Keys.Up) && (e.Modifiers == Keys.Alt) && (e.Modifiers != Keys.Control) && (e.Modifiers != Keys.Shift))
                {
                    ContractDown(1);
                    e.Handled = true;
                }

                if ((e.KeyCode == Keys.Right) && (e.Modifiers == Keys.Alt) && (e.Modifiers == Keys.Control) && (e.Modifiers != Keys.Shift))
                {
                    ExtendRight(10);
                    e.Handled = true;
                }
                if ((e.KeyCode == Keys.Left) && (e.Modifiers == Keys.Alt) && (e.Modifiers == Keys.Control) && (e.Modifiers != Keys.Shift))
                {
                    ContractRight(10);
                    e.Handled = true;
                }
                if ((e.KeyCode == Keys.Down) && (e.Modifiers == Keys.Alt) && (e.Modifiers == Keys.Control) && (e.Modifiers != Keys.Shift))
                {
                    ExtendDown(10);
                    e.Handled = true;
                }
                if ((e.KeyCode == Keys.Up) && (e.Modifiers == Keys.Alt) && (e.Modifiers == Keys.Control) && (e.Modifiers != Keys.Shift))
                {
                    ContractDown(10);
                    e.Handled = true;
                }

                if ((e.KeyCode == Keys.Add) && (e.Modifiers != Keys.Alt) && (e.Modifiers == Keys.Control) && (e.Modifiers != Keys.Shift))
                {
                    applyzoom(1.25f, -1, -1);                    
                    e.Handled = true;
                }
                if ((e.KeyCode == Keys.Subtract) && (e.Modifiers != Keys.Alt) && (e.Modifiers == Keys.Control) && (e.Modifiers != Keys.Shift))
                {
                    applyzoom(0.8f, -1, -1);
                    e.Handled = true;
                }


                /*
                // Extend 1
                if ((e.KeyCode == Keys.Left) && (e.Modifiers != Keys.Control) && (e.Modifiers == Keys.ShiftKey))
                {
                    ExtendRight(); 
                    e.Handled = true;
                }
                if ((e.KeyCode == Keys.Right) && (e.Modifiers != Keys.Control) && (e.Modifiers == Keys.ShiftKey))
                {
                    ContractRight();
                    e.Handled = true;
                }
                if ((e.KeyCode == Keys.Down) && (e.Modifiers != Keys.Control) && (e.Modifiers == Keys.ShiftKey))
                {
                    ExtendDown();
                    e.Handled = true;
                }
                if ((e.KeyCode == Keys.Up) && (e.Modifiers != Keys.Control) && (e.Modifiers == Keys.ShiftKey))
                {
                    ContractDown();
                    e.Handled = true;
                }
                */

                // Move to next object
                if ((e.KeyCode == Keys.Left) && (e.Modifiers == Keys.Control) && (e.Modifiers != Keys.Shift))
                {
                    MoveNextLeft(10);
                    e.Handled = true;
                }
                if ((e.KeyCode == Keys.Right) && (e.Modifiers == Keys.Control) && (e.Modifiers != Keys.Shift))
                {
                    MoveNextRight(10);
                    e.Handled = true;
                }
                if ((e.KeyCode == Keys.Down) && (e.Modifiers == Keys.Control) && (e.Modifiers != Keys.Shift))
                {
                    MoveNextDn(10);
                    e.Handled = true;
                }
                if ((e.KeyCode == Keys.Up) && (e.Modifiers == Keys.Control) && (e.Modifiers != Keys.Shift))
                {
                    MoveNextUp(10);
                    e.Handled = true;
                }
            }
            else
            {
                if ((e.KeyCode == Keys.Left) && (e.Modifiers != Keys.Control))
                {
                    Speed.Text = ((int)(float.Parse(Speed.Text) / 2)).ToString();
                    if (Speed.Text == "0")
                        Speed.Text = "1";
                    simspeedcommand = "Speed:" + Speed.Text;
                    setsimspeedcolor((int)(float.Parse(Speed.Text)));
                }
                if ((e.KeyCode == Keys.Right) && (e.Modifiers != Keys.Control))
                {
                    Speed.Text = ((int)(float.Parse(Speed.Text) * 2)).ToString();
                    simspeedcommand = "Speed:" + Speed.Text;
                    setsimspeedcolor((int)(float.Parse(Speed.Text)));
                }
                if (e.Control && e.KeyCode == Keys.L)
                    simspeedcommand = "SaveLogicState";
                if (e.Control && e.KeyCode == Keys.K)
                    simspeedcommand = "LoadLogicState";
            }
            if (e.KeyCode == Keys.PageDown)
            {
                SendObjectstoBack();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.PageUp)
            {
                BringObjectstoFront();
                e.Handled = true;
            }

        }



        private TextBox textBox14;
        private TextBox textBox15;
        private TextBox textBox16;



        private ToolStripMenuItem degClockwiseToolStripMenuItem;
        private ToolStripMenuItem degCounterClockwiseToolStripMenuItem;

        private void degClockwiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RotateC();
        }

        private void degCounterClockwiseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            RotateAC();
        }

        private ToolStripButton toolStripButton57;

        private void toolStripButton57_Click(object sender, EventArgs e)
        {
            try
            {
                float amount = float.Parse(toolStripTextBox2.Text);
                //RotateSelectionBy(float.Parse(toolStripTextBox2.Text));
            }
            catch
            {
                MessageBox.Show("Error reading Scale: " + toolStripTextBox2.Text, "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private ToolStrip toolStrip4;
        private ToolStripButton toolStripButton59;
        private ToolStripTextBox toolStripTextBox9;
        private ToolStripTextBox toolStripTextBox10;
        private ToolStripButton toolStripButton60;

        private void toolStripButton58_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton59_Click(object sender, EventArgs e)
        {
            try
            {
                CircularizeBy(float.Parse(toolStripTextBox10.Text),float.Parse(toolStripTextBox9.Text));
            }
            catch
            {
                MessageBox.Show("Error reading Scale: " + toolStripTextBox2.Text, "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void toolStripButton60_Click(object sender, EventArgs e)
        {
            try
            {
                Spoke(float.Parse(toolStripTextBox12.Text), float.Parse(toolStripTextBox11.Text));
            }
            catch
            {
                MessageBox.Show("Error reading Scale: " + toolStripTextBox2.Text, "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private ToolStripTextBox toolStripTextBox12;
        private ToolStripTextBox toolStripTextBox11;

        private void frmMChild_SimMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) || panInArrowMode)
            {
                if(highlighteditem == Indications.Count-1)
                    SendObjecttoBack(); 
                else BringObjecttoFront();
            }
        }

        private void SearchbyString_Click(object sender, EventArgs e)
        {

        }

        private void SearchbyString_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void SearchbyString_KeyUp(object sender, KeyEventArgs e)
        {
            searchbystring();
        }
        private TextBox textBox17;
        private TextBox textBox18;
        private TextBox textBox19;
        private Label label18;
        private Label label19;
        private Label label20;

        private void toolStripButton58_Click_1(object sender, EventArgs e)
        {
            applyzoom(0.8f, -1, -1);
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private ToolStripButton toolStripButton58;

        private void toolStripButton61_Click(object sender, EventArgs e)
        {
            simspeedcommand = "LoadLogicState";
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {

        }
        private ToolStripButton toolStripButton61;

        private void toolStripButton15_Click_1(object sender, EventArgs e)
        {
            Speed.Text = ((int)(float.Parse(Speed.Text) * 100)).ToString();
            simspeedcommand = "Speed:" + Speed.Text;
            setsimspeedcolor((int)(float.Parse(Speed.Text)));
        }

        private ToolStripTextBox Speed;

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            Speed.Text = ((int)(float.Parse(Speed.Text) * 2)).ToString();
            simspeedcommand = "Speed:" + Speed.Text;
            setsimspeedcolor((int)(float.Parse(Speed.Text)));
        }

        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton17;
        private ToolStripButton toolStripButton16;

        private void toolStripButton17_Click_1(object sender, EventArgs e)
        {
            Speed.Text = ((int)(float.Parse(Speed.Text) /100)).ToString();
            if (Speed.Text == "0")
                Speed.Text = "1";
            simspeedcommand = "Speed:" + Speed.Text;
            setsimspeedcolor((int)(float.Parse(Speed.Text)));
        }

        private void toolStripButton11_Click_1(object sender, EventArgs e)
        {
            Speed.Text = ((int)(float.Parse(Speed.Text) / 2)).ToString();
            if (Speed.Text == "0")
                Speed.Text = "1";
            simspeedcommand = "Speed:" + Speed.Text;
            setsimspeedcolor((int)(float.Parse(Speed.Text)));
        }

        private void toolStripButton16_Click_1(object sender, EventArgs e)
        {
            Speed.Text = ((int)(float.Parse(Speed.Text) / 10)).ToString();
            if (Speed.Text == "0")
                Speed.Text = "1";
            simspeedcommand = "Speed:" + Speed.Text;
            setsimspeedcolor((int)(float.Parse(Speed.Text)));
        }

        private ToolStripButton toolStripButton11;

        private void toolStripButton18_Click_1(object sender, EventArgs e)
        {
            simspeedcommand = "SaveMap" + this.Text;
        }

        private void Thumbtack_Click(object sender, EventArgs e)
        {

        }

        public ToolStripButton Thumbtack;

        private void frmMChild_SimMap_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private ToolStripButton toolStripButton18;
        private ToolStripSeparator toolStripSeparator6;

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private ToolStripSeparator toolStripSeparator5;

        private void Textsizetext_TextChanged(object sender, EventArgs e)
        {
            CommitChange();
        }

        private TextBox Textsizetext;

        private void toolStripButton62_Click(object sender, EventArgs e)
        {
            fitscreen(PORTRAITLANDSCAPE);
        }

        private ToolStripButton toolStripButton62;

        private void toolStripButton63_Click(object sender, EventArgs e)
        {
            fitscreen(PORTRAIT);
        }

        private ToolStripButton toolStripButton63;

        private void toolStripButton64_Click(object sender, EventArgs e)
        {
            fitscreen(LANDSCAPE);
        }

        private void frmMChild_SimMap_Resize(object sender, EventArgs e)
        {
            fitscreen(PORTRAITLANDSCAPE);
        }

        private ToolStripButton toolStripButton64;
    }

    //PushIndications();

    public struct MapObj
    {
        public string TypeofObj; //Label, Indication, ActivateText
        public string Name;
        public string Text;
        public int Textsize;
        public string Indication1;
        public string Indication2;
        public string Indication3;
        public string Indication4;
        public Point StartLocation;
        public Point EndLocation;
        public int RotationAngle;
        public String Shape;
        public Color HighColour;
        public Color IndColour1;
        public Color IndColour2;
        public Color IndColour3;
        public Color IndColour4;
        public Color LowColour;
        public string Control;
        public List<Point> PolygonPoints;
        public bool Transparent;
        public bool IsTrack;
    }

}
