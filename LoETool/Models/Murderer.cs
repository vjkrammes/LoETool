using LoETool.Infrastructure;

namespace LoETool.Models;

public class Murderer : NotifyBase
{
    private string? _name;
    public string Name
    {
        get => _name ?? string.Empty;
        set => SetProperty(ref _name, value);
    }

    private int _count;
    public int Count
    {
        get => _count;
        set => SetProperty(ref _count, value);
    }

    public Murderer()
    {
        Name = string.Empty;
        Count = 0;
    }

    public Murderer(string name, int count)
    {
        Name = name;
        Count = count;
    }
}
