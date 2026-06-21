using OnlineCourses.Contracts;
using ParticipantActivity = OnlineCourses.InformationSystem.Models.ParticipantActivity;

namespace OnlineCourses.InformationSystem.Persistence;

public interface IDataPersistence
{
    void SaveCourses(List<Course> courses);

    List<Course> LoadCourses();

    void SaveActivities(List<ParticipantActivity> activities);

    List<ParticipantActivity> LoadActivities();
}
