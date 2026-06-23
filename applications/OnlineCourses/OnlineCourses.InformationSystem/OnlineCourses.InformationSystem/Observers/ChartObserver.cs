using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using OnlineCourses.Contracts;
using SkiaSharp;
using DomainActivity = OnlineCourses.InformationSystem.Models.ParticipantActivity;

namespace OnlineCourses.InformationSystem.Observers;

public class ChartObserver : IObserver
{
    private readonly List<DomainActivity> _trackedActivities = new();

    private readonly ObservableValue _popularValue     = new(0);
    private readonly ObservableValue _decliningValue   = new(0);
    private readonly ObservableValue _recommendedValue = new(0);
    private readonly ObservableValue _archivedValue    = new(0);

    public ISeries[] Series { get; }
    public Axis[]    XAxes  { get; }
    public Axis[]    YAxes  { get; }

    public ChartObserver()
    {
        Series = new ISeries[]
        {
            new ColumnSeries<ObservableValue>
            {
                Values = new[] { _popularValue, _decliningValue, _recommendedValue, _archivedValue },
                Name   = "Activities",
                Fill   = new SolidColorPaint(new SKColor(79, 129, 189))
            }
        };

        XAxes = new[]
        {
            new Axis
            {
                Labels         = new[] { "Popular", "Declining", "Recommended", "Archived" },
                LabelsRotation = 0
            }
        };

        YAxes = new[]
        {
            new Axis
            {
                MinLimit = 0
            }
        };
    }

    public void SetActivities(IEnumerable<DomainActivity> activities)
    {
        _trackedActivities.Clear();
        _trackedActivities.AddRange(activities);
        Recount();
    }

    public void Update(string message) => Recount();

    private void Recount()
    {
        var popular     = 0;
        var declining   = 0;
        var recommended = 0;
        var archived    = 0;

        foreach (var a in _trackedActivities)
        {
            switch (a.Status)
            {
                case CourseStatus.Popular:           popular++;     break;
                case CourseStatus.DecliningInterest: declining++;   break;
                case CourseStatus.Recommended:       recommended++; break;
                case CourseStatus.Archived:          archived++;    break;
            }
        }

        _popularValue.Value     = popular;
        _decliningValue.Value   = declining;
        _recommendedValue.Value = recommended;
        _archivedValue.Value    = archived;
    }
}
