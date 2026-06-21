using OnlineCourses.Contracts;
using OnlineCourses.InformationSystem.Persistence;

namespace OnlineCourses.InformationSystem.Repositories;

public class CourseRepository : ICourseRepository
{
    private List<Course> _courses;
    private IDataPersistence _persistence;

    public void Add(Course course)
    {
        throw new NotImplementedException();
    }

    public void Update(Course course)
    {
        throw new NotImplementedException();
    }

    public void Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<Course> GetAll()
    {
        throw new NotImplementedException();
    }

    public Course GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}
