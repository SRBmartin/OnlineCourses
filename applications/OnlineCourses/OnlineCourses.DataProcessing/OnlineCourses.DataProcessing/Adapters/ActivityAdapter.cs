using OnlineCourses.Contracts;
using OnlineCourses.DataProcessing.Models;

namespace OnlineCourses.DataProcessing.Adapters;

public class ActivityAdapter : IActivityAdapter
{
    public Dictionary<string, List<ReducedActivity>> Adapt(List<ParticipantActivity> source, Guid courseId, DateTime from, DateTime to)
    {
        // DP-7: Group source activities into Dictionary<"CourseID-Date1-Date2", List<ReducedActivity>>.
        //        Map each ParticipantActivity to ReducedActivity (strip CourseId, keep all other fields).
        throw new NotImplementedException("DP-7");
    }
}
