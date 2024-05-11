namespace TaskAutomationDB.Entities
{
    /// <summary>
    /// Сущность связывающая параметр, класс и функцию
    /// </summary>
    public class ParameterClassFunction:Entity
    {
        /// <summary>
        /// Параметр
        /// </summary>
        public Parameter Parameter { get; set; } = null!;
        /// <summary>
        /// Класс автоматизации
        /// </summary>
        public Class Class { get; set; } = null!;
        /// <summary>
        /// Функция параметра
        /// </summary>
        public FunctionParameter FunctionParameter { get; set; } = null!;
        public override string ToString()
        {
            return $"{Parameter?.Name}; {Class?.Name}; {FunctionParameter?.Name}";
        }
    }
}
