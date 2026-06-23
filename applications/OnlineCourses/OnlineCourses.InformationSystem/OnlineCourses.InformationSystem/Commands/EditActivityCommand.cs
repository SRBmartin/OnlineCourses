using OnlineCourses.InformationSystem.Models;
using OnlineCourses.InformationSystem.Repositories;

namespace OnlineCourses.InformationSystem.Commands;

public class EditActivityCommand : IUndoableCommand
{
    private readonly ParticipantActivity _oldActivity;
    private readonly ParticipantActivity _newActivity;
    private readonly IActivityRepository _repository;

    public EditActivityCommand(ParticipantActivity oldActivity, ParticipantActivity newActivity,
        IActivityRepository repository)
    {
        _oldActivity = oldActivity;
        _newActivity = newActivity;
        _repository  = repository;
    }

    public void Execute() => _repository.Update(_newActivity);

    public void Undo() => _repository.Update(_oldActivity);
}
