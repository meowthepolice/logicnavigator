using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Logic_Navigator
{
    public class frmMChild_Monitor : System.Windows.Forms.Form
    {
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
        public ArrayList SimInputs = new ArrayList();
        public bool recentChange = false;
        public bool block = false;
        public string searchString = "";

        private int X;
        private int Y;
        private bool Paused = false;

        public frmMChild_Monitor(ArrayList interlockingOldPointer, ArrayList interlockingNewPointer, Font drawFont)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call

            interlockingOld = interlockingOldPointer;
            interlockingNew = interlockingNewPointer;
            //timersOld = timersOldPointer;
            //timersNew = timersNewPointer;
            //formatRungList();
            //populateInputList("");

            drawFnt = drawFont;

        }

        private IPEndPoint listenerIP;// = new IPEndPoint(IPAddress.Loopback, 4201);

        public void DemonstrateUdpClientBug()
        {
            IPAddress send_to_address = IPAddress.Parse(IPAddressText.Text);
            listenerIP = new IPEndPoint(IPAddress.Loopback, 4201);
            //Console.WriteLine(); Console.WriteLine("UdpClient.Receive Bug");

            // Setup listener socket
            UdpClient listener = new UdpClient(listenerIP); // Setup sending socket
            UdpClient sender = new UdpClient();
            sender.Connect(listenerIP);

            // Sending  three datagrams to the listener 
            Send(sender, "One");
            Send(sender, "Two");
            Send(sender, "Three");
            Send(sender, "Four");

            // Now receive the three datagrams from the listener
            Receive(listener);
            Receive(listener);
            Receive(listener);
            Receive(listener);

            listener.Close();
            sender.Close();
        }

        public void SendDemonstrateUdpClientBug()
        {
            IPAddress send_to_address = IPAddress.Parse(IPAddressText.Text);
            listenerIP = new IPEndPoint(send_to_address, 4201);
            //Console.WriteLine(); Console.WriteLine("UdpClient.Receive Bug");

            // Setup listener socket
            //UdpClient listener = new UdpClient(listenerIP); // Setup sending socket
            UdpClient sender = new UdpClient();
            sender.Connect(listenerIP);

            // Sending  three datagrams to the listener 
            Send(sender, "One");
            Send(sender, "Two");
            Send(sender, "Three");
            Send(sender, "Four");

            // Now receive the three datagrams from the listener
            //Receive(listener);
            //Receive(listener);
            //Receive(listener);
           // Receive(listener);

            //listener.Close();
            sender.Close();
        }


        public void ReceiveDemonstrateUdpClientBug()
        {
            IPAddress send_to_address = IPAddress.Parse(IPAddressText.Text);
            listenerIP = new IPEndPoint(send_to_address, 4201);
            //Console.WriteLine(); Console.WriteLine("UdpClient.Receive Bug");

            // Setup listener socket
            UdpClient listener = new UdpClient(listenerIP); // Setup sending socket
            //UdpClient sender = new UdpClient();
            //sender.Connect(listenerIP);

            // Sending  three datagrams to the listener 
            //Send(sender, "One");
            //Send(sender, "Two");
            //Send(sender, "Three");
            //Send(sender, "Four");

            // Now receive the three datagrams from the listener
            Receive(listener);
            Receive(listener);
            Receive(listener);
            Receive(listener);

            listener.Close();
           // sender.Close();
        }


        void Send(UdpClient sender, string s)
        {
            byte[] dgram = Encoding.ASCII.GetBytes(s);
            Console.WriteLine("Sending '" + s + "' (" + dgram.Length.ToString()
                               + " bytes)");
            sender.Send(dgram, dgram.Length);
        }

        void Receive(UdpClient listener)
        {
            IPEndPoint from = new IPEndPoint(IPAddress.Any, 0);
            byte[] dgram = listener.Receive(ref from);
            string s = Encoding.ASCII.GetString(dgram, 0, dgram.Length);
            COS.Text += s;
            //Console.WriteLine
            //(
            //  "Received {0} bytes, s = '{1}', s.Length = {2}",
            //  dgram.Length, s, s.Length
            //);
        }




        public void AddChange(string message)
        {
            if ((message.LastIndexOf(this.IPAddressText.Text) != -1) && !Paused)
                COS.AppendText(message);
        }

        public bool MessageRelevant(string message)
        {
            if ((message.LastIndexOf(this.IPAddressText.Text) != -1) && !Paused)
            {
                //COS.AppendText(message);
                return (SoundcheckBox.Checked);
            }
            else return (false);
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.IPAddressText = new System.Windows.Forms.TextBox();
            this.COS = new System.Windows.Forms.TextBox();
            this.Pause = new System.Windows.Forms.Button();
            this.SoundcheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(846, 526);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 29;
            // 
            // IPAddressText
            // 
            this.IPAddressText.Location = new System.Drawing.Point(12, 4);
            this.IPAddressText.Name = "IPAddressText";
            this.IPAddressText.Size = new System.Drawing.Size(126, 20);
            this.IPAddressText.TabIndex = 30;
            this.IPAddressText.Text = "192.168.0.17";
            this.IPAddressText.TextChanged += new System.EventHandler(this.searchString1_TextChanged_1);
            // 
            // COS
            // 
            this.COS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.COS.Location = new System.Drawing.Point(-2, 85);
            this.COS.Multiline = true;
            this.COS.Name = "COS";
            this.COS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.COS.Size = new System.Drawing.Size(390, 527);
            this.COS.TabIndex = 31;
            // 
            // Pause
            // 
            this.Pause.Location = new System.Drawing.Point(144, 1);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(111, 40);
            this.Pause.TabIndex = 32;
            this.Pause.Text = "Loopback UDP Packets";
            this.Pause.UseVisualStyleBackColor = true;
            this.Pause.Click += new System.EventHandler(this.Pause_Click);
            // 
            // SoundcheckBox
            // 
            this.SoundcheckBox.AutoSize = true;
            this.SoundcheckBox.Location = new System.Drawing.Point(81, 42);
            this.SoundcheckBox.Name = "SoundcheckBox";
            this.SoundcheckBox.Size = new System.Drawing.Size(57, 17);
            this.SoundcheckBox.TabIndex = 33;
            this.SoundcheckBox.Text = "Sound";
            this.SoundcheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(261, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 23);
            this.button1.TabIndex = 34;
            this.button1.Text = "Receive UDP Packets";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(261, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 23);
            this.button2.TabIndex = 35;
            this.button2.Text = "Send UDP Packets";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // frmMChild_Monitor
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(388, 615);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SoundcheckBox);
            this.Controls.Add(this.Pause);
            this.Controls.Add(this.COS);
            this.Controls.Add(this.IPAddressText);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmMChild_Monitor";
            this.Text = "NCDM Monitor";
            this.Load += new System.EventHandler(this.frmMChild_Log_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void formatRungList()
        {

        }

        private void populateInputList(string searchString)
        {
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Input";
            dataGridView1.Columns[0].Width = 110;
            dataGridView1.Columns[1].Name = "Status";
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Name = "";//Cardname   
            dataGridView1.Columns[2].Visible = false;//Cardname   
            getInputs(searchString);
        }

        private void getInputs(string searchString)
        {
            int rungNumber = 0;
            dataRungCount = 0;
            string name = "";
            string[] row = new string[] { "1", "", "" };

            ArrayList Coils = new ArrayList();
            ArrayList Inputs = new ArrayList();
            try
            {
                getRungs(Coils, interlockingNew);
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
                                    row = new string[] { contact.name.ToString(), "Low", "Card" };
                                    dataGridView1.Rows.Add(row);
                                    Inputs.Add(contact.name);
                                }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error populating input list (rung number:" + rungNumber.ToString() + ", " + name.ToString() + ")", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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
            string input = (string)IPAddressText.Text;
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
            IPAddressText.Text = searchString;
        }

        private void ShowRungWindow()
        {
            int newIndex = findRung(interlockingNew, rungName);
            int oldIndex = findRung(interlockingOld, rungName);
            if (newIndex == -1)
            {
                frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, oldIndex - 1,
                    oldIndex - 1, 0.75F, "All Old", drawFnt, "", false, true, Color.Blue, Color.Red);
                objfrmMChild.Size = new Size(700, 400);
                objfrmMChild.Location = new System.Drawing.Point(1, 1);
                objfrmMChild.Text = rungName;
                objfrmMChild.MdiParent = this.MdiParent;
                objfrmMChild.Show();
            }
            else
            {
                if (oldIndex == -1)
                {
                    frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, newIndex - 1,
                        newIndex - 1, 0.75F, "All New", drawFnt, "", false, true, Color.Blue, Color.Red);
                    objfrmMChild.Size = new Size(700, 400);
                    objfrmMChild.Location = new System.Drawing.Point(1, 1);
                    objfrmMChild.Text = rungName;
                    objfrmMChild.MdiParent = this.MdiParent;
                    objfrmMChild.Show();
                }
                else
                {
                    frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, oldIndex - 1,
                        newIndex - 1, 0.75F, "Normal", drawFnt, "", false, true, Color.Blue, Color.Red);
                    objfrmMChild.Size = new Size(700, 400);
                    objfrmMChild.Location = new System.Drawing.Point(1, 1);
                    objfrmMChild.Text = rungName;
                    objfrmMChild.MdiParent = this.MdiParent;
                    objfrmMChild.Show();
                }
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            IPAddressText.Text = "";
            while (dataGridView1.Rows.Count > 1)
                if (!dataGridView1.Rows[0].IsNewRow)
                    dataGridView1.Rows.RemoveAt(0);
            getInputs("");
            CopySimInputsToDataGrid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView1.CurrentCell.RowIndex;
            string inputstate = (string)dataGridView1.Rows[row].Cells[1].Value;
            recentChange = true;
            if (dataGridView1.Rows[row].Cells[0].Value != null)
            {
                if (inputstate == "Low")
                {
                    dataGridView1.Rows[row].Cells[1].Value = "High";
                    dataGridView1.Rows[row].DefaultCellStyle.ForeColor = Color.Red;
                }
                if (inputstate == "High")
                {
                    dataGridView1.Rows[row].Cells[1].Value = "Low";
                    dataGridView1.Rows[row].DefaultCellStyle.ForeColor = Color.Blue;
                }
                UpdateSimInputs();
            }
        }

        private void UpdateSimInputs()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if ((string)dataGridView1.Rows[i].Cells[1].Value == "High")
                    AddToSimInputs((string)dataGridView1.Rows[i].Cells[0].Value, true/*Is High*/);
                if ((string)dataGridView1.Rows[i].Cells[1].Value == "Low")
                    AddToSimInputs((string)dataGridView1.Rows[i].Cells[0].Value, false/*Is Low*/);
            }
        }

        private void AddToSimInputs(string input, bool IsHigh)
        {
            bool found = false;
            for (int i = 0; i < SimInputs.Count; i++)
                if (SimInputs[i].ToString() == input)
                {
                    if (!IsHigh) //Input went low so take it out of the list
                        SimInputs.RemoveAt(i);
                    found = true;
                }
            if (IsHigh && !found) SimInputs.Add(input);
        }

        public void UpdateSimInputsList(ArrayList extSimInputs) //Reverse direction to UpdateSimInputs
        {
            DuplicateList(extSimInputs, SimInputs);
            CopySimInputsToDataGrid();
        }

        private void CopySimInputsToDataGrid()
        {
            bool found = false;
            for (int j = 0; j < dataGridView1.RowCount; j++)
            {
                found = false;
                for (int i = 0; i < SimInputs.Count; i++)
                {
                    if ((string)dataGridView1.Rows[j].Cells[0].Value == SimInputs[i].ToString())
                    {
                        found = true;
                        dataGridView1.Rows[j].Cells[1].Value = "High";
                        dataGridView1.Rows[j].DefaultCellStyle.ForeColor = Color.Red;
                    }
                }
                if (!found && (dataGridView1.Rows[j].Cells[0].Value != null))
                {
                    dataGridView1.Rows[j].Cells[1].Value = "Low";
                    dataGridView1.Rows[j].DefaultCellStyle.ForeColor = Color.Blue;
                }
            }
        }

        private void DuplicateList(ArrayList original, ArrayList duplicate)
        {
            duplicate.Clear();
            for (int i = 0; i < original.Count; i++)
                duplicate.Add(original[i].ToString());
        }

        private void button3_Click(object sender, EventArgs e) //
        {
            recentChange = true;
            for (int row = 0; row < dataGridView1.RowCount - 1; row++)
            {
                dataGridView1.Rows[row].Cells[1].Value = "Low";
                dataGridView1.Rows[row].DefaultCellStyle.ForeColor = Color.Blue;
            }
            UpdateSimInputs();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            recentChange = true;
            for (int row = 0; row < dataGridView1.RowCount - 1; row++)
            {
                dataGridView1.Rows[row].Cells[1].Value = "High";
                dataGridView1.Rows[row].DefaultCellStyle.ForeColor = Color.Red;
            }
            UpdateSimInputs();
        }
        private IContainer components;
        private DataGridView dataGridView1;
        public TextBox IPAddressText;
        private TextBox COS;
        private Button Pause;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Pause_Click(object sender, EventArgs e)
        {

                        DemonstrateUdpClientBug();
            /*
            if (Paused)
            {
                Paused = false;
                COS.AppendText("Unpaused - " + DateTime.Now.ToLongTimeString() + "\r\n");
            }
            else
            {
                Paused = true;
                COS.AppendText("Paused - " + DateTime.Now.ToLongTimeString() + "\r\n");
            }
            */
        }

        private void searchString1_TextChanged_1(object sender, EventArgs e)
        {

            //string input = (string)searchString1.Text;
            //searchString = input;
        }

        private void frmMChild_Log_Load(object sender, EventArgs e)
        {

        }

        private CheckBox SoundcheckBox;
        private Button button1;
        private Button button2;

        private void button2_Click_1(object sender, EventArgs e)
        {
            SendDemonstrateUdpClientBug();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ReceiveDemonstrateUdpClientBug();
        }
    }
}
