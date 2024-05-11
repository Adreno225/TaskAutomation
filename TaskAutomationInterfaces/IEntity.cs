namespace TaskAutomationInterfaces;
/// <summary>
/// Интерфейс сущности БД
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    int Id { get; set; }
}