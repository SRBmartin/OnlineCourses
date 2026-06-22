using OnlineCourses.Contracts;
using OnlineCourses.InformationSystem.Repositories;

namespace OnlineCourses.InformationSystem.Commands;

public class DeleteCourseCommand : IUndoableCommand
{
    private readonly Course _course;
    private readonly ICourseRepository _repository;

    public DeleteCourseCommand(Course course, ICourseRepository repository)
    {
        _course = course;
        _repository = repository;
    }

    public void Execute() => _repository.Remove(_course.Id);

    public void Undo() => _repository.Add(_course);
}
