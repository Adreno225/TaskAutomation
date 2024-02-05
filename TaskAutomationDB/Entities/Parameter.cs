namespace TaskAutomationDB.Entities;

public class Parameter : NamedEntity
{
    public ObjectAutomation ObjectAutomation { get; set; } = null!;
}