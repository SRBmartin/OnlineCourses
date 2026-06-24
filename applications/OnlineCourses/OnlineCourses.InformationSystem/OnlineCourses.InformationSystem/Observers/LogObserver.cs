using System.IO;

namespace OnlineCourses.InformationSystem.Observers;

public class LogObserver : IObserver
{
    private readonly string _logFilePath;

    public LogObserver(string logFilePath)
    {
        _logFilePath = logFilePath;
        var dir = Path.GetDirectoryName(logFilePath)!;
        Directory.CreateDirectory(dir);
    }

    public void Update(string message)
    {
        var entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
        File.AppendAllText(_logFilePath, entry);
    }
}
