namespace TaskAutomationDB.Entities;

public abstract class DependentEntityFromParameter : NamedEntity
{
    public virtual ICollection<Parameter> Parameters { get; set; } = new List<Parameter>();
}