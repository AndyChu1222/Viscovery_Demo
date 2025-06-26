using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // �T�O���y�j�i�H�ߧY��J
            InputTextBox.Focus();
        }
        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // ���y�����A�۰���������
                DialogResult = true;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
