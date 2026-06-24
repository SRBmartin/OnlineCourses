using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Microsoft.Win32;
using OnlineCourses.Contracts;
using OnlineCourses.DataProcessing.Adapters;
using OnlineCourses.DataProcessing.Models;
using OnlineCourses.DataProcessing.Services;
using OnlineCourses.DataProcessing.Strategies;

namespace OnlineCourses.DataProcessing.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IInformationSystemService _service;
    private readonly IActivityAdapter _adapter;
    private readonly StatisticsProcessor _processor;
    private readonly IStatisticsCalculator _calculator;

    private CourseViewModel? _selectedCourse;
    private DateTime         _from = DateTime.Today.AddMonths(-1);
    private DateTime         _to   = DateTime.Today;
    private string           _selectedStrategyName = string.Empty;
    private string           _activitiesText       = string.Empty;
    private string           _statisticsResult     = string.Empty;

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

    public string StatisticsResult
    {
        get => _statisticsResult;
        private set => SetField(ref _statisticsResult, value);
    }

    public ICommand FetchActivitiesCommand { get; }
    public ICommand RunStatisticsCommand { get; }
    public ICommand ExportCsvCommand { get; }
    public ICommand RefreshCoursesCommand { get; }

    public MainViewModel(IInformationSystemService service, IActivityAdapter adapter, StatisticsProcessor processor, IStatisticsCalculator calculator)
    {
        _service    = service;
        _adapter    = adapter;
        _processor  = processor;
        _calculator = calculator;

        FetchActivitiesCommand = new RelayCommand(
            _ => FetchActivities(),
            _ => SelectedCourse != null);

        RunStatisticsCommand = new RelayCommand(
            _ => RunStatistics(),
            _ => SelectedCourse != null && !string.IsNullOrEmpty(SelectedStrategyName));

        ExportCsvCommand = new RelayCommand(
            _ => ExportCsv(),
            _ => !string.IsNullOrEmpty(StatisticsResult));

        RefreshCoursesCommand = new RelayCommand(_ => LoadCourses());

        LoadCourses();
    }

    private async void LoadCourses()
    {
        try
        {
            var courses = await Task.Run(() => _service.GetAllCourses());
            Courses.Clear();
            foreach (var c in courses)
                Courses.Add(new CourseViewModel(c));
        }
        catch (Exception)
        {
            ActivitiesText = "Error: Information System is unavailable.";
        }
    }

    private async void FetchActivities()
    {
        var courseId = SelectedCourse!.Id;
        try
        {
            var raw = await Task.Run(() => _service.GetActivities(courseId, From, To));
            var newEntries = _adapter.Adapt(raw, courseId, From, To);
            foreach (var (key, value) in newEntries)
                _data[key] = value;

            if (_data.Values.All(v => v.Count == 0))
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
            sb.Append($"{key}: ");
            var ordered = activities.OrderBy(a => a.CaptureTime).ToList();
            DateTime? lastDate = null;
            for (int i = 0; i < ordered.Count; i++)
            {
                var a = ordered[i];
                if (i > 0) sb.Append(", ");
                var date = a.CaptureTime.Date;
                if (lastDate != date)
                {
                    sb.Append($"({a.CaptureTime:yyyy-MM-dd})->[{a.EnrollmentCount}, {a.ProcessedTopicsCount}, {a.AverageGrade:F2}]");
                    lastDate = date;
                }
                else
                {
                    sb.Append($"[{a.EnrollmentCount}, {a.ProcessedTopicsCount}, {a.AverageGrade:F2}]");
                }
            }
            sb.AppendLine();
        }
        return sb.ToString().TrimEnd();
    }

    private IStatisticalStrategy? ResolveStrategy() => SelectedStrategyName switch
    {
        "Min Topics & Avg Grade" => new MinTopicsAndAvgGradeStrategy(_calculator),
        "Average Enrollments"    => new AverageEnrollmentsStrategy(_calculator),
        "Recommended Count"      => new RecommendedCountStrategy(_calculator),
        _                        => null
    };

    private async void RunStatistics()
    {
        var strategy = ResolveStrategy();
        if (strategy == null) return;

        _processor.SetStrategy(strategy);
        var data = _data;
        try
        {
            var result = await Task.Run(() => _processor.RunStatistics(data));
            StatisticsResult = result;
        }
        catch (Exception)
        {
            StatisticsResult = "Error: Statistics calculation failed.";
        }
    }

    private void ExportCsv()
    {
        var dialog = new SaveFileDialog
        {
            Title      = "Export Statistics to CSV",
            Filter     = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
            DefaultExt = ".csv",
            FileName   = "statistics_result"
        };

        if (dialog.ShowDialog() != true) return;

        try
        {
            _processor.ExportToCsv(_data, dialog.FileName);
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(
                $"Export failed: {ex.Message}",
                "Export Error",
                System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Error);
        }
    }
}
