namespace OnlineCourses.InformationSystem.Persistence;

public interface IEntityStore<T>
{
    IReadOnlyList<T> Items { get; }
    void Add(T item);
    void Remove(T item);
    void Save();
}
