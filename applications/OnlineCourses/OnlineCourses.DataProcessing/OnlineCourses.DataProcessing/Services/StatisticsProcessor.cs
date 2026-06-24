using OnlineCourses.DataProcessing.Export;
using OnlineCourses.DataProcessing.Models;
using OnlineCourses.DataProcessing.Strategies;

namespace OnlineCourses.DataProcessing.Services;

public class StatisticsProcessor
{
    private IStatisticalStrategy? _strategy;
    private readonly ICsvExporter _csvExporter;

    public StatisticsProcessor(ICsvExporter csvExporter)
    {
        _csvExporter = csvExporter;
    }

    public void SetStrategy(IStatisticalStrategy strategy) => _strategy = strategy;

    public string RunStatistics(Dictionary<string, List<ReducedActivity>> data)
    {
        if (_strategy == null) return "No strategy selected.";
        return _strategy.Calculate(data);
    }

    public void ExportToCsv(Dictionary<string, List<ReducedActivity>> data, string filePath)
    {
        if (_strategy == null) return;
        _csvExporter.Export(_strategy.CalculateCsv(data), filePath);
    }
}
