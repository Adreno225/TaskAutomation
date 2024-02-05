namespace TaskAutomationDB.Entities
{
    public class ParameterClassFunction:Entity
    {
        public Parameter Parameter { get; set; } = null!;
        public Class Class { get; set; } = null!;
        public FunctionParameter FunctionParameter { get; set; } = null!;
    }
}
