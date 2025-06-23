using System.Windows.Input;
using System.Windows.Controls;
using ViscoveryDemo.BLL.Services;
using ViscoveryDemo.Presentation.Views;

namespace ViscoveryDemo.Presentation.ViewModels
{
    public class MainViewModel : BaseViewModel, INavigable
    {
        private readonly INavigationService _navigationService;
        private UserControl _myUserControlInstance;

        public NavigationStateMachine StateMachine { get; } = new NavigationStateMachine();

        public ICommand NavigateCommand { get; }

        public UserControl MyUserControlInstance
        {
            get => _myUserControlInstance;
            set
            {
                if (_myUserControlInstance != value)
                {
                    _myUserControlInstance = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new RelayCommand(_ =>
                MyUserControlInstance = _navigationService.NavigateToView<page1>());
        }

        public void OnEnter()
        {
            StateMachine.Enter();
            System.Diagnostics.Debug.WriteLine("Enter MainViewModel");
            MyUserControlInstance = _navigationService.NavigateToView<page1>();
        }

        public void OnExit()
        {
            StateMachine.Exit();
            System.Diagnostics.Debug.WriteLine("Exit MainViewModel");
        }
    }
}
