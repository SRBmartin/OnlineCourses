using OnlineCourses.Contracts;
using ParticipantActivity = OnlineCourses.InformationSystem.Models.ParticipantActivity;

namespace OnlineCourses.InformationSystem.States;

public class DecliningInterestState : ICourseStatusState
{
    public CourseStatus GetStatus() => CourseStatus.DecliningInterest;

    public void HandleState(ParticipantActivity context)
        => context.ChangeState(new RecommendedState());
}
