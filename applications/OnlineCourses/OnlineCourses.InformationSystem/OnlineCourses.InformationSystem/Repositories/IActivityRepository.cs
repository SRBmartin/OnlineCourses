using OnlineCourses.InformationSystem.Models;

namespace OnlineCourses.InformationSystem.Repositories;

public interface IActivityRepository
{
    void Add(ParticipantActivity activity);

    void AddRange(IEnumerable<ParticipantActivity> activities);

    void Update(ParticipantActivity activity);

    void Remove(ParticipantActivity activity);

    void RemoveRange(IEnumerable<ParticipantActivity> activities);

    List<ParticipantActivity> GetAll();

    List<ParticipantActivity> GetByCourseAndPeriod(Guid courseId, DateTime from, DateTime to);
}
