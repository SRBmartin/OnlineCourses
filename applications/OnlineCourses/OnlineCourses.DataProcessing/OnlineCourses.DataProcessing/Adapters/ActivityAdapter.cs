using OnlineCourses.Contracts;
using OnlineCourses.DataProcessing.Models;

namespace OnlineCourses.DataProcessing.Adapters;

public class ActivityAdapter : IActivityAdapter
{
    public Dictionary<string, List<ReducedActivity>> Adapt(List<ParticipantActivity> source, Guid courseId, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }
}
