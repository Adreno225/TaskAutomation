using Microsoft.EntityFrameworkCore;
using TaskAutomationDB.Entities;

namespace TaskAutomationDB.Context
{
    public class TaskAutomationContext:DbContext
    {
        public DbSet<Class> Classes { get; set; }
        public DbSet<Mode> Modes { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public TaskAutomationContext(DbContextOptions<TaskAutomationContext> options):base(options) { }
    }
}