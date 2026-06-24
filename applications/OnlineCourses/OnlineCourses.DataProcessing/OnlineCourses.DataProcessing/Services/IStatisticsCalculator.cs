using OnlineCourses.DataProcessing.Models;

namespace OnlineCourses.DataProcessing.Services;

public interface IStatisticsCalculator
{
    int GetMinTopics(IEnumerable<ReducedActivity> activities);
    double GetAverageGrade(IEnumerable<ReducedActivity> activities);
    double GetAverageEnrollments(IEnumerable<ReducedActivity> activities);
    int GetRecommendedCount(IEnumerable<ReducedActivity> activities);
}
