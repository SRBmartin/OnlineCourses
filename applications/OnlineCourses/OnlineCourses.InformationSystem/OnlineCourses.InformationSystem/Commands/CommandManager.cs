namespace OnlineCourses.InformationSystem.Commands;

public class CommandManager
{
    private Stack<IUndoableCommand> _undoStack;
    private Stack<IUndoableCommand> _redoStack;

    public void ExecuteCommand(IUndoableCommand command)
    {
        throw new NotImplementedException();
    }

    public void Undo()
    {
        throw new NotImplementedException();
    }

    public void Redo()
    {
        throw new NotImplementedException();
    }
}
