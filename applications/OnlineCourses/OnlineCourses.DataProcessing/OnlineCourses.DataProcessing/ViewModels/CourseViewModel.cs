using OnlineCourses.Contracts;

namespace OnlineCourses.DataProcessing.ViewModels;

public class CourseViewModel : ViewModelBase
{
    private readonly Course _course;

    public CourseViewModel(Course course) => _course = course;

    public Guid   Id       => _course.Id;
    public string Name     => _course.Name;
    public string Field    => _course.Field;
    public string Lecturer => _course.Lecturer;
}
