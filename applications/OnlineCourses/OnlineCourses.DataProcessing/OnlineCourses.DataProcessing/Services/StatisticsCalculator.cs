using OnlineCourses.Contracts;
using OnlineCourses.DataProcessing.Models;

namespace OnlineCourses.DataProcessing.Services;

public class StatisticsCalculator : IStatisticsCalculator
{
    public int GetMinTopics(IEnumerable<ReducedActivity> activities) =>
        activities.Min(a => a.ProcessedTopicsCount);

    public double GetAverageGrade(IEnumerable<ReducedActivity> activities) =>
        activities.Average(a => a.AverageGrade);

    public double GetAverageEnrollments(IEnumerable<ReducedActivity> activities) =>
        activities.Average(a => a.EnrollmentCount);

    public int GetRecommendedCount(IEnumerable<ReducedActivity> activities) =>
        activities.Count(a => a.Status == CourseStatus.Recommended);
}
