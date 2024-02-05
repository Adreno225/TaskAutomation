using TaskAutomationInterfaces;

namespace TaskAutomationDB.Entities;

public abstract class Entity:IEntity
{
    public int Id { get; set; }
}