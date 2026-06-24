using System.Text;
using OnlineCourses.Contracts;
using OnlineCourses.DataProcessing.Models;
using OnlineCourses.DataProcessing.Services;

namespace OnlineCourses.DataProcessing.Strategies;

public class RecommendedCountStrategy : IStatisticalStrategy
{
    private readonly IStatisticsCalculator _calculator;

    public RecommendedCountStrategy(IStatisticsCalculator calculator)
    {
        _calculator = calculator;
    }

    public string Calculate(Dictionary<string, List<ReducedActivity>> data)
    {
        if (data.Values.All(v => v.Count == 0))
            return "No data available.";

        var sb = new StringBuilder();
        sb.AppendLine("Recommended Count");
        foreach (var (key, activities) in data)
        {
            int count = _calculator.GetRecommendedCount(activities);
            sb.AppendLine($"{key}:");
            sb.AppendLine($"  Recommended: {count} time(s)");
        }
        return sb.ToString().TrimEnd();
    }

    public string CalculateCsv(Dictionary<string, List<ReducedActivity>> data)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Period,RecommendedCount");
        foreach (var (key, activities) in data)
        {
            int count = _calculator.GetRecommendedCount(activities);
            sb.AppendLine($"{key},{count}");
        }
        return sb.ToString().TrimEnd();
    }
}
