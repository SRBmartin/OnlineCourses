using System.Runtime.Serialization;

namespace OnlineCourses.Contracts;

[DataContract]
public class Course
{
    [DataMember] public Guid Id { get; set; }
    [DataMember] public string Name { get; set; }
    [DataMember] public string Field { get; set; }
    [DataMember] public string Lecturer { get; set; }
}
