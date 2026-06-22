namespace OnlineCourses.InformationSystem.Commands;

public class CommandManager
{
    private readonly Stack<IUndoableCommand> _undoStack = new();
    private readonly Stack<IUndoableCommand> _redoStack = new();

    public bool CanUndo => _undoStack.Count > 0;
    public bool CanRedo => _redoStack.Count > 0;

    public event Action? HistoryChanged;

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
