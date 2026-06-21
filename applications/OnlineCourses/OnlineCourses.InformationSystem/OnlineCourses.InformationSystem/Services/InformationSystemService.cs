using OnlineCourses.Contracts;
using OnlineCourses.InformationSystem.Repositories;

namespace OnlineCourses.InformationSystem.Services;

public class InformationSystemService : IInformationSystemService
{
    private ICourseRepository _courseRepository;
    private IActivityRepository _activityRepository;

    public InformationSystemService(ICourseRepository courseRepository, IActivityRepository activityRepository)
    {
        throw new NotImplementedException();
    }

    public List<ParticipantActivity> GetActivities(Guid courseId, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }

    public List<Course> GetAllCourses()
    {
        throw new NotImplementedException();
    }
}
