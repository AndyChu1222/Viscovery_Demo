using System.Windows;
using ViscoveryDemo.BLL.Services;

namespace ViscoveryDemo.Presentation.Views
{
    public partial class ShellWindow : Window
    {
        public ShellWindow(INavigationService navigationService)
        {
            InitializeComponent();
            DataContext = navigationService;
        }
    }
}
