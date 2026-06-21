using OnlineCourses.InformationSystem.Models;
using OnlineCourses.InformationSystem.Repositories;

namespace OnlineCourses.InformationSystem.Commands;

public class AddActivityCommand : IUndoableCommand
{
    private ParticipantActivity _activity;
    private IActivityRepository _repository;

    public void Execute()
    {
        throw new NotImplementedException();
    }

    public void Undo()
    {
        throw new NotImplementedException();
    }
}
