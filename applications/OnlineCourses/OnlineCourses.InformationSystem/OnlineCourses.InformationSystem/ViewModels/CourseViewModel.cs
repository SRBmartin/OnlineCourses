using OnlineCourses.Contracts;

namespace OnlineCourses.InformationSystem.ViewModels;

public class CourseViewModel : ViewModelBase
{
    private Guid _id;
    private string _name = string.Empty;
    private string _field = string.Empty;
    private string _lecturer = string.Empty;

    public Guid Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public string Field
    {
        get => _field;
        set => SetField(ref _field, value);
    }

    public string Lecturer
    {
        get => _lecturer;
        set => SetField(ref _lecturer, value);
    }

    public CourseViewModel() { }

    public CourseViewModel(Course course)
    {
        _id = course.Id;
        _name = course.Name;
        _field = course.Field;
        _lecturer = course.Lecturer;
    }

    public Course ToModel() => new() { Id = _id, Name = _name, Field = _field, Lecturer = _lecturer };
}
