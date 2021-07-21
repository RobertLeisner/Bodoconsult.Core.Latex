using System;
using System.Windows.Forms;
using LaTexConvert.Business.Models;
using LaTexConvert.Business.ViewModels;

namespace LaTexConvert
{
    public static class Program
    {

        public static void Main()
        {

            var viewModel = new MainWindowViewModel();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow(viewModel));
        }
    }
}
