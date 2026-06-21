using OnlineCourses.Contracts;

namespace OnlineCourses.InformationSystem.Repositories;

public interface ICourseRepository
{
    void Add(Course course);

    void Update(Course course);

    void Remove(Guid id);

    List<Course> GetAll();

    Course GetById(Guid id);
}
