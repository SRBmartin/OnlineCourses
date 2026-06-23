using OnlineCourses.Contracts;
using OnlineCourses.InformationSystem.States;
using DomainActivity = OnlineCourses.InformationSystem.Models.ParticipantActivity;

namespace OnlineCourses.InformationSystem.ViewModels;

public class ActivityViewModel : ViewModelBase
{
    private readonly DomainActivity _activity;

    public ActivityViewModel(DomainActivity activity, string courseName)
    {
        _activity  = activity;
        CourseName = courseName;
    }

    public Guid Id                  => _activity.Id;
    public Guid CourseId            => _activity.CourseId;
    public string CourseName        { get; }
    public DateTime CaptureTime     => _activity.CaptureTime;
    public int EnrollmentCount      => _activity.EnrollmentCount;
    public int ProcessedTopicsCount => _activity.ProcessedTopicsCount;
    public double AverageGrade      => _activity.AverageGrade;
    public string StatusName             => FormatStatus(_activity.Status);
    public ICourseStatusState CurrentState => _activity.CurrentState;

    internal DomainActivity Activity => _activity;

    public void RefreshStatus() => OnPropertyChanged(nameof(StatusName));

    private static string FormatStatus(CourseStatus s) => s switch
    {
        CourseStatus.DecliningInterest => "Declining Interest",
        _ => s.ToString()
    };
}
