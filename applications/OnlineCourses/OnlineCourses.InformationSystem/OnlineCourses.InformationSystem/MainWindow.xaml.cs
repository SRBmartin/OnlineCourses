using System.Windows;
using OnlineCourses.InformationSystem.ViewModels;

namespace OnlineCourses.InformationSystem;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
