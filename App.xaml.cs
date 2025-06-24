using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ViscoveryDemo.BLL.Services;
using ViscoveryDemo.Presentation.ViewModels;
using ViscoveryDemo.DAL.Repositories;
using ViscoveryDemo.Presentation.Views;

namespace ViscoveryDemo
{
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();

            // ±Ò°Ê VisAgent
            var visAgentService = _serviceProvider.GetRequiredService<IShowViscoveryService>();
            visAgentService.StartVisAgent();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = _serviceProvider.GetRequiredService<MainViewModel>();

            var page = _serviceProvider.GetRequiredService<page1>();
            page.DataContext = _serviceProvider.GetRequiredService<Page1ViewModel>();

            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IShowViscoveryService, ShowViscoveryService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
            services.AddSingleton<IUnifiedRecognitionRepository, ApiUnifiedRecognitionRepository>();
            services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<page1>();
            services.AddSingleton<Page1ViewModel>();
            services.AddSingleton<ShellWindow>();
        }
    }
}
