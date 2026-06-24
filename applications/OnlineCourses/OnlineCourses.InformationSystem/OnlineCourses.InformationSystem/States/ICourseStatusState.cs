using OnlineCourses.Contracts;
using ParticipantActivity = OnlineCourses.InformationSystem.Models.ParticipantActivity;

namespace OnlineCourses.InformationSystem.States;

public interface ICourseStatusState
{
    void HandleState(ParticipantActivity context);
    CourseStatus GetStatus();
}
