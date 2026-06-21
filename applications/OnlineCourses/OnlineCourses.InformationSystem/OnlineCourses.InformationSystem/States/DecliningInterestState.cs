using OnlineCourses.Contracts;
using ParticipantActivity = OnlineCourses.InformationSystem.Models.ParticipantActivity;

namespace OnlineCourses.InformationSystem.States;

public class DecliningInterestState : ICourseStatusState
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
