using OnlineCourses.Contracts;
using OnlineCourses.InformationSystem.Repositories;

namespace OnlineCourses.InformationSystem.Commands;

public class EditCourseCommand : IUndoableCommand
{
    private readonly Course _oldCourse;
    private readonly Course _newCourse;
    private readonly ICourseRepository _repository;

    public EditCourseCommand(Course oldCourse, Course newCourse, ICourseRepository repository)
    {
        _oldCourse = oldCourse;
        _newCourse = newCourse;
        _repository = repository;
    }

    public void Execute() => _repository.Update(_newCourse);

    public void Undo() => _repository.Update(_oldCourse);
}
