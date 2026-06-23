using OnlineCourses.DataProcessing.Models;

namespace OnlineCourses.DataProcessing.Strategies;

public class AverageEnrollmentsStrategy : IStatisticalStrategy
{
    public string Calculate(Dictionary<string, List<ReducedActivity>> data)
    {
        // DP-8: For each key in data, compute the average EnrollmentCount across all entries.
        //        Return results as a formatted string grouped by period key.
        throw new NotImplementedException("DP-8");
    }
}
