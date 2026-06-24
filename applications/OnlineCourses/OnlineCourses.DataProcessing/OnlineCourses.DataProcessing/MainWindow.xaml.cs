using OnlineCourses.DataProcessing.ViewModels;
using System.Windows;

namespace OnlineCourses.DataProcessing;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
