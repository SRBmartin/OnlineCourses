using System.Windows.Input;

namespace OnlineCourses.InformationSystem.ViewModels;

public class CourseDialogViewModel : ViewModelBase
{
    private string _name = string.Empty;
    private string _field = string.Empty;
    private string _lecturer = string.Empty;
    private string _validationError = string.Empty;

    private readonly Action _close;

    public string Title { get; }
    public bool Confirmed { get; private set; }

    public string Name     { get => _name;     set => SetField(ref _name, value); }
    public string Field    { get => _field;    set => SetField(ref _field, value); }
    public string Lecturer { get => _lecturer; set => SetField(ref _lecturer, value); }

    public string ValidationError
    {
        get => _validationError;
        private set => SetField(ref _validationError, value);
    }

    public ICommand SaveCommand   { get; }
    public ICommand CancelCommand { get; }

    public CourseDialogViewModel(string title, Action close, CourseViewModel? existing = null)
    {
        Title  = title;
        _close = close;

        if (existing != null)
        {
            _name     = existing.Name;
            _field    = existing.Field;
            _lecturer = existing.Lecturer;
        }

        SaveCommand   = new RelayCommand(_ => Save(),    _ => CanSave());
        CancelCommand = new RelayCommand(_ => _close());
    }

    private bool CanSave() =>
        !string.IsNullOrWhiteSpace(_name) &&
        !string.IsNullOrWhiteSpace(_field) &&
        !string.IsNullOrWhiteSpace(_lecturer);

    private void Save()
    {
        if (!CanSave())
        {
            ValidationError = "All fields are required.";
            return;
        }
        Confirmed = true;
        _close();
    }

    public CourseViewModel ToCourseViewModel(Guid? existingId = null) => new()
    {
        Id       = existingId ?? Guid.NewGuid(),
        Name     = _name,
        Field    = _field,
        Lecturer = _lecturer
    };
}
