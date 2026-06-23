using ICommand = System.Windows.Input.ICommand;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using OnlineCourses.InformationSystem.Commands;
using OnlineCourses.InformationSystem.Models;
using OnlineCourses.InformationSystem.Observers;
using OnlineCourses.InformationSystem.Repositories;
using OnlineCourses.InformationSystem.States;
using OnlineCourses.InformationSystem.Views.Dialogs;

namespace OnlineCourses.InformationSystem.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly ICourseRepository   _courseRepository;
    private readonly IActivityRepository _activityRepository;
    private readonly CommandManager      _commandManager;
    private readonly IObserver            _logObserver;
    private readonly ChartObserver        _chartObserver;

    private readonly ObservableCollection<CourseViewModel>   _courses    = new();
    private readonly ObservableCollection<ActivityViewModel> _activities = new();

    private CourseViewModel?   _selectedCourse;
    private ActivityViewModel? _selectedActivity;
    private string             _searchText = string.Empty;
    private bool               _isSimulating;

    public ICollectionView CoursesView    { get; }
    public ICollectionView ActivitiesView { get; }

    public string ActivitiesHeader =>
        _selectedCourse != null ? $"Activities — {_selectedCourse.Name}" : "Activities";

    public CourseViewModel? SelectedCourse
    {
        get => _selectedCourse;
        set
        {
            if (!SetField(ref _selectedCourse, value)) return;
            OnPropertyChanged(nameof(ActivitiesHeader));
            LoadActivities(value);
        }
    }

    public ActivityViewModel? SelectedActivity
    {
        get => _selectedActivity;
        set => SetField(ref _selectedActivity, value);
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (!SetField(ref _searchText, value)) return;
            CoursesView.Refresh();
        }
    }

    public ICommand AddCourseCommand    { get; }
    public ICommand EditCourseCommand   { get; }
    public ICommand DeleteCourseCommand { get; }
    public ICommand UndoCommand         { get; }
    public ICommand RedoCommand         { get; }

    public ICommand AddActivityCommand    { get; }
    public ICommand EditActivityCommand   { get; }
    public ICommand DeleteActivityCommand { get; }
    public ICommand SimulateStatesCommand { get; }

    public ISeries[] ChartSeries => _chartObserver.Series;
    public Axis[]    ChartXAxes  => _chartObserver.XAxes;
    public Axis[]    ChartYAxes  => _chartObserver.YAxes;

    public MainViewModel(ICourseRepository courseRepository, IActivityRepository activityRepository,
        CommandManager commandManager, IObserver logObserver, ChartObserver chartObserver)
    {
        _courseRepository   = courseRepository;
        _activityRepository = activityRepository;
        _commandManager     = commandManager;
        _logObserver        = logObserver;
        _chartObserver      = chartObserver;

        CoursesView    = CollectionViewSource.GetDefaultView(_courses);
        CoursesView.Filter = FilterCourse;

        ActivitiesView = CollectionViewSource.GetDefaultView(_activities);

        AddCourseCommand    = new RelayCommand(_ => ExecuteAddCourse());
        EditCourseCommand   = new RelayCommand(_ => ExecuteEditCourse(),   _ => _selectedCourse != null);
        DeleteCourseCommand = new RelayCommand(_ => ExecuteDeleteCourse(), _ => _selectedCourse != null);

        UndoCommand = new RelayCommand(_ =>
        {
            _commandManager.Undo();
            _logObserver.Update("Undo last action");
        }, _ => _commandManager.CanUndo);

        RedoCommand = new RelayCommand(_ =>
        {
            _commandManager.Redo();
            _logObserver.Update("Redo last action");
        }, _ => _commandManager.CanRedo);

        AddActivityCommand    = new RelayCommand(_ => ExecuteAddActivity(),    _ => _selectedCourse != null);
        EditActivityCommand   = new RelayCommand(_ => ExecuteEditActivity(),   _ => _selectedActivity != null);
        DeleteActivityCommand = new RelayCommand(_ => ExecuteDeleteActivity(), _ => _selectedActivity != null);
        SimulateStatesCommand = new RelayCommand(_ => SimulateStatesAsync(),
            _ => _selectedActivity != null && !_isSimulating);

        _commandManager.HistoryChanged += () =>
        {
            SyncCoursesFromRepository();
            System.Windows.Input.CommandManager.InvalidateRequerySuggested();
        };

        SyncCoursesFromRepository();
    }

    private void SyncCoursesFromRepository()
    {
        var selectedId = _selectedCourse?.Id;
        _courses.Clear();
        foreach (var course in _courseRepository.GetAll())
            _courses.Add(new CourseViewModel(course));
        SelectedCourse = _courses.FirstOrDefault(c => c.Id == selectedId);
    }

    private void LoadActivities(CourseViewModel? course)
    {
        var selectedId = _selectedActivity?.Id;
        _activities.Clear();
        if (course == null)
        {
            _chartObserver.SetActivities([]);
            return;
        }
        var domainActivities = new List<ParticipantActivity>();
        foreach (var a in _activityRepository.GetAll().Where(a => a.CourseId == course.Id))
        {
            a.Subscribe(_logObserver);
            a.Subscribe(_chartObserver);
            domainActivities.Add(a);
            _activities.Add(new ActivityViewModel(a, course.Name));
        }
        _chartObserver.SetActivities(domainActivities);
        SelectedActivity = _activities.FirstOrDefault(a => a.Id == selectedId);
    }

    private bool FilterCourse(object obj) =>
        obj is CourseViewModel c &&
        (string.IsNullOrEmpty(_searchText) ||
         c.Name.Contains(_searchText,     StringComparison.OrdinalIgnoreCase) ||
         c.Field.Contains(_searchText,    StringComparison.OrdinalIgnoreCase) ||
         c.Lecturer.Contains(_searchText, StringComparison.OrdinalIgnoreCase));

    private void ExecuteAddCourse()
    {
        CourseDialog dialog = null!;
        var vm = new CourseDialogViewModel("Add Course", () => dialog.Close());
        dialog = new CourseDialog(vm) { Owner = Application.Current.MainWindow };
        dialog.ShowDialog();

        if (!vm.Confirmed) return;

        var course = vm.ToCourseViewModel().ToModel();
        _commandManager.ExecuteCommand(new AddCourseCommand(course, _courseRepository));
    }

    private void ExecuteEditCourse()
    {
        if (_selectedCourse == null) return;

        CourseDialog dialog = null!;
        var vm = new CourseDialogViewModel("Edit Course", () => dialog.Close(), _selectedCourse);
        dialog = new CourseDialog(vm) { Owner = Application.Current.MainWindow };
        dialog.ShowDialog();

        if (!vm.Confirmed) return;

        var oldCourse = _selectedCourse.ToModel();
        var newCourse = vm.ToCourseViewModel(existingId: _selectedCourse.Id).ToModel();
        _commandManager.ExecuteCommand(new EditCourseCommand(oldCourse, newCourse, _courseRepository));
    }

    private void ExecuteDeleteCourse()
    {
        if (_selectedCourse == null) return;

        var result = MessageBox.Show(
            $"Delete '{_selectedCourse.Name}'?",
            "Confirm Delete",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes) return;

        var course = _selectedCourse.ToModel();
        _commandManager.ExecuteCommand(new DeleteCourseCommand(course, _courseRepository, _activityRepository));
    }

    private void ExecuteAddActivity()
    {
        if (_selectedCourse == null) return;

        ActivityDialog dialog = null!;
        var vm = new ActivityDialogViewModel("Add Activity", () => dialog.Close(),
            _courses, _selectedCourse);
        dialog = new ActivityDialog(vm) { Owner = Application.Current.MainWindow };
        dialog.ShowDialog();

        if (!vm.Confirmed) return;

        var activity = vm.ToActivityModel();
        _commandManager.ExecuteCommand(new AddActivityCommand(activity, _activityRepository));
        _logObserver.Update(
            $"Add activity {activity.Id}: course={activity.CourseId}, " +
            $"date={activity.CaptureTime:d}, enrolled={activity.EnrollmentCount}, " +
            $"topics={activity.ProcessedTopicsCount}, grade={activity.AverageGrade:F2}");
    }

    private void ExecuteEditActivity()
    {
        if (_selectedActivity == null) return;

        ActivityDialog dialog = null!;
        var vm = new ActivityDialogViewModel("Edit Activity", () => dialog.Close(),
            _courses, _selectedCourse, _selectedActivity);
        dialog = new ActivityDialog(vm) { Owner = Application.Current.MainWindow };
        dialog.ShowDialog();

        if (!vm.Confirmed) return;

        var oldActivity = _selectedActivity.Activity;
        var newActivity = vm.ToActivityModel();
        _commandManager.ExecuteCommand(new EditActivityCommand(oldActivity, newActivity, _activityRepository));
        _logObserver.Update(
            $"Edit activity {newActivity.Id}: course={newActivity.CourseId}, " +
            $"date={newActivity.CaptureTime:d}, enrolled={newActivity.EnrollmentCount}, " +
            $"topics={newActivity.ProcessedTopicsCount}, grade={newActivity.AverageGrade:F2}");
    }

    private void ExecuteDeleteActivity()
    {
        if (_selectedActivity == null) return;

        var result = MessageBox.Show(
            $"Delete activity from {_selectedActivity.CaptureTime:d}?",
            "Confirm Delete",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes) return;

        var activity = _selectedActivity.Activity;
        _commandManager.ExecuteCommand(new DeleteActivityCommand(activity, _activityRepository));
        _logObserver.Update($"Delete activity {activity.Id} (date={activity.CaptureTime:d})");
    }

    private async void SimulateStatesAsync()
    {
        if (_selectedActivity == null || _isSimulating) return;

        _isSimulating = true;
        System.Windows.Input.CommandManager.InvalidateRequerySuggested();

        var vm             = _selectedActivity;
        var activity       = vm.Activity;
        var preSimActivity = ParticipantActivity.FromDto(activity.ToDto());

        _logObserver.Update($"Simulate start for activity {activity.Id}");

        try
        {
            activity.ChangeState(new PopularState());
            vm.RefreshStatus();
            await Task.Delay(600);

            while (!(activity.CurrentState is ArchivedState))
            {
                activity.CurrentState.HandleState(activity);
                vm.RefreshStatus();
                await Task.Delay(600);
            }

            _commandManager.ExecuteCommand(new EditActivityCommand(preSimActivity, activity, _activityRepository));
            _logObserver.Update($"Simulate complete for activity {activity.Id}, final: {activity.Status}");
        }
        finally
        {
            _isSimulating = false;
            System.Windows.Input.CommandManager.InvalidateRequerySuggested();
        }
    }
}
