using System.Runtime.Serialization;

namespace OnlineCourses.Contracts;

[DataContract]
public class ParticipantActivity
{
    [DataMember] public Guid CourseId { get; set; }
    [DataMember] public DateTime CaptureTime { get; set; }
    [DataMember] public int EnrollmentCount { get; set; }
    [DataMember] public int ProcessedTopicsCount { get; set; }
    [DataMember] public double AverageGrade { get; set; }
    [DataMember] public CourseStatus Status { get; set; }
}
