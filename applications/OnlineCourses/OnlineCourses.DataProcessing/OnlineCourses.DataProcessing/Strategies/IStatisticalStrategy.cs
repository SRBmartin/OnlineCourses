using OnlineCourses.DataProcessing.Models;

namespace OnlineCourses.DataProcessing.Strategies;

public interface IStatisticalStrategy
{
    string Calculate(Dictionary<string, List<ReducedActivity>> data);
}
