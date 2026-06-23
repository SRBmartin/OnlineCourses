using OnlineCourses.DataProcessing.Models;

namespace OnlineCourses.DataProcessing.Strategies;

public class MinTopicsAndAvgGradeStrategy : IStatisticalStrategy
{
    public string Calculate(Dictionary<string, List<ReducedActivity>> data)
    {
        // DP-8: For each key in data, find the entry with the minimum ProcessedTopicsCount
        //        and compute the average AverageGrade across all entries. Return as formatted string.
        throw new NotImplementedException("DP-8");
    }
}
