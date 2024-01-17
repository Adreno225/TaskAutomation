namespace TaskAutomationDB.Entities
{
    public abstract class NamedEntity:Entity
    {
        public string? Name { get; set; }
        public override string ToString() => Name;

    }
}
