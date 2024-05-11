using TaskAutomationInterfaces;

namespace TaskAutomationDB.Entities;
/// <summary>
/// <Базовая сущность
/// </summary>
public abstract class Entity:IEntity
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public int Id { get; set; }
}