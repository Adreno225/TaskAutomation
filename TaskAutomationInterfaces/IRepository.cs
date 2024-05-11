namespace TaskAutomationInterfaces;
/// <summary>
/// Интерфейс репозитория
/// </summary>
/// <typeparam name="T">Тип элементов репозитория</typeparam>
public interface IRepository<T> where T : IEntity, new()
{
    /// <summary>
    /// Элементы репозитория (IQueryble)
    /// </summary>
    IQueryable<T> Items { get; }
    /// <summary>
    /// Элементы репозитория (массив)
    /// </summary>
    T[] ItemsArray { get; }
    /// <summary>
    /// Получить элемент по его id
    /// </summary>
    /// <param name="id">Идентификатор элемента</param>
    /// <returns>Элемент</returns>
    T Get(int id);
    /// <summary>
    /// Получить элемент по его id (асинхронная версия)
    /// </summary>
    /// <param name="id">Идентификатор элемента</param>
    /// <param name="cancel">Токен отмены</param>
    /// <returns></returns>
    Task<T>  GetAsync(int id, CancellationToken cancel = default);
    /// <summary>
    /// Добавить элемент
    /// </summary>
    /// <param name="item">Элемент</param>
    /// <returns>Элемент</returns>
    T Add(T item);
    /// <summary>
    /// Добавить элемент (асинхронная версия)
    /// </summary>
    /// <param name="item">Элемент</param>
    /// <param name="cancel">Токен отмены</param>
    /// <returns></returns>
    Task<T> AddAsync(T item, CancellationToken cancel = default);
    /// <summary>
    /// Обновить элемент
    /// </summary>
    /// <param name="item">Элемент</param>
    void Update(T item);
    /// <summary>
    /// Обновить элемент (асинхронная версия)
    /// </summary>
    /// <param name="item">Элемент</param>
    /// <param name="cancel">Токен отмены</param>
    /// <returns></returns>
    Task UpdateAsync(T item, CancellationToken cancel = default);
    /// <summary>
    /// Удалить элемент по его id
    /// </summary>
    /// <param name="id">Идентификатор элемента</param>
    void Remove(int id);
    /// <summary>
    /// Удалить элемент по его id (асинхронная версия)
    /// </summary>
    /// <param name="id">Идентификатор элемента</param>
    /// <param name="cancel">Токен отмены</param>
    /// <returns></returns>
    Task RemoveAsync(int id, CancellationToken cancel = default);
}