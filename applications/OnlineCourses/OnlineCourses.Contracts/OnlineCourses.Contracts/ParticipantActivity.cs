namespace OnlineCourses.Contracts;

public class ParticipantActivity
{
    private Guid courseId;
    private DateTime captureTime;
    private int enrollmentCount;
    private int processedTopicsCount;
    private double averageGrade;
    private ICourseStatusState _state;
    private List<IObserver> _observers;

    public Guid CourseId
    {
        get { throw new NotImplementedException(); }
        set { throw new NotImplementedException(); }
    }

    public DateTime CaptureTime
    {
        get { throw new NotImplementedException(); }
        set { throw new NotImplementedException(); }
    }

    public int EnrollmentCount
    {
        get { throw new NotImplementedException(); }
        set { throw new NotImplementedException(); }
    }

    public int ProcessedTopicsCount
    {
        get { throw new NotImplementedException(); }
        set { throw new NotImplementedException(); }
    }

    public double AverageGrade
    {
        get { throw new NotImplementedException(); }
        set { throw new NotImplementedException(); }
    }

    public CourseStatus Status
    {
        get { throw new NotImplementedException(); }
    }

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
