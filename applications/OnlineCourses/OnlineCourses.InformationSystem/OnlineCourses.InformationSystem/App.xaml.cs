using System.Windows;
using OnlineCourses.InformationSystem.Commands;
using OnlineCourses.InformationSystem.Repositories;
using OnlineCourses.InformationSystem.ViewModels;

namespace OnlineCourses.InformationSystem;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var courseRepository = new CourseRepository();
        var commandManager   = new CommandManager();

        // IS-3: var activityRepository = new ActivityRepository();
        // IS-6: Start CoreWCF service host here before showing the window.

        var mainViewModel = new MainViewModel(courseRepository, commandManager);

        var mainWindow = new MainWindow(mainViewModel);
        MainWindow = mainWindow;
        mainWindow.Show();
    }
}
