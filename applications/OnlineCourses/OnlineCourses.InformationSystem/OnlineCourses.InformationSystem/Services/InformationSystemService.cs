using OnlineCourses.Contracts;
using OnlineCourses.InformationSystem.Repositories;

namespace OnlineCourses.InformationSystem.Services;

public class InformationSystemService : IInformationSystemService
{
    private readonly ICourseRepository   _courseRepository;
    private readonly IActivityRepository _activityRepository;

    public InformationSystemService(ICourseRepository courseRepository, IActivityRepository activityRepository)
    {
        _courseRepository   = courseRepository;
        _activityRepository = activityRepository;
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
