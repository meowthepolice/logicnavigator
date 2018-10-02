
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Logic_Navigator
{
    public class frmMChild_GameOfLife : System.Windows.Forms.Form
    {
        private SolidBrush HighlightBrush = new SolidBrush(Color.HotPink);
        private Pen HighlightPen = new Pen(Color.HotPink);

        Graphics eGfx;

        const int WIDTH = 300;
        const int HEIGHT = 200;
        int age = 0;
        bool same = false;

        private int offset = 0;
        private int[,] painting = new int[WIDTH, HEIGHT];
        bool outtofile = false;

        private bool[,] world = new bool[WIDTH, HEIGHT];

        private bool[,] prevworld = new bool[WIDTH, HEIGHT];

        private bool[,] prevprevworld = new bool[WIDTH, HEIGHT];

        private bool[,] template = new bool[20, 20];
        bool walkerfind = false;

        private bool[,] newworld = new bool[WIDTH, HEIGHT];

        Bitmap myBitmap = new Bitmap(WIDTH, HEIGHT);  

        bool show = true;
        

        private Font drawFont = new Font("Tahoma", 11, FontStyle.Regular);
        StringFormat drawFormat = new StringFormat();
                
        //Point offset = new Point(0, 0); Point mouseposition = new Point(0, 0);

        Random randObj = new Random();

        private const int Empty = 0;

        ImageAttributes attr = new ImageAttributes();

        private System.Windows.Forms.ColumnHeader Date;
        private System.Windows.Forms.ColumnHeader VersionNumber;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader Name_test;
        private TextBox textBox1;
        private Label label15;
        private TextBox TimePerMove;
        private Label label29;
        private TextBox ECOmovelist;
        private Label label36;
        private TextBox textBox2;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private Label label37;
        private Label label38;
        private CheckBox IncrementalBoardEvaluation;
        private Button button7;
        private Button button8;
        private Timer generate;
        private Button button1;
        private Button button2;
        private Button button3;
        private Label time;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button9;
        private Button button10;
        private Button button11;
        private Button button12;
        private Button button13;
        private Button button14;
        private Button button15;
        private Button button16;
        private System.ComponentModel.IContainer components;

        public frmMChild_GameOfLife()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            getpainting();
            Invalidate();

        }

        private void getpainting()//drawmandelbrot()
        {

            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                    world[i, j] = false;
            /*for (int i = 200; i < 300; i += 2)
                for (int j = 200; j < 300; j += 2)
                    world[i, j] = true;
              */         
        }

        private void dozoomout()
        {
            
        }

        private void dofactor()
        {
            
        }

        private void drawmandelbrot()
        {
            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                {
                    Rectangle r = new Rectangle(2*i, 2*j, 2, 2);
                    if (world[i, j])
                        eGfx.FillRectangle(Brushes.Blue, r);
                }
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    Rectangle r = new Rectangle(2*WIDTH + 2 * i, 2 * j, 2, 2);
                    if (template[i, j])
                        eGfx.FillRectangle(Brushes.Blue, r);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMChild_GameOfLife));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Name_test = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VersionNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.TimePerMove = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.ECOmovelist = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.IncrementalBoardEvaluation = new System.Windows.Forms.CheckBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.generate = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.time = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(830, 936);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(113, 20);
            this.textBox1.TabIndex = 14;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(827, 920);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(135, 13);
            this.label15.TabIndex = 30;
            this.label15.Text = "50 move stalemate counter";
            // 
            // TimePerMove
            // 
            this.TimePerMove.Location = new System.Drawing.Point(828, 977);
            this.TimePerMove.Name = "TimePerMove";
            this.TimePerMove.Size = new System.Drawing.Size(113, 20);
            this.TimePerMove.TabIndex = 66;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(825, 959);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(116, 13);
            this.label29.TabIndex = 67;
            this.label29.Text = "Average time per move";
            // 
            // ECOmovelist
            // 
            this.ECOmovelist.Location = new System.Drawing.Point(543, 959);
            this.ECOmovelist.Multiline = true;
            this.ECOmovelist.Name = "ECOmovelist";
            this.ECOmovelist.Size = new System.Drawing.Size(278, 38);
            this.ECOmovelist.TabIndex = 79;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(947, 962);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(65, 13);
            this.label36.TabIndex = 92;
            this.label36.Text = "Loop Count:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(950, 977);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(72, 20);
            this.textBox2.TabIndex = 91;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(1147, 980);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(42, 17);
            this.checkBox2.TabIndex = 95;
            this.checkBox2.Text = "AI2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(1099, 980);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(42, 17);
            this.checkBox3.TabIndex = 94;
            this.checkBox3.Text = "AI1";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(1037, 980);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(56, 13);
            this.label37.TabIndex = 93;
            this.label37.Text = "Pawn Blitz";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(1037, 959);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(58, 13);
            this.label38.TabIndex = 96;
            this.label38.Text = "Handicaps";
            // 
            // IncrementalBoardEvaluation
            // 
            this.IncrementalBoardEvaluation.AutoSize = true;
            this.IncrementalBoardEvaluation.Checked = true;
            this.IncrementalBoardEvaluation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IncrementalBoardEvaluation.Location = new System.Drawing.Point(1195, 976);
            this.IncrementalBoardEvaluation.Name = "IncrementalBoardEvaluation";
            this.IncrementalBoardEvaluation.Size = new System.Drawing.Size(164, 17);
            this.IncrementalBoardEvaluation.TabIndex = 97;
            this.IncrementalBoardEvaluation.Text = "Incremental board Evaluation";
            this.IncrementalBoardEvaluation.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.Location = new System.Drawing.Point(757, 12);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 134;
            this.button7.Text = "randomise";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.Location = new System.Drawing.Point(757, 41);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 135;
            this.button8.Text = "gen";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // generate
            // 
            this.generate.Enabled = true;
            this.generate.Interval = 50;
            this.generate.Tick += new System.EventHandler(this.generate_Tick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(757, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 136;
            this.button1.Text = "Start/Stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(758, 118);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 137;
            this.button2.Text = "spaceship";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(757, 147);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 138;
            this.button3.Text = "gun";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_3);
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.Location = new System.Drawing.Point(755, 96);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(26, 13);
            this.time.TabIndex = 139;
            this.time.Text = "time";
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(757, 176);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 140;
            this.button4.Text = "lidka";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(758, 205);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 141;
            this.button5.Text = "century";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Location = new System.Drawing.Point(758, 234);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 142;
            this.button6.Text = "boat";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button9.Location = new System.Drawing.Point(758, 263);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 143;
            this.button9.Text = "cow";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button10.Location = new System.Drawing.Point(757, 292);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 144;
            this.button10.Text = "washerwoman";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button11.Location = new System.Drawing.Point(758, 321);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 145;
            this.button11.Text = "puffer";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button12.Location = new System.Drawing.Point(758, 350);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(75, 23);
            this.button12.TabIndex = 146;
            this.button12.Text = "timebomb";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button13.Location = new System.Drawing.Point(757, 379);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(75, 23);
            this.button13.TabIndex = 147;
            this.button13.Text = "glider";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button14.Location = new System.Drawing.Point(758, 408);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(75, 23);
            this.button14.TabIndex = 148;
            this.button14.Text = "Pufferfish";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button15.Location = new System.Drawing.Point(676, 379);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(75, 23);
            this.button15.TabIndex = 149;
            this.button15.Text = "small rand";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button16
            // 
            this.button16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button16.Location = new System.Drawing.Point(676, 408);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(75, 23);
            this.button16.TabIndex = 150;
            this.button16.Text = "repeat";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // frmMChild_GameOfLife
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(845, 447);
            this.Controls.Add(this.button16);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.time);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.IncrementalBoardEvaluation);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.ECOmovelist);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.TimePerMove);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMChild_GameOfLife";
            this.Text = "Conway\'s Game of Life";
            this.Load += new System.EventHandler(this.frmMChild_Chess_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMChild_Chess_Paint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.frmMChild_Chess_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMChild_Chess_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMChild_Chess_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmMChild_Chess_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void frmMChild_Chess_Load(object sender, EventArgs e)
        {
            // Magic code that allows fast screen drawing
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            randomise();
        }

        private void frmMChild_Chess_Paint(object sender, PaintEventArgs e)
        {
            string progress = "";
            try
            {
                eGfx = e.Graphics;
                if (show) drawmandelbrot();
            }
            catch { MessageBox.Show("Paint: " + progress, "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }
        private void frmMChild_Chess_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void frmMChild_Chess_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void frmMChild_Chess_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void realcoord_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void zoomout_Click(object sender, EventArgs e)
        {
        }

        private void frmMChild_Chess_MouseDoubleClick(object sender, MouseEventArgs e)
        {            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            
        }


        private void smrand()
        {
            Random rnd = new Random((int)DateTime.Now.Millisecond);
            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                    world[i, j] = false;
            for (int i = 0; i < 50; i++)
            {
                int x = rnd.Next(10/*20*/) + WIDTH/2;
                int y = rnd.Next(10/*20*/) + HEIGHT/2;
                world[x, y] = true;
            }
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                    template[i, j] = false;
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                    template[i, j] = world[i + WIDTH/2, j + HEIGHT/2];

            Invalidate();
        }


        private void randomise()
        {
            Random rnd = new Random((int)DateTime.Now.Millisecond);
            for (int i = 0; i < WIDTH * HEIGHT / 3; i++)
            {
                int x = rnd.Next(WIDTH);
                int y = rnd.Next(HEIGHT);
                world[x, y] = true;
            }
            Invalidate();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            randomise();
            /*
            Random rnd = new Random((int)DateTime.Now.Millisecond);
            for (int i = 0; i < WIDTH*HEIGHT/3; i++)
            {
                int x = rnd.Next(WIDTH);
                int y = rnd.Next(HEIGHT);
                world[x, y] = true;                
            }
            Invalidate();*/
        }

        private void newgen()
        {
            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                    newworld[i, j] = false;

            int alivecount = 0;
            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                {
                    alivecount = 0;
                    for (int t = -1; t < 2; t++)
                        if (alive(i - 1, j + t)) alivecount++;
                    if (alive(i, j + 1)) alivecount++;
                    if (alive(i, j - 1)) alivecount++;
                    for (int t = -1; t < 2; t++)
                        if (alive(i + 1, j + t)) alivecount++;
                    if (alive(i, j))
                    {
                        if ((alivecount == 2) || (alivecount == 3))
                            newworld[i, j] = true;
                    }
                    else
                        if (alivecount == 3)
                            newworld[i, j] = true;
                }
            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                    world[i, j] = newworld[i, j];

            time.Text = "Iterations: " + age.ToString();
            if (walkerfind)
            {

                same = true;
                int bits = 0;
                for (int i = 0; i < WIDTH; i++)
                    for (int j = 0; j < HEIGHT; j++)
                    {
                        if (world[i, j]) bits++;
                        bool ppb = prevprevworld[i, j];
                        bool pb = prevworld[i, j];
                        bool b = world[i, j];

                        if (prevprevworld[i, j] != world[i, j])
                            same = false;
                    }
                for (int i = 0; i < WIDTH; i++)
                    for (int j = 0; j < HEIGHT; j++)
                        prevprevworld[i, j] = prevworld[i, j];
                for (int i = 0; i < WIDTH; i++)
                    for (int j = 0; j < HEIGHT; j++)
                        prevworld[i, j] = world[i, j];
                if ((age > 2000) || same)
                {
                    age = 0;
                    smrand();
                    for (int i = 0; i < WIDTH; i++)
                        for (int j = 0; j < HEIGHT; j++)
                            prevprevworld[i, j] = false;
                }
                if (travelled())
                    generate.Enabled = false;
            }
            
            Invalidate();
            age++;

        }

        private bool travelled()
        {
            for (int j = 0; j < HEIGHT; j++)
                if (world[0, j])
                    return (!smallspaceship(0 , j));
            for (int j = 0; j < HEIGHT; j++)
                if (world[WIDTH - 1, j])
                    return (!smallspaceship(WIDTH - 1, j));
            for (int i = 0; i < WIDTH; i++)
                if (world[i, 0])
                    return (!smallspaceship(i, 0));
            for (int i = 0; i < WIDTH; i++)
                if (world[i, HEIGHT - 1])
                    return (!smallspaceship(i, HEIGHT - 1));
            return (false);
        }

        private bool smallspaceship(int i, int j)
        {
            int count = 0;
            for (int k = -3; k < 4; k++)
                for (int l = -3; l < 4; l++)
                    if ((k + i < WIDTH) &&
                        (k + i > -1) &&
                        (l + j < HEIGHT) &&
                        (l + j > -1))
                        if (world[k + i, l + j])
                            count++;
            if (count > 5)
                return (false);
            for (int k = -3; k < 4; k++)
                for (int l = -3; l < 4; l++)
                    if ((k + i < WIDTH) &&
                        (k + i > -1) &&
                        (l + j < HEIGHT) &&
                        (l + j > -1))
                        world[k + i, l + j] = false;
            return (true);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            newgen();
        }

        private bool alive(int x, int y)
        {
            if ((x >= 0) && (x < WIDTH) && (y >= 0) && (y < HEIGHT))
                if (world[x, y])
                    return true;
                else return false;
            else return false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(generate.Enabled) generate.Enabled = false;
            else generate.Enabled = true;
        }

        private void generate_Tick(object sender, EventArgs e)
        {
            newgen();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int[,] spaceship = new int[,] { {0,1,1,0,0}, 
                                            {1,1,1,1,0},
                                            {1,1,0,1,1},
                                            {0,0,1,1,0},
                                            {0,0,0,0,0}};
            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                    world[i, j] = false;
            for (int s = 0; s < 5; s++)
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (spaceship[i, j] == 1)
                            world[10 + i + s * 10, j] = true;
            Invalidate();
        }

        private void button3_Click_3(object sender, EventArgs e)
        {
            int[,] gun = new int[,] {       {0,0,0,0, 0,0,0,0, 0,0,0,0, 0,0,0,0, 0,0,0,0, 0,1,0,0, 0,0,0,0, 0,0,0,0},
                                            {0,0,0,0, 0,0,0,0, 0,0,0,0, 0,0,0,0, 0,0,0,1, 0,1,0,0, 0,0,0,0, 0,0,0,0},
                                            {0,0,0,0, 0,0,0,0, 0,1,1,0, 0,0,0,0, 0,1,1,0, 0,0,0,0, 0,0,0,0, 0,0,1,1},
                                            {0,0,0,0, 0,0,0,0, 1,0,0,0, 1,0,0,0, 0,1,1,0, 0,0,0,0, 0,0,0,0, 0,0,1,1},
                                            {1,1,0,0, 0,0,0,1, 0,0,0,0, 0,1,0,0, 0,1,1,0, 0,0,0,0, 0,0,0,0, 0,0,0,0},
                                            {1,1,0,0, 0,0,0,1, 0,0,0,1, 0,1,1,0, 0,0,0,1, 0,1,0,0, 0,0,0,0, 0,0,0,0},
                                            {0,0,0,0, 0,0,0,1, 0,0,0,0, 0,1,0,0, 0,0,0,0, 0,1,0,0, 0,0,0,0, 0,0,0,0},
                                            {0,0,0,0, 0,0,0,0, 1,0,0,0, 1,0,0,0, 0,0,0,0, 0,0,0,0, 0,0,0,0, 0,0,0,0},
                                            {0,0,0,0, 0,0,0,0, 0,1,1,0, 0,0,0,0, 0,0,0,0, 0,0,0,0, 0,0,0,0, 0,0,0,0},
                                            };
            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                    world[i, j] = false;
            for (int i = 0; i < 32; i++)
                    for (int j = 0; j < 9; j++)
                        if (gun[j, i] == 1)
                            world[i + 10, j + 10] = true;
            Invalidate();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            int[,] gun = new int[,] {{0,0,0,0, 0,0,0,0, 0,0,0},
                                     {0,0,1,0, 0,0,0,0, 0,0,0},
                                     {0,1,0,1, 0,0,0,0, 0,0,0},
                                     {0,0,1,0, 0,0,0,0, 0,0,0},
                                     {0,0,0,0, 0,0,0,0, 0,0,0},

                                     {0,0,0,0, 0,0,0,0, 0,0,0},
                                     {0,0,0,0, 0,0,0,0, 0,0,0},
                                     {0,0,0,0, 0,0,0,0, 0,0,0},
                                     {0,0,0,0, 0,0,0,0, 0,0,0},
                                     {0,0,0,0, 0,0,0,0, 0,0,0},
                                     
                                     {0,0,0,0, 0,0,0,0, 0,0,0},
                                     {0,0,0,0, 0,0,0,0, 0,0,0},
                                     {0,0,0,0, 0,0,0,0, 0,1,0},
                                     {0,0,0,0, 0,0,0,1, 0,1,0},
                                     {0,0,0,0, 0,0,1,1, 0,1,0},
                                     
                                     {0,0,0,0, 0,0,0,0, 0,0,0},
                                     {0,0,0,0, 0,1,1,1, 0,0,0},
                                     {0,0,0,0, 0,0,0,0, 0,0,0}
                                            };
            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                    world[i, j] = false;
            for (int i = 0; i < 11; i++)
                for (int j = 0; j < 18; j++)
                    if (gun[j, i] == 1)
                        world[i + 100, j + 100] = true;
            Invalidate();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            int[,] gun = new int[,] {{0,0,0,0,0,0},
                                     {0,0,0,1,1,0},
                                     {0,1,1,1,0,0},
                                     {0,0,1,0,0,0},
                                     {0,0,0,0,0,0}
                                            };
            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                    world[i, j] = false;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 5; j++)
                    if (gun[j, i] == 1)
                        world[i + 100, j + 100] = true;
            Invalidate();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {

            int[,] gun = new int[,] {   {0,0,0,0, 0,0,0,0, 0,0,0,0, 0,0,1,1},
                                        {0,0,0,0, 0,0,0,0, 0,0,0,0, 0,1,0,1},
                                        {0,0,0,0, 0,0,0,0, 0,0,0,0, 1,0,0,0},
                                        {0,0,0,0, 0,0,0,0, 0,0,0,1, 0,0,0,0},

                                        {0,0,0,0, 0,0,0,0, 0,0,1,0, 0,0,0,0},
                                        {0,0,0,0, 0,0,0,0, 0,1,0,0, 0,0,0,0},
                                        {0,0,0,0, 0,0,0,0, 1,0,0,0, 0,0,0,0},
                                        {0,0,0,0, 0,0,0,1, 0,0,0,0, 0,0,0,0},

                                        {0,0,0,0, 0,0,1,0, 0,0,0,0, 0,0,0,0},
                                        {0,0,0,0, 0,1,0,0, 0,0,0,0, 0,0,0,0},
                                        {0,0,0,0, 1,0,0,0, 0,0,0,0, 0,0,0,0},
                                        {0,0,0,1, 0,0,0,0, 0,0,0,0, 0,0,0,0},

                                        {1,1,1,0, 0,0,0,0, 0,0,0,0, 0,0,0,0},
                                        {0,1,0,0, 0,0,0,0, 0,0,0,0, 0,0,0,0} };


           
            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                    world[i, j] = false;
            for (int i = 0; i < 16; i++)
                for (int j = 0; j < 14; j++)
                    if (gun[j, i] == 1)
                        world[i + 100, j + 100] = true;
            Invalidate();
        }

        private void button9_Click(object sender, EventArgs e)
        {

            int[,] gun = new int[,] {{1,1,0,0, 0,0,0,0, 0,1,1,0, 0,1,1,0, 0,1,1,0, 0,1,1,0, 0,1,1,0, 0,1,1,0, 0,1,1,0, 0,0,0,0},
                                     {1,1,0,0,0,0,1,0,1,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,0,1,1},
                                     {0,0,0,0,1,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1},
                                     {0,0,0,0,1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                                     {0,0,0,0,1,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
                                     {1,1,0,0,0,0,1,0,1,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0},
                                     {1,1,0,0,0,0,0,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,0,0,0} };
            
            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                    world[i, j] = false;
            for (int i = 0; i < 40; i++)
                for (int j = 0; j < 7; j++)
                    if (gun[j, i] == 1)
                        world[i + 100, j + 100] = true;
            Invalidate();
        }

        private void button10_Click(object sender, EventArgs e)
        {


            int[,] gun = new int[,] {{1,0,0,0, 0,0,0,0, 0,0,0,0, 0,0,0,0, 0,0,0,0,  0,0,0,0, 0,0,0,0, 0,0,0,0, 0,0,0,0, 0,0,0,0,  0,0,0,0, 0,0,0,0, 0,0,0,0, 0,0,0,0 },
                                     {1,1,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0 },
                                     {1,1,1,0,0,1,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,0,1,0,1 },
                                     {1,1,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0 },
                                     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 } };





            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                    world[i, j] = false;
            for (int i = 0; i < 14*4; i++)
                for (int j = 0; j < 5; j++)
                    if (gun[j, i] == 1)
                        world[i + 100, j + 100] = true;
            Invalidate();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int[,] gun = new int[,] { { 0, 0, 0, 0, 1, 1, 1, 1, 0, 0 },
                                      { 0, 0, 1, 1, 1, 1, 1, 1, 1, 0 },
                                      { 1, 1, 1, 0, 1, 1, 1, 0, 1, 1 },
                                      { 1, 0, 1, 1, 0, 0, 0, 1, 1, 0 },
                                      { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },

                                      { 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 },
                                      { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
                                      { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
                                      { 0, 0, 0, 0, 1, 0, 0, 0, 1, 0 },
                                      { 0, 0, 0, 0, 0, 1, 1, 1, 1, 0 }};

            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                    world[i, j] = false;
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (gun[j, i] == 1)
                        world[i + 100, j + 100] = true;
            Invalidate();
        }

        private void button12_Click(object sender, EventArgs e)
        {

            int[,] gun = new int[,] { {0,1,0,0, 0,0,0,0, 0,0,0,0, 0,1,1},
                                      {1,0,1,0,0,0,0,1,0,0,0,0,0,0,1},
                                      {0,0,0,0,0,0,0,1,0,0,0,0,1,0,0},
                                      {0,0,1,0,0,1,0,0,0,1,0,0,1,0,0},
                                      {0,0,1,1,0,0,0,0,0,0,1,0,0,0,0},
                                      {0,0,0,1,0,0,0,0,0,0,0,0,0,0,0}
            };

            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                    world[i, j] = false;
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 6; j++)
                    if (gun[j, i] == 1)
                        world[i + 100, j + 100] = true;
            Invalidate();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int[,] gun = new int[,] { {0,0,0,0, 0,0,0,0, 0,0,0,1, 0,1,0,0},
                                      {1,1,0,0, 0,0,0,0, 0,0,1,0, 0,0,0,0},
                                      {1,1,0,0, 0,0,0,0, 0,0,0,1, 0,0,1,0},
                                      {0,0,0,0, 0,0,0,0, 0,0,0,0, 0,1,1,1}
            };

            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                    world[i, j] = false;
            for (int i = 0; i < 16; i++)
                for (int j = 0; j < 4; j++)
                    if (gun[j, i] == 1)
                        world[i + 100, j + 100] = true;
            Invalidate();
        }

        private void button14_Click(object sender, EventArgs e)
        {

            int[,] gun = new int[,] { {0,0,0,1,0,0,0, 0, 0,0,0,1,0,0,0},
                                      {0,0,1,1,1,0,0, 0, 0,0,1,1,1,0,0},
                                      {0,1,1,0,0,1,0, 0, 0,1,0,0,1,1,0},
                                      {0,0,0,1,1,1,0, 0, 0,1,1,1,0,0,0},
                                      {0,0,0,0,0,0,0, 0, 0,0,0,0,0,0,0},
                                      {0,0,0,0,1,0,0, 0, 0,0,1,0,0,0,0},
                                      
                                      {0,0,1,0,0,1,0, 0, 0,1,0,0,1,0,0},
                                      {1,0,0,0,0,0,1, 0, 1,0,0,0,0,0,1},
                                      {1,1,0,0,0,0,1, 0, 1,0,0,0,0,1,1},
                                      {0,0,0,0,0,0,1, 0, 1,0,0,0,0,0,0},
                                      {0,0,0,1,0,1,0, 0, 0,1,0,1,0,0,0},
                                      {0,0,0,0,1,0,0, 0, 0,0,1,0,0,0,0} };

            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                    world[i, j] = false;
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 12; j++)
                    if (gun[j, i] == 1)
                        world[i + 100, j + 100] = true;
            Invalidate();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            generate.Enabled = true;
            age = 0;
            walkerfind = true;
            smrand();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            age = 0;
            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                    world[i, j] = false;
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                    world[i + WIDTH / 2, j + HEIGHT / 2] = template[i,j];
        }
    }
}