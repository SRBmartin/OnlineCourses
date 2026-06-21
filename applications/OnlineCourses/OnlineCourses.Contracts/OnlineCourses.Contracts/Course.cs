namespace OnlineCourses.Contracts;

public class Course
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Field { get; set; }
    public string Lecturer { get; set; }

    public Course()
    {
        throw new NotImplementedException();
    }
}
