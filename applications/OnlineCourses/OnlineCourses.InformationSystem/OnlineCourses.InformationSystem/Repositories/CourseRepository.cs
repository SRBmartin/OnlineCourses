using OnlineCourses.Contracts;
using OnlineCourses.InformationSystem.Persistence;

namespace OnlineCourses.InformationSystem.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly IEntityStore<Course> _store;

    public CourseRepository(IEntityStore<Course> store)
    {
        _store = store;
    }

    public void Add(Course course)
    {
        _store.Add(course);
        _store.Save();
    }

    public void Update(Course course)
    {
        var existing = _store.Items.FirstOrDefault(c => c.Id == course.Id)
            ?? throw new InvalidOperationException($"Course {course.Id} not found.");
        _store.Remove(existing);
        _store.Add(course);
        _store.Save();
    }

    public void Remove(Guid id)
    {
        var existing = _store.Items.FirstOrDefault(c => c.Id == id)
            ?? throw new InvalidOperationException($"Course {id} not found.");
        _store.Remove(existing);
        _store.Save();
    }

    public List<Course> GetAll() => _store.Items.ToList();

    public Course GetById(Guid id) =>
        _store.Items.FirstOrDefault(c => c.Id == id)
            ?? throw new InvalidOperationException($"Course {id} not found.");
}
