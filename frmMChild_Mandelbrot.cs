
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Logic_Navigator
{
    public class frmMChild_Mandelbrot : System.Windows.Forms.Form
    {
        private SolidBrush HighlightBrush = new SolidBrush(Color.HotPink);
        private Pen HighlightPen = new Pen(Color.HotPink);

        Graphics eGfx;

        const int WIDTH = 1920;
        const int HEIGHT = 1080;

        int sweepwidth = WIDTH;
        int sweepheight = HEIGHT;

        int sweep = 1;

        private int[,] painting = new int[WIDTH, HEIGHT];
        bool outtofile = false;

        Bitmap myBitmap = new Bitmap(WIDTH, HEIGHT);  //"C:\\Chess\\background.png");



        double refr = -2f;
        double refi = -1.5f;

        double factorrange = 0;

        int depth = 256;

        double scaleamount = 1;

        bool show = true;
        double zoom = 1;


        private Font drawFont = new Font("Tahoma", 11, FontStyle.Regular);
        StringFormat drawFormat = new StringFormat();

        ArrayList ECOCodeList = new ArrayList();

        Point offset = new Point(0, 0); Point mouseposition = new Point(0, 0);
        private bool autoplay = false;
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
        private TextBox real;
        private TextBox imaginary;
        private TextBox scale;
        private Button button1;
        private Button button2;
        private Label realcoord;
        private Label imaginarycoord;
        private Label imaginary4;
        private Label real4;
        private Label imaginarytrans;
        private Label realtrans;
        private Button zoomin;
        private Button zoomout;
        private Label label5;
        private Label label6;
        private Label label7;
        private RichTextBox richTextBox1;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox Factor;
        private PictureBox pictureBox1;
        private Button button4;
        private TextBox Factortext;
        private Button button3;
        private Label im;
        private Label re;
        private Label label2;
        private TextBox quality;
        private Button button5;
        private Label label3;
        private TextBox depthtext;
        private TextBox sweeptext;
        private Label label4;
        private TextBox zoomouttext;
        private Label label8;
        private System.ComponentModel.IContainer components;

        public frmMChild_Mandelbrot()
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
            double cr = 0;
            double ci = 0;
            bool run = true;

            try
            {
                refr = Double.Parse(real.Text);
                refi = Double.Parse(imaginary.Text);
                scaleamount = Double.Parse(scale.Text);
            }
            catch
            { run = false; }
            if (run)
            {
                double zi = 0;
                double zr = 0;

                double zit = 0;
                double zrt = 0;
                double zpm = 0;//modulus
                double zpa = 0;//angle
                double zrtch = 0;
                double zitch = 0;
                

                int iterations = 0;
                bool bail = false;
                factorrange = Double.Parse(Factortext.Text);
                depth = Int32.Parse(depthtext.Text);
                sweep = Int32.Parse(sweeptext.Text);

                //factorrange = factor;

                int qual = Int32.Parse(quality.Text);

                sweepwidth = (WIDTH / 2) / sweep;
                sweepheight = (HEIGHT / 2) / sweep;

                for (int i = -sweepwidth; i < sweepwidth; i += qual)
                    for (int j = -sweepheight; j < sweepheight; j += qual)
                    {

                        zi = 0;
                        zr = 0;
                        cr = refr + ((double)  i / (WIDTH/4)) / (scaleamount);//1920/4
                        ci = refi + ((double) -j / (WIDTH/4)) / (scaleamount);//1080/4
                        // z = z^2 + c
                        // z = (zr+zi) * (zr+zi) + cr + ci
                        // zr = zr*zr - zi*zi + cr
                        // zi = 2*zr*zi + ci
                        iterations = 0;
                        bail = false;
                        while ((zi * zi + zr * zr < 4) && !bail)
                        {
                            // z factor
                            /*zpm = Math.Sqrt(zi * zi + zr * zr);
                            zpa = Math.Atan2(zi, zr);
                            zpm = Math.Pow(zpm, factorrange);
                            zpa = zpa * factorrange;
                            zrt = zpm * Math.Cos(zpa) + cr;//r*cos(theta)
                            zit = zpm * Math.Sin(zpa) + ci;//r*sin(theta)
                            */
                            /*zrtch = zr * zr - zi * zi + cr;
                            zitch = 2 * zr * zi + ci;
                            if ((zrt > zrtch + 0.2) ||
                               (zrt < zrtch - 0.2))
                                zrt = zrt;*/

                            // z squared
                            zrt = zr * zr - zi * zi + cr;
                            zit = 2 * zr * zi + ci;

                            //c squared, z squared
                            //zrt = zr * zr - zi * zi + cr * cr - ci * ci;
                            //zit = 2 * zr * zi + 2 * cr * ci;

                            //z cubed
                            // z = (zr+zi) * (zr+zi) * (zr+zi) + cr + ci
                            // zr = zr*zr*zr - zi*zi*zi + 3*zr*zr*zi - 3*zr*zi*zi + cr
                            // zi = 2*zr*zi + ci
                            //zrt = zr*zr*zr - 3*zr*zi*zi + cr;
                            //zit = -zi*zi*zi + 3*zr*zr*zi + ci;
                            
                            zr = zrt;
                            zi = zit;
                            iterations++;
                            if (iterations >= depth - 1)
                            {
                                bail = true;
                                iterations = 0;
                            }
                        }
                        for (int q = 0; q < qual; q++)
                            for (int r = 0; r < qual; r++)
                                painting[i + (WIDTH/2) + q, j + (HEIGHT/2) + r] = iterations;
                    }
            }
        }
        
        private void dozoomout()
        {
            int photo = 1;
            outtofile = true;
            while(scaleamount > 1)
            {
                getpainting();
                drawmandelbrot();
                myBitmap.Save("C:\\Users\\Ken\\Pictures\\Mandelbrot\\Series\\photo" + photo.ToString() + ".png");
                //Invalidate();
                photo++;
                scaleamount /= Double.Parse(zoomouttext.Text);
                scale.Text = scaleamount.ToString();
            }
            outtofile = false;
        }

        private void dofactor()
        {
            int photo = 1;

            factorrange = Double.Parse(Factortext.Text);

            while (factorrange < 8)
            {
                getpainting();
                drawmandelbrot();
                myBitmap.Save("C:\\Users\\Ken\\Pictures\\Mandelbrot\\Series\\photob" + photo.ToString() + ".png");
                //Invalidate();
                photo++;
                factorrange += 0.01;
                Factortext.Text = factorrange.ToString();
            }
        }

        private void drawmandelbrot()
        {
            /*if (this.Width < this.Height)
                zoom = this.Width;
            else
                zoom = this.Height;*/
            //int qual = Int32.Parse(quality.Text);
            int r, g, b;
            
            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                {
                    //HsvToRgb(360 * painting[i,j]/depth, ((float) painting[i,j])/depth, 1, out r, out g, out b);

                    myBitmap.SetPixel(i, j,
                    //Color.FromArgb(painting[i, j], 16 * (painting[i, j] / 16), 16 * (painting[i, j] % 16)));
                    //Color.FromArgb(0, (painting[i, j] * 16) % 256, painting[i, j] % 256));
                    Color.FromArgb((3 * (painting[i, j] % 16) % 256), (painting[i, j]) % 256, (16 * (painting[i, j] % 16) % 256)));
                    //Color.FromArgb(r,g,b));

                    //Color.FromArgb((painting[i, j] / 16) % 256, (16 * (painting[i, j] / 16)) % 256, (16 * (painting[i, j] % 16) % 256)));
                }
            Graphics gr = Graphics.FromImage(myBitmap);

            RectangleF rectf = new RectangleF(0, 0, myBitmap.Width, myBitmap.Height);
            Rectangle rect = new Rectangle(0, 0, 580, 55);// myBitmap.Width, myBitmap.Height);
            //Rectangle rect = new Rectangle(0, 0, 540, 55);// myBitmap.Width, myBitmap.Height);
            Pen whitepen = new Pen(Brushes.WhiteSmoke);

            //g.SmoothingMode = SmoothingMode.AntiAlias;
            //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gr.FillRectangle(Brushes.OldLace, rect);
            //g.DrawString("Z(n+1) = Z(n)^" + factorrange.ToString("F") + " + C", new Font("Tahoma", 36), Brushes.Black, rectf);
            gr.DrawString("Magnification: " +  scaleamount.ToString("E2"), new Font("Tahoma", 36), Brushes.Black, rectf);

            gr.Flush();
            if(!outtofile) pictureBox1.Image = myBitmap;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMChild_Mandelbrot));
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
            this.real = new System.Windows.Forms.TextBox();
            this.imaginary = new System.Windows.Forms.TextBox();
            this.scale = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.realcoord = new System.Windows.Forms.Label();
            this.imaginarycoord = new System.Windows.Forms.Label();
            this.imaginary4 = new System.Windows.Forms.Label();
            this.real4 = new System.Windows.Forms.Label();
            this.imaginarytrans = new System.Windows.Forms.Label();
            this.realtrans = new System.Windows.Forms.Label();
            this.zoomin = new System.Windows.Forms.Button();
            this.zoomout = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.depthtext = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.quality = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Factor = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button4 = new System.Windows.Forms.Button();
            this.Factortext = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.im = new System.Windows.Forms.Label();
            this.re = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.sweeptext = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.zoomouttext = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // real
            // 
            this.real.Location = new System.Drawing.Point(47, 50);
            this.real.Name = "real";
            this.real.Size = new System.Drawing.Size(167, 20);
            this.real.TabIndex = 98;
            this.real.Text = "0";
            // 
            // imaginary
            // 
            this.imaginary.Location = new System.Drawing.Point(47, 73);
            this.imaginary.Name = "imaginary";
            this.imaginary.Size = new System.Drawing.Size(167, 20);
            this.imaginary.TabIndex = 99;
            this.imaginary.Text = "0";
            // 
            // scale
            // 
            this.scale.Location = new System.Drawing.Point(47, 96);
            this.scale.Name = "scale";
            this.scale.Size = new System.Drawing.Size(167, 20);
            this.scale.TabIndex = 100;
            this.scale.Text = "1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(849, 251);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 20);
            this.button1.TabIndex = 101;
            this.button1.Text = "Show/Hide";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 122);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 20);
            this.button2.TabIndex = 102;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // realcoord
            // 
            this.realcoord.AutoSize = true;
            this.realcoord.Location = new System.Drawing.Point(1003, 346);
            this.realcoord.Name = "realcoord";
            this.realcoord.Size = new System.Drawing.Size(16, 13);
            this.realcoord.TabIndex = 103;
            this.realcoord.Text = "re";
            this.realcoord.Click += new System.EventHandler(this.realcoord_Click);
            // 
            // imaginarycoord
            // 
            this.imaginarycoord.AutoSize = true;
            this.imaginarycoord.Location = new System.Drawing.Point(1003, 359);
            this.imaginarycoord.Name = "imaginarycoord";
            this.imaginarycoord.Size = new System.Drawing.Size(17, 13);
            this.imaginarycoord.TabIndex = 104;
            this.imaginarycoord.Text = "im";
            // 
            // imaginary4
            // 
            this.imaginary4.AutoSize = true;
            this.imaginary4.Location = new System.Drawing.Point(1081, 359);
            this.imaginary4.Name = "imaginary4";
            this.imaginary4.Size = new System.Drawing.Size(17, 13);
            this.imaginary4.TabIndex = 106;
            this.imaginary4.Text = "im";
            // 
            // real4
            // 
            this.real4.AutoSize = true;
            this.real4.Location = new System.Drawing.Point(1081, 346);
            this.real4.Name = "real4";
            this.real4.Size = new System.Drawing.Size(16, 13);
            this.real4.TabIndex = 105;
            this.real4.Text = "re";
            // 
            // imaginarytrans
            // 
            this.imaginarytrans.AutoSize = true;
            this.imaginarytrans.Location = new System.Drawing.Point(1162, 359);
            this.imaginarytrans.Name = "imaginarytrans";
            this.imaginarytrans.Size = new System.Drawing.Size(17, 13);
            this.imaginarytrans.TabIndex = 108;
            this.imaginarytrans.Text = "im";
            // 
            // realtrans
            // 
            this.realtrans.AutoSize = true;
            this.realtrans.Location = new System.Drawing.Point(1162, 346);
            this.realtrans.Name = "realtrans";
            this.realtrans.Size = new System.Drawing.Size(16, 13);
            this.realtrans.TabIndex = 107;
            this.realtrans.Text = "re";
            // 
            // zoomin
            // 
            this.zoomin.Location = new System.Drawing.Point(10, 21);
            this.zoomin.Name = "zoomin";
            this.zoomin.Size = new System.Drawing.Size(36, 20);
            this.zoomin.TabIndex = 109;
            this.zoomin.Text = "+";
            this.zoomin.UseVisualStyleBackColor = true;
            this.zoomin.Click += new System.EventHandler(this.button3_Click);
            // 
            // zoomout
            // 
            this.zoomout.Location = new System.Drawing.Point(48, 21);
            this.zoomout.Name = "zoomout";
            this.zoomout.Size = new System.Drawing.Size(35, 20);
            this.zoomout.TabIndex = 110;
            this.zoomout.Text = "-";
            this.zoomout.UseVisualStyleBackColor = true;
            this.zoomout.Click += new System.EventHandler(this.zoomout_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 115;
            this.label5.Text = "Zoom";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 116;
            this.label6.Text = "Re";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 13);
            this.label7.TabIndex = 117;
            this.label7.Text = "Im";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(9, 198);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(208, 73);
            this.richTextBox1.TabIndex = 118;
            this.richTextBox1.Text = "- Click on the image, the point that is clicked will be the centre of the new ima" +
    "ge.\n- Zoom in and out, using + and - buttons.\n(Image takes about 10 secs to rend" +
    "er)\n(Maximum zoom 10^13)";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.depthtext);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.quality);
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.zoomout);
            this.groupBox1.Controls.Add(this.zoomin);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.scale);
            this.groupBox1.Controls.Add(this.imaginary);
            this.groupBox1.Controls.Add(this.real);
            this.groupBox1.Location = new System.Drawing.Point(738, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 280);
            this.groupBox1.TabIndex = 119;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 128;
            this.label3.Text = "Depth";
            // 
            // depthtext
            // 
            this.depthtext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.depthtext.Location = new System.Drawing.Point(178, 149);
            this.depthtext.Name = "depthtext";
            this.depthtext.Size = new System.Drawing.Size(36, 20);
            this.depthtext.TabIndex = 127;
            this.depthtext.Text = "360";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 126;
            this.label2.Text = "Quality (1=best)";
            // 
            // quality
            // 
            this.quality.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.quality.Location = new System.Drawing.Point(178, 123);
            this.quality.Name = "quality";
            this.quality.Size = new System.Drawing.Size(36, 20);
            this.quality.TabIndex = 125;
            this.quality.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(846, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 120;
            this.label1.Text = "Factor";
            // 
            // Factor
            // 
            this.Factor.Location = new System.Drawing.Point(884, 248);
            this.Factor.Name = "Factor";
            this.Factor.Size = new System.Drawing.Size(36, 20);
            this.Factor.TabIndex = 119;
            this.Factor.Text = "2";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(2, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(960, 540);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 122;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDoubleClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(738, 315);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 123;
            this.button4.Text = "Save img";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Factortext
            // 
            this.Factortext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Factortext.Location = new System.Drawing.Point(785, 292);
            this.Factortext.Name = "Factortext";
            this.Factortext.Size = new System.Drawing.Size(36, 20);
            this.Factortext.TabIndex = 124;
            this.Factortext.Text = "2.99";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(738, 344);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 125;
            this.button3.Text = "Do series";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_2);
            // 
            // im
            // 
            this.im.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.im.AutoSize = true;
            this.im.Location = new System.Drawing.Point(746, 424);
            this.im.Name = "im";
            this.im.Size = new System.Drawing.Size(18, 13);
            this.im.TabIndex = 127;
            this.im.Text = "Im";
            // 
            // re
            // 
            this.re.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.re.AutoSize = true;
            this.re.Location = new System.Drawing.Point(746, 400);
            this.re.Name = "re";
            this.re.Size = new System.Drawing.Size(21, 13);
            this.re.TabIndex = 126;
            this.re.Text = "Re";
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(738, 373);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 128;
            this.button5.Text = "Do factor series";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // sweeptext
            // 
            this.sweeptext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sweeptext.Location = new System.Drawing.Point(905, 317);
            this.sweeptext.Name = "sweeptext";
            this.sweeptext.Size = new System.Drawing.Size(50, 20);
            this.sweeptext.TabIndex = 129;
            this.sweeptext.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(833, 317);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 130;
            this.label4.Text = "Sweep";
            // 
            // zoomouttext
            // 
            this.zoomouttext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomouttext.Location = new System.Drawing.Point(905, 343);
            this.zoomouttext.Name = "zoomouttext";
            this.zoomouttext.Size = new System.Drawing.Size(50, 20);
            this.zoomouttext.TabIndex = 131;
            this.zoomouttext.Text = "1.1";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(833, 343);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 132;
            this.label8.Text = "zoomout rate";
            // 
            // frmMChild_Mandelbrot
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(964, 543);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.zoomouttext);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.sweeptext);
            this.Controls.Add(this.im);
            this.Controls.Add(this.re);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Factortext);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.imaginarytrans);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.realtrans);
            this.Controls.Add(this.imaginary4);
            this.Controls.Add(this.real4);
            this.Controls.Add(this.Factor);
            this.Controls.Add(this.imaginarycoord);
            this.Controls.Add(this.IncrementalBoardEvaluation);
            this.Controls.Add(this.realcoord);
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
            this.Name = "frmMChild_Mandelbrot";
            this.Text = "Mandelbrot Set";
            this.Load += new System.EventHandler(this.frmMChild_Chess_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMChild_Chess_Paint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.frmMChild_Chess_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMChild_Chess_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMChild_Chess_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmMChild_Chess_MouseUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
            try
            {
                real.Text = ((refr + (((e.X * (1920 / zoom)) - 960) / 480) / scaleamount)).ToString();
                imaginary.Text = ((refi + (((e.Y * (1080 / zoom)) - 540) / -270) / scaleamount)).ToString();
                getpainting();
                Invalidate();
            }
            catch { MessageBox.Show("Mousedown", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void frmMChild_Chess_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (!autoplay)
                {
                    mouseposition.X = (int)e.X; mouseposition.Y = (int)e.Y;
                    realcoord.Text = ((int)(e.X * (1920 / zoom))).ToString();
                    imaginarycoord.Text = ((int)(e.Y * (1080 / zoom))).ToString();

                    real4.Text = ((((e.X * (1920 / zoom)) - 960) / 480) / scaleamount).ToString();
                    imaginary4.Text = ((((e.Y * (1080 / zoom)) - 540) / -270) / scaleamount).ToString();

                    realtrans.Text = (refr + (((e.X * (1920 / zoom)) - 960) / 480) / scaleamount).ToString();
                    imaginarytrans.Text = (refi + (((e.Y * (1080 / zoom)) - 540) / -270) / scaleamount).ToString();

                }
            }
            catch { MessageBox.Show("Mouse Move", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void frmMChild_Chess_MouseUp(object sender, MouseEventArgs e)
        {

            Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (show == true) show = false;
            else show = true;

            Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getpainting();
            Invalidate();
        }

        private void realcoord_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //scaleamount = (float) (int.Parse(scale.Text));
            scaleamount *= 10f;
            scale.Text = scaleamount.ToString();
            getpainting();
            Invalidate();
        }

        private void zoomout_Click(object sender, EventArgs e)
        {
            //scaleamount = (float)(int.Parse(scale.Text));
            scaleamount *= 1 / 10f;
            scale.Text = scaleamount.ToString();
            getpainting();
            Invalidate();
        }

        private void frmMChild_Chess_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            real.Text = ((refr + (((e.X * (1920 / zoom)) - 960) / 480) / scaleamount)).ToString();
            imaginary.Text = ((refi + (((e.Y * (1080 / zoom)) - 540) / -270) / scaleamount)).ToString();
            getpainting();
            scaleamount *= 10f;
            scale.Text = scaleamount.ToString();
            getpainting();
            Invalidate();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //read image
            Bitmap bmp = new Bitmap("C:\\Chess\\trailer512b.png");

            //load image in picturebox
            pictureBox1.Image = bmp;

            //write image
            bmp.Save("C:\\Chess\\trailer512a.png");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //read imageyourtext
            //Bitmap bmp = new Bitmap(GetFormImageWithoutBorders(this));

            //load image in picturebox
            pictureBox1.Image = myBitmap;

            //write image
            myBitmap.Save("C:\\Chess\\background.png");
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            /*
            try
            {
                double width = pictureBox1.Width;
                double height = pictureBox1.Height;  //960 equates to 2, 0 equates to -2

                real.Text = (refr + ((4 * (e.X - (width/2)) / width) / scaleamount)).ToString();
                imaginary.Text = (refi + ((4 * ((height/2) - e.Y) / width) / scaleamount)).ToString();

                //imaginary.Text = ((refi + (((e.Y * (1080 / zoom)) - 540) / -270) / scaleamount)).ToString();
                getpainting();
                Invalidate();
            }
            catch { MessageBox.Show("Mousedown", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            */
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            dozoomout();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                re.Text = e.X.ToString() + "/" + pictureBox1.Width.ToString();
                im.Text = e.Y.ToString() + "/" + pictureBox1.Height.ToString();                
                Invalidate();
            }
            catch { MessageBox.Show("Mousedown", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            dofactor();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {   
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {                
                double width = pictureBox1.Width;
                double height = pictureBox1.Height;  //960 equates to 2, 0 equates to -2

                real.Text = (refr + ((4 * (e.X - (width / 2)) / width) / scaleamount)).ToString();
                imaginary.Text = (refi + ((4 * ((height / 2) - e.Y) / width) / scaleamount)).ToString();

                scaleamount *= 10f;
                scale.Text = scaleamount.ToString();

                //imaginary.Text = ((refi + (((e.Y * (1080 / zoom)) - 540) / -270) / scaleamount)).ToString();
                getpainting();
                Invalidate();
            }
            catch { MessageBox.Show("Mousedown", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }


        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

            try
            {
                double width = pictureBox1.Width;
                double height = pictureBox1.Height;  //960 equates to 2, 0 equates to -2

                real.Text = (refr + ((4 * (e.X - (width / 2)) / width) / scaleamount)).ToString();
                imaginary.Text = (refi + ((4 * ((height / 2) - e.Y) / width) / scaleamount)).ToString();

                //imaginary.Text = ((refi + (((e.Y * (1080 / zoom)) - 540) / -270) / scaleamount)).ToString();
                getpainting();
                Invalidate();
            }
            catch { MessageBox.Show("Mousedown", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            
        }

        /// <summary>
        /// Convert HSV to RGB
        /// h is from 0-360
        /// s,v values are 0-1
        /// r,g,b values are 0-255
        /// Based upon http://ilab.usc.edu/wiki/index.php/HSV_And_H2SV_Color_Space#HSV_Transformation_C_.2F_C.2B.2B_Code_2
        /// </summary>
        void HsvToRgb(double h, double S, double V, out int r, out int g, out int b)
        {
            // ######################################################################
            // T. Nathan Mundhenk
            // mundhenk@usc.edu
            // C/C++ Macro HSV to RGB

            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((int)(R * 255.0));
            g = Clamp((int)(G * 255.0));
            b = Clamp((int)(B * 255.0));
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }

    }
}