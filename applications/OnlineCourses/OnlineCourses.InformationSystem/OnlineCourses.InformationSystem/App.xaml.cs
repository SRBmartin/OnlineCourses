using System.IO;
using System.Windows;
using CoreWCF;
using CoreWCF.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnlineCourses.Contracts;
using OnlineCourses.InformationSystem.Commands;
using OnlineCourses.InformationSystem.Observers;
using OnlineCourses.InformationSystem.Persistence;
using OnlineCourses.InformationSystem.Repositories;
using OnlineCourses.InformationSystem.Services;
using OnlineCourses.InformationSystem.ViewModels;
using ActivityDto = OnlineCourses.Contracts.ParticipantActivity;
using CourseDto   = OnlineCourses.Contracts.Course;

namespace OnlineCourses.InformationSystem;

public partial class App : Application
{
    private WebApplication? _wcfApp;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        var coursesFile        = Path.Combine(dataDir, "courses.xml");
        var courseStore        = new XmlEntityStore<CourseDto>(coursesFile);
        var courseRepository   = new CourseRepository(courseStore);

        var activitiesFile     = Path.Combine(dataDir, "activities.xml");
        var activityStore      = new XmlEntityStore<ActivityDto>(activitiesFile);
        var activityRepository = new ActivityRepository(activityStore);

        var commandManager = new CommandManager();
        var logObserver    = new LogObserver(Path.Combine(dataDir, "actions.log"));
        var chartObserver  = new ChartObserver();

        StartWcfService(courseRepository, activityRepository);

        var mainViewModel = new MainViewModel(courseRepository, activityRepository,
            commandManager, logObserver, chartObserver);

        var mainWindow = new MainWindow(mainViewModel);
        MainWindow = mainWindow;
        mainWindow.Show();
    }

    private void StartWcfService(ICourseRepository courseRepository, IActivityRepository activityRepository)
    {
        var wcfBuilder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            Args = []
        });

        wcfBuilder.Logging.ClearProviders();
        wcfBuilder.WebHost.UseUrls("http://localhost:20000");
        wcfBuilder.Services.AddServiceModelServices();
        wcfBuilder.Services.AddSingleton(
            new InformationSystemService(courseRepository, activityRepository));

        var wcfApp = wcfBuilder.Build();
        wcfApp.UseServiceModel(sb =>
        {
            sb.AddService<InformationSystemService>();
            sb.AddServiceEndpoint<InformationSystemService, IInformationSystemService>(
                new BasicHttpBinding(), "/InformationSystemService");
        });

        _wcfApp = wcfApp;
        _ = wcfApp.RunAsync();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
        _wcfApp?.StopAsync(cts.Token).GetAwaiter().GetResult();
        base.OnExit(e);
    }
}
