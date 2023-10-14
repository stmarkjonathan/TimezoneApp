using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimezoneApp.ViewModels;

namespace TimezoneApp
{

    public partial class  ClockWindow : Window
    {

        public ClockWindowViewModel ViewModel;

        public ClockWindow()
        {
            InitializeComponent();
            SetWindowPosition();

            DataContext = ViewModel = new ClockWindowViewModel();

        }

        public void SetWindowPosition()
        {
            var screenArea = SystemParameters.WorkArea;
            this.Left = screenArea.Right - this.Width;
            this.Top = screenArea.Bottom - this.Height;
        }
    }
}
