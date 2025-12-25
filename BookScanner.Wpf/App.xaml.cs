using System.Configuration;
using System.Data;
using System.Windows;
using BookScanner.Infrastructure.DependencyInjection;
using BookScanner.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace BookScanner.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    private ServiceProvider? _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();

        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddBookScannerServices();
        services.AddTransient<MainViewModel>();
        services.AddTransient<MainWindow>();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _serviceProvider?.Dispose();
        base.OnExit(e);
    }
}


