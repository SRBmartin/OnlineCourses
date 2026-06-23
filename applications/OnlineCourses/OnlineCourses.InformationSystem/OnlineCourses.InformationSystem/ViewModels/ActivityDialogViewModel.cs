using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using OnlineCourses.InformationSystem.Models;
using OnlineCourses.InformationSystem.States;

namespace OnlineCourses.InformationSystem.ViewModels;

public class ActivityDialogViewModel : ViewModelBase
{
    private CourseViewModel? _selectedCourse;
    private DateTime _captureTime = DateTime.Today;
    private string _enrollmentCountText = "0";
    private string _processedTopicsCountText = "0";
    private string _averageGradeText = "0.00";
    private string _validationError = string.Empty;

    private readonly Action _close;
    private readonly Guid? _existingId;
    private readonly ICourseStatusState? _existingState;

    public string Title { get; }
    public bool Confirmed { get; private set; }

    public ObservableCollection<CourseViewModel> Courses { get; }

    public CourseViewModel? SelectedCourse
    {
        get => _selectedCourse;
        set => SetField(ref _selectedCourse, value);
    }

    public DateTime CaptureTime
    {
        get => _captureTime;
        set => SetField(ref _captureTime, value);
    }

    public string EnrollmentCountText
    {
        get => _enrollmentCountText;
        set => SetField(ref _enrollmentCountText, value);
    }

    public string ProcessedTopicsCountText
    {
        get => _processedTopicsCountText;
        set => SetField(ref _processedTopicsCountText, value);
    }

    public string AverageGradeText
    {
        get => _averageGradeText;
        set => SetField(ref _averageGradeText, value);
    }

    public string ValidationError
    {
        get => _validationError;
        private set => SetField(ref _validationError, value);
    }

    public ICommand SaveCommand   { get; }
    public ICommand CancelCommand { get; }

    public ActivityDialogViewModel(string title, Action close,
        IEnumerable<CourseViewModel> courses,
        CourseViewModel? selectedCourse = null,
        ActivityViewModel? existing = null)
    {
        Title  = title;
        _close = close;

        Courses       = new ObservableCollection<CourseViewModel>(courses);
        SelectedCourse = selectedCourse ?? Courses.FirstOrDefault();

        if (existing != null)
        {
            _existingId    = existing.Id;
            _existingState = existing.CurrentState;
            SelectedCourse = Courses.FirstOrDefault(c => c.Id == existing.CourseId)
                             ?? SelectedCourse;
            CaptureTime             = existing.CaptureTime;
            EnrollmentCountText     = existing.EnrollmentCount.ToString();
            ProcessedTopicsCountText = existing.ProcessedTopicsCount.ToString();
            AverageGradeText        = existing.AverageGrade.ToString("F2", CultureInfo.InvariantCulture);
        }

        SaveCommand   = new RelayCommand(_ => Save(), _ => SelectedCourse != null);
        CancelCommand = new RelayCommand(_ => _close());
    }

    private void Save()
    {
        if (!Validate()) return;
        Confirmed = true;
        _close();
    }

    private bool Validate()
    {
        if (SelectedCourse == null)
        {
            ValidationError = "Please select a course.";
            return false;
        }
        if (!int.TryParse(EnrollmentCountText, out int enrollment) || enrollment < 0)
        {
            ValidationError = "Enrollment count must be a whole number ≥ 0.";
            return false;
        }
        if (!int.TryParse(ProcessedTopicsCountText, out int topics) || topics < 0)
        {
            ValidationError = "Processed topics count must be a whole number ≥ 0.";
            return false;
        }
        if (!double.TryParse(AverageGradeText, NumberStyles.Any, CultureInfo.InvariantCulture, out double grade)
            || grade < 0 || grade > 10)
        {
            ValidationError = "Average grade must be a number between 0 and 10.";
            return false;
        }
        ValidationError = string.Empty;
        return true;
    }

    public ParticipantActivity ToActivityModel()
    {
        var enrollment = int.Parse(EnrollmentCountText);
        var topics     = int.Parse(ProcessedTopicsCountText);
        var grade      = double.Parse(AverageGradeText, CultureInfo.InvariantCulture);

        return new ParticipantActivity(
            _existingId ?? Guid.NewGuid(),
            SelectedCourse!.Id,
            CaptureTime,
            enrollment,
            topics,
            grade,
            _existingState ?? new PopularState());
    }
}
