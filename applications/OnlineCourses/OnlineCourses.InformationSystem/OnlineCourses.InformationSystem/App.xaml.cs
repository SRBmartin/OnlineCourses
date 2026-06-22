using System.IO;
using System.Windows;
using OnlineCourses.Contracts;
using OnlineCourses.InformationSystem.Commands;
using OnlineCourses.InformationSystem.Persistence;
using OnlineCourses.InformationSystem.Repositories;
using OnlineCourses.InformationSystem.ViewModels;

namespace OnlineCourses.InformationSystem;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var coursesFile    = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "courses.xml");
        var courseStore      = new XmlEntityStore<Course>(coursesFile);
        var courseRepository = new CourseRepository(courseStore);
        var commandManager   = new CommandManager();

        // IS-3: var activitiesFile    = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "activities.xml");
        // IS-3: var activityStore      = new XmlEntityStore<Contracts.ParticipantActivity>(activitiesFile);
        // IS-3: var activityRepository = new ActivityRepository(activityStore);
        // IS-6: Start CoreWCF service host here before showing the window.

        var mainViewModel = new MainViewModel(courseRepository, commandManager);

        var mainWindow = new MainWindow(mainViewModel);
        MainWindow = mainWindow;
        mainWindow.Show();
    }
}
