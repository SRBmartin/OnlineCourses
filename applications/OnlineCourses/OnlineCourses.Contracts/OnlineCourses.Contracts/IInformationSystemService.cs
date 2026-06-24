using System.ServiceModel;

namespace OnlineCourses.Contracts;

[ServiceContract]
public interface IInformationSystemService
{
    [OperationContract]
    List<ParticipantActivity> GetActivities(Guid courseId, DateTime from, DateTime to);

    [OperationContract]
    List<Course> GetAllCourses();
}
