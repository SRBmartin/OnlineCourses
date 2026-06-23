using System.ServiceModel;
using System.Windows;
using OnlineCourses.Contracts;
using OnlineCourses.DataProcessing.ViewModels;
using OnlineCourses.DataProcessing.Views;

namespace OnlineCourses.DataProcessing;

public partial class App : Application
{
    private const string ServiceUrl = "http://localhost:20000/InformationSystemService";

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

        var mainVm = new MainViewModel(service);
        var mainWindow = new MainWindow(mainVm);
        MainWindow = mainWindow;
        connectionWindow.Close();
        mainWindow.Show();
    }

    private static async Task<IInformationSystemService?> ConnectWithRetryAsync(ConnectionViewModel vm)
    {
        int attempt = 0;
        while (!vm.CancellationToken.IsCancellationRequested)
        {
            attempt++;
            vm.StatusText = $"Connecting to Information System... (attempt {attempt})";
            try
            {
                var channel = await Task.Run(() =>
                {
                    var binding = new BasicHttpBinding
                    {
                        OpenTimeout    = TimeSpan.FromSeconds(4),
                        SendTimeout    = TimeSpan.FromSeconds(4),
                        ReceiveTimeout = TimeSpan.FromSeconds(30)
                    };
                    var endpoint = new EndpointAddress(ServiceUrl);
                    var svc = new ChannelFactory<IInformationSystemService>(binding, endpoint).CreateChannel();
                    svc.GetAllCourses();
                    return svc;
                }, vm.CancellationToken);

                vm.StatusText = "Connected.";
                return channel;
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            catch
            {
                vm.StatusText = $"Information System not available. Retrying in 2s... (attempt {attempt})";
                try
                {
                    await Task.Delay(2000, vm.CancellationToken);
                }
                catch
                {
                    return null;
                }
            }
        }
        return null;
    }
}
