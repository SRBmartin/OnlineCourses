using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using OnlineCourses.Contracts;
using OnlineCourses.DataProcessing.Adapters;
using OnlineCourses.DataProcessing.Models;
using OnlineCourses.DataProcessing.Services;

namespace OnlineCourses.DataProcessing.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IInformationSystemService _service;
    private readonly IActivityAdapter _adapter;
    private readonly StatisticsProcessor _processor;

    private CourseViewModel? _selectedCourse;
    private DateTime         _from = DateTime.Today.AddMonths(-1);
    private DateTime         _to   = DateTime.Today;
    private string           _selectedStrategyName = string.Empty;
    private string           _activitiesText       = string.Empty;

    private Dictionary<string, List<ReducedActivity>> _data = new();

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

    public string ActivitiesText
    {
        get => _activitiesText;
        private set => SetField(ref _activitiesText, value);
    }

    public ICommand FetchActivitiesCommand { get; }

    // DP-8: RunStatisticsCommand — resolves strategy from SelectedStrategyName,
    //        calls StatisticsProcessor.SetStrategy + RunStatistics, updates StatisticsResult.
    public ICommand RunStatisticsCommand { get; }

    // DP-9: ExportCsvCommand — calls StatisticsProcessor.ExportToCsv with a SaveFileDialog path.
    public ICommand ExportCsvCommand { get; }

    public MainViewModel(IInformationSystemService service, IActivityAdapter adapter, StatisticsProcessor processor)
    {
        _service   = service;
        _adapter   = adapter;
        _processor = processor;

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
        try
        {
            var courses = _service.GetAllCourses();
            Courses.Clear();
            foreach (var c in courses)
                Courses.Add(new CourseViewModel(c));
        }
        catch (Exception)
        {
            ActivitiesText = "Error: Information System is unavailable.";
        }
    }

    private void FetchActivities()
    {
        try
        {
            var raw = _service.GetActivities(SelectedCourse!.Id, From, To);
            _data = _adapter.Adapt(raw, SelectedCourse.Id, From, To);

            if (_data.Count == 0 || _data.Values.All(v => v.Count == 0))
            {
                ActivitiesText = "No data found.";
                return;
            }

            ActivitiesText = FormatData(_data);
        }
        catch (Exception)
        {
            ActivitiesText = "Error: Information System is unavailable.";
        }
    }

    private static string FormatData(Dictionary<string, List<ReducedActivity>> data)
    {
        var sb = new StringBuilder();
        foreach (var (key, activities) in data)
        {
            sb.AppendLine($"{key}:");
            foreach (var a in activities.OrderBy(a => a.CaptureTime))
                sb.AppendLine($"  {a.CaptureTime:yyyy-MM-dd} -> [{a.EnrollmentCount}, {a.ProcessedTopicsCount}, {a.AverageGrade:F2}]");
        }
        return sb.ToString().TrimEnd();
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
