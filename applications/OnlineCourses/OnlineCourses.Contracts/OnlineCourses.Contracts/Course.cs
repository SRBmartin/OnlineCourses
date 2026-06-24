using System.Runtime.Serialization;

namespace OnlineCourses.Contracts;

[DataContract]
public class Course
{
    [DataMember] public Guid Id { get; set; }
    [DataMember] public string Name { get; set; } = string.Empty;
    [DataMember] public string Field { get; set; } = string.Empty;
    [DataMember] public string Lecturer { get; set; } = string.Empty;
}
