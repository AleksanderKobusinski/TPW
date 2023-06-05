using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ViewModel;

namespace View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();

            Task.Run(async () =>
            {
                while (true)
                {
                    Dispatcher.Invoke(() => itemss.Items.Refresh());
                    await Task.Delay(20);
                }
            });
        }

        private void ChangedTextBox(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            Regex regex = new Regex("^[0-9]*$");

            if (!regex.IsMatch(text))
            {
                Dispatcher.BeginInvoke(new Action(() => ((TextBox)sender).Undo()));
            }
        }
    }
}
