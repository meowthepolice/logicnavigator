using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Media;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

//using System.Data;
using System.Data.OleDb;


namespace Logic_Navigator
{

    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class frmMDIMain : System.Windows.Forms.Form
    {
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem mnuFile;
        private System.Windows.Forms.MenuItem mnuWindow;
        private System.Windows.Forms.MenuItem mnuWindowCascade;
        private System.Windows.Forms.MenuItem mnuWindowTileVertical;
        private System.Windows.Forms.MenuItem mnuWindowTileHorizontal;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem mnuFileExit;
        private System.Windows.Forms.MenuItem mnuHelpAbout;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.ComponentModel.IContainer components;
        private System.IO.StreamReader SR;
        private System.IO.StreamReader SR1;
        private System.Windows.Forms.ListView Rungs;
        private System.Windows.Forms.TreeView treeView;

        /// <summary>
        /// ///////////////////////EXPIRY DATES//////////////////////////////////
        /// </summary>
        private DateTime expiryDate = new DateTime(2130, 12, 1, 0, 0, 0);
        private DateTime warningDate = new DateTime(2115, 12, 1, 0, 0, 0);
        /// ///////////////////////EXPIRY DATES//////////////////////////////////

        //private int FileChar;
        public string contactRequest = "";
        public string oldContactRequest = "";
        string rungName = "";
        private bool reloading = false;
        //private bool expired = false;
        private bool windowCurrentlyMaximised;
        private Point windowSize;
        private string fileType = "NIL";
        private float scaleFactor = 0.75F;
        private bool openoldmap = false;
        Color HighColor = Color.Blue;
        Color LowColor = Color.Red;

        //Font drawFont = new Font("Tahoma", (float) cellWidth/9);
        private Font drawFont = new Font("Tahoma", 11, FontStyle.Regular);//new Font("Tahoma", 9);
        private bool gridlines = false;
        private bool showTimers = true;
        private ArrayList S2PTimersTiming = new ArrayList();
        private ArrayList S2DTimersTiming = new ArrayList();
        private float simspeed = 1.0F;
        private bool sound = false;
        /*private SoundPlayer MyUpSoundPlayer = new SoundPlayer();
        private SoundPlayer MyDownSoundPlayer = new SoundPlayer();
        private SoundPlayer MyTimerStartUpSoundPlayer = new SoundPlayer();
        private SoundPlayer MyTimerStartDownSoundPlayer = new SoundPlayer();

        private SoundPlayer SoundOne = new SoundPlayer();
        private SoundPlayer SoundTwo = new SoundPlayer();
        private SoundPlayer SoundThree = new SoundPlayer();
        private SoundPlayer SoundFour = new SoundPlayer();
        private SoundPlayer SoundFive = new SoundPlayer();
        private SoundPlayer SoundSix = new SoundPlayer();
        private SoundPlayer SoundSeven = new SoundPlayer();
        private SoundPlayer SoundEight = new SoundPlayer();
        private SoundPlayer SoundNine = new SoundPlayer();
        private SoundPlayer SoundTen = new SoundPlayer();
        private SoundPlayer SoundEleven = new SoundPlayer();
        private SoundPlayer SoundTwelve = new SoundPlayer();
        private SoundPlayer SoundThirteen = new SoundPlayer();
        private SoundPlayer SoundFourteen = new SoundPlayer();
        private SoundPlayer SoundFifeteen = new SoundPlayer();
        private SoundPlayer SoundSixteen = new SoundPlayer();
        private SoundPlayer SoundSeventeen = new SoundPlayer();
        private SoundPlayer SoundEighteen = new SoundPlayer();
        private SoundPlayer SoundNineteen = new SoundPlayer();
        private SoundPlayer SoundTwenty = new SoundPlayer();
        private SoundPlayer SoundThirty = new SoundPlayer();
        private SoundPlayer SoundForty = new SoundPlayer();
        private SoundPlayer SoundFifty = new SoundPlayer();
        private SoundPlayer SoundSixty = new SoundPlayer();
        private SoundPlayer SoundSeventy = new SoundPlayer();
        private SoundPlayer SoundEighty = new SoundPlayer();
        private SoundPlayer SoundNinety = new SoundPlayer();
        private SoundPlayer SoundHundred = new SoundPlayer();
        private SoundPlayer SoundTwoHundred = new SoundPlayer();
        private SoundPlayer SoundThreeHundred = new SoundPlayer();
        private SoundPlayer SoundA = new SoundPlayer();
        private SoundPlayer SoundB = new SoundPlayer();
        private SoundPlayer SoundC = new SoundPlayer();
        private SoundPlayer SoundD = new SoundPlayer();
        private SoundPlayer SoundE = new SoundPlayer();
        private SoundPlayer SoundF = new SoundPlayer();
        private SoundPlayer SoundG = new SoundPlayer();
        private SoundPlayer SoundH = new SoundPlayer();
        private SoundPlayer SoundI = new SoundPlayer();
        private SoundPlayer SoundJ = new SoundPlayer();
        private SoundPlayer SoundK = new SoundPlayer();
        private SoundPlayer SoundL = new SoundPlayer();
        private SoundPlayer SoundM = new SoundPlayer();
        private SoundPlayer SoundN = new SoundPlayer();
        private SoundPlayer SoundO = new SoundPlayer();
        private SoundPlayer SoundP = new SoundPlayer();
        private SoundPlayer SoundQ = new SoundPlayer();
        private SoundPlayer SoundR = new SoundPlayer();
        private SoundPlayer SoundS = new SoundPlayer();
        private SoundPlayer SoundT = new SoundPlayer();
        private SoundPlayer SoundU = new SoundPlayer();
        private SoundPlayer SoundV = new SoundPlayer();
        private SoundPlayer SoundW = new SoundPlayer();
        private SoundPlayer SoundX = new SoundPlayer();
        private SoundPlayer SoundY = new SoundPlayer();
        private SoundPlayer SoundZ = new SoundPlayer();
        private SoundPlayer SoundHigh = new SoundPlayer();
        private SoundPlayer SoundLow = new SoundPlayer();*/

        public List<string> SoundQueue = new List<string>();
        int queuemarker = 0;


        //private bool playstuff = false;


        public bool showEvaluationSequence = false;

        StringComparison sc = StringComparison.OrdinalIgnoreCase;

        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.MenuItem menuItem2;
        private string toolbarState = "Maximise";


        private List<MapObj> mapObjects = new List<MapObj>();

        ArrayList interlockingOld = new ArrayList();
        ArrayList interlockingNew = new ArrayList();
        ArrayList siminterlocking = new ArrayList();
        ArrayList Housings_Old = new ArrayList();
        ArrayList Housings_New = new ArrayList();
        ArrayList versionRecOld = new ArrayList();
        ArrayList versionRecNew = new ArrayList();
        ArrayList timersOld = new ArrayList();
        ArrayList timersNew = new ArrayList();
        ArrayList contactAnalysis = new ArrayList();
        CultureInfo ci = new CultureInfo("en-US");
        private string installationNameOld = "";
        private string installationNameNew = "";
        private string GCSSVersionNew = "";
        private string GCSSVersionOld = "";


        private List<string> objects = new List<string>();
        private int RELAYCALC = 0;
        private int WESTRACECALC = 1;
        private int ticker = 0;
        private int cycletimespeed = 250;

        private int calcmethod = 1;

        ArrayList interlockingTAL = new ArrayList(); //TAL = Turn Around Logic
        ArrayList timersTAL = new ArrayList(); //TAL = Turn Around Logic
        string duplicates = "";
        public ArrayList SimInputs = new ArrayList();
        public ArrayList HighRungs = new ArrayList();
        public List<List<Contact>> sim = new List<List<Contact>>();
        public List<string> Coilnames = new List<string>();
        public List<int> CoilIsTimer = new List<int>();
        public List<bool> Coilstates = new List<bool>();
        public List<bool> Coildrive = new List<bool>();//for Slow to pick timers
        public List<bool> Coilnodrive = new List<bool>();//for slow to drop timers
        public int workload = 0;
        public string runglisting = "";
        public Contact globalcontact = new Contact();
        public bool[,] voltagematrix = new bool[64, 64];
        ArrayList voltagematrixes = new ArrayList();
        private string ncdcomment = "";



        //Serial Port

        bool[] controlswitch = { false, false, false, false,
                                 false, false, false, false,
                                 false, false, false, false,
                                 false, false, false, false,

                                 false, false, false, false,
                                 false, false, false, false,
                                 false, false, false, false,
                                 false, false, false, false };

        bool[] controlswitchnew = { false, false, false, false,
                                 false, false, false, false,
                                 false, false, false, false,
                                 false, false, false, false,

                                 false, false, false, false,
                                 false, false, false, false,
                                 false, false, false, false,
                                 false, false, false, false };


        bool serialworking = false;


        public List<string> Inputnames = new List<string>();
        public List<bool> Inputstates = new List<bool>();
        public List<bool> InputstatesPrev = new List<bool>();
              

        public List<string> ExclusionList = new List<string>();

        public List<Point> CoilContactrefs = new List<Point>();
        
        public List<String> forceLow = new List<String>();
        public List<String> forceHigh = new List<String>();
        public List<List<int>> depbookCoils = new List<List<int>>();
        public List<List<int>> depbookInputs = new List<List<int>>();

        public List<bool> evaluationlist = new List<bool>();

        //public List<string> depbookInputNames = new List<string>();
        //public List<bool> inputstates = new List<bool>();


        //private Coilcoordsx  
        private int rungBeingEvaluated = 0;

        private string debuginfo1 = "";
        private bool TrainBeadDevelopment1 = false;
        private int globalcounter = 0;
        private int rungsevaluated = 0;
        private int searchdepth = 0;

        string HighRungsString = "";
        public ArrayList Changes = new ArrayList();
        public ArrayList ChangesInts = new ArrayList();
        public bool freezeRungStates = false;
        public bool SimMode = false;
        public int windowsopen = 0;
        public bool processorbusy = false;
        public int processorhits = 0;

        DateTime PreviousEvaluation;
        DateTime BeginEvaluation;
        DateTime EndEvaluation;
        DateTime BeginScan;
        DateTime EndScan;
        DateTime BeginBroadcast;
        DateTime EndBroadcast;


        private int widthBetweenColumns = 23;
        private int columns = 2;
        private int linespergrouping = 4;
        private bool spaceBetweenGroupings = true;
        private bool indentationOnFirstRow = true;


        private System.Data.DataSet myDataSet;
        private DataGridTableStyle DGStyle;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.TextBox NewFileName;
        private System.Windows.Forms.TextBox OldFileName;
        private System.Windows.Forms.TextBox FileName;
        private System.Windows.Forms.MenuItem mnuFileCloseChild;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem CloseAll;
        private System.Windows.Forms.MenuItem Tools;
        private System.Windows.Forms.MenuItem View;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.DataGrid RungGrid;
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.MenuItem menuItem14;
        private System.Windows.Forms.MenuItem menuItem15;
        private System.Windows.Forms.MenuItem menuItem16;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolBarButton Search;
        private System.Windows.Forms.ToolBarButton OpenNew;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.ToolBarButton OpenOld;
        private System.Windows.Forms.ToolBarButton CompareUniqueRungs;
        private System.Windows.Forms.ToolBarButton ShowRungs;
        private System.Windows.Forms.ToolBarButton ShowHousing;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolBarButton Separator;
        private System.Windows.Forms.MenuItem menuItem17;
        private System.Windows.Forms.ToolBarButton CloseAllRungs;
        private System.Windows.Forms.ToolBarButton CloseAllFiles;
        private System.Windows.Forms.ToolBarButton ShowHorizontal;
        private System.Windows.Forms.ToolBarButton Separator1;
        private System.Windows.Forms.ToolBarButton Separator2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuItem menuItem18;
        private System.Windows.Forms.MenuItem menuItem19;
        private System.Windows.Forms.MenuItem menuItem13;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.ToolBarButton ExportToText;
        private System.Windows.Forms.ToolBarButton ZoomIn;
        private System.Windows.Forms.ToolBarButton ZoomOut;
        private System.Windows.Forms.MenuItem menuItem11;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.MenuItem menuItem21;
        private System.Windows.Forms.TextBox Arg1;
        private System.Windows.Forms.TextBox Arg2;
        private System.Windows.Forms.TextBox Arg3;
        private System.Windows.Forms.TextBox Dialog;
        private MenuItem menuItem22;
        private MenuItem menuItem23;
        private MenuItem menuItem24;
        private MenuItem menuItem25;
        private MenuItem menuItem26;
        private MenuItem menuItem27;
        private MenuItem menuItem28;
        private MenuItem menuItem29;
        private MenuItem menuItem30;
        private MenuItem menuItem31;
        private MenuItem menuItem7;
        private MenuItem menuItem34;
        private Timer Simulationtimer;
        private MenuItem menuItem35;
        private MenuItem menuItem36;
        private MenuItem menuItem37;
        private MenuItem menuItem40;
        private MenuItem menuItem41;
        private SaveFileDialog saveSt8FileDialog;
        private MenuItem menuItem42;
        private OpenFileDialog openst8FileDialog;
        private MenuItem menuItem44;
        private MenuItem menuItem45;
        private MenuItem menuItem46;
        private MenuItem menuItem48;
        private MenuItem menuItem47;
        private TextBox ProcessorHits;
        private MenuItem menuItem49;
        private MenuItem menuItem51;
        private MenuItem menuItem52;
        private OpenFileDialog openLayoutFileDialog;
        private SaveFileDialog saveLayoutFileDialog;
        private MenuItem menuItem53;
        private TextBox TALFileName;
        private MenuItem menuItem38;
        private MenuItem menuItem32;
        private MenuItem menuItem33;
        private MenuItem menuItem43;
        private MenuItem menuItem55;
        private MenuItem menuItem54;
        private BackgroundWorker backgroundWorker1;
        private MenuItem menuItem50;
        private MenuItem menuItem57;
        private MenuItem menuItem58;
        private SaveFileDialog saveFileDialog3;
        private MenuItem menuItem39;
        private MenuItem menuItem59;
        private OpenFileDialog openFileDialog3;
        private TextBox textBox3;
        private MenuItem menuItem61;
        private MenuItem menuItem62;
        private MenuItem menuItem64;
        private TextBox LinesPerGroup;
        private TextBox columnsoutput;
        private TextBox Indentations;
        private TextBox spacesbetweencolumns;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox SpaceperGrouping;
        private Label label5;
        private Label label6;
        private MenuItem menuItem65;
        private MenuItem menuItem66;
        private TextBox CurrentLayout;
        private TextBox CurrentMap1;
        private TextBox CurrentState;
        private TextBox CurrentTALFile;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private SaveFileDialog saveProjectFileDialog;
        private OpenFileDialog openProjectFileDialog;
        private MenuItem menuItem20;
        private MenuItem menuItem68;
        private MenuItem menuItem63;
        private MenuItem menuItem70;
        private MenuItem menuItem71;
        private MenuItem menuItem72;
        private TextBox CurrentMap5;
        private TextBox CurrentMap3;
        private TextBox CurrentMap2;
        private TextBox CurrentMap4;
        private MenuItem menuItem73;
        private MenuItem menuItem56;
        private MenuItem menuItem74;
        private TextBox ProjectDirectory;
        private Label label11;
        private MenuItem menuItem75;
        private MenuItem menuItem76;
        private TextBox NewFileText1;
        private TextBox OldFileText1;
        private Label NewFileText;
        private Label OldFileText;
        private MenuItem menuItem77;
        private MenuItem menuItem79;
        private MenuItem menuItem81;
        private MenuItem menuItem80;
        private MenuItem menuItem82;
        private MenuItem menuItem83;
        private MenuItem menuItem84;
        private MenuItem menuItem85;
        private MenuItem menuItem86;
        private Label label12;
        private TextBox ImageDirectory;
        private TextBox VoltageMatrixText;
        private TextBox result;
        private MenuItem menuItem87;
        private MenuItem menuItem88;
        private MenuItem menuItem89;
        private MenuItem menuItem90;
        private MenuItem menuItem91;
        private MenuItem menuItem92;
        private MenuItem menuItem93;
        private MenuItem menuItem94;
        private MenuItem menuItem95;
        private MenuItem menuItem96;
        private MenuItem menuItem97;
        private ToolBarButton OpenProject;
        private StatusStrip statusStrip1;
        private ToolBarButton Open;
        private ToolBarButton ProjectSave;
        private MenuItem menuItem100;
        private MenuItem menuItem101;
        private MenuItem menuItem102;
        private MenuItem menuItem103;
        private ToolBarButton Question;
        private MenuItem menuItem98;
        private MenuItem menuItem99;
        private MenuItem menuItem104;
        private MenuItem menuItem105;
        private MenuItem menuItem78;
        private TextBox CurrentMap9;
        private TextBox CurrentMap7;
        private TextBox CurrentMap8;
        private TextBox CurrentMap10;
        private TextBox CurrentMap6;
        private System.IO.Ports.SerialPort serialPort1;
        private TextBox SendMessages;
        private TextBox textBox1;
        private MenuItem menuItem67;
        private MenuItem menuItem106;
        private MenuItem menuItem107;
        private MenuItem menuItem60;
        private MenuItem menuItem108;
        private Label label13;
        private Label label14;
        private MenuItem menuItem109;
        private MenuItem menuItem111;
        private MenuItem menuItem114;
        private MenuItem menuItem116;
        private MenuItem menuItem69;
        private MenuItem menuItem110;
        private TextBox TAL2FileName;
        private TextBox TAL3FileName;
        private TextBox TAL4FileName;
        private TextBox prefixt4;
        private TextBox prefixt3;
        private TextBox prefixt2;
        private TextBox prefixt1;
        private TextBox prefixmain;
        private MenuItem menuItem112;
        private MenuItem menuItem113;
        private MenuItem menuItem115;
        private MenuItem menuItem117;
        private MenuItem menuItem118;
        private MenuItem menuItem119;
        private MenuItem menuItem120;
        private MenuItem menuItem121;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MenuItem menuItem6;

        public frmMDIMain()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();



            //
            // TODO: Add any constructor code after InitializeComponent call
            //
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMDIMain));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuFile = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem71 = new System.Windows.Forms.MenuItem();
            this.menuItem72 = new System.Windows.Forms.MenuItem();
            this.menuItem73 = new System.Windows.Forms.MenuItem();
            this.mnuFileCloseChild = new System.Windows.Forms.MenuItem();
            this.CloseAll = new System.Windows.Forms.MenuItem();
            this.mnuFileExit = new System.Windows.Forms.MenuItem();
            this.View = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem23 = new System.Windows.Forms.MenuItem();
            this.menuItem24 = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.menuItem26 = new System.Windows.Forms.MenuItem();
            this.menuItem27 = new System.Windows.Forms.MenuItem();
            this.menuItem28 = new System.Windows.Forms.MenuItem();
            this.menuItem29 = new System.Windows.Forms.MenuItem();
            this.menuItem30 = new System.Windows.Forms.MenuItem();
            this.menuItem31 = new System.Windows.Forms.MenuItem();
            this.Tools = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem63 = new System.Windows.Forms.MenuItem();
            this.menuItem81 = new System.Windows.Forms.MenuItem();
            this.menuItem82 = new System.Windows.Forms.MenuItem();
            this.menuItem83 = new System.Windows.Forms.MenuItem();
            this.menuItem114 = new System.Windows.Forms.MenuItem();
            this.menuItem111 = new System.Windows.Forms.MenuItem();
            this.menuItem79 = new System.Windows.Forms.MenuItem();
            this.menuItem116 = new System.Windows.Forms.MenuItem();
            this.menuItem68 = new System.Windows.Forms.MenuItem();
            this.menuItem62 = new System.Windows.Forms.MenuItem();
            this.menuItem61 = new System.Windows.Forms.MenuItem();
            this.menuItem64 = new System.Windows.Forms.MenuItem();
            this.menuItem65 = new System.Windows.Forms.MenuItem();
            this.menuItem112 = new System.Windows.Forms.MenuItem();
            this.menuItem113 = new System.Windows.Forms.MenuItem();
            this.menuItem115 = new System.Windows.Forms.MenuItem();
            this.menuItem35 = new System.Windows.Forms.MenuItem();
            this.menuItem53 = new System.Windows.Forms.MenuItem();
            this.menuItem38 = new System.Windows.Forms.MenuItem();
            this.menuItem36 = new System.Windows.Forms.MenuItem();
            this.menuItem37 = new System.Windows.Forms.MenuItem();
            this.menuItem32 = new System.Windows.Forms.MenuItem();
            this.menuItem41 = new System.Windows.Forms.MenuItem();
            this.menuItem96 = new System.Windows.Forms.MenuItem();
            this.menuItem40 = new System.Windows.Forms.MenuItem();
            this.menuItem33 = new System.Windows.Forms.MenuItem();
            this.menuItem52 = new System.Windows.Forms.MenuItem();
            this.menuItem97 = new System.Windows.Forms.MenuItem();
            this.menuItem51 = new System.Windows.Forms.MenuItem();
            this.menuItem43 = new System.Windows.Forms.MenuItem();
            this.menuItem58 = new System.Windows.Forms.MenuItem();
            this.menuItem56 = new System.Windows.Forms.MenuItem();
            this.menuItem74 = new System.Windows.Forms.MenuItem();
            this.menuItem109 = new System.Windows.Forms.MenuItem();
            this.menuItem59 = new System.Windows.Forms.MenuItem();
            this.menuItem44 = new System.Windows.Forms.MenuItem();
            this.menuItem48 = new System.Windows.Forms.MenuItem();
            this.menuItem46 = new System.Windows.Forms.MenuItem();
            this.menuItem45 = new System.Windows.Forms.MenuItem();
            this.menuItem47 = new System.Windows.Forms.MenuItem();
            this.menuItem117 = new System.Windows.Forms.MenuItem();
            this.menuItem121 = new System.Windows.Forms.MenuItem();
            this.menuItem118 = new System.Windows.Forms.MenuItem();
            this.menuItem119 = new System.Windows.Forms.MenuItem();
            this.menuItem120 = new System.Windows.Forms.MenuItem();
            this.menuItem87 = new System.Windows.Forms.MenuItem();
            this.menuItem88 = new System.Windows.Forms.MenuItem();
            this.menuItem89 = new System.Windows.Forms.MenuItem();
            this.menuItem55 = new System.Windows.Forms.MenuItem();
            this.menuItem34 = new System.Windows.Forms.MenuItem();
            this.menuItem42 = new System.Windows.Forms.MenuItem();
            this.menuItem50 = new System.Windows.Forms.MenuItem();
            this.menuItem54 = new System.Windows.Forms.MenuItem();
            this.menuItem39 = new System.Windows.Forms.MenuItem();
            this.menuItem49 = new System.Windows.Forms.MenuItem();
            this.menuItem57 = new System.Windows.Forms.MenuItem();
            this.menuItem60 = new System.Windows.Forms.MenuItem();
            this.menuItem67 = new System.Windows.Forms.MenuItem();
            this.menuItem108 = new System.Windows.Forms.MenuItem();
            this.menuItem106 = new System.Windows.Forms.MenuItem();
            this.menuItem107 = new System.Windows.Forms.MenuItem();
            this.mnuWindow = new System.Windows.Forms.MenuItem();
            this.mnuWindowCascade = new System.Windows.Forms.MenuItem();
            this.mnuWindowTileVertical = new System.Windows.Forms.MenuItem();
            this.mnuWindowTileHorizontal = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem84 = new System.Windows.Forms.MenuItem();
            this.menuItem85 = new System.Windows.Forms.MenuItem();
            this.menuItem86 = new System.Windows.Forms.MenuItem();
            this.menuItem90 = new System.Windows.Forms.MenuItem();
            this.menuItem91 = new System.Windows.Forms.MenuItem();
            this.menuItem92 = new System.Windows.Forms.MenuItem();
            this.menuItem93 = new System.Windows.Forms.MenuItem();
            this.menuItem94 = new System.Windows.Forms.MenuItem();
            this.menuItem95 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem100 = new System.Windows.Forms.MenuItem();
            this.menuItem103 = new System.Windows.Forms.MenuItem();
            this.menuItem101 = new System.Windows.Forms.MenuItem();
            this.menuItem102 = new System.Windows.Forms.MenuItem();
            this.menuItem104 = new System.Windows.Forms.MenuItem();
            this.menuItem105 = new System.Windows.Forms.MenuItem();
            this.menuItem98 = new System.Windows.Forms.MenuItem();
            this.menuItem99 = new System.Windows.Forms.MenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.MenuItem();
            this.menuItem75 = new System.Windows.Forms.MenuItem();
            this.menuItem76 = new System.Windows.Forms.MenuItem();
            this.menuItem77 = new System.Windows.Forms.MenuItem();
            this.menuItem78 = new System.Windows.Forms.MenuItem();
            this.menuItem80 = new System.Windows.Forms.MenuItem();
            this.menuItem70 = new System.Windows.Forms.MenuItem();
            this.menuItem66 = new System.Windows.Forms.MenuItem();
            this.menuItem69 = new System.Windows.Forms.MenuItem();
            this.menuItem110 = new System.Windows.Forms.MenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.NewFileName = new System.Windows.Forms.TextBox();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.Rungs = new System.Windows.Forms.ListView();
            this.treeView = new System.Windows.Forms.TreeView();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.OldFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.FileName = new System.Windows.Forms.TextBox();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.RungGrid = new System.Windows.Forms.DataGrid();
            this.Search = new System.Windows.Forms.ToolBarButton();
            this.OpenNew = new System.Windows.Forms.ToolBarButton();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.Open = new System.Windows.Forms.ToolBarButton();
            this.OpenOld = new System.Windows.Forms.ToolBarButton();
            this.OpenProject = new System.Windows.Forms.ToolBarButton();
            this.ProjectSave = new System.Windows.Forms.ToolBarButton();
            this.Separator = new System.Windows.Forms.ToolBarButton();
            this.CompareUniqueRungs = new System.Windows.Forms.ToolBarButton();
            this.Separator2 = new System.Windows.Forms.ToolBarButton();
            this.ShowRungs = new System.Windows.Forms.ToolBarButton();
            this.ShowHousing = new System.Windows.Forms.ToolBarButton();
            this.ShowHorizontal = new System.Windows.Forms.ToolBarButton();
            this.ExportToText = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.ZoomIn = new System.Windows.Forms.ToolBarButton();
            this.ZoomOut = new System.Windows.Forms.ToolBarButton();
            this.Separator1 = new System.Windows.Forms.ToolBarButton();
            this.CloseAllRungs = new System.Windows.Forms.ToolBarButton();
            this.CloseAllFiles = new System.Windows.Forms.ToolBarButton();
            this.Question = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.Arg1 = new System.Windows.Forms.TextBox();
            this.Arg2 = new System.Windows.Forms.TextBox();
            this.Arg3 = new System.Windows.Forms.TextBox();
            this.Dialog = new System.Windows.Forms.TextBox();
            this.Simulationtimer = new System.Windows.Forms.Timer(this.components);
            this.saveSt8FileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openst8FileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ProcessorHits = new System.Windows.Forms.TextBox();
            this.openLayoutFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveLayoutFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.TALFileName = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.saveFileDialog3 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog3 = new System.Windows.Forms.OpenFileDialog();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.LinesPerGroup = new System.Windows.Forms.TextBox();
            this.columnsoutput = new System.Windows.Forms.TextBox();
            this.Indentations = new System.Windows.Forms.TextBox();
            this.spacesbetweencolumns = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SpaceperGrouping = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CurrentLayout = new System.Windows.Forms.TextBox();
            this.CurrentMap1 = new System.Windows.Forms.TextBox();
            this.CurrentState = new System.Windows.Forms.TextBox();
            this.CurrentTALFile = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.saveProjectFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openProjectFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.CurrentMap5 = new System.Windows.Forms.TextBox();
            this.CurrentMap3 = new System.Windows.Forms.TextBox();
            this.CurrentMap2 = new System.Windows.Forms.TextBox();
            this.CurrentMap4 = new System.Windows.Forms.TextBox();
            this.ProjectDirectory = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.NewFileText1 = new System.Windows.Forms.TextBox();
            this.OldFileText1 = new System.Windows.Forms.TextBox();
            this.NewFileText = new System.Windows.Forms.Label();
            this.OldFileText = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ImageDirectory = new System.Windows.Forms.TextBox();
            this.VoltageMatrixText = new System.Windows.Forms.TextBox();
            this.result = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.CurrentMap9 = new System.Windows.Forms.TextBox();
            this.CurrentMap7 = new System.Windows.Forms.TextBox();
            this.CurrentMap8 = new System.Windows.Forms.TextBox();
            this.CurrentMap10 = new System.Windows.Forms.TextBox();
            this.CurrentMap6 = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.SendMessages = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.TAL2FileName = new System.Windows.Forms.TextBox();
            this.TAL3FileName = new System.Windows.Forms.TextBox();
            this.TAL4FileName = new System.Windows.Forms.TextBox();
            this.prefixt4 = new System.Windows.Forms.TextBox();
            this.prefixt3 = new System.Windows.Forms.TextBox();
            this.prefixt2 = new System.Windows.Forms.TextBox();
            this.prefixt1 = new System.Windows.Forms.TextBox();
            this.prefixmain = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RungGrid)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile,
            this.View,
            this.Tools,
            this.menuItem35,
            this.mnuWindow,
            this.menuItem5});
            // 
            // mnuFile
            // 
            this.mnuFile.Index = 0;
            this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem4,
            this.menuItem1,
            this.menuItem10,
            this.menuItem71,
            this.menuItem72,
            this.menuItem73,
            this.mnuFileCloseChild,
            this.CloseAll,
            this.mnuFileExit});
            this.mnuFile.Text = "&File";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 0;
            this.menuItem4.Text = "O&pen Old file";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click_1);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.Text = "&Open New file";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 2;
            this.menuItem10.Text = "-";
            // 
            // menuItem71
            // 
            this.menuItem71.Index = 3;
            this.menuItem71.Text = "Open Project";
            this.menuItem71.Click += new System.EventHandler(this.menuItem71_Click);
            // 
            // menuItem72
            // 
            this.menuItem72.Index = 4;
            this.menuItem72.Text = "Save Project";
            this.menuItem72.Click += new System.EventHandler(this.menuItem72_Click);
            // 
            // menuItem73
            // 
            this.menuItem73.Index = 5;
            this.menuItem73.Text = "-";
            // 
            // mnuFileCloseChild
            // 
            this.mnuFileCloseChild.Index = 6;
            this.mnuFileCloseChild.Text = "&Close";
            this.mnuFileCloseChild.Visible = false;
            this.mnuFileCloseChild.Click += new System.EventHandler(this.mnuFileClose_Click);
            // 
            // CloseAll
            // 
            this.CloseAll.Index = 7;
            this.CloseAll.Text = "Close &All";
            this.CloseAll.Visible = false;
            this.CloseAll.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Index = 8;
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // View
            // 
            this.View.Index = 1;
            this.View.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem9,
            this.menuItem8,
            this.menuItem21,
            this.menuItem7,
            this.menuItem3,
            this.menuItem12,
            this.menuItem23,
            this.menuItem26,
            this.menuItem29});
            this.View.Text = "&View";
            this.View.Visible = false;
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 0;
            this.menuItem9.Text = "&Housings";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click_1);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 1;
            this.menuItem8.Text = "&Version History";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // menuItem21
            // 
            this.menuItem21.Index = 2;
            this.menuItem21.Text = "&Latches";
            this.menuItem21.Click += new System.EventHandler(this.menuItem21_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 3;
            this.menuItem7.Text = "-";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 4;
            this.menuItem3.Text = "&Font";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 5;
            this.menuItem12.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem14,
            this.menuItem15});
            this.menuItem12.Text = "Rung View &Options";
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 0;
            this.menuItem14.RadioCheck = true;
            this.menuItem14.Text = "&DataGrid";
            this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 1;
            this.menuItem15.RadioCheck = true;
            this.menuItem15.Text = "&ListBox";
            this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
            // 
            // menuItem23
            // 
            this.menuItem23.Index = 6;
            this.menuItem23.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem24,
            this.menuItem25});
            this.menuItem23.Text = "Grid Lines";
            // 
            // menuItem24
            // 
            this.menuItem24.Index = 0;
            this.menuItem24.Text = "Show";
            this.menuItem24.Click += new System.EventHandler(this.menuItem24_Click);
            // 
            // menuItem25
            // 
            this.menuItem25.Index = 1;
            this.menuItem25.Text = "Hide";
            this.menuItem25.Click += new System.EventHandler(this.menuItem25_Click);
            // 
            // menuItem26
            // 
            this.menuItem26.Index = 7;
            this.menuItem26.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem27,
            this.menuItem28});
            this.menuItem26.Text = "Timers";
            // 
            // menuItem27
            // 
            this.menuItem27.Index = 0;
            this.menuItem27.Text = "Show";
            this.menuItem27.Click += new System.EventHandler(this.menuItem27_Click);
            // 
            // menuItem28
            // 
            this.menuItem28.Index = 1;
            this.menuItem28.Text = "Hide";
            this.menuItem28.Click += new System.EventHandler(this.menuItem28_Click);
            // 
            // menuItem29
            // 
            this.menuItem29.Index = 8;
            this.menuItem29.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem30,
            this.menuItem31});
            this.menuItem29.Text = "Evaluation Sequence";
            // 
            // menuItem30
            // 
            this.menuItem30.Index = 0;
            this.menuItem30.Text = "Show";
            this.menuItem30.Click += new System.EventHandler(this.menuItem30_Click);
            // 
            // menuItem31
            // 
            this.menuItem31.Index = 1;
            this.menuItem31.Text = "Hide";
            this.menuItem31.Click += new System.EventHandler(this.menuItem31_Click);
            // 
            // Tools
            // 
            this.Tools.Index = 2;
            this.Tools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem6,
            this.menuItem22,
            this.menuItem13,
            this.menuItem19,
            this.menuItem18,
            this.menuItem20,
            this.menuItem11,
            this.menuItem63,
            this.menuItem81,
            this.menuItem114,
            this.menuItem116,
            this.menuItem112,
            this.menuItem115});
            this.Tools.Text = "&Tools";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "&Compare different rungs";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 1;
            this.menuItem6.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.menuItem6.Text = "&Search Rungs";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem22
            // 
            this.menuItem22.Index = 2;
            this.menuItem22.Text = "Copy Diff list to Clipboard";
            this.menuItem22.Click += new System.EventHandler(this.menuItem22_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 3;
            this.menuItem13.Text = "-";
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 4;
            this.menuItem19.Text = "Export &Old file rungs to Text file";
            this.menuItem19.Click += new System.EventHandler(this.menuItem19_Click);
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 5;
            this.menuItem18.Text = "Export &New file rungs to Text file";
            this.menuItem18.Click += new System.EventHandler(this.menuItem18_Click);
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 6;
            this.menuItem20.Text = "-";
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 7;
            this.menuItem11.Text = "&Swap Old <-> New";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItem63
            // 
            this.menuItem63.Index = 8;
            this.menuItem63.Text = "-";
            // 
            // menuItem81
            // 
            this.menuItem81.Index = 9;
            this.menuItem81.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem82,
            this.menuItem83});
            this.menuItem81.Text = "Options";
            this.menuItem81.Click += new System.EventHandler(this.menuItem81_Click);
            // 
            // menuItem82
            // 
            this.menuItem82.Index = 0;
            this.menuItem82.Text = "Red is High, Blue is Low";
            this.menuItem82.Click += new System.EventHandler(this.menuItem82_Click);
            // 
            // menuItem83
            // 
            this.menuItem83.Index = 1;
            this.menuItem83.Text = "Red is Low, Blue is High";
            // 
            // menuItem114
            // 
            this.menuItem114.Index = 10;
            this.menuItem114.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem111,
            this.menuItem79});
            this.menuItem114.Text = "Westrace Tools";
            // 
            // menuItem111
            // 
            this.menuItem111.Index = 0;
            this.menuItem111.Text = "Prefix Editor";
            this.menuItem111.Click += new System.EventHandler(this.menuItem111_Click);
            // 
            // menuItem79
            // 
            this.menuItem79.Index = 1;
            this.menuItem79.Text = "Monitor NCDM (Under Development)";
            this.menuItem79.Click += new System.EventHandler(this.menuItem79_Click);
            // 
            // menuItem116
            // 
            this.menuItem116.Index = 11;
            this.menuItem116.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem68});
            this.menuItem116.Text = "Microlock Tools";
            this.menuItem116.Click += new System.EventHandler(this.menuItem116_Click);
            // 
            // menuItem68
            // 
            this.menuItem68.Index = 0;
            this.menuItem68.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem62,
            this.menuItem61,
            this.menuItem64,
            this.menuItem65});
            this.menuItem68.Text = "Microlock Formatting Tool";
            // 
            // menuItem62
            // 
            this.menuItem62.Index = 0;
            this.menuItem62.Text = "Load Unformatted File";
            this.menuItem62.Click += new System.EventHandler(this.menuItem62_Click);
            // 
            // menuItem61
            // 
            this.menuItem61.Index = 1;
            this.menuItem61.Text = "Load Formatted File";
            this.menuItem61.Click += new System.EventHandler(this.menuItem61_Click);
            // 
            // menuItem64
            // 
            this.menuItem64.Index = 2;
            this.menuItem64.Text = "Hide Parameters";
            this.menuItem64.Click += new System.EventHandler(this.menuItem64_Click);
            // 
            // menuItem65
            // 
            this.menuItem65.Index = 3;
            this.menuItem65.Text = "Show Parameters";
            this.menuItem65.Click += new System.EventHandler(this.menuItem65_Click_1);
            // 
            // menuItem112
            // 
            this.menuItem112.Index = 12;
            this.menuItem112.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem113});
            this.menuItem112.Text = "Sim Tools";
            // 
            // menuItem113
            // 
            this.menuItem113.Index = 0;
            this.menuItem113.Text = "Append Prefix to MAP";
            this.menuItem113.Click += new System.EventHandler(this.menuItem113_Click);
            // 
            // menuItem115
            // 
            this.menuItem115.Index = 13;
            this.menuItem115.Text = "Logic Visualiser (under construction)";
            this.menuItem115.Click += new System.EventHandler(this.menuItem115_Click);
            // 
            // menuItem35
            // 
            this.menuItem35.Index = 3;
            this.menuItem35.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem53,
            this.menuItem38,
            this.menuItem36,
            this.menuItem37,
            this.menuItem32,
            this.menuItem41,
            this.menuItem96,
            this.menuItem40,
            this.menuItem33,
            this.menuItem52,
            this.menuItem97,
            this.menuItem51,
            this.menuItem43,
            this.menuItem58,
            this.menuItem56,
            this.menuItem74,
            this.menuItem109,
            this.menuItem59,
            this.menuItem44,
            this.menuItem117,
            this.menuItem87,
            this.menuItem55,
            this.menuItem34,
            this.menuItem42,
            this.menuItem50,
            this.menuItem54,
            this.menuItem39,
            this.menuItem49,
            this.menuItem57,
            this.menuItem60,
            this.menuItem67,
            this.menuItem108,
            this.menuItem106,
            this.menuItem107});
            this.menuItem35.Text = "Simulator";
            this.menuItem35.Click += new System.EventHandler(this.menuItem35_Click);
            // 
            // menuItem53
            // 
            this.menuItem53.Index = 0;
            this.menuItem53.Text = "Load Turnaround logic file";
            this.menuItem53.Click += new System.EventHandler(this.menuItem53_Click);
            // 
            // menuItem38
            // 
            this.menuItem38.Index = 1;
            this.menuItem38.Text = "-";
            // 
            // menuItem36
            // 
            this.menuItem36.Index = 2;
            this.menuItem36.Text = "S&tart";
            this.menuItem36.Click += new System.EventHandler(this.menuItem36_Click);
            // 
            // menuItem37
            // 
            this.menuItem37.Index = 3;
            this.menuItem37.Text = "St&op";
            this.menuItem37.Click += new System.EventHandler(this.menuItem37_Click);
            // 
            // menuItem32
            // 
            this.menuItem32.Index = 4;
            this.menuItem32.Text = "-";
            // 
            // menuItem41
            // 
            this.menuItem41.Index = 5;
            this.menuItem41.Text = "Load Logic State File";
            this.menuItem41.Click += new System.EventHandler(this.menuItem41_Click);
            // 
            // menuItem96
            // 
            this.menuItem96.Index = 6;
            this.menuItem96.Text = "Save Logic State File";
            this.menuItem96.Click += new System.EventHandler(this.menuItem96_Click);
            // 
            // menuItem40
            // 
            this.menuItem40.Index = 7;
            this.menuItem40.Text = "&SaveAs Logic State File";
            this.menuItem40.Click += new System.EventHandler(this.menuItem40_Click);
            // 
            // menuItem33
            // 
            this.menuItem33.Index = 8;
            this.menuItem33.Text = "-";
            // 
            // menuItem52
            // 
            this.menuItem52.Index = 9;
            this.menuItem52.Text = "Load Screen Layout";
            this.menuItem52.Click += new System.EventHandler(this.menuItem52_Click);
            // 
            // menuItem97
            // 
            this.menuItem97.Index = 10;
            this.menuItem97.Text = "Save Screen Layout";
            this.menuItem97.Click += new System.EventHandler(this.menuItem97_Click);
            // 
            // menuItem51
            // 
            this.menuItem51.Index = 11;
            this.menuItem51.Text = "SaveAs Screen Layout";
            this.menuItem51.Click += new System.EventHandler(this.menuItem51_Click);
            // 
            // menuItem43
            // 
            this.menuItem43.Index = 12;
            this.menuItem43.Text = "-";
            // 
            // menuItem58
            // 
            this.menuItem58.Index = 13;
            this.menuItem58.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.menuItem58.Text = "Load Map";
            this.menuItem58.Click += new System.EventHandler(this.menuItem58_Click);
            // 
            // menuItem56
            // 
            this.menuItem56.Index = 14;
            this.menuItem56.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.menuItem56.Text = "Save Map";
            this.menuItem56.Click += new System.EventHandler(this.menuItem56_Click_3);
            // 
            // menuItem74
            // 
            this.menuItem74.Index = 15;
            this.menuItem74.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.menuItem74.Text = "SaveAs Map";
            this.menuItem74.Click += new System.EventHandler(this.menuItem74_Click);
            // 
            // menuItem109
            // 
            this.menuItem109.Index = 16;
            this.menuItem109.Text = "SaveAs Map (XML)";
            this.menuItem109.Click += new System.EventHandler(this.menuItem109_Click);
            // 
            // menuItem59
            // 
            this.menuItem59.Index = 17;
            this.menuItem59.Text = "-";
            // 
            // menuItem44
            // 
            this.menuItem44.Index = 18;
            this.menuItem44.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem48,
            this.menuItem46,
            this.menuItem45,
            this.menuItem47});
            this.menuItem44.Text = "Simulation Speed (x1.0)";
            // 
            // menuItem48
            // 
            this.menuItem48.Index = 0;
            this.menuItem48.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.menuItem48.Text = "Normal Speed";
            this.menuItem48.Click += new System.EventHandler(this.menuItem48_Click);
            // 
            // menuItem46
            // 
            this.menuItem46.Index = 1;
            this.menuItem46.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.menuItem46.Text = "Slower (x 0.5)";
            this.menuItem46.Click += new System.EventHandler(this.menuItem46_Click);
            // 
            // menuItem45
            // 
            this.menuItem45.Index = 2;
            this.menuItem45.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.menuItem45.Text = "Faster (x2)";
            this.menuItem45.Click += new System.EventHandler(this.menuItem45_Click);
            // 
            // menuItem47
            // 
            this.menuItem47.Index = 3;
            this.menuItem47.Shortcut = System.Windows.Forms.Shortcut.F4;
            this.menuItem47.Text = "Instantaneous";
            this.menuItem47.Click += new System.EventHandler(this.menuItem47_Click);
            // 
            // menuItem117
            // 
            this.menuItem117.Index = 19;
            this.menuItem117.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem121,
            this.menuItem118,
            this.menuItem119,
            this.menuItem120});
            this.menuItem117.Text = "Cycle Time";
            // 
            // menuItem121
            // 
            this.menuItem121.Index = 0;
            this.menuItem121.Text = "125ms";
            this.menuItem121.Click += new System.EventHandler(this.menuItem121_Click);
            // 
            // menuItem118
            // 
            this.menuItem118.Index = 1;
            this.menuItem118.Text = "250ms <";
            this.menuItem118.Click += new System.EventHandler(this.menuItem118_Click);
            // 
            // menuItem119
            // 
            this.menuItem119.Index = 2;
            this.menuItem119.Text = "500ms";
            this.menuItem119.Click += new System.EventHandler(this.menuItem119_Click);
            // 
            // menuItem120
            // 
            this.menuItem120.Index = 3;
            this.menuItem120.Text = "1000ms";
            this.menuItem120.Click += new System.EventHandler(this.menuItem120_Click);
            // 
            // menuItem87
            // 
            this.menuItem87.Index = 20;
            this.menuItem87.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem88,
            this.menuItem89});
            this.menuItem87.Text = "Rung Evaluation Method";
            // 
            // menuItem88
            // 
            this.menuItem88.Index = 0;
            this.menuItem88.Text = "Do not allow reverse paths (PLC) <";
            this.menuItem88.Click += new System.EventHandler(this.menuItem88_Click);
            // 
            // menuItem89
            // 
            this.menuItem89.Index = 1;
            this.menuItem89.Text = "Allow reverse paths";
            this.menuItem89.Click += new System.EventHandler(this.menuItem89_Click);
            // 
            // menuItem55
            // 
            this.menuItem55.Index = 21;
            this.menuItem55.Text = "-";
            // 
            // menuItem34
            // 
            this.menuItem34.Index = 22;
            this.menuItem34.Text = "Open Simulator Input Panel";
            this.menuItem34.Click += new System.EventHandler(this.menuItem34_Click);
            // 
            // menuItem42
            // 
            this.menuItem42.Index = 23;
            this.menuItem42.Text = "Open Simulator Rung Panel";
            this.menuItem42.Click += new System.EventHandler(this.menuItem42_Click);
            // 
            // menuItem50
            // 
            this.menuItem50.Index = 24;
            this.menuItem50.Text = "New Map Panel";
            this.menuItem50.Click += new System.EventHandler(this.menuItem50_Click_3);
            // 
            // menuItem54
            // 
            this.menuItem54.Index = 25;
            this.menuItem54.Text = "New Change of State Panel";
            this.menuItem54.Click += new System.EventHandler(this.menuItem54_Click);
            // 
            // menuItem39
            // 
            this.menuItem39.Index = 26;
            this.menuItem39.Text = "-";
            this.menuItem39.Visible = false;
            // 
            // menuItem49
            // 
            this.menuItem49.Index = 27;
            this.menuItem49.Text = "Sound";
            this.menuItem49.Visible = false;
            this.menuItem49.Click += new System.EventHandler(this.menuItem49_Click);
            // 
            // menuItem57
            // 
            this.menuItem57.Index = 28;
            this.menuItem57.Text = "Clear Buffer";
            this.menuItem57.Visible = false;
            this.menuItem57.Click += new System.EventHandler(this.menuItem57_Click);
            // 
            // menuItem60
            // 
            this.menuItem60.Index = 29;
            this.menuItem60.Text = "-";
            // 
            // menuItem67
            // 
            this.menuItem67.Index = 30;
            this.menuItem67.Text = "Serial Comms Settings";
            this.menuItem67.Click += new System.EventHandler(this.menuItem67_Click_1);
            // 
            // menuItem108
            // 
            this.menuItem108.Index = 31;
            this.menuItem108.Text = "-";
            // 
            // menuItem106
            // 
            this.menuItem106.Index = 32;
            this.menuItem106.Text = "Start Arduino Serial Comms";
            this.menuItem106.Click += new System.EventHandler(this.menuItem106_Click);
            // 
            // menuItem107
            // 
            this.menuItem107.Index = 33;
            this.menuItem107.Text = "Stop Arduino Serial Comms";
            this.menuItem107.Click += new System.EventHandler(this.menuItem107_Click);
            // 
            // mnuWindow
            // 
            this.mnuWindow.Index = 4;
            this.mnuWindow.MdiList = true;
            this.mnuWindow.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuWindowCascade,
            this.mnuWindowTileVertical,
            this.mnuWindowTileHorizontal,
            this.menuItem17,
            this.menuItem16,
            this.menuItem84,
            this.menuItem90,
            this.menuItem93});
            this.mnuWindow.Text = "&Window";
            // 
            // mnuWindowCascade
            // 
            this.mnuWindowCascade.Index = 0;
            this.mnuWindowCascade.Text = "&Cascade";
            this.mnuWindowCascade.Click += new System.EventHandler(this.mnuWindowCascade_Click);
            // 
            // mnuWindowTileVertical
            // 
            this.mnuWindowTileVertical.Index = 1;
            this.mnuWindowTileVertical.Text = "Tile &Vertical";
            this.mnuWindowTileVertical.Click += new System.EventHandler(this.mnuWindowTileVertical_Click);
            // 
            // mnuWindowTileHorizontal
            // 
            this.mnuWindowTileHorizontal.Index = 2;
            this.mnuWindowTileHorizontal.Text = "Tile &Horizontal";
            this.mnuWindowTileHorizontal.Click += new System.EventHandler(this.mnuWindowTileHorizontal_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 3;
            this.menuItem17.Shortcut = System.Windows.Forms.Shortcut.F11;
            this.menuItem17.Text = "Full Screen";
            this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 4;
            this.menuItem16.Text = "Close &all Rungs";
            this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // menuItem84
            // 
            this.menuItem84.Index = 5;
            this.menuItem84.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem85,
            this.menuItem86});
            this.menuItem84.Text = "Toolbar";
            // 
            // menuItem85
            // 
            this.menuItem85.Index = 0;
            this.menuItem85.Text = "Hide";
            this.menuItem85.Click += new System.EventHandler(this.menuItem85_Click);
            // 
            // menuItem86
            // 
            this.menuItem86.Index = 1;
            this.menuItem86.Text = "Show";
            this.menuItem86.Click += new System.EventHandler(this.menuItem86_Click);
            // 
            // menuItem90
            // 
            this.menuItem90.Index = 6;
            this.menuItem90.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem91,
            this.menuItem92});
            this.menuItem90.Text = "WindowBehaviour";
            // 
            // menuItem91
            // 
            this.menuItem91.Index = 0;
            this.menuItem91.Text = "border";
            this.menuItem91.Click += new System.EventHandler(this.menuItem91_Click);
            // 
            // menuItem92
            // 
            this.menuItem92.Index = 1;
            this.menuItem92.Text = "no border";
            this.menuItem92.Click += new System.EventHandler(this.menuItem92_Click);
            // 
            // menuItem93
            // 
            this.menuItem93.Index = 7;
            this.menuItem93.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem94,
            this.menuItem95});
            this.menuItem93.Text = "Transperancy";
            // 
            // menuItem94
            // 
            this.menuItem94.Index = 0;
            this.menuItem94.Text = "50%";
            this.menuItem94.Click += new System.EventHandler(this.menuItem94_Click);
            // 
            // menuItem95
            // 
            this.menuItem95.Index = 1;
            this.menuItem95.Text = "100%";
            this.menuItem95.Click += new System.EventHandler(this.menuItem95_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 5;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem100,
            this.mnuHelpAbout,
            this.menuItem75,
            this.menuItem70});
            this.menuItem5.Text = "&Help";
            // 
            // menuItem100
            // 
            this.menuItem100.Index = 0;
            this.menuItem100.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem103,
            this.menuItem101,
            this.menuItem102,
            this.menuItem104,
            this.menuItem105,
            this.menuItem98,
            this.menuItem99});
            this.menuItem100.Text = "Logic Navigator Website";
            // 
            // menuItem103
            // 
            this.menuItem103.Index = 0;
            this.menuItem103.Text = "Logic Navigator Website";
            this.menuItem103.Click += new System.EventHandler(this.menuItem103_Click);
            // 
            // menuItem101
            // 
            this.menuItem101.Index = 1;
            this.menuItem101.Text = "Get the Latest Version of Logic Navigator";
            this.menuItem101.Click += new System.EventHandler(this.menuItem101_Click);
            // 
            // menuItem102
            // 
            this.menuItem102.Index = 2;
            this.menuItem102.Text = "Browse Sample Projects";
            this.menuItem102.Click += new System.EventHandler(this.menuItem102_Click);
            // 
            // menuItem104
            // 
            this.menuItem104.Index = 3;
            this.menuItem104.Text = "Old Website";
            this.menuItem104.Visible = false;
            this.menuItem104.Click += new System.EventHandler(this.menuItem104_Click);
            // 
            // menuItem105
            // 
            this.menuItem105.Index = 4;
            this.menuItem105.Text = "Report an error";
            this.menuItem105.Click += new System.EventHandler(this.menuItem105_Click);
            // 
            // menuItem98
            // 
            this.menuItem98.Enabled = false;
            this.menuItem98.Index = 5;
            this.menuItem98.Text = "www.logicnavigator.net";
            this.menuItem98.Click += new System.EventHandler(this.menuItem98_Click);
            // 
            // menuItem99
            // 
            this.menuItem99.Enabled = false;
            this.menuItem99.Index = 6;
            this.menuItem99.Text = "User Manual (Under development)";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Index = 1;
            this.mnuHelpAbout.Text = "&About...";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // menuItem75
            // 
            this.menuItem75.Index = 2;
            this.menuItem75.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem76,
            this.menuItem77,
            this.menuItem78,
            this.menuItem80});
            this.menuItem75.Text = "Developer Tools";
            // 
            // menuItem76
            // 
            this.menuItem76.Index = 0;
            this.menuItem76.Text = "Toggle Save Filenames";
            this.menuItem76.Click += new System.EventHandler(this.menuItem76_Click);
            // 
            // menuItem77
            // 
            this.menuItem77.Index = 1;
            this.menuItem77.Text = "OpenOldMap";
            this.menuItem77.Click += new System.EventHandler(this.menuItem77_Click);
            // 
            // menuItem78
            // 
            this.menuItem78.Index = 2;
            this.menuItem78.Text = "Show Train Beads";
            this.menuItem78.Visible = false;
            this.menuItem78.Click += new System.EventHandler(this.menuItem78_Click);
            // 
            // menuItem80
            // 
            this.menuItem80.Index = 3;
            this.menuItem80.Text = "Red is High, Blue is Low";
            // 
            // menuItem70
            // 
            this.menuItem70.Index = 3;
            this.menuItem70.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem66,
            this.menuItem69,
            this.menuItem110});
            this.menuItem70.Text = "Other";
            // 
            // menuItem66
            // 
            this.menuItem66.Index = 0;
            this.menuItem66.Text = "Play Chess";
            this.menuItem66.Click += new System.EventHandler(this.menuItem66_Click_1);
            // 
            // menuItem69
            // 
            this.menuItem69.Index = 1;
            this.menuItem69.Text = "Mandelbrot Set";
            this.menuItem69.Click += new System.EventHandler(this.menuItem69_Click);
            // 
            // menuItem110
            // 
            this.menuItem110.Index = 2;
            this.menuItem110.Text = "Game of Life";
            this.menuItem110.Click += new System.EventHandler(this.menuItem110_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "L:\\My Projects\\c#\\MDI";
            this.openFileDialog1.Title = "Open INS/WT2/NCD/ML2/GN2/MLK/VTL/LSV/NV/TXT file (Old)";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // NewFileName
            // 
            this.NewFileName.Location = new System.Drawing.Point(312, 16);
            this.NewFileName.Name = "NewFileName";
            this.NewFileName.Size = new System.Drawing.Size(144, 20);
            this.NewFileName.TabIndex = 1;
            this.NewFileName.Visible = false;
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(144, 531);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(1122, 21);
            this.statusBar1.TabIndex = 3;
            this.statusBar1.Text = "Logic Navigator";
            // 
            // Rungs
            // 
            this.Rungs.HideSelection = false;
            this.Rungs.Location = new System.Drawing.Point(150, 176);
            this.Rungs.Name = "Rungs";
            this.Rungs.Size = new System.Drawing.Size(112, 72);
            this.Rungs.TabIndex = 5;
            this.Rungs.UseCompatibleStateImageBehavior = false;
            this.Rungs.View = System.Windows.Forms.View.List;
            this.Rungs.Visible = false;
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView.Location = new System.Drawing.Point(0, 88);
            this.treeView.Name = "treeView";
            this.treeView.ShowRootLines = false;
            this.treeView.Size = new System.Drawing.Size(144, 443);
            this.treeView.TabIndex = 11;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.DefaultExt = "L:\\My Projects\\c#\\MDI";
            this.openFileDialog2.Filter = resources.GetString("openFileDialog2.Filter");
            this.openFileDialog2.Title = "Open INS/WT2/NCD/ML2/GN2/MLK/VTL/LSV/NV/TXT file (Old)";
            this.openFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog2_FileOk);
            // 
            // OldFileName
            // 
            this.OldFileName.Location = new System.Drawing.Point(268, 202);
            this.OldFileName.Name = "OldFileName";
            this.OldFileName.Size = new System.Drawing.Size(144, 20);
            this.OldFileName.TabIndex = 13;
            this.OldFileName.Visible = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Rungs";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitter1
            // 
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter1.Location = new System.Drawing.Point(0, 64);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(144, 488);
            this.splitter1.TabIndex = 18;
            this.splitter1.TabStop = false;
            // 
            // FileName
            // 
            this.FileName.Location = new System.Drawing.Point(268, 176);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(144, 20);
            this.FileName.TabIndex = 20;
            this.FileName.Visible = false;
            // 
            // fontDialog1
            // 
            this.fontDialog1.AllowScriptChange = false;
            this.fontDialog1.Color = System.Drawing.SystemColors.ControlText;
            this.fontDialog1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontDialog1.ShowEffects = false;
            // 
            // RungGrid
            // 
            this.RungGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.RungGrid.CaptionVisible = false;
            this.RungGrid.DataMember = "";
            this.RungGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.RungGrid.Location = new System.Drawing.Point(0, 88);
            this.RungGrid.Name = "RungGrid";
            this.RungGrid.PreferredColumnWidth = 30;
            this.RungGrid.RowHeadersVisible = false;
            this.RungGrid.Size = new System.Drawing.Size(144, 443);
            this.RungGrid.TabIndex = 22;
            this.RungGrid.Visible = false;
            this.RungGrid.Navigate += new System.Windows.Forms.NavigateEventHandler(this.RungGrid_Navigate);
            this.RungGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RungGrid_MouseDown);
            // 
            // Search
            // 
            this.Search.ImageIndex = 0;
            this.Search.Name = "Search";
            this.Search.Text = "Search";
            this.Search.ToolTipText = "Open search window";
            this.Search.Visible = false;
            // 
            // OpenNew
            // 
            this.OpenNew.ImageIndex = 2;
            this.OpenNew.Name = "OpenNew";
            this.OpenNew.Text = "New File";
            this.OpenNew.ToolTipText = "Open NEW file";
            this.OpenNew.Visible = false;
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.Open,
            this.OpenOld,
            this.OpenNew,
            this.OpenProject,
            this.ProjectSave,
            this.Separator,
            this.Search,
            this.CompareUniqueRungs,
            this.Separator2,
            this.ShowRungs,
            this.ShowHousing,
            this.ShowHorizontal,
            this.ExportToText,
            this.toolBarButton1,
            this.ZoomIn,
            this.ZoomOut,
            this.Separator1,
            this.CloseAllRungs,
            this.CloseAllFiles,
            this.Question});
            this.toolBar1.ButtonSize = new System.Drawing.Size(15, 15);
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, -2);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(1212, 58);
            this.toolBar1.TabIndex = 26;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // Open
            // 
            this.Open.ImageIndex = 2;
            this.Open.Name = "Open";
            this.Open.Text = "Open File";
            this.Open.ToolTipText = "Open a File";
            // 
            // OpenOld
            // 
            this.OpenOld.ImageIndex = 1;
            this.OpenOld.Name = "OpenOld";
            this.OpenOld.Text = "Old File";
            this.OpenOld.ToolTipText = "Open OLD file";
            this.OpenOld.Visible = false;
            // 
            // OpenProject
            // 
            this.OpenProject.ImageIndex = 15;
            this.OpenProject.Name = "OpenProject";
            this.OpenProject.Text = "Open Project";
            // 
            // ProjectSave
            // 
            this.ProjectSave.ImageIndex = 20;
            this.ProjectSave.Name = "ProjectSave";
            this.ProjectSave.Text = "Save Project";
            this.ProjectSave.Visible = false;
            // 
            // Separator
            // 
            this.Separator.Name = "Separator";
            this.Separator.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            this.Separator.Visible = false;
            // 
            // CompareUniqueRungs
            // 
            this.CompareUniqueRungs.ImageIndex = 8;
            this.CompareUniqueRungs.Name = "CompareUniqueRungs";
            this.CompareUniqueRungs.Text = "Compare Rungs";
            this.CompareUniqueRungs.ToolTipText = "Compare different rungs";
            this.CompareUniqueRungs.Visible = false;
            // 
            // Separator2
            // 
            this.Separator2.Name = "Separator2";
            this.Separator2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            this.Separator2.Visible = false;
            // 
            // ShowRungs
            // 
            this.ShowRungs.ImageIndex = 19;
            this.ShowRungs.Name = "ShowRungs";
            this.ShowRungs.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.ShowRungs.Text = "Rungs";
            this.ShowRungs.ToolTipText = "Show/Hide rung panel";
            this.ShowRungs.Visible = false;
            // 
            // ShowHousing
            // 
            this.ShowHousing.ImageIndex = 17;
            this.ShowHousing.Name = "ShowHousing";
            this.ShowHousing.Text = "Housings";
            this.ShowHousing.ToolTipText = "Show Westrace housings";
            this.ShowHousing.Visible = false;
            // 
            // ShowHorizontal
            // 
            this.ShowHorizontal.ImageIndex = 10;
            this.ShowHorizontal.Name = "ShowHorizontal";
            this.ShowHorizontal.Text = "Tile";
            this.ShowHorizontal.ToolTipText = "Show windows in horizontal tiled form";
            this.ShowHorizontal.Visible = false;
            // 
            // ExportToText
            // 
            this.ExportToText.ImageIndex = 21;
            this.ExportToText.Name = "ExportToText";
            this.ExportToText.Text = "Export to text file";
            this.ExportToText.ToolTipText = "Export rungs to a text file";
            this.ExportToText.Visible = false;
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            this.toolBarButton1.Visible = false;
            // 
            // ZoomIn
            // 
            this.ZoomIn.ImageIndex = 11;
            this.ZoomIn.Name = "ZoomIn";
            this.ZoomIn.Text = "Zoom In";
            this.ZoomIn.Visible = false;
            // 
            // ZoomOut
            // 
            this.ZoomOut.ImageIndex = 12;
            this.ZoomOut.Name = "ZoomOut";
            this.ZoomOut.Text = "Zoom Out";
            this.ZoomOut.Visible = false;
            // 
            // Separator1
            // 
            this.Separator1.Name = "Separator1";
            this.Separator1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            this.Separator1.Visible = false;
            // 
            // CloseAllRungs
            // 
            this.CloseAllRungs.ImageIndex = 9;
            this.CloseAllRungs.Name = "CloseAllRungs";
            this.CloseAllRungs.Text = "Close Rungs";
            this.CloseAllRungs.ToolTipText = "Close all currently open rungs";
            this.CloseAllRungs.Visible = false;
            // 
            // CloseAllFiles
            // 
            this.CloseAllFiles.ImageIndex = 3;
            this.CloseAllFiles.Name = "CloseAllFiles";
            this.CloseAllFiles.Text = "Close Files";
            this.CloseAllFiles.ToolTipText = "Close all files";
            this.CloseAllFiles.Visible = false;
            // 
            // Question
            // 
            this.Question.ImageIndex = 22;
            this.Question.Name = "Question";
            this.Question.Text = "Help";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "binocular_icon_white-32.bmp");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "close.png");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "");
            this.imageList1.Images.SetKeyName(9, "");
            this.imageList1.Images.SetKeyName(10, "");
            this.imageList1.Images.SetKeyName(11, "zoomin.png");
            this.imageList1.Images.SetKeyName(12, "zoomout.png");
            this.imageList1.Images.SetKeyName(13, "Export Old to Text file.bmp");
            this.imageList1.Images.SetKeyName(14, "Export New to Text file.bmp");
            this.imageList1.Images.SetKeyName(15, "screen4.png");
            this.imageList1.Images.SetKeyName(16, "rungs.png");
            this.imageList1.Images.SetKeyName(17, "housing.png");
            this.imageList1.Images.SetKeyName(18, "norungscreen.png");
            this.imageList1.Images.SetKeyName(19, "rungscreen.png");
            this.imageList1.Images.SetKeyName(20, "save.jpg");
            this.imageList1.Images.SetKeyName(21, "export.jpg");
            this.imageList1.Images.SetKeyName(22, "question.jpg");
            // 
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter3.Location = new System.Drawing.Point(0, 0);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(1266, 64);
            this.splitter3.TabIndex = 30;
            this.splitter3.TabStop = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files|*.*";
            this.saveFileDialog1.Title = "Export Rungs to text (New)";
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.Filter = "txt files (*.txt)|*.txt|All files|*.*";
            this.saveFileDialog2.Title = "Export Rungs to text (Old)";
            // 
            // Arg1
            // 
            this.Arg1.Location = new System.Drawing.Point(150, 72);
            this.Arg1.Name = "Arg1";
            this.Arg1.Size = new System.Drawing.Size(544, 20);
            this.Arg1.TabIndex = 34;
            this.Arg1.Visible = false;
            this.Arg1.TextChanged += new System.EventHandler(this.Arg1_TextChanged);
            // 
            // Arg2
            // 
            this.Arg2.Location = new System.Drawing.Point(150, 98);
            this.Arg2.Name = "Arg2";
            this.Arg2.Size = new System.Drawing.Size(544, 20);
            this.Arg2.TabIndex = 35;
            this.Arg2.Visible = false;
            // 
            // Arg3
            // 
            this.Arg3.Location = new System.Drawing.Point(150, 124);
            this.Arg3.Name = "Arg3";
            this.Arg3.Size = new System.Drawing.Size(544, 20);
            this.Arg3.TabIndex = 36;
            this.Arg3.Visible = false;
            // 
            // Dialog
            // 
            this.Dialog.Location = new System.Drawing.Point(150, 150);
            this.Dialog.Name = "Dialog";
            this.Dialog.Size = new System.Drawing.Size(544, 20);
            this.Dialog.TabIndex = 37;
            this.Dialog.Visible = false;
            // 
            // Simulationtimer
            // 
            this.Simulationtimer.Interval = 125;
            this.Simulationtimer.Tick += new System.EventHandler(this.Simulationtimer_Tick);
            // 
            // saveSt8FileDialog
            // 
            this.saveSt8FileDialog.Filter = "st8 files (*.st8)|*.st8|All files|*.*";
            this.saveSt8FileDialog.Title = "Save Current State of Interlocking";
            // 
            // openst8FileDialog
            // 
            this.openst8FileDialog.DefaultExt = "L:\\My Projects\\c#\\MDI";
            this.openst8FileDialog.Filter = "Logic State file|*.st8|All Files|*.*";
            this.openst8FileDialog.Title = "Open Logic State File";
            // 
            // ProcessorHits
            // 
            this.ProcessorHits.Location = new System.Drawing.Point(817, 254);
            this.ProcessorHits.Name = "ProcessorHits";
            this.ProcessorHits.Size = new System.Drawing.Size(144, 20);
            this.ProcessorHits.TabIndex = 39;
            this.ProcessorHits.Visible = false;
            // 
            // openLayoutFileDialog
            // 
            this.openLayoutFileDialog.DefaultExt = "L:\\My Projects\\c#\\MDI";
            this.openLayoutFileDialog.Filter = "LYT files (*.lyt)|*.lyt|All files|*.*";
            this.openLayoutFileDialog.Title = "Open Logic State File";
            // 
            // saveLayoutFileDialog
            // 
            this.saveLayoutFileDialog.Filter = "LYT files (*.lyt)|*.lyt|All files|*.*";
            this.saveLayoutFileDialog.Title = "Save Layout of Forms";
            this.saveLayoutFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveLayoutFileDialog_FileOk);
            // 
            // TALFileName
            // 
            this.TALFileName.Location = new System.Drawing.Point(268, 228);
            this.TALFileName.Name = "TALFileName";
            this.TALFileName.Size = new System.Drawing.Size(94, 20);
            this.TALFileName.TabIndex = 41;
            this.TALFileName.Visible = false;
            this.TALFileName.TextChanged += new System.EventHandler(this.TALFileName_TextChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // openFileDialog3
            // 
            this.openFileDialog3.FileName = "openFileDialog3";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(817, 228);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(144, 20);
            this.textBox3.TabIndex = 45;
            this.textBox3.Visible = false;
            // 
            // LinesPerGroup
            // 
            this.LinesPerGroup.Location = new System.Drawing.Point(977, 72);
            this.LinesPerGroup.Name = "LinesPerGroup";
            this.LinesPerGroup.Size = new System.Drawing.Size(144, 20);
            this.LinesPerGroup.TabIndex = 47;
            this.LinesPerGroup.Text = "4";
            this.LinesPerGroup.Visible = false;
            // 
            // columnsoutput
            // 
            this.columnsoutput.Location = new System.Drawing.Point(977, 98);
            this.columnsoutput.Name = "columnsoutput";
            this.columnsoutput.Size = new System.Drawing.Size(144, 20);
            this.columnsoutput.TabIndex = 48;
            this.columnsoutput.Text = "2";
            this.columnsoutput.Visible = false;
            // 
            // Indentations
            // 
            this.Indentations.Location = new System.Drawing.Point(977, 125);
            this.Indentations.Name = "Indentations";
            this.Indentations.Size = new System.Drawing.Size(144, 20);
            this.Indentations.TabIndex = 49;
            this.Indentations.Text = "true";
            this.Indentations.Visible = false;
            // 
            // spacesbetweencolumns
            // 
            this.spacesbetweencolumns.Location = new System.Drawing.Point(977, 151);
            this.spacesbetweencolumns.Name = "spacesbetweencolumns";
            this.spacesbetweencolumns.Size = new System.Drawing.Size(144, 20);
            this.spacesbetweencolumns.TabIndex = 50;
            this.spacesbetweencolumns.Text = "23";
            this.spacesbetweencolumns.Visible = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(826, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 16);
            this.label2.TabIndex = 51;
            this.label2.Text = "LinesPerGroup";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(825, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 16);
            this.label3.TabIndex = 52;
            this.label3.Text = "Columns";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(825, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 16);
            this.label4.TabIndex = 53;
            this.label4.Text = "Indentations on first row";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // SpaceperGrouping
            // 
            this.SpaceperGrouping.Location = new System.Drawing.Point(977, 176);
            this.SpaceperGrouping.Name = "SpaceperGrouping";
            this.SpaceperGrouping.Size = new System.Drawing.Size(144, 20);
            this.SpaceperGrouping.TabIndex = 54;
            this.SpaceperGrouping.Text = "true";
            this.SpaceperGrouping.Visible = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(825, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 16);
            this.label5.TabIndex = 55;
            this.label5.Text = "Space per Grouping";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(825, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 16);
            this.label6.TabIndex = 56;
            this.label6.Text = "Spaces Between Columns";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label6.Visible = false;
            // 
            // CurrentLayout
            // 
            this.CurrentLayout.Location = new System.Drawing.Point(269, 313);
            this.CurrentLayout.Name = "CurrentLayout";
            this.CurrentLayout.Size = new System.Drawing.Size(544, 20);
            this.CurrentLayout.TabIndex = 58;
            this.CurrentLayout.Visible = false;
            // 
            // CurrentMap1
            // 
            this.CurrentMap1.Location = new System.Drawing.Point(268, 392);
            this.CurrentMap1.Name = "CurrentMap1";
            this.CurrentMap1.Size = new System.Drawing.Size(233, 20);
            this.CurrentMap1.TabIndex = 60;
            this.CurrentMap1.Visible = false;
            // 
            // CurrentState
            // 
            this.CurrentState.Location = new System.Drawing.Point(269, 340);
            this.CurrentState.Name = "CurrentState";
            this.CurrentState.Size = new System.Drawing.Size(544, 20);
            this.CurrentState.TabIndex = 61;
            this.CurrentState.Visible = false;
            // 
            // CurrentTALFile
            // 
            this.CurrentTALFile.Location = new System.Drawing.Point(268, 366);
            this.CurrentTALFile.Name = "CurrentTALFile";
            this.CurrentTALFile.Size = new System.Drawing.Size(472, 20);
            this.CurrentTALFile.TabIndex = 62;
            this.CurrentTALFile.Visible = false;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(150, 314);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 16);
            this.label7.TabIndex = 64;
            this.label7.Text = "Layout";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label7.Visible = false;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(149, 392);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 16);
            this.label8.TabIndex = 65;
            this.label8.Text = "Current Maps";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label8.Visible = false;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(150, 340);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 16);
            this.label9.TabIndex = 66;
            this.label9.Text = "Current Logic State";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label9.Visible = false;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(150, 366);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 16);
            this.label10.TabIndex = 67;
            this.label10.Text = "TAL File";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label10.Visible = false;
            // 
            // saveProjectFileDialog
            // 
            this.saveProjectFileDialog.Filter = "prj file (*.prj)|*.prj|All files|*.*";
            this.saveProjectFileDialog.Title = "Save Project File";
            this.saveProjectFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveProjectFileDialog_FileOk);
            // 
            // openProjectFileDialog
            // 
            this.openProjectFileDialog.FileName = "openProjectFileDialog";
            // 
            // CurrentMap5
            // 
            this.CurrentMap5.Location = new System.Drawing.Point(268, 496);
            this.CurrentMap5.Name = "CurrentMap5";
            this.CurrentMap5.Size = new System.Drawing.Size(233, 20);
            this.CurrentMap5.TabIndex = 68;
            this.CurrentMap5.Visible = false;
            // 
            // CurrentMap3
            // 
            this.CurrentMap3.Location = new System.Drawing.Point(268, 444);
            this.CurrentMap3.Name = "CurrentMap3";
            this.CurrentMap3.Size = new System.Drawing.Size(233, 20);
            this.CurrentMap3.TabIndex = 69;
            this.CurrentMap3.Visible = false;
            // 
            // CurrentMap2
            // 
            this.CurrentMap2.Location = new System.Drawing.Point(268, 418);
            this.CurrentMap2.Name = "CurrentMap2";
            this.CurrentMap2.Size = new System.Drawing.Size(233, 20);
            this.CurrentMap2.TabIndex = 70;
            this.CurrentMap2.Visible = false;
            // 
            // CurrentMap4
            // 
            this.CurrentMap4.Location = new System.Drawing.Point(268, 470);
            this.CurrentMap4.Name = "CurrentMap4";
            this.CurrentMap4.Size = new System.Drawing.Size(233, 20);
            this.CurrentMap4.TabIndex = 71;
            this.CurrentMap4.Visible = false;
            // 
            // ProjectDirectory
            // 
            this.ProjectDirectory.Location = new System.Drawing.Point(268, 287);
            this.ProjectDirectory.Name = "ProjectDirectory";
            this.ProjectDirectory.Size = new System.Drawing.Size(544, 20);
            this.ProjectDirectory.TabIndex = 73;
            this.ProjectDirectory.Visible = false;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(147, 287);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 20);
            this.label11.TabIndex = 74;
            this.label11.Text = "Project Directory";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label11.Visible = false;
            // 
            // NewFileText1
            // 
            this.NewFileText1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NewFileText1.Location = new System.Drawing.Point(917, 444);
            this.NewFileText1.Name = "NewFileText1";
            this.NewFileText1.Size = new System.Drawing.Size(292, 20);
            this.NewFileText1.TabIndex = 76;
            this.NewFileText1.Visible = false;
            // 
            // OldFileText1
            // 
            this.OldFileText1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OldFileText1.Location = new System.Drawing.Point(906, 362);
            this.OldFileText1.Name = "OldFileText1";
            this.OldFileText1.Size = new System.Drawing.Size(290, 20);
            this.OldFileText1.TabIndex = 77;
            this.OldFileText1.Visible = false;
            // 
            // NewFileText
            // 
            this.NewFileText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NewFileText.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewFileText.ForeColor = System.Drawing.Color.Red;
            this.NewFileText.Location = new System.Drawing.Point(1183, 36);
            this.NewFileText.Name = "NewFileText";
            this.NewFileText.Size = new System.Drawing.Size(211, 28);
            this.NewFileText.TabIndex = 80;
            this.NewFileText.Text = "New File Name";
            this.NewFileText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewFileText.DragDrop += new System.Windows.Forms.DragEventHandler(this.NewFileText_DragDrop);
            this.NewFileText.DragEnter += new System.Windows.Forms.DragEventHandler(this.NewFileText_DragEnter);
            // 
            // OldFileText
            // 
            this.OldFileText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OldFileText.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OldFileText.ForeColor = System.Drawing.Color.Blue;
            this.OldFileText.Location = new System.Drawing.Point(1183, -1);
            this.OldFileText.Name = "OldFileText";
            this.OldFileText.Size = new System.Drawing.Size(211, 34);
            this.OldFileText.TabIndex = 81;
            this.OldFileText.Text = "Old File Name";
            this.OldFileText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.OldFileText.DragDrop += new System.Windows.Forms.DragEventHandler(this.OldFileText_DragDrop);
            this.OldFileText.DragEnter += new System.Windows.Forms.DragEventHandler(this.OldFileText_DragEnter);
            this.OldFileText.MouseHover += new System.EventHandler(this.OldFileText_MouseHover);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(147, 522);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(113, 16);
            this.label12.TabIndex = 84;
            this.label12.Text = "Image Directory";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label12.Visible = false;
            // 
            // ImageDirectory
            // 
            this.ImageDirectory.Location = new System.Drawing.Point(268, 522);
            this.ImageDirectory.Name = "ImageDirectory";
            this.ImageDirectory.Size = new System.Drawing.Size(544, 20);
            this.ImageDirectory.TabIndex = 83;
            this.ImageDirectory.Visible = false;
            // 
            // VoltageMatrixText
            // 
            this.VoltageMatrixText.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VoltageMatrixText.Location = new System.Drawing.Point(171, 71);
            this.VoltageMatrixText.Multiline = true;
            this.VoltageMatrixText.Name = "VoltageMatrixText";
            this.VoltageMatrixText.Size = new System.Drawing.Size(330, 48);
            this.VoltageMatrixText.TabIndex = 86;
            this.VoltageMatrixText.Visible = false;
            // 
            // result
            // 
            this.result.Location = new System.Drawing.Point(185, 612);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(100, 20);
            this.result.TabIndex = 87;
            this.result.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Red;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(144, 509);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1122, 22);
            this.statusStrip1.TabIndex = 89;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(34, 17);
            this.toolStripStatusLabel1.Text = "Okay";
            // 
            // CurrentMap9
            // 
            this.CurrentMap9.Location = new System.Drawing.Point(507, 470);
            this.CurrentMap9.Name = "CurrentMap9";
            this.CurrentMap9.Size = new System.Drawing.Size(233, 20);
            this.CurrentMap9.TabIndex = 95;
            this.CurrentMap9.Visible = false;
            // 
            // CurrentMap7
            // 
            this.CurrentMap7.Location = new System.Drawing.Point(507, 418);
            this.CurrentMap7.Name = "CurrentMap7";
            this.CurrentMap7.Size = new System.Drawing.Size(233, 20);
            this.CurrentMap7.TabIndex = 94;
            this.CurrentMap7.Visible = false;
            // 
            // CurrentMap8
            // 
            this.CurrentMap8.Location = new System.Drawing.Point(507, 444);
            this.CurrentMap8.Name = "CurrentMap8";
            this.CurrentMap8.Size = new System.Drawing.Size(233, 20);
            this.CurrentMap8.TabIndex = 93;
            this.CurrentMap8.Visible = false;
            // 
            // CurrentMap10
            // 
            this.CurrentMap10.Location = new System.Drawing.Point(507, 496);
            this.CurrentMap10.Name = "CurrentMap10";
            this.CurrentMap10.Size = new System.Drawing.Size(233, 20);
            this.CurrentMap10.TabIndex = 92;
            this.CurrentMap10.Visible = false;
            // 
            // CurrentMap6
            // 
            this.CurrentMap6.Location = new System.Drawing.Point(507, 392);
            this.CurrentMap6.Name = "CurrentMap6";
            this.CurrentMap6.Size = new System.Drawing.Size(233, 20);
            this.CurrentMap6.TabIndex = 91;
            this.CurrentMap6.Visible = false;
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM3";
            // 
            // SendMessages
            // 
            this.SendMessages.Location = new System.Drawing.Point(120, 94);
            this.SendMessages.Name = "SendMessages";
            this.SendMessages.Size = new System.Drawing.Size(143, 20);
            this.SendMessages.TabIndex = 97;
            this.SendMessages.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(120, 120);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(143, 20);
            this.textBox1.TabIndex = 98;
            this.textBox1.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(31, 97);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 13);
            this.label13.TabIndex = 100;
            this.label13.Text = "Messages Sent:";
            this.label13.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 123);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(107, 13);
            this.label14.TabIndex = 101;
            this.label14.Text = "Messages Received:";
            this.label14.Visible = false;
            // 
            // TAL2FileName
            // 
            this.TAL2FileName.Location = new System.Drawing.Point(367, 228);
            this.TAL2FileName.Name = "TAL2FileName";
            this.TAL2FileName.Size = new System.Drawing.Size(93, 20);
            this.TAL2FileName.TabIndex = 107;
            this.TAL2FileName.Visible = false;
            // 
            // TAL3FileName
            // 
            this.TAL3FileName.Location = new System.Drawing.Point(466, 228);
            this.TAL3FileName.Name = "TAL3FileName";
            this.TAL3FileName.Size = new System.Drawing.Size(93, 20);
            this.TAL3FileName.TabIndex = 108;
            this.TAL3FileName.Visible = false;
            // 
            // TAL4FileName
            // 
            this.TAL4FileName.Location = new System.Drawing.Point(565, 228);
            this.TAL4FileName.Name = "TAL4FileName";
            this.TAL4FileName.Size = new System.Drawing.Size(93, 20);
            this.TAL4FileName.TabIndex = 109;
            this.TAL4FileName.Visible = false;
            // 
            // prefixt4
            // 
            this.prefixt4.Location = new System.Drawing.Point(565, 254);
            this.prefixt4.Name = "prefixt4";
            this.prefixt4.Size = new System.Drawing.Size(93, 20);
            this.prefixt4.TabIndex = 114;
            this.prefixt4.Visible = false;
            // 
            // prefixt3
            // 
            this.prefixt3.Location = new System.Drawing.Point(466, 254);
            this.prefixt3.Name = "prefixt3";
            this.prefixt3.Size = new System.Drawing.Size(93, 20);
            this.prefixt3.TabIndex = 113;
            this.prefixt3.Visible = false;
            // 
            // prefixt2
            // 
            this.prefixt2.Location = new System.Drawing.Point(367, 254);
            this.prefixt2.Name = "prefixt2";
            this.prefixt2.Size = new System.Drawing.Size(93, 20);
            this.prefixt2.TabIndex = 112;
            this.prefixt2.Visible = false;
            // 
            // prefixt1
            // 
            this.prefixt1.Location = new System.Drawing.Point(268, 254);
            this.prefixt1.Name = "prefixt1";
            this.prefixt1.Size = new System.Drawing.Size(94, 20);
            this.prefixt1.TabIndex = 111;
            this.prefixt1.Visible = false;
            // 
            // prefixmain
            // 
            this.prefixmain.Location = new System.Drawing.Point(418, 176);
            this.prefixmain.Name = "prefixmain";
            this.prefixmain.Size = new System.Drawing.Size(144, 20);
            this.prefixmain.TabIndex = 116;
            this.prefixmain.Visible = false;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Image = global::Logic_Navigator.Properties.Resources.close16;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button2.Location = new System.Drawing.Point(88, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(28, 24);
            this.button2.TabIndex = 32;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Image = global::Logic_Navigator.Properties.Resources.expandright16;
            this.button1.Location = new System.Drawing.Point(112, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 23);
            this.button1.TabIndex = 24;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMDIMain
            // 
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(1266, 552);
            this.Controls.Add(this.prefixmain);
            this.Controls.Add(this.prefixt4);
            this.Controls.Add(this.prefixt3);
            this.Controls.Add(this.prefixt2);
            this.Controls.Add(this.prefixt1);
            this.Controls.Add(this.TAL4FileName);
            this.Controls.Add(this.TAL3FileName);
            this.Controls.Add(this.TAL2FileName);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.SendMessages);
            this.Controls.Add(this.CurrentMap9);
            this.Controls.Add(this.CurrentMap7);
            this.Controls.Add(this.CurrentMap8);
            this.Controls.Add(this.CurrentMap10);
            this.Controls.Add(this.CurrentMap6);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.result);
            this.Controls.Add(this.VoltageMatrixText);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.ImageDirectory);
            this.Controls.Add(this.OldFileText);
            this.Controls.Add(this.NewFileText);
            this.Controls.Add(this.OldFileText1);
            this.Controls.Add(this.NewFileText1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.ProjectDirectory);
            this.Controls.Add(this.CurrentMap4);
            this.Controls.Add(this.CurrentMap2);
            this.Controls.Add(this.CurrentMap3);
            this.Controls.Add(this.CurrentMap5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.CurrentTALFile);
            this.Controls.Add(this.CurrentState);
            this.Controls.Add(this.CurrentMap1);
            this.Controls.Add(this.CurrentLayout);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SpaceperGrouping);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.spacesbetweencolumns);
            this.Controls.Add(this.Indentations);
            this.Controls.Add(this.columnsoutput);
            this.Controls.Add(this.LinesPerGroup);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.TALFileName);
            this.Controls.Add(this.ProcessorHits);
            this.Controls.Add(this.Dialog);
            this.Controls.Add(this.Arg3);
            this.Controls.Add(this.Arg2);
            this.Controls.Add(this.Arg1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.RungGrid);
            this.Controls.Add(this.FileName);
            this.Controls.Add(this.OldFileName);
            this.Controls.Add(this.NewFileName);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Rungs);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.splitter3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Menu = this.mainMenu1;
            this.Name = "frmMDIMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Logic Navigator";
            this.TransparencyKey = System.Drawing.Color.White;
            this.Deactivate += new System.EventHandler(this.frmMDIMain_Deactivate);
            this.Load += new System.EventHandler(this.frmMDIMain_Load);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.frmMDIMain_Scroll);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmMDIMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.frmMDIMain_DragEnter);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMDIMain_KeyPress);
            this.MouseHover += new System.EventHandler(this.frmMDIMain_MouseHover);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.frmMDIMain_MouseWheel);
            ((System.ComponentModel.ISupportInitialize)(this.RungGrid)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>


        private void frmMDIMain_Load(object sender, System.EventArgs e)
        {
            if (DateTime.Compare(DateTime.Now, expiryDate) < 0)
            {
                if (DateTime.Compare(DateTime.Now, warningDate) > 0)
                {
                    System.TimeSpan diff = expiryDate.Subtract(DateTime.Now);
                    string message = "Logic Navigator will expire in " + diff.Days + " days. Contact Ken Karrasch for a newer version. (mobile: 0438 751 993, email: kenkarrasch@y7mail.com)";
                    MessageBox.Show(message, "Expiry date approaching");
                }
            }
            else
            {
                MessageBox.Show("Logic Navigator software has expired, contact Ken Karrasch for newer version. (mobile: 0438 751 993, email: kenkarrasch@y7mail.com)", "Software Expired");
                this.menuItem4.Visible = false;
                this.menuItem1.Visible = false;
            }
            HideRungPane();

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
            NameSeqStyle.Width = 62;

            OldSeqStyle = new DataGridTextBoxColumn();
            OldSeqStyle.MappingName = "Old";
            OldSeqStyle.HeaderText = "Old";
            OldSeqStyle.Width = 25;

            StatusSeqStyle = new DataGridTextBoxColumn();
            StatusSeqStyle.MappingName = "Status";
            StatusSeqStyle.HeaderText = "Status";
            StatusSeqStyle.Width = 62;

            //PropertyDescriptorCollection pcol; //= this.BindingContext[myDataSet, "Table1"].GetItemProperties();
            //pcol.Add(
            NewSeqStyle = new DataGridTextBoxColumn();
            //NewSeqStyle = new ColumnStyle();//pcol["Table1"]);
            NewSeqStyle.MappingName = "New";
            NewSeqStyle.HeaderText = "New";
            NewSeqStyle.Width = 25;

            tableStyle.GridColumnStyles.Add(OldSeqStyle);
            tableStyle.GridColumnStyles.Add(NewSeqStyle);
            tableStyle.GridColumnStyles.Add(NameSeqStyle);
            tableStyle.GridColumnStyles.Add(StatusSeqStyle);
            RungGrid.TableStyles.Add(tableStyle);
            RungGrid.TableStyles["Points"].RowHeadersVisible = false;

            RungGrid.SelectionBackColor = Color.Blue;

            wipevoltages();

            /////////////////Process Commandline////////////////
            string[] args = Environment.GetCommandLineArgs();
            int argnumber = 1;
            foreach (string arg in args)
            {
                if (argnumber == 1) Arg1.Text = arg;
                if (argnumber == 2) Arg2.Text = arg;
                if (argnumber == 3) Arg3.Text = arg;
                argnumber++;
            }
            if (string.Compare(Arg2.Text, "") != 0)
            {
                if (Arg2.TextLength > 4)
                {
                    if (Arg2.Text.Substring(Arg2.TextLength - 4) == ".prj")
                        openProjectFile(Arg2.Text);
                    else
                    {
                        CommandlineOpenOldFile(Arg2.Text);
                        CommandlineOpenNewFile(Arg2.Text);
                    }
                }
            }

            ////////////////////////////////////////////////////

            //MyUpSoundPlayer = new SoundPlayer(@"C:\Users\Ken\Documents\Visual Studio 2012\Projects\Sounds\revup3.wav");
            //MyDownSoundPlayer = new SoundPlayer(@"C:\Users\Ken\Documents\Visual Studio 2012\Projects\Sounds\revdn3.wav");
            //MyTimerStartUpSoundPlayer = new SoundPlayer(@"C:\Users\Ken\Documents\Visual Studio 2012\Projects\Sounds\click.wav");
            //MyTimerStartDownSoundPlayer = new SoundPlayer(@"C:\Users\Ken\Documents\Visual Studio 2012\Projects\Sounds\f-2.wav");
            /*
            MyUpSoundPlayer = new SoundPlayer(@"revup3.wav");
            MyDownSoundPlayer = new SoundPlayer(@"revdn3.wav");
            MyTimerStartUpSoundPlayer = new SoundPlayer(@"click.wav");
            MyTimerStartDownSoundPlayer = new SoundPlayer(@"f-2.wav");

            SoundOne = new SoundPlayer(@"Natasha\1.wav");
            SoundTwo = new SoundPlayer(@"Natasha\2.wav");
            SoundThree = new SoundPlayer(@"Natasha\3.wav");
            SoundFour = new SoundPlayer(@"Natasha\4.wav");
            SoundFive = new SoundPlayer(@"Natasha\5.wav");
            SoundSix = new SoundPlayer(@"Natasha\6.wav");
            SoundSeven = new SoundPlayer(@"Natasha\7.wav");
            SoundEight = new SoundPlayer(@"Natasha\8.wav");
            SoundNine = new SoundPlayer(@"Natasha\9.wav");
            SoundTen = new SoundPlayer(@"Natasha\10.wav");
            SoundEleven = new SoundPlayer(@"Natasha\11.wav");
            SoundTwelve = new SoundPlayer(@"Natasha\12.wav");
            SoundThirteen = new SoundPlayer(@"Natasha\13.wav");
            SoundFourteen = new SoundPlayer(@"Natasha\14.wav");
            SoundFifeteen = new SoundPlayer(@"Natasha\15.wav");
            SoundSixteen = new SoundPlayer(@"Natasha\16.wav");
            SoundSeventeen = new SoundPlayer(@"Natasha\17.wav");
            SoundEighteen = new SoundPlayer(@"Natasha\18.wav");
            SoundNineteen = new SoundPlayer(@"Natasha\19.wav");
            SoundTwenty = new SoundPlayer(@"Natasha\20.wav");
            SoundThirty = new SoundPlayer(@"Natasha\30.wav");
            SoundForty = new SoundPlayer(@"Natasha\40.wav");
            SoundFifty = new SoundPlayer(@"Natasha\50.wav");
            SoundSixty = new SoundPlayer(@"Natasha\60.wav");
            SoundSeventy = new SoundPlayer(@"Natasha\70.wav");
            SoundEighty = new SoundPlayer(@"Natasha\80.wav");
            SoundNinety = new SoundPlayer(@"Natasha\90.wav");
            SoundHundred = new SoundPlayer(@"Natasha\100.wav");
            SoundTwoHundred = new SoundPlayer(@"Natasha\200.wav");
            SoundThreeHundred = new SoundPlayer(@"Natasha\300.wav");

            SoundA = new SoundPlayer(@"Natasha\a.wav");
            SoundB = new SoundPlayer(@"Natasha\b.wav");
            SoundC = new SoundPlayer(@"Natasha\c.wav");
            SoundD = new SoundPlayer(@"Natasha\d.wav");
            SoundE = new SoundPlayer(@"Natasha\e.wav");
            SoundF = new SoundPlayer(@"Natasha\f.wav");
            SoundG = new SoundPlayer(@"Natasha\g.wav");
            SoundH = new SoundPlayer(@"Natasha\h.wav");
            SoundI = new SoundPlayer(@"Natasha\i.wav");
            SoundJ = new SoundPlayer(@"Natasha\j.wav");
            SoundK = new SoundPlayer(@"Natasha\k.wav");
            SoundL = new SoundPlayer(@"Natasha\l.wav");
            SoundM = new SoundPlayer(@"Natasha\m.wav");
            SoundN = new SoundPlayer(@"Natasha\n.wav");
            SoundO = new SoundPlayer(@"Natasha\o.wav");
            SoundP = new SoundPlayer(@"Natasha\p.wav");
            SoundQ = new SoundPlayer(@"Natasha\q.wav");
            SoundR = new SoundPlayer(@"Natasha\r.wav");
            SoundS = new SoundPlayer(@"Natasha\s.wav");
            SoundT = new SoundPlayer(@"Natasha\t.wav");
            SoundU = new SoundPlayer(@"Natasha\u.wav");
            SoundV = new SoundPlayer(@"Natasha\v.wav");
            SoundW = new SoundPlayer(@"Natasha\w.wav");
            SoundX = new SoundPlayer(@"Natasha\x.wav");
            SoundY = new SoundPlayer(@"Natasha\y.wav");
            SoundZ = new SoundPlayer(@"Natasha\z.wav");
            SoundHigh = new SoundPlayer(@"Natasha\high.wav");
            SoundLow = new SoundPlayer(@"Natasha\low.wav");
            */
        }


        private void mnuWindowCascade_Click(object sender, System.EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void mnuWindowTileVertical_Click(object sender, System.EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void mnuWindowTileHorizontal_Click(object sender, System.EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void mnuFileClose_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.ActiveMdiChild.Name == "frmMChild")
                {
                    frmMChild objfrmMChild = (frmMChild)this.ActiveMdiChild;
                    objfrmMChild.Close();
                }
                else
                {
                    frmSChild objfrmSChild = (frmSChild)this.ActiveMdiChild;
                    objfrmSChild.Close();
                }
            }
            catch
            {
                MessageBox.Show("Error closing files", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void mnuFileExit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void mnuHelpAbout_Click(object sender, System.EventArgs e)
        {
            frmAbout objfrmAbout = new frmAbout();
            objfrmAbout.ShowDialog();
        }

        private void process_interlockings()
        {
            try
            {
                int rungNumber;
                for (int i = 0; i < interlockingOld.Count; i++)
                {
                    ArrayList rungPointer = (ArrayList)interlockingOld[i];
                    rungNumber = findRung(interlockingNew, (string)rungPointer[rungPointer.Count - 1]);
                    TreeNode newNode;

                    try
                    {
                        if (rungNumber == -1) newNode = new TreeNode(rungPointer[0].ToString() + ": " + (string)rungPointer[rungPointer.Count - 1]);
                        else
                        {
                            if (rungNumber != (int)rungPointer[0])
                                newNode = new TreeNode(rungPointer[0].ToString() + " -> " + rungNumber.ToString() + ": "
                                    + (string)rungPointer[rungPointer.Count - 1]);
                            else
                                newNode = new TreeNode(rungPointer[0].ToString() + ": "
                                    + (string)rungPointer[rungPointer.Count - 1]);
                        }
                        if ((int)rungNumber != -1)
                            if (!RungsSame(rungPointer, (ArrayList)interlockingNew[rungNumber - 1])) //Same Name, but differing contacts
                            {
                                newNode.ForeColor = Color.Lime;
                                //RungsSame(rungPointer, (ArrayList)interlockingNew[rungNumber - 1]);
                            }
                        if ((int)rungNumber != -1) //Same name, same contacts, but diffent rung numbers
                            if (rungNumber != (int)rungPointer[0])
                                if (RungsSame(rungPointer, (ArrayList)interlockingNew[rungNumber - 1]))
                                    if ((int)rungNumber != -1) newNode.ForeColor = Color.Black;//Color.MediumSeaGreen;
                        if ((int)rungNumber == -1) //rung removed, (rung not found in new data)
                            newNode.ForeColor = Color.Blue;

                        this.treeView.Nodes.Add(newNode);
                    }
                    catch { MessageBox.Show("Error generating old rungs list, " + i.ToString() + " ", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

                }
                try
                {
                    for (int i = 0; i < interlockingNew.Count; i++)
                    {
                        ArrayList rungPointer = (ArrayList)interlockingNew[i];
                        rungNumber = findRung(interlockingOld, (string)rungPointer[rungPointer.Count - 1]);
                        if (rungNumber == -1)
                        {
                            TreeNode newNode = new TreeNode(rungPointer[0].ToString() + ": " + (string)rungPointer[rungPointer.Count - 1]);
                            newNode.ForeColor = Color.Red; //New rung
                            this.treeView.Nodes.Add(newNode);
                        }
                    }
                }
                catch { MessageBox.Show("Error generating new rungs list", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            catch { MessageBox.Show("Error generating rungs list", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void menuItem4_Click_1(object sender, System.EventArgs e) //Open Old INS file
        {
            AppOpenOldFile();
        }

        private void AppOpenOldFile()
        {
            /// Old File ///////////////////////////////////////
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (interlockingOld.Count != 0)
                {
                    DisposeOldMemory();
                    reloading = true;
                    treeView.Nodes.Clear();
                    reloading = false;
                }
                OldFileName.Text = openFileDialog1.FileName;
                FileName.Text = OldFileName.Text;
                OpenOldFile();
                if (interlockingNew.Count == 0)
                    CommandlineOpenNewFile(FileName.Text);
            }
            this.CloseAll.Visible = true;
        }

        private void CommandlineOpenOldFile(string commandFileName)
        {
            if (interlockingOld.Count != 0)
            {
                //interlockingOld.Clear(); versionRecOld.Clear();
                DisposeOldMemory();
                reloading = true;
                treeView.Nodes.Clear();
                reloading = false;
            }
            OldFileName.Text = commandFileName;//openFileDialog1.FileName;
            FileName.Text = commandFileName;//OldFileName.Text;
            OpenOldFile();

            this.CloseAll.Visible = true;
        }

        private void OpenOldFile()
        {
            try
            {
                string filenameString = FileName.Text.ToString();
                bool housing = false;
                if (filenameString.EndsWith(".ncd", true, ci))
                {
                    fileType = "NCD";
                    installationNameOld = ParseInstallationName();
                    GCSSVersionOld = ParseGCSSVersion();
                    ParseCommentField();
                    ParseVersionRecord(versionRecOld, filenameString);
                    ParseINSRungs(interlockingOld, filenameString);
                    ParseHousings(Housings_Old, timersOld, "NCD", filenameString);
                    //ParseHousings(Housings_Old);
                }
                else if (filenameString.EndsWith(".ins", true, ci))
                {
                    fileType = "INS";
                    installationNameOld = ParseInstallationName();
                    GCSSVersionOld = ParseGCSSVersion();
                    ParseVersionRecord(versionRecOld, filenameString);
                    ParseINSRungs(interlockingOld, filenameString);
                    ParseHousings(Housings_Old, timersOld, "INS", filenameString);
                    housing = true;
                }
                else if (filenameString.EndsWith(".wt2", true, ci))
                {
                    fileType = "WT2";
                    installationNameOld = ParseInstallationName();
                    GCSSVersionOld = ParseGCSSVersion();
                    ParseVersionRecord(versionRecOld, filenameString);
                    ParseWT2Rungs(interlockingOld, filenameString);
                    ParseHousings(Housings_Old, timersOld, "WT2", filenameString);
                }
                else if (filenameString.EndsWith(".ml2", true, ci))
                {
                    fileType = "ML2";
                    installationNameOld = ParseML2InstallationName(FileName.Text);
                    GCSSVersionOld = "";
                    ParseML2Timers(timersOld, filenameString);
                    ParseML2Rungs(interlockingOld, filenameString);
                }
                else if (filenameString.EndsWith(".gn2", true, ci))
                {
                    fileType = "GN2";
                    installationNameOld = ParseGN2InstallationName();
                    GCSSVersionOld = "";
                    ParseML2Timers(timersOld, filenameString);
                    ParseML2Rungs(interlockingOld, filenameString);
                }
                else if (filenameString.EndsWith(".lsv", true, ci))
                {
                    fileType = "LSV";
                    installationNameOld = "";
                    GCSSVersionOld = "";
                    ParseLSVRungs(interlockingOld, filenameString);
                }
                else if (filenameString.EndsWith(".mlk", true, ci))
                {
                    fileType = "MLK";
                    installationNameOld = ParseMLKInstallationName();
                    GCSSVersionOld = "";
                    ParseMLKTimers(timersOld, filenameString);
                    ParseMLKRungs(interlockingOld, filenameString);
                }
                else if (filenameString.EndsWith(".vtl", true, ci))
                {
                    fileType = "VTL";
                    installationNameOld = "";//ParseML2InstallationName();
                    GCSSVersionOld = "";
                    ParseVTLTimers(timersOld, filenameString);
                    ParseVTLRungs(interlockingOld, filenameString);
                }
                else if (filenameString.EndsWith(".nv", true, ci))
                {
                    fileType = "NV";
                    installationNameOld = "";//ParseML2InstallationName();
                    GCSSVersionOld = "";
                    ParseVTLTimers(timersOld, filenameString);
                    ParseVTLRungs(interlockingOld, filenameString);
                }
                else if (filenameString.EndsWith(".TXT", true, ci))
                {
                    fileType = "TXT";
                    installationNameOld = ParseTXTInstallationName(filenameString);
                    GCSSVersionOld = "";
                    //ParseTXTTimers(timersOld, FileName.Text);
                    ParseTXTRungs(interlockingOld, timersOld, filenameString);
                }
                else if (filenameString.EndsWith(".INI", true, ci))
                {
                    fileType = "FEP";
                    //installationNameOld = ParseFEPInstallationName(filenameString);
                    GCSSVersionOld = "";
                    //ParseTXTTimers(timersOld, FileName.Text);
                    ParseFEPRungs(interlockingOld, filenameString);
                }

                this.Text = "(" + OldFileName.Text + ")" + " vs (" + NewFileName.Text + ") - Logic Navigator";
                NewFileText.Text = NewFileName.Text;
                OldFileText.Text = OldFileName.Text;
                statusBar1.Text = "(" + OldFileName.Text + ")" + " vs (" + NewFileName.Text + ")";
                Close_All_Rungs();
                if ((interlockingOld.Count != 0) && (interlockingNew.Count != 0))
                {
                    process_interlockings(); // Populate rungs list
                    DoDatabase(); // Populate rungs database format
                    ShowMenu(housing);
                    ShowRungPane();
                }
            }
            catch { MessageBox.Show("Error opening old file", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void menuItem1_Click(object sender, System.EventArgs e) //Open New INS file
        {
            AppOpenNewFile();
        }

        private void AppOpenNewFile()
        {
            /// New File ///////////////////////////////////////
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                if (interlockingNew.Count != 0)
                {
                    DisposeNewMemory();
                    reloading = true;
                    treeView.Nodes.Clear();
                    reloading = false;
                }
                NewFileName.Text = openFileDialog2.FileName;
                FileName.Text = NewFileName.Text;
                OpenNewFile("");
                if (interlockingOld.Count == 0)
                    CommandlineOpenOldFile(FileName.Text);

            }
            this.CloseAll.Visible = true;
        }

        private void CommandlineOpenNewFile(string commandFileName)
        {
            if (interlockingNew.Count != 0)
            {
                DisposeNewMemory();
                reloading = true;
                treeView.Nodes.Clear();
                reloading = false;
            }
            NewFileName.Text = commandFileName;
            FileName.Text = commandFileName;
            OpenNewFile("");

            this.CloseAll.Visible = true;
        }

        private void OpenNewFile(string prefix)
        {
            try
            {
                bool housing = false;
                string filenameString = FileName.Text.ToString();
                if (filenameString.EndsWith(".ncd", true, ci))
                {
                    fileType = "NCD";
                    installationNameNew = ParseInstallationName();
                    GCSSVersionNew = ParseGCSSVersion();
                    ParseVersionRecord(versionRecNew, filenameString);
                    ParseCommentField();
                    ParseINSRungs(interlockingNew, filenameString);
                    ParseHousings(Housings_New, timersNew, "NCD", filenameString);
                    //ParseNCDtimers(timersNew);
                }
                else if (filenameString.EndsWith(".ins", true, ci))
                {
                    fileType = "INS";
                    installationNameNew = ParseInstallationName();
                    GCSSVersionNew = ParseGCSSVersion();
                    ParseVersionRecord(versionRecNew, filenameString);
                    ParseINSRungs(interlockingNew, filenameString);
                    ParseHousings(Housings_New, timersNew, "INS", filenameString);
                    housing = true;
                }
                else if (filenameString.EndsWith(".wt2", true, ci))
                {
                    fileType = "WT2";
                    installationNameNew = ParseInstallationName();
                    GCSSVersionNew = ParseGCSSVersion();
                    ParseVersionRecord(versionRecNew, filenameString);
                    ParseWT2Rungs(interlockingNew, filenameString);
                    ParseHousings(Housings_New, timersNew, "WT2", filenameString);
                }
                else if (filenameString.EndsWith(".lsv", true, ci))
                {
                    fileType = "LSV";
                    installationNameNew = "";
                    GCSSVersionNew = "";
                    //ParseVersionRecord(versionRecOld);
                    ParseLSVRungs(interlockingNew, filenameString);
                    //ParseHousings(Housings_Old);
                }
                else if (filenameString.EndsWith(".ml2", true, ci))
                {
                    fileType = "ML2";
                    installationNameNew = ParseML2InstallationName(FileName.Text);
                    GCSSVersionNew = "";
                    //ParseVersionRecord(versionRecOld);
                    ParseML2Timers(timersNew, filenameString);
                    ParseML2Rungs(interlockingNew, filenameString);
                    //ParseHousings(Housings_Old);
                }
                else if (filenameString.EndsWith(".gn2", true, ci))
                {
                    fileType = "GN2";
                    installationNameNew = ParseGN2InstallationName();
                    GCSSVersionNew = "";
                    //ParseVersionRecord(versionRecOld);
                    ParseML2Timers(timersNew, filenameString);
                    ParseML2Rungs(interlockingNew, filenameString);
                    //ParseHousings(Housings_Old);
                }
                else if (filenameString.EndsWith(".mlk", true, ci))
                {
                    fileType = "MLK";
                    installationNameNew = ParseMLKInstallationName();
                    GCSSVersionNew = "";
                    //ParseVersionRecord(versionRecOld);
                    ParseMLKTimers(timersNew, filenameString);
                    ParseMLKRungs(interlockingNew, filenameString);
                    //ParseHousings(Housings_Old);
                }
                else if (filenameString.EndsWith(".vtl", true, ci))
                {
                    fileType = "VTL";
                    installationNameNew = "";
                    GCSSVersionNew = "";
                    ParseVTLTimers(timersNew, FileName.Text);
                    ParseVTLRungs(interlockingNew, filenameString);
                }
                else if (filenameString.EndsWith(".nv", true, ci))
                {
                    fileType = "NV";
                    installationNameNew = "";//ParseML2InstallationName();
                    GCSSVersionNew = "";
                    ParseVTLTimers(timersNew, FileName.Text);
                    ParseVTLRungs(interlockingNew, filenameString);
                }
                else if (filenameString.EndsWith(".TXT", true, ci))
                {
                    fileType = "TXT";
                    installationNameOld = ParseTXTInstallationName(filenameString);
                    GCSSVersionOld = "";
                    //ParseTXTTimers(timersOld, FileName.Text);
                    ParseTXTRungs(interlockingNew, timersNew, filenameString);
                }
                else if (filenameString.EndsWith(".INI", true, ci))
                {
                    fileType = "FEP";
                    //installationNameOld = ParseFEPInstallationName(filenameString);
                    GCSSVersionNew = "";
                    //ParseTXTTimers(timersOld, FileName.Text);
                    ParseFEPRungs(interlockingNew, filenameString);
                }
                this.Text = "(" + OldFileName.Text + ")" + " vs (" + NewFileName.Text + ") - Logic Navigator";
                NewFileText.Text = NewFileName.Text;
                OldFileText.Text = OldFileName.Text;
                //addprefixtorungs(prefix);
                statusBar1.Text = "(" + OldFileName.Text + ")" + " vs (" + NewFileName.Text + ")";
                Close_All_Rungs();
                if ((interlockingOld.Count != 0) && (interlockingNew.Count != 0))
                {
                    process_interlockings();
                    DoDatabase();
                    ShowMenu(housing);
                    ShowRungPane();
                }
            }
            catch { MessageBox.Show("Error opening new file", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }


        private void Openst8File(string filenameString)
        {
            try
            {
                if (filenameString.EndsWith(".st8", true, ci))
                {
                    fileType = "st8";
                    ParseSt8(filenameString);
                }
            }
            catch { MessageBox.Show("Error opening file", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

        }

        private void ParseSt8(string filenameString)
        {
            string line = "";
            bool endofSt8File = false;
            bool RungStateMode = false;
            bool SimInputMode = false;

            SR = File.OpenText(filenameString);
            while (((line = SR.ReadLine()) != null) && (endofSt8File != true))//Put logic into a single string
            {
                if (line.LastIndexOf("~~END of ST8 File~~~~~~~~~") != -1)
                    endofSt8File = true;
                else
                {
                    if (line.LastIndexOf("Input States:") != -1)
                    { RungStateMode = false; SimInputMode = true; }
                    if (SimInputMode &&
                        !(line.LastIndexOf("Rung States:") != -1) &&
                        !(line.LastIndexOf("Input States:") != -1) &&
                        !(line.LastIndexOf("~~~~~~~~~~~~~") != -1))
                        if (getCoilIndex(line) == -1)
                            SimInputs.Add(line);

                    if (line.LastIndexOf("Rung States:") != -1)
                    { RungStateMode = true; SimInputMode = false; }
                    if (RungStateMode &&
                        !(line.LastIndexOf("Rung States:") != -1) &&
                        !(line.LastIndexOf("Input States:") != -1) &&
                        !(line.LastIndexOf("~~~~~~~~~~~~~") != -1))
                        HighRungs.Add(line);

                }
            }
            SR.Close();
            TransferHighRungstoCoilStates();
        }


        private void ShowMenu(bool housing)
        {
            this.CloseAll.Visible = true;
            this.mnuFileCloseChild.Visible = true;
            this.Tools.Visible = true;
            //this.mnuFileSep1.Visible = true;
            this.View.Visible = true;
            ShowRungPane();
            for (int i = 1; i < 19; i++)
                this.toolBar1.Buttons[i].Visible = true;
            if (housing) this.toolBar1.Buttons[10].Visible = true;
            else this.toolBar1.Buttons[10].Visible = false;
            this.toolBar1.Buttons[7].Visible = false;
            this.toolBar1.Buttons[0].Visible = false;

        }

        private void HideMenu()
        {
            this.CloseAll.Visible = false;
            this.mnuFileCloseChild.Visible = false;
            this.Tools.Visible = false;
            //this.mnuFileSep1.Visible = false;
            this.View.Visible = false;
            HideRungPane();
            for (int i = 1; i < 19; i++)
                this.toolBar1.Buttons[i].Visible = false;
            this.toolBar1.Buttons[0].Visible = true;
            this.toolBar1.Buttons[3].Visible = true;
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

        private bool RungsSame(ArrayList rung1, ArrayList rung2)
        {
            if (rung1.Count != rung2.Count)
                return false;
            for (int i = 1; i < rung1.Count - 1; i++)
            {
                Contact contact1 = (Contact)rung1[i];
                Contact contact2 = (Contact)rung2[i];
                if (contact1.leftLink != contact2.leftLink)
                    return false;
                if (contact1.bottomLink != contact2.bottomLink)
                    return false;
                if (contact1.topLink != contact2.topLink)
                    return false;
                if (contact1.NormallyClosed != contact2.NormallyClosed)
                    return false;
                if (contact1.x != contact2.x)
                    return false;
                if (contact1.y != contact2.y)
                    return false;
                if (string.Compare(contact1.typeOfCell, contact2.typeOfCell) != 0)
                    return false;
                if (string.Compare(contact1.name, contact2.name) != 0)
                    return false;
            }

            int timerNewnumber = findTimer(timersNew, rung2[rung2.Count - 1].ToString());
            int timerOldnumber = findTimer(timersOld, rung1[rung1.Count - 1].ToString());
            if ((timerNewnumber == -1) && (timerOldnumber == -1)) return true;
            if (timerNewnumber == -1)
                return false;
            if (timerOldnumber == -1)
                return false;
            ML2Timer timerOldElement = (ML2Timer)timersOld[timerOldnumber];
            ML2Timer timerNewElement = (ML2Timer)timersNew[timerNewnumber];
            if ((timerOldElement.clearTime != timerNewElement.clearTime) ||
                (timerOldElement.setTime != timerNewElement.setTime))
                return false;
            else return true;
        }

        private void ParseMLKRungs(ArrayList interlocking, string filenameString)
        {
            string line; string code = ""; string frag = ""; int counter = 0; string lineofcode = "";
            string rungName; int RungNumber; int coils = 0; string rungNameExtended = ""; bool dumpline = false;
            string charlookahead = ""; string charlookahead2 = "";
            bool endOfLadder = false; bool commentMode = false; bool commentLineMode = false;
            SR = File.OpenText(filenameString);

            while (((line = SR.ReadLine()) != null) && (endOfLadder != true))//Put logic into a single string
            {
                commentLineMode = false;
                if ((commentMode == false) && (commentLineMode == false))
                    line = TranslateMLline(line);
                dumpline = false;
                lineofcode = "";
                for (int index = 0; index < line.Length; index++)
                {
                    if (index < line.Length - 1)
                    {
                        charlookahead2 = line.Substring(index, 1);
                        if (charlookahead2 == "%") commentMode = true; // Find start of comments
                        if (charlookahead2 == "//") commentLineMode = true;
                    }
                    if ((commentMode == true) || (commentLineMode == true))
                    {
                        if (line.IndexOf("\\") == -1)
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
                            if (commentMode == false)
                                lineofcode += charlookahead;
                        //if (index > 0)
                        if (line.Substring(index, 1) == "\\")
                            commentMode = false;
                    }
                }
                code += lineofcode;
                if (line.LastIndexOf("END LOGIC", sc) != -1)
                    endOfLadder = true;
                statusBar1.Text = "File: " + filenameString + ", Reading Line: " + counter++.ToString();
            }
            SR.Close();
            int logicBeginIndex = code.LastIndexOf("BEGIN", sc) + 5;//snip off anything before logic begin
            string logic = code.Substring(logicBeginIndex, code.Length - logicBeginIndex);
            int logicEndIndex = logic.LastIndexOf("END", sc);//snip off anything after end logic
            logic = logic.Substring(0, logicEndIndex);
            string RungLogic = "";
            RungNumber = 1;
            while (logic.IndexOf("ASSIGN", sc) != -1)
            {
                RungLogic = logic.Substring(logic.IndexOf("ASSIGN", sc) + 6, logic.IndexOf(";") - (6 + logic.IndexOf("ASSIGN", sc)));
                logicBeginIndex = logic.IndexOf(";") + 1;//snip off rung just scanned, until they are all scanned in
                logic = logic.Substring(logicBeginIndex, logic.Length - logicBeginIndex);
                rungName = RungLogic.Substring(RungLogic.IndexOf("=") + 1, RungLogic.Length - (1 + RungLogic.IndexOf("=")));
                RungLogic = RungLogic.Substring(0, RungLogic.IndexOf("="));
                coils = CountCommas(rungName);
                rungNameExtended = rungName;
                for (int i = 0; i < coils; i++)
                {
                    ArrayList runglist = new ArrayList();
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
                    statusBar1.Text = "Forming Rung: " + RungNumber + " - " + rungName.ToString();
                    interlocking.Add(rung);
                }
            }
        }

        private void ParseML2Timers(ArrayList timers, string filename)
        {
            string line = "";
            try
            {
                string code = ""; int counter = 0; string lineofcode = ""; string timerstring = "";
                bool dumpline = false;
                string charlookahead = ""; string charlookahead2 = "";
                bool endOfLadder = false; bool startOfTimers = false; bool commentMode = false; bool commentLineMode = false;
                SR = File.OpenText(filename);
                while (((line = SR.ReadLine()) != null) && (endOfLadder != true)) //Put logic into a single string
                {
                    line = " " + line + " ";
                    line = line.Replace("FIXED", "");
                    commentLineMode = false;
                    if (counter == 700)
                        dumpline = false;
                    line = TranslateMLline(line);
                    dumpline = false;
                    lineofcode = ""; timerstring = "";
                    for (int index = 0; index < line.Length; index++)
                    {
                        if (index < line.Length - 1)
                        {
                            charlookahead2 = line.Substring(index, 2);
                            if (charlookahead2 == "/*") commentMode = true; // Find start of comments
                            if (charlookahead2 == "//")
                                commentLineMode = true;
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
                            timerstring += charlookahead;
                            if ((charlookahead != " ") && (charlookahead != "\r\n") && (charlookahead != "\t"))
                                //Remove carraige returns, spaces & tabs
                                if (!commentMode)
                                    lineofcode += charlookahead;
                            if (index > 0)
                                if (line.Substring(index - 1, 2) == "*/") commentMode = false;
                        }
                    }
                    if (startOfTimers)
                    {
                        code += lineofcode;
                        if (lineofcode.LastIndexOf("LOG", sc) != -1)
                            endOfLadder = true;
                        if (lineofcode.LastIndexOf("BEGIN", sc) != -1)
                            endOfLadder = true;
                        if (lineofcode.LastIndexOf("CONSTANTS", sc) != -1)
                            endOfLadder = true; 
                        if (lineofcode.LastIndexOf("CONFIGURATION", sc) != -1)
                            endOfLadder = true;
                    }
                    if (timerstring.IndexOf(" TIMER ", sc) != -1)
                        startOfTimers = true;
                    statusBar1.Text = "File: " + FileName.Text + ", Reading Line: " + counter++.ToString();
                }
                SR.Close();
                string timer = code;

                while (timer.IndexOf(":") != -1)
                {

                    string setstring = timer.Substring(timer.IndexOf(":"), timer.IndexOf("CLEAR=", sc) - timer.IndexOf(":"));
                    string setstringamount = setstring.Substring(setstring.IndexOf("SET=", sc) + 4, setstring.IndexOf(":", 1) - (4 + setstring.IndexOf("SET=", sc)));
                    int setTime = Convert.ToInt32(setstringamount);
                    string settimeunit = setstring.Substring(setstring.IndexOf(":", 1) + 1);

                    string clearstring = timer.Substring(timer.IndexOf("CLEAR=", sc) + 6, timer.IndexOf(";") - (6 + timer.IndexOf("CLEAR=", sc)));
                    string clearstringamount = clearstring.Substring(0, clearstring.IndexOf(":"));
                    int clearTime = Convert.ToInt32(clearstringamount);
                    string cleartimeunit = clearstring.Substring(clearstring.IndexOf(":") + 1);

                    string timerlist = timer.Substring(0, timer.IndexOf(":", 1));
                    while (timerlist.IndexOf(",") != -1)
                    {
                        string timerName = timerlist.Substring(0, timerlist.IndexOf(","));

                        Addtimer(timers, timerName, setTime, settimeunit, clearTime, cleartimeunit);
                        timerlist = timerlist.Substring(timerlist.IndexOf(",") + 1);
                    }
                    Addtimer(timers, timerlist, setTime, settimeunit, clearTime, cleartimeunit);
                    timer = timer.Substring(timer.IndexOf(";") + 1);
                    if (timerlist == "28.1M.U1PR.G.C")
                        timer = timer;

                }
            }
            catch { MessageBox.Show("Problem parsing ML2 timers, line:" + line.ToString() + ", try compiling the ML2 for a diagnosis of errors", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }


        private void ParseVTLTimers(ArrayList timers, string filename)
        {
            string line = "", timestring = "";
            try
            {
                string code = ""; string lineofcode = ""; string token = "";
                bool endOfLadder = false; bool commentMode = false; bool commentLineMode = false;
                int timerstart, timerend = 0;
                string timerstartstring = "";//, timestring = "";
                int minutes = 0, seconds = 0, hours = 0;
                int setTime = 0;
                string settimeunit = "";
                string timeunit = "seconds";
                bool minutefound = false;
                SR = File.OpenText(filename);
                while (((line = SR.ReadLine()) != null) && (endOfLadder != true))//Put logic into a single string
                {
                    commentLineMode = false;

                    if (line.LastIndexOf("VSL RESET") != -1)
                        statusBar1.Text = "made it";
                    if (line.LastIndexOf("END BOOLEAN EQUATION SECTION") != -1)
                        endOfLadder = true;
                    else
                    {
                        lineofcode = "";
                        for (int index = 0; index < line.Length; index++)
                        {
                            token = line.Substring(index, 1);
                            if (index < line.Length)// - 1)
                                if ((index == 0) && line.Substring(index, 1) == "*")
                                    commentLineMode = true; // Remove comments                        
                            if ((commentMode == false) && (commentLineMode == false))
                                if ((line.Substring(index, 1) != " ") && (line.Substring(index, 1) != "\r\n") &&
                                    (line.Substring(index, 1) != "\t"))//Remove carriage returns, spaces & tabs
                                    lineofcode += line.Substring(index, 1);
                            if (index > 0)
                                if (line.Substring(index - 1, 2) == "*/") commentMode = false;
                        }
                        code += lineofcode;
                    }
                }

                SR.Close();
                string timer = code;

                while (timer.IndexOf("TIMEDELAY=") != -1)
                {
                    timerstart = timer.IndexOf("TIMEDELAY=");
                    timerstartstring = timer.Substring(timerstart);
                    timerend = timerstartstring.IndexOf("=", 11);
                    timestring = timerstartstring.Substring(0, timerend);
                    minutes = 0; seconds = 0; hours = 0;
                    if (timestring.IndexOf("HOURS") != -1) timeunit = "HRS";
                    if (timestring.IndexOf("MINUTES") != -1) timeunit = "MIN";
                    if (timestring.IndexOf("SECOND") != -1) timeunit = "SEC";
                    /*if (timestring.IndexOf("HOURS") != -1)
                    {
                        hours = Convert.ToInt32(timestring.Substring(0, timestring.IndexOf("HOURS,")));
                    }*/
                    if (timestring.IndexOf("MINUTES") != -1)
                    {
                        if (timestring.IndexOf("SECOND") == -1)
                            minutes = Convert.ToInt32(timestring.Substring(10, timestring.IndexOf("MINUTES") - 10));
                    }
                    if (timestring.IndexOf("SECOND") != -1)
                    {
                        if (timestring.IndexOf("MINUTE") != -1)
                        {
                            //Four different ways of expressing minutes!
                            minutefound = false;
                            if ((timestring.IndexOf("MINUTES,") != -1) && !minutefound)
                            {
                                minutes = Convert.ToInt32(timestring.Substring(10, timestring.IndexOf("MINUTES") - 10));
                                seconds = Convert.ToInt32(timestring.Substring(timestring.IndexOf("MINUTES") + 8,
                                    timestring.IndexOf("SECOND") - (timestring.IndexOf("MINUTES") + 8)));
                                seconds = seconds + minutes * 60;
                                minutefound = true;
                            }
                            if ((timestring.IndexOf("MINUTE,") != -1) && !minutefound)
                            {
                                minutes = Convert.ToInt32(timestring.Substring(10, timestring.IndexOf("MINUTE") - 10));
                                seconds = Convert.ToInt32(timestring.Substring(timestring.IndexOf("MINUTE") + 7,
                                    timestring.IndexOf("SECOND") - (timestring.IndexOf("MINUTE") + 7)));
                                seconds = seconds + minutes * 60;
                                minutefound = true;
                            }
                            if ((timestring.IndexOf("MINUTES") != -1) && !minutefound)
                            {
                                minutes = Convert.ToInt32(timestring.Substring(10, timestring.IndexOf("MINUTES") - 10));
                                seconds = Convert.ToInt32(timestring.Substring(timestring.IndexOf("MINUTES") + 7,
                                    timestring.IndexOf("SECOND") - (timestring.IndexOf("MINUTES") + 7)));
                                seconds = seconds + minutes * 60;
                                minutefound = true;
                            }
                            if ((timestring.IndexOf("MINUTE") != -1) && !minutefound)
                            {
                                minutes = Convert.ToInt32(timestring.Substring(10, timestring.IndexOf("MINUTES") - 10));
                                seconds = Convert.ToInt32(timestring.Substring(timestring.IndexOf("MINUTES") + 6,
                                    timestring.IndexOf("SECOND") - (timestring.IndexOf("MINUTES") + 6)));
                                seconds = seconds + minutes * 60;
                                minutefound = true;
                            }
                        }
                        else
                        {
                            seconds = Convert.ToInt32(timestring.Substring(10, timestring.IndexOf("SECOND") - 10));
                        }
                    }
                    if (timeunit == "MIN")
                        setTime = Convert.ToInt32(minutes);
                    else setTime = Convert.ToInt32(seconds);

                    settimeunit = timeunit;
                    string timerlist = timestring.Substring(timestring.IndexOf("BOOL") + 4);

                    Addtimer(timers, timerlist, setTime, settimeunit, 0, "SEC");
                    timer = timer.Substring(timer.IndexOf("TIMEDELAY") + 1);
                }
            }
            catch
            {
                MessageBox.Show("Problem parsing VTL timers, line:" + timestring.ToString() + ", try compiling the VTL for a diagnosis of errors", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ParseTXTTimers(ArrayList timers, string filename)
        {
            string line = "";
            try
            {
                /*
                string code = ""; int counter = 0; string lineofcode = ""; string timerstring = "";
                bool dumpline = false;
                string charlookahead = ""; string charlookahead2 = "";
                bool endOfLadder = false; bool startOfTimers = false; bool commentMode = false; bool commentLineMode = false;
                SR = File.OpenText(filename);
                while (((line = SR.ReadLine()) != null) && (endOfLadder != true)) //Put logic into a single string
                {
                    line = " " + line + " ";
                    commentLineMode = false;
                    if (counter == 700)
                        dumpline = false;
                    if ((commentMode == false) && (commentLineMode == false))
                        line = TranslateMLline(line);
                    dumpline = false;
                    lineofcode = ""; timerstring = "";
                    for (int index = 0; index < line.Length; index++)
                    {
                        if (index < line.Length - 1)
                        {
                            charlookahead2 = line.Substring(index, 2);
                            if (charlookahead2 == "/*") commentMode = true; // Find start of comments
                            if (charlookahead2 == "//")
                                commentLineMode = true;
                        }
                        if ((commentMode == true) || (commentLineMode == true))
                        {
                            //if (line.IndexOf("*//*") == -1)
                            /*{
                                dumpline = true; //In Comment mode and not end comment in the line
                                index = line.Length;
                            }
                        }
                        if (!dumpline)
                        {
                            charlookahead = line.Substring(index, 1);
                            timerstring += charlookahead;
                            if ((charlookahead != " ") && (charlookahead != "\r\n") && (charlookahead != "\t"))
                                //Remove carraige returns, spaces & tabs
                                lineofcode += charlookahead;
                            if (index > 0)
                                if (line.Substring(index - 1, 2) == "*//*") commentMode = false;
                        }
                    }
                    if (startOfTimers)
                    {
                        code += lineofcode;
                        if (lineofcode.LastIndexOf("LOG", sc) != -1)
                            endOfLadder = true;
                        if (lineofcode.LastIndexOf("BEGIN", sc) != -1)
                            endOfLadder = true;
                    }
                    if (timerstring.IndexOf(" TIMER ", sc) != -1)
                        startOfTimers = true;
                    statusBar1.Text = "File: " + FileName.Text + ", Reading Line: " + counter++.ToString();
                }
                SR.Close();
                string timer = code;

                while (timer.IndexOf(":") != -1)
                {

                    string setstring = timer.Substring(timer.IndexOf(":"), timer.IndexOf("CLEAR=", sc) - timer.IndexOf(":"));
                    string setstringamount = setstring.Substring(setstring.IndexOf("SET=", sc) + 4, setstring.IndexOf(":", 1) - (4 + setstring.IndexOf("SET=", sc)));
                    int setTime = Convert.ToInt32(setstringamount);
                    string settimeunit = setstring.Substring(setstring.IndexOf(":", 1) + 1);

                    string clearstring = timer.Substring(timer.IndexOf("CLEAR=", sc) + 6, timer.IndexOf(";") - (6 + timer.IndexOf("CLEAR=", sc)));
                    string clearstringamount = clearstring.Substring(0, clearstring.IndexOf(":"));
                    int clearTime = Convert.ToInt32(clearstringamount);
                    string cleartimeunit = clearstring.Substring(clearstring.IndexOf(":") + 1);


                    string timerlist = timer.Substring(0, timer.IndexOf(":", 1));
                    while (timerlist.IndexOf(",") != -1)
                    {
                        string timerName = timerlist.Substring(0, timerlist.IndexOf(","));

                        Addtimer(timers, timerName, setTime, settimeunit, clearTime, cleartimeunit);
                        timerlist = timerlist.Substring(timerlist.IndexOf(",") + 1);
                    }
                    Addtimer(timers, timerlist, setTime, settimeunit, clearTime, cleartimeunit);
                    timer = timer.Substring(timer.IndexOf(";") + 1);
                }*/
            }
            catch { MessageBox.Show("Problem parsing ML2 timers, line:" + line.ToString() + ", try compiling the ML2 for a diagnosis of errors", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void ParseMLKTimers(ArrayList timers, string filename)
        {

            string line = "";
            try
            {
                string code = ""; int counter = 0; string lineofcode = ""; string timerstring = "";
                bool dumpline = false;
                string charlookahead = ""; string charlookahead2 = "";
                bool endOfLadder = false; bool startOfTimers = false; bool commentMode = false; bool commentLineMode = false;
                SR = File.OpenText(filename);

                while (((line = SR.ReadLine()) != null) && (endOfLadder != true)) //Put logic into a single string
                {
                    line = " " + line + " ";
                    commentLineMode = false;
                    if (counter == 700)
                        dumpline = false;
                    if ((commentMode == false) && (commentLineMode == false))
                        line = TranslateMLline(line);
                    dumpline = false;
                    lineofcode = ""; timerstring = "";
                    for (int index = 0; index < line.Length; index++)
                    {
                        if (index < line.Length - 1)
                        {
                            charlookahead2 = line.Substring(index, 1);
                            if (charlookahead2 == "%") commentMode = true; // Find start of comments
                            if (charlookahead2 == "//")
                                commentLineMode = true;
                        }
                        if ((commentMode == true) || (commentLineMode == true))
                        {
                            if (line.IndexOf("\\") == -1)
                            {
                                dumpline = true; //In Comment mode and not end comment in the line
                                index = line.Length;
                            }
                        }
                        if (!dumpline)
                        {
                            charlookahead = line.Substring(index, 1);
                            timerstring += charlookahead;
                            if ((charlookahead != " ") && (charlookahead != "\r\n") && (charlookahead != "\t"))
                                //Remove carraige returns, spaces & tabs
                                lineofcode += charlookahead;
                            //if (index > 0)
                            if (line.Substring(index, 1) == "\\") commentMode = false;
                        }
                    }
                    if (startOfTimers)
                    {
                        code += lineofcode;
                        if (lineofcode.LastIndexOf("LOG", sc) != -1)
                            endOfLadder = true;
                        if (lineofcode.LastIndexOf("BEGIN", sc) != -1)
                            endOfLadder = true;
                    }
                    if (timerstring.IndexOf(" TIMER ", sc) != -1)
                        startOfTimers = true;
                    statusBar1.Text = "File: " + FileName.Text + ", Reading Line: " + counter++.ToString();
                }
                SR.Close();
                string timer = code;

                while (timer.IndexOf(":") != -1)
                {

                    string setstring = timer.Substring(timer.IndexOf(":"), timer.IndexOf("CLEAR=", sc) - timer.IndexOf(":"));
                    string setstringamount = setstring.Substring(setstring.IndexOf("SET=", sc) + 4, setstring.IndexOf(":", 1) - (4 + setstring.IndexOf("SET=", sc)));
                    int setTime = Convert.ToInt32(setstringamount);
                    string settimeunit = setstring.Substring(setstring.IndexOf(":", 1) + 1);

                    string clearstring = timer.Substring(timer.IndexOf("CLEAR=", sc) + 6, timer.IndexOf(";") - (6 + timer.IndexOf("CLEAR=", sc)));
                    string clearstringamount = clearstring.Substring(0, clearstring.IndexOf(":"));
                    int clearTime = Convert.ToInt32(clearstringamount);
                    string cleartimeunit = clearstring.Substring(clearstring.IndexOf(":") + 1);


                    string timerlist = timer.Substring(0, timer.IndexOf(":", 1));
                    while (timerlist.IndexOf(",") != -1)
                    {
                        string timerName = timerlist.Substring(0, timerlist.IndexOf(","));

                        Addtimer(timers, timerName, setTime, settimeunit, clearTime, cleartimeunit);
                        timerlist = timerlist.Substring(timerlist.IndexOf(",") + 1);
                    }
                    Addtimer(timers, timerlist, setTime, settimeunit, clearTime, cleartimeunit);
                    timer = timer.Substring(timer.IndexOf(";") + 1);
                }
            }
            catch { MessageBox.Show("Problem parsing MLk timers, line:" + line.ToString() + ", try compiling the MLK for a diagnosis of errors", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }


        private void Addtimer(ArrayList timers, string timerName, int setTime, string setTimeunit, int clearTime, string cleartimeunit)
        {
            ML2Timer timeRec = new ML2Timer();
            timeRec.timerName = timerName;
            timeRec.setTime = setTime.ToString() + setTimeunit;
            timeRec.clearTime = clearTime.ToString() + cleartimeunit;
            timeRec.setStartTime = DateTime.Now;
            timeRec.clearStartTime = DateTime.Now;
            timers.Add(timeRec);
        }

        private void ParseML2Rungs(ArrayList interlocking, string filename)
        {
            string line = "", rungnamedebug = "";
            try
            {
                string code = ""; string frag = ""; int counter = 0; string lineofcode = "";
                string rungName; int RungNumber; int coils = 0; string rungNameExtended = ""; bool dumpline = false;
                string charlookahead = ""; string charlookahead2 = "";
                bool endOfLadder = false; bool commentMode = false; bool commentLineMode = false;
                SR = File.OpenText(filename);
                while (((line = SR.ReadLine()) != null) && (endOfLadder != true))//Put logic into a single string
                {
                    commentLineMode = false;
                    //if ((commentMode == false) && (commentLineMode == false))
                    line = TranslateMLline(line); // Convert the and's, or's, to's to *'s +'s and ='s

                    //if (line.IndexOf("GG-YY") != -1)
                    //line = line;
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
                    if (line.LastIndexOf("END LOGIC", sc) != -1)
                        endOfLadder = true;
                    statusBar1.Text = "File: " + FileName.Text + ", Reading Line: " + counter++.ToString();
                }
                SR.Close();
                int logicBeginIndex = code.LastIndexOf("LOGICBEGIN", sc) + 10;//snip off anything before logic begin
                string logic = code.Substring(logicBeginIndex, code.Length - logicBeginIndex);
                int logicEndIndex = logic.LastIndexOf("ENDLOGIC", sc);//snip off anything after end logic
                logic = logic.Substring(0, logicEndIndex);
                string RungLogic = "";
                RungNumber = 1;
                int linecount = 0;
                while ((logic.IndexOf("ASSIGN", sc) != -1) || (logic.IndexOf("NV.ASSIGN", sc) != -1))
                {
                    linecount++;
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
                        statusBar1.Text = "Forming Rung: " + RungNumber + " - " + rungName.ToString();
                        interlocking.Add(TakeOutBrackets(rung));
                    }
                }
            }
            catch { MessageBox.Show("Problem parsing ML2 file, line: " + line.ToString() + ", " + rungnamedebug.ToString() + ", try compiling the ML2 for a diagnosis of errors", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void ParseFEPRungs(ArrayList interlocking, string filename)
        {
            string line = "", rungnamedebug = "";
            try
            {
                string code = ""; string frag = ""; int counter = 0; string lineofcode = "";
                string rungName; int RungNumber; int coils = 0; string rungNameExtended = ""; bool dumpline = false;
                string charlookahead = ""; string charlookahead2 = "";
                bool endOfLadder = false; bool commentMode = false; bool commentLineMode = false;
                SR = File.OpenText(filename);
                while (((line = SR.ReadLine()) != null) && (endOfLadder != true))//Put logic into a single string
                {
                    commentLineMode = false;
                    //if ((commentMode == false) && (commentLineMode == false))
                    line = TranslateMLline(line); // Convert the and's, or's, to's to *'s +'s and ='s

                    //if (line.IndexOf("GG-YY") != -1)
                    //line = line;
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
                    if (line.LastIndexOf("END LOGIC", sc) != -1)
                        endOfLadder = true;
                    statusBar1.Text = "File: " + FileName.Text + ", Reading Line: " + counter++.ToString();
                }
                SR.Close();
                int logicBeginIndex = code.IndexOf("<EXP>", sc) + 5;//snip off anything before the first <EXP>
                string logic = code.Substring(logicBeginIndex, code.Length - logicBeginIndex);
                int logicEndIndex = logic.LastIndexOf("</EXP>", sc);//snip off anything after end logic
                logic = logic.Substring(0, logicEndIndex);
                logic = logic.Replace("<EQUATION>", "ASSIGN");
                logic = logic.Replace("</EQUATION>", "=");
                logic = logic.Replace("<EXP_RES_ADDR>", "");
                logic = logic.Replace("</EXP_RES_ADDR>", ";");
                logic = logic.Replace("<EXP>", "");
                logic = logic.Replace("</EXP>", "");
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
                        //if (RungLogic.IndexOf("(GG-YY)*(br)w") != -1)
                        // RungLogic = RungLogic;

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
                        statusBar1.Text = "Forming Rung: " + RungNumber + " - " + rungName.ToString();
                        interlocking.Add(TakeOutBrackets(rung));
                    }
                }
            }
            catch { MessageBox.Show("Problem parsing ML2 file, line: " + line.ToString() + ", " + rungnamedebug.ToString() + ", try compiling the ML2 for a diagnosis of errors", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        /*
        private void ParseML2Rungs(ArrayList interlocking, string filename)
        {
            string line = "", rungnamedebug = "";
            try
            {
                string code = ""; string frag = ""; int counter = 0; string lineofcode = "";
                string rungName; int RungNumber; int coils = 0; string rungNameExtended = ""; bool dumpline = false;
                string charlookahead = ""; string charlookahead2 = "";
                bool endOfLadder = false; bool commentMode = false; bool commentLineMode = false;
                SR = File.OpenText(filename);
                while (((line = SR.ReadLine()) != null) && (endOfLadder != true))//Put logic into a single string
                {
                    commentLineMode = false;
                    if ((commentMode == false) && (commentLineMode == false))
                        line = TranslateMLline(line); // Convert the and's, or's, to's to *'s +'s and ='s

                    //if (line.IndexOf("GG-YY") != -1)
                        //line = line;
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
                            if (line.IndexOf("") == -1)
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
                                if (line.Substring(index - 1, 2) == "") commentMode = false;
                        }
                    }
                    code += lineofcode;
                    if (line.LastIndexOf("END LOGIC", sc) != -1)
                        endOfLadder = true;
                    statusBar1.Text = "File: " + FileName.Text + ", Reading Line: " + counter++.ToString();
                }
                SR.Close();
                int logicBeginIndex = code.LastIndexOf("LOGICBEGIN", sc) + 10;//snip off anything before logic begin
                string logic = code.Substring(logicBeginIndex, code.Length - logicBeginIndex);
                int logicEndIndex = logic.LastIndexOf("ENDLOGIC", sc);//snip off anything after end logic
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

                    while(rungName.LastIndexOf("{StartBracket}", sc) != -1)
                        rungName = rungName.Substring(0, rungName.LastIndexOf("{StartBracket}", sc)) + "(" + rungName.Substring(rungName.LastIndexOf("{StartBracket}", sc) + 14);
                    while(rungName.LastIndexOf("{EndBracket}", sc) != -1)
                        rungName = rungName.Substring(0, rungName.LastIndexOf("{EndBracket}", sc)) + ")" + rungName.Substring(rungName.LastIndexOf("{EndBracket}", sc) + 12);  

                    RungLogic = RungLogic.Substring(0, RungLogic.IndexOf("="));
                    
                    coils = CountCommas(rungName);
                    rungNameExtended = rungName;
                    rungnamedebug = rungName;

                    for (int i = 0; i < coils; i++)
                    {
                        ArrayList runglist = new ArrayList();
                        //if (RungLogic.IndexOf("(GG-YY)*(br)w") != -1)
                           // RungLogic = RungLogic;

                        RungLogic = ProcessBrackets(RungLogic);

                        ParseML2Rung(runglist, RungLogic);
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
                        statusBar1.Text = "Forming Rung: " + RungNumber + " - " + rungName.ToString();
                        interlocking.Add(TakeOutBrackets(rung));
                    }
                }
            }
            catch { MessageBox.Show("Problem parsing ML2 file, line: " + line.ToString() + ", " + rungnamedebug.ToString() + ", try compiling the ML2 for a diagnosis of errors", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }*/

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



        public string TranslateMLline(string lineread)
        {


            ////////////////////////////////////////////////////////////

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
            //while ((line.IndexOf(")", sc) != -1) || (line.IndexOf("(", sc) != -1))
            //{
            //    int endbracket = line.IndexOf(")", sc); 
            //    int startbracket = -1;

            //    if(endbracket != -1) startbracket = line.Substring(0, endbracket).LastIndexOf("(",sc);                               
            //    else startbracket = line.LastIndexOf("(", sc);
            //    if ((endbracket != -1) || (startbracket != -1))
            //    {
            //        if(endbracket != -1)
            //        {                                        
            //            bool real = false;
            //            if (endbracket + 1 < line.Length)
            //            {
            //                if ((line.Substring(endbracket + 1, 1) == " "))
            //                    real = true;
            //                if (!real)
            //                {
            //                    string inspect1 = StripWhiteSpace(line.Substring(endbracket + 1), 1).Substring(0,1);
            //                    if ((inspect1.Substring(0, 1) == "*") || (inspect1.Substring(0, 1) == "+") ||
            //                        (inspect1.Substring(0, 1) == "=") || (inspect1.Substring(0, 1) == "~") || (inspect1.Substring(0, 1) == ")"))
            //                        real = true;
            //                    if (endbracket + 3 < line.Length)
            //                    {
            //                        string inspect3 = StripWhiteSpace(line.Substring(endbracket + 1), 0).Substring(0,3);
            //                        if ((inspect3.IndexOf("and", sc) != -1) || (inspect3.IndexOf("or", sc) != -1) ||
            //                            (inspect3.IndexOf("to", sc) != -1) || (inspect3.IndexOf("not", sc) != -1))
            //                            real = true;
            //                    }
            //                }
            //            }
            //            if (!real)
            //            {
            //                line = line.Substring(0, endbracket) + "{EndBracket}" + line.Substring(endbracket + 1);
            //                if(startbracket != -1)
            //                    line = line.Substring(0, startbracket) + "{StartBracket}" + line.Substring(startbracket + 1);                            
            //            }
            //            else
            //            {
            //                line = line.Substring(0, endbracket) + "{RealEndBracket}" + line.Substring(endbracket + 1);
            //                if (startbracket != -1)                            
            //                    line = line.Substring(0, startbracket) + "{RealStartBracket}" + line.Substring(startbracket + 1);

            //            }
            //        }
            //        else
            //            line = line.Substring(0, startbracket) + "{RealStartBracket}" + line.Substring(startbracket + 1);
            //    }
            //}    

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

        private string StripWhiteSpace(string line, int startpoint)
        {
            string output = ""; string character = "";
            for (int i = 0; i < line.Length; i++)
            {
                character = line.Substring(i, 1);
                if ((((character != " ") && (character != "\r") && (character != "\n") && (character != "\t"))) || (i < startpoint))
                    output = output + character;
            }
            return (output);
        }

        private void ParseLSVRungs(ArrayList interlocking, string filenameString)
        {

            string line; string code = ""; string frag = ""; string token = "";
            string rungName; int RungNumber; int coils = 0; string rungNameExtended = ""; string lineofcode;
            int nextApp = 0; int nextTime = 0; int nextBool = 0; int endRung = 0; int locBool = 0;
            bool endOfLadder = false; bool commentMode = true; bool commentLineMode = false;

            SR = File.OpenText(filenameString);
            while (((line = SR.ReadLine()) != null) && (endOfLadder != true))//Put logic into a single string
            {
                commentLineMode = false;

                if ((commentMode == false) && (commentLineMode == false))
                    if (line.LastIndexOf(" CH1 ") != -1)
                        line = line.Substring(0, line.LastIndexOf(" CH1 ") - 4) + " BOOL " +
                             line.Substring(line.LastIndexOf(" CH1 ") + 4);
                if (line.LastIndexOf("TIME DELAY") != -1)
                    statusBar1.Text = "made it";
                if (line.LastIndexOf("CSE/CSEX") != -1)
                    endOfLadder = true;
                else
                {
                    if ((line.IndexOf("GENERAL") == -1) &&
                       (line.IndexOf("PAGE NO") == -1) &&
                       (line.IndexOf("SASIB GROUP") == -1) &&
                       (line.IndexOf("RUN DATE") == -1) &&
                       (line.IndexOf("COPYRIGHT GRS") == -1) &&
                       (line.IndexOf("ADV RUN TIME") == -1) &&
                       (line.IndexOf("BOOLEAN EXPRESSION DESCRIPTION ") == -1) &&
                       (line.IndexOf("CONTINUED") == -1) &&
                       (line.IndexOf("TIME DELAY") == -1))
                    {
                        lineofcode = "";
                        for (int index = 0; index < line.Length; index++)
                        {
                            token = line.Substring(index, 1);
                            if (index < line.Length)// - 1)
                                if ((index == 0) && line.Substring(index, 1) == "*")
                                    commentLineMode = true; // Remove comments                        
                            if ((commentMode == false) && (commentLineMode == false))
                                if ((line.Substring(index, 1) != " ") && (line.Substring(index, 1) != "\r\n") &&
                                    (line.Substring(index, 1) != "\t"))//Remove carriage returns, spaces & tabs
                                    lineofcode += line.Substring(index, 1);
                            if (index > 29)
                                if (line.Substring(index - 29, 30) == "BOOLEAN EXPRESSION DESCRIPTION")
                                    commentMode = false;
                        }
                        code += lineofcode;
                    }
                }
            }
            SR.Close();
            //int logicBeginIndex = code.LastIndexOf("BOOLEANEQUATIONSECTION") + 22;//snip off anything before application
            int logicBeginIndex = 1;//code.IndexOf("BOOLEANEQUATIONSECTION") + 22;//snip off anything before application

            string logic = code.Substring(logicBeginIndex, code.Length - logicBeginIndex).Replace(".N.", "~");
            //int logicEndIndex = logic.LastIndexOf("ENDLOGIC");//snip off anything after end logic
            //logic = logic.Substring(0, logicEndIndex);
            string RungLogic = "";
            RungNumber = 1;

            while (logic.IndexOf("BOOL") != -1)
            {
                locBool = logic.IndexOf("BOOL");
                endRung = logic.Length;
                nextBool = logic.IndexOf("BOOL", locBool + 1);
                if ((nextBool > locBool) && (endRung > nextBool)) endRung = nextBool;
                nextApp = logic.IndexOf("APPLICATION", locBool);
                if ((nextApp > locBool) && (endRung > nextApp)) endRung = nextApp;
                nextTime = logic.IndexOf("TIMEDELAY", locBool);
                if ((nextTime > locBool) && (endRung > nextTime)) endRung = nextTime;
                RungLogic = logic.Substring(locBool + 4, endRung - (4 + locBool));
                rungName = RungLogic.Substring(0, RungLogic.IndexOf("="));
                logic = logic.Substring(endRung, logic.Length - endRung);//snip off rung just scanned, until they are all scanned in

                RungLogic = RungLogic.Substring(RungLogic.IndexOf("=") + 1);
                coils = CountCommas(rungName);
                rungNameExtended = rungName;
                for (int i = 0; i < coils; i++)
                {
                    ArrayList runglist = new ArrayList();
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
                    statusBar1.Text = "Forming Rung: " + RungNumber + " - " + rungName.ToString();
                    interlocking.Add(rung);
                }
            }

        }

        private void ParseVTLRungs(ArrayList interlocking, string filenameString)
        {

            string line; string code = ""; string frag = ""; string token = "";
            string rungName; int RungNumber; int coils = 0; string rungNameExtended = ""; string lineofcode;
            int nextApp = 0; int nextTime = 0; int nextBool = 0; int endRung = 0; int locBool = 0;
            bool endOfLadder = false; bool commentMode = false; bool commentLineMode = false;

            SR = File.OpenText(filenameString);
            while (((line = SR.ReadLine()) != null) && (endOfLadder != true))//Put logic into a single string
            {
                commentLineMode = false;

                if (line.LastIndexOf("VSL RESET") != -1)
                    statusBar1.Text = "made it";
                if (line.LastIndexOf("END BOOLEAN EQUATION SECTION") != -1)
                    endOfLadder = true;
                else
                {
                    lineofcode = "";
                    for (int index = 0; index < line.Length; index++)
                    {
                        token = line.Substring(index, 1);
                        if (index < line.Length)// - 1)
                            if ((index == 0) && line.Substring(index, 1) == "*")
                                commentLineMode = true; // Remove comments                        
                        if ((commentMode == false) && (commentLineMode == false))
                            if ((line.Substring(index, 1) != " ") && (line.Substring(index, 1) != "\r\n") &&
                                (line.Substring(index, 1) != "\t"))//Remove carriage returns, spaces & tabs
                                lineofcode += line.Substring(index, 1);
                        if (index > 0)
                            if (line.Substring(index - 1, 2) == "*/") commentMode = false;
                    }
                    code += lineofcode;
                }
            }
            SR.Close();
            //int logicBeginIndex = code.LastIndexOf("BOOLEANEQUATIONSECTION") + 22;//snip off anything before application
            int logicBeginIndex = code.IndexOf("BOOLEANEQUATIONSECTION") + 22;//snip off anything before application

            string logic = code.Substring(logicBeginIndex, code.Length - logicBeginIndex).Replace(".N.", "~");
            //int logicEndIndex = logic.LastIndexOf("ENDLOGIC");//snip off anything after end logic
            //logic = logic.Substring(0, logicEndIndex);
            string RungLogic = "";
            RungNumber = 1;

            while (logic.IndexOf("BOOL") != -1)
            {
                locBool = logic.IndexOf("BOOL");
                endRung = logic.Length;
                nextBool = logic.IndexOf("BOOL", locBool + 1);
                if ((nextBool > locBool) && (endRung > nextBool)) endRung = nextBool;
                nextApp = logic.IndexOf("APPLICATION", locBool);
                if ((nextApp > locBool) && (endRung > nextApp)) endRung = nextApp;
                nextTime = logic.IndexOf("TIMEDELAY", locBool);
                if ((nextTime > locBool) && (endRung > nextTime)) endRung = nextTime;
                RungLogic = logic.Substring(locBool + 4, endRung - (4 + locBool));
                rungName = RungLogic.Substring(0, RungLogic.IndexOf("="));
                logic = logic.Substring(endRung, logic.Length - endRung);//snip off rung just scanned, until they are all scanned in

                RungLogic = RungLogic.Substring(RungLogic.IndexOf("=") + 1);
                coils = CountCommas(rungName);
                rungNameExtended = rungName;
                for (int i = 0; i < coils; i++)
                {
                    ArrayList runglist = new ArrayList();
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
                    statusBar1.Text = "Forming Rung: " + RungNumber + " - " + rungName.ToString();
                    interlocking.Add(rung);
                }
            }

        }

        //private int ReplaceDotNDot(string logic)
        //{
        //  logic.Replace(".N.", "~");                        
        //}

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
            if (countst != countend) MessageBox.Show("Error reading rung number: "
                     + (linenum + 1).ToString() + ", brackets are unmatched, there are " + countst.ToString() + " start brackets but "
                     + countend.ToString() + " end brackets. \r\n" + rung, "Brackets Unmatched", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private Contact MakeContact(string name, int x, int y, bool toplink, bool bottomLink, bool leftlink, string typeCell, bool normallyClosed)
        {
            Contact contact = new Contact();
            contact.NormallyClosed = normallyClosed;
            if (name.Length < 1) contact.name = "";
            else
            {
                contact.NormallyClosed = normallyClosed;
                if (name.LastIndexOf("GG-YY") != -1)
                    name = name;
                //if (name.LastIndexOf("{StartBracket}", sc) != -1)
                //    name = name.Substring(0, name.LastIndexOf("{StartBracket}", sc)) + "(" + name.Substring(name.LastIndexOf("{StartBracket}", sc) + 14);
                //if (name.LastIndexOf("{EndBracket}", sc) != -1)
                //    name = name.Substring(0, name.LastIndexOf("{EndBracket}", sc)) + ")" + name.Substring(name.LastIndexOf("{EndBracket}", sc) + 12);  
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


        private int FindElement(int x, int y, ArrayList rung)// find arraylist element number at these coordinates
        {
            for (int j = 0; j < rung.Count; j++)
            {
                Contact contactPointer = (Contact)rung[j];
                if ((contactPointer.x == x) && (contactPointer.y == y))
                    return j;
            }
            return -1;
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

        private void ParseTXTRungs(ArrayList interlocking, ArrayList timers, string filename)
        {
            string line = "", previousline = "", secondpreviousline = "", timername = ""; string contacts = ""; int sizeOfRung = 10; int ycoord = 1;
            string contactline1 = ""; string contactline2 = ""; string contactline3 = ""; string contactline4 = "";
            int counter = 0;
            Boolean timer = true; int totalrungs = 0;
            string rungName = ""; int RungNumber = 0;
            Boolean topofrung = true;
            bool endOfRung = false; bool endOfLadder = false;
            try
            {
                SR = File.OpenText(FileName.Text);
                while (((line = SR.ReadLine()) != null) && (endOfLadder != true))
                {
                    counter++;
                    rungName = "";
                    if ((line.LastIndexOf("Rung") != -1) && (line.LastIndexOf("End of Rungs") == -1))
                    {
                        ArrayList rung = new ArrayList();

                        RungNumber = int.Parse(line.Substring(line.IndexOf("Rung") + 5,
                            (line.IndexOf("of") - line.IndexOf("Rung") - 6)));
                        totalrungs = RungNumber;
                        rung.Add(RungNumber);
                        ycoord = 1;
                        while ((endOfRung != true) && (endOfLadder != true))
                        {
                            contacts = SR.ReadLine();
                            if ((contacts.Length == 0) || (contacts.IndexOf("Pvalue") != -1)) endOfRung = true;
                            else
                            {
                                contactline1 = contacts;
                                contactline2 = SR.ReadLine();
                                contactline3 = SR.ReadLine();
                                contactline4 = SR.ReadLine();
                                counter += 3;
                                sizeOfRung = 0;
                                if (contactline2.Length > 79)
                                {
                                    for (int i = 0; i < 8; i++)
                                    {
                                        if (contactline2.Substring(i * 8 + 5, 1) == "[")
                                            sizeOfRung = i + 2;
                                        if ((contactline2.Substring(i * 8 + 0, 1) == "+")
                                            || (contactline2.Substring(i * 8 + 0, 1) == "|"))
                                            if (sizeOfRung < i + 1) sizeOfRung = i + 1;
                                    }
                                }
                                else
                                    sizeOfRung = 1; //end
                                for (int i = 0; i < sizeOfRung; i++)
                                {
                                    Contact contact = new Contact();
                                    contact.y = i + 1;
                                    contact.x = ycoord;
                                    if (contactline2.Substring(i * 8 + 4, 1) == "") contact.typeOfCell = "Empty";
                                    if (contactline2.Substring(i * 8 + 1, 7) == "-------") contact.typeOfCell = "Horizontal Shunt";
                                    if ((contactline1.Substring(i * 8 + 0, 1) == "|") && (contactline2.Substring(i * 8 + 5, 1) != "[")) contact.typeOfCell = "Vertical Shunt";
                                    if (contactline1.Substring(i * 8 + 4, 3) == "END")
                                        contact.typeOfCell = "End Contact";
                                    contact.name = contactline1.Substring(i * 8 + 1, 7).TrimEnd() +
                                                   contactline3.Substring(i * 8 + 1, 7).TrimEnd();
                                    if ((contact.typeOfCell == null) && (contact.name != "")) contact.typeOfCell = "Contact";
                                    if ((sizeOfRung == i + 1) && (ycoord == 1))
                                    {
                                        contact.typeOfCell = "Coil";
                                        timer = false;
                                        if (contactline2.IndexOf("TS") != -1)
                                            timer = true;
                                        rungName = contactline1.Substring(73, 7).TrimEnd() + contactline3.Substring(73, 7).TrimEnd();
                                        if (timer) rungName = rungName + "_UP";
                                        contact.name = rungName;
                                    }
                                    if (contactline2.Substring(i * 8 + 4, 1) == "/")
                                        contact.NormallyClosed = true;
                                    else contact.NormallyClosed = false;
                                    if (i != 0)
                                        if (contactline2.Substring(i * 8 - 1, 1) == "-") contact.leftLink = true;
                                    if (contactline3.Substring(i * 8 + 0, 1) == "|") contact.bottomLink = true;
                                    if (contactline1.Substring(i * 8 + 0, 1) == "|") contact.topLink = true;
                                    if ((contact.x == 1) && (contact.y == 1)) contact.topLink = true;
                                    if ((!endOfRung) && (contact.typeOfCell != null)) rung.Add(contact);
                                }
                                ycoord++;
                            }
                        }
                        endOfRung = false;
                        rung.Add(rungName);
                        interlocking.Add(rung);
                    }
                    endOfLadder = false;
                }

                SR.Close();
                //Timers
                counter = 0;
                SR = File.OpenText(FileName.Text);
                while (((line = SR.ReadLine()) != null) && (endOfLadder != true))
                {
                    counter++;
                    rungName = "";
                    if (previousline.IndexOf("(TS)") != -1)
                        timername = secondpreviousline.Substring(73, 7).TrimEnd() + line.Substring(73, 7).TrimEnd();
                    secondpreviousline = previousline; previousline = line;
                    if (line.IndexOf("Pvalue") != -1)
                    {
                        ArrayList rung = new ArrayList();
                        totalrungs++;
                        rung.Add(totalrungs);
                        ycoord = 1;
                        topofrung = true;
                        while ((endOfRung != true) && (endOfLadder != true))
                        {
                            contacts = line;
                            if (topofrung) contactline1 = contacts;
                            else contactline1 = SR.ReadLine();
                            topofrung = false;
                            contactline2 = SR.ReadLine();
                            contactline3 = SR.ReadLine();
                            contactline4 = SR.ReadLine();
                            counter += 3;
                            if ((contactline1.Length == 0) || (contactline2.Length == 0) || (contactline3.Length == 0))
                                endOfRung = true;
                            else
                            {
                                sizeOfRung = 0;
                                if (contactline2.Length > 79)
                                {
                                    for (int i = 0; i < 8; i++)
                                        if ((contactline2.Substring(i * 8 + 5, 1) == "[") || (contactline2.Substring(i * 8 + 8, 1) == "+") || (contactline2.Substring(i * 8 + 0, 1) == "|"))
                                            sizeOfRung = i + 2;
                                }
                                else
                                    sizeOfRung = 1; //end
                                for (int i = 0; i < sizeOfRung; i++)
                                {
                                    Contact contact = new Contact();
                                    contact.y = i + 1;
                                    contact.x = ycoord;
                                    if (contactline2.Substring(i * 8 + 4, 1) == "") contact.typeOfCell = "Empty";
                                    if (contactline2.Substring(i * 8 + 1, 7) == "-------") contact.typeOfCell = "Horizontal Shunt";
                                    if ((contactline1.Substring(i * 8 + 0, 1) == "|") && (contactline2.Substring(i * 8 + 5, 1) != "[")) contact.typeOfCell = "Vertical Shunt";
                                    if (contactline1.Substring(i * 8 + 4, 3) == "END")
                                        contact.typeOfCell = "End Contact";
                                    if (contact.typeOfCell == null) contact.typeOfCell = "Contact";
                                    contact.name = contactline1.Substring(i * 8 + 1, 7).TrimEnd() +
                                                   contactline3.Substring(i * 8 + 1, 7).TrimEnd();
                                    if ((sizeOfRung == i + 1) && (ycoord == 1))
                                    {
                                        contact.typeOfCell = "Coil";
                                        timer = false;
                                        rungName = timername;
                                        rungName = rungName + "_DOWN";
                                        contact.name = rungName;

                                        ML2Timer timeRec = new ML2Timer();
                                        timeRec.timerName = timername + "_UP";
                                        timeRec.setTime = int.Parse(contactline3.Substring(73, 7).TrimEnd()).ToString() + "s";
                                        timeRec.clearTime = "0" + "s";
                                        timers.Add(timeRec);
                                    }
                                    if (contactline2.Substring(i * 8 + 4, 1) == "/")
                                        contact.NormallyClosed = true;
                                    else contact.NormallyClosed = false;
                                    if (i != 0)
                                        if (contactline2.Substring(i * 8 - 1, 1) == "-") contact.leftLink = true;
                                    if (contactline3.Substring(i * 8 + 0, 1) == "|") contact.bottomLink = true;
                                    if (contactline1.Substring(i * 8 + 0, 1) == "|") contact.topLink = true;
                                    if ((contact.x == 1) && (contact.y == 1)) contact.topLink = true;

                                    if (!endOfRung)
                                        rung.Add(contact);
                                }
                                ycoord++;
                            }
                        }
                        endOfRung = false;
                        rung.Add(rungName);
                        interlocking.Add(rung);
                        {

                            ArrayList controlrung = new ArrayList();
                            Contact latch = new Contact();
                            Contact pickpath = new Contact();
                            Contact holdpath = new Contact();
                            Contact coil = new Contact();
                            Contact shunt = new Contact();
                            Contact vshunt = new Contact();

                            totalrungs++;
                            controlrung.Add(totalrungs);

                            latch.y = 1;
                            latch.x = 1;
                            latch.NormallyClosed = false;
                            latch.leftLink = false;
                            latch.name = timername;
                            latch.bottomLink = true;
                            latch.topLink = true;
                            latch.typeOfCell = "Contact";
                            controlrung.Add(latch);

                            pickpath.y = 2;
                            pickpath.x = 1;
                            pickpath.NormallyClosed = true;
                            pickpath.leftLink = true;
                            pickpath.name = timername + "_DOWN";
                            pickpath.bottomLink = false;
                            pickpath.topLink = false;
                            pickpath.typeOfCell = "Contact";
                            controlrung.Add(pickpath);

                            coil.y = 3;
                            coil.x = 1;
                            coil.NormallyClosed = true;
                            coil.leftLink = true;
                            coil.name = timername;
                            coil.bottomLink = true;
                            coil.topLink = false;
                            coil.typeOfCell = "Coil";
                            controlrung.Add(coil);

                            holdpath.y = 1;
                            holdpath.x = 2;
                            holdpath.NormallyClosed = false;
                            holdpath.leftLink = false;
                            holdpath.name = timername + "_UP";
                            holdpath.bottomLink = true;
                            holdpath.topLink = true;
                            holdpath.typeOfCell = "Contact";
                            controlrung.Add(holdpath);

                            shunt.y = 2;
                            shunt.x = 2;
                            shunt.NormallyClosed = false;
                            shunt.leftLink = true;
                            shunt.name = "";
                            shunt.bottomLink = false;
                            shunt.topLink = false;
                            shunt.typeOfCell = "Horizontal Shunt";
                            controlrung.Add(shunt);

                            vshunt.y = 3;
                            vshunt.x = 2;
                            vshunt.NormallyClosed = false;
                            vshunt.leftLink = true;
                            vshunt.name = "";
                            vshunt.bottomLink = false;
                            vshunt.topLink = true;
                            vshunt.typeOfCell = "Empty";
                            controlrung.Add(vshunt);

                            controlrung.Add(timername);
                            interlocking.Add(controlrung);
                        }

                    }
                    endOfLadder = false;
                }

                SR.Close();

            }
            catch
            {
                MessageBox.Show("Error opening rungs on line number: " + counter.ToString() +
                    ",  line: " + line.ToString() + ", Logic Navigator will attempt to read the rest of the file.",
                    "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private string ParseCommentField()
        {
            string subnet = "";
            string line; bool endOfInst = false;
            SR = File.OpenText(FileName.Text);
            while (((line = SR.ReadLine()) != null) && (endOfInst != true))
            {
                if (line.LastIndexOf("COMMENTS") != -1)
                {
                    endOfInst = true;
                    string temp_store = SR.ReadLine();
                    if (ParseSubnet(temp_store))
                    {
                        if (ParseDefGate(temp_store))
                        {
                            ncdcomment = "Pass";
                        }
                    }
                }
            }
            return ("");
        }

        private bool ParseSubnet(string subnet_line)
        {
            try
            {
                string subnet = "";
                if (subnet_line.LastIndexOf("SUBNET") != -1)
                {
                    int start_location = subnet_line.LastIndexOf("SUBNET MASK");
                    if (start_location == -1)
                    {
                        MessageBox.Show("Subnet mask start text not defined correctly.");
                        return false;
                    }
                    // Subnet is being attempted to be set.
                    if (start_location > subnet_line.IndexOf('"') + 1)
                    {
                        MessageBox.Show("Subnet mask not configured per manual.");
                        return false;
                    }
                    ncdcomment = subnet_line[start_location].ToString();
                    if (subnet_line.Length - start_location > (14 + 8))
                    {
                        if (subnet_line[start_location + 11] == " "[0] && subnet_line[start_location + 12] == "="[0] && subnet_line[start_location + 13] == " "[0])
                        {
                            // Next step
                            subnet = subnet_line.Substring(start_location + 14, 8);
                            if (OnlyHexInString(subnet))
                            {
                                if (subnet_line.Length - start_location > (23))
                                {
                                    if (subnet_line[start_location + 23] != "D"[0])
                                    { // Not necessarily a bad thing, but we should warn the user somehow..
                                      // This is just a message box but this might get annoying as not all stations will have a default gateway.
                                      // Any ideas?
                                        MessageBox.Show("No default gateway configured.");
                                    }
                                }
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("Error in Subnet Mask Hex.");
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Subnet Mask comment line not configured correctly.");
                            return false;
                        }
                    }
                    else
                    {
                        ncdcomment = "Subnet Fail";
                        return false;
                    }
                }
                else
                {
                    ncdcomment = "Subnet not set";
                    return false;
                }
            }
            catch { MessageBox.Show("Subnet Loading Error"); return false; }
        }

        private bool ParseDefGate(string subnet_line)
        {
            try
            {
                string ip = "";
                if (subnet_line.LastIndexOf("DEF GATE") != -1)
                {
                    int start_location = subnet_line.LastIndexOf("DEF GATE");
                    // Subnet is being attempted to be set.
                    ncdcomment = subnet_line[start_location].ToString();
                    if (subnet_line[start_location + 8] == " "[0] && subnet_line[start_location + 9] == "="[0] && subnet_line[start_location + 10] == " "[0])
                    {
                        // Next step
                        ip = subnet_line.Substring(start_location + 11);
                        ip = ip.Substring(0, ip.Length - 1);
                        if (IsValidIP(ip))
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Error in Default Gateway Configuration.");
                            return false;
                        }
                    }
                    else
                    {
                        ncdcomment = "IP Fail";
                        return false;
                    }
                }
                else
                {
                    ncdcomment = "IP not set";
                    return false;
                }
            }
            catch { MessageBox.Show("Default Gateway Loading Error"); return false; }
        }

        // Taken from https://stackoverflow.com/a/32702160
        public bool IsValidIP(string Ip)
        {
            var Pattern = new string[]
            {
            "^",                                            // Start of string
            @"([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])\.",    // Between 000 and 255 and "."
            @"([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])\.",
            @"([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])\.",
            @"([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])",      // Same as before, no period
            "$",                                            // End of string
            };

            return Regex.IsMatch(Ip, string.Join(string.Empty, Pattern));
        }

        // Taken from https://stackoverflow.com/a/223857 to check only hexadecimal characters are in a string
        // Used to parse comments field for correct subnet mask configuration
        public bool OnlyHexInString(string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b[0-9a-fA-F]+\b\Z");
        }


        private void ParseINSRungs(ArrayList interlocking, string filenameString)
        {
            string line = ""; string contacts = ""; int counter = 0;
            string rungName = ""; int RungNumber = 0;
            bool endOfRung = false; bool endOfLadder = false;
            try
            {
                SR = File.OpenText(filenameString);
                while (((line = SR.ReadLine()) != null) && (endOfLadder != true))
                {
                    counter++;
                    rungName = "";
                    if ((fileType == "INS") || (fileType == "WT2"))
                        if (line.LastIndexOf("END LADDER") != -1) endOfLadder = true;
                    if (fileType == "NCD")
                        if (line.LastIndexOf("END NVLADDER") != -1) endOfLadder = true;
                    if (line.LastIndexOf("RUNG") != -1)
                        if (line.LastIndexOf("VALID") != -1)		// Start of Rung & Valid
                        {
                            ArrayList rung = new ArrayList();
                            RungNumber = int.Parse(line.Substring(line.LastIndexOf("RUNG") + 5,
                                (line.LastIndexOf("IS") - line.LastIndexOf("RUNG") - 6)));
                            rung.Add(RungNumber);
                            while (((contacts = SR.ReadLine()) != null) && (endOfRung != true) && (endOfLadder != true))
                            {
                                Contact contact = new Contact();
                                if (contacts.LastIndexOf("END RUNG") != -1)
                                    endOfRung = true;
                                if (contacts.LastIndexOf("CELL") != -1)
                                {
                                    contact.x = int.Parse(contacts.Substring(contacts.LastIndexOf("CELL") + 6, 2));
                                    contact.y = int.Parse(contacts.Substring(contacts.LastIndexOf("CELL") + 9, 2));

                                    //Type of Cell
                                    contact.typeOfCell = ""; //Default to null								
                                    if (contacts.LastIndexOf("COIL") != -1) contact.typeOfCell = "Coil";
                                    if (contacts.LastIndexOf("EMPTY") != -1) contact.typeOfCell = "Empty";
                                    if (contacts.LastIndexOf("HORIZONTAL_SHUNT") != -1) contact.typeOfCell = "Horizontal Shunt";
                                    if (contacts.LastIndexOf("VERTICAL_SHUNT") != -1) contact.typeOfCell = "Vertical Shunt";
                                    if (contacts.LastIndexOf("END_CONTACT") != -1) contact.typeOfCell = "End Contact";
                                    if (contact.typeOfCell == "") contact.typeOfCell = "Contact";
                                    // Type	of Contact							
                                    contact.NormallyClosed = true;
                                    if (contacts.LastIndexOf("NORMALLY_OPEN") != -1) contact.NormallyClosed = false;
                                    else contact.NormallyClosed = true;

                                    // Name of Contact
                                    contact.name = "";
                                    if (contact.typeOfCell == "Coil")
                                    {
                                        contact.name = contacts.Substring(contacts.LastIndexOf("COIL") + 6,
                                            contacts.LastIndexOf("WITH") - (8 + contacts.LastIndexOf("COIL")));
                                        rungName = contact.name;
                                    }
                                    if ((contact.typeOfCell == "Contact") && (contact.NormallyClosed))
                                        contact.name = contacts.Substring(contacts.LastIndexOf("NORMALLY_CLOSED") + 17,
                                            contacts.LastIndexOf("WITH") - (19 + contacts.LastIndexOf("NORMALLY_CLOSED")));
                                    if ((contact.typeOfCell == "Contact") && (!contact.NormallyClosed))
                                        contact.name = contacts.Substring(contacts.LastIndexOf("NORMALLY_OPEN") + 15,
                                            contacts.LastIndexOf("WITH") - (17 + contacts.LastIndexOf("NORMALLY_OPEN")));

                                    // Links
                                    if (contacts.LastIndexOf(" LEFT ") != -1) contact.leftLink = true;
                                    else contact.leftLink = false;
                                    if (contacts.LastIndexOf(" BOTTOM ") != -1) contact.bottomLink = true;
                                    else contact.bottomLink = false;
                                    if (contacts.LastIndexOf(" TOP ") != -1) contact.topLink = true;
                                    else contact.topLink = false;
                                    if (!endOfRung) rung.Add(contact);
                                }
                            }
                            endOfRung = false;
                            rung.Add(rungName);
                            interlocking.Add(rung);
                        }
                    endOfLadder = false;
                }

                SR.Close();
                SR = File.OpenText(filenameString);
                string input; bool endOfInputs = false; bool startTimers = false;
                while (((input = SR.ReadLine()) != null) && (!endOfInputs))
                {
                    if (input.LastIndexOf("RESERVED TIMERS") != -1)
                        startTimers = true;
                    if (input.LastIndexOf("END RESERVED TIMERS") != -1)
                        endOfInputs = true;
                    if (startTimers)
                        if (input.LastIndexOf("TIMER ") != -1)
                        {
                            UserTimer userTimer = new UserTimer();
                            if (input.LastIndexOf("ACCESS") != -1)
                                userTimer.triggerName = input.Substring(input.LastIndexOf("TIMER TRIGGER ") + 15,
                                    (input.LastIndexOf("ACCESS") - 2) - (input.LastIndexOf("TIMER TRIGGER ") + 15));
                            if ((input = SR.ReadLine()) != null)
                                if (input.LastIndexOf("OUTPUT ") != -1)
                                    userTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT ") + 8,
                                        (input.LastIndexOf("ACCESS") - 2) - (input.LastIndexOf("OUTPUT ") + 8));

                            ArrayList rung = new ArrayList();
                            RungNumber++;
                            rung.Add(RungNumber);
                            Contact trigger = new Contact();
                            trigger.x = 1;
                            trigger.y = 1;
                            trigger.typeOfCell = "Contact";
                            trigger.NormallyClosed = false;
                            trigger.name = userTimer.triggerName;
                            trigger.leftLink = false;
                            trigger.topLink = true;
                            trigger.bottomLink = true;
                            rung.Add(trigger);
                            Contact coil = new Contact();
                            coil.x = 1;
                            coil.y = 2;
                            coil.typeOfCell = "Coil";
                            coil.NormallyClosed = false;
                            coil.name = userTimer.outputName;
                            coil.leftLink = true;
                            coil.topLink = false;
                            coil.bottomLink = false;
                            rung.Add(coil);
                            rung.Add(userTimer.outputName);
                            interlocking.Add(rung);
                        }
                }
                endOfInputs = false;
                if ((fileType == "INS") || (fileType == "WT2"))
                {
                    while (((input = SR.ReadLine()) != null) && (!endOfInputs))
                    {
                        if (input.LastIndexOf("USER TIMERS") != -1)
                            startTimers = true;
                        if (startTimers)
                            if (input.LastIndexOf("TIMER ") != -1)
                            {
                                UserTimer userTimer = new UserTimer();
                                if (input.LastIndexOf("SHORT") != -1)
                                    userTimer.triggerName = input.Substring(input.LastIndexOf("TRIGGER LONG ") + 14,
                                        (input.LastIndexOf("SHORT") - 2) - (input.LastIndexOf("TRIGGER LONG ") + 14));
                                else
                                    userTimer.triggerName = input.Substring(input.LastIndexOf("TRIGGER LONG ") + 14,
                                        input.Length - (15 + input.LastIndexOf("TRIGGER LONG")));
                                if ((input = SR.ReadLine()) != null)
                                {
                                    if (input.LastIndexOf("SHORT") == -1)
                                        userTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT  LONG") + 14,
                                            input.Length - (15 + input.LastIndexOf("OUTPUT  LONG")));
                                    else
                                        userTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT  LONG") + 14,
                                            (input.LastIndexOf("SHORT") - 2) - (14 + input.LastIndexOf("OUTPUT  LONG")));
                                }
                                if ((input = SR.ReadLine()) != null)
                                    userTimer.duration = input.Substring(input.LastIndexOf("DURATION") + 11,
                                        input.Length - (11 + input.LastIndexOf("DURATION")));

                                ArrayList rung = new ArrayList();
                                RungNumber++;
                                rung.Add(RungNumber);
                                Contact trigger = new Contact();
                                trigger.x = 1;
                                trigger.y = 1;
                                trigger.typeOfCell = "Contact";
                                trigger.NormallyClosed = false;
                                trigger.name = userTimer.triggerName;
                                trigger.leftLink = false;
                                trigger.topLink = true;
                                trigger.bottomLink = true;
                                rung.Add(trigger);
                                Contact coil = new Contact();
                                coil.x = 1;
                                coil.y = 2;
                                coil.typeOfCell = "Coil";
                                coil.NormallyClosed = false;
                                coil.name = userTimer.outputName;
                                coil.leftLink = true;
                                coil.topLink = false;
                                coil.bottomLink = false;
                                rung.Add(coil);
                                rung.Add(userTimer.outputName);
                                interlocking.Add(rung);
                            }
                    }
                }

                if (fileType == "NCD")
                {
                    while (((input = SR.ReadLine()) != null) && (!endOfInputs))
                    {
                        if (input.LastIndexOf("USER TIMERS") != -1)
                            startTimers = true;
                        if (startTimers)
                            if (input.LastIndexOf("TIMER ") != -1)
                            {
                                UserTimer userTimer = new UserTimer();
                                if (input.LastIndexOf("SHORT") != -1)
                                    userTimer.triggerName = input.Substring(input.LastIndexOf("TRIGGER LONG ") + 14,
                                        (input.LastIndexOf("SHORT") - 2) - (input.LastIndexOf("TRIGGER LONG ") + 14));
                                else
                                    userTimer.triggerName = input.Substring(input.LastIndexOf("TRIGGER") + 9,
                                        input.Length - (10 + input.LastIndexOf("TRIGGER")));
                                if ((input = SR.ReadLine()) != null)
                                {
                                    if (input.LastIndexOf("SHORT") == -1)
                                        userTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT") + 8,
                                            input.Length - (9 + input.LastIndexOf("OUTPUT")));
                                    else
                                        userTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT  LONG") + 14,
                                            (input.LastIndexOf("SHORT") - 2) - (14 + input.LastIndexOf("OUTPUT  LONG")));
                                }
                                ArrayList rung = new ArrayList();
                                RungNumber++;
                                rung.Add(RungNumber);
                                Contact trigger = new Contact();
                                trigger.x = 1;
                                trigger.y = 1;
                                trigger.typeOfCell = "Contact";
                                trigger.NormallyClosed = false;
                                trigger.name = userTimer.triggerName;
                                trigger.leftLink = false;
                                trigger.topLink = true;
                                trigger.bottomLink = true;
                                rung.Add(trigger);
                                Contact coil = new Contact();
                                coil.x = 1;
                                coil.y = 2;
                                coil.typeOfCell = "Coil";
                                coil.NormallyClosed = false;
                                coil.name = userTimer.outputName;
                                coil.leftLink = true;
                                coil.topLink = false;
                                coil.bottomLink = false;
                                rung.Add(coil);
                                rung.Add(userTimer.outputName);
                                interlocking.Add(rung);
                            }
                    }
                }
                SR.Close();
            }
            catch
            {
                MessageBox.Show("Error opening rungs on line number: " + counter.ToString() +
                    ",  line: " + line.ToString() + ", Logic Navigator will attempt to read the rest of the file.",
                    "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /*
        private void ParseNCDRungs(ArrayList interlocking)
        {
            string line = ""; string contacts = ""; int counter = 0;
            string rungName = ""; int RungNumber = 0;
            bool endOfRung = false; bool endOfLadder = false;
            try
            {
                SR = File.OpenText(FileName.Text);
                while (((line = SR.ReadLine()) != null) && (endOfLadder != true))
                {
                    counter++;
                    rungName = "";
                    if ((string.Compare(fileType, "INS") == 0) || (string.Compare(fileType, "WT2") == 0))
                        if (line.LastIndexOf("END LADDER") != -1) endOfLadder = true;
                    if (string.Compare(fileType, "NCD") == 0)
                        if (line.LastIndexOf("END NVLADDER") != -1) endOfLadder = true;
                    if (line.LastIndexOf("RUNG") != -1)
                        if (line.LastIndexOf("VALID") != -1)		// Start of Rung & Valid
                        {
                            ArrayList rung = new ArrayList();
                            RungNumber = int.Parse(line.Substring(line.LastIndexOf("RUNG") + 5,
                                (line.LastIndexOf("IS") - line.LastIndexOf("RUNG") - 6)));
                            rung.Add(RungNumber);
                            while (((contacts = SR.ReadLine()) != null) && (endOfRung != true) && (endOfLadder != true))
                            {
                                Contact contact = new Contact();
                                if (contacts.LastIndexOf("END RUNG") != -1)
                                    endOfRung = true;
                                if (contacts.LastIndexOf("CELL") != -1)
                                {
                                    contact.x = int.Parse(contacts.Substring(contacts.LastIndexOf("CELL") + 6, 2));
                                    contact.y = int.Parse(contacts.Substring(contacts.LastIndexOf("CELL") + 9, 2));

                                    //Type of Cell
                                    contact.typeOfCell = ""; //Default to null								
                                    if (contacts.LastIndexOf("COIL") != -1) contact.typeOfCell = "Coil";
                                    if (contacts.LastIndexOf("EMPTY") != -1) contact.typeOfCell = "Empty";
                                    if (contacts.LastIndexOf("HORIZONTAL_SHUNT") != -1) contact.typeOfCell = "Horizontal Shunt";
                                    if (contacts.LastIndexOf("VERTICAL_SHUNT") != -1) contact.typeOfCell = "Vertical Shunt";
                                    if (contacts.LastIndexOf("END_CONTACT") != -1) contact.typeOfCell = "End Contact";
                                    if (contact.typeOfCell == "") contact.typeOfCell = "Contact";
                                    // Type	of Contact							
                                    contact.NormallyClosed = true;
                                    if (contacts.LastIndexOf("NORMALLY_OPEN") != -1) contact.NormallyClosed = false;
                                    else contact.NormallyClosed = true;

                                    // Name of Contact
                                    contact.name = "";
                                    if (contact.typeOfCell == "Coil")
                                    {
                                        contact.name = contacts.Substring(contacts.LastIndexOf("COIL") + 6,
                                            contacts.LastIndexOf("WITH") - (8 + contacts.LastIndexOf("COIL")));
                                        rungName = contact.name;
                                    }
                                    if ((contact.typeOfCell == "Contact") && (contact.NormallyClosed))
                                        contact.name = contacts.Substring(contacts.LastIndexOf("NORMALLY_CLOSED") + 17,
                                            contacts.LastIndexOf("WITH") - (19 + contacts.LastIndexOf("NORMALLY_CLOSED")));
                                    if ((contact.typeOfCell == "Contact") && (!contact.NormallyClosed))
                                        contact.name = contacts.Substring(contacts.LastIndexOf("NORMALLY_OPEN") + 15,
                                            contacts.LastIndexOf("WITH") - (17 + contacts.LastIndexOf("NORMALLY_OPEN")));

                                    // Links
                                    if (contacts.LastIndexOf(" LEFT ") != -1) contact.leftLink = true;
                                    else contact.leftLink = false;
                                    if (contacts.LastIndexOf(" BOTTOM ") != -1) contact.bottomLink = true;
                                    else contact.bottomLink = false;
                                    if (contacts.LastIndexOf(" TOP ") != -1) contact.topLink = true;
                                    else contact.topLink = false;
                                    if (!endOfRung) rung.Add(contact);
                                }
                            }
                            endOfRung = false;
                            rung.Add(rungName);
                            interlocking.Add(rung);
                        }
                    endOfLadder = false;
                }

                SR.Close();
                SR = File.OpenText(FileName.Text);
                string input; bool endOfInputs = false; bool startTimers = false;
                while (((input = SR.ReadLine()) != null) && (!endOfInputs))
                {
                    if (input.LastIndexOf("RESERVED TIMERS") != -1)
                        startTimers = true;
                    if (input.LastIndexOf("END RESERVED TIMERS") != -1)
                        endOfInputs = true;
                    if (startTimers)
                        if (input.LastIndexOf("TIMER ") != -1)
                        {
                            UserTimer userTimer = new UserTimer();
                            if (input.LastIndexOf("ACCESS") != -1)
                                userTimer.triggerName = input.Substring(input.LastIndexOf("TIMER TRIGGER ") + 15,
                                    (input.LastIndexOf("ACCESS") - 2) - (input.LastIndexOf("TIMER TRIGGER ") + 15));
                            if ((input = SR.ReadLine()) != null)
                                if (input.LastIndexOf("OUTPUT ") != -1)
                                    userTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT ") + 8,
                                        (input.LastIndexOf("ACCESS") - 2) - (input.LastIndexOf("OUTPUT ") + 8));


                            ArrayList rung = new ArrayList();
                            RungNumber++;
                            rung.Add(RungNumber);
                            Contact trigger = new Contact();
                            trigger.x = 1;
                            trigger.y = 1;
                            trigger.typeOfCell = "Contact";
                            trigger.NormallyClosed = false;
                            trigger.name = userTimer.triggerName;
                            trigger.leftLink = false;
                            trigger.topLink = true;
                            trigger.bottomLink = true;
                            rung.Add(trigger);
                            Contact coil = new Contact();
                            coil.x = 1;
                            coil.y = 2;
                            coil.typeOfCell = "Coil";
                            coil.NormallyClosed = false;
                            coil.name = userTimer.outputName;
                            coil.leftLink = true;
                            coil.topLink = false;
                            coil.bottomLink = false;
                            rung.Add(coil);
                            rung.Add(userTimer.outputName);
                            interlocking.Add(rung);
                        }
                }
                endOfInputs = false;
                while (((input = SR.ReadLine()) != null) && (!endOfInputs))
                {
                    if (input.LastIndexOf("USER TIMERS") != -1)
                        startTimers = true;
                    if (startTimers)
                        if (input.LastIndexOf("TIMER ") != -1)
                        {
                            UserTimer userTimer = new UserTimer();
                            if (input.LastIndexOf("SHORT") != -1)
                                userTimer.triggerName = input.Substring(input.LastIndexOf("TRIGGER LONG ") + 14,
                                    (input.LastIndexOf("SHORT") - 2) - (input.LastIndexOf("TRIGGER LONG ") + 14));
                            else
                                userTimer.triggerName = input.Substring(input.LastIndexOf("TRIGGER LONG ") + 14,
                                    input.Length - (15 + input.LastIndexOf("TRIGGER LONG")));
                            if ((input = SR.ReadLine()) != null)
                            {
                                if (input.LastIndexOf("SHORT") == -1)
                                    userTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT  LONG") + 14,
                                        input.Length - (15 + input.LastIndexOf("OUTPUT  LONG")));
                                else
                                    userTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT  LONG") + 14,
                                        (input.LastIndexOf("SHORT") - 2) - (14 + input.LastIndexOf("OUTPUT  LONG")));
                            }
                            if ((input = SR.ReadLine()) != null)
                                userTimer.duration = input.Substring(input.LastIndexOf("DURATION") + 11,
                                    input.Length - (11 + input.LastIndexOf("DURATION")));

                            ArrayList rung = new ArrayList();
                            RungNumber++;
                            rung.Add(RungNumber);
                            Contact trigger = new Contact();
                            trigger.x = 1;
                            trigger.y = 1;
                            trigger.typeOfCell = "Contact";
                            trigger.NormallyClosed = false;
                            trigger.name = userTimer.triggerName;
                            trigger.leftLink = false;
                            trigger.topLink = true;
                            trigger.bottomLink = true;
                            rung.Add(trigger);
                            Contact coil = new Contact();
                            coil.x = 1;
                            coil.y = 2;
                            coil.typeOfCell = "Coil";
                            coil.NormallyClosed = false;
                            coil.name = userTimer.outputName;
                            coil.leftLink = true;
                            coil.topLink = false;
                            coil.bottomLink = false;
                            rung.Add(coil);
                            rung.Add(userTimer.outputName);
                            interlocking.Add(rung);
                        }
                }
                SR.Close();
            }
            catch
            {
                MessageBox.Show("Error opening rungs on line number: " + counter.ToString() +
                    ",  line: " + line.ToString() + ", Logic Navigator will attempt to read the rest of the file.",
                    "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }*/

        private void ParseWT2Rungs(ArrayList interlocking, string filenameString)
        {
            string line = ""; string contacts = ""; int counter = 0;
            string rungName = ""; int RungNumber = 0;
            bool endOfRung = false; bool endOfLadder = false;
            try
            {
                SR = File.OpenText(filenameString);
                while (((line = SR.ReadLine()) != null) && (endOfLadder != true))
                {
                    counter++;
                    rungName = "";
                    if ((string.Compare(fileType, "INS") == 0) || (string.Compare(fileType, "WT2") == 0))
                        if (line.LastIndexOf("RESERVED TIMERS") != -1) endOfLadder = true;
                    if (string.Compare(fileType, "NCD") == 0)
                        if (line.LastIndexOf("END NVLADDER") != -1) endOfLadder = true;
                    if (line.LastIndexOf("RUNG") != -1)
                        if (line.LastIndexOf("VALID") != -1)		// Start of Rung & Valid
                        {
                            ArrayList rung = new ArrayList();
                            RungNumber = int.Parse(line.Substring(line.LastIndexOf("RUNG") + 5,
                                (line.LastIndexOf("IS") - line.LastIndexOf("RUNG") - 6)));
                            rung.Add(RungNumber);
                            while (((contacts = SR.ReadLine()) != null) && (endOfRung != true) && (endOfLadder != true))
                            {
                                Contact contact = new Contact();
                                if (contacts.LastIndexOf("END RUNG") != -1)
                                    endOfRung = true;
                                if (contacts.LastIndexOf("CELL") != -1)
                                {
                                    contact.x = int.Parse(contacts.Substring(contacts.LastIndexOf("CELL") + 6, 2));
                                    contact.y = int.Parse(contacts.Substring(contacts.LastIndexOf("CELL") + 9, 2));

                                    //Type of Cell
                                    contact.typeOfCell = ""; //Default to null								
                                    if (contacts.LastIndexOf("COIL") != -1) contact.typeOfCell = "Coil";
                                    if (contacts.LastIndexOf("EMPTY") != -1) contact.typeOfCell = "Empty";
                                    if (contacts.LastIndexOf("HORIZONTAL_SHUNT") != -1) contact.typeOfCell = "Horizontal Shunt";
                                    if (contacts.LastIndexOf("VERTICAL_SHUNT") != -1) contact.typeOfCell = "Vertical Shunt";
                                    if (contacts.LastIndexOf("END_CONTACT") != -1) contact.typeOfCell = "End Contact";
                                    if (contact.typeOfCell == "") contact.typeOfCell = "Contact";
                                    // Type	of Contact							
                                    contact.NormallyClosed = true;
                                    if (contacts.LastIndexOf("NORMALLY_OPEN") != -1) contact.NormallyClosed = false;
                                    else contact.NormallyClosed = true;

                                    // Name of Contact
                                    contact.name = "";
                                    if (contact.typeOfCell == "Coil")
                                    {
                                        contact.name = contacts.Substring(contacts.LastIndexOf("COIL") + 6,
                                            contacts.LastIndexOf("WITH") - (8 + contacts.LastIndexOf("COIL")));
                                        rungName = contact.name;
                                    }
                                    if ((contact.typeOfCell == "Contact") && (contact.NormallyClosed))
                                        contact.name = contacts.Substring(contacts.LastIndexOf("NORMALLY_CLOSED") + 17,
                                            contacts.LastIndexOf("WITH") - (19 + contacts.LastIndexOf("NORMALLY_CLOSED")));
                                    if ((contact.typeOfCell == "Contact") && (!contact.NormallyClosed))
                                        contact.name = contacts.Substring(contacts.LastIndexOf("NORMALLY_OPEN") + 15,
                                            contacts.LastIndexOf("WITH") - (17 + contacts.LastIndexOf("NORMALLY_OPEN")));

                                    // Links
                                    if (contacts.LastIndexOf(" LEFT ") != -1) contact.leftLink = true;
                                    else contact.leftLink = false;
                                    if (contacts.LastIndexOf(" BOTTOM ") != -1) contact.bottomLink = true;
                                    else contact.bottomLink = false;
                                    if (contacts.LastIndexOf(" TOP ") != -1) contact.topLink = true;
                                    else contact.topLink = false;
                                    if (!endOfRung) rung.Add(contact);
                                }
                            }
                            endOfRung = false;
                            rung.Add(rungName);
                            interlocking.Add(rung);
                        }
                    endOfLadder = false;
                }

                SR.Close();
                SR = File.OpenText(filenameString);
                string input; bool endOfInputs = false; bool startTimers = false;
                while (((input = SR.ReadLine()) != null) && (!endOfInputs))
                {
                    if (input.LastIndexOf("RESERVED TIMERS") != -1)
                        startTimers = true;
                    if (input.LastIndexOf("END RESERVED TIMERS") != -1)
                        endOfInputs = true;
                    if (startTimers)
                        if (input.LastIndexOf("TIMER ") != -1)
                        {
                            UserTimer userTimer = new UserTimer();
                            if (input.LastIndexOf("ACCESS") != -1)
                                userTimer.triggerName = input.Substring(input.LastIndexOf("TIMER TRIGGER ") + 15,
                                    (input.LastIndexOf("ACCESS") - 2) - (input.LastIndexOf("TIMER TRIGGER ") + 15));
                            if ((input = SR.ReadLine()) != null)
                                if (input.LastIndexOf("OUTPUT ") != -1)
                                    userTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT ") + 8,
                                        (input.LastIndexOf("ACCESS") - 2) - (input.LastIndexOf("OUTPUT ") + 8));
                            if ((input = SR.ReadLine()) != null)
                                userTimer.duration = input.Substring(input.LastIndexOf("DURATION") + 11,
                                    input.Length - (11 + input.LastIndexOf("DURATION")));

                            ArrayList rung = new ArrayList();
                            RungNumber++;
                            rung.Add(RungNumber);
                            Contact trigger = new Contact();
                            trigger.x = 1;
                            trigger.y = 1;
                            trigger.typeOfCell = "Contact";
                            trigger.NormallyClosed = false;
                            trigger.name = userTimer.triggerName;
                            trigger.leftLink = false;
                            trigger.topLink = true;
                            trigger.bottomLink = true;
                            rung.Add(trigger);
                            Contact coil = new Contact();
                            coil.x = 1;
                            coil.y = 2;
                            coil.typeOfCell = "Coil";
                            coil.NormallyClosed = false;
                            coil.name = userTimer.outputName;
                            coil.leftLink = true;
                            coil.topLink = false;
                            coil.bottomLink = false;
                            rung.Add(coil);
                            rung.Add(userTimer.outputName);
                            interlocking.Add(rung);
                        }
                }
                endOfInputs = false;
                while (((input = SR.ReadLine()) != null) && (!endOfInputs))
                {
                    if (input.LastIndexOf("USER TIMERS") != -1)
                        startTimers = true;
                    if (startTimers)
                        if (input.LastIndexOf("TIMER ") != -1)
                        {
                            UserTimer userTimer = new UserTimer();
                            if (input.LastIndexOf("SHORT") != -1)
                                userTimer.triggerName = input.Substring(input.LastIndexOf("TRIGGER LONG ") + 14,
                                    (input.LastIndexOf("SHORT") - 2) - (input.LastIndexOf("TRIGGER LONG ") + 14));
                            else
                                userTimer.triggerName = input.Split(((char)34))[1];// (char) 34 = "
                                                                                   //userTimer.triggerName = input.Substring(input.LastIndexOf("TRIGGER ") + 9,
                                                                                   //  input.Length - (10 + input.LastIndexOf("TRIGGER ")));

                            if ((input = SR.ReadLine()) != null)
                            {
                                if (input.LastIndexOf("SHORT") == -1)
                                    userTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT  ") + 9,
                                        input.Length - (10 + input.LastIndexOf("OUTPUT  ")));
                                else
                                    userTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT  ") + 9,
                                        (input.LastIndexOf("SHORT") - 2) - (10 + input.LastIndexOf("OUTPUT  ")));
                            }
                            if ((input = SR.ReadLine()) != null)
                                userTimer.duration = input.Substring(input.LastIndexOf("DURATION") + 11,
                                    input.Length - (11 + input.LastIndexOf("DURATION")));

                            ArrayList rung = new ArrayList();
                            RungNumber++;
                            rung.Add(RungNumber);
                            Contact trigger = new Contact();
                            trigger.x = 1;
                            trigger.y = 1;
                            trigger.typeOfCell = "Contact";
                            trigger.NormallyClosed = false;
                            trigger.name = userTimer.triggerName;
                            trigger.leftLink = false;
                            trigger.topLink = true;
                            trigger.bottomLink = true;
                            rung.Add(trigger);
                            Contact coil = new Contact();
                            coil.x = 1;
                            coil.y = 2;
                            coil.typeOfCell = "Coil";
                            coil.NormallyClosed = false;
                            coil.name = userTimer.outputName;
                            coil.leftLink = true;
                            coil.topLink = false;
                            coil.bottomLink = false;
                            rung.Add(coil);
                            rung.Add(userTimer.outputName);
                            interlocking.Add(rung);
                        }
                }
                SR.Close();
            }
            catch
            {
                MessageBox.Show("Error opening rungs on line number: " + counter.ToString() +
                    ",  line: " + line.ToString() + ", Logic Navigator will attempt to read the rest of the file.",
                    "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void ParseHousings(ArrayList Housings, ArrayList Timers, string typeofFile, string filenameString) //Parse Westrace housings
        {
            string line = ""; int counter = 0; int HousingNumber = 1;
            try
            {
                bool endOfHousing = false; bool endOfModule = false;
                string module_line;
                SR = File.OpenText(filenameString);
                if (typeofFile == "INS")
                {
                    while (((line = SR.ReadLine()) != null) && (endOfHousing != true))
                    {
                        counter++;
                        if (line.LastIndexOf("END HOUSING") != -1) endOfHousing = true;
                        if (line.LastIndexOf("HOUSING") != -1)
                        {
                            ArrayList housing = new ArrayList();
                            HousingNumber = int.Parse(line.Substring(line.LastIndexOf("HOUSING") + 8, 1));
                            //housings.Add(HousingNumber);
                            while (((module_line = SR.ReadLine()) != null) && (endOfHousing != true) && (endOfModule != true))
                            {
                                ArrayList Module = new ArrayList();
                                if (module_line.LastIndexOf("END MODULE") != -1)
                                    endOfModule = true;
                                if ((module_line.LastIndexOf("MODULE") != -1) & (endOfModule != true))
                                {
                                    if (module_line.LastIndexOf("HVLM128") != -1)
                                    {
                                        Module.Add("HVLM128");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        //ScanFaultName(Module);
                                        ScanHVM128(Module, Timers);

                                    }
                                    if (module_line.LastIndexOf("VLM6") != -1)
                                    {
                                        Module.Add("VLM6");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        //ScanFaultName(Module);																	
                                        ScanVLM6(Module, Timers);
                                    }
                                    if (module_line.LastIndexOf("NVC422") != -1)
                                    {
                                        Module.Add("NVC422");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        //ScanFaultName(Module);	
                                        //TODO: PAIR's
                                        ScanNVC422(Module);
                                    }
                                    if (module_line.LastIndexOf("EVTC") != -1)
                                    {
                                        Module.Add("EVTC");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        //TODO: PortAddress
                                        //TODO: Adjacent Installation
                                        //TODO: Installation address
                                        ScanEVTC(Module);
                                    }
                                    if (module_line.LastIndexOf("VTC232") != -1)
                                    {
                                        Module.Add("VTC232");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        ScanFaultName(Module);
                                        ScanVTC(Module);
                                    }
                                    if (module_line.LastIndexOf("DIAG") != -1) { Module.Add("DIAG"); Module.Add(GetSlotNumber(module_line)); }
                                    if (module_line.LastIndexOf("WCM") != -1) { Module.Add("WCM"); Module.Add(GetSlotNumber(module_line)); }
                                    if (module_line.LastIndexOf("VROM50") != -1)
                                    {
                                        Module.Add("VROM50");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        ScanFaultName(Module);
                                        ScanVROM50(Module);
                                    }
                                    if (module_line.LastIndexOf("VPIM50") != -1)
                                    {
                                        Module.Add("VPIM50");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        ScanFaultName(Module);
                                        ScanVPIM50(Module);
                                    }
                                    if (module_line.LastIndexOf("VLOMFT110") != -1)
                                    {
                                        Module.Add("VLOMFT110");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        ScanFaultName(Module);
                                        ScanVLOMFT110(Module);
                                    }
                                    if (module_line.LastIndexOf("NCDM") != -1)
                                    {
                                        Module.Add("NCDM");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        ScanFaultName(Module);
                                    }
                                    housing.Add(Module);
                                }
                                endOfModule = false;
                                if (module_line.LastIndexOf("END HOUSING") != -1) endOfHousing = true;
                            }
                            endOfHousing = false;
                            Housings.Add(housing);
                        }
                    }
                }

                if (typeofFile == "NCD")
                {
                    ArrayList housing = new ArrayList();
                    ArrayList Module = new ArrayList();
                    ScanNCD(Module, Timers);
                    housing.Add(Module);
                    Housings.Add(housing);
                }

                if (typeofFile == "WT2")
                {
                    ArrayList housing = new ArrayList();
                    ArrayList Module = new ArrayList();
                    ScanLM(Module, Timers);
                    housing.Add(Module);
                    Housings.Add(housing);

                    /*

                    while (((line = SR.ReadLine()) != null) && (endOfHousing != true))
                    {
                        counter++;
                        if (line.LastIndexOf("END HOUSING") != -1) endOfHousing = true;
                        if (line.LastIndexOf("HOUSING") != -1)
                        {
                            ArrayList housing = new ArrayList();
                            HousingNumber = int.Parse(line.Substring(line.LastIndexOf("HOUSING") + 8, 1));
                            //housings.Add(HousingNumber);
                            while (((module_line = SR.ReadLine()) != null) && (endOfHousing != true) && (endOfModule != true))
                            {
                                ArrayList Module = new ArrayList();
                                if (module_line.LastIndexOf("END MODULE") != -1)
                                    endOfModule = true;
                                if ((module_line.LastIndexOf("MODULE") != -1) & (endOfModule != true))
                                {
                                    if (module_line.LastIndexOf("HVLM128") != -1)
                                    {
                                        Module.Add("HVLM128");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        //ScanFaultName(Module);
                                        ScanHVM128(Module, Timers);

                                    }
                                    if (module_line.LastIndexOf("VLM6") != -1)
                                    {
                                        Module.Add("VLM6");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        //ScanFaultName(Module);																	
                                        ScanVLM6(Module, Timers);
                                    }
                                    if (module_line.LastIndexOf("NVC422") != -1)
                                    {
                                        Module.Add("NVC422");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        //ScanFaultName(Module);	
                                        //TODO: PAIR's
                                        ScanNVC422(Module);
                                    }
                                    if (module_line.LastIndexOf("EVTC") != -1)
                                    {
                                        Module.Add("EVTC");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        //TODO: PortAddress
                                        //TODO: Adjacent Installation
                                        //TODO: Installation address
                                        ScanEVTC(Module);
                                    }
                                    if (module_line.LastIndexOf("VTC232") != -1)
                                    {
                                        Module.Add("VTC232");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        ScanFaultName(Module);
                                        ScanVTC(Module);
                                    }
                                    if (module_line.LastIndexOf("DIAG") != -1) { Module.Add("DIAG"); Module.Add(GetSlotNumber(module_line)); }
                                    if (module_line.LastIndexOf("WCM") != -1) { Module.Add("WCM"); Module.Add(GetSlotNumber(module_line)); }
                                    if (module_line.LastIndexOf("VROM50") != -1)
                                    {
                                        Module.Add("VROM50");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        ScanFaultName(Module);
                                        ScanVROM50(Module);
                                    }
                                    if (module_line.LastIndexOf("VPIM50") != -1)
                                    {
                                        Module.Add("VPIM50");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        ScanFaultName(Module);
                                        ScanVPIM50(Module);
                                    }
                                    if (module_line.LastIndexOf("VLOMFT110") != -1)
                                    {
                                        Module.Add("VLOMFT110");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        ScanFaultName(Module);
                                        ScanVLOMFT110(Module);
                                    }
                                    if (module_line.LastIndexOf("NCDM") != -1)
                                    {
                                        Module.Add("NCDM");
                                        ScanSequenceNumber(Module, module_line);
                                        Module.Add(GetSlotNumber(module_line));
                                        ScanFaultName(Module);
                                    }
                                    housing.Add(Module);
                                }
                                endOfModule = false;
                                if (module_line.LastIndexOf("END HOUSING") != -1) endOfHousing = true;
                            }
                            endOfHousing = false;
                            Housings.Add(housing);
                        }
                    }*/
                }
                SR.Close();

            }
            catch
            {
                MessageBox.Show("Error opening housing information at Housing number: " + HousingNumber.ToString() + ", line number: " + counter.ToString() +
                    ", line data: " + line.ToString() + ", Logic Navigator will attempt to read rungs.",
                    "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void ScanVLM6(ArrayList Module, ArrayList Timers)
        {
            //ScanNetworkPorts(Module);
            ScanReservedTimers(Module, Timers, "INS");
            ScanUserTimers(Module, Timers, "INS");
            ScanReservedLatches(Module);
            ScanUserLatches(Module);
        }

        private void ScanNCD(ArrayList Module, ArrayList Timers)
        {
            //ScanNetworkPorts(Module);
            ScanReservedTimers(Module, Timers, "NCD");
            ScanUserTimers(Module, Timers, "NCD");
            //ScanReservedLatches(Module);
            //ScanUserLatches(Module);
        }

        private void ScanLM(ArrayList Module, ArrayList Timers) //Westrace Mark 2
        {
            //ScanNetworkPorts(Module);
            ScanReservedTimers(Module, Timers, "WT2");
            ScanUserTimers(Module, Timers, "WT2");
            //ScanReservedLatches(Module);
            //ScanUserLatches(Module);
        }

        private void ScanHVM128(ArrayList Module, ArrayList Timers)
        {
            ScanReservedTimers(Module, Timers, "INS");
            ScanUserTimers(Module, Timers, "INS");
            ScanReservedLatches(Module);
            ScanUserLatches(Module);
        }

        private void ScanReservedTimers(ArrayList Module, ArrayList timers, string TypeOfFile)
        {
            string input; bool endOfInputs = false;
            ArrayList Inputs = new ArrayList();
            if (TypeOfFile == "WT2")
            {
                while (((input = SR.ReadLine()) != null) && (endOfInputs != true))
                {
                    if (input.LastIndexOf("END RESERVED TIMERS") != -1) endOfInputs = true;
                    if (input.LastIndexOf("TIMER TRIGGER ") != -1)
                    {
                        ReserveTimer reserveTimer = new ReserveTimer();
                        reserveTimer.triggerName = input.Substring(input.LastIndexOf("TRIGGER") + 9,
                            input.LastIndexOf("ACCESS") - (11 + input.LastIndexOf("TRIGGER")));
                        reserveTimer.triggerAccess = input.Substring(input.LastIndexOf("ACCESS") + 9, input.Length - (input.LastIndexOf("ACCESS") + 9));
                        if ((input = SR.ReadLine()) != null)
                        {
                            reserveTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT") + 8,
                                input.LastIndexOf("ACCESS") - (10 + input.LastIndexOf("OUTPUT")));
                            reserveTimer.outputAccess = input.Substring(input.LastIndexOf("ACCESS") + 9, input.Length - (input.LastIndexOf("ACCESS") + 9));
                        }
                        if ((input = SR.ReadLine()) != null)
                        {
                            reserveTimer.duration = input.Substring(input.LastIndexOf("DURATION") + 11,
                                input.LastIndexOf("SETTING") - (12 + input.LastIndexOf("DURATION")));
                            reserveTimer.durationSetting = input.Substring(input.LastIndexOf("SETTING") + 10, input.Length - (input.LastIndexOf("SETTING") + 10));
                        }
                        Inputs.Add(reserveTimer);

                        ML2Timer timeRec = new ML2Timer();
                        timeRec.timerName = reserveTimer.outputName;
                        timeRec.setTime = reserveTimer.duration;
                        timeRec.clearTime = "0" + "s";
                        timers.Add(timeRec);
                    }
                }
            }
            if (TypeOfFile == "INS")
            {
                while (((input = SR.ReadLine()) != null) && (endOfInputs != true))
                {
                    if (input.LastIndexOf("END RESERVED TIMERS") != -1) endOfInputs = true;
                    if (input.LastIndexOf("TIMER TRIGGER ") != -1)
                    {
                        ReserveTimer reserveTimer = new ReserveTimer();
                        reserveTimer.triggerName = input.Substring(input.LastIndexOf("TRIGGER") + 9,
                            input.LastIndexOf("ACCESS") - (11 + input.LastIndexOf("TRIGGER")));
                        reserveTimer.triggerAccess = input.Substring(input.LastIndexOf("ACCESS") + 9, input.Length - (input.LastIndexOf("ACCESS") + 9));
                        if ((input = SR.ReadLine()) != null)
                        {
                            reserveTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT") + 8,
                                input.LastIndexOf("ACCESS") - (10 + input.LastIndexOf("OUTPUT")));
                            reserveTimer.outputAccess = input.Substring(input.LastIndexOf("ACCESS") + 9, input.Length - (input.LastIndexOf("ACCESS") + 9));
                        }
                        if ((input = SR.ReadLine()) != null)
                        {
                            reserveTimer.duration = input.Substring(input.LastIndexOf("DURATION") + 11,
                                input.LastIndexOf("SETTING") - (12 + input.LastIndexOf("DURATION")));
                            reserveTimer.durationSetting = input.Substring(input.LastIndexOf("SETTING") + 10, input.Length - (input.LastIndexOf("SETTING") + 10));
                        }
                        Inputs.Add(reserveTimer);

                        ML2Timer timeRec = new ML2Timer();
                        timeRec.timerName = reserveTimer.outputName;
                        timeRec.setTime = reserveTimer.duration;
                        timeRec.clearTime = "0" + "s";
                        timers.Add(timeRec);
                    }
                }
            }
            if (TypeOfFile == "NCD")
            {
                while (((input = SR.ReadLine()) != null) && (endOfInputs != true))
                {
                    if (input.LastIndexOf("END RESERVED TIMERS") != -1) endOfInputs = true;
                    if (input.LastIndexOf("TIMER TRIGGER ") != -1)
                    {
                        ReserveTimer reserveTimer = new ReserveTimer();
                        ML2Timer timeRec = new ML2Timer();
                        reserveTimer.triggerName = input.Substring(input.LastIndexOf("TRIGGER") + 9,
                            input.LastIndexOf("ACCESS") - (11 + input.LastIndexOf("TRIGGER")));
                        reserveTimer.triggerAccess = input.Substring(input.LastIndexOf("ACCESS") + 9, input.Length - (input.LastIndexOf("ACCESS") + 9));
                        if ((input = SR.ReadLine()) != null)
                        {
                            reserveTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT") + 8,
                                input.LastIndexOf("ACCESS") - (10 + input.LastIndexOf("OUTPUT")));
                            reserveTimer.outputAccess = input.Substring(input.LastIndexOf("ACCESS") + 9, input.Length - (input.LastIndexOf("ACCESS") + 9));
                        }
                        if ((input = SR.ReadLine()) != null)
                        {
                            reserveTimer.duration = input.Substring(input.LastIndexOf("PICK DELAY") + 13,
                                input.Length - (13 + input.LastIndexOf("PICK DELAY")));
                            reserveTimer.durationSetting = "FIXED";// input.Substring(input.LastIndexOf("SETTING") + 10, input.Length - (input.LastIndexOf("SETTING") + 10));
                        }
                        if ((input = SR.ReadLine()) != null)
                        {
                            timeRec.clearTime = input.Substring(input.LastIndexOf("DROP DELAY") + 13,
                                input.Length - (13 + input.LastIndexOf("DROP DELAY")));
                        }
                        Inputs.Add(reserveTimer);

                        timeRec.timerName = reserveTimer.outputName;
                        timeRec.setTime = reserveTimer.duration;
                        timers.Add(timeRec);
                    }
                }
            }
            Module.Add(Inputs);
        }

        private void ScanUserTimers(ArrayList Module, ArrayList timers, string TypeOfFile)
        {
            string input; bool endOfInputs = false;

            ArrayList userTimers = new ArrayList();
            if (TypeOfFile == "INS")
            {
                while (((input = SR.ReadLine()) != null) && (endOfInputs != true))
                {
                    if (input.LastIndexOf("END USER TIMERS") != -1) endOfInputs = true;
                    if (input.LastIndexOf("TIMER ") != -1)
                    {
                        UserTimer userTimer = new UserTimer();
                        if (input.LastIndexOf("SHORT") != -1)
                            userTimer.triggerName = input.Substring(input.LastIndexOf("TRIGGER LONG ") + 14,
                                (input.LastIndexOf("SHORT") - 2) - (input.LastIndexOf("TRIGGER LONG ") + 14));
                        else
                            userTimer.triggerName = input.Substring(input.LastIndexOf("TRIGGER LONG ") + 14,
                                input.Length - (15 + input.LastIndexOf("TRIGGER LONG")));
                        if ((input = SR.ReadLine()) != null)
                        {
                            if (input.LastIndexOf("SHORT") == -1)
                                userTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT  LONG") + 14,
                                    input.Length - (15 + input.LastIndexOf("OUTPUT  LONG")));
                            else
                                userTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT  LONG") + 14,
                                    (input.LastIndexOf("SHORT") - 2) - (14 + input.LastIndexOf("OUTPUT  LONG")));
                        }
                        if ((input = SR.ReadLine()) != null)
                            userTimer.duration = input.Substring(input.LastIndexOf("DURATION") + 11,
                                input.Length - (11 + input.LastIndexOf("DURATION")));
                        userTimers.Add(userTimer);

                        ML2Timer timeRec = new ML2Timer();
                        timeRec.timerName = userTimer.outputName;
                        timeRec.setTime = userTimer.duration;
                        timeRec.clearTime = "0" + "s";
                        timers.Add(timeRec);
                    }
                }
            }
            if (TypeOfFile == "NCD")
            {
                while (((input = SR.ReadLine()) != null) && (endOfInputs != true))
                {
                    if (input.LastIndexOf("END USER TIMERS") != -1) endOfInputs = true;
                    if (input.LastIndexOf("TIMER ") != -1)
                    {
                        UserTimer userTimer = new UserTimer();
                        ML2Timer timeRec = new ML2Timer();
                        userTimer.triggerName = input.Substring(input.LastIndexOf("TRIGGER") + 9,
                            input.Length - (10 + input.LastIndexOf("TRIGGER")));
                        if ((input = SR.ReadLine()) != null)
                        {
                            userTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT") + 8,
                                input.Length - (9 + input.LastIndexOf("OUTPUT")));
                        }
                        if ((input = SR.ReadLine()) != null)
                        {
                            userTimer.duration = input.Substring(input.LastIndexOf("PICK DELAY") + 13,
                                input.Length - (13 + input.LastIndexOf("PICK DELAY")));
                        }
                        if ((input = SR.ReadLine()) != null)
                        {
                            timeRec.clearTime = input.Substring(input.LastIndexOf("DROP DELAY") + 13,
                                input.Length - (13 + input.LastIndexOf("DROP DELAY")));
                        }
                        userTimers.Add(userTimer);

                        timeRec.timerName = userTimer.outputName;
                        timeRec.setTime = userTimer.duration;
                        timers.Add(timeRec);
                    }
                }
            }
            if (TypeOfFile == "WT2")
            {
                while (((input = SR.ReadLine()) != null) && (endOfInputs != true))
                {
                    if (input.LastIndexOf("END USER TIMERS") != -1) endOfInputs = true;
                    if (input.LastIndexOf("TIMER ") != -1)
                    {
                        UserTimer userTimer = new UserTimer();
                        if (input.LastIndexOf("SHORT") != -1)
                            userTimer.triggerName = input.Substring(input.LastIndexOf("TRIGGER LONG ") + 14,
                                (input.LastIndexOf("SHORT") - 2) - (input.LastIndexOf("TRIGGER LONG ") + 14));
                        else
                        {
                            //userTimer.triggerName = input.Substring(input.LastIndexOf("TRIGGER ") + 9,
                            //    input.Length - (10 + input.LastIndexOf("TRIGGER ")));
                            if (input.LastIndexOf("TRIGGER") != -1)
                                userTimer.triggerName = input.Substring(input.IndexOf("\"") + 1,
                                    input.LastIndexOf("\"") - (1 + input.IndexOf("\"")));
                        }
                        if ((input = SR.ReadLine()) != null)
                        {
                            if (input.LastIndexOf("SHORT") == -1)
                                userTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT  ") + 9,
                                    input.Length - (10 + input.LastIndexOf("OUTPUT  ")));
                            else
                                userTimer.outputName = input.Substring(input.LastIndexOf("OUTPUT  ") + 9,
                                    (input.LastIndexOf("SHORT") - 2) - (10 + input.LastIndexOf("OUTPUT  ")));
                        }
                        if ((input = SR.ReadLine()) != null)
                            userTimer.duration = input.Substring(input.LastIndexOf("DURATION") + 11,
                                input.Length - (11 + input.LastIndexOf("DURATION")));
                        userTimers.Add(userTimer);

                        ML2Timer timeRec = new ML2Timer();
                        timeRec.timerName = userTimer.outputName;
                        timeRec.setTime = userTimer.duration;
                        timeRec.clearTime = "0" + "s";
                        timers.Add(timeRec);
                    }
                }
            }
            Module.Add(userTimers);
        }

        private void ScanReservedLatches(ArrayList Module)
        {
            string input; bool endOfInputs = false;
            ArrayList reserveLatches = new ArrayList();
            while (((input = SR.ReadLine()) != null) && (endOfInputs != true))
            {
                if (input.LastIndexOf("END RESERVED LATCHES") != -1) endOfInputs = true;
                if (input.LastIndexOf("LATCH ") != -1)
                {
                    ReserveLatches reserveLatch = new ReserveLatches();
                    reserveLatch.latchName = input.Substring(input.LastIndexOf("LATCH") + 7,
                        (input.LastIndexOf("ACCESS") - 2) - (7 + input.LastIndexOf("LATCH")));
                    reserveLatch.latchAccess = input.Substring(input.LastIndexOf("ACCESS") + 9,
                            input.Length - (input.LastIndexOf("ACCESS") + 9));
                    reserveLatches.Add(reserveLatch);
                }
            }
            Module.Add(reserveLatches);
        }

        private void ScanUserLatches(ArrayList Module)
        {
            string input; bool endOfInputs = false; int test;
            ArrayList userLatches = new ArrayList();
            while (((input = SR.ReadLine()) != null) && (endOfInputs != true))
            {
                if (input.LastIndexOf("END USER LATCHES") != -1) endOfInputs = true;
                if (input.LastIndexOf("LATCH ") != -1)
                {
                    UserLatches userLatch = new UserLatches();
                    userLatch.latchNumber = input.Substring(input.IndexOf("LATCH") + 6,
                        (input.LastIndexOf("= LONG") - 1) - (6 + input.IndexOf("LATCH")));
                    test = (input.LastIndexOf("SHORT") - 2);
                    test = (input.LastIndexOf("= LONG") + 8);
                    if (input.LastIndexOf("SHORT") != -1)
                        userLatch.latchName = input.Substring(input.LastIndexOf("= LONG") + 8,
                            (input.LastIndexOf("SHORT") - 2) - (input.LastIndexOf("= LONG") + 8));
                    else
                        userLatch.latchName = input.Substring(input.LastIndexOf("= LONG") + 8,
                            (input.Length - 1) - (input.LastIndexOf("= LONG") + 8));
                    userLatches.Add(userLatch);
                }
            }
            Module.Add(userLatches);
        }

        private void ScanVPIM50(ArrayList Module)
        {
            string input; bool endOfInputs = false; int counter = 1;
            ArrayList Inputs = new ArrayList();
            while (((input = SR.ReadLine()) != null) && (endOfInputs != true))
            {
                if (input.LastIndexOf("END INPUTS") != -1) endOfInputs = true;
                if (input.LastIndexOf("INPUT ") != -1)
                {
                    while (int.Parse(input.Substring(input.LastIndexOf("INPUT ") + 6, 3)) > counter++) Inputs.Add("");
                    if (input.LastIndexOf("SHORT") != -1)
                        Inputs.Add(input.Substring(input.LastIndexOf("LONG") + 6,
                            input.LastIndexOf("SHORT") - (8 + input.LastIndexOf("LONG"))));
                    else Inputs.Add(input.Substring(input.LastIndexOf("LONG") + 6,
                             input.LastIndexOf("INITIAL STATE") - (8 + input.LastIndexOf("LONG"))));
                }
            }
            for (; counter < 12; counter++) Inputs.Add("");
            Module.Add(Inputs);
        }

        private void ScanVROM50(ArrayList Module)
        {
            string output; bool endOfInputs = false; int counter = 1;
            ArrayList Outputs = new ArrayList();
            while (((output = SR.ReadLine()) != null) && (endOfInputs != true))
            {
                if (output.LastIndexOf("END OUTPUTS") != -1) endOfInputs = true;
                if (output.LastIndexOf("OUTPUT ") != -1)
                {
                    while (int.Parse(output.Substring(output.LastIndexOf("OUTPUT  ") + 6, 3)) > counter++) Outputs.Add("");
                    if (output.LastIndexOf("SHORT") != -1)
                        Outputs.Add(output.Substring(output.LastIndexOf("LONG") + 6,
                            output.LastIndexOf("SHORT") - (8 + output.LastIndexOf("LONG"))));
                    else Outputs.Add(output.Substring(output.LastIndexOf("LONG") + 6,
                             output.LastIndexOf("INITIAL STATE") - (8 + output.LastIndexOf("LONG"))));
                }
            }
            for (; counter < 8; counter++) Outputs.Add("");
            Module.Add(Outputs);
        }

        private void ScanVLOMFT110(ArrayList Module)
        {
            string output; bool endOfInputs = false; int counter = 1;
            ArrayList Outputs = new ArrayList();
            while (((output = SR.ReadLine()) != null) && (endOfInputs != true))
            {
                if (output.LastIndexOf("END OUTPUTS") != -1) endOfInputs = true;
                if (output.LastIndexOf("OUTPUT ") != -1)
                {
                    while (int.Parse(output.Substring(output.LastIndexOf("OUTPUT ") + 6, 3)) > counter++) Outputs.Add("");
                    if (output.LastIndexOf("SHORT") != -1)
                        Outputs.Add(output.Substring(output.LastIndexOf("LONG") + 6,
                            output.LastIndexOf("SHORT") - (8 + output.LastIndexOf("LONG"))));
                    else Outputs.Add(output.Substring(output.LastIndexOf("LONG") + 6,
                             output.LastIndexOf("INITIAL STATE") - (8 + output.LastIndexOf("LONG"))));
                }
            }
            for (; counter < 8; counter++) Outputs.Add("");
            Module.Add(Outputs);
        }

        private void ScanSequenceNumber(ArrayList Module, string line)
        {
            Module.Add(line.Substring(line.LastIndexOf("SEQUENCE NUMBER ") + 16,
                line.Length - (line.LastIndexOf("SEQUENCE NUMBER ") + 16)));
        }

        private void ScanFaultName(ArrayList Module)
        {
            string output;
            if ((output = SR.ReadLine()) != null)
                Module.Add(output.Substring(output.LastIndexOf("FAULT = LONG ") + 14,
                    output.Length - (output.LastIndexOf("FAULT = LONG ") + 15)));
        }

        private void ScanNVC422(ArrayList Module)
        {
            string input; string output; bool endOfInputs = false; int counter = 1;
            ArrayList Outputs = new ArrayList();
            while (((output = SR.ReadLine()) != null) && (endOfInputs != true))
            {
                if (output.LastIndexOf("END OUTPUTS") != -1) endOfInputs = true;
                if (output.LastIndexOf("OUTPUT ") != -1)
                {
                    while (int.Parse(output.Substring(output.LastIndexOf("OUTPUT ") + 6, 3)) > counter++) Outputs.Add("");
                    if (output.LastIndexOf("SHORT") != -1)
                        Outputs.Add(output.Substring(output.LastIndexOf("LONG") + 6,
                            output.LastIndexOf("SHORT") - (8 + output.LastIndexOf("LONG"))));
                    else Outputs.Add(output.Substring(output.LastIndexOf("LONG") + 6,
                             output.LastIndexOf("INITIAL STATE") - (8 + output.LastIndexOf("LONG"))));
                }
            }
            for (; counter < 64; counter++) Outputs.Add("");

            endOfInputs = false; counter = 1;
            ArrayList Inputs = new ArrayList();
            while (((input = SR.ReadLine()) != null) && (endOfInputs != true))
            {
                if (input.LastIndexOf("END INPUTS") != -1) endOfInputs = true;
                if (input.LastIndexOf("INPUT ") != -1)
                {
                    while (int.Parse(input.Substring(input.LastIndexOf("INPUT ") + 5, 3)) > counter++) Inputs.Add("");
                    if (input.LastIndexOf("SHORT") != -1)
                        Inputs.Add(input.Substring(input.LastIndexOf("LONG") + 6,
                            input.LastIndexOf("SHORT") - (8 + input.LastIndexOf("LONG"))));
                    else Inputs.Add(input.Substring(input.LastIndexOf("LONG") + 6,
                             input.LastIndexOf("INITIAL STATE") - (8 + input.LastIndexOf("LONG"))));
                }
            }
            for (; counter < 48; counter++) Inputs.Add("");
            Module.Add(Outputs);
            Module.Add(Inputs);
        }

        private void ScanEVTC(ArrayList Module)
        {
            string input; string output; bool endOfInputs = false; int counter = 1;
            ArrayList Outputs = new ArrayList();
            while (((output = SR.ReadLine()) != null) && (endOfInputs != true))
            {
                if (output.LastIndexOf("END OUTPUTS") != -1) endOfInputs = true;
                if (output.LastIndexOf("OUTPUT ") != -1)
                {
                    while (int.Parse(output.Substring(output.LastIndexOf("OUTPUT ") + 6, 3)) > counter++) Outputs.Add("");
                    if (output.LastIndexOf("SHORT") != -1)
                        Outputs.Add(output.Substring(output.LastIndexOf("LONG") + 6,
                            output.LastIndexOf("SHORT") - (8 + output.LastIndexOf("LONG"))));
                    else Outputs.Add(output.Substring(output.LastIndexOf("LONG") + 6,
                             output.LastIndexOf("INITIAL STATE") - (8 + output.LastIndexOf("LONG"))));
                }
            }
            for (; counter < 66; counter++) Outputs.Add("");

            endOfInputs = false; counter = 1;
            ArrayList Inputs = new ArrayList();
            while (((input = SR.ReadLine()) != null) && (endOfInputs != true))
            {
                if (input.LastIndexOf("END INPUTS") != -1) endOfInputs = true;
                if (input.LastIndexOf("INPUT ") != -1)
                {
                    while (int.Parse(input.Substring(input.LastIndexOf("INPUT ") + 5, 3)) > counter++) Inputs.Add("");
                    if (input.LastIndexOf("SHORT") != -1)
                        Inputs.Add(input.Substring(input.LastIndexOf("LONG") + 6,
                            input.LastIndexOf("SHORT") - (8 + input.LastIndexOf("LONG"))));
                    else Inputs.Add(input.Substring(input.LastIndexOf("LONG") + 6,
                             input.LastIndexOf("INITIAL STATE") - (8 + input.LastIndexOf("LONG"))));
                }
            }
            for (; counter < 66; counter++) Inputs.Add("");
            Module.Add(Outputs);
            Module.Add(Inputs);
        }

        private void ScanVTC(ArrayList Module)
        {
            string input; string output; bool endOfInputs = false; int counter = 1;
            ArrayList Outputs = new ArrayList();
            while (((output = SR.ReadLine()) != null) && (endOfInputs != true))
            {
                if (output.LastIndexOf("END OUTPUTS") != -1) endOfInputs = true;
                if (output.LastIndexOf("OUTPUT ") != -1)
                {
                    while (int.Parse(output.Substring(output.LastIndexOf("OUTPUT ") + 6, 3)) > counter++) Outputs.Add("");
                    if (output.LastIndexOf("SHORT") != -1)
                        Outputs.Add(output.Substring(output.LastIndexOf("LONG") + 6,
                            output.LastIndexOf("SHORT") - (8 + output.LastIndexOf("LONG"))));
                    else Outputs.Add(output.Substring(output.LastIndexOf("LONG") + 6,
                             output.LastIndexOf("INITIAL STATE") - (8 + output.LastIndexOf("LONG"))));
                }
            }
            for (; counter < 17; counter++) Outputs.Add("");

            endOfInputs = false; counter = 1;
            ArrayList Inputs = new ArrayList();
            while (((input = SR.ReadLine()) != null) && (endOfInputs != true))
            {
                if (input.LastIndexOf("END INPUTS") != -1) endOfInputs = true;
                if (input.LastIndexOf("INPUT ") != -1)
                {
                    while (int.Parse(input.Substring(input.LastIndexOf("INPUT ") + 5, 3)) > counter++) Inputs.Add("");
                    if (input.LastIndexOf("SHORT") != -1)
                        Inputs.Add(input.Substring(input.LastIndexOf("LONG") + 6,
                            input.LastIndexOf("SHORT") - (8 + input.LastIndexOf("LONG"))));
                    else Inputs.Add(input.Substring(input.LastIndexOf("LONG") + 6,
                             input.LastIndexOf("INITIAL STATE") - (8 + input.LastIndexOf("LONG"))));
                }
            }
            for (; counter < 17; counter++) Inputs.Add("");
            Module.Add(Outputs);
            Module.Add(Inputs);
        }

        private int GetSlotNumber(string line)
        {
            return int.Parse(line.Substring(line.LastIndexOf("SLOT") + 5, 2));
        }

        private string ParseGCSSVersion()
        {
            string GCSSVersion = "";
            string line; bool endOfInst = false;
            SR = File.OpenText(FileName.Text);
            while (((line = SR.ReadLine()) != null) && (endOfInst != true))
            {
                if (line.LastIndexOf("GCSS RELEASE") != -1)
                {
                    endOfInst = true;
                    if (line.Length > 14) GCSSVersion = line.Substring(15, line.Length - 15);
                }
            }
            SR.Close();
            return (GCSSVersion);
        }

        private string ParseML2InstallationName(string filename)
        {
            string installationName = "";
            string line; bool endOfInst = false;
            SR = File.OpenText(filename);
            //installationName = "";
            while (((line = SR.ReadLine()) != null) && (endOfInst != true))
            {
                if (line.LastIndexOf("MICROLOK_II PROGRAM") != -1)
                {
                    endOfInst = true;
                    if (line.Length > 19) installationName = line.Substring(20, line.Length - 21);
                }
            }
            SR.Close();
            return (installationName);
        }

        private string ParseTXTInstallationName(string filename)
        {
            return (filename);
        }


        private string ParseGN2InstallationName()
        {
            string installationName = "";
            string line; bool endOfInst = false;
            SR = File.OpenText(FileName.Text);
            //installationName = "";
            while (((line = SR.ReadLine()) != null) && (endOfInst != true))
            {
                if (line.LastIndexOf("GENISYS_II PROGRAM") != -1)
                {
                    endOfInst = true;
                    if (line.Length > 19) installationName = line.Substring(20, line.Length - 21);
                }
            }
            SR.Close();
            return (installationName);
        }

        private string ParseMLKInstallationName()
        {
            string installationName = "";
            string line; bool endOfInst = false;
            SR = File.OpenText(FileName.Text);
            //installationName = "";
            while (((line = SR.ReadLine()) != null) && (endOfInst != true))
            {
                if (line.LastIndexOf("MICROLOK PROGRAM") != -1)
                {
                    endOfInst = true;
                    if (line.Length > 16) installationName = line.Substring(17, line.Length - 18);
                }
            }
            SR.Close();
            return (installationName);
        }

        private string ParseInstallationName()
        {
            string installationName = "";
            string line; bool endOfInst = false;
            SR = File.OpenText(FileName.Text);
            //installationName = "";
            while (((line = SR.ReadLine()) != null) && (endOfInst != true))
            {
                if (line.LastIndexOf("INSTALLATION") != -1)
                {
                    endOfInst = true;
                    if (line.Length > 11) installationName = line.Substring(12, line.Length - 12);
                }
            }
            SR.Close();
            return (installationName);
        }

        private void ParseVersionRecord(ArrayList versionRecords, string filenameString)
        {
            string line; bool endOfVer = false;
            string versionType = "";

            int currentVersionMinor = 0;
            int currentVersionMajor = 0;
            string currentVersionName = "";

            SR = File.OpenText(filenameString);

            while (((line = SR.ReadLine()) != null) && (endOfVer != true))
            {
                if ((string.Compare(fileType, "INS") == 0) || (string.Compare(fileType, "WT2") == 0))
                    if (line.LastIndexOf("END CONFIGURATION CONTROL") != -1) endOfVer = true;
                if (string.Compare(fileType, "NCD") == 0)
                    if (line.LastIndexOf("END NVCONFIGURATION CONTROL") != -1) endOfVer = true;
                if (line.LastIndexOf("MODIFICATION RECORD") != -1) versionType = "Modification Record";
                if (line.LastIndexOf("CHECKING RECORD") != -1) versionType = "Checking Record";
                if (line.LastIndexOf("APPROVAL RECORD") != -1) versionType = "Approval Record";
                if (line.LastIndexOf("CREATED BY") != -1)
                    currentVersionName = line.Substring(line.LastIndexOf("CREATED BY") + 12,
                        line.Length - (line.LastIndexOf("CREATED BY") + 13));
                if (line.LastIndexOf("CURRENT VERSION") != -1)
                {
                    currentVersionMajor = int.Parse(line.Substring(line.LastIndexOf("CURRENT VERSION") + 16, 2));
                    currentVersionMinor = int.Parse(line.Substring(line.LastIndexOf("CURRENT VERSION") + 19, 4));
                }
                else if ((line.LastIndexOf("VERSION") != -1) && line.LastIndexOf("DATA LANGUAGE SPEC VERSION") == -1)
                {
                    VersionRecord versionRec = new VersionRecord();
                    versionRec.majorVer = int.Parse(line.Substring(line.LastIndexOf("VERSION") + 8, 2));
                    versionRec.minorVer = int.Parse(line.Substring(line.LastIndexOf("VERSION") + 11, 4));
                    versionRec.name_Person = line.Substring(line.LastIndexOf("BY ") + 4,
                        line.LastIndexOf("ON ") - (6 + line.LastIndexOf("BY ")));
                    IFormatProvider culture = new CultureInfo("fr-FR", true);
                    string myDateTimeFrenchValue = line.Substring(line.LastIndexOf("ON ") + 7, 19); ;
                    versionRec.date_time = DateTime.Parse(myDateTimeFrenchValue, culture, DateTimeStyles.NoCurrentDateDefault);
                    if (versionType == "Modification Record") versionRec.typeOfVersion = "Modification";
                    if (versionType == "Checking Record") versionRec.typeOfVersion = "Checking";
                    if (versionType == "Approval Record") versionRec.typeOfVersion = "Approval";
                    versionRecords.Add(versionRec);
                }
            }
            SR.Close();
            //checkVersionRecords(versionRecords);
        }

        private void checkVersionRecords(ArrayList versRec)
        {
            for (int i = 1; i < versRec.Count; i++)
            {
                VersionRecord VPtrprev = (VersionRecord)versRec[i - 1];
                VersionRecord VPtr = (VersionRecord)versRec[i];
                if (VPtr.date_time > VPtrprev.date_time)
                    MessageBox.Show("Version Records are not in Date order: "
                        + "v" + VPtr.majorVer + "." + VPtr.minorVer + ", " +
                        VPtr.date_time, "Version Records Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



            ArrayList outVersRec = new ArrayList();
            ArrayList sortedlist = new ArrayList();
            for (int i = 0; i < versRec.Count; i++)
            {
                VersionRecord VPtr = (VersionRecord)versRec[i];
                DateTime dateAndTime = VPtr.date_time;
                sortedlist.Add(dateAndTime);
            }
            sortedlist.Sort();
            for (int i = 0; i < versRec.Count; i++)
                for (int j = 0; j < versRec.Count; j++)
                {
                    VersionRecord VPtr = (VersionRecord)versRec[j];
                    if (DateTime.Compare((DateTime)sortedlist[versRec.Count - i - 1], VPtr.date_time) == 0)

                        outVersRec.Add(VPtr);
                }


        }

        public string GetRungName(string treeViewString)
        {
            return treeViewString.Substring(treeViewString.LastIndexOf(":") + 2);
        }

        private void treeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            rungName = GetRungName(treeView.Nodes[treeView.SelectedNode.Index].ToString());
            //try
            {
                if (!reloading) ShowRungWindow();
            }
            /*catch
            {
                MessageBox.Show("Search" , "Logic Navigator failure: " + rungName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }*/
        }

        private void ShowRungWindow()
        {
            int newIndex = 0;
            int oldIndex = 0;
            try { newIndex = findRung(interlockingNew, rungName); }
            catch { MessageBox.Show(rungName, "Logic Navigator failure: findrung, new", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            try { oldIndex = findRung(interlockingOld, rungName); }
            catch { MessageBox.Show(rungName, "Logic Navigator failure: findrung, old ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            if (newIndex == -1)
            {
                frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, oldIndex - 1,
                    oldIndex - 1, scaleFactor, "All Old", drawFont, "", gridlines, showTimers, HighColor, LowColor);
                objfrmMChild.Size = new Size(objfrmMChild.RecommendedWidthofWindow(oldIndex - 1), objfrmMChild.RecommendedHeightofWindow(oldIndex - 1));
                objfrmMChild.Location = new System.Drawing.Point(1, 1);
                objfrmMChild.Text = rungName;
                objfrmMChild.MdiParent = this;
                objfrmMChild.Show();
            }
            else
            {
                if (oldIndex == -1)
                {
                    frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, newIndex - 1,
                        newIndex - 1, scaleFactor, "All New", drawFont, "", gridlines, showTimers, HighColor, LowColor);
                    objfrmMChild.Size = new Size(objfrmMChild.RecommendedWidthofWindow(newIndex - 1), objfrmMChild.RecommendedHeightofWindow(newIndex - 1));
                    objfrmMChild.Location = new System.Drawing.Point(1, 1);
                    objfrmMChild.Text = rungName;
                    objfrmMChild.MdiParent = this;
                    objfrmMChild.Show();
                }
                else
                {
                    frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, oldIndex - 1,
                        newIndex - 1, scaleFactor, "Normal", drawFont, "", gridlines, showTimers, HighColor, LowColor);
                    objfrmMChild.Size = new Size(objfrmMChild.RecommendedWidthofWindow(newIndex - 1), objfrmMChild.RecommendedHeightofWindow(newIndex - 1));
                    objfrmMChild.Location = new System.Drawing.Point(1, 1);
                    objfrmMChild.Text = rungName;
                    objfrmMChild.MdiParent = this;
                    objfrmMChild.Show();
                }
            }
            //////////////////////////////
            if (string.Compare(toolbarState, "Tile") == 0)
                this.LayoutMdi(MdiLayout.TileHorizontal);
            else
                this.ActiveMdiChild.WindowState = FormWindowState.Maximized;
            ///////////////////////////
        }

        private void menuItem2_Click(object sender, System.EventArgs e) //Choose rungs to compare
        {
            CompareRungs();
        }

        private void CompareRungs()
        {
            frmMChild_Choose objfrmMChild = new frmMChild_Choose(interlockingOld, interlockingNew, timersOld, timersNew, drawFont);
            objfrmMChild.Text = "Choose Rungs";
            objfrmMChild.MdiParent = this;
            objfrmMChild.Show();
        }

        private void menuItem6_Click(object sender, System.EventArgs e) //Search Rungs
        {
            SearchWindow();
        }

        private void SearchWindow()
        {
            try
            {
                frmMChild_Search objfrmMChild = new frmMChild_Search(interlockingOld, interlockingNew, timersOld, timersNew, drawFont, HighColor, LowColor);
                objfrmMChild.Text = "Search";
                objfrmMChild.MdiParent = this;
                objfrmMChild.Show();
            }
            catch
            {
                //MessageBox.Show("Search" , "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

        private void menuItem8_Click(object sender, System.EventArgs e) ///Version History
        {
            frmMChild_Versions objfrmMChild = new frmMChild_Versions(interlockingOld, interlockingNew, 1 - 1,
                1 - 1, scaleFactor, "Normal", versionRecOld, versionRecNew);
            objfrmMChild.Size = new Size(700, 400);
            objfrmMChild.Location = new System.Drawing.Point(1, 1);
            objfrmMChild.Text = rungName;
            objfrmMChild.MdiParent = this;
            objfrmMChild.Show();
        }

        private void menuItem9_Click(object sender, System.EventArgs e) //Close all
        {
            Close_All();
        }

        private void Close_All()
        {
            Close_All_Rungs();
            DisposeOldMemory(); DisposeNewMemory();
            reloading = true;
            treeView.Nodes.Clear();
            reloading = false;
            this.View.Visible = false;
            this.CloseAll.Visible = false;
            this.mnuFileCloseChild.Visible = false;
            this.Tools.Visible = false;
            //this.mnuFileSep1.Visible = false;
            OldFileName.Text = "";
            NewFileName.Text = "";
            this.Text = "Logic Navigator";
            HideMenu();
            statusStrip1.Visible = false;
            HideRungPane();
        }

        private void DisposeNewMemory()
        {
            interlockingNew.Clear();
            timersNew.Clear();
            Housings_New.Clear();
            versionRecNew.Clear();
            installationNameNew = "";
            GCSSVersionNew = "";
        }

        private void DisposeOldMemory()
        {
            interlockingOld.Clear();
            timersOld.Clear();
            Housings_Old.Clear();
            versionRecOld.Clear();
            installationNameOld = "";
            GCSSVersionOld = "";
        }

        private void menuItem3_Click(object sender, System.EventArgs e) // Font dialog
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                drawFont = fontDialog1.Font;
                for (int x = 0; x < this.MdiChildren.Length; x++)
                {
                    if (this.MdiChildren[x].Name == "frmMChild")
                    {
                        frmMChild objfrmMChild = (frmMChild)this.MdiChildren[x];
                        objfrmMChild.drawFnt = drawFont;
                        objfrmMChild.InvalidateForm();
                    }
                }
            }
        }

        private void menuItem9_Click_1(object sender, System.EventArgs e) // View Housings
        {
            ViewHousings();
        }

        private void ViewHousings()
        {
            frmMChild_Housings objfrmMChild = new frmMChild_Housings(Housings_Old, Housings_New, interlockingOld, interlockingNew, timersOld, timersNew, drawFont, "Normal");
            objfrmMChild.Size = new Size(700, 400);
            objfrmMChild.Text = "Housings";
            objfrmMChild.MdiParent = this;
            objfrmMChild.Show();
        }

        private void DoDatabase()
        {  //Prepare DataGrid view of rungs
            string statusInfo = "";

            ////////////////////////////////////
            ///

            System.Data.DataTable points = new System.Data.DataTable("Points");
            points.Columns.Add(new DataColumn("Key", typeof(int)));
            points.Columns.Add(new DataColumn("Old", typeof(string)));
            points.Columns.Add(new DataColumn("New", typeof(string)));
            points.Columns.Add(new DataColumn("Name", typeof(string)));
            points.Columns.Add(new DataColumn("Status", typeof(string)));

            int rungNumber;
            for (int i = 0; i < interlockingOld.Count; i++)
            {
                statusInfo = "";
                ArrayList rungPointer = (ArrayList)interlockingOld[i];
                rungNumber = findRung(interlockingNew, (string)rungPointer[rungPointer.Count - 1]);
                if ((int)rungNumber != -1)
                    if (!RungsSame(rungPointer, (ArrayList)interlockingNew[rungNumber - 1])) //Same Name, but differing contacts
                        statusInfo = "Changed";//newNode.ForeColor = Color.Lime;				
                if ((int)rungNumber != -1) //Same name, same contacts, but different rung numbers
                    if (rungNumber != (int)rungPointer[0])
                        if (RungsSame(rungPointer, (ArrayList)interlockingNew[rungNumber - 1]))
                            if ((int)rungNumber != -1) statusInfo = "";//"Moved";//newNode.ForeColor = Color.MediumSeaGreen;
                if ((int)rungNumber == -1) //rung removed, (rung not found in new data)
                    statusInfo = "Old"; //newNode.ForeColor = Color.Blue;
                if ((int)rungNumber == -1)
                    points.Rows.Add(new object[]{(int) i, rungPointer[0].ToString(), "-",
                                                    (string) rungPointer[rungPointer.Count-1], statusInfo.ToString()});
                if ((int)rungNumber != -1)
                    points.Rows.Add(new object[]{(int) i, rungPointer[0].ToString(), rungNumber.ToString(),
                                                    (string) rungPointer[rungPointer.Count-1], statusInfo.ToString()});
            }
            for (int i = 0; i < interlockingNew.Count; i++)
            {
                ArrayList rungPointer = (ArrayList)interlockingNew[i];
                rungNumber = findRung(interlockingOld, (string)rungPointer[rungPointer.Count - 1]);
                if (rungNumber == -1)
                    points.Rows.Add(new object[] { (int)i, "-", rungPointer[0].ToString(), (string)rungPointer[rungPointer.Count - 1], "New" });
            }
            RungGrid.DataSource = points;
        }


        private void CreateDataGridStyle()
        {
            DataGridColumnStyle GridSeqStyle;
            DGStyle = new DataGridTableStyle();

            DGStyle.MappingName = "Table1";

            GridSeqStyle = new DataGridTextBoxColumn();
            GridSeqStyle.MappingName = "Column1";
            GridSeqStyle.HeaderText = "Column1";
            GridSeqStyle.Width = 100;
            DGStyle.GridColumnStyles.Add(GridSeqStyle);

            PropertyDescriptorCollection pcol = this.BindingContext[myDataSet, "Table1"].GetItemProperties();

            DGStyle.AllowSorting = true;
            DGStyle.RowHeadersVisible = true;
        }

        private void RungGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DataGrid myGrid = (DataGrid)sender;

            System.Windows.Forms.DataGrid.HitTestInfo hti;
            hti = myGrid.HitTest(e.X, e.Y);
            string message = "You clicked ";

            switch (hti.Type)
            {
                case System.Windows.Forms.DataGrid.HitTestType.None:
                    message += "the background.";
                    break;
                case System.Windows.Forms.DataGrid.HitTestType.Cell:
                    {
                        message += "cell at row " + hti.Row + ", col " + hti.Column;
                        statusBar1.Text = message + ", " + RungGrid[hti.Row, 2/*hti.Column*/].ToString();
                        rungName = RungGrid[hti.Row, 2].ToString();
                        if (!reloading) ShowRungWindow();
                        break;
                    }
                case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader:
                    message += "the column header for column " + hti.Column;
                    break;
                case System.Windows.Forms.DataGrid.HitTestType.RowHeader:
                    message += "the row header for row " + hti.Row;
                    break;
                case System.Windows.Forms.DataGrid.HitTestType.ColumnResize:
                    message += "the column resizer for column " + hti.Column;
                    break;
                case System.Windows.Forms.DataGrid.HitTestType.RowResize:
                    message += "the row resizer for row " + hti.Row;
                    break;
                case System.Windows.Forms.DataGrid.HitTestType.Caption:
                    message += "the caption";
                    break;
                case System.Windows.Forms.DataGrid.HitTestType.ParentRows:
                    message += "the parent row";
                    break;
            }
        }

        private void menuItem14_Click(object sender, System.EventArgs e) //Change rung view to grid
        {
            treeView.Visible = false;
            RungGrid.Visible = true;
            splitter1.SplitPosition = 100;
        }

        private void menuItem15_Click(object sender, System.EventArgs e) //Change rung view to tree
        {
            treeView.Visible = true;
            RungGrid.Visible = false;
        }

        private void menuItem16_Click(object sender, System.EventArgs e) //Close All Rungs
        {
            Close_All_Rungs();
        }

        private void Close_All_Rungs()
        {
            do
            {
                try
                {
                    if (this.ActiveMdiChild.Name == "frmMChild")
                    {
                        frmMChild objfrmMChild = (frmMChild)this.ActiveMdiChild;
                        objfrmMChild.Close();
                    }
                    if (this.ActiveMdiChild.Name == "frmMChild_Card")
                    {
                        frmMChild_Card objfrmMChild = (frmMChild_Card)this.ActiveMdiChild;
                        objfrmMChild.Close();
                    }
                    if (this.ActiveMdiChild.Name == "frmMChild_Housings")
                    {
                        frmMChild_Housings objfrmMChild = (frmMChild_Housings)this.ActiveMdiChild;
                        objfrmMChild.Close();
                    }
                    if (this.ActiveMdiChild.Name == "frmMChild_Choose")
                    {
                        frmMChild_Choose objfrmMChild = (frmMChild_Choose)this.ActiveMdiChild;
                        objfrmMChild.Close();
                    }
                    if (this.ActiveMdiChild.Name == "frmMChild_Search")
                    {
                        frmMChild_Search objfrmMChild = (frmMChild_Search)this.ActiveMdiChild;
                        objfrmMChild.Close();
                    }
                    if (this.ActiveMdiChild.Name == "frmMChild_Versions")
                    {
                        frmMChild_Versions objfrmMChild = (frmMChild_Versions)this.ActiveMdiChild;
                        objfrmMChild.Close();
                    }
                    if (this.ActiveMdiChild.Name == "frmMChild_SimInputs")
                    {
                        frmMChild_SimInputs objfrmMChild = (frmMChild_SimInputs)this.ActiveMdiChild;
                        objfrmMChild.Close();
                    }
                    if (this.ActiveMdiChild.Name == "frmMChild_SimOutputs")
                    {
                        frmMChild_SimOutputs objfrmMChild = (frmMChild_SimOutputs)this.ActiveMdiChild;
                        objfrmMChild.Close();
                    }
                    if (this.ActiveMdiChild.Name == "frmAbout")
                    {
                        frmAbout objfrmMChild = (frmAbout)this.ActiveMdiChild;
                        objfrmMChild.Close();
                    }
                    if (this.ActiveMdiChild.Name == "frmMChild_Chess")
                    {
                        frmMChild_Chess objfrmMChild = (frmMChild_Chess)this.ActiveMdiChild;
                        objfrmMChild.Close();
                    }
                    if (this.ActiveMdiChild.Name == "frmMChild_Log")
                    {
                        frmMChild_Log objfrmMChild = (frmMChild_Log)this.ActiveMdiChild;
                        objfrmMChild.Close();
                    }
                    if (this.ActiveMdiChild.Name == "frmMChild_SimMap")
                    {
                        frmMChild_SimMap objfrmMChild = (frmMChild_SimMap)this.ActiveMdiChild;
                        objfrmMChild.Close();
                    }
                    if (this.ActiveMdiChild.Name == "frmMChild_Visualiser")
                    {
                        frmMChild_Visualiser objfrmMChild = (frmMChild_Visualiser)this.ActiveMdiChild;
                        objfrmMChild.Close();
                    }
                    else
                    {
                        frmSChild objfrmSChild = (frmSChild)this.ActiveMdiChild;
                        objfrmSChild.Close();
                    }
                }
                catch
                {
                    //MessageBox.Show("Error closing rungs", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                }
            }
            while (this.ActiveMdiChild != null);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (RungGrid.TableStyles[0].GridColumnStyles[1].Width == 25)//button1.Text == ">>")
            {
                button1.Text = "";//<<";
                RungGrid.Width = 244;
                treeView.Width = 244;
                RungGrid.TableStyles[0].GridColumnStyles[1].Width = 32;
                RungGrid.TableStyles[0].GridColumnStyles[1].Width = 32;
                RungGrid.TableStyles[0].GridColumnStyles[1].Width = 32;
                RungGrid.TableStyles[0].GridColumnStyles[2].Width = 100;
                RungGrid.TableStyles[0].GridColumnStyles[3].Width = 62;
            }
            else
            {
                button1.Text = "";//>>";
                RungGrid.Width = 144;
                treeView.Width = 144;
                RungGrid.TableStyles[0].GridColumnStyles[0].Width = 25;
                RungGrid.TableStyles[0].GridColumnStyles[1].Width = 25;
                RungGrid.TableStyles[0].GridColumnStyles[2].Width = 62;
                RungGrid.TableStyles[0].GridColumnStyles[3].Width = 10;
            }
        }



        private void HelpButton()
        {
            frmWeb objfrmWeb = new frmWeb("https://logicnavigator.weebly.com");
            objfrmWeb.ShowDialog();
        }

        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            statusBar1.Text = e.Button.Text;
            if (string.Compare(e.Button.Text, "Help") == 0)
                HelpButton();
            if (string.Compare(e.Button.Text, "New File") == 0)
                AppOpenNewFile();
            if (string.Compare(e.Button.Text, "Open File") == 0)
                AppOpenNewFile();
            if (string.Compare(e.Button.Text, "Search") == 0)
                SearchWindow();
            if (string.Compare(e.Button.Text, "Old File") == 0)
                AppOpenOldFile();
            if (string.Compare(e.Button.Text, "Open Project") == 0)
                openProjectFile("");
            if (string.Compare(e.Button.Text, "Export to text file") == 0)
                SaveFile(interlockingNew, timersNew, "New");
            if (string.Compare(e.Button.Text, "Rungs") == 0)
                if (e.Button.Pushed == true)
                    ShowRungPane();
                else HideRungPane();
            if (string.Compare(e.Button.Text, "Close Files") == 0)
                Close_All();
            if (string.Compare(e.Button.Text, "Close Rungs") == 0)
                Close_All_Rungs();
            if (string.Compare(e.Button.Text, "Compare Rungs") == 0)
                CompareRungs();
            if (string.Compare(e.Button.Text, "Housings") == 0)
                ViewHousings();
            if (string.Compare(e.Button.Text, "Zoom In") == 0)
                ZoomInRungs();
            if (string.Compare(e.Button.Text, "Zoom Out") == 0)
                ZoomOutRungs();
            // if (string.Compare(e.Button.Text, "Housings") == 0)
            //   ViewHousings();
            if (string.Compare(e.Button.Text, "Tile") == 0)
            {
                this.LayoutMdi(MdiLayout.TileHorizontal);
                e.Button.Text = "Maximise"; toolbarState = "Tile";
            }
            else
                if (string.Compare(e.Button.Text, "Maximise") == 0)
            {
                e.Button.Text = "Tile"; toolbarState = "Maximise";
                if (this.MdiChildren.Length < 1) statusBar1.Text = "No rungs open";
                else { this.ActiveMdiChild.WindowState = FormWindowState.Maximized; statusBar1.Text = ""; }

            }
        }

        private void ZoomOutRungs()
        {
            if (this.MdiChildren.Length < 1) statusBar1.Text = "No rungs open";
            else
            {
                if (this.ActiveMdiChild.Name == "frmMChild")
                {
                    frmMChild objfrmMChild = (frmMChild)this.ActiveMdiChild;
                    objfrmMChild.applyzoom(0.8f, -1, -1);
                    //objfrmMChild.scaleFactor = objfrmMChild.scaleFactor * 0.9F;
                    objfrmMChild.InvalidateForm();
                }
                if (this.ActiveMdiChild.Name == "frmMChild_SimMap")
                {
                    frmMChild_SimMap objfrmMChild = (frmMChild_SimMap)this.ActiveMdiChild;
                    //objfrmMChild.scaleFactor = objfrmMChild.scaleFactor / 1.25F;
                    objfrmMChild.applyzoom(0.8f, -1, -1);
                    objfrmMChild.Invalidate();
                }
                statusBar1.Text = "";
            }
        }

        private void ZoomInRungs()
        {
            if (this.MdiChildren.Length < 1) statusBar1.Text = "No rungs open";
            else
            {
                if (this.ActiveMdiChild.Name == "frmMChild")
                {
                    frmMChild objfrmMChild = (frmMChild)this.ActiveMdiChild;
                    objfrmMChild.applyzoom(1.25f, -1, -1);
                    //objfrmMChild.scaleFactor = objfrmMChild.scaleFactor * 1.1F;
                    objfrmMChild.InvalidateForm();
                }
                if (this.ActiveMdiChild.Name == "frmMChild_SimMap")
                {
                    frmMChild_SimMap objfrmMChild = (frmMChild_SimMap)this.ActiveMdiChild;
                    //objfrmMChild.scaleFactor = objfrmMChild.scaleFactor * 1.25F;
                    objfrmMChild.applyzoom(1.25f, -1, -1);
                    objfrmMChild.Invalidate();
                }
                statusBar1.Text = "";
            }
        }

        private void ShowRungPane()
        {
            treeView.Visible = true;
            RungGrid.Visible = false;
            label1.Visible = true;
            button1.Visible = true;
            splitter1.Visible = true;
            button2.Visible = true;
            this.toolBar1.Buttons[9].ImageIndex = 19;
            if (this.MdiChildren.Length < 1) statusBar1.Text = "No rungs open";
            else
            {
                if (string.Compare(toolbarState, "Tile") == 0)
                    this.LayoutMdi(MdiLayout.TileHorizontal);
                else
                    this.ActiveMdiChild.WindowState = FormWindowState.Maximized;
                statusBar1.Text = "";
            }
        }

        private void HideRungPane()
        {
            treeView.Visible = false;
            RungGrid.Visible = false;
            label1.Visible = false;
            button1.Visible = false;
            splitter1.Visible = false;
            button2.Visible = false;
            this.toolBar1.Buttons[9].ImageIndex = 18;
            //if (this.MdiChildren.Length < 1) statusBar1.Text = "No rungs open";
            //else
            //{
            //    if (string.Compare(toolbarState, "Tile") == 0)
            //        this.LayoutMdi(MdiLayout.TileHorizontal);
            //    else
            //        this.ActiveMdiChild.WindowState = FormWindowState.Maximized;
            //    statusBar1.Text = "";
            //}
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            HideRungPane();
        }

        private void menuItem17_Click(object sender, System.EventArgs e)
        {
            if (this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.None)
            {
                if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized) windowCurrentlyMaximised = true;
                else
                {
                    windowSize.Y = this.Size.Height;
                    windowSize.X = this.Size.Width;
                    this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    windowCurrentlyMaximised = false;
                }
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            }
            else
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                if (windowCurrentlyMaximised == false)
                {
                    this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    this.Size = new System.Drawing.Size(windowSize);
                }
            }
        }

        private void menuItem18_Click(object sender, System.EventArgs e)
        {
            SaveFile(interlockingNew, timersNew, "New");
        }

        private void menuItem19_Click(object sender, System.EventArgs e)
        {
            SaveFile(interlockingOld, timersOld, "Old");
        }

        private void SaveFile(ArrayList interlockingSave, ArrayList timersSave, string filetype)
        {
            Stream myStream = null;
            ArrayList exportVersion;
            bool found = false;
            string CRLF = "\r\n";

            bool dialog1 = false; bool dialog2 = false;
            string saveNewFile = NewFileName.Text.ToString();
            string saveOldFile = OldFileName.Text.ToString();
            saveFileDialog1.FileName = saveNewFile.Substring(0, saveNewFile.Length - 4) + ".txt";
            saveFileDialog2.FileName = saveOldFile.Substring(0, saveOldFile.Length - 4) + ".txt";
            if (string.Compare(filetype, "New") == 0)
            {
                exportVersion = SortVersionRecordsByVersionNumber(versionRecNew);//SortVersionRecordsByDate(versionRecNew);
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                        dialog1 = true;
            }
            else
            {
                exportVersion = SortVersionRecordsByVersionNumber(versionRecOld);
                if (saveFileDialog2.ShowDialog() == DialogResult.OK)
                    if ((myStream = saveFileDialog2.OpenFile()) != null)
                        dialog2 = true;
            }
            if ((dialog1 == true) || (dialog2 == true))
            {
                WriteString(myStream, "Installation Name: ");
                if (string.Compare(filetype, "New") == 0) WriteString(myStream, installationNameNew.ToString());
                if (string.Compare(filetype, "Old") == 0) WriteString(myStream, installationNameOld.ToString());
                WriteString(myStream, CRLF);
                if (string.Compare(filetype, "New") == 0) WriteString(myStream, "Filename/Path: " + NewFileName.Text.ToString());
                if (string.Compare(filetype, "Old") == 0) WriteString(myStream, "Filename/Path: " + OldFileName.Text.ToString());
                WriteString(myStream, CRLF);
                if (exportVersion.Count > 0)
                {
                    VersionRecord versionPtr = (VersionRecord)exportVersion[0];
                    WriteString(myStream, versionPtr.typeOfVersion.ToString() + ": ");
                    WriteString(myStream, versionPtr.majorVer.ToString() + ".");
                    if (versionPtr.minorVer < 100) WriteString(myStream, "0");
                    if (versionPtr.minorVer < 10) WriteString(myStream, "0");
                    WriteString(myStream, versionPtr.minorVer.ToString() + ", ");
                    WriteString(myStream, versionPtr.name_Person.ToString());
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "Date/Time of last version record: " + versionPtr.date_time.ToString());
                    WriteString(myStream, CRLF);
                }
                WriteString(myStream, "Date/Time of Export: " + DateTime.Now.ToString());
                WriteString(myStream, CRLF);
                if (string.Compare(filetype, "New") == 0) WriteString(myStream, "GCSS Version: " + GCSSVersionNew);
                if (string.Compare(filetype, "Old") == 0) WriteString(myStream, "GCSS Version: " + GCSSVersionOld);
                WriteString(myStream, CRLF);
                WriteString(myStream, CRLF);

                for (int i = 0; i < interlockingSave.Count; i++)
                {
                    ArrayList rungNewPointer = (ArrayList)interlockingSave[i];
                    int length = GetRungLength(rungNewPointer);
                    int height = GetRungHeight(rungNewPointer);


                    WriteString(myStream, "Rung: ");
                    WriteString(myStream, rungNewPointer[rungNewPointer.Count - 1].ToString());
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "|      ");
                    for (int j = 0; j < length - 1; j++)
                        WriteString(myStream, "       ");
                    WriteString(myStream, CRLF);
                    for (int x = 1; x < 1 + height; x++)
                    {
                        for (int scanline = 0; scanline < 6; scanline++)
                        {
                            for (int y = 1; y < 1 + length; y++)
                            {
                                found = false;
                                //timersNew
                                for (int j = 1; j < rungNewPointer.Count - 1; j++)	// New Rungs
                                {
                                    Contact contactPointer = (Contact)rungNewPointer[j];
                                    string contactName = contactPointer.name.ToString();
                                    string contactNamestring = contactName;
                                    if ((contactPointer.x == x) && (contactPointer.y == y))
                                    {
                                        found = true;
                                        int len = contactName.Length;
                                        for (int sp = 0; sp < 16 - len; sp++)
                                            contactName = " " + contactName;
                                        if (scanline == 4)
                                        {
                                            if ((contactPointer.topLink == false) && (contactPointer.bottomLink == false))
                                                WriteString(myStream, "-");
                                            else
                                            {
                                                if ((contactPointer.topLink == true) && (contactPointer.bottomLink == true) && (string.Compare(contactPointer.typeOfCell, "Empty") == 0))
                                                    WriteString(myStream, "|");
                                                else WriteString(myStream, "+");
                                            }
                                            if (string.Compare(contactPointer.typeOfCell, "Coil") == 0)
                                            {
                                                WriteString(myStream, "-( ) ");
                                                for (int k = 0; k < timersSave.Count; k++)
                                                {
                                                    ML2Timer timer = (ML2Timer)timersSave[k];
                                                    if (timer.timerName == contactNamestring)
                                                        WriteString(myStream, " Slow to Pick: " + timer.setTime + ", Slow to Drop: " + timer.clearTime);
                                                }
                                            }
                                            if (string.Compare(contactPointer.typeOfCell, "Contact") == 0)
                                            {
                                                if (contactPointer.NormallyClosed == true)
                                                    WriteString(myStream, "-]/[--");
                                                else WriteString(myStream, "-] [--");
                                            }
                                            if (string.Compare(contactPointer.typeOfCell, "Horizontal Shunt") == 0) WriteString(myStream, "------");
                                            if (string.Compare(contactPointer.typeOfCell, "Empty") == 0) WriteString(myStream, "      ");
                                            if (string.Compare(contactPointer.typeOfCell, "End Contact") == 0) WriteString(myStream, "      ");

                                        }
                                        if (scanline == 5)
                                        {
                                            if ((contactPointer.bottomLink == true) && (x != height))
                                                WriteString(myStream, "|      ");
                                            else WriteString(myStream, "       ");
                                        }
                                        if (scanline < 4)
                                        {
                                            if (contactPointer.topLink == true) WriteString(myStream, "| ");
                                            else WriteString(myStream, "  ");
                                            WriteString(myStream, contactName.Substring(scanline * 4, 4));
                                            WriteString(myStream, " ");
                                        }
                                    }
                                }
                                if (found == false) WriteString(myStream, "       ");
                            }
                            WriteString(myStream, CRLF);
                        }
                    }
                }
                myStream.Close();
            }
        }

        protected void SaveLogicStateFile(string filename)
        {
            Stream myStream = null;
            string CRLF = "\r\n";

            bool dialog = false;
            if (filename == "")
            {
                string saveNewFile = NewFileName.Text.ToString();
                if (saveNewFile.Length < 5) saveNewFile = "Logic State.st8";
                saveSt8FileDialog.FileName = saveNewFile.Substring(0, saveNewFile.Length - 4) + ".st8";
                if (saveSt8FileDialog.ShowDialog() == DialogResult.OK)
                    if ((myStream = saveSt8FileDialog.OpenFile()) != null)
                    {
                        dialog = true;
                        CurrentState.Text = saveSt8FileDialog.FileName;
                    }
            }
            else
            {
                myStream = File.OpenWrite(filename);
                dialog = true;
            }
            if (dialog == true)
            {
                WriteString(myStream, "Input States:");
                WriteString(myStream, CRLF);
                WriteString(myStream, "~~~~~~~~~~~~~");
                WriteString(myStream, CRLF);
                for (int i = 0; i < SimInputs.Count; i++)
                {
                    WriteString(myStream, SimInputs[i].ToString());
                    WriteString(myStream, CRLF);
                }
                WriteString(myStream, CRLF); WriteString(myStream, CRLF);

                WriteString(myStream, "Rung States:");
                WriteString(myStream, CRLF);
                WriteString(myStream, "~~~~~~~~~~~~~");
                WriteString(myStream, CRLF);
                for (int i = 0; i < HighRungs.Count; i++)
                {
                    WriteString(myStream, HighRungs[i].ToString());
                    WriteString(myStream, CRLF);
                }
                WriteString(myStream, "~~END of ST8 File~~~~~~~~~");
                myStream.Close();
            }
        }

        private void OpenLogicStateFile()
        {
            if (openst8FileDialog.ShowDialog() == DialogResult.OK)
            {
                SimInputs.Clear();
                HighRungs.Clear();
                Changes.Clear();
                Openst8File(openst8FileDialog.FileName);
                CurrentState.Text = openst8FileDialog.FileName;
            }
        }

        public ArrayList SortVersionRecordsByDate(ArrayList versRec)
        {
            ArrayList outVersRec = new ArrayList();
            ArrayList sortedlist = new ArrayList();
            for (int i = 0; i < versRec.Count; i++)
            {
                VersionRecord VPtr = (VersionRecord)versRec[i];
                DateTime dateAndTime = VPtr.date_time;
                sortedlist.Add(dateAndTime);
            }
            sortedlist.Sort();
            for (int i = 0; i < versRec.Count; i++)
                for (int j = 0; j < versRec.Count; j++)
                {
                    VersionRecord VPtr = (VersionRecord)versRec[j];
                    if (DateTime.Compare((DateTime)sortedlist[versRec.Count - i - 1], VPtr.date_time) == 0)
                        outVersRec.Add(VPtr);
                }
            return outVersRec;
        }


        public ArrayList SortVersionRecordsByVersionNumber(ArrayList versRec)
        {
            ArrayList outVersRec = new ArrayList();
            ArrayList sortedlist = new ArrayList();
            for (int i = 0; i < versRec.Count; i++)
            {
                VersionRecord VPtr = (VersionRecord)versRec[i];
                int versionnumber = VPtr.majorVer * 10000 + VPtr.minorVer;
                sortedlist.Add(versionnumber);
            }
            sortedlist.Sort();
            for (int i = 0; i < versRec.Count; i++)
                for (int j = 0; j < versRec.Count; j++)
                {
                    VersionRecord VPtr = (VersionRecord)versRec[j];
                    if ((int)sortedlist[versRec.Count - i - 1] == VPtr.majorVer * 10000 + VPtr.minorVer)
                        outVersRec.Add(VPtr);
                }
            return outVersRec;
        }

        private void WriteString(Stream file, string characters)
        {
            for (int i = 0; i < characters.Length; i++)
                file.WriteByte((byte)characters[i]);
        }

        private int GetRungLength(ArrayList rungNewPointer)
        {
            int maxlength = 0;
            for (int i = 1; i < rungNewPointer.Count - 1; i++)
            {
                Contact contactPointer = (Contact)rungNewPointer[i];
                if (maxlength < contactPointer.y) maxlength = contactPointer.y;
            }
            return maxlength;
        }

        private int GetRungHeight(ArrayList rungNewPointer)
        {
            int maxheight = 0;
            for (int i = 1; i < rungNewPointer.Count - 1; i++)
            {
                Contact contactPointer = (Contact)rungNewPointer[i];
                if (maxheight < contactPointer.x) maxheight = contactPointer.x;
            }
            return maxheight;
        }

        private void menuItem11_Click(object sender, System.EventArgs e)
        {
            Close_All_Rungs();
            ArrayList temp;

            temp = interlockingNew;
            interlockingNew = interlockingOld;
            interlockingOld = temp;

            temp = Housings_New;
            Housings_New = Housings_Old;
            Housings_Old = temp;

            temp = versionRecNew;
            versionRecNew = versionRecOld;
            versionRecOld = temp;

            string tempString;
            tempString = installationNameNew;
            installationNameNew = installationNameOld;
            installationNameOld = tempString;

            tempString = GCSSVersionNew;
            GCSSVersionNew = GCSSVersionOld;
            GCSSVersionOld = tempString;

            reloading = true;
            treeView.Nodes.Clear();
            reloading = false;

            this.Text = "(" + OldFileName.Text + ")" + " vs (" + NewFileName.Text + ") - Logic Navigator";
            NewFileText.Text = NewFileName.Text;
            OldFileText.Text = OldFileName.Text;
            statusBar1.Text = "(" + OldFileName.Text + ")" + " vs (" + NewFileName.Text + ")";
            Close_All_Rungs();
            if ((interlockingOld.Count != 0) && (interlockingNew.Count != 0))
            {
                process_interlockings();
                DoDatabase();
                ShowMenu(false);
                ShowRungPane();
            }
        }

        private void menuItem21_Click(object sender, System.EventArgs e)
        {
            LatchWindow();
        }

        private void LatchWindow()
        {
            frmMChild_Latches objfrmMChild = new frmMChild_Latches(Housings_Old, Housings_New, interlockingOld, interlockingNew, timersOld, timersNew, drawFont);
            objfrmMChild.Text = "Latches";
            objfrmMChild.MdiParent = this;
            objfrmMChild.Show();
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SearchWindow();
        }

        private void menuItem22_Click(object sender, EventArgs e)
        {
            string sColumns = "";
            //int width = RungGrid.VisibleColumnCount;
            int width = ((DataTable)this.RungGrid.DataSource).Columns.Count;
            //int height = RungGrid.VisibleRowCount;
            int height = ((DataTable)this.RungGrid.DataSource).Rows.Count;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width - 1; j++)
                {

                    if ((j == 3) && (RungGrid[i, j].ToString() == ""))
                        sColumns += RungGrid[i, j].ToString() + "No change, ";
                    else sColumns += RungGrid[i, j].ToString() + ", ";

                }
                sColumns += "\r\n";
            }
            Clipboard.SetDataObject(sColumns.ToString(), true);
        }

        private void menu_Chess_Click(object sender, EventArgs e)
        {
            frmMChild_Chess objfrmMChild = new frmMChild_Chess(scaleFactor, 80);
            //objfrmMChild.Size = new Size(700, 700);            
            //objfrmMChild.Location = new System.Drawing.Point(1, 1);
            objfrmMChild.Text = rungName;
            objfrmMChild.MdiParent = this;
            objfrmMChild.Show();
        }

        private void menuItem24_Click(object sender, EventArgs e)
        {  //Show Grid lines
            gridlines = true;
        }

        private void menuItem25_Click(object sender, EventArgs e)
        {  //Hide Grid lines
            gridlines = false;
        }

        private void menuItem27_Click(object sender, EventArgs e)
        {   //Show timers
            showTimers = true;
        }

        private void menuItem28_Click(object sender, EventArgs e)
        {   //Hide timers
            showTimers = false;
        }

        private void menuItem30_Click(object sender, EventArgs e)
        {
            showEvaluationSequence = true;
        }

        private void menuItem31_Click(object sender, EventArgs e)
        {
            showEvaluationSequence = false;
        }

        //---------------------------------------- Simulation area ---------------------

        private void menuItem32_Click(object sender, EventArgs e)
        {
            //UpdateRungsInSimMode();
        }

        private void menuItem34_Click(object sender, EventArgs e)
        {
            frmMChild_SimInputs objfrmMChild = new frmMChild_SimInputs(interlockingOld, interlockingNew, timersOld, timersNew, drawFont, HighColor, LowColor);
            objfrmMChild.Text = "Inputs";
            objfrmMChild.MdiParent = this;
            objfrmMChild.Show();
        }

        private void menuItem36_Click(object sender, EventArgs e)
        {
            StartSimulation();
        }

        private void StartSimulation()
        {
            //calcmethod = RELAYCALC;
            Simulationtimer.Enabled = true;
            PopulateContactAnalysis();
            PrepareSimulationRungs();
            PrepareCoilPositions();
            PrepareDependencyListCoils();
            PrepareDependencyListInputs();
            PrepareTimerAnalysis();
            // if (!backgroundWorker1.IsBusy)
            //   backgroundWorker1.RunWorkerAsync();
            menuItem35.Text = "Simulator - Running";
            SimMode = true;
            SetSimModeinWindows();
        }

        private void menuItem37_Click(object sender, EventArgs e)
        {
            Simulationtimer.Enabled = false;
            menuItem35.Text = "Simulator";
            SimMode = false;
            SetSimModeinWindows();
        }

        private void PrepareCoilPositions()
        {
            CoilContactrefs.Clear();
            for (int j = 0; j < sim.Count; j++)	// New Rung
            {
                List<Contact> rung = (List<Contact>)sim[j];
                CoilContactrefs.Add(FindCoilList(rung));
            }
        }

        private void EvaluateAll()
        {
            for (int t = 0; t < siminterlocking.Count; t++)
                evaluationlist[t] = true;
        }

        private void PrepareDependencyListCoils()
        {
            depbookCoils.Clear();
            evaluationlist.Clear();
            for (int i = 0; i < siminterlocking.Count; i++)
            {
                ArrayList rung = (ArrayList)siminterlocking[i];
                List<int> coillinks = new List<int>();
                for (int k = 0; k < siminterlocking.Count; k++)
                {
                    ArrayList rungr = (ArrayList)siminterlocking[k];
                    for (int l = 1; l < rungr.Count - 1; l++) 
                    {
                        Contact cont = (Contact)rungr[l];
                        if (cont.name == (string)rung[rung.Count - 1])
                            if(cont.typeOfCell != "Coil")   
                                if(coillinks.IndexOf(k) == -1)
                                    coillinks.Add(k);
                    }
                }
                depbookCoils.Add(coillinks);
                evaluationlist.Add(true);
            }
        }

        private void PrepareDependencyListInputs()
        {
            depbookInputs.Clear();
            List<string> coils = new List<string>();
            List<string> inputs = new List<string>();

            for (int i = 0; i < siminterlocking.Count; i++)
            {
                ArrayList rung = (ArrayList)siminterlocking[i];
                coils.Add((string)rung[rung.Count - 1]);
            }

            for (int k = 0; k < siminterlocking.Count; k++)
            {
                ArrayList rungr = (ArrayList)siminterlocking[k];
                for (int l = 1; l < rungr.Count - 1; l++) 
                {
                    Contact cont = (Contact)rungr[l];
                    bool incoillist = false; bool ininputlist = false;
                    for (int c = 0; c < coils.Count; c++)
                        if (cont.name == coils[c])                           
                             incoillist = true;
                    for (int c = 0; c < inputs.Count; c++)
                        if (cont.name == inputs[c])
                            ininputlist = true;
                    if (!incoillist && !ininputlist)
                        if(cont.name != "")
                            inputs.Add(cont.name);
                }
            }

            for (int i = 0; i < inputs.Count; i++)
            {                
                List<int> coillinks = new List<int>();
                for (int k = 0; k < siminterlocking.Count; k++)
                {
                    ArrayList rungr = (ArrayList)siminterlocking[k];
                    for (int l = 1; l < rungr.Count - 1; l++)
                    {
                        Contact cont = (Contact)rungr[l];
                        if (cont.name == inputs[i])
                            if (cont.typeOfCell != "Coil")
                                if (coillinks.IndexOf(k) == -1)
                                    coillinks.Add(k);
                    }
                }
                depbookInputs.Add(coillinks);
                //depbookInputNames.Add(inputs[i]);
                //inputstates.Add(false);
            }

        }

        private void PrepareSimulationRungs()
        {
            PrepareCoils(interlockingNew);
            PrepareInputStateList();
            sim.Clear();
            siminterlocking.Clear();
            for (int i = 0; i < interlockingNew.Count; i++)
            {
                ArrayList rung = (ArrayList)interlockingNew[i];
                ArrayList newrung = new ArrayList();
                List<Contact> newrunglist = new List<Contact>();

                newrung.Add(rung[0]);
                for (int j = 1; j < rung.Count - 1; j++)	// New Rung
                {
                    Contact contact = (Contact)rung[j];
                    Contact newcontact = new Contact();

                    newcontact.name = contact.name;
                    newcontact.x = contact.x;
                    newcontact.y = contact.y;
                    newcontact.typeOfCell = contact.typeOfCell;
                    newcontact.NormallyClosed = contact.NormallyClosed;
                    newcontact.topLink = contact.topLink;
                    newcontact.bottomLink = contact.bottomLink;
                    newcontact.leftLink = contact.leftLink;
                    newcontact.live = contact.live;
                    newcontact.rungindex = getCoilIndex(contact.name);
                    if (contact.y > 1) //If the contact is in the first column it does not matter if it has left, bottom or top links.
                    {
                        newcontact.rightLinkindex = FindArrayIndex(rung, contact.x, contact.y + 1) - 1;
                        if (contact.leftLink) newcontact.leftLinkindex = FindArrayIndex(rung, contact.x, contact.y - 1) - 1;
                        if (contact.bottomLink) newcontact.bottomLinkindex = FindArrayIndex(rung, contact.x + 1, contact.y) - 1;
                        if (contact.topLink) newcontact.topLinkindex = FindArrayIndex(rung, contact.x - 1, contact.y) - 1;
                    }
                    newcontact.inputindex = getInputIndex(contact.name);
                    newrunglist.Add(newcontact);
                    newrung.Add(newcontact);
                }
                sim.Add(newrunglist);
                newrung.Add(rung[rung.Count - 1]);
                siminterlocking.Add(newrung);
            }
        }

        private void addprefixtorungs(string prefix)
        {
            int lastrung = interlockingNew.Count;
            int size = interlockingNew.Count;

            for (int i = 0; i < interlockingNew.Count; i++)
            {
                ArrayList rung = (ArrayList)interlockingNew[i];
                ArrayList rungp = new ArrayList();
                rungp.Add((int)rung[0] + size);
                for (int c = 1; c < rung.Count - 1; c++)
                {
                    Contact talr = (Contact)rung[c];
                    Contact ncont = new Contact();
                    ncont.name = prefix + talr.name;
                    ncont.rungindex = talr.rungindex;
                    ncont.inputindex = talr.inputindex;
                    ncont.x = talr.x;
                    ncont.y = talr.y;
                    ncont.typeOfCell = talr.typeOfCell;
                    ncont.NormallyClosed = talr.NormallyClosed;
                    ncont.topLink = talr.topLink;
                    ncont.topLinkindex = talr.topLinkindex;
                    ncont.bottomLink = talr.bottomLink;
                    ncont.bottomLinkindex = talr.bottomLinkindex;
                    ncont.leftLink = talr.leftLink;
                    ncont.leftLinkindex = talr.leftLinkindex;
                    ncont.rightLinkindex = talr.rightLinkindex;
                    ncont.live = talr.live;
                    rungp.Add(ncont);
                }
                rungp.Add(prefix + rung[rung.Count - 1]);
                interlockingNew.Add(rungp);
                interlockingOld.Add(rungp);
            }
        }

        private void ConcatenateTAL(string prefix)
        {
            int lastrung = interlockingNew.Count;
            int size = interlockingNew.Count;
            for (int i = 0; i < interlockingTAL.Count; i++)
            {
                ArrayList rung = (ArrayList)interlockingTAL[i];
                ArrayList rungp = new ArrayList();
                rungp.Add((int)rung[0] + size);
                for (int c = 1; c < rung.Count - 1; c++)
                {
                    Contact talr = (Contact)rung[c];
                    Contact ncont = new Contact();
                    ncont.name = prefix + talr.name;
                    ncont.rungindex = talr.rungindex;
                    ncont.inputindex = talr.inputindex;
                    ncont.x = talr.x;
                    ncont.y = talr.y;
                    ncont.typeOfCell = talr.typeOfCell;
                    ncont.NormallyClosed = talr.NormallyClosed;
                    ncont.topLink = talr.topLink;
                    ncont.topLinkindex = talr.topLinkindex;
                    ncont.bottomLink = talr.bottomLink;
                    ncont.bottomLinkindex = talr.bottomLinkindex;
                    ncont.leftLink = talr.leftLink;
                    ncont.leftLinkindex = talr.leftLinkindex;
                    ncont.rightLinkindex = talr.rightLinkindex;
                    ncont.live = talr.live;
                    rungp.Add(ncont);
                }
                rungp.Add(prefix + rung[rung.Count - 1]);
                interlockingNew.Add(rungp);
                interlockingOld.Add(rungp);
            }
        }


        private void ConcatenateTALtimers(string prefix)
        {
            for (int i = 0; i < timersTAL.Count; i++)
            {
                ML2Timer timerdetail = (ML2Timer)timersTAL[i];
                timerdetail.timerName = prefix + timerdetail.timerName;
                timersNew.Add(timerdetail);
                timersOld.Add(timerdetail);
            }
        }


        private void PrepareTimerAnalysis()
        {
            CoilIsTimer.Clear();
            for (int j = 0; j < sim.Count; j++)	// Go through each rung and see if it is a timer, then put it into CoilIsTimer list
            {
                List<Contact> rung = (List<Contact>)sim[j];
                CoilIsTimer.Add(findS2DTimer(timersNew, Coilnames[j]));
            }
        }

        private void PrepareInputStateList()
        {
            Inputnames.Clear();
            Inputstates.Clear();
            for (int r = 0; r < interlockingNew.Count; r++)
            {
                ArrayList rungPointer = (ArrayList)interlockingNew[r];
                for (int k = 1; k < rungPointer.Count - 1; k++)
                {
                    Contact contact = (Contact)rungPointer[k];
                    if (!inListString(contact.name, Inputnames) && !inListString(contact.name, Coilnames))
                        if (contact.name != "")
                        {
                            Inputnames.Add(contact.name);
                            Inputstates.Add(isInputHigh(contact.name));
                            InputstatesPrev.Add(false);
                        }
                }
            }
        }

        private bool isInputHigh(string name)
        {
            for (int r = 0; r < SimInputs.Count; r++)
                if (name == (string)SimInputs[r])
                    return true;
            return false;
        }

        private int getCoilIndex(string name)
        {
            for (int i = 0; i < Coilnames.Count; i++)
            {
                if (Coilnames[i] == name)
                    return (i);
            }
            return (-1);
        }

        private int getInputIndex(string name)
        {
            for (int i = 0; i < Inputnames.Count; i++)
            {
                if (Inputnames[i] == name)
                    return (i);
            }
            return (-1);
        }

        private void PrepareCoils(ArrayList interlockingNewPointer)
        {
            Coilnames.Clear();
            Coilstates.Clear();
            Coildrive.Clear();
            Coilnodrive.Clear();
            for (int i = 0; i < interlockingNewPointer.Count; i++)
            {
                ArrayList rungPointer = (ArrayList)interlockingNewPointer[i];
                Coilnames.Add((string)rungPointer[rungPointer.Count - 1]);
                Coilstates.Add(false);
                Coildrive.Add(false);
                Coilnodrive.Add(false);
            }
        }

        private void PopulateContactAnalysis()
        {
            contactAnalysis.Clear();
            statusBar1.Text = "Preparing Contact Analysis";
            for (int r = 0; r < interlockingNew.Count - 1; r++) //Add rungs first 
            {
                ArrayList rungPointer = (ArrayList)interlockingNew[r];
                ContactAnalysis ContactSummary = new ContactAnalysis();
                ContactSummary.name = rungPointer[rungPointer.Count - 1].ToString();
                ContactSummary.index = r;
                ArrayList usedinlist = new ArrayList();
                ContactSummary.usedin = usedinlist;
                contactAnalysis.Add(ContactSummary);
            }
            for (int r = 0; r < interlockingNew.Count - 1; r++) //Go through each rung
            {
                ArrayList rungPointer = (ArrayList)interlockingNew[r];
                statusBar1.Text = "Preparing Contact Analysis: " + rungPointer[rungPointer.Count - 1].ToString();
                for (int k = 1; k < rungPointer.Count - 1 - 1; k++) //Go through each contact in the rung, Don't bother with coils
                {
                    Contact contact = (Contact)rungPointer[k];
                    AddtoContactAnalysis(contact.name, r); //Add the rung number to the list
                }
            }
            statusBar1.Text = "";
        }

        private void AddtoContactAnalysis(string contactname, int r)
        {
            int index = findInContactAnalysisList(contactname);
            if (index == -1)
                AddNewContactAnalysis(contactname, r);
            else
                AppendContactAnalysis(contactname, r, index);
        }

        private void AddNewContactAnalysis(string contactname, int r)
        {
            ContactAnalysis ContactSummary = new ContactAnalysis();
            ContactSummary.name = contactname;
            ContactSummary.index = findRung(interlockingNew, contactname);
            ArrayList usedinlist = new ArrayList();
            usedinlist.Add(r);
            ContactSummary.usedin = usedinlist;
            contactAnalysis.Add(ContactSummary);
        }

        private void AppendContactAnalysis(string contactname, int r, int index)
        {
            ContactAnalysis ContactSummary = (ContactAnalysis)contactAnalysis[index];
            ArrayList list = ContactSummary.usedin;
            list.Add(r);
        }

        private int findInContactAnalysisList(string contactname)
        {
            for (int r = 0; r < contactAnalysis.Count; r++)
            {
                ContactAnalysis ContactSummary = (ContactAnalysis)contactAnalysis[r];
                if (ContactSummary.name == contactname) return r;
            }
            return -1;
        }

        public void doserial(string toggle)
        {
            int switchnumber = Int32.Parse(toggle.Substring(14)) - 1;
            if (controlswitch[switchnumber]) controlswitch[switchnumber] = false;
            else controlswitch[switchnumber] = true;
            SendSerialMessage();
        }

        private void Simulationtimer_Tick(object sender, EventArgs e)
        {
            ticker += 125;
            if (ticker % cycletimespeed == 0)
            {
                if (ticker == 1000) ticker = 0;
                try
                {
                    //if (ticker % 3 == 0)
                    {
                        BeginScan = DateTime.Now;
                        getPreviousScan();
                        ScanInputs();
                        findChangesinInputs();
                        EndScan = DateTime.Now;
                    }
                    ScanSimSpeed();
                    if (windowsopen != this.MdiChildren.Length)
                    {
                        windowsopen = this.MdiChildren.Length;
                        SetSimModeinWindows();
                    }
                    if (!freezeRungStates)
                    {
                        PreviousEvaluation = EndEvaluation;
                        BeginEvaluation = DateTime.Now;
                        //wipevoltages();
                        EvaluateRungs();
                        EndEvaluation = DateTime.Now;
                    }
                    try
                    {
                        BeginBroadcast = DateTime.Now;
                        BroadcastRungStates();
                        EndBroadcast = DateTime.Now;
                    }
                    catch { MessageBox.Show("Error Broadcasting Rung states", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    TimeSpan waittime = BeginEvaluation - PreviousEvaluation;
                    TimeSpan evaltime = EndEvaluation - BeginEvaluation;
                    TimeSpan scantime = EndScan - BeginScan;
                    TimeSpan broadcasttime = EndBroadcast - BeginBroadcast;

                    if ((forceHigh.Count > 0) || (forceLow.Count > 0))
                    {
                        statusStrip1.Visible = true;
                        toolStripStatusLabel1.Text = "REMINDER: False feeds/pin pulls are currently applied, coils forced high [";
                        for (int m = 0; m < forceHigh.Count; m++)
                        {
                            toolStripStatusLabel1.Text += forceHigh[m] + ", ";
                            if (m < forceLow.Count - 1)
                                toolStripStatusLabel1.Text += ", ";
                        }
                        toolStripStatusLabel1.Text += "], coils forced low [";
                        for (int m = 0; m < forceLow.Count; m++)
                        {
                            toolStripStatusLabel1.Text += forceLow[m];
                            if (m < forceLow.Count - 1)
                                toolStripStatusLabel1.Text += ", ";
                        }
                        toolStripStatusLabel1.Text += "], To remove false feed left click on the coil, To remove coil suppression left click on the coil, Click HERE to remove all false feeds/suppressions";
                        statusStrip1.BackColor = Color.Yellow;
                    }
                    else
                    {
                        statusStrip1.Visible = false;
                        toolStripStatusLabel1.Text = "";
                    }
                    statusBar1.Text = "Processing time: " + (evaltime.TotalMilliseconds + scantime.TotalMilliseconds + broadcasttime.TotalMilliseconds).ToString("000.000") + " msecs; "
                         + "(Evaluation time (evaluate rungs): " + (evaltime.TotalMilliseconds).ToString("000.000") + " msecs; "
                         + "Scan time (reading inputs): " + (scantime.TotalMilliseconds).ToString("000.000") + " msecs; "
                         + "Broadcast time (render rungs and map view): " + (broadcasttime.TotalMilliseconds).ToString("000.000") + " msecs)" 
                         + " Cycle Trigger time : " + (waittime.TotalMilliseconds).ToString("000.000") + " msecs; "                         
                         + "(Workload: " + workload + " rungs evaulated, rungs " + runglisting + ")";

                }
                catch { MessageBox.Show("Error with Simulation, " + debuginfo1, "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
        }

        private void DrawVoltageMatrix()
        {
            string voltagestring = "";
            VoltageMatrixText.Text = "";
            for (int i = 0; i < 64; i++)
            {
                for (int j = 0; j < 64; j++)
                {
                    if (voltagematrix[i, j])
                        voltagestring += "0";
                    else
                        voltagestring += "-";
                }
                voltagestring += "\r\n";
            }
            VoltageMatrixText.Text = voltagestring;


        }

        private void SetSimModeinWindows()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Name == "frmMChild")
                {
                    frmMChild objfrmMChild = (frmMChild)this.MdiChildren[i];
                    objfrmMChild.SimMode = SimMode;
                    objfrmMChild.InvalidateForm();
                }
            }
        }

        private void SendScrollinWindows(int ScrollCommand)
        {

            //frmMChild activeForm;
            //if (this.ActiveMdiChild 
            ////    .ActiveForm.Name == "frmMChild")
            //{
            //    activeForm = (frmMChild) Form.ActiveForm;
            //for (int i = 0; i < this.MdiChildren.Length; i++)
            //{
            if (this.MdiChildren.Length != 0)
                if (this.ActiveMdiChild.Name == "frmMChild")
                {
                    frmMChild objfrmMChild = (frmMChild)this.ActiveMdiChild;
                    objfrmMChild.ScrollCommand = ScrollCommand;
                    objfrmMChild.InvalidateForm();
                }
        }

        private void getPreviousScan()
        {
            for (int i = 0; i < Inputstates.Count; i++)
                InputstatesPrev[i] = Inputstates[i];            
        }

        private void ScanInputs()
        {
            string inputToggle = "";
            string togglelist = "";
            string togglechunk = "";
            bool forceup = false;
            bool forcedown = false;
            bool madeit = false;
            bool inlist = false;
            try
            {
                for (int i = 0; i < this.MdiChildren.Length; i++)
                    if (this.MdiChildren[i].Name == "frmMChild_SimInputs")
                    {
                        frmMChild_SimInputs objfrmMChild = (frmMChild_SimInputs)this.MdiChildren[i];
                        if (objfrmMChild.recentChange)
                        {
                            objfrmMChild.recentChange = false;
                            FindChanges(SimInputs, objfrmMChild.SimInputs);
                            SimInputs.Clear();
                            DuplicateList(objfrmMChild.SimInputs, SimInputs);//Grab Sim Inputs from SimInputs form

                        }
                    }

                for (int i = 0; i < this.MdiChildren.Length; i++)
                {
                    inputToggle = "";
                    if (this.MdiChildren[i].Name == "frmMChild")
                    {
                        frmMChild objfrmMChild = (frmMChild)this.MdiChildren[i];
                        inputToggle = objfrmMChild.inputToggle;
                        objfrmMChild.inputToggle = "";
                    }
                    if (this.MdiChildren[i].Name == "frmMChild_SimOutputs")
                    {
                        frmMChild_SimOutputs objfrmMChild = (frmMChild_SimOutputs)this.MdiChildren[i];
                        inputToggle = objfrmMChild.inputToggle;
                        objfrmMChild.inputToggle = "";
                    }
                    if (this.MdiChildren[i].Name == "frmMChild_SimMap")
                    {
                        frmMChild_SimMap objfrmMChild = (frmMChild_SimMap)this.MdiChildren[i];
                        inputToggle = objfrmMChild.inputToggle;
                        objfrmMChild.inputToggle = "";
                    }
                    if (inputToggle != "")
                    {
                        if (inputToggle.Length > 15)
                            if (inputToggle.Substring(0, 15) == "{FORCE HIGH} - ")
                            {
                                bool inforcelist = false;
                                string forceinput = inputToggle.Substring(15);
                                for (int lst = 0; lst < forceHigh.Count; lst++)
                                    if (forceinput == forceHigh[lst])
                                        inforcelist = true;
                                if (!inforcelist)
                                {
                                    forceHigh.Add(forceinput);
                                    inputToggle = ";";
                                }
                                else
                                {
                                    for (int lst = 0; lst < forceHigh.Count; lst++)
                                        if (forceinput == forceHigh[lst])
                                        {
                                            forceHigh.RemoveAt(lst);
                                            break;
                                        }
                                }
                                EvaluateAll();
                            }
                        if (inputToggle.Length > 14)
                            if (inputToggle.Substring(0, 14) == "{FORCE LOW} - ")
                            {
                                bool inforcelist = false;
                                string forceinput = inputToggle.Substring(14);
                                for (int lst = 0; lst < forceLow.Count; lst++)
                                    if (forceinput == forceLow[lst])
                                        inforcelist = true;
                                if (!inforcelist)
                                {
                                    forceLow.Add(forceinput);
                                    inputToggle = ";";
                                }
                                else
                                {
                                    for (int lst = 0; lst < forceLow.Count; lst++)
                                        if (forceinput == forceLow[lst])
                                        {
                                            forceLow.RemoveAt(lst);
                                            break;
                                        }
                                }
                                EvaluateAll();
                            }
                        if (inputToggle.Length > 9)
                            if (inputToggle.Substring(0, 9) == "{DOWN} - ")
                            {
                                forcedown = true;
                                inputToggle = inputToggle.Substring(9);
                            }
                        if (inputToggle.Length > 7)
                            if (inputToggle.Substring(0, 7) == "{UP} - ")
                            {
                                forceup = true;
                                inputToggle = inputToggle.Substring(7);
                            }
                        madeit = true;
                        if (inputToggle.LastIndexOf(";") == -1) inputToggle = inputToggle + ";";
                        togglelist = inputToggle.Substring(0, inputToggle.Length - 1);
                        while (togglelist != "")
                        {
                            if (togglelist.LastIndexOf(";") != -1)
                                togglechunk = togglelist.Substring(togglelist.LastIndexOf(";") + 1);
                            else togglechunk = togglelist;
                            if (InSimInputs(togglechunk))
                            {
                                inlist = inList(togglechunk, SimInputs);
                                if (!forcedown && !forceup)
                                {
                                    if (inlist)
                                        RemoveRungfromList(togglechunk, SimInputs);
                                    else
                                        SimInputs.Add(togglechunk);
                                }
                                if (forcedown && inlist)
                                    RemoveRungfromList(togglechunk, SimInputs);
                                if (forceup && inlist)
                                    SimInputs.Add(togglechunk);
                                freezeRungStates = true;
                                transferSimInputstoSimInputsForm();
                                freezeRungStates = false;
                            }
                            if (togglelist.LastIndexOf(";") != -1)
                                togglelist = togglelist.Substring(0, togglelist.LastIndexOf(";"));
                            else togglelist = "";
                        }
                    }

                }

                scanSimInputs();// Copy SimInputs from SimInputs Form to Array
            }
            catch
            {
                MessageBox.Show("Error Scanning inputs for simulation, " + inputToggle + ", " + madeit.ToString() + ", "
            + forceup.ToString() + ", " + forcedown.ToString() + ", " + inlist.ToString(), "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void findChangesinInputs()
        {
            for(int i = 0; i < Inputstates.Count; i++)            
                if(Inputstates[i] != InputstatesPrev[i])                
                    for (int j = 0; j < depbookInputs[i].Count; j++)
                        evaluationlist[depbookInputs[i][j]] = true;     
        }

        private void findChangesinCoils(int coil)
        {
            for (int j = 0; j < depbookCoils[coil].Count; j++)
                evaluationlist[depbookCoils[coil][j]] = true;            
        }

        private void ScanSimSpeed()
        {
            try
            {
                for (int i = 0; i < this.MdiChildren.Length; i++)
                {

                    if (this.MdiChildren[i].Name == "frmMChild_SimMap")
                    {
                        frmMChild_SimMap objfrmMChild = (frmMChild_SimMap)this.MdiChildren[i];
                        string command = objfrmMChild.simspeedcommand;
                        // if (command == "NormalSpeed") simspeed = 1;
                        if (command.IndexOf("Speed:") != -1)
                            simspeed = float.Parse(command.Substring(6));
                        //if (command == "Faster") simspeed = simspeed * 2;
                        // if (command == "Slower") simspeed = simspeed / 2;
                        // if (command == "WarpSpeed") simspeed = 10000;
                        if (command == "SaveLogicState")
                            SaveLogicStateFile(CurrentState.Text);
                        if (command == "LoadLogicState")
                            LoadLogicState();
                        if (command.IndexOf("SaveMap") != -1)
                            SaveMap(command.Substring(7), false);
                        menuItem44.Text = "Simulation Speed (x" + simspeed.ToString() + ")";
                        objfrmMChild.simspeedcommand = "";
                        //objfrmMChild.SimSpeed.Text = simspeed.ToString();
                    }

                }

                //scanSimInputs();// Copy SimInputs from SimInputs Form to Array
            }
            catch { MessageBox.Show("Error Scanning inputs for simulation", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private bool InSimInputs(string name)
        {
            for (int i = 0; i < Inputnames.Count; i++)
                if (Inputnames[i] == name)
                    return true;
            return false;
        }

        private void scanSimInputs()
        {
            for (int i = 0; i < Inputnames.Count; i++)
                if (inList(Inputnames[i], SimInputs))
                    Inputstates[i] = true;
                else Inputstates[i] = false;
        }

        private bool InExclusionList(string member)
        {
            for (int i = 0; i < ExclusionList.Count; i++)
                if (ExclusionList[i] == member) return true;
            return false;
        }

        private void wipevoltages()
        {
            for (int i = 0; i < 64; i++)
                for (int j = 0; j < 64; j++)
                    voltagematrix[i, j] = false;
        }

        private void EvaluateRungs()
        {
            string rungtitle = "";
            int timerremoveindex;
            //HighRungs.Clear();
            //for (int r = 0; r < interlockingNew.Count; r++)
            globalcounter = 0; searchdepth = 0; rungsevaluated = 0;
            bool evalnexttime = false;
            runglisting = "";
            workload = 0;            
            for (int r = 0; r < sim.Count; r++)
            {
                rungtitle = Coilnames[r];
                globalcounter++;
                evalnexttime = false;
                //if (Coildrive[r] && !Coilstates[r])
                    //runglisting += "^" + rungtitle;
                if (evaluationlist[r] || (Coildrive[r] && !Coilstates[r]) || (!Coildrive[r] && Coilstates[r])) 
                //On the evaluation list because input has changed, or slow to pick timer not timed yet, or slow to drop timer not dropped yet
                {
                    List<Contact> rung = (List<Contact>)sim[r];
                    wipevoltages();
                    workload++;
                    runglisting += ", " + rungtitle;
                    if ((EvaluateRungList(rung, r) || inforcehighlist(rung, r)) && !inforcelowlist(rung, r))
                    { // Add to the list of High Rungs
                        try
                        {
                            Coildrive[r] = true;//There is voltage on the coil, and it's a timer
                            //Coilnodrive[r] = false;########
                            rungsevaluated++;
                            if (!inList(rungtitle, HighRungs))
                            {
                                int timerindex = findS2PTimer(timersNew, rungtitle);
                                if (timerindex != -1) // Slow to pick timer
                                {
                                    //evalnexttime = true;
                                    //Coildrive[r] = true;//There is voltage on the coil, and it's a timer#####
                                    int timerstartindex = inTimerList(rungtitle, S2PTimersTiming);
                                    if (timerstartindex == -1) // Start timing
                                    {
                                        TimersTimingStruct timing = new TimersTimingStruct();
                                        timing.timername = rungtitle;
                                        timing.timerstarttime = DateTime.Now;
                                        timing.timeElapsed = 0;
                                        timing.totaltime = (int)(((float)Gettime(timerindex, "Set")) / 1000);
                                        if (timing.totaltime == 0)
                                        {
                                            if (!Coilstates[r])
                                                findChangesinCoils(r); //If there is a change of state, then work out what other rungs are affected
                                            Coilstates[r] = true;//Coil has picked (straight away because zero timer)
                                            evalnexttime = true; //just in case the timer is contact in its own rung
                                        }
                                        S2PTimersTiming.Add(timing);
                                        if (!InExclusionList(Coilnames[r]))
                                            if (sound)
                                                PlayTimerStartUpSound();
                                    }
                                    else // In the timer list already, see if timed out yet
                                    {
                                        TimersTimingStruct timerElement = (TimersTimingStruct)S2PTimersTiming[timerstartindex];
                                        TimeSpan difference = DateTime.Now - (DateTime)timerElement.timerstarttime;
                                        int diffmsec = (int)(((float)(difference.TotalMilliseconds)) * simspeed);
                                        timerElement.timeElapsed = (int)(((float)diffmsec) / 1000);
                                        UpdateS2PTimersTiming(timerElement);
                                        if (diffmsec > Gettime(timerindex, "Set")) //timer completed
                                        {
                                            if (Coilstates[r] != true)
                                                if (!InExclusionList(Coilnames[r]))
                                                    LogEvent(Coilnames[r].ToString(), "High", DateTime.Now.ToLongTimeString());
                                            if (sound)
                                            {
                                                SoundQueue.Add(Coilnames[r].ToString() + "High");
                                                //playstuff = true;
                                            }
                                            if (!Coilstates[r])
                                                findChangesinCoils(r); //If there is a change of state, then work out what other rungs are affected
                                            Coilstates[r] = true;//Coil has picked because because the Coil Voltage has been applied for the required time
                                            //evalnexttime = true; //just in case the timer is contact in its own rung
                                        }
                                    }
                                }
                                else //Not a timer, go straight to high state
                                {
                                    if (Coilstates[r] != true)
                                    {
                                        if (!InExclusionList(Coilnames[r]))
                                            LogEvent(Coilnames[r].ToString(), "High", DateTime.Now.ToLongTimeString());
                                        if (sound)
                                        {
                                            SoundQueue.Add(Coilnames[r].ToString() + "High");
                                            //playstuff = true;
                                        }
                                    }
                                    if (!Coilstates[r])
                                        findChangesinCoils(r); //If there is a change of state, then work out what other rungs are affected
                                    Coilstates[r] = true;
                                }
                            }
                            if (CoilIsTimer[r] != -1) //Coil is a S2D timer 
                            {
                                timerremoveindex = inTimerList(rungtitle, S2DTimersTiming); // remove the timer from list now that it is not timing
                                if (timerremoveindex != -1)
                                    S2DTimersTiming.RemoveRange(timerremoveindex, 1);
                            }
                        }
                        catch { MessageBox.Show("Error Evaluating Rungs - Low to High" + rungtitle.ToString(), "Logic Navigator Error"); }
                    }
                    else
                    {
                        try
                        {
                            Coildrive[r] = false;
                            int timerindex = findS2DTimer(timersNew, rungtitle);
                            if (timerindex != -1) // Slow to drop timer
                            {
                                //Coilnodrive[r] = true;//There is no voltage on the coil, and it's a timer#########
                                //evalnexttime = true; //because it is a timer
                                int timerstartindex = inTimerList(rungtitle, S2DTimersTiming);
                                if (timerstartindex == -1) // Start timing
                                {
                                    TimersTimingStruct timing = new TimersTimingStruct();
                                    timing.timername = rungtitle;
                                    timing.timerstarttime = DateTime.Now;
                                    timing.timeElapsed = 0;
                                    timing.totaltime = (int)(((float)Gettime(timerindex, "Clear")) / 1000);
                                    if (timing.totaltime == 0)
                                    {
                                        if (Coilstates[r])
                                            findChangesinCoils(r); //If there is a change of state, then work out what other rungs are affected
                                        Coilstates[r] = false;
                                        evalnexttime = true; //just in case the timer is contact in its own rung
                                    }
                                    S2DTimersTiming.Add(timing);
                                    if (!InExclusionList(Coilnames[r]))
                                        if (sound)
                                            PlayTimerStartDownSound();
                                }
                                else // In the timer list already, see if timed out yet
                                {
                                    TimersTimingStruct timerElement = (TimersTimingStruct)S2DTimersTiming[timerstartindex];
                                    TimeSpan difference = DateTime.Now - (DateTime)timerElement.timerstarttime;
                                    int diffmsec = (int)(((float)(difference.TotalMilliseconds)) * simspeed);
                                    timerElement.timeElapsed = (int)(((float)diffmsec) / 1000);
                                    UpdateS2DTimersTiming(timerElement);
                                    if (diffmsec > Gettime(timerindex, "Clear")) //3 secs
                                    {
                                        if (Coilstates[r] != false)
                                            if (!InExclusionList(Coilnames[r]))
                                            {
                                                LogEvent(Coilnames[r].ToString(), "Low", DateTime.Now.ToLongTimeString());
                                                if (sound)
                                                {
                                                    SoundQueue.Add(Coilnames[r].ToString() + "Low");
                                                    //playstuff = true;
                                                }
                                            }
                                        if (Coilstates[r])
                                            findChangesinCoils(r); //If there is a change of state, then work out what other rungs are affected
                                        Coilstates[r] = false;
                                        //evalnexttime = true; //just in case the timer is contact in its own rung
                                    }
                                }
                            }
                            else //  not a timer
                            {
                                if (Coilstates[r] != false)
                                    if (!InExclusionList(Coilnames[r]))
                                    {
                                        LogEvent(Coilnames[r].ToString(), "Low", DateTime.Now.ToLongTimeString());
                                        if (sound)
                                        {
                                            SoundQueue.Add(Coilnames[r].ToString() + "Low");
                                            //playstuff = true;
                                        }
                                    }
                                if (Coilstates[r])
                                    findChangesinCoils(r); //If there is a change of state, then work out what other rungs are affected
                                Coilstates[r] = false;
                            }
                            timerremoveindex = inTimerList(rungtitle, S2PTimersTiming); // remove the timer from list now that it has it is not timing
                            if (timerremoveindex != -1)
                                S2PTimersTiming.RemoveRange(timerremoveindex, 1);
                        }
                        catch { MessageBox.Show("Error Evaluating Rungs - High to Low" + rungtitle.ToString(), "Logic Navigator Error"); }
                    }
                }
                if (!evalnexttime) evaluationlist[r] = false; // cross this off the list of rungs to evaluate 
            }
            TransferCoilStatestoHighRungs();
            //statusBar1.Text = "Workload " + workload.ToString() + ", " + runglisting;//;
            //statusBar1.Text = ((float)searchdepth / (float)globalcounter).ToString() + ", " + rungsevaluated.ToString();             
        }

        private void TransferCoilStatestoHighRungs()
        {
            HighRungs.Clear();
            for (int k = 0; k < Coilstates.Count; k++)
                if (Coilstates[k])
                    HighRungs.Add(Coilnames[k]);
        }


        private void TransferHighRungstoCoilStates()
        {
            for (int k = 0; k < Coilstates.Count; k++)
                Coilstates[k] = false;
            for (int k = 0; k < Coilstates.Count; k++)
                for (int i = 0; i < HighRungs.Count; i++)
                    if ((string)HighRungs[i] == Coilnames[k])
                        Coilstates[k] = true;
        }

        private void GetRungstoEvaluate() //Get a list of rungs to be evaluated
        {
            try
            {
                for (int k = 0; k < Changes.Count; k++) //Go through each input and rung that has changed
                {
                    int contactnumber = findInContactAnalysisList((string)Changes[k]);
                    ContactAnalysis contact = (ContactAnalysis)contactAnalysis[contactnumber];
                    for (int j = 0; j < contact.usedin.Count; j++)
                        AddtoChangesInts((int)contact.usedin[j]);
                }
            }
            catch { MessageBox.Show("Error working out which rungs to evaluate", "Logic Navigator Error"); }

        }

        private void AddtoChangesInts(int rungnumber)
        {
            if (!FindNumber(rungnumber))
            {
                ChangesInts.Add(rungnumber);
                ChangesInts.Sort();
            }
        }

        private void ChangeRungState(int rungindex, bool state)
        {
            Coilstates[rungindex] = state;
        }

        private bool FindNumber(int rungnumber)
        {
            for (int i = 0; i < ChangesInts.Count; i++)
                if ((int)ChangesInts[i] == rungnumber)
                    return true;
            return false;
        }

        private void UpdateS2PTimersTiming(TimersTimingStruct timerElement)
        {
            for (int i = 0; i < S2PTimersTiming.Count; i++)
            {
                TimersTimingStruct timerElementold = (TimersTimingStruct)S2PTimersTiming[i];
                if (timerElementold.timername == timerElement.timername)
                {
                    S2PTimersTiming.RemoveAt(i);
                    S2PTimersTiming.Add(timerElement);
                }
            }
        }

        private void UpdateS2DTimersTiming(TimersTimingStruct timerElement)
        {
            for (int i = 0; i < S2DTimersTiming.Count; i++)
            {
                TimersTimingStruct timerElementold = (TimersTimingStruct)S2DTimersTiming[i];
                if (timerElementold.timername == timerElement.timername)
                {
                    S2DTimersTiming.RemoveAt(i);
                    S2DTimersTiming.Add(timerElement);
                }
            }
        }

        private int Gettime(int timerindex, string setOrClear) //in Milliseconds
        {

            ML2Timer timerElement = (ML2Timer)timersNew[timerindex];
            string time = "";
            if (setOrClear == "Set")
                time = timerElement.setTime;
            if (setOrClear == "Clear")
                time = timerElement.clearTime;
            if (time.LastIndexOf("msec", sc) != -1)
                return Convert.ToInt32(time.Substring(0, time.LastIndexOf("msec", sc)));
            if (time.LastIndexOf("sec", sc) != -1)
                return 1000 * Convert.ToInt32(time.Substring(0, time.LastIndexOf("sec", sc)));
            if (time.LastIndexOf("s", sc) != -1)
                return 1000 * (int)Convert.ToDouble(time.Substring(0, time.LastIndexOf("s", sc)));
            if (time.LastIndexOf("m", sc) != -1)
                return 60000 * (int)Convert.ToDouble(time.Substring(0, time.LastIndexOf("m", sc)));
            if (time.LastIndexOf("h", sc) != -1)
                return 3600000 * (int)Convert.ToDouble(time.Substring(0, time.LastIndexOf("h", sc)));
            return 0;
        }


        private int findS2PTimer(ArrayList timer, string name) //Find slow to Pick timer
        {
            if (name == "")
                return -1;
            for (int i = 0; i < timer.Count; i++)
            {
                ML2Timer timerElement = (ML2Timer)timer[i];
                if (((string)timerElement.timerName == name) && (timerElement.setTime != ""))
                    return i;
            }
            return -1;
        }

        private int findS2DTimer(ArrayList timer, string name) //Find slow to Drop timer
        {
            if (name == "")
                return -1;
            for (int i = 0; i < timer.Count; i++)
            {
                ML2Timer timerElement = (ML2Timer)timer[i];
                if (((string)timerElement.timerName == name) && (timerElement.clearTime != ""))
                    return i;
            }
            return -1;
        }

        private int findTimer(ArrayList timer, string name) //Find slow to timer
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

        private void RemoveRungfromList(string name, ArrayList list)
        {
            for (int i = 0; i < list.Count; i++)
                if (name == (string)list[i])
                    list.RemoveAt(i);
        }

        private void RemoveRungfromString(string name, string str)
        {
            if (str.IndexOf("%" + name + "%") != -1)
                str = str.Substring(0, str.IndexOf("%" + name)) +
                str.Substring(str.IndexOf("%" + name) + name.Length + 2, str.Length - (str.IndexOf("%" + name) + name.Length + 2));
        }

        private bool inListString(string name, List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
                if (name == list[i]) return true;
            return false;
        }


        private bool inList(string name, ArrayList list)
        {
            for (int i = 0; i < list.Count; i++)
                if (name == (string)list[i]) return true;
            return false;
        }

        private int inTimerList(string name, ArrayList list)
        {
            if (list == null) return -1;
            for (int i = 0; i < list.Count; i++)
            {
                TimersTimingStruct timerElement = (TimersTimingStruct)list[i];
                if (name == timerElement.timername)
                    return i;
            }
            return -1;
        }

        private bool inforcelowlist(List<Contact> rung, int coilindex)
        {
            if (forceLow.Count > 0)
                for (int i = 0; i < rung.Count; i++)
                    if (rung[i].typeOfCell == "Coil")
                        for (int j = 0; j < forceLow.Count; j++)
                            if (rung[i].name == forceLow[j])
                                return (true);
            return (false);
        }

        private bool inforcehighlist(List<Contact> rung, int coilindex)
        {
            if(forceHigh.Count > 0)      
                for(int i = 0; i < rung.Count; i++)                                     
                    if (rung[i].typeOfCell == "Coil")
                        for(int j = 0; j < forceHigh.Count; j++)                        
                            if (rung[i].name == forceHigh[j])
                                return (true);                                
            return (false);
        }

        private bool EvaluateRungList(List<Contact> rung, int coilindex)
        {
            Point Coil = new Point();
            Coil = CoilContactrefs[coilindex];
            int contactindex = FindArrayIndexList(rung, Coil.X, Coil.Y);
            if ((coilindex == 0) && (contactindex != -1))
            {
                if (RungCrawlerList(rung, Coil.X, Coil.Y, Coil.X, Coil.Y + 1, contactindex))
                    result.Text = "High" + coilindex.ToString();
                else
                    result.Text = "Low" + coilindex.ToString();
            }
            wipevoltages();
            return RungCrawlerList(rung, Coil.X, Coil.Y, Coil.X, Coil.Y + 1, contactindex);
        }

        private bool RungCrawlerList(List<Contact> rung, int currentX, int currentY, int prevX, int prevY, int index)
        {
            int voltagefeeds = 0;
            bool voltagehere = false;
            searchdepth++;
            if (currentY == 0)
                return true; //Made it to the positive rail            
            int contactindex = index;
            if (index == -1) contactindex = FindArrayIndexList(rung, currentX, currentY); //Find the array index based on the rungs coordinates            
            if (contactindex == -1) return false; //contact does not exist in the array, this return path should never occur    
            Contact contact = (Contact)rung[contactindex];
            bool contactstate = false;
            if (contact.rungindex != -1)
                contactstate = Coilstates[contact.rungindex];
            if (contact.inputindex != -1)
                contactstate = Inputstates[contact.inputindex];
            //Contact, Coil, Horizontal Shunt, Vertical Shunt, Empty cell, End Contact
            try
            {
                if (
                        (contact.typeOfCell == "Empty") ||
                        (contact.typeOfCell == "Horizontal Shunt") ||
                        (contact.typeOfCell == "Coil") ||
                        (
                            (contact.typeOfCell == "Contact") &&
                            (prevY != currentY + 1)
                        ) ||
                        (
                            (
                                (contact.typeOfCell == "Contact") &&
                                (prevY == currentY + 1)
                            )
                            &&
                            (
                                (
                                    //!inList(contact.name, HighRungs) && // 
                                    !contactstate &&
                                    //!inList(contact.name, SimInputs) &&
                                    contact.NormallyClosed //Backcontact
                                )
                                ||
                                (
                                    (
                                        contactstate //||
                                                     //inList(contact.name, HighRungs) ||
                                                     //inList(contact.name, SimInputs)
                                    )
                                    &&
                                    !contact.NormallyClosed //Front Contact
                                )
                            )
                        )
                    )
                {
                    voltagehere = voltagematrix[currentX, currentY];
                    voltagematrix[currentX, currentY] = true;
                    if (currentY == 1)
                        return true; //Made it to the Positive Rail
                    if (voltagehere)
                        return false; //Voltage here already, look no further                       
                    if (contact.leftLink) //See if there is any voltage from the cell to the left
                        if (CrawlList(rung, currentX, currentY - 1, currentX, currentY, prevX, prevY, contact.leftLinkindex))
                            voltagefeeds++;
                    if (contact.bottomLink) //See if there is any voltage from the cell below
                        if (CrawlList(rung, currentX + 1, currentY, currentX, currentY, prevX, prevY, contact.bottomLinkindex))
                            voltagefeeds++;
                    if (contact.topLink) //See if there is any voltage from the cell above
                        if (CrawlList(rung, currentX - 1, currentY, currentX, currentY, prevX, prevY, contact.topLinkindex))
                            voltagefeeds++;
                    if (calcmethod == RELAYCALC)
                    {
                        if ((contact.typeOfCell == "Horizontal Shunt") && (prevY != currentY + 1)) //Reverse path
                            if (CrawlList(rung, currentX, currentY + 1, currentX, currentY, prevX, prevY, contact.rightLinkindex))
                                voltagefeeds++;
                        if ((contact.typeOfCell == "Contact") && (prevY != currentY + 1) &&
                            ((!contactstate && contact.NormallyClosed /*Backcontact*/)
                            || (contactstate && !contact.NormallyClosed /*Front Contact*/)))  //Reverse Path through a contact
                            if (CrawlList(rung, currentX, currentY + 1, currentX, currentY, prevX, prevY, contact.rightLinkindex))
                                voltagefeeds++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error Evaluating Contact - " + contact.rungindex + ", " + contact.name + ", " + debuginfo1 + ", " + currentY + ", " +
                    contact.typeOfCell + ", " + contactstate.ToString() + ", " + contactindex, "Logic Navigator Error");
            }
            if (contact.typeOfCell == "Vertical Shunt")
            {
                if (CrawlList(rung, currentX - 1, currentY, currentX, currentY, prevX, prevY, -1)) voltagefeeds++;
                if (CrawlList(rung, currentX + 1, currentY, currentX, currentY, prevX, prevY, -1)) voltagefeeds++;
            }
            if (voltagefeeds > 0) return true;
            return false;
        }

        private bool CrawlList(List<Contact> rung, int nextX, int nextY, int currentX, int currentY, int prevX, int prevY, int index)
        {
            if ((nextX == prevX) && (nextY == prevY))
                return false; //crawler is back tracking so return false            
            return RungCrawlerList(rung, nextX, nextY, currentX, currentY, index);
        }

        private int FindArrayIndex(ArrayList rung, int coordX, int coordY)
        {
            for (int i = 1; i < rung.Count - 1; i++)
            {
                Contact contact = (Contact)rung[i];
                if ((contact.x == coordX) && (contact.y == coordY))
                    return i;
            }
            return -1;
        }

        private int FindArrayIndexList(List<Contact> rung, int coordX, int coordY)
        {
            for (int i = 0; i < rung.Count; i++)
            {
                Contact contact = (Contact)rung[i];
                if ((contact.x == coordX) && (contact.y == coordY))
                    return i;
            }
            return -1;
        }

        private Point FindCoil(ArrayList rung)
        {
            Point coord = new Point();
            coord.X = 0;
            coord.Y = 0;
            for (int i = 1; i < rung.Count - 1; i++)
            {
                Contact contact = (Contact)rung[i];
                if (contact.typeOfCell == "Coil")
                {
                    coord.X = contact.x;
                    coord.Y = contact.y;
                }
            }
            return (coord);
        }

        private Point FindCoilList(List<Contact> rung)
        {
            Point coord = new Point();
            coord.X = 0;
            coord.Y = 0;
            for (int i = 1; i < rung.Count; i++)
            {
                Contact contact = (Contact)rung[i];
                if (contact.typeOfCell == "Coil")
                {
                    coord.X = contact.x;
                    coord.Y = contact.y;
                }
            }
            return (coord);
        }

        private void BroadcastRungStates()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Name == "frmMChild")
                {
                    frmMChild objfrmMChild = (frmMChild)this.MdiChildren[i];
                    objfrmMChild.SimInputs = SimInputs;
                    objfrmMChild.SimRungs = HighRungs;
                    objfrmMChild.SimS2DTimers = S2DTimersTiming;
                    objfrmMChild.SimS2PTimers = S2PTimersTiming;
                    objfrmMChild.InvalidateForm();
                }
                transferSimInputstoSimInputsForm();
                if (this.MdiChildren[i].Name == "frmMChild_SimOutputs")
                {
                    frmMChild_SimOutputs frmMChild_SimOutputs = (frmMChild_SimOutputs)this.MdiChildren[i];
                    //frmMChild_SimOutputs.SimRungs = HighRungs;
                    frmMChild_SimOutputs.UpdateSimInputsList(HighRungs, S2PTimersTiming, S2DTimersTiming);
                }
                if (this.MdiChildren[i].Name == "frmMChild_SimMap")
                {
                    frmMChild_SimMap frmMChild_SimMap = (frmMChild_SimMap)this.MdiChildren[i];
                    //frmMChild_SimOutputs.SimRungs = HighRungs;
                    frmMChild_SimMap.UpdateSimRungsList(HighRungs, S2PTimersTiming, S2DTimersTiming);
                    frmMChild_SimMap.UpdateSimInputsList(SimInputs);
                }
                if (serialworking)
                    BroadcastToSerialLink();

            }
        }

        private void BroadcastToSerialLink()
        {
            int switchnumber = 0; bool changed = false; string tag = "";
            for (int i = 0; i < 32; i++)
                controlswitchnew[i] = false;
            for (int i = 0; i < HighRungs.Count; i++)
            {
                tag = HighRungs[i].ToString();
                if (tag.IndexOf("CONTROLSWITCH#") != -1)
                {
                    switchnumber = Int32.Parse(tag.Substring(14)) - 1;
                    controlswitchnew[switchnumber] = true;
                }
            }
            for (int i = 0; i < 32; i++)
                if (controlswitchnew[i] != controlswitch[i])
                {
                    changed = true;
                    controlswitch[i] = controlswitchnew[i];
                }
            //if (changed) SendSerialMessage();
        }

        private void LogEvent(string Coil, string State, String Time)
        {
            string logmessage = "";
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Name == "frmMChild_Log")
                {
                    frmMChild_Log frmMChild_Log = (frmMChild_Log)this.MdiChildren[i];
                    logmessage = Coil + " - " + State + ",";
                    for (int j = 0; j < 40 - logmessage.Length; j++)
                        logmessage += " ";
                    logmessage += Time + "\r\n";
                    frmMChild_Log.AddChange(logmessage);
                }
            }
        }

        private bool MessageRelevantinLogs(string Coil)
        {
            string logmessage = "";
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Name == "frmMChild_Log")
                {
                    frmMChild_Log frmMChild_Log = (frmMChild_Log)this.MdiChildren[i];
                    logmessage = Coil;
                    if (frmMChild_Log.MessageRelevant(logmessage))
                        return (true);
                }
            }
            return (false);
        }


        private void DuplicateList(ArrayList original, ArrayList duplicate)
        {
            duplicate.Clear();
            for (int i = 0; i < original.Count; i++)
                duplicate.Add(original[i].ToString());
        }

        private void FindChanges(ArrayList oldnames, ArrayList newnames)
        {
            Changes.Clear();
            for (int i = 0; i < oldnames.Count; i++)
                if (!inList(oldnames[i].ToString(), newnames))
                    Changes.Add(oldnames[i].ToString());
            for (int i = 0; i < newnames.Count; i++)
                if (!inList(newnames[i].ToString(), oldnames))
                    if (!inList(newnames[i].ToString(), Changes))
                        Changes.Add(newnames[i].ToString());
        }

        private void menuItem40_Click(object sender, EventArgs e)
        {
            SaveLogicStateFile("");
        }

        private void menuItem41_Click(object sender, EventArgs e)
        {
            freezeRungStates = true;
            OpenLogicStateFile();
            transferSimInputstoSimInputsForm();
            /*
            for (int i = 0; i < this.MdiChildren.Length; i++)
                if (this.MdiChildren[i].Name == "frmMChild_SimInputs")
                {
                    frmMChild_SimInputs objfrmMChild = (frmMChild_SimInputs)this.MdiChildren[i];
                    
                    objfrmMChild.UpdateSimInputsList(SimInputs);
                }*/
            freezeRungStates = false;
        }

        private void LoadLogicState()
        {
            freezeRungStates = true;
            SimInputs.Clear();
            HighRungs.Clear();
            Changes.Clear();
            Openst8File(CurrentState.Text);
            transferSimInputstoSimInputsForm();
            freezeRungStates = false;
        }

        private void transferSimInputstoSimInputsForm()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
                if (this.MdiChildren[i].Name == "frmMChild_SimInputs")
                {
                    frmMChild_SimInputs objfrmMChild = (frmMChild_SimInputs)this.MdiChildren[i];
                    objfrmMChild.UpdateSimInputsList(SimInputs);
                }
        }

        private void transferSimInputstoSimMapForm()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
                if (this.MdiChildren[i].Name == "frmMChild_SimMap")
                {
                    frmMChild_SimMap objfrmMChild = (frmMChild_SimMap)this.MdiChildren[i];
                    //objfrmMChild.UpdateSimMapList(SimInputs);
                }
        }

        private void menuItem42_Click(object sender, EventArgs e)
        {
            frmMChild_SimOutputs objfrmMChild = new frmMChild_SimOutputs(interlockingOld, interlockingNew, timersOld, timersNew, drawFont, HighColor, LowColor);
            objfrmMChild.Text = "Rungs";
            objfrmMChild.MdiParent = this;
            objfrmMChild.Show();
        }

        private void Arg1_TextChanged(object sender, EventArgs e)
        {

        }

        private void menuItem45_Click(object sender, EventArgs e)
        {
            simspeed = simspeed * 2;
            //simspeedtext = (int) simspeed;
            menuItem44.Text = "Simulation Speed (x" + simspeed.ToString() + ")";
        }

        private void menuItem46_Click(object sender, EventArgs e)
        {
            simspeed = simspeed / 2;
            menuItem44.Text = "Simulation Speed (x" + simspeed.ToString() + ")";
        }

        private void menuItem48_Click(object sender, EventArgs e)
        {
            simspeed = 1;
            menuItem44.Text = "Simulation Speed (x" + simspeed.ToString() + ")";
        }

        private void menuItem47_Click(object sender, EventArgs e)
        {
            simspeed = 10000;
            menuItem44.Text = "Simulation Speed (x" + simspeed.ToString() + ")";
        }

        private void menuItem50_Click(object sender, EventArgs e)
        {
            //menuItem49.Text = "Cycle Time (200ms)";
            //Simulationtimer.Interval = 2000;
        }

        private void menuItem50_Click_1(object sender, EventArgs e)
        {

        }

        private void PlayTimerStartUpSound()
        {
            try
            {
                ///MyTimerStartUpSoundPlayer.Play();
            }
            catch (Exception MyError)
            {
                MessageBox.Show("An error has occurred: " + MyError.Message);
            }
        }

        private void PlayTimerStartDownSound()
        {
            try
            {
                //MyTimerStartDownSoundPlayer.Play();
            }
            catch (Exception MyError)
            {
                MessageBox.Show("An error has occurred: " + MyError.Message);
            }
        }

        private void PlayUpSound()
        {
            try
            {
                //MyUpSoundPlayer.Play();
            }
            catch (Exception MyError)
            {
                MessageBox.Show("An error has occurred: " + MyError.Message);
            }
        }

        private void PlayDownSound()
        {
            try
            {
                //MyDownSoundPlayer.Play();
            }
            catch (Exception MyError)
            {
                MessageBox.Show("An error has occurred: " + MyError.Message);
            }
        }

        private void ReadCoil(string name)
        {
            //PlayNextToken(name);
        }

        /*private bool PlayNextToken(string name)
        {
            if (name == "") return (false);

            if (name == "Low")
            { SoundLow.PlaySync(); return (true); }
            if (name == "High")
            { SoundHigh.PlaySync(); return (true); }

            if (IsLetter(name[0]))
            {
                PlayLetterSound(name[0]);
                if (name.Length == 1)
                    return (true);
                else PlayNextToken(name.Substring(1));
                return (true);
            }
            if (IsNumber(name[0]))
            {
                int number = GetprefixNumber(name);
                int hundreds = 0;
                int tens = 0;
                int ones = 0;
                if (number > 9)
                {

                    if (number > 99)
                    {
                        hundreds = 100 * (int)Math.Floor((decimal)number / 100);
                        tens = 10 * (int)Math.Floor((decimal)(number - hundreds) / 10);
                        ones = number - tens;
                        PlayNumberSound(hundreds);
                        PlayNumberSound(tens);
                        PlayNumberSound(ones);
                        if (name.Length == 3)
                            return (true);
                        else PlayNextToken(name.Substring(3));
                        return (true);
                    }
                    if ((number > 20) && (number <= 99))
                    {
                        tens = 10 * (int)Math.Floor((decimal)number / 10);
                        ones = number - tens;
                        PlayNumberSound(tens);
                        PlayNumberSound(ones);
                        if (name.Length == 2)
                            return (true);
                        else PlayNextToken(name.Substring(2));
                    }
                    if ((number > 10) && (number <= 20))
                    {
                        PlayNumberSound(number);
                        if (name.Length == 2)
                            return (true);
                        else PlayNextToken(name.Substring(2));
                    }
                }
                else
                {
                    PlayNumberSound(number);
                    if (name.Length == 1)
                        return (true);
                    else PlayNextToken(name.Substring(1));
                    return (true);
                }
            }
            if (!IsLetter(name[0]) && !IsNumber(name[0]))
            {
                if (name.Length == 1)
                    return (true);
                else PlayNextToken(name.Substring(1));
            }
            return (true);
        }*/

        private int GetprefixNumber(string name)
        {
            int end = 0; bool ended = false;
            for (int i = 0; i < name.Length; i++)
            {
                if (!ended)
                {
                    char letter = name[i];
                    if (((letter >= '0') && (letter <= '9')))
                        end = i + 1;
                    else
                        ended = true;
                }
            }
            if (end != 0)
                return (Convert.ToInt32(name.Substring(0, end)));
            return (-1);
        }

        private bool IsLetter(char letter)
        {
            if ((letter >= 'a') && (letter <= 'z'))
                return (true);
            if ((letter >= 'A') && (letter <= 'Z'))
                return (true);
            return (false);
        }

        private bool IsNumber(char number)
        {
            if ((number >= '0') && (number <= '9'))
                return (true);
            return (false);
        }

        /*private void PlayNumberSound(int number)
        {
            try
            {
                switch (number)
                {
                    case 1:
                        SoundOne.PlaySync(); break;
                    case 2:
                        SoundTwo.PlaySync(); break;
                    case 3:
                        SoundThree.PlaySync(); break;
                    case 4:
                        SoundFour.PlaySync(); break;
                    case 5:
                        SoundFive.PlaySync(); break;
                    case 6:
                        SoundSix.PlaySync(); break;
                    case 7:
                        SoundSeven.PlaySync(); break;
                    case 8:
                        SoundEight.PlaySync(); break;
                    case 9:
                        SoundNine.PlaySync(); break;
                    case 10:
                        SoundTen.PlaySync(); break;
                    case 11:
                        SoundEleven.PlaySync(); break;
                    case 12:
                        SoundTwelve.PlaySync(); break;
                    case 13:
                        SoundThirteen.PlaySync(); break;
                    case 14:
                        SoundFourteen.PlaySync(); break;
                    case 15:
                        SoundFifeteen.PlaySync(); break;
                    case 16:
                        SoundSixteen.PlaySync(); break;
                    case 17:
                        SoundSeventeen.PlaySync(); break;
                    case 18:
                        SoundEighteen.PlaySync(); break;
                    case 19:
                        SoundNineteen.PlaySync(); break;
                    case 20:
                        SoundTwenty.PlaySync(); break;
                    case 30:
                        SoundThirty.PlaySync(); break;
                    case 40:
                        SoundForty.PlaySync(); break;
                    case 50:
                        SoundFifty.PlaySync(); break;
                    case 60:
                        SoundSixty.PlaySync(); break;
                    case 70:
                        SoundSeventy.PlaySync(); break;
                    case 80:
                        SoundEighty.PlaySync(); break;
                    case 90:
                        SoundNinety.PlaySync(); break;
                    case 100:
                        SoundHundred.PlaySync(); break;
                    case 200:
                        SoundTwoHundred.PlaySync(); break;
                    case 300:
                        SoundThreeHundred.PlaySync(); break;
                    default:
                        //Console.WriteLine("Invalid selection. Please select 1, 2, or 3.");            
                        break;
                }
            }
            catch (Exception MyError)
            {
                MessageBox.Show("An error has occurred: " + number + ", " + MyError.Message);
            }
        }*/

        /*private void PlayLetterSound(char letter)
        {
            try
            {
                switch (letter)
                {
                    case 'A':
                        SoundA.PlaySync(); break;
                    case 'B':
                        SoundB.PlaySync(); break;
                    case 'C':
                        SoundC.PlaySync(); break;
                    case 'D':
                        SoundD.PlaySync(); break;
                    case 'E':
                        SoundE.PlaySync(); break;
                    case 'F':
                        SoundF.PlaySync(); break;
                    case 'G':
                        SoundG.PlaySync(); break;
                    case 'H':
                        SoundH.PlaySync(); break;
                    case 'I':
                        SoundI.PlaySync(); break;
                    case 'J':
                        SoundJ.PlaySync(); break;
                    case 'K':
                        SoundK.PlaySync(); break;
                    case 'L':
                        SoundL.PlaySync(); break;
                    case 'M':
                        SoundM.PlaySync(); break;
                    case 'N':
                        SoundN.PlaySync(); break;
                    case 'O':
                        SoundO.PlaySync(); break;
                    case 'P':
                        SoundP.PlaySync(); break;
                    case 'Q':
                        SoundQ.PlaySync(); break;
                    case 'R':
                        SoundR.PlaySync(); break;
                    case 'S':
                        SoundS.PlaySync(); break;
                    case 'T':
                        SoundT.PlaySync(); break;
                    case 'U':
                        SoundU.PlaySync(); break;
                    case 'V':
                        SoundV.PlaySync(); break;
                    case 'W':
                        SoundW.PlaySync(); break;
                    case 'X':
                        SoundX.PlaySync(); break;
                    case 'Y':
                        SoundY.PlaySync(); break;
                    case 'Z':
                        SoundZ.PlaySync(); break;


                    default:
                        //Console.WriteLine("Invalid selection. Please select 1, 2, or 3.");            
                        break;
                }
            }
            catch (Exception MyError)
            {
                MessageBox.Show("An error has occurred: " + letter + ", " + MyError.Message);
            }
        }*/

        private void menuItem49_Click(object sender, EventArgs e)
        {
            if (menuItem49.Checked == true) { menuItem49.Checked = false; sound = false; }
            else { menuItem49.Checked = true; sound = true; }
        }

        private void menuItem50_Click_2(object sender, EventArgs e)
        {
            PlayDownSound();
        }

        private string SaveMapXMLFile(string filename, string name, int height, int width, int left, int top)
        {
            string fileNameChosen = "";
            Stream myStream = null;
            string CRLF = "\r\n";
            bool dialog = false;
            string saveNewFile = name;
            if (filename == "")
            {
                if (saveNewFile.Length > 4)
                    saveFileDialog3.FileName = saveNewFile.Substring(0, saveNewFile.Length - 4) + ".map";
                else saveFileDialog3.FileName = "Map File.map";
                if (saveFileDialog3.ShowDialog() == DialogResult.OK)
                {
                    fileNameChosen = saveFileDialog3.FileName;
                    if ((myStream = saveFileDialog3.OpenFile()) != null)
                        dialog = true;
                }
            }
            else
            {
                fileNameChosen = filename;
                myStream = File.OpenWrite(filename);
                dialog = true;
            }

            if (dialog == true)
            {

                //WriteString(myStream, "Logic Navigator Map File:");
                WriteString(myStream, "<?xml version=\u00221.0\u0022 encoding=\u0022UTF-8\u0022?>" + CRLF);
                // <? xml version = "1.0" encoding = "UTF-8" ?>

                WriteString(myStream, "<map_file>" + CRLF);
                //
                //WriteString(myStream, "~~~~~~~~~~~~~");
                //
                //WriteString(myStream, "The purpose of this file is to store the current layout of the map window so that it can be reopened later.");
                //

                WriteString(myStream, "\t<window_properties>" + CRLF);
                WriteString(myStream, "\t\t<name>" + name + "</name>" + CRLF);
                WriteString(myStream, "\t\t<top>" + top + "</top>" + CRLF);
                WriteString(myStream, "\t\t<left>" + left + "</left>" + CRLF);
                WriteString(myStream, "\t\t<height>" + height + "</height>" + CRLF);
                WriteString(myStream, "\t\t<width>" + width + "</width>" + CRLF);
                WriteString(myStream, "\t</window_properties>" + CRLF);
                WriteString(myStream, "\t<objects>" + CRLF);
                for (int i = 0; i < mapObjects.Count; i++)
                {
                    MapObj item = mapObjects[i];
                    WriteString(myStream, "\t\t<object>" + CRLF);

                    WriteString(myStream, "\t\t\t<type>" + item.TypeofObj + "</type>" + CRLF);
                    WriteString(myStream, "\t\t\t<name>" + item.Name + "</name>" + CRLF);
                    WriteString(myStream, "\t\t\t<text>" + item.Textsize + "</text>" + CRLF);
                    WriteString(myStream, "\t\t\t<textsize>" + item.Text + "</textsize>" + CRLF);
                    WriteString(myStream, "\t\t\t<control>" + item.Control + "</control>" + CRLF);
                    WriteString(myStream, "\t\t\t<highcolour>" + item.HighColour.ToArgb() + "</highcolour>" + CRLF);
                    WriteString(myStream, "\t\t\t<lowcolour>" + item.LowColour.ToArgb() + "</lowcolour>" + CRLF);
                    WriteString(myStream, "\t\t\t<rotation_angle>" + item.RotationAngle + "</rotation_angle>" + CRLF);
                    WriteString(myStream, "\t\t\t<shape>" + item.Shape + "</shape>" + CRLF);
                    WriteString(myStream, "\t\t\t<transparent>" + item.Transparent.ToString() + "</transparent>" + CRLF);

                    WriteString(myStream, "\t\t\t<indications>" + CRLF);
                    WriteString(myStream, "\t\t\t\t<indication>" + CRLF);
                    WriteString(myStream, "\t\t\t\t\t<name>" + item.Indication1 + "</name>" + CRLF);
                    WriteString(myStream, "\t\t\t\t\t<colour>" + item.IndColour1.ToArgb() + "</colour>" + CRLF);
                    WriteString(myStream, "\t\t\t\t</indication>" + CRLF);
                    WriteString(myStream, "\t\t\t\t<indication>" + CRLF);
                    WriteString(myStream, "\t\t\t\t\t<name>" + item.Indication2 + "</name>" + CRLF);
                    WriteString(myStream, "\t\t\t\t\t<colour>" + item.IndColour2.ToArgb() + "</colour>" + CRLF);
                    WriteString(myStream, "\t\t\t\t</indication>" + CRLF);
                    WriteString(myStream, "\t\t\t\t<indication>" + CRLF);
                    WriteString(myStream, "\t\t\t\t\t<name>" + item.Indication3 + "</name>" + CRLF);
                    WriteString(myStream, "\t\t\t\t\t<colour>" + item.IndColour3.ToArgb() + "</colour>" + CRLF);
                    WriteString(myStream, "\t\t\t\t</indication>" + CRLF);
                    WriteString(myStream, "\t\t\t\t<indication>" + CRLF);
                    WriteString(myStream, "\t\t\t\t\t<name>" + item.Indication4 + "</name>" + CRLF);
                    WriteString(myStream, "\t\t\t\t\t<colour>" + item.IndColour4.ToArgb() + "</colour>" + CRLF);
                    WriteString(myStream, "\t\t\t\t</indication>" + CRLF);
                    WriteString(myStream, "\t\t\t</indications>" + CRLF);

                    WriteString(myStream, "\t\t\t<start>" + CRLF);
                    WriteString(myStream, "\t\t\t\t<coordinate>" + CRLF);
                    WriteString(myStream, "\t\t\t\t\t<x>" + item.StartLocation.X + "</x>" + CRLF);
                    WriteString(myStream, "\t\t\t\t\t<y>" + item.StartLocation.Y + "</y>" + CRLF);
                    WriteString(myStream, "\t\t\t\t</coordinate>" + CRLF);
                    WriteString(myStream, "\t\t\t</start>" + CRLF);

                    WriteString(myStream, "\t\t\t<end>" + CRLF);
                    WriteString(myStream, "\t\t\t\t<coordinate>" + CRLF);
                    WriteString(myStream, "\t\t\t\t\t<x>" + item.EndLocation.X + "</x>" + CRLF);
                    WriteString(myStream, "\t\t\t\t\t<y>" + item.EndLocation.Y + "</y>" + CRLF);
                    WriteString(myStream, "\t\t\t\t</coordinate>" + CRLF);
                    WriteString(myStream, "\t\t\t</end>" + CRLF);

                    WriteString(myStream, "\t\t</object>" + CRLF);
                }
                WriteString(myStream, "\t</objects>" + CRLF);
                WriteString(myStream, "</map_file>");
                myStream.Close();
            }
            return fileNameChosen;
        }


        private string SaveMapFile(string filename, string name, int height, int width, int left, int top)
        {
            string fileNameChosen = "";
            Stream myStream = null;
            string CRLF = "\r\n";
            bool dialog = false;
            string saveNewFile = name;
            if (filename == "")
            {
                if (saveNewFile.Length > 4)
                    saveFileDialog3.FileName = saveNewFile.Substring(0, saveNewFile.Length - 4) + ".map";
                else saveFileDialog3.FileName = "Map File.map";
                if (saveFileDialog3.ShowDialog() == DialogResult.OK)
                {
                    fileNameChosen = saveFileDialog3.FileName;
                    if ((myStream = saveFileDialog3.OpenFile()) != null)
                        dialog = true;
                }
            }
            else
            {
                fileNameChosen = filename;
                myStream = File.OpenWrite(filename);
                dialog = true;
            }

            if (dialog == true)
            {

                WriteString(myStream, "Logic Navigator Map File:");
                WriteString(myStream, CRLF);
                WriteString(myStream, "~~~~~~~~~~~~~");
                WriteString(myStream, CRLF);
                WriteString(myStream, "The purpose of this file is to store the current layout of the map window so that it can be reopened later.");
                WriteString(myStream, CRLF);
                WriteString(myStream, CRLF);
                WriteString(myStream, "Window: Name: " + name + ", Top: " + top + ", Left: " + left + ", Height: " + height + ", Width: " + width);
                WriteString(myStream, CRLF);
                WriteString(myStream, CRLF);
                for (int i = 0; i < mapObjects.Count; i++)
                {
                    MapObj item = mapObjects[i];
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "Object:");
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "~~~~~~~~~~~~~");
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "TypeofObj:" + item.TypeofObj);
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "Name:" + item.Name);
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "Text:" + item.Text);
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "Textsize:" + item.Textsize);
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "Indication1:" + item.Indication1);
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "Indication2:" + item.Indication2);
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "Indication3:" + item.Indication3);
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "Indication4:" + item.Indication4);
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "StartLocation: X:" + item.StartLocation.X + ", Y:" + +item.StartLocation.Y);
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "EndLocation: X:" + item.EndLocation.X + ", Y:" + +item.EndLocation.Y);
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "RotationAngle: " + item.RotationAngle.ToString());
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "Shape: " + item.Shape);
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "HighColour: " + item.HighColour.ToArgb());
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "IndColour1: " + item.IndColour1.ToArgb());
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "IndColour2: " + item.IndColour2.ToArgb());
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "IndColour3: " + item.IndColour3.ToArgb());
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "IndColour4: " + item.IndColour4.ToArgb());
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "ControlName: " + item.Control);
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "LowColour: " + item.LowColour.ToArgb());
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "Transparent: " + item.Transparent.ToString());
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "IsTrack: " + item.IsTrack.ToString());
                    WriteString(myStream, CRLF); ;
                }
                WriteString(myStream, "~~END of MAP File~~~~~~~~~");
                myStream.Close();
            }
            return fileNameChosen;
        }

        private void SaveLayoutFile(string filename)
        {
            Stream myStream = null;
            string CRLF = "\r\n";
            int height = 0;
            int width = 0;
            int top = 0;
            int left = 0;
            string name = "";
            string wildcard = "";
            int oldindex; //index number of the rung that is open. (Old interlocking)
            int newindex; //index number of the rung that is open. (New interlocking)
            bool dialog = false;
            if (filename == "")
            {
                string saveNewFile = NewFileName.Text.ToString();
                if (saveNewFile.Length < 5) saveNewFile = "Layout.lyt";
                saveLayoutFileDialog.FileName = saveNewFile.Substring(0, saveNewFile.Length - 4) + ".lyt";
                if (saveLayoutFileDialog.ShowDialog() == DialogResult.OK)
                    if ((myStream = saveLayoutFileDialog.OpenFile()) != null)
                    {
                        dialog = true;
                        CurrentLayout.Text = saveLayoutFileDialog.FileName;
                    }
            }
            else
            {
                myStream = File.OpenWrite(filename);
                dialog = true;
            }
            if (dialog == true)
            {
                WriteString(myStream, "Logic Navigator Layout File:");
                WriteString(myStream, CRLF);
                WriteString(myStream, "~~~~~~~~~~~~~");
                WriteString(myStream, CRLF);
                WriteString(myStream, "The purpose of this file is to store the current layout of the windows so they can be reopened later.");
                WriteString(myStream, CRLF);
                WriteString(myStream, CRLF);
                if (dialog == true)
                {
                    WriteString(myStream, "Input Forms:");
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "~~~~~~~~~~~~~");
                    WriteString(myStream, CRLF);

                    for (int i = 0; i < this.MdiChildren.Length; i++)
                        if (this.MdiChildren[i].Name == "frmMChild_SimInputs")
                        {
                            frmMChild_SimInputs objfrmMChild = (frmMChild_SimInputs)this.MdiChildren[i];
                            height = objfrmMChild.Height;
                            width = objfrmMChild.Width;
                            left = objfrmMChild.Location.X;
                            top = objfrmMChild.Location.Y;
                            wildcard = objfrmMChild.searchString;
                            WriteString(myStream, "SimInput: Top: " + top + ", Left: " + left + ", Height: " + height + ", Width: " + width + ", Wildcard: " + wildcard);
                            WriteString(myStream, CRLF);
                        }
                    WriteString(myStream, CRLF); WriteString(myStream, CRLF);

                    WriteString(myStream, "Rung Forms:");
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "~~~~~~~~~~~~~");
                    WriteString(myStream, CRLF);
                    for (int i = 0; i < this.MdiChildren.Length; i++)
                        if (this.MdiChildren[i].Name == "frmMChild_SimOutputs")
                        {
                            frmMChild_SimOutputs objfrmMChild = (frmMChild_SimOutputs)this.MdiChildren[i];
                            height = objfrmMChild.Height;
                            width = objfrmMChild.Width;
                            left = objfrmMChild.Left;
                            top = objfrmMChild.Top;
                            wildcard = objfrmMChild.searchString;
                            WriteString(myStream, "SimOutput: Top: " + top + ", Left: " + left + ", Height: " + height + ", Width: " + width + ", Wildcard: " + wildcard);
                            WriteString(myStream, CRLF);
                        }
                    WriteString(myStream, CRLF);

                    WriteString(myStream, "Rungs:");
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "~~~~~~~~~~~~~");
                    WriteString(myStream, CRLF);
                    for (int i = 0; i < this.MdiChildren.Length; i++)
                        if (this.MdiChildren[i].Name == "frmMChild")
                        {
                            frmMChild objfrmMChild = (frmMChild)this.MdiChildren[i];
                            height = objfrmMChild.Height;
                            width = objfrmMChild.Width;
                            left = objfrmMChild.Left;
                            top = objfrmMChild.Top;
                            name = objfrmMChild.Text;
                            oldindex = objfrmMChild.CurrentOldCell;
                            newindex = objfrmMChild.CurrentNewCell;
                            WriteString(myStream, "Rung: Top: " + top + ", Left: " + left + ", Height: " + height +
                                ", Width: " + width + ", Oldindex: " + oldindex + ", Newindex: " + newindex + ", Name: " + name);
                            WriteString(myStream, CRLF);
                        }
                    WriteString(myStream, CRLF);

                    WriteString(myStream, "Change of State Forms:");
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "~~~~~~~~~~~~~");
                    WriteString(myStream, CRLF);
                    for (int i = 0; i < this.MdiChildren.Length; i++)
                        if (this.MdiChildren[i].Name == "frmMChild_Log")
                        {
                            frmMChild_Log objfrmMChild = (frmMChild_Log)this.MdiChildren[i];
                            height = objfrmMChild.Height;
                            width = objfrmMChild.Width;
                            left = objfrmMChild.Location.X;
                            top = objfrmMChild.Location.Y;
                            wildcard = objfrmMChild.searchString;
                            WriteString(myStream, "Change of State: Top: " + top + ", Left: " + left + ", Height: " + height + ", Width: " + width + ", Wildcard: " + wildcard);
                            WriteString(myStream, CRLF);
                        }
                    WriteString(myStream, CRLF); WriteString(myStream, CRLF);
                    /*
                    WriteString(myStream, "Maps:");
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "~~~~~~~~~~~~~");
                    WriteString(myStream, CRLF);
                    for (int i = 0; i < this.MdiChildren.Length; i++)
                        if (this.MdiChildren[i].Name == "frmMChild_SimMap")
                        {
                            frmMChild_SimMap objfrmMChild = (frmMChild_SimMap)this.MdiChildren[i];
                            height = objfrmMChild.Height;
                            width = objfrmMChild.Width;
                            left = objfrmMChild.Location.X;
                            top = objfrmMChild.Location.Y;
                            name = objfrmMChild.Text.Substring(ProjectDirectory.Text.Length);
                            WriteString(myStream, "Map: Name: " + name  + ", Top: " + top + ", Left: " + left + ", Height: " + height + ", Width: " + width);
                            WriteString(myStream, CRLF);
                        }
                    WriteString(myStream, CRLF); WriteString(myStream, CRLF);
                    */
                    WriteString(myStream, "Exclusion List:");
                    WriteString(myStream, CRLF);
                    WriteString(myStream, "~~~~~~~~~~~~~");
                    WriteString(myStream, CRLF);
                    for (int i = 0; i < ExclusionList.Count; i++)
                    {
                        WriteString(myStream, "Exclude: " + ExclusionList[i]);
                        WriteString(myStream, CRLF);
                    }
                    WriteString(myStream, CRLF);

                    WriteString(myStream, "~~END of LYT File~~~~~~~~~");
                }
                myStream.Close();
            }
        }

        private void LoadLayoutFile()
        {
            if (openLayoutFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenLytFile(openLayoutFileDialog.FileName);
                CurrentLayout.Text = openLayoutFileDialog.SafeFileName;
            }
        }

        private int OpenLytFile(string filenameString)
        {
            int maps = 0;
            try
            {
                if (filenameString.EndsWith(".lyt", true, ci))
                {
                    fileType = "lyt";
                    maps = ParseLYT(filenameString);
                }
            }
            catch { MessageBox.Show("Error opening lyt file", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            return (maps);
        }

        private int ParseLYT(string filenameString)
        {
            string line = "";
            bool endofLYTfile = false;
            int maps = 0;

            int height = 0;
            int width = 0;
            int top = 0;
            int left = 0;
            int oldIndex = 0;
            int newIndex = 0;
            string wildcard = "";
            string name = "";

            Close_All_Rungs();
            ExclusionList.Clear();

            SR = File.OpenText(filenameString);
            while (((line = SR.ReadLine()) != null) && (!endofLYTfile))//Put logic into a single string
            {
                if (line.LastIndexOf("~~END of LYT File~~~~~~~~~") != -1)
                    endofLYTfile = true;
                else
                {
                    if (line.LastIndexOf("SimInput: ") != -1)
                    {
                        top = Int32.Parse(line.Substring(line.LastIndexOf("Top: ") + 5, (line.LastIndexOf(", Left: ") - (5 + line.LastIndexOf("Top: ")))));
                        left = Int32.Parse(line.Substring(line.LastIndexOf("Left: ") + 6, (line.LastIndexOf(", Height: ") - (6 + line.LastIndexOf("Left: ")))));
                        height = Int32.Parse(line.Substring(line.LastIndexOf("Height: ") + 8, (line.LastIndexOf(", Width: ") - (8 + line.LastIndexOf("Height: ")))));
                        width = Int32.Parse(line.Substring(line.LastIndexOf("Width: ") + 7, (line.LastIndexOf(", Wildcard: ") - (7 + line.LastIndexOf("Width: ")))));
                        wildcard = line.Substring(line.LastIndexOf("Wildcard: ") + 10);

                        frmMChild_SimInputs objfrmMChild = new frmMChild_SimInputs(interlockingOld, interlockingNew, timersOld, timersNew, drawFont, HighColor, LowColor);
                        objfrmMChild.Text = "Simulator Inputs";
                        objfrmMChild.MdiParent = this;
                        objfrmMChild.Width = width;
                        objfrmMChild.Height = height;
                        objfrmMChild.StartPosition = FormStartPosition.Manual;
                        objfrmMChild.Location = new Point(left, top);
                        objfrmMChild.searchString = wildcard;
                        objfrmMChild.Show();
                        objfrmMChild.CommitSearchString();
                        objfrmMChild.searchStringTextChanged();
                    }

                    if (line.LastIndexOf("SimOutput: ") != -1)
                    {
                        top = Int32.Parse(line.Substring(line.LastIndexOf("Top: ") + 5, (line.LastIndexOf(", Left: ") - (5 + line.LastIndexOf("Top: ")))));
                        left = Int32.Parse(line.Substring(line.LastIndexOf("Left: ") + 6, (line.LastIndexOf(", Height: ") - (6 + line.LastIndexOf("Left: ")))));
                        height = Int32.Parse(line.Substring(line.LastIndexOf("Height: ") + 8, (line.LastIndexOf(", Width: ") - (8 + line.LastIndexOf("Height: ")))));
                        width = Int32.Parse(line.Substring(line.LastIndexOf("Width: ") + 7, (line.LastIndexOf(", Wildcard: ") - (7 + line.LastIndexOf("Width: ")))));
                        wildcard = line.Substring(line.LastIndexOf("Wildcard: ") + 10);
                        frmMChild_SimOutputs objfrmMChild = new frmMChild_SimOutputs(interlockingOld, interlockingNew, timersOld, timersNew, drawFont, HighColor, LowColor);
                        //objfrmMChild.Text = "Simulator Rungs";
                        objfrmMChild.Text = "Rungs";
                        objfrmMChild.MdiParent = this;
                        objfrmMChild.Width = width;
                        objfrmMChild.Height = height;
                        objfrmMChild.StartPosition = FormStartPosition.Manual;
                        objfrmMChild.Location = new Point(left, top);
                        objfrmMChild.searchString = wildcard;
                        objfrmMChild.Show();
                        objfrmMChild.CommitSearchString();
                        objfrmMChild.searchStringTextChanged();
                    }

                    if (line.LastIndexOf("Rung: ") != -1)
                    {

                        top = Int32.Parse(line.Substring(line.LastIndexOf("Top: ") + 5, (line.LastIndexOf(", Left: ") - (5 + line.LastIndexOf("Top: ")))));
                        left = Int32.Parse(line.Substring(line.LastIndexOf("Left: ") + 6, (line.LastIndexOf(", Height: ") - (6 + line.LastIndexOf("Left: ")))));
                        height = Int32.Parse(line.Substring(line.LastIndexOf("Height: ") + 8, (line.LastIndexOf(", Width: ") - (8 + line.LastIndexOf("Height: ")))));
                        width = Int32.Parse(line.Substring(line.LastIndexOf("Width: ") + 7, (line.LastIndexOf(", Oldindex: ") - (7 + line.LastIndexOf("Width: ")))));
                        oldIndex = Int32.Parse(line.Substring(line.LastIndexOf("Oldindex: ") + 10, (line.LastIndexOf(", Newindex: ") - (10 + line.LastIndexOf("Oldindex: ")))));
                        newIndex = Int32.Parse(line.Substring(line.LastIndexOf("Newindex: ") + 10, (line.LastIndexOf(", Name: ") - (10 + line.LastIndexOf("Newindex: ")))));
                        name = line.Substring(line.LastIndexOf("Name: ") + 6);
                        string drawmode = "";
                        newIndex = findRung(interlockingNew, name) - 1;
                        oldIndex = findRung(interlockingOld, name) - 1;
                        if (newIndex == -2) drawmode = "All Old";
                        else
                        {
                            if (oldIndex == -2) drawmode = "All New";
                            else
                                drawmode = "Normal";
                        }
                        if ((newIndex > 0) && (oldIndex > 0))
                        {
                            frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, oldIndex, newIndex,
                                scaleFactor, drawmode, drawFont, this.Text, false, showTimers, HighColor, LowColor);
                            objfrmMChild.Text = name;
                            objfrmMChild.MdiParent = this;
                            objfrmMChild.Width = width;
                            objfrmMChild.Height = height;
                            objfrmMChild.StartPosition = FormStartPosition.Manual;
                            objfrmMChild.Location = new Point(left, top);
                            objfrmMChild.Show();
                        }
                    }
                    /*
                    if (line.LastIndexOf("Map: ") != -1)
                    {
                        name = line.Substring(line.LastIndexOf("Name: ") + 6, (line.LastIndexOf(", Top: ") - (6 + line.LastIndexOf("Name: "))));
                        top = Int32.Parse(line.Substring(line.LastIndexOf("Top: ") + 5, (line.LastIndexOf(", Left: ") - (5 + line.LastIndexOf("Top: ")))));
                        left = Int32.Parse(line.Substring(line.LastIndexOf("Left: ") + 6, (line.LastIndexOf(", Height: ") - (6 + line.LastIndexOf("Left: ")))));
                        ParseMap(ProjectDirectory.Text + name, top, left);
                        maps++;
                    }*/

                    if (line.LastIndexOf("Exclude:") != -1)
                        ExclusionList.Add(line.Substring(9));
                }
            }
            SR.Close();
            return (maps);
        }

        private void LoadMapFile()
        {
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                OpenMapFile(openFileDialog3.FileName);
                if (CurrentMap1.Text == ProjectDirectory.Text)
                    CurrentMap1.Text = openFileDialog3.FileName;
                else
                    if (CurrentMap2.Text == ProjectDirectory.Text)
                    CurrentMap2.Text = openFileDialog3.FileName;
                else
                        if (CurrentMap3.Text == ProjectDirectory.Text)
                    CurrentMap3.Text = openFileDialog3.FileName;
                else
                            if (CurrentMap4.Text == ProjectDirectory.Text)
                    CurrentMap4.Text = openFileDialog3.FileName;
                else
                                if (CurrentMap5.Text == ProjectDirectory.Text)
                    CurrentMap5.Text = openFileDialog3.FileName;
                else
                                    if (CurrentMap6.Text == ProjectDirectory.Text)
                    CurrentMap6.Text = openFileDialog3.FileName;
                else
                                        if (CurrentMap7.Text == ProjectDirectory.Text)
                    CurrentMap7.Text = openFileDialog3.FileName;
                else
                                            if (CurrentMap8.Text == ProjectDirectory.Text)
                    CurrentMap8.Text = openFileDialog3.FileName;
                else
                                                if (CurrentMap9.Text == ProjectDirectory.Text)
                    CurrentMap9.Text = openFileDialog3.FileName;
                else
                                                    if (CurrentMap10.Text == ProjectDirectory.Text)
                    CurrentMap10.Text = openFileDialog3.FileName;
            }
        }

        private void OpenMapFile(string filenameString)
        {
            try
            {
                if (filenameString.EndsWith(".map", true, ci))
                {
                    fileType = "map";
                    ParseMap(filenameString, 0, 0);
                }
            }
            catch { MessageBox.Show("Error opening map file", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

        }

        private void ParseMap(string filenameString, int top, int left)
        {
            string line = "";
            bool endofMAPfile = false;

            int height = 0;
            int width = 0;

            mapObjects.Clear();
            SR1 = File.OpenText(filenameString);

            while (((line = SR1.ReadLine()) != null) && (endofMAPfile != true))//Put logic into a single string
            {
                if (line.LastIndexOf("~~END of MAP File~~~~~~~~~") != -1)
                    endofMAPfile = true;
                else
                {
                    if (line.LastIndexOf("Window:") != -1)
                    {
                        if (top == 0) top = Int32.Parse(line.Substring(line.LastIndexOf("Top:") + 5, line.LastIndexOf("Left:") - (line.LastIndexOf("Top:") + 7)));
                        if (left == 0) left = Int32.Parse(line.Substring(line.LastIndexOf("Left:") + 6, line.LastIndexOf("Height:") - (line.LastIndexOf("Left:") + 8)));
                        height = Int32.Parse(line.Substring(line.LastIndexOf("Height:") + 8, line.LastIndexOf("Width:") - (line.LastIndexOf("Height:") + 10)));
                        width = Int32.Parse(line.Substring(line.LastIndexOf("Width:") + 7));
                    }

                    if (line.LastIndexOf("Object:") != -1)
                    {
                        MapObj item = new MapObj();
                        line = SR1.ReadLine();
                        line = SR1.ReadLine();
                        item.TypeofObj = line.Substring(line.LastIndexOf("TypeofObj:") + 10);
                        line = SR1.ReadLine();
                        item.Name = line.Substring(line.LastIndexOf("Name:") + 5);
                        line = SR1.ReadLine();
                        item.Text = line.Substring(line.LastIndexOf("Text:") + 5);
                        line = SR1.ReadLine();
                        if (line.LastIndexOf("Textsize:") != -1)
                        {
                            item.Textsize = Int32.Parse(line.Substring(line.LastIndexOf("Textsize:") + 9));
                            line = SR1.ReadLine();
                        }
                        else item.Textsize = 12;
                        item.Indication1 = line.Substring(line.LastIndexOf("Indication1:") + 12);
                        line = SR1.ReadLine();
                        item.Indication2 = line.Substring(line.LastIndexOf("Indication2:") + 12);
                        line = SR1.ReadLine();
                        item.Indication3 = line.Substring(line.LastIndexOf("Indication3:") + 12);
                        line = SR1.ReadLine();
                        item.Indication4 = line.Substring(line.LastIndexOf("Indication4:") + 12);
                        line = SR1.ReadLine();
                        item.StartLocation.X = Int32.Parse(line.Substring(line.LastIndexOf("StartLocation: X:") + 17,
                            (line.LastIndexOf(", Y:") - (17 + line.LastIndexOf("StartLocation: X:")))));
                        item.StartLocation.Y = Int32.Parse(line.Substring(line.LastIndexOf(", Y:") + 4));
                        line = SR1.ReadLine();
                        item.EndLocation.X = Int32.Parse(line.Substring(line.LastIndexOf("EndLocation: X:") + 15,
                            (line.LastIndexOf(", Y:") - (15 + line.LastIndexOf("EndLocation: X:")))));
                        item.EndLocation.Y = Int32.Parse(line.Substring(line.LastIndexOf(", Y:") + 4));
                        line = SR1.ReadLine();
                        item.RotationAngle = Int32.Parse(line.Substring(line.LastIndexOf("RotationAngle:") + 14));
                        line = SR1.ReadLine();
                        item.Shape = line.Substring(line.LastIndexOf("Shape:") + 7);
                        line = SR1.ReadLine();
                        item.HighColour = Color.FromArgb(Int32.Parse(line.Substring(line.LastIndexOf("HighColour: ") + 12)));
                        line = SR1.ReadLine();
                        item.IndColour1 = Color.FromArgb(Int32.Parse(line.Substring(line.LastIndexOf("IndColour1: ") + 12)));
                        line = SR1.ReadLine();
                        item.IndColour2 = Color.FromArgb(Int32.Parse(line.Substring(line.LastIndexOf("IndColour2: ") + 12)));
                        line = SR1.ReadLine();
                        item.IndColour3 = Color.FromArgb(Int32.Parse(line.Substring(line.LastIndexOf("IndColour3: ") + 12)));
                        line = SR1.ReadLine();
                        item.IndColour4 = Color.FromArgb(Int32.Parse(line.Substring(line.LastIndexOf("IndColour4: ") + 12)));
                        line = SR1.ReadLine();
                        item.Control = line.Substring(line.LastIndexOf("ControlName:") + 13);
                        line = SR1.ReadLine();
                        item.LowColour = Color.FromArgb(Int32.Parse(line.Substring(line.LastIndexOf("LowColour: ") + 11)));
                        line = SR1.ReadLine();
                        if (line.Substring(line.LastIndexOf("Transparent: ") + 13) == "False")
                            item.Transparent = false;
                        else item.Transparent = true;
                        if (!openoldmap)
                        {
                            line = SR1.ReadLine();
                            if (line.Substring(line.LastIndexOf("IsTrack: ") + 9) == "False")
                                item.IsTrack = false;
                            else item.IsTrack = true;
                        }
                        mapObjects.Add(item);
                    }
                }
            }

            frmMChild_SimMap objfrmMChild = new frmMChild_SimMap(interlockingOld, interlockingNew, timersOld,
                timersNew, drawFont, HighColor, LowColor, ProjectDirectory.Text + "\\Images\\", this);
            objfrmMChild.Text = filenameString;

            objfrmMChild.MdiParent = this;
            for (int j = 0; j < mapObjects.Count; j++)
            {
                MapObj item = mapObjects[j];
                objfrmMChild.Indications.Add(item);
            }
            objfrmMChild.Width = width;
            objfrmMChild.Height = height;
            objfrmMChild.StartPosition = FormStartPosition.Manual;
            objfrmMChild.Location = new Point(left, top);

            objfrmMChild.Show();

            SR1.Close();
        }

        private void saveLayoutFileDialog_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void menuItem51_Click(object sender, EventArgs e)
        {
            SaveLayoutFile("");
        }

        private void menuItem53_Click(object sender, EventArgs e)
        {
            AppOpenTALFile();
        }


        private void AppOpenTALFile()
        {
            //CommandlineOpenNewFile(NewFileName.Text);
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                NewFileName.Text = openFileDialog2.FileName;
                TALFileName.Text = NewFileName.Text;
                interlockingTAL.Clear();
                OpenTALFiles();
                CurrentTALFile.Text = openFileDialog2.FileName;
                CheckForMultipleAssignments();
                if (interlockingOld.Count == 0)
                    CommandlineOpenOldFile(FileName.Text);
            }
            this.CloseAll.Visible = true;
        }

        private void CheckForMultipleAssignments()
        {
            string name1 = "", name2 = "", premessage = "Warning, the following rungs have been assigned multiple times", warningmessage = "";
            int counter = 0;
            for (int i = 0; i < interlockingNew.Count; i++)
            {
                ArrayList rungPointer = (ArrayList)interlockingNew[i];
                name1 = (string)rungPointer[rungPointer.Count - 1];
                for (int j = 0; j < interlockingNew.Count; j++)
                {
                    ArrayList rungPointer1 = (ArrayList)interlockingNew[j];
                    name2 = (string)rungPointer1[rungPointer1.Count - 1];
                    if (name1 == name2) counter++;
                }
                if (counter != 1) warningmessage += ",  " + name1 + ": " + i.ToString() + ": assigned " + counter.ToString() + " times";
                counter = 0;
            }
            if (warningmessage != "")
                MessageBox.Show(premessage + warningmessage, "Logic Navigator warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void OpenTALFile()
        {
            try
            {
                string filenameString = TALFileName.Text.ToString();
                if (filenameString.EndsWith(".ncd", true, ci))
                {
                    fileType = "NCD";
                    installationNameOld = ParseInstallationName();
                    GCSSVersionOld = ParseGCSSVersion();
                    ParseCommentField();
                    ParseVersionRecord(versionRecOld, filenameString);
                    ParseINSRungs(interlockingTAL, filenameString);
                    //ParseHousings(Housings_Old);
                }
                else if (filenameString.EndsWith(".ins", true, ci))
                {
                    fileType = "INS";
                    installationNameOld = ParseInstallationName();
                    GCSSVersionOld = ParseGCSSVersion();
                    ParseVersionRecord(versionRecOld, filenameString);
                    ParseINSRungs(interlockingTAL, filenameString);
                    //ParseHousings(Housings_Old, timersOld);
                }
                else if (filenameString.EndsWith(".wt2", true, ci))
                {
                    fileType = "WT2";
                    installationNameOld = ParseInstallationName();
                    GCSSVersionOld = ParseGCSSVersion();
                    ParseVersionRecord(versionRecOld, filenameString);
                    ParseWT2Rungs(interlockingTAL, filenameString);
                    //ParseHousings(Housings_Old, timersOld);
                }
                else if (filenameString.EndsWith(".ml2", true, ci) || filenameString.EndsWith(".ML2", true, ci))
                {
                    fileType = "ML2";
                    installationNameOld = ParseML2InstallationName(filenameString);
                    GCSSVersionOld = "";
                    ParseML2Timers(timersTAL, filenameString);
                    ParseML2Rungs(interlockingTAL, filenameString);
                }
                else if (filenameString.EndsWith(".gn2", true, ci) || filenameString.EndsWith(".GN2", true, ci))
                {
                    fileType = "GN2";
                    installationNameOld = ParseGN2InstallationName();
                    GCSSVersionOld = "";
                    ParseML2Timers(timersTAL, filenameString);
                    ParseML2Rungs(interlockingTAL, filenameString);
                }
                else if (filenameString.EndsWith(".lsv", true, ci) || filenameString.EndsWith(".LSV", true, ci))
                {
                    fileType = "LSV";
                    installationNameOld = "";
                    GCSSVersionOld = "";
                    ParseLSVRungs(interlockingTAL, filenameString);
                }
                else if (filenameString.EndsWith(".mlk", true, ci) || filenameString.EndsWith(".MLK", true, ci))
                {
                    fileType = "MLK";
                    installationNameOld = ParseMLKInstallationName();
                    GCSSVersionOld = "";
                    //ParseVersionRecord(versionRecOld);
                    ParseMLKTimers(timersTAL, FileName.Text);
                    ParseMLKRungs(interlockingTAL, filenameString);
                    //ParseHousings(Housings_Old);
                }
                else if (filenameString.EndsWith(".vtl", true, ci) || filenameString.EndsWith(".vtl", true, ci))
                {
                    fileType = "VTL";
                    installationNameOld = "";//ParseML2InstallationName();
                    GCSSVersionOld = "";
                    ParseVTLTimers(timersTAL, filenameString);
                    ParseVTLRungs(interlockingTAL, filenameString);
                }
                else if (filenameString.EndsWith(".nv", true, ci) || filenameString.EndsWith(".nv", true, ci))
                {
                    fileType = "NV";
                    installationNameOld = "";//ParseML2InstallationName();
                    GCSSVersionOld = "";
                    ParseVTLTimers(timersNew, FileName.Text);
                    ParseVTLRungs(interlockingTAL, filenameString);
                }
                else if (filenameString.EndsWith(".txt", true, ci) || filenameString.EndsWith(".TXT", true, ci))
                {
                    fileType = "TXT";
                    installationNameOld = ParseTXTInstallationName(filenameString);
                    GCSSVersionOld = "";
                    //ParseTXTTimers(timersTAL, filenameString);
                    ParseTXTRungs(interlockingTAL, timersNew, filenameString);
                }
                Close_All_Rungs();
                if (interlockingNew.Count != 0)
                {
                    DisposeNewMemory();
                    reloading = true;
                    treeView.Nodes.Clear();
                    reloading = false;
                }
                OpenNewFile("");
                ConcatenateTAL("");
                ConcatenateTALtimers("");
                duplicates = checkforduplicates(interlockingNew);
                statusStrip1.Visible = false;
                statusStrip1.Text = "Okay";
                if (duplicates != "")
                {
                    toolStripStatusLabel1.Text = "Error - Duplicate rung found: - " + duplicates;
                    statusStrip1.Visible = true;
                }
                if ((interlockingOld.Count != 0) && (interlockingNew.Count != 0))
                {
                    this.treeView.Nodes.Clear();
                    process_interlockings(); // Populate rungs list
                    DoDatabase(); // Populate rungs database format
                    ShowMenu(false);
                    //ShowRungPane();
                }
            }
            catch { MessageBox.Show("Error opening old file", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void OpenTALFiles()
        {
            try
            {
                Close_All_Rungs();
                if (interlockingNew.Count != 0)
                {
                    DisposeNewMemory();
                    reloading = true;
                    treeView.Nodes.Clear();
                    reloading = false;
                }
                if (interlockingOld.Count != 0)
                {
                    DisposeOldMemory();
                    reloading = true;
                    treeView.Nodes.Clear();
                    reloading = false;
                }
                //OpenNewFile(prefixmain.Text);

                for (int i = -1; i < 4; i++)
                {
                    string cTALFileName = ""; string prefix = "";
                    if (i == -1) { cTALFileName = FileName.Text; prefix = prefixmain.Text; }
                    if (i == 0) { cTALFileName = TALFileName.Text; prefix = prefixt1.Text; }
                    if (i == 1) { cTALFileName = TAL2FileName.Text; prefix = prefixt2.Text; }
                    if (i == 2) { cTALFileName = TAL3FileName.Text; prefix = prefixt3.Text; }
                    if (i == 3) { cTALFileName = TAL4FileName.Text; prefix = prefixt4.Text; }
                    interlockingTAL.Clear();
                    timersTAL.Clear();
                    if (cTALFileName != "")
                    {
                        string filenameString = cTALFileName;
                        if (filenameString.EndsWith(".ncd", true, ci))
                        {
                            fileType = "NCD";
                            installationNameOld = ParseInstallationName();
                            GCSSVersionOld = ParseGCSSVersion();
                            ParseCommentField();
                            ParseVersionRecord(versionRecOld, filenameString);
                            ParseINSRungs(interlockingTAL, filenameString);
                            //ParseHousings(Housings_New, timersTAL, "INS", filenameString);
                        }
                        else if (filenameString.EndsWith(".ins", true, ci))
                        {

                            fileType = "INS";
                            installationNameNew = ParseInstallationName();
                            GCSSVersionNew = ParseGCSSVersion();
                            ParseVersionRecord(versionRecNew, filenameString);
                            ParseINSRungs(interlockingTAL, filenameString);
                            ParseHousings(Housings_New, timersTAL, "INS", filenameString);
                        }
                        else if (filenameString.EndsWith(".wt2", true, ci))
                        {
                            fileType = "WT2";
                            installationNameNew = ParseInstallationName();
                            GCSSVersionNew = ParseGCSSVersion();
                            ParseVersionRecord(versionRecNew, filenameString);
                            ParseWT2Rungs(interlockingTAL, filenameString);
                            ParseHousings(Housings_New, timersTAL, "WT2", filenameString);
                        }
                        else if (filenameString.EndsWith(".ml2", true, ci))
                        {
                            fileType = "ML2";
                            installationNameOld = ParseML2InstallationName(filenameString);
                            GCSSVersionOld = "";
                            ParseML2Timers(timersTAL, filenameString);
                            ParseML2Rungs(interlockingTAL, filenameString);
                        }
                        else if (filenameString.EndsWith(".gn2", true, ci))
                        {
                            fileType = "GN2";
                            installationNameOld = ParseGN2InstallationName();
                            GCSSVersionOld = "";
                            ParseML2Timers(timersTAL, filenameString);
                            ParseML2Rungs(interlockingTAL, filenameString);
                        }
                        else if (filenameString.EndsWith(".lsv", true, ci))
                        {
                            fileType = "LSV";
                            installationNameOld = "";
                            GCSSVersionOld = "";
                            ParseLSVRungs(interlockingTAL, filenameString);
                        }
                        else if (filenameString.EndsWith(".mlk", true, ci))
                        {
                            fileType = "MLK";
                            installationNameOld = ParseMLKInstallationName();
                            GCSSVersionOld = "";
                            //ParseVersionRecord(versionRecOld);
                            ParseMLKTimers(timersTAL, filenameString);
                            ParseMLKRungs(interlockingTAL, filenameString);
                            //ParseHousings(Housings_Old);
                        }
                        else if (filenameString.EndsWith(".vtl", true, ci))
                        {
                            fileType = "VTL";
                            installationNameOld = "";//ParseML2InstallationName();
                            GCSSVersionOld = "";
                            ParseVTLTimers(timersTAL, filenameString);
                            ParseVTLRungs(interlockingTAL, filenameString);
                        }
                        else if (filenameString.EndsWith(".nv", true, ci))
                        {
                            fileType = "NV";
                            installationNameOld = "";//ParseML2InstallationName();
                            GCSSVersionOld = "";
                            ParseVTLTimers(timersTAL, FileName.Text);
                            ParseVTLRungs(interlockingTAL, filenameString);
                        }
                        else if (filenameString.EndsWith(".txt", true, ci))
                        {
                            fileType = "TXT";
                            installationNameOld = ParseTXTInstallationName(filenameString);
                            GCSSVersionOld = "";
                            //ParseTXTTimers(timersTAL, filenameString);
                            ParseTXTRungs(interlockingTAL, timersNew, filenameString);
                        }
                        ConcatenateTAL(prefix);
                        ConcatenateTALtimers(prefix);
                    }
                }
                /*Close_All_Rungs();
                if (interlockingNew.Count != 0)
                {
                    DisposeNewMemory();
                    reloading = true;
                    treeView.Nodes.Clear();
                    reloading = false;
                }
                OpenNewFile();*/

                //                ConcatenateTAL();
                //              ConcatenateTALtimers();                    

                duplicates = checkforduplicates(interlockingNew);
                statusStrip1.Visible = false;
                statusStrip1.Text = "Okay";
                if (duplicates != "")
                {
                    toolStripStatusLabel1.Text = "Error - Duplicate rung found: - " + duplicates;
                    statusStrip1.Visible = true;
                }
                if ((interlockingOld.Count != 0) && (interlockingNew.Count != 0))
                {
                    this.treeView.Nodes.Clear();
                    process_interlockings(); // Populate rungs list
                    DoDatabase(); // Populate rungs database format
                    ShowMenu(false);
                    //ShowRungPane();
                }
            }
            catch { MessageBox.Show("Error opening old file", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private string checkforduplicates(ArrayList interlocking)
        {
            string left = "";
            for (int i = 0; i < interlocking.Count; i++)
            {
                ArrayList rungPointer = (ArrayList)interlocking[i];
                left = (string)rungPointer[rungPointer.Count - 1];
                for (int j = i + 1; j < interlocking.Count; j++)
                {
                    ArrayList rungPointer1 = (ArrayList)interlocking[j];
                    if (left == (string)rungPointer1[rungPointer1.Count - 1])
                        return (left);
                }
            }
            return "";
        }

        private void menuItem52_Click(object sender, EventArgs e)
        {
            freezeRungStates = true;
            LoadLayoutFile();
            freezeRungStates = false;
        }

        private void frmMDIMain_Scroll(object sender, ScrollEventArgs e)
        {
            statusBar1.Text = "NewValue: " + e.NewValue.ToString() + "OldValue: " + e.OldValue.ToString();
        }

        private void frmMDIMain_MouseWheel(object sender, MouseEventArgs e)
        {
            statusBar1.Text = e.Delta.ToString();
            //SendScrollinWindows(e.Delta);
        }

        private void menuItem54_Click(object sender, EventArgs e)
        {
            frmMChild_Log objfrmMChild = new frmMChild_Log(interlockingOld, interlockingNew, timersOld, timersNew, drawFont);
            objfrmMChild.Text = "Change of State";
            objfrmMChild.MdiParent = this;
            objfrmMChild.Show();
        }

        private void menuItem56_Click(object sender, EventArgs e)
        {
            ExclusionList.Add("FLASH");
        }

        private void process1_Exited(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //int test = 0;
            do
            {
                //if (playstuff)
                {
                    if (SoundQueue.Count > queuemarker)
                    {
                        if (SoundQueue[queuemarker] != null)
                        {
                            if (MessageRelevantinLogs(SoundQueue[queuemarker]))
                                ReadCoil(SoundQueue[queuemarker]);
                            queuemarker++;
                        }
                        //else
                        // test = 1;
                    }
                }
            } while (true);
        }

        private void menuItem50_Click_3(object sender, EventArgs e)
        {
            string mapname = "";
            for (int mapnumber = 100; mapnumber > 0; mapnumber--)
            {
                bool maphit = false;
                for (int i = 0; i < this.MdiChildren.Length; i++)
                    if (this.MdiChildren[i].Text == "Map" + mapnumber.ToString() + ".map")
                        maphit = true;
                if (maphit == false)
                {
                    mapname = "Map" + mapnumber.ToString() + ".map";
                }
            }
            frmMChild_SimMap objfrmMChild = new frmMChild_SimMap(interlockingOld, interlockingNew, timersOld, timersNew, drawFont, HighColor, LowColor, ProjectDirectory.Text + "\\Images\\", this);
            objfrmMChild.Text = mapname;
            objfrmMChild.MdiParent = this;
            objfrmMChild.Show();
        }

        private void menuItem56_Click_1(object sender, EventArgs e)
        {

        }

        private void menuItem56_Click_2(object sender, EventArgs e)
        {
            for (int i = 1; i < 100; i++)
            {
                if (Math.Ceiling((double)i / 3) == i / 3)
                    SoundQueue.Add("B");
                else
                {
                    if (Math.Ceiling((double)i / 5) == i / 5)
                        SoundQueue.Add("F");
                    else
                        SoundQueue.Add(i.ToString());
                }
            }
        }

        private void menuItem57_Click(object sender, EventArgs e)
        {
            SoundQueue.Clear();
        }

        private void menuItem56_Click_3(object sender, EventArgs e)
        {
            SaveMap(CurrentMap1.Text, false/*not XML*/);
        }

        public void SaveMap(string filename, bool XML)
        {
            bool found = false;
            mapObjects.Clear();
            int height = 0; int width = 0; int left = 0; int top = 0; string name = "";
            if (filename == "")
            {
                for (int i = 0; i < this.MdiChildren.Length; i++)
                {
                    if (this.MdiChildren[i].Name == "frmMChild_SimMap")
                    {
                        if (this.MdiChildren[i] == this.ActiveMdiChild)
                        {
                            found = true;
                            frmMChild_SimMap frmMChild_SimMap = (frmMChild_SimMap)this.MdiChildren[i];
                            for (int j = 0; j < frmMChild_SimMap.Indications.Count; j++)
                            {
                                MapObj item = frmMChild_SimMap.Indications[j];
                                mapObjects.Add(item);
                            }
                            height = frmMChild_SimMap.Height;
                            width = frmMChild_SimMap.Width;
                            left = frmMChild_SimMap.Left;
                            top = frmMChild_SimMap.Top;
                            name = frmMChild_SimMap.Text;
                        }
                    }
                }
                if (found)
                {
                    if (XML)
                        CurrentMap1.Text = SaveMapXMLFile(filename, name, height, width, left, top);
                    else
                        CurrentMap1.Text = SaveMapFile(filename, name, height, width, left, top);
                }
            }
            else
            {
                for (int i = 0; i < this.MdiChildren.Length; i++)
                {
                    if (this.MdiChildren[i].Name == "frmMChild_SimMap")
                    {
                        frmMChild_SimMap frmMChild_SimMap = (frmMChild_SimMap)this.MdiChildren[i];
                        if (frmMChild_SimMap.Text == filename)
                        {
                            found = true;
                            for (int j = 0; j < frmMChild_SimMap.Indications.Count; j++)
                            {
                                MapObj item = frmMChild_SimMap.Indications[j];
                                mapObjects.Add(item);
                            }
                            height = frmMChild_SimMap.Height;
                            width = frmMChild_SimMap.Width;
                            left = frmMChild_SimMap.Left;
                            top = frmMChild_SimMap.Top;
                            name = frmMChild_SimMap.Text;
                        }
                    }
                }
                if (found) SaveMapFile(filename, name, height, width, left, top);
            }
            if (!found) //CurrentMap1.Text = SaveMapFile(filename, name, height, width, left, top);
                MessageBox.Show("Please select a map form first, then choose the save menu", "Directions");
        }

        private void menuItem58_Click(object sender, EventArgs e)
        {
            freezeRungStates = true;
            LoadMapFile();
            freezeRungStates = false;
        }

        private void menuItem60_Click(object sender, EventArgs e)
        {
            OpenSpecialFormattingfile();
        }

        private void OpenSpecialFormattingfile()
        {
            if (openst8FileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenspcFile(openst8FileDialog.FileName);
            }
        }


        private void OpenspcFile(string filenameString)
        {
            try
            {
                if (filenameString.EndsWith(".spc", true, ci))
                {
                    fileType = "spc";
                    Parsespc(filenameString);
                }
            }
            catch { MessageBox.Show("Error opening file", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

        }

        private void Parsespc(string filenameString)
        {
            string line = "";
            bool endofSt8File = false;

            SR = File.OpenText(filenameString);
            string stuff = "";
            while (((line = SR.ReadLine()) != null) && (endofSt8File != true))//Put logic into a single string
            {
                line = line.Trim();
                Regex reg = new Regex(@"\s*");
                stuff = stuff + reg.Replace(line, "");
            }


            while (stuff.Length > 0)
            {
                int marker = stuff.IndexOf(",");
                if (marker != -1)
                {
                    objects.Add(stuff.Substring(0, marker));
                    stuff = stuff.Substring(marker + 1);
                }
                else stuff = "";
            }

            SR.Close();
        }

        private void SavespcFile()
        {
            Stream myStream = null;
            string CRLF = "\r\n";

            bool dialog = false;
            string saveNewFile = NewFileName.Text.ToString();
            saveSt8FileDialog.FileName = "save.txt";
            if (saveSt8FileDialog.ShowDialog() == DialogResult.OK)
                if ((myStream = saveSt8FileDialog.OpenFile()) != null)
                    dialog = true;
            if (dialog == true)
            {
                int index = 0;
                while (objects.Count > index)
                {
                    int widthBetweenColumns = Convert.ToInt32(spacesbetweencolumns.Text);
                    int columns = Convert.ToInt32(columnsoutput.Text);
                    int linespergrouping = Convert.ToInt32(LinesPerGroup.Text);
                    bool spaceBetweenGroupings = false;
                    if (SpaceperGrouping.Text == "true") spaceBetweenGroupings = true;
                    bool indentationOnFirstRow = false;
                    if (Indentations.Text == "true") indentationOnFirstRow = true;

                    for (int k = 0; k < linespergrouping; k++)
                    {
                        if ((k != 0) && indentationOnFirstRow)
                            WriteString(myStream, "  ");
                        WriteString(myStream, "        ");
                        for (int i = 0; i < columns; i++)
                        {
                            if (index < objects.Count)
                            {
                                WriteString(myStream, objects[index] + ",");
                                for (int j = 0; j < (widthBetweenColumns - objects[index].Length); j++)
                                    WriteString(myStream, " ");
                                index++;
                            }
                        }
                        WriteString(myStream, CRLF);
                    }
                    if (spaceBetweenGroupings) WriteString(myStream, CRLF);
                }
            }
            myStream.Close();
        }

        private void menuItem61_Click(object sender, EventArgs e)
        {
            SavespcFile();
        }

        private void menuItem62_Click(object sender, EventArgs e)
        {
            objects.Clear();
            OpenSpecialFormattingfile();
        }

        private void menuItem65_Click(object sender, EventArgs e)
        {
        }

        private void menuItem66_Click(object sender, EventArgs e)
        {
        }

        private void menuItem63_Click(object sender, EventArgs e)
        {

        }

        private void menuItem64_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            LinesPerGroup.Visible = false;
            columnsoutput.Visible = false;
            Indentations.Visible = false;
            spacesbetweencolumns.Visible = false;
            SpaceperGrouping.Visible = false;
        }

        private void menuItem65_Click_1(object sender, EventArgs e)
        {
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            LinesPerGroup.Visible = true;
            columnsoutput.Visible = true;
            Indentations.Visible = true;
            spacesbetweencolumns.Visible = true;
            SpaceperGrouping.Visible = true;
        }

        private void menuItem66_Click_1(object sender, EventArgs e)
        {
            frmMChild_Chess objfrmMChild = new frmMChild_Chess(scaleFactor / 2, 40);
            //objfrmMChild.Size = new Size(700, 700);            
            //objfrmMChild.Location = new System.Drawing.Point(1, 1);
            objfrmMChild.Text = rungName;
            objfrmMChild.MdiParent = this;
            objfrmMChild.Show();
        }

        private void menuItem67_Click(object sender, EventArgs e)
        {
            frmMChild_Chess objfrmMChild = new frmMChild_Chess(scaleFactor / 2, 60);
            //objfrmMChild.Size = new Size(700, 700);            
            //objfrmMChild.Location = new System.Drawing.Point(1, 1);
            objfrmMChild.Text = rungName;
            objfrmMChild.MdiParent = this;
            objfrmMChild.Show();
        }

        private void saveProjectFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void menuItem71_Click(object sender, EventArgs e)
        {
            openProjectFile("");
        }

        private void openProjectFile(string projectfilename)
        {
            SimInputs.Clear();
            HighRungs.Clear();
            Changes.Clear();
            if (projectfilename == "")
            {
                if (openProjectFileDialog.ShowDialog() == DialogResult.OK)
                {

                    string directory = openProjectFileDialog.FileName;
                    if (directory.LastIndexOf("\\") != -1)
                        directory = directory.Substring(0, directory.LastIndexOf("\\"));
                    ProjectDirectory.Text = directory;
                    openprjFile(openProjectFileDialog.FileName);
                }
            }
            else
            {
                string directory = projectfilename;
                if (directory.LastIndexOf("\\") != -1)
                    directory = directory.Substring(0, directory.LastIndexOf("\\"));
                ProjectDirectory.Text = directory;
                openprjFile(projectfilename);
            }
            OpenFiles();
        }

        private void addprefix(int talnumber, string prefix)
        {
            if (talnumber == 1) prefixt1.Text = prefix.Substring(7);
            if (talnumber == 2) prefixt2.Text = prefix.Substring(7);
            if (talnumber == 3) prefixt3.Text = prefix.Substring(7);
            if (talnumber == 4) prefixt4.Text = prefix.Substring(7);
        }

        private void openprjFile(string prjFile)
        {
            string line = "";
            bool endofPRJfile = false;

            Close_All_Rungs();
            ExclusionList.Clear();

            SR = File.OpenText(prjFile);
            while (((line = SR.ReadLine()) != null) && (endofPRJfile != true))//Put logic into a single string
            {
                if (line.LastIndexOf("End of Project File") != -1)
                    endofPRJfile = true;
                else
                {
                    if (line.LastIndexOf("Main Data Filename") != -1)
                    {
                        line = SR.ReadLine();
                        line = SR.ReadLine();
                        FileName.Text = ""; prefixmain.Text = "";
                        FileName.Text = ProjectDirectory.Text + "\\" + line;
                        OldFileName.Text = ProjectDirectory.Text + "\\" + line;
                        line = SR.ReadLine();
                        if (line != "")
                            if (line.Substring(0, 2) != "//")
                                if (line.IndexOf("Prefix:") != -1)
                                    prefixmain.Text = line.Substring(7);
                    }
                    if (line.LastIndexOf("Turn Around Logic Data Filename") != -1)
                    {
                        line = SR.ReadLine();
                        line = SR.ReadLine();
                        int TALnumber = 2;
                        TALFileName.Text = ""; prefixt1.Text = "";
                        TAL2FileName.Text = ""; prefixt2.Text = "";
                        TAL3FileName.Text = ""; prefixt3.Text = "";
                        TAL4FileName.Text = ""; prefixt4.Text = "";
                        CurrentTALFile.Text = ProjectDirectory.Text + "\\" + line;
                        TALFileName.Text = ProjectDirectory.Text + "\\" + line;
                        line = SR.ReadLine();
                        while (line != "")
                        {
                            if (line.Substring(0, 2) != "//")
                            {
                                if (line.IndexOf("Prefix:") != -1)
                                    addprefix(TALnumber - 1, line);
                                else
                                {
                                    if (TALnumber == 2) TAL2FileName.Text = ProjectDirectory.Text + "\\" + line;
                                    if (TALnumber == 3) TAL3FileName.Text = ProjectDirectory.Text + "\\" + line;
                                    if (TALnumber == 4) TAL4FileName.Text = ProjectDirectory.Text + "\\" + line;
                                    TALnumber++;
                                }
                            }
                            else
                                line = line;
                            line = SR.ReadLine();
                        }
                    }
                    if (line.LastIndexOf("Current Layout File") != -1)
                    {
                        line = SR.ReadLine();
                        line = SR.ReadLine();
                        CurrentLayout.Text = ProjectDirectory.Text + "\\" + line;
                    }
                    if (line.LastIndexOf("Current Logic State File") != -1)
                    {
                        line = SR.ReadLine();
                        line = SR.ReadLine();
                        CurrentState.Text = ProjectDirectory.Text + "\\" + line;
                    }
                    if (line.LastIndexOf("Map Files:") != -1)
                    {
                        line = SR.ReadLine();
                        line = SR.ReadLine();
                        try
                        {


                            CurrentMap1.Text = ProjectDirectory.Text + "\\" + line.Substring(7);

                            line = SR.ReadLine();
                            CurrentMap2.Text = ProjectDirectory.Text + "\\" + line.Substring(7);

                            line = SR.ReadLine();
                            CurrentMap3.Text = ProjectDirectory.Text + "\\" + line.Substring(7);

                            line = SR.ReadLine();
                            CurrentMap4.Text = ProjectDirectory.Text + "\\" + line.Substring(7);

                            line = SR.ReadLine();
                            CurrentMap5.Text = ProjectDirectory.Text + "\\" + line.Substring(7);
                            try
                            {
                                line = SR.ReadLine();
                                CurrentMap6.Text = ProjectDirectory.Text + "\\" + line.Substring(7);

                                line = SR.ReadLine();
                                CurrentMap7.Text = ProjectDirectory.Text + "\\" + line.Substring(7);

                                line = SR.ReadLine();
                                CurrentMap8.Text = ProjectDirectory.Text + "\\" + line.Substring(7);

                                line = SR.ReadLine();
                                CurrentMap9.Text = ProjectDirectory.Text + "\\" + line.Substring(7);

                                line = SR.ReadLine();
                                CurrentMap10.Text = ProjectDirectory.Text + "\\" + line.Substring(8);
                            }
                            catch { }
                            /*
                            if (line.IndexOf("Map 1: ") != -1)
                                CurrentMap1.Text = ProjectDirectory.Text + "\\" + line.Substring(7);

                            line = SR.ReadLine();
                            if (line.IndexOf("Map 2: ") != -1)
                                CurrentMap2.Text = ProjectDirectory.Text + "\\" + line.Substring(7);

                            line = SR.ReadLine();
                            if (line.IndexOf("Map 3: ") != -1)
                                CurrentMap3.Text = ProjectDirectory.Text + "\\" + line.Substring(7);

                            line = SR.ReadLine();
                            if (line.IndexOf("Map 4: ") != -1)
                                CurrentMap4.Text = ProjectDirectory.Text + "\\" + line.Substring(7);

                            line = SR.ReadLine();
                            if (line.IndexOf("Map 5: ") != -1)
                                CurrentMap5.Text = ProjectDirectory.Text + "\\" + line.Substring(7);

                            line = SR.ReadLine();
                            if (line.IndexOf("Map 6: ") != -1)
                                CurrentMap6.Text = ProjectDirectory.Text + "\\" + line.Substring(7);

                            line = SR.ReadLine();
                            if (line.IndexOf("Map 7: ") != -1)
                                CurrentMap7.Text = ProjectDirectory.Text + "\\" + line.Substring(7);

                            line = SR.ReadLine();
                            if (line.IndexOf("Map 8: ") != -1)
                                CurrentMap8.Text = ProjectDirectory.Text + "\\" + line.Substring(7);

                            line = SR.ReadLine();
                            if (line.IndexOf("Map 9: ") != -1)
                                CurrentMap9.Text = ProjectDirectory.Text + "\\" + line.Substring(7);

                            line = SR.ReadLine();
                            if (line.IndexOf("Map 10: ") != -1)
                                CurrentMap10.Text = ProjectDirectory.Text + "\\" + line.Substring(8);
                                */
                        }
                        catch { }
                    }
                }
            }
            SR.Close();
        }

        private void OpenFiles()
        {
            OpenOldFile();
            OpenNewFile("");
            interlockingTAL.Clear();
            //interlockingOld.Clear();
            //interlockingNew.Clear();
            OpenTALFiles();

            StartSimulation();

            SimInputs.Clear();
            HighRungs.Clear();
            Changes.Clear();
            Openst8File(CurrentState.Text);
            transferSimInputstoSimInputsForm();

            if (OpenLytFile(CurrentLayout.Text) == 0)


                if (CurrentMap1.Text != "")
                    OpenMapFile(CurrentMap1.Text);
            if (CurrentMap2.Text != "")
                OpenMapFile(CurrentMap2.Text);
            if (CurrentMap3.Text != "")
                OpenMapFile(CurrentMap3.Text);
            if (CurrentMap4.Text != "")
                OpenMapFile(CurrentMap4.Text);
            if (CurrentMap5.Text != "")
                OpenMapFile(CurrentMap5.Text);
            if (CurrentMap6.Text != "")
                OpenMapFile(CurrentMap6.Text);
            if (CurrentMap7.Text != "")
                OpenMapFile(CurrentMap7.Text);
            if (CurrentMap8.Text != "")
                OpenMapFile(CurrentMap8.Text);
            if (CurrentMap9.Text != "")
                OpenMapFile(CurrentMap9.Text);
            if (CurrentMap10.Text != "")
                OpenMapFile(CurrentMap10.Text);
            HideRungPane();
        }

        private void menuItem72_Click(object sender, EventArgs e)
        {
            SaveProjectFile();
        }

        private void SaveProjectFile()
        {
            Stream myStream = null;
            string CRLF = "\r\n";
            bool dialog = false;
            string directory = "";
            string saveNewFile = NewFileName.Text.ToString();
            if (saveNewFile.Length < 5) saveNewFile = "Project.prj";
            saveProjectFileDialog.FileName = saveNewFile.Substring(0, saveNewFile.Length - 4) + ".prj";


            if (saveProjectFileDialog.ShowDialog() == DialogResult.OK)
                if ((myStream = saveProjectFileDialog.OpenFile()) != null)
                {
                    dialog = true;
                    FileInfo fi = new FileInfo(saveProjectFileDialog.FileName);
                    directory = fi.DirectoryName;
                }

            if (dialog == true)
            {
                //if (directory.LastIndexOf("\\") != -1)
                //    directory = directory.Substring(directory.LastIndexOf("\\"));

                WriteString(myStream, "Logic Navigator Project File:");
                WriteString(myStream, CRLF);
                WriteString(myStream, "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                WriteString(myStream, CRLF);
                WriteString(myStream, "The purpose of this file is to store the details of a simulation so that it can quickly be opened later.");
                WriteString(myStream, CRLF);
                WriteString(myStream, CRLF);

                WriteString(myStream, "Main Data Filename");
                WriteString(myStream, CRLF);
                WriteString(myStream, "~~~~~~~~~~~~~");
                WriteString(myStream, CRLF);
                WriteString(myStream, WorkoutSavePlace(FileName.Text, directory));
                WriteString(myStream, CRLF);
                WriteString(myStream, CRLF);

                WriteString(myStream, "Turn Around Logic Data Filename");
                WriteString(myStream, CRLF);
                WriteString(myStream, "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                WriteString(myStream, CRLF);
                WriteString(myStream, WorkoutSavePlace(CurrentTALFile.Text, directory));
                WriteString(myStream, CRLF);
                WriteString(myStream, CRLF);

                WriteString(myStream, "Current Layout File");
                WriteString(myStream, CRLF);
                WriteString(myStream, "~~~~~~~~~~~~~~~~~~~");
                WriteString(myStream, CRLF);
                WriteString(myStream, WorkoutSavePlace(CurrentLayout.Text, directory));
                WriteString(myStream, CRLF);
                WriteString(myStream, CRLF);

                WriteString(myStream, "Current Logic State File");
                WriteString(myStream, CRLF);
                WriteString(myStream, "~~~~~~~~~~~~~~~~~~~~~~~~");
                WriteString(myStream, CRLF);
                WriteString(myStream, WorkoutSavePlace(CurrentState.Text, directory));
                WriteString(myStream, CRLF);
                WriteString(myStream, CRLF);

                WriteString(myStream, "Map Files:");
                WriteString(myStream, CRLF);
                WriteString(myStream, "~~~~~~~~~~");
                WriteString(myStream, CRLF);
                WriteString(myStream, "Map 1: " + WorkoutSavePlace(CurrentMap1.Text, directory));
                WriteString(myStream, CRLF);
                WriteString(myStream, "Map 2: " + WorkoutSavePlace(CurrentMap2.Text, directory));
                WriteString(myStream, CRLF);
                WriteString(myStream, "Map 3: " + WorkoutSavePlace(CurrentMap3.Text, directory));
                WriteString(myStream, CRLF);
                WriteString(myStream, "Map 4: " + WorkoutSavePlace(CurrentMap4.Text, directory));
                WriteString(myStream, CRLF);
                WriteString(myStream, "Map 5: " + WorkoutSavePlace(CurrentMap5.Text, directory));
                WriteString(myStream, CRLF);
                WriteString(myStream, "Map 6: " + WorkoutSavePlace(CurrentMap6.Text, directory));
                WriteString(myStream, CRLF);
                WriteString(myStream, "Map 7: " + WorkoutSavePlace(CurrentMap7.Text, directory));
                WriteString(myStream, CRLF);
                WriteString(myStream, "Map 8: " + WorkoutSavePlace(CurrentMap8.Text, directory));
                WriteString(myStream, CRLF);
                WriteString(myStream, "Map 9: " + WorkoutSavePlace(CurrentMap9.Text, directory));
                WriteString(myStream, CRLF);
                WriteString(myStream, "Map 10: " + WorkoutSavePlace(CurrentMap10.Text, directory));
                WriteString(myStream, CRLF);
                WriteString(myStream, CRLF);
                WriteString(myStream, "End of Project File");
                WriteString(myStream, CRLF);
                myStream.Close();
            }
            SaveLayoutFile(CurrentLayout.Text);
            //SaveLogicStateFile(CurrentState.Text);
            SaveMap(CurrentMap1.Text, false);
        }

        private string WorkoutSavePlace(string filename, string projectDirectory)
        {
            int startat = -1;
            startat = filename.LastIndexOf(projectDirectory);
            if (startat != -1)
                return (filename.Substring(startat + projectDirectory.Length + 1));
            else return (filename);
        }

        private void menuItem74_Click(object sender, EventArgs e)
        {
            SaveMap("", false/*not XML*/);
        }

        private void menuItem76_Click(object sender, EventArgs e)
        {
            if (ProjectDirectory.Visible) ProjectDirectory.Visible = false;
            else ProjectDirectory.Visible = true;
            if (CurrentLayout.Visible) CurrentLayout.Visible = false;
            else CurrentLayout.Visible = true;
            if (CurrentState.Visible) CurrentState.Visible = false;
            else CurrentState.Visible = true;
            if (CurrentTALFile.Visible) CurrentTALFile.Visible = false;
            else CurrentTALFile.Visible = true;
            if (CurrentMap1.Visible) CurrentMap1.Visible = false;
            else CurrentMap1.Visible = true;
            if (CurrentMap2.Visible) CurrentMap2.Visible = false;
            else CurrentMap2.Visible = true;
            if (CurrentMap3.Visible) CurrentMap3.Visible = false;
            else CurrentMap3.Visible = true;
            if (CurrentMap4.Visible) CurrentMap4.Visible = false;
            else CurrentMap4.Visible = true;
            if (CurrentMap5.Visible) CurrentMap5.Visible = false;
            else CurrentMap5.Visible = true;
            if (CurrentMap6.Visible) CurrentMap6.Visible = false;
            else CurrentMap6.Visible = true;
            if (CurrentMap7.Visible) CurrentMap7.Visible = false;
            else CurrentMap7.Visible = true;
            if (CurrentMap8.Visible) CurrentMap8.Visible = false;
            else CurrentMap8.Visible = true;
            if (CurrentMap9.Visible) CurrentMap9.Visible = false;
            else CurrentMap9.Visible = true;
            if (CurrentMap10.Visible) CurrentMap10.Visible = false;
            else CurrentMap10.Visible = true;
            if (label11.Visible) label11.Visible = false;
            else label11.Visible = true;
            if (label7.Visible) label7.Visible = false;
            else label7.Visible = true;
            if (label9.Visible) label9.Visible = false;
            else label9.Visible = true;
            if (label10.Visible) label10.Visible = false;
            else label10.Visible = true;
            if (label8.Visible) label8.Visible = false;
            else label8.Visible = true;
            if (ImageDirectory.Visible) ImageDirectory.Visible = false;
            else ImageDirectory.Visible = true;
            if (label12.Visible) label12.Visible = false;
            else label12.Visible = true;



        }

        private void menuItem77_Click(object sender, EventArgs e)
        {
            openoldmap = true;
            freezeRungStates = true;
            LoadMapFile();
            freezeRungStates = false;
            openoldmap = false;

        }

        private void menuItem78_Click(object sender, EventArgs e)
        {
        }

        private void menuItem79_Click(object sender, EventArgs e)
        {
            frmMChild_Monitor objfrmMChild = new frmMChild_Monitor(interlockingOld, interlockingNew, drawFont);
            objfrmMChild.Text = "NCDM Monitor";
            objfrmMChild.MdiParent = this;
            objfrmMChild.Show();
        }

        private void menuItem81_Click(object sender, EventArgs e)
        {
            HighColor = Color.Blue;
            LowColor = Color.Red;
        }

        private void menuItem82_Click(object sender, EventArgs e)
        {
            HighColor = Color.Red;
            LowColor = Color.Blue;
        }

        private void menuItem85_Click(object sender, EventArgs e)
        {
            toolBar1.Visible = false;
            splitter3.Visible = false;
            OldFileText.Visible = false;
            NewFileText.Visible = false;
            //toolBar1.Size.Height = 0;
        }

        private void menuItem86_Click(object sender, EventArgs e)
        {
            toolBar1.Visible = true;
            splitter3.Visible = true;
            OldFileText.Visible = true;
            NewFileText.Visible = true;
            //toolBar1.Size.Height = 0;
        }

        private void menuItem88_Click(object sender, EventArgs e)
        {
            calcmethod = WESTRACECALC;
            menuItem88.Text = "Do not allow reverse paths (PLC) <";// "Westrace <";
            menuItem89.Text = "Allow reverse paths";//"Relay";
        }

        private void menuItem89_Click(object sender, EventArgs e)
        {
            calcmethod = RELAYCALC;
            menuItem88.Text = "Do not allow reverse paths (PLC)";//"Westrace";
            menuItem89.Text = "Allow reverse paths <";//"Relay <";
        }

        private void frmMDIMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Delete)
            {
                //inputToggle = (char)Keys.Delete;
            }
        }

        private void menuItem91_Click(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void menuItem92_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void frmMDIMain_Deactivate(object sender, EventArgs e)
        {
            //window.Topmost = true;
        }

        private void menuItem94_Click(object sender, EventArgs e)
        {
            this.AllowTransparency = true;
            this.Opacity = 50;
        }

        private void menuItem95_Click(object sender, EventArgs e)
        {
            this.AllowTransparency = false;
            this.Opacity = 100;
        }

        private void frmMDIMain_MouseHover(object sender, EventArgs e)
        {
            //this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void menuItem96_Click(object sender, EventArgs e)
        {
            SaveLogicStateFile(CurrentState.Text);
        }

        private void menuItem97_Click(object sender, EventArgs e)
        {
            SaveLayoutFile(CurrentLayout.Text);
        }

        private void frmMDIMain_DragDrop(object sender, DragEventArgs e)
        {
            Boolean first = true;
            //statusBar1.Text = e.Data.ToString();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {    //CommandlineOpenNewFile(file);
                if (first)
                {
                    first = false;
                    if (file.Substring(file.Length - 3) == "prj")
                    {
                        SimInputs.Clear();
                        HighRungs.Clear();
                        Changes.Clear();
                        string directory = file;
                        if (directory.LastIndexOf("\\") != -1)
                            directory = directory.Substring(0, directory.LastIndexOf("\\"));
                        ProjectDirectory.Text = directory;
                        openprjFile(file);
                        OpenFiles();
                    }
                    else
                    {
                        if (interlockingNew.Count != 0)
                        {
                            DisposeNewMemory();
                            reloading = true;
                            treeView.Nodes.Clear();
                            reloading = false;
                        }
                        NewFileName.Text = file;
                        FileName.Text = NewFileName.Text;
                        OpenNewFile("");
                        if (interlockingOld.Count == 0)
                            CommandlineOpenOldFile(FileName.Text);
                    }
                }
            }
            this.CloseAll.Visible = true;
        }

        private void frmMDIMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void NewFileText_DragDrop(object sender, DragEventArgs e)
        {
            Boolean first = true;
            //statusBar1.Text = e.Data.ToString();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {    //CommandlineOpenNewFile(file);
                if (first)
                {
                    first = false;
                    if (interlockingNew.Count != 0)
                    {
                        DisposeNewMemory();
                        reloading = true;
                        treeView.Nodes.Clear();
                        reloading = false;
                    }
                    NewFileName.Text = file;
                    FileName.Text = NewFileName.Text;
                    OpenNewFile("");
                    if (interlockingOld.Count == 0)
                        CommandlineOpenOldFile(FileName.Text);
                }
            }
            this.CloseAll.Visible = true;
        }

        private void NewFileText_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void OldFileText_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void OldFileText_DragDrop(object sender, DragEventArgs e)
        {
            Boolean first = true;
            //statusBar1.Text = e.Data.ToString();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {    //CommandlineOpenNewFile(file);
                if (first)
                {
                    first = false;
                    if (interlockingOld.Count != 0)
                    {
                        DisposeNewMemory();
                        reloading = true;
                        treeView.Nodes.Clear();
                        reloading = false;
                    }
                    OldFileName.Text = file;
                    FileName.Text = OldFileName.Text;
                    OpenOldFile();
                    if (interlockingNew.Count == 0)
                        CommandlineOpenNewFile(FileName.Text);
                }
            }
            this.CloseAll.Visible = true;
        }



        private void menuItem102_Click(object sender, EventArgs e)
        {
            frmWeb objfrmWeb = new frmWeb("http://logicnavigator.weebly.com/sample-projects.html");
            objfrmWeb.ShowDialog();
        }

        private void menuItem101_Click(object sender, EventArgs e)
        {
            frmWeb objfrmWeb = new frmWeb("http://logicnavigator.weebly.com/download.html");
            objfrmWeb.ShowDialog();
        }

        private void menuItem103_Click(object sender, EventArgs e)
        {
            frmWeb objfrmWeb = new frmWeb("https://logicnavigator.weebly.com");
            objfrmWeb.ShowDialog();
        }

        private void menuItem104_Click(object sender, EventArgs e)
        {
            frmWeb objfrmWeb = new frmWeb("https://logicnavigator.weebly.com");
            objfrmWeb.ShowDialog();
        }


        protected void SendMail()
        {
            // Gmail Address from where you send the mail
            var fromAddress = "username@gmail.com"; //your email id from which you want to send e-mail
                                                    // any address where the email will be sending
            var toAddress = "logicnavigator@y7mail.com";// YourEmail.Text.ToString();  //email id to whom you you are sending e-mail
                                                        //Password of your gmail address
            const string fromPassword = "password"; //your email password
                                                    // Passing the values and make a email formate to display
            string subject = "subject";// YourSubject.Text.ToString();
            string body = "From: " + "ken" /*YourName.Text*/ + "\n";
            body += "Email: " + "email";// YourEmail.Text + "\n";
            //body += "Subject: " + YourSubject.Text + "\n";
            //body += "Question: \n" + Comments.Text + "\n";
            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.gmail.com"; //host name
                smtp.Port = 587; //port number
                smtp.EnableSsl = true; //whether your smtp server requires SSL
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            }
            // Passing values to smtp object
            smtp.Send(fromAddress, toAddress, subject, body);
        }

        private void menuItem105_Click(object sender, EventArgs e)
        {
            frmWeb objfrmWeb = new frmWeb("http://logicnavigator.weebly.com/feedback.html");
            objfrmWeb.ShowDialog();
        }

        private void menuItem98_Click(object sender, EventArgs e)
        {
            frmWeb objfrmWeb = new frmWeb("http://www.logicnavigator.net");
            objfrmWeb.ShowDialog();
        }

        private void menuItem35_Click(object sender, EventArgs e)
        {

        }

        private void OldFileText_MouseHover(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }


        private void SendSerialMessage()
        {
            byte[] frame = { packmessage(0),  packmessage(8),
                             packmessage(16), packmessage(24)};
            SendMessages.Text =
                frame[0].ToString() + ", " +
                frame[1].ToString() + ", " +
                frame[2].ToString() + ", " +
                frame[3].ToString();
            try
            {
                serialPort1.Open();
                serialPort1.Write(frame, 0, 4);
            }
            catch
            {
                try
                {
                    serialPort1.Write(frame, 0, 4);
                }
                catch
                {
                    if (serialworking)
                        MessageBox.Show(serialPort1.PortName + " is closed",
                            "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    serialworking = false;
                }
            }
        }

        private byte packmessage(int reference)
        {
            byte numerator = 0;
            byte adder = 1;
            for (int i = reference; i < reference + 8; i++)
            {
                if (controlswitch[i])
                    numerator += adder;
                adder *= 2;
            }
            return (numerator);
        }

        private void menuItem67_Click_1(object sender, EventArgs e)
        {
            objfrmComms objfrmComms = new objfrmComms();

            objfrmComms.ShowDialog();

            serialPort1.PortName = objfrmComms.commport;
            serialPort1.BaudRate = Int32.Parse(objfrmComms.baudrate);
            //serialworking = true;
        }

        private void menuItem106_Click(object sender, EventArgs e)
        {
            serialworking = true;
            label13.Visible = true;
            label14.Visible = true;
            SendMessages.Visible = true;
            textBox1.Visible = true;
        }

        private void menuItem107_Click(object sender, EventArgs e)
        {
            serialworking = false;
            label13.Visible = false;
            label14.Visible = false;
            SendMessages.Visible = false;
            textBox1.Visible = false;
        }

        private void menuItem109_Click(object sender, EventArgs e)
        {
            SaveMap("", true/*save as XML*/);
        }

        private void menuItem111_Click(object sender, EventArgs e)
        {
            frmMChild_Prefix objfrmMChild = new frmMChild_Prefix();
            objfrmMChild.Text = "Prefix Editor";
            objfrmMChild.MdiParent = this;
            objfrmMChild.Show();
        }

        private void menuItem69_Click(object sender, EventArgs e)
        {
            frmMChild_Mandelbrot objfrmMChild = new frmMChild_Mandelbrot();
            objfrmMChild.Text = "Mandelbrot";
            objfrmMChild.MdiParent = this;
            objfrmMChild.Show();
        }

        private void menuItem110_Click(object sender, EventArgs e)
        {
            frmMChild_GameOfLife objfrmMChild = new frmMChild_GameOfLife();
            objfrmMChild.Text = "Game of Life";
            objfrmMChild.MdiParent = this;
            objfrmMChild.Show();
        }

        private void TALFileName_TextChanged(object sender, EventArgs e)
        {

        }

        private void RungGrid_Navigate(object sender, NavigateEventArgs ne)
        {

        }

        private void menuItem113_Click(object sender, EventArgs e)
        {
            frmMChild_Prefixmap objfrmMChild = new frmMChild_Prefixmap();
            objfrmMChild.Text = "MAP file Prefix Editor";
            objfrmMChild.MdiParent = this;
            objfrmMChild.Show();
        }

        private void menuItem116_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void menuItem115_Click(object sender, EventArgs e)
        {
            toolBar1.Visible = false;
            splitter3.Visible = false;
            OldFileText.Visible = false;
            NewFileText.Visible = false;
            //toolBar1.Size.Height = 0;


            frmMChild_Visualiser objfrmMChild_Visualiser = new frmMChild_Visualiser(interlockingOld, interlockingNew, timersOld, timersNew, 2,
    2, scaleFactor, "All New", drawFont, "", gridlines, showTimers, HighColor, LowColor);
            objfrmMChild_Visualiser.Size = new Size(300, 300);// objfrmMChild_Visualiser.RecommendedWidthofWindow(newIndex - 1), objfrmMChild_Visualiser.RecommendedHeightofWindow(newIndex - 1));
            objfrmMChild_Visualiser.Location = new System.Drawing.Point(1, 1);
            objfrmMChild_Visualiser.Text = rungName;
            objfrmMChild_Visualiser.MdiParent = this;
            objfrmMChild_Visualiser.Show();
        }

        private void menuItem118_Click(object sender, EventArgs e)
        {
            menuItem121.Text = "125ms";
            menuItem118.Text = "250ms <";
            menuItem119.Text = "500ms";
            menuItem120.Text = "1000ms";
            cycletimespeed = 250;
        }

        private void menuItem119_Click(object sender, EventArgs e)
        {
            menuItem121.Text = "125ms";
            menuItem118.Text = "250ms";
            menuItem119.Text = "500ms <";
            menuItem120.Text = "1000ms";
            cycletimespeed = 500;
        }

        private void menuItem120_Click(object sender, EventArgs e)
        {
            menuItem121.Text = "125ms";
            menuItem118.Text = "250ms";
            menuItem119.Text = "500ms";
            menuItem120.Text = "1000ms <";
            cycletimespeed = 1000;
        }

        private void menuItem121_Click(object sender, EventArgs e)
        {
            menuItem121.Text = "125ms <";
            menuItem118.Text = "250ms";
            menuItem119.Text = "500ms";
            menuItem120.Text = "1000ms";
            cycletimespeed = 125;
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int lst = 0; lst < forceLow.Count; lst++)
            {
                forceLow.RemoveAt(lst);
                break;
            }
            for (int lst = 0; lst < forceHigh.Count; lst++)
            {
                forceHigh.RemoveAt(lst);
                break;
            }
            statusStrip1.Visible = false;
            EvaluateAll();
        }
    }
}
