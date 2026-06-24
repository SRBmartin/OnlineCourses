using System.IO;
using System.Text;

namespace OnlineCourses.DataProcessing.Export;

public class CsvExporter : ICsvExporter
{
    public void Export(string content, string filePath)
    {
        File.WriteAllText(filePath, content, Encoding.UTF8);
    }
}
