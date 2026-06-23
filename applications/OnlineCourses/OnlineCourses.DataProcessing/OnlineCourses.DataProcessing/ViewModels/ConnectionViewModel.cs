using System.Windows.Input;

namespace OnlineCourses.DataProcessing.ViewModels;

public class ConnectionViewModel : ViewModelBase
{
    private string _statusText = "Connecting to Information System...";
    private readonly CancellationTokenSource _cts = new();

    public string StatusText
    {
        get => _statusText;
        set => SetField(ref _statusText, value);
    }

    public ICommand CancelCommand { get; }

    public CancellationToken CancellationToken => _cts.Token;

    public ConnectionViewModel()
    {
        CancelCommand = new RelayCommand(_ => _cts.Cancel());
    }
}
