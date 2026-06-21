using OnlineCourses.InformationSystem.Models;
using OnlineCourses.InformationSystem.Persistence;

namespace OnlineCourses.InformationSystem.Repositories;

public class ActivityRepository : IActivityRepository
{
    private List<ParticipantActivity> _activities;
    private IDataPersistence _persistence;

    public void Add(ParticipantActivity activity)
    {
        throw new NotImplementedException();
    }

    public void Update(ParticipantActivity activity)
    {
        throw new NotImplementedException();
    }

    public void Remove(ParticipantActivity activity)
    {
        throw new NotImplementedException();
    }

    public List<ParticipantActivity> GetAll()
    {
        throw new NotImplementedException();
    }

    public List<ParticipantActivity> GetByCourseAndPeriod(Guid courseId, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }
}
