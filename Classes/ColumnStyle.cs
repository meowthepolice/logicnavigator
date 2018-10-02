using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Logic_Navigator.Classes
{
	/// <summary>
	/// Summary description for ColumnStyle.
	/// </summary>
	public class ColumnStyle:DataGridTextBoxColumn
	{

		public ColumnStyle()//PropertyDescriptor pcol)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		protected override void Abort(int RowNum) 
		{
		}

		protected override bool Commit(CurrencyManager DataSource,int RowNum) 
		{
			return true;
		}
		protected override void Edit(CurrencyManager Source ,int Rownum,Rectangle Bounds, bool ReadOnly,string InstantText, bool CellIsVisible) 
		{
		}

		protected override int GetMinimumHeight() 
		{
			//
			// return here your minimum height
			//
			return 16;
		}

		protected override int GetPreferredHeight(Graphics g ,object Value) 
		{
			//
			// return here your preferred height
			//
			return 16;
		}

		protected override Size GetPreferredSize(Graphics g, object Value) 
		{
			//
			// return here your preferred size
			//
			Size cellSize = new Size(75 ,16 );
			return cellSize;
		}

		protected override void Paint(Graphics g,Rectangle Bounds,CurrencyManager Source,int RowNum) 
		{
			Brush BackBrush = new SolidBrush(Color.White);

			bool bdel = (bool) GetColumnValueAtRow(Source, RowNum);

			if(bdel == true)
				BackBrush = Brushes.Coral;
			else
				BackBrush = Brushes.White;

			g.FillRectangle(BackBrush, Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);

			System.Drawing.Font font = new Font(System.Drawing.FontFamily.GenericSansSerif , (float)8.25 );
			g.DrawString( bdel.ToString() ,font ,Brushes.Black ,Bounds.X ,Bounds.Y );

		}

		protected override void Paint(Graphics g,Rectangle Bounds,CurrencyManager Source,int RowNum,bool AlignToRight) 
		{
			Brush BackBrush = new SolidBrush(Color.White);
			bool bdel = (bool) GetColumnValueAtRow(Source, RowNum);

			if(bdel == true)
				BackBrush = Brushes.Coral;
			else
				BackBrush = Brushes.White;

			g.FillRectangle(BackBrush, Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);

			System.Drawing.Font font = new Font(System.Drawing.FontFamily.GenericSansSerif , (float)8.25 );
			g.DrawString( bdel.ToString() ,font ,Brushes.Black ,Bounds.X ,Bounds.Y );

		}

		protected override void Paint(Graphics g,Rectangle Bounds,CurrencyManager Source,int RowNum, Brush BackBrush ,Brush ForeBrush ,bool AlignToRight) 
		{
					
			bool bdel = (bool) GetColumnValueAtRow(Source, RowNum);

			if(bdel == true)
				BackBrush = Brushes.Coral;
			else
				BackBrush = Brushes.White;

			g.FillRectangle(BackBrush, Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);

			System.Drawing.Font font = new Font(System.Drawing.FontFamily.GenericSansSerif , (float)8.25 );
			g.DrawString( bdel.ToString() ,font ,Brushes.Black ,Bounds.X ,Bounds.Y );


		}
	}
}
