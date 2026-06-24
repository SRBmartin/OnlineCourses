using OnlineCourses.InformationSystem.Persistence;
using DomainActivity = OnlineCourses.InformationSystem.Models.ParticipantActivity;
using DtoActivity    = OnlineCourses.Contracts.ParticipantActivity;

namespace OnlineCourses.InformationSystem.Repositories;

public class ActivityRepository : IActivityRepository
{
    private readonly IEntityStore<DtoActivity> _store;

    public ActivityRepository(IEntityStore<DtoActivity> store)
    {
        _store = store;
    }

    public void Add(DomainActivity activity)
    {
        _store.Add(activity.ToDto());
        _store.Save();
    }

    public void AddRange(IEnumerable<DomainActivity> activities)
    {
        foreach (var activity in activities)
            _store.Add(activity.ToDto());
        _store.Save();
    }

    public void Update(DomainActivity activity)
    {
        _store.Remove(FindOrThrow(activity.Id));
        _store.Add(activity.ToDto());
        _store.Save();
    }

    public void Remove(DomainActivity activity)
    {
        var existing = FindOrThrow(activity.Id);
        _store.Remove(existing);
        _store.Save();
    }

    public void RemoveRange(IEnumerable<DomainActivity> activities)
    {
        foreach (var activity in activities)
            _store.Remove(FindOrThrow(activity.Id));
        _store.Save();
    }

    private DtoActivity FindOrThrow(Guid id)
        => _store.Items.FirstOrDefault(a => a.Id == id)
           ?? throw new InvalidOperationException($"Activity {id} not found.");

    public List<DomainActivity> GetAll()
        => _store.Items.Select(DomainActivity.FromDto).ToList();

    public List<DomainActivity> GetByCourseAndPeriod(Guid courseId, DateTime from, DateTime to)
        => _store.Items
            .Where(a => a.CourseId == courseId && a.CaptureTime >= from && a.CaptureTime <= to)
            .Select(DomainActivity.FromDto)
            .ToList();
}
