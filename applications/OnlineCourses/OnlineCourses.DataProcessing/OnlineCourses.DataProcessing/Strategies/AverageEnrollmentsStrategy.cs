using System.Text;
using OnlineCourses.DataProcessing.Models;
using OnlineCourses.DataProcessing.Services;

namespace OnlineCourses.DataProcessing.Strategies;

public class AverageEnrollmentsStrategy : IStatisticalStrategy
{
    private readonly IStatisticsCalculator _calculator;

    public AverageEnrollmentsStrategy(IStatisticsCalculator calculator)
    {
        _calculator = calculator;
    }

    public string Calculate(Dictionary<string, List<ReducedActivity>> data)
    {
        if (data.Values.All(v => v.Count == 0))
            return "No data available.";

        var sb = new StringBuilder();
        sb.AppendLine("Average Enrollments");
        foreach (var (key, activities) in data)
        {
            if (activities.Count == 0) continue;
            double avgEnrollments = _calculator.GetAverageEnrollments(activities);
            sb.AppendLine($"{key}:");
            sb.AppendLine($"  Avg Enrollments: {avgEnrollments:F2}");
        }
        return sb.ToString().TrimEnd();
    }

    public string CalculateCsv(Dictionary<string, List<ReducedActivity>> data)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Period,AvgEnrollments");
        foreach (var (key, activities) in data)
        {
            if (activities.Count == 0) continue;
            double avgEnrollments = _calculator.GetAverageEnrollments(activities);
            sb.AppendLine($"{key},{avgEnrollments:F2}");
        }
        return sb.ToString().TrimEnd();
    }
}
