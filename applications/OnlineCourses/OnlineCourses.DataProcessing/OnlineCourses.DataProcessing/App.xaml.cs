using System.ServiceModel;
using System.Windows;
using OnlineCourses.Contracts;
using OnlineCourses.DataProcessing.Adapters;
using OnlineCourses.DataProcessing.Export;
using OnlineCourses.DataProcessing.Services;
using OnlineCourses.DataProcessing.ViewModels;
using OnlineCourses.DataProcessing.Views;

namespace OnlineCourses.DataProcessing;

public partial class App : Application
{
    private const string ServiceUrl  = "http://localhost:20000/InformationSystemService";
    private const int    MaxRetries  = 3;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var connectionVm = new ConnectionViewModel();
        var connectionWindow = new ConnectionWindow(connectionVm);
        MainWindow = connectionWindow;
        connectionWindow.Show();

        var service = await ConnectWithRetryAsync(connectionVm);

        if (service == null)
        {
            Shutdown();
            return;
        }

        var adapter     = new ActivityAdapter();
        var exporter    = new CsvExporter();
        var processor   = new StatisticsProcessor(exporter);
        var calculator  = new StatisticsCalculator();
        var mainVm      = new MainViewModel(service, adapter, processor, calculator);
        var mainWindow = new MainWindow(mainVm);
        MainWindow = mainWindow;
        connectionWindow.Close();
        mainWindow.Show();
    }

    private static async Task<IInformationSystemService?> ConnectWithRetryAsync(ConnectionViewModel vm)
    {
        for (int attempt = 1; attempt <= MaxRetries; attempt++)
        {
            if (vm.CancellationToken.IsCancellationRequested)
                return null;

            vm.StatusText = $"Connecting to Information System... (attempt {attempt}/{MaxRetries})";

            IInformationSystemService? svc;
            try
            {
                svc = await Task.Run<IInformationSystemService?>(() =>
                {
                    try
                    {
                        var binding = new BasicHttpBinding
                        {
                            OpenTimeout    = TimeSpan.FromSeconds(4),
                            SendTimeout    = TimeSpan.FromSeconds(4),
                            ReceiveTimeout = TimeSpan.FromSeconds(30)
                        };
                        var endpoint = new EndpointAddress(ServiceUrl);
                        var channel = new ChannelFactory<IInformationSystemService>(binding, endpoint).CreateChannel();
                        channel.GetAllCourses();
                        return channel;
                    }
                    catch (OperationCanceledException) { throw; }
                    catch { return null; }
                }, vm.CancellationToken);
            }
            catch (OperationCanceledException)
            {
                return null;
            }

            if (svc != null)
            {
                vm.StatusText = "Connected.";
                return svc;
            }

            if (attempt < MaxRetries)
            {
                vm.StatusText = $"Information System not available. Retrying in 4s... (attempt {attempt}/{MaxRetries})";
                try { await Task.Delay(4000, vm.CancellationToken); }
                catch (OperationCanceledException) { return null; }
            }
        }

        MessageBox.Show(
            $"Could not connect to the Information System after {MaxRetries} attempts.\n" +
            "The service is unavailable. Please start OnlineCourses.InformationSystem and try again.",
            "Service Unavailable",
            MessageBoxButton.OK,
            MessageBoxImage.Error);

        return null;
    }
}
