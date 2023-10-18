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
using System.Windows.Threading;
using TimezoneApp.ViewModels;

namespace TimezoneApp
{

    public partial class  ClockWindow : Window
    {

        public ClockWindowViewModel ViewModel;
        private DispatcherTimer _clockTimer;

        public ClockWindow()
        {
            InitializeComponent();
            SetWindowPosition();
            StartClock();

            DataContext = ViewModel = new ClockWindowViewModel();

        }

        public void SetWindowPosition()
        {
            var screenArea = SystemParameters.WorkArea;
            this.Left = screenArea.Right - this.Width;
            this.Top = screenArea.Bottom - this.Height;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            ViewModel.UpdateList(textBox.Text);
        }

        private void StartClock()
        {
            _clockTimer = new DispatcherTimer();
            _clockTimer.Interval = TimeSpan.FromSeconds(1);
            _clockTimer.Tick += clockTick;
            _clockTimer.Start();

        }

        private void ToggleVisibility()
        {
            if (Visibility == Visibility.Visible)
            {
                Visibility = Visibility.Hidden;
                _clockTimer.Stop();
            }
            else
            {
                Visibility = Visibility.Visible;
                _clockTimer.Start();
            }
        }

        private void clockTick(object sender, EventArgs e) 
        {
            ViewModel.UpdateClock();
        }

        private void toggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleVisibility();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
