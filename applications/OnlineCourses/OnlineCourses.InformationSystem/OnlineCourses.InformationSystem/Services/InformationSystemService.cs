using OnlineCourses.Contracts;
using OnlineCourses.InformationSystem.Repositories;
using DomainActivity = OnlineCourses.InformationSystem.Models.ParticipantActivity;

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
        => _activityRepository.GetByCourseAndPeriod(courseId, from, to)
                              .Select(a => a.ToDto())
                              .ToList();

    public List<Course> GetAllCourses()
        => _courseRepository.GetAll();
}
