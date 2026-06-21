using OnlineCourses.Contracts;

namespace OnlineCourses.DataProcessing.Models;

public class ReducedActivity
{
    public DateTime CaptureTime { get; set; }
    public int EnrollmentCount { get; set; }
    public int ProcessedTopicsCount { get; set; }
    public double AverageGrade { get; set; }
    public CourseStatus Status { get; set; }

    public ReducedActivity()
    {
        throw new NotImplementedException();
    }
}
