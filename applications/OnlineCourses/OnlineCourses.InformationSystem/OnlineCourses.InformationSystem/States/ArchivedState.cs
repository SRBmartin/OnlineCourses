using OnlineCourses.Contracts;
using ParticipantActivity = OnlineCourses.InformationSystem.Models.ParticipantActivity;

namespace OnlineCourses.InformationSystem.States;

public class ArchivedState : ICourseStatusState
{
    public CourseStatus GetStatus() => CourseStatus.Archived;

    public void HandleState(ParticipantActivity context) { }
}
