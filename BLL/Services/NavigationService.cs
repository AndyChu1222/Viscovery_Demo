using System;
using System.Windows.Controls;
using ViscoveryDemo.Presentation.ViewModels;

namespace ViscoveryDemo.BLL.Services
{
    public interface INavigationService
    {
        BaseViewModel CurrentViewModel { get; }
        void NavigateTo<T>() where T : BaseViewModel;
        UserControl NavigateToView<TView>() where TView : UserControl;
    }

    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private UserControl _currentView;
        private BaseViewModel _current;
        public BaseViewModel CurrentViewModel
        {
            get => _current;
            private set
            {
                _current = value;
                (value as INavigable)?.OnEnter();
            }
        }

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<T>() where T : BaseViewModel
        {
            if (CurrentViewModel is INavigable oldNav)
            {
                oldNav.OnExit();
            }

            var vm = (BaseViewModel)_serviceProvider.GetService(typeof(T));
            CurrentViewModel = vm;
        }

        public UserControl NavigateToView<TView>() where TView : UserControl
        {
            if (_currentView?.DataContext is INavigable oldNav)
            {
                oldNav.OnExit();
            }

            var view = (UserControl)_serviceProvider.GetService(typeof(TView));

            if (view.DataContext is INavigable newNav)
            {
                newNav.OnEnter();
            }

            _currentView = view;
            return view;
        }
    }
}
