using System.Windows;
using OnlineCourses.InformationSystem.ViewModels;

namespace OnlineCourses.InformationSystem.Views.Dialogs;

public partial class CourseDialog : Window
{
    public CourseDialog(CourseDialogViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
