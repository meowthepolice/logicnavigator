using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Logic_Navigator
{
    public partial class Advance_Pawn : Form
    {
        int advance_color = 0;
        public int piece = 0;

        public const int White = 0;
        public const int Black = 1;

        private const int Black_King = 1;
        private const int Black_Queen = 2;
        private const int Black_Rook = 3;
        private const int Black_Bishop = 4;
        private const int Black_Knight = 5;
        private const int Black_Pawn = 6;

        private const int White_King = 7;
        private const int White_Queen = 8;
        private const int White_Rook = 9;
        private const int White_Bishop = 10;
        private const int White_Knight = 11;
        private const int White_Pawn = 12;

        public Advance_Pawn(int color)
        {            
            InitializeComponent();
            advance_color = color;
        }

        private void Queen_Click(object sender, EventArgs e)
        {
            if (advance_color == White) piece = White_Queen; 
            if (advance_color == Black) piece = Black_Queen; 
            this.Close();
        }

        private void Knight_Click(object sender, EventArgs e)
        {
            if (advance_color == White) piece = White_Knight;
            if (advance_color == Black) piece = Black_Knight; 
            this.Close();
        }

        private void Bishop_Click(object sender, EventArgs e)
        {
            if (advance_color == White) piece = White_Bishop;
            if (advance_color == Black) piece = Black_Bishop;
            this.Close();
        }

        private void Rook_Click(object sender, EventArgs e)
        {
            if (advance_color == White) piece = White_Rook; 
            if (advance_color == Black) piece = Black_Rook; 
            this.Close();
        }
    }
}
