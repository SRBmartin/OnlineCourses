using ICommand = System.Windows.Input.ICommand;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using OnlineCourses.InformationSystem.Commands;
using OnlineCourses.InformationSystem.Repositories;

namespace OnlineCourses.InformationSystem.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly ICourseRepository _courseRepository;
    private readonly CommandManager    _commandManager;

    // IS-3: private readonly IActivityRepository _activityRepository;
    // IS-4: private LogObserver   _logObserver;
    // IS-5: private ChartObserver _chartObserver;

    private readonly ObservableCollection<CourseViewModel> _courses = new();
    private CourseViewModel? _selectedCourse;
    private string _searchText = string.Empty;

    // IS-3: private readonly ObservableCollection<ActivityViewModel> _activities = new();
    // IS-3: private ActivityViewModel? _selectedActivity;

    public ICollectionView CoursesView { get; }

    public CourseViewModel? SelectedCourse
    {
        get => _selectedCourse;
        set
        {
            if (!SetField(ref _selectedCourse, value)) return;
            OnPropertyChanged(nameof(ActivitiesHeader));
            // IS-3: LoadActivities(value);
        }
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

    public string ActivitiesHeader =>
        _selectedCourse != null ? $"Activities — {_selectedCourse.Name}" : "Activities";

    // IS-3: public ICollectionView ActivitiesView { get; }
    // IS-3: public ActivityViewModel? SelectedActivity { get => ...; set => ...; }

    public ICommand AddCourseCommand    { get; }
    public ICommand EditCourseCommand   { get; }
    public ICommand DeleteCourseCommand { get; }
    public ICommand UndoCommand         { get; }
    public ICommand RedoCommand         { get; }

    // IS-3: public ICommand AddActivityCommand    { get; }
    // IS-3: public ICommand EditActivityCommand   { get; }
    // IS-3: public ICommand DeleteActivityCommand { get; }
    // IS-4: public ICommand SimulateStatesCommand { get; }

    public MainViewModel(ICourseRepository courseRepository, CommandManager commandManager)
    {
        _courseRepository = courseRepository;
        _commandManager   = commandManager;

        CoursesView = CollectionViewSource.GetDefaultView(_courses);
        CoursesView.Filter = FilterCourse;

        // IS-3: ActivitiesView = CollectionViewSource.GetDefaultView(_activities);
        // IS-4: _logObserver   = new LogObserver();
        // IS-5: _chartObserver = new ChartObserver();

        AddCourseCommand    = new RelayCommand(_ => ExecuteAddCourse());
        EditCourseCommand   = new RelayCommand(_ => ExecuteEditCourse(),   _ => _selectedCourse != null);
        DeleteCourseCommand = new RelayCommand(_ => ExecuteDeleteCourse(), _ => _selectedCourse != null);
        UndoCommand         = new RelayCommand(_ => _commandManager.Undo(), _ => _commandManager.CanUndo);
        RedoCommand         = new RelayCommand(_ => _commandManager.Redo(), _ => _commandManager.CanRedo);

        _commandManager.HistoryChanged += () => System.Windows.Input.CommandManager.InvalidateRequerySuggested();
    }

    private bool FilterCourse(object obj) =>
        obj is CourseViewModel c &&
        (string.IsNullOrEmpty(_searchText) ||
         c.Name.Contains(_searchText,     StringComparison.OrdinalIgnoreCase) ||
         c.Field.Contains(_searchText,    StringComparison.OrdinalIgnoreCase) ||
         c.Lecturer.Contains(_searchText, StringComparison.OrdinalIgnoreCase));

    private void ExecuteAddCourse()
    {
        // IS-2: Open CourseDialog("Add Course"), wrap result in AddCourseCommand,
        //       execute via _commandManager, add returned CourseViewModel to _courses.
    }

    private void ExecuteEditCourse()
    {
        // IS-2: Open CourseDialog("Edit Course", _selectedCourse), wrap result in
        //       EditCourseCommand, execute via _commandManager, update item in _courses.
    }

    private void ExecuteDeleteCourse()
    {
        // IS-2: Confirm with MessageBox, wrap in DeleteCourseCommand,
        //       execute via _commandManager, remove from _courses.
    }

    // IS-3: private void LoadActivities(CourseViewModel? course)
    //       {
    //           _activities.Clear();
    //           if (course == null) return;
    //           foreach (var a in _activityRepository.GetByCourseAndPeriod(course.Id, ...))
    //               _activities.Add(ActivityViewModel.FromActivity(a));
    //       }
}
