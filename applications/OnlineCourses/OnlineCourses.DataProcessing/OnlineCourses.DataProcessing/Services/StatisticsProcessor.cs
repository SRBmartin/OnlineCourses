using OnlineCourses.Contracts;
using OnlineCourses.DataProcessing.Adapters;
using OnlineCourses.DataProcessing.Export;
using OnlineCourses.DataProcessing.Models;
using OnlineCourses.DataProcessing.Strategies;

namespace OnlineCourses.DataProcessing.Services;

public class StatisticsProcessor
{
    private IStatisticalStrategy _strategy;
    private IActivityAdapter _adapter;
    private IInformationSystemService _service;
    private Dictionary<string, List<ReducedActivity>> _data;
    private ICsvExporter _csvExporter;

    public StatisticsProcessor(IInformationSystemService service, IActivityAdapter adapter, ICsvExporter csvExporter)
    {
        throw new NotImplementedException();
    }

    public void SetStrategy(IStatisticalStrategy strategy)
    {
        throw new NotImplementedException();
    }

    public string RunStatistics(Guid courseId, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }

    public void ExportToCsv(string result, string filePath)
    {
        throw new NotImplementedException();
    }
}
