using OnlineCourses.Contracts;
using OnlineCourses.InformationSystem.Repositories;
using DomainActivity = OnlineCourses.InformationSystem.Models.ParticipantActivity;

namespace OnlineCourses.InformationSystem.Commands;

public class DeleteCourseCommand : IUndoableCommand
{
    private readonly Course _course;
    private readonly ICourseRepository _courseRepository;
    private readonly IActivityRepository _activityRepository;
    private List<DomainActivity>? _deletedActivities;

    public DeleteCourseCommand(Course course,
        ICourseRepository courseRepository,
        IActivityRepository activityRepository)
    {
        _course             = course;
        _courseRepository   = courseRepository;
        _activityRepository = activityRepository;
    }

    public void Execute()
    {
        _deletedActivities = _activityRepository.GetAll()
            .Where(a => a.CourseId == _course.Id)
            .ToList();

        _activityRepository.RemoveRange(_deletedActivities);
        _courseRepository.Remove(_course.Id);
    }

    public void Undo()
    {
        _courseRepository.Add(_course);

        if (_deletedActivities != null)
            _activityRepository.AddRange(_deletedActivities);
    }
}
