using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViscoveryDemo.BLL.Models;
using ViscoveryDemo.BLL.Services;
using ViscoveryDemo.Presentation.Views;

namespace ViscoveryDemo.Presentation.ViewModels
{
    public class Page1ViewModel : BaseViewModel, INavigable
    {
        private readonly INavigationService _navigationService;
        private readonly IOrderService _orderService;

        public NavigationStateMachine StateMachine { get; } = new NavigationStateMachine();

        public ObservableCollection<Order> Orders { get; } = new ObservableCollection<Order>();

        private decimal _subtotal;
        public decimal Subtotal
        {
            get => _subtotal;
            set { _subtotal = value; OnPropertyChanged(); }
        }

        private decimal _tax;
        public decimal Tax
        {
            get => _tax;
            set { _tax = value; OnPropertyChanged(); }
        }

        private decimal _serviceFee;
        public decimal ServiceFee
        {
            get => _serviceFee;
            set { _serviceFee = value; OnPropertyChanged(); }
        }

        private decimal _total;
        public decimal Total
        {
            get => _total;
            set { _total = value; OnPropertyChanged(); }
        }

        private Visibility _standbyVisibility = Visibility.Visible;
        public Visibility StandbyVisibility
        {
            get => _standbyVisibility;
            set { _standbyVisibility = value; OnPropertyChanged(); }
        }

        private Visibility _successVisibility = Visibility.Collapsed;
        public Visibility SuccessVisibility
        {
            get => _successVisibility;
            set { _successVisibility = value; OnPropertyChanged(); }
        }

        private Visibility _failedVisibility = Visibility.Collapsed;
        public Visibility FailedVisibility
        {
            get => _failedVisibility;
            set { _failedVisibility = value; OnPropertyChanged(); }
        }

        private bool _canConfirm;
        public bool CanConfirm
        {
            get => _canConfirm;
            set
            {
                if (_canConfirm != value)
                {
                    _canConfirm = value;
                    OnPropertyChanged();
                    _confirmCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        private RelayCommand _confirmCommand;
        public ICommand ConfirmCommand => _confirmCommand = new RelayCommand(_ => Confirm(), _ => CanConfirm);

        public AsyncRelayCommand ScanCommand { get; }
        public ICommand NewOrderCommand { get; }

        public Page1ViewModel(INavigationService navigationService, IOrderService orderService)
        {
            _navigationService = navigationService;
            _orderService = orderService;
            ScanCommand = new AsyncRelayCommand(ScanAsync);
            //ScanCommand = new RelayCommand(_ => Scan());
            NewOrderCommand = new RelayCommand(_ => ClearOrders());
        }

        public void OnEnter()
        {
            StateMachine.Enter();
            System.Diagnostics.Debug.WriteLine("Enter Page1ViewModel");
            ClearOrders();
        }

        public void OnExit()
        {
            StateMachine.Exit();
            System.Diagnostics.Debug.WriteLine("Exit Page1ViewModel");
        }

        private async Task ScanAsync()
        {
            var dialog = new ScanInputWindow();
            if (dialog.ShowDialog() != true)
            {
                return;
            }

            Orders.Clear();

            var loadedOrders = await _orderService.LoadOrdersAsync(dialog.OrderType);

            foreach (var order in loadedOrders)
            {
                Orders.Add(order);
            }

            UpdateSummary();

            if (Orders.Any(o => o.Status == OrderStatus.Undetected))
            {
                FailedVisibility = Visibility.Visible;
                SuccessVisibility = Visibility.Collapsed;
            }
            else
            {
                FailedVisibility = Visibility.Collapsed;
                SuccessVisibility = Visibility.Visible;
            }

            StandbyVisibility = Visibility.Collapsed;
        }

        private void ClearOrders()
        {
            Orders.Clear();
            StandbyVisibility = Visibility.Visible;
            SuccessVisibility = Visibility.Collapsed;
            FailedVisibility = Visibility.Collapsed;
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            Subtotal = Orders.Where(o => o.Status == OrderStatus.Confirm).Sum(o => o.Price);
            Tax = Subtotal * 0.05m;
            ServiceFee = Subtotal * 0.1m;
            Total = Subtotal + Tax + ServiceFee;

            CanConfirm = Orders.Any() && Orders.All(o => o.Status == OrderStatus.Confirm);
        }

        private void Confirm()
        {
            if (Application.Current.MainWindow?.DataContext is MainViewModel mainVm)
            {
                mainVm.MyUserControlInstance = null;
            }
            OnExit();
        }
    }
}
