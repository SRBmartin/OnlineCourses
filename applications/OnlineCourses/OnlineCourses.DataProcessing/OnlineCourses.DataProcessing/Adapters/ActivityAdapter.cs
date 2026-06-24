using OnlineCourses.Contracts;
using OnlineCourses.DataProcessing.Models;

namespace OnlineCourses.DataProcessing.Adapters;

public class ActivityAdapter : IActivityAdapter
{
    public Dictionary<string, List<ReducedActivity>> Adapt(List<ParticipantActivity> source, Guid courseId, DateTime from, DateTime to)
    {
        var key = $"{courseId}-{from:yyyy-MM-dd}-{to:yyyy-MM-dd}";
        var activities = source
            .Select(a => new ReducedActivity
            {
                CaptureTime          = a.CaptureTime,
                EnrollmentCount      = a.EnrollmentCount,
                ProcessedTopicsCount = a.ProcessedTopicsCount,
                AverageGrade         = a.AverageGrade,
                Status               = a.Status
            })
            .ToList();

        return new Dictionary<string, List<ReducedActivity>> { [key] = activities };
    }
}
