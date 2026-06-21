namespace OnlineCourses.DataProcessing.Export;

public interface ICsvExporter
{
    void Export(string content, string filePath);
}
