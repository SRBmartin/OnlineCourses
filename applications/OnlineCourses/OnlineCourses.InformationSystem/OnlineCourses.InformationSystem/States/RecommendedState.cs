using OnlineCourses.Contracts;
using ParticipantActivity = OnlineCourses.InformationSystem.Models.ParticipantActivity;

namespace OnlineCourses.InformationSystem.States;

public class RecommendedState : ICourseStatusState
{
    public CourseStatus GetStatus() => CourseStatus.Recommended;

    public void HandleState(ParticipantActivity context)
        => context.ChangeState(new ArchivedState());
}
