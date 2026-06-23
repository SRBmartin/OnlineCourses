using OnlineCourses.DataProcessing.Models;

namespace OnlineCourses.DataProcessing.Strategies;

public class RecommendedCountStrategy : IStatisticalStrategy
{
    public string Calculate(Dictionary<string, List<ReducedActivity>> data)
    {
        // DP-8: For each key in data, count entries where Status == CourseStatus.Recommended.
        //        Return the count per period key as a formatted string.
        throw new NotImplementedException("DP-8");
    }
}
