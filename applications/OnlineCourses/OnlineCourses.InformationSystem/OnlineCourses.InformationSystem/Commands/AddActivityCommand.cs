using OnlineCourses.InformationSystem.Models;
using OnlineCourses.InformationSystem.Repositories;

namespace OnlineCourses.InformationSystem.Commands;

public class AddActivityCommand : IUndoableCommand
{
    private readonly ParticipantActivity _activity;
    private readonly IActivityRepository _repository;

    public AddActivityCommand(ParticipantActivity activity, IActivityRepository repository)
    {
        _activity   = activity;
        _repository = repository;
    }

    public void Execute() => _repository.Add(_activity);

    public void Undo() => _repository.Remove(_activity);
}
