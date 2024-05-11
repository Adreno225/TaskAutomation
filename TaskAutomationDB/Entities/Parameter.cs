namespace TaskAutomationDB.Entities;
/// <summary>
/// Сущность параметра
/// </summary>
public class Parameter : NamedEntity
{
    /// <summary>
    /// Объект автоматизации
    /// </summary>
    public ObjectAutomation ObjectAutomation { get; set; } = null!;
}