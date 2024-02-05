namespace TaskAutomationDB.Entities;

public abstract class NamedEntity:Entity
{
    public string Name { get; set; } = null!;
    public override string ToString() => Name;

}