using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Logic_Navigator
{
    public partial class objfrmComms : Form
    {
        public string commport = "test";
        public string baudrate = "9600";

        public objfrmComms()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            commport = comboBox1.Text;
            baudrate = comboBox2.Text;
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
