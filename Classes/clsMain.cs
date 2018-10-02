using System;
using System.Windows.Forms;
namespace Logic_Navigator.Classes
{
	/// <summary>
	/// Summary description for clsMain.
	/// </summary>
	public class clsMain
	{
		public clsMain()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		[STAThread]
		static void Main() 
		{
			try
			{
				frmSplash objfrmSplash = new frmSplash();
				objfrmSplash.ShowDialog();
				clsGlobal.g_objfrmMDIMain = new frmMDIMain();
				Application.Run(clsGlobal.g_objfrmMDIMain);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message,"Logic Navigator",MessageBoxButtons.OK,MessageBoxIcon.Stop);												
			}
		}
	}
}
