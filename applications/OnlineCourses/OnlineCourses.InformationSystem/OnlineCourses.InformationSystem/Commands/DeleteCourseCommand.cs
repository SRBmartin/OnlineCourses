using OnlineCourses.Contracts;
using OnlineCourses.InformationSystem.Repositories;

namespace OnlineCourses.InformationSystem.Commands;

public class DeleteCourseCommand : IUndoableCommand
{
    private Course _course;
    private ICourseRepository _repository;

    public void Execute()
    {
        throw new NotImplementedException();
    }

    public void Undo()
    {
        throw new NotImplementedException();
    }
}
