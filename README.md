# OnlineCourses
Project for University - Development of layered applications. Utilizing .NET Core 10 (WPF and WCF) utilizing multiple patterns including MVVM (for WPF application).

## Design Patterns

The two components communicate over WCF.

---

### Observer - Information System

**Classes:** `IObserver`, `ChartObserver`, `LogObserver`, `ParticipantActivity`.

`ParticipantActivity` is the subject and holds a list of `IObserver` instances. When an activity changes state (via `ChangeState()`), it calls `NotifyObservers()` and all subscribed observers react. In `MainViewModel`, both `LogObserver` and `ChartObserver` are subscribed to each loaded activity.

The reason for this pattern is that the domain model needed to trigger two completely different reactions on state change: writing to a log file and updating a live chart without knowing anything about either. Adding a new reaction in the future would only require writing a new observer class, with zero changes to `ParticipantActivity`.

---

### Command - Information System

**Classes:** `IUndoableCommand`, `CommandManager`, `AddCourseCommand`, `EditCourseCommand`, `DeleteCourseCommand`, `AddActivityCommand`, `EditActivityCommand`, `DeleteActivityCommand`.

Every operation that modifies data (add, edit, delete for both courses and activities) is wrapped in a command object that knows both `Execute()` and `Undo()`. `CommandManager` maintains undo and redo stacks and fires a `HistoryChanged` event so the UI can update it's button states.

The undo/redo requirement made this the natural choice. Wrapping mutations as objects also keeps the UI completely decoupled from the repository layer. The state simulator reuses this too as it wraps the simulated state change as an `EditActivityCommand` so the whole simulation can be undone with a single step.

---

### State - Information System

**Classes:** `ICourseStatusState`, `PopularState`, `DecliningInterestState`, `RecommendedState`, `ArchivedState`, `ParticipantActivity`.

`ParticipantActivity` holds a reference to the current `ICourseStatusState`. Calling `HandleState()` on the current state object determines the next state and performs the transition. The simulation loop in `MainViewModel` keeps calling `HandleState()` until `ArchivedState` is reached.

Without this pattern the activity class would need a growing chain of if/else blocks to handle lifecycle logic. Each state class owns its own transition logic, so adding a new phase only requires writing a new class so none of the existing states need to change.

---

### Strategy - Data Processing

**Classes:** `IStatisticalStrategy`, `MinTopicsAndAvgGradeStrategy`, `AverageEnrollmentsStrategy`, `RecommendedCountStrategy`, `StatisticsProcessor`.

`StatisticsProcessor` holds a reference to the currently selected `IStatisticalStrategy`. Before running, `MainViewModel` calls `SetStrategy()` with whichever strategy matches the user's dropdown selection. The processor then delegates both `RunStatistics()` and `ExportToCsv()` to that strategy so that each strategy provides both a formatted text result (`Calculate()`) and CSV content (`CalculateCsv()`).

The application needs to support multiple interchangeable analysis algorithms on the same data. Strategy makes it possible to add a new analysis type by writing one new class, with no changes needed in `StatisticsProcessor` or the view model. It also keeps the display and CSV formatting logic together per analysis type instead of scattered across the codebase.

---

### Adapter - Data Processing

**Classes:** `IActivityAdapter`, `ActivityAdapter`

After the WCF call returns a `List<ParticipantActivity>` DTO, `ActivityAdapter.Adapt()` transforms it into `Dictionary<string, List<ReducedActivity>>` where the key is `"{courseId}-{from}-{to}"`. That dictionary is merged into the local data store and passed directly to all strategies.

The WCF service contract returns a flat list because that is what WCF serialization requires. The processing layer needs data grouped by course and date range, with only a subset of fields (`ReducedActivity` instead of the full DTO). The adapter sits between these two incompatible interfaces without modifying either side. If the service contract changes in the future, only the adapter needs to be updated.

