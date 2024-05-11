namespace TaskAutomationDB.Entities;
/// <summary>
/// Базовая именованная сущность
/// </summary>
public abstract class NamedEntity:Entity
{
    /// <summary>
    /// Наименование сущности
    /// </summary>
    public string Name { get; set; } = null!;
    public override string ToString() => Name;

}