namespace OnlineCourses.Contracts;

public interface IInformationSystemService
{
    List<ParticipantActivity> GetActivities(Guid courseId, DateTime from, DateTime to);

    List<Course> GetAllCourses();
}
