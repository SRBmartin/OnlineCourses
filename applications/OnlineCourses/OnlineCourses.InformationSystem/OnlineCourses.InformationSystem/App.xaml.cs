using System.IO;
using System.Windows;
using OnlineCourses.InformationSystem.Commands;
using OnlineCourses.InformationSystem.Observers;
using OnlineCourses.InformationSystem.Persistence;
using OnlineCourses.InformationSystem.Repositories;
using OnlineCourses.InformationSystem.ViewModels;
using ActivityDto = OnlineCourses.Contracts.ParticipantActivity;
using CourseDto   = OnlineCourses.Contracts.Course;

namespace OnlineCourses.InformationSystem;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        var coursesFile      = Path.Combine(dataDir, "courses.xml");
        var courseStore      = new XmlEntityStore<CourseDto>(coursesFile);
        var courseRepository = new CourseRepository(courseStore);

        var activitiesFile   = Path.Combine(dataDir, "activities.xml");
        var activityStore    = new XmlEntityStore<ActivityDto>(activitiesFile);
        var activityRepository = new ActivityRepository(activityStore);

        var commandManager = new CommandManager();
        var logObserver    = new LogObserver(Path.Combine(dataDir, "actions.log"));

        // IS-6: Start CoreWCF service host here before showing the window.

        var mainViewModel = new MainViewModel(courseRepository, activityRepository,
            commandManager, logObserver);

        var mainWindow = new MainWindow(mainViewModel);
        MainWindow = mainWindow;
        mainWindow.Show();
    }
}
