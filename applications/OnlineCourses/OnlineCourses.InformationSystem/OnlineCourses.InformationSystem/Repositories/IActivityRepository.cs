using OnlineCourses.Contracts;

namespace OnlineCourses.InformationSystem.Repositories;

public interface IActivityRepository
{
    void Add(ParticipantActivity activity);

    void Update(ParticipantActivity activity);

    void Remove(ParticipantActivity activity);

    List<ParticipantActivity> GetAll();

    List<ParticipantActivity> GetByCourseAndPeriod(Guid courseId, DateTime from, DateTime to);
}
