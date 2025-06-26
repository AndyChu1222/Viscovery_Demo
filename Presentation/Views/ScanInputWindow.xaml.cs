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
            // 確保掃描槍可以立即輸入
            InputTextBox.Focus();
        }
        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // 掃描結束，自動關閉視窗
                DialogResult = true;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
