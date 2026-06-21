using OnlineCourses.Contracts;

namespace OnlineCourses.InformationSystem.States;

public class ArchivedState : ICourseStatusState
{
    public void HandleState(ParticipantActivity context)
    {
        throw new NotImplementedException();
    }

    public CourseStatus GetStatus()
    {
        throw new NotImplementedException();
    }
}
