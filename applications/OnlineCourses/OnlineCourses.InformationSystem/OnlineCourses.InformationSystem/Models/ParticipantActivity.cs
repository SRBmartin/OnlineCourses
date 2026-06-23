using OnlineCourses.Contracts;
using OnlineCourses.InformationSystem.Observers;
using OnlineCourses.InformationSystem.States;
using ContractActivity = OnlineCourses.Contracts.ParticipantActivity;

namespace OnlineCourses.InformationSystem.Models;

public class ParticipantActivity
{
    private ICourseStatusState _state;
    private readonly List<IObserver> _observers = new();

    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public DateTime CaptureTime { get; set; }
    public int EnrollmentCount { get; set; }
    public int ProcessedTopicsCount { get; set; }
    public double AverageGrade { get; set; }
    public CourseStatus Status => _state.GetStatus();
    public ICourseStatusState CurrentState => _state;

    public ParticipantActivity()
    {
        _state = new PopularState();
    }

    public ParticipantActivity(Guid id, Guid courseId, DateTime captureTime,
        int enrollmentCount, int processedTopicsCount, double averageGrade,
        ICourseStatusState initialState)
    {
        Id                   = id;
        CourseId             = courseId;
        CaptureTime          = captureTime;
        EnrollmentCount      = enrollmentCount;
        ProcessedTopicsCount = processedTopicsCount;
        AverageGrade         = averageGrade;
        _state               = initialState;
    }

    public void ChangeState(ICourseStatusState newState)
    {
        _state = newState;
        NotifyObservers($"Status changed to {_state.GetStatus()} for activity {Id}");
    }

    public void Subscribe(IObserver observer)   => _observers.Add(observer);
    public void Unsubscribe(IObserver observer) => _observers.Remove(observer);

    public void NotifyObservers(string message)
    {
        foreach (var observer in _observers)
            observer.Update(message);
    }

    public ContractActivity ToDto() => new ContractActivity
    {
        Id                   = Id,
        CourseId             = CourseId,
        CaptureTime          = CaptureTime,
        EnrollmentCount      = EnrollmentCount,
        ProcessedTopicsCount = ProcessedTopicsCount,
        AverageGrade         = AverageGrade,
        Status               = Status
    };

    public static ParticipantActivity FromDto(ContractActivity dto)
    {
        ICourseStatusState state = dto.Status switch
        {
            CourseStatus.DecliningInterest => new DecliningInterestState(),
            CourseStatus.Recommended       => new RecommendedState(),
            CourseStatus.Archived          => new ArchivedState(),
            _                              => new PopularState()
        };
        return new ParticipantActivity(dto.Id, dto.CourseId, dto.CaptureTime,
            dto.EnrollmentCount, dto.ProcessedTopicsCount, dto.AverageGrade, state);
    }
}
