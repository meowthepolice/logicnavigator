using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Globalization;


namespace Logic_Navigator
{
    public class frmMChild_SimInputs : System.Windows.Forms.Form
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
        Color HighColor, LowColor;

        private int X;
        private int Y;

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;

        public frmMChild_SimInputs(ArrayList interlockingOldPointer, ArrayList interlockingNewPointer, ArrayList timersOldPointer, 
                ArrayList timersNewPointer, Font drawFont, Color HighColorInput, Color LowColorInput)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call

            interlockingOld = interlockingOldPointer;
            interlockingNew = interlockingNewPointer;
            timersOld = timersOldPointer;
            timersNew = timersNewPointer;
            formatRungList();
            populateInputList("");
            HighColor = HighColorInput;
            LowColor = LowColorInput;
            drawFnt = drawFont;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMChild_SimInputs));
            this.searchString1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // searchString1
            // 
            this.searchString1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.searchString1.Location = new System.Drawing.Point(46, 5);
            this.searchString1.Name = "searchString1";
            this.searchString1.Size = new System.Drawing.Size(77, 20);
            this.searchString1.TabIndex = 15;
            this.searchString1.TextChanged += new System.EventHandler(this.searchString1_TextChanged);
            // 
            // label3
            // 
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(-1, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "Search:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(119, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 24);
            this.button1.TabIndex = 26;
            this.button1.Text = "Clear";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.dataGridView1.Location = new System.Drawing.Point(2, 31);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.SeaGreen;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Size = new System.Drawing.Size(205, 313);
            this.dataGridView1.TabIndex = 27;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(167, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 24);
            this.button2.TabIndex = 28;
            this.button2.Text = "All High";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(228, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(52, 24);
            this.button3.TabIndex = 29;
            this.button3.Text = "All Low";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            this.contextMenuStrip1.Click += new System.EventHandler(this.contextMenuStrip1_Click);
            // 
            // frmMChild_SimInputs
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(211, 346);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.searchString1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMChild_SimInputs";
            this.Text = "Inputs";
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
            for(int i = 0; i < list.Count; i++)            
                if (name == (string) list[i]) return true;
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

        /*
        private void ShowRungWindow()
        {
            int newIndex = findRung(interlockingNew, rungName);
            int oldIndex = findRung(interlockingOld, rungName);
            if (newIndex == -1)
            {
                frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, oldIndex - 1,
                    oldIndex - 1, 0.75F, "All Old", drawFnt, "", false, true, HighColor, LowColor);
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
                        newIndex - 1, 0.75F, "All New", drawFnt, "", false, true, HighColor, LowColor);
                    objfrmMChild.Size = new Size(700, 400);
                    objfrmMChild.Location = new System.Drawing.Point(1, 1);
                    objfrmMChild.Text = rungName;
                    objfrmMChild.MdiParent = this.MdiParent;
                    objfrmMChild.Show();
                }
                else
                {
                    frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, oldIndex - 1,
                        newIndex - 1, 0.75F, "Normal", drawFnt, "", false, true, HighColor, LowColor);
                    objfrmMChild.Size = new Size(700, 400);
                    objfrmMChild.Location = new System.Drawing.Point(1, 1);
                    objfrmMChild.Text = rungName;
                    objfrmMChild.MdiParent = this.MdiParent;
                    objfrmMChild.Show();
                }
            }
        }
        */
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
            string inputstate = (string) dataGridView1.Rows[row].Cells[1].Value;            
            recentChange = true;
            if (dataGridView1.Rows[row].Cells[0].Value != null)
            {
                if (inputstate == "Low")
                {
                    dataGridView1.Rows[row].Cells[1].Value = "High";
                    //dataGridView1.Rows[row].DefaultCellStyle.ForeColor = Color.Red;
                    dataGridView1.Rows[row].DefaultCellStyle.ForeColor = HighColor;
                }
                if (inputstate == "High")
                {
                    dataGridView1.Rows[row].Cells[1].Value = "Low";
                    //dataGridView1.Rows[row].DefaultCellStyle.ForeColor = Color.Blue;
                    dataGridView1.Rows[row].DefaultCellStyle.ForeColor = LowColor;
                }
                UpdateSimInputs();
            }

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
                        dataGridView1.Rows[j].DefaultCellStyle.ForeColor = HighColor;
                    }
                }
                if (!found && (dataGridView1.Rows[j].Cells[0].Value != null))
                {
                    dataGridView1.Rows[j].Cells[1].Value = "Low";
                    dataGridView1.Rows[j].DefaultCellStyle.ForeColor = Color.Red;
                }
            }
        }

        private void DuplicateList(ArrayList original, ArrayList duplicate)
        {
            duplicate.Clear();
            for (int i = 0; i < original.Count; i++)
                duplicate.Add(original[i].ToString());
        }

        private Button button2;
        private Button button3;

        private void button3_Click(object sender, EventArgs e) //
        {
            recentChange = true;
            for (int row = 0; row < dataGridView1.RowCount-1; row++)
            {
                dataGridView1.Rows[row].Cells[1].Value = "Low";
                dataGridView1.Rows[row].DefaultCellStyle.ForeColor = LowColor;
            }
            UpdateSimInputs();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            recentChange = true;
            for (int row = 0; row < dataGridView1.RowCount-1; row++)
            {
                dataGridView1.Rows[row].Cells[1].Value = "High";
                dataGridView1.Rows[row].DefaultCellStyle.ForeColor = HighColor;
            }
            UpdateSimInputs();
        }
        private IContainer components;
        public TextBox searchString1;
        private ContextMenuStrip contextMenuStrip1;

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int menulength = contextMenuStrip1.Items.Count;
            if (contextMenuStrip1.Items.Count > 1)
                for (int i = 0; i < menulength; i++)
                    contextMenuStrip1.Items.RemoveAt(0);
            int row = dataGridView1.CurrentCell.RowIndex;
            if (dataGridView1.Rows[row].Cells[0].Value != null)
            {
                string contactname = dataGridView1.Rows[row].Cells[0].Value.ToString();
                contextMenuStrip1.Items.Add(contactname + " used in:");
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
            }
        }
        
        private void ShowRungWindow()
        {
            int newIndex = findRung(interlockingNew, rungName);
            int oldIndex = findRung(interlockingOld, rungName);
            if (newIndex == -1)
            {
                frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, oldIndex - 1,
                    oldIndex - 1, 0.75F, "All Old", drawFnt, "", false, true, HighColor, LowColor);
                objfrmMChild.Size = new Size(objfrmMChild.RecommendedWidthofWindow(oldIndex - 1), objfrmMChild.RecommendedHeightofWindow(oldIndex - 1));
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
                        newIndex - 1, 0.75F, "All New", drawFnt, "", false, true, HighColor, LowColor);
                    objfrmMChild.Size = new Size(objfrmMChild.RecommendedWidthofWindow(newIndex - 1), objfrmMChild.RecommendedHeightofWindow(newIndex - 1));
                    objfrmMChild.Location = new System.Drawing.Point(1, 1);
                    objfrmMChild.Text = rungName;
                    objfrmMChild.MdiParent = this.MdiParent;
                    objfrmMChild.Show();
                }
                else
                {
                    frmMChild objfrmMChild = new frmMChild(interlockingOld, interlockingNew, timersOld, timersNew, oldIndex - 1,
                        newIndex - 1, 0.75F, "Normal", drawFnt, "", false, true, HighColor, LowColor);
                    objfrmMChild.Size = new Size(objfrmMChild.RecommendedWidthofWindow(newIndex - 1), objfrmMChild.RecommendedHeightofWindow(newIndex - 1));
                    objfrmMChild.Location = new System.Drawing.Point(1, 1);
                    objfrmMChild.Text = rungName;
                    objfrmMChild.MdiParent = this.MdiParent;
                    objfrmMChild.Show();
                }
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            rungName = item.ToString();
            ShowRungWindow();
        }
    }
}
