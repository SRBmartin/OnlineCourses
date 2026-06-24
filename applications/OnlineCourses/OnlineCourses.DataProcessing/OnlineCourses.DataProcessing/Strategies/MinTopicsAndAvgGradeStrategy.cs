using System.Text;
using OnlineCourses.DataProcessing.Models;
using OnlineCourses.DataProcessing.Services;

namespace OnlineCourses.DataProcessing.Strategies;

public class MinTopicsAndAvgGradeStrategy : IStatisticalStrategy
{
    private readonly IStatisticsCalculator _calculator;

    public MinTopicsAndAvgGradeStrategy(IStatisticsCalculator calculator)
    {
        _calculator = calculator;
    }

    public string Calculate(Dictionary<string, List<ReducedActivity>> data)
    {
        if (data.Values.All(v => v.Count == 0))
            return "No data available.";

        var sb = new StringBuilder();
        sb.AppendLine("Min Topics & Avg Grade");
        foreach (var (key, activities) in data)
        {
            if (activities.Count == 0) continue;
            int minTopics = _calculator.GetMinTopics(activities);
            double avgGrade = _calculator.GetAverageGrade(activities);
            sb.AppendLine($"{key}:");
            sb.AppendLine($"  Min Topics: {minTopics}");
            sb.AppendLine($"  Avg Grade: {avgGrade:F2}");
        }
        return sb.ToString().TrimEnd();
    }

    public string CalculateCsv(Dictionary<string, List<ReducedActivity>> data)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Period,MinTopics,AvgGrade");
        foreach (var (key, activities) in data)
        {
            if (activities.Count == 0) continue;
            int minTopics = _calculator.GetMinTopics(activities);
            double avgGrade = _calculator.GetAverageGrade(activities);
            sb.AppendLine($"{key},{minTopics},{avgGrade:F2}");
        }
        return sb.ToString().TrimEnd();
    }
}
