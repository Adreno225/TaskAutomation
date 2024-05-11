using Microsoft.EntityFrameworkCore;
using TaskAutomationDB.Entities;

namespace TaskAutomationDB.Context;
/// <summary>
/// Контекст БД
/// </summary>
public class TaskAutomationContext:DbContext
{
    /// <summary>
    /// Таблица функций параметров
    /// </summary>
    public DbSet<FunctionParameter> FunctionsParameters { get; set; } = null!;
    /// <summary>
    /// Таблица объектов автоматизации 
    /// </summary>
    public DbSet<ObjectAutomation> ObjectsAutomation { get; set; } = null!;
    /// <summary>
    /// Таблица классов автоматизации
    /// </summary>
    public DbSet<Class> Classes { get; set; } = null!;
    /// <summary>
    /// Таблица параметров автоматизации
    /// </summary>
    public DbSet<Parameter> Parameters { get; set; } = null!;
    /// <summary>
    /// Cвязывающая таблца параметры, классы и функции
    /// </summary>
    public DbSet<ParameterClassFunction> ParameterClassFunction { get; set; } = null!;
    /// <summary>
    /// Таблица режимов работы
    /// </summary>
    public DbSet<Mode> Modes { get; set; } = null!;
    /// <summary>
    /// Таблица стадий
    /// </summary>
    public DbSet<Stage> Stages { get; set; } = null!;
    /// <summary>
    /// Таблица заказчиков
    /// </summary>
    public DbSet<Customer> Customers { get; set; } = null!;
    /// <summary>
    /// Таблица типов КО
    /// </summary>
    public DbSet<TypeCO> TypesCO { get; set; } = null!;
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="options">настройки контекста</param>
    public TaskAutomationContext(DbContextOptions<TaskAutomationContext> options):base(options) { }
}