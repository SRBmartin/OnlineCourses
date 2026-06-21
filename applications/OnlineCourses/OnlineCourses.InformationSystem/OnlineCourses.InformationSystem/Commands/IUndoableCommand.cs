namespace OnlineCourses.InformationSystem.Commands;

public interface IUndoableCommand
{
    void Execute();

    void Undo();
}
