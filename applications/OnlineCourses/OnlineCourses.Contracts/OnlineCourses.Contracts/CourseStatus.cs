using System.Runtime.Serialization;

namespace OnlineCourses.Contracts;

[DataContract]
public enum CourseStatus
{
    [EnumMember] Popular,
    [EnumMember] DecliningInterest,
    [EnumMember] Recommended,
    [EnumMember] Archived
}
