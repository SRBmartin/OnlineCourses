using OnlineCourses.Contracts;
using OnlineCourses.DataProcessing.Adapters;
using OnlineCourses.DataProcessing.Export;
using OnlineCourses.DataProcessing.Models;
using OnlineCourses.DataProcessing.Strategies;

namespace OnlineCourses.DataProcessing.Services;

public class StatisticsProcessor
{
    private IStatisticalStrategy? _strategy;
    private readonly IActivityAdapter _adapter;
    private readonly IInformationSystemService _service;
    private Dictionary<string, List<ReducedActivity>> _data = new();
    private readonly ICsvExporter _csvExporter;

    public StatisticsProcessor(IInformationSystemService service, IActivityAdapter adapter, ICsvExporter csvExporter)
    {
        // DP-7: Assign all injected dependencies.
        _service     = service;
        _adapter     = adapter;
        _csvExporter = csvExporter;
    }

    public void SetStrategy(IStatisticalStrategy strategy)
    {
        // DP-8: Assign the chosen strategy (Strategy pattern).
        _strategy = strategy;
    }

    public string RunStatistics(Guid courseId, DateTime from, DateTime to)
    {
        // DP-7: Fetch activities from _service, adapt via _adapter, store in _data.
        // DP-8: Call _strategy.Calculate(_data) and return the result string.
        throw new NotImplementedException("DP-7 / DP-8");
    }

    public void ExportToCsv(string result, string filePath)
    {
        // DP-9: Delegate to _csvExporter.Export.
        _csvExporter.Export(result, filePath);
    }
}
