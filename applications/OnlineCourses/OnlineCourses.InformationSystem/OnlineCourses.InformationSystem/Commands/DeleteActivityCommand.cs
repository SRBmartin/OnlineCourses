using OnlineCourses.InformationSystem.Models;
using OnlineCourses.InformationSystem.Repositories;

namespace OnlineCourses.InformationSystem.Commands;

public class DeleteActivityCommand : IUndoableCommand
{
    private readonly ParticipantActivity _activity;
    private readonly IActivityRepository _repository;

    public DeleteActivityCommand(ParticipantActivity activity, IActivityRepository repository)
    {
        _activity   = activity;
        _repository = repository;
    }

    public void Execute() => _repository.Remove(_activity);

    public void Undo() => _repository.Add(_activity);
}
