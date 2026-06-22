// IS-3: Implement ActivityViewModel wrapping InformationSystem.Models.ParticipantActivity.
// IS-3: Properties — CourseId (Guid), CaptureTime (DateTime), EnrollmentCount (int),
//        ProcessedTopicsCount (int), AverageGrade (double), StatusName (string).
// IS-3: StatusName formats CourseStatus enum: e.g. DecliningInterest → "Declining Interest".
// IS-3: Add static FromActivity(ParticipantActivity) factory and ToActivity() method.

namespace OnlineCourses.InformationSystem.ViewModels;

public class ActivityViewModel : ViewModelBase
{
    // IS-3: Properties go here
}
