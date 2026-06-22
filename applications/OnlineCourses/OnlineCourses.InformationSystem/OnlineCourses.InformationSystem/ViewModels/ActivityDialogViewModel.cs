// IS-4: Implement ActivityDialogViewModel.
// IS-4: Constructor(string title, Action close, IEnumerable<CourseViewModel> courses,
//        ActivityViewModel? existing = null)
// IS-4: Properties — Courses (ObservableCollection), SelectedCourse, CaptureTime (DateTime),
//        EnrollmentCount (string, validated int >= 0), ProcessedTopicsCount (string, validated int >= 0),
//        AverageGrade (string, validated double 0–10), ValidationError.
// IS-4: SaveCommand — validates all fields; sets Confirmed = true and calls _close().
// IS-4: CancelCommand — calls _close().
// IS-4: ToActivityViewModel() — returns a new ActivityViewModel with entered values.

namespace OnlineCourses.InformationSystem.ViewModels;

public class ActivityDialogViewModel : ViewModelBase
{
    // IS-4: Properties and commands go here
}
