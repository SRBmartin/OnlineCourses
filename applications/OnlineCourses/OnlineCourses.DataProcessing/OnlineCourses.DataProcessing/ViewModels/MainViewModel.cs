using System.Collections.ObjectModel;
using System.Windows.Input;
using OnlineCourses.Contracts;

namespace OnlineCourses.DataProcessing.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IInformationSystemService _service;

    private CourseViewModel? _selectedCourse;
    private DateTime         _from = DateTime.Today.AddMonths(-1);
    private DateTime         _to   = DateTime.Today;
    private string           _selectedStrategyName = string.Empty;

    public ObservableCollection<CourseViewModel> Courses { get; } = new();

    public IReadOnlyList<string> StrategyNames { get; } =
    [
        "Min Topics & Avg Grade",
        "Average Enrollments",
        "Recommended Count"
    ];

    public CourseViewModel? SelectedCourse
    {
        get => _selectedCourse;
        set => SetField(ref _selectedCourse, value);
    }

    public DateTime From
    {
        get => _from;
        set => SetField(ref _from, value);
    }

    public DateTime To
    {
        get => _to;
        set => SetField(ref _to, value);
    }

    public string SelectedStrategyName
    {
        get => _selectedStrategyName;
        set => SetField(ref _selectedStrategyName, value);
    }

    // DP-7: FetchActivitiesCommand — calls service.GetActivities, runs ActivityAdapter.Adapt,
    //        stores result in StatisticsProcessor._data, populates ActivitiesView.
    public ICommand FetchActivitiesCommand { get; }

    // DP-8: RunStatisticsCommand — resolves strategy from SelectedStrategyName,
    //        calls StatisticsProcessor.SetStrategy + RunStatistics, updates StatisticsResult.
    public ICommand RunStatisticsCommand { get; }

    // DP-9: ExportCsvCommand — calls StatisticsProcessor.ExportToCsv with a SaveFileDialog path.
    public ICommand ExportCsvCommand { get; }

    public MainViewModel(IInformationSystemService service)
    {
        _service = service;

        FetchActivitiesCommand = new RelayCommand(
            _ => FetchActivities(),
            _ => SelectedCourse != null);

        RunStatisticsCommand = new RelayCommand(
            _ => RunStatistics(),
            _ => SelectedCourse != null && !string.IsNullOrEmpty(SelectedStrategyName));

        ExportCsvCommand = new RelayCommand(
            _ => ExportCsv(),
            _ => false); // DP-9: enable when statistics result is available

        LoadCourses();
    }

    private void LoadCourses()
    {
        var courses = _service.GetAllCourses();
        Courses.Clear();
        foreach (var c in courses)
            Courses.Add(new CourseViewModel(c));
    }

    private void FetchActivities()
    {
        // DP-7: Call _service.GetActivities(SelectedCourse!.Id, From, To),
        //        adapt result via ActivityAdapter, store in StatisticsProcessor.
        throw new NotImplementedException("DP-7");
    }

    private void RunStatistics()
    {
        // DP-8: Resolve IStatisticalStrategy from SelectedStrategyName,
        //        call StatisticsProcessor.SetStrategy + RunStatistics, show result.
        throw new NotImplementedException("DP-8");
    }

    private void ExportCsv()
    {
        // DP-9: Show SaveFileDialog, call StatisticsProcessor.ExportToCsv.
        throw new NotImplementedException("DP-9");
    }
}
