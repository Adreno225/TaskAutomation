namespace TaskAutomation.Models
{
    /// <summary>
    /// Типизированный интерфейс копирования объекта
    /// </summary>
    /// <typeparam name="T">Тип копируемого объекта</typeparam>
    public interface ICopy<T>
    {
        /// <summary>
        /// Метод копирования объекта
        /// </summary>
        /// <returns></returns>
        T Copy();
    }
}
