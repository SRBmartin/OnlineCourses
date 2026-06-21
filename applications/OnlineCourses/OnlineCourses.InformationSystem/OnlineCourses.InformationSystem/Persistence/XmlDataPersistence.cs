using OnlineCourses.Contracts;
using ParticipantActivity = OnlineCourses.InformationSystem.Models.ParticipantActivity;

namespace OnlineCourses.InformationSystem.Persistence;

public class XmlDataPersistence : IDataPersistence
{
    public void SaveCourses(List<Course> courses)
    {
        throw new NotImplementedException();
    }

    public List<Course> LoadCourses()
    {
        throw new NotImplementedException();
    }

    public void SaveActivities(List<ParticipantActivity> activities)
    {
        throw new NotImplementedException();
    }

    public List<ParticipantActivity> LoadActivities()
    {
        throw new NotImplementedException();
    }
}
