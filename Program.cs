using System;
using System.Windows.Forms;

namespace CustomOpenFileDialog
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(new CustomOpenFileDialog(){OpenButtonCaption = "Create", OpenDialog = { Title = @"Custom text"}}));
        }
    }
}