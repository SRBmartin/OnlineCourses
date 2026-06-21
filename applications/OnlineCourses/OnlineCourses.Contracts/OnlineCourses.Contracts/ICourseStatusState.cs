namespace OnlineCourses.Contracts;

public interface ICourseStatusState
{
    void HandleState(ParticipantActivity context);

    CourseStatus GetStatus();
}
