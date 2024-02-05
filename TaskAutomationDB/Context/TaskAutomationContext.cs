using Microsoft.EntityFrameworkCore;
using TaskAutomationDB.Entities;

namespace TaskAutomationDB.Context;

public class TaskAutomationContext:DbContext
{
    public DbSet<FunctionParameter> FunctionsParameters { get; set; } = null!;
    public DbSet<ObjectAutomation> ObjectsAutomation { get; set; } = null!;
    public DbSet<Class> Classes { get; set; } = null!;
    public DbSet<Parameter> Parameters { get; set; } = null!;
    public DbSet<ParameterClassFunction> ParameterClassFunction { get; set; } = null!;
    public DbSet<Mode> Modes { get; set; } = null!;
    public DbSet<Stage> Stages { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<TypeCO> TypesCO { get; set; } = null!;
    public TaskAutomationContext(DbContextOptions<TaskAutomationContext> options):base(options) { }
}