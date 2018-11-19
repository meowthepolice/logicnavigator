namespace Logic_Navigator
{
    partial class Advance_Pawn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.Queen = new System.Windows.Forms.Button();
            this.Knight = new System.Windows.Forms.Button();
            this.Bishop = new System.Windows.Forms.Button();
            this.Rook = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Queen
            // 
            this.Queen.Location = new System.Drawing.Point(69, 48);
            this.Queen.Name = "Queen";
            this.Queen.Size = new System.Drawing.Size(90, 30);
            this.Queen.TabIndex = 0;
            this.Queen.Text = "Queen";
            this.Queen.UseVisualStyleBackColor = true;
            this.Queen.Click += new System.EventHandler(this.Queen_Click);
            // 
            // Knight
            // 
            this.Knight.Location = new System.Drawing.Point(165, 49);
            this.Knight.Name = "Knight";
            this.Knight.Size = new System.Drawing.Size(107, 29);
            this.Knight.TabIndex = 1;
            this.Knight.Text = "Knight";
            this.Knight.UseVisualStyleBackColor = true;
            this.Knight.Click += new System.EventHandler(this.Knight_Click);
            // 
            // Bishop
            // 
            this.Bishop.Location = new System.Drawing.Point(278, 47);
            this.Bishop.Name = "Bishop";
            this.Bishop.Size = new System.Drawing.Size(95, 31);
            this.Bishop.TabIndex = 2;
            this.Bishop.Text = "Bishop";
            this.Bishop.UseVisualStyleBackColor = true;
            this.Bishop.Click += new System.EventHandler(this.Bishop_Click);
            // 
            // Rook
            // 
            this.Rook.Location = new System.Drawing.Point(379, 47);
            this.Rook.Name = "Rook";
            this.Rook.Size = new System.Drawing.Size(95, 31);
            this.Rook.TabIndex = 3;
            this.Rook.Text = "Rook";
            this.Rook.UseVisualStyleBackColor = true;
            this.Rook.Click += new System.EventHandler(this.Rook_Click);
            // 
            // Advance_Pawn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 116);
            this.Controls.Add(this.Rook);
            this.Controls.Add(this.Bishop);
            this.Controls.Add(this.Knight);
            this.Controls.Add(this.Queen);
            this.Name = "Advance_Pawn";
            this.Text = "Advance_Pawn";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Queen;
        private System.Windows.Forms.Button Knight;
        private System.Windows.Forms.Button Bishop;
        private System.Windows.Forms.Button Rook;
    }
}