namespace OnlineCourses.Contracts;

public class ParticipantActivity
{
    private ICourseStatusState _state;
    private List<IObserver> _observers;

    public Guid CourseId { get; set; }
    public DateTime CaptureTime { get; set; }
    public int EnrollmentCount { get; set; }
    public int ProcessedTopicsCount { get; set; }
    public double AverageGrade { get; set; }
    public CourseStatus Status { get; }

    public ParticipantActivity()
    {
        throw new NotImplementedException();
    }

    public void ChangeState(ICourseStatusState newState)
    {
        throw new NotImplementedException();
    }

    public void Subscribe(IObserver observer)
    {
        throw new NotImplementedException();
    }

    public void Unsubscribe(IObserver observer)
    {
        throw new NotImplementedException();
    }

    public void NotifyObservers(string message)
    {
        throw new NotImplementedException();
    }
}
