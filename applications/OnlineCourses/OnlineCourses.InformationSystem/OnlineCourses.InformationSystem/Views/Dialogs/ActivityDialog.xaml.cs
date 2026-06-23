using System.Windows;
using OnlineCourses.InformationSystem.ViewModels;

namespace OnlineCourses.InformationSystem.Views.Dialogs;

public partial class ActivityDialog : Window
{
    public ActivityDialog(ActivityDialogViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
