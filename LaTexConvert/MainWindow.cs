using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaTexConvert.Business.ViewModels;

namespace LaTexConvert
{
    public partial class MainWindow : Form
    {
        private MainWindowViewModel _viewModel;


        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            _viewModel = mainWindowViewModel;

            InitializeComponent();
        }
    }
}
