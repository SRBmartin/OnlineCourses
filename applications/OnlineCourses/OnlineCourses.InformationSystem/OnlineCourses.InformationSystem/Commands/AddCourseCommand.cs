using OnlineCourses.Contracts;
using OnlineCourses.InformationSystem.Repositories;

namespace OnlineCourses.InformationSystem.Commands;

public class AddCourseCommand : IUndoableCommand
{
    private readonly Course _course;
    private readonly ICourseRepository _repository;

    public AddCourseCommand(Course course, ICourseRepository repository)
    {
        _course = course;
        _repository = repository;
    }

    public void Execute() => _repository.Add(_course);

    public void Undo() => _repository.Remove(_course.Id);
}
