using System.Windows;

namespace ViscoveryDemo.Presentation.Views
{
    public partial class ScanInputWindow : Window
    {
        public string OrderType => InputTextBox.Text;

        public ScanInputWindow()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
