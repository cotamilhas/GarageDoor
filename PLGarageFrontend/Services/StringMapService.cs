namespace PLGarageFrontend.Services;

public class StringMapEntry
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Curator { get; set; } = "";
}

public class StringMapService
{
    private readonly Dictionary<int, StringMapEntry> _entries = new();

    public StringMapService()
    {
        Load("stringmap.txt");
    }

    private void Load(string path)
    {
        if (!File.Exists(path)) return;

        foreach (var line in File.ReadAllLines(path))
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#')) continue;

            var parts = line.Split('|');
            if (parts.Length < 2) continue;
            if (!int.TryParse(parts[0].Trim(), out int id)) continue;

            _entries[id] = new StringMapEntry
            {
                Id = id,
                Name = parts.Length > 1 ? parts[1].Trim() : "",
                Description = parts.Length > 2 ? parts[2].Trim() : "",
                Curator = parts.Length > 3 ? parts[3].Trim() : "",
            };
        }
    }

    public StringMapEntry? Get(int id) => _entries.TryGetValue(id, out var entry) ? entry : null;
    public string GetName(int id, string fallback = "") =>
        _entries.TryGetValue(id, out var entry) ? entry.Name : fallback;
    public bool Has(int id) => _entries.ContainsKey(id);
}