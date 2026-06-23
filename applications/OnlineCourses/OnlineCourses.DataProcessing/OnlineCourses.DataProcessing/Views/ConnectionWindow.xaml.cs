using OnlineCourses.DataProcessing.ViewModels;
using System.Windows;

namespace OnlineCourses.DataProcessing.Views;

public partial class ConnectionWindow : Window
{
    public ConnectionWindow(ConnectionViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
