using System.IO;
using System.Xml.Serialization;

namespace OnlineCourses.InformationSystem.Persistence;

public class XmlEntityStore<T> : IEntityStore<T>
{
    private readonly string _filePath;
    private readonly List<T> _items;
    private readonly XmlSerializer _serializer = new(typeof(List<T>));

    public XmlEntityStore(string filePath)
    {
        _filePath = filePath;
        _items = File.Exists(filePath) ? Load() : new List<T>();
    }

    public IReadOnlyList<T> Items => _items.AsReadOnly();

    public void Add(T item) => _items.Add(item);

    public void Remove(T item) => _items.Remove(item);

    public void Save()
    {
        var dir = Path.GetDirectoryName(_filePath)!;
        Directory.CreateDirectory(dir);
        var tmp = Path.Combine(dir, Path.GetRandomFileName());
        try
        {
            using var writer = new StreamWriter(tmp);
            _serializer.Serialize(writer, _items);
        }
        catch
        {
            File.Delete(tmp);
            throw;
        }
        File.Move(tmp, _filePath, overwrite: true);
    }

    private List<T> Load()
    {
        using var reader = new StreamReader(_filePath);
        return _serializer.Deserialize(reader) as List<T> ?? new List<T>();
    }
}
