using Microsoft.EntityFrameworkCore;
using TaskAutomationDB.Context;
using TaskAutomationDB.Entities;
using TaskAutomationInterfaces;

namespace TaskAutomationDB;
/// <summary>
/// Реализация репозиотрия БД
/// </summary>
/// <typeparam name="T">Тип элементов репозитория</typeparam>
internal class DbRepository<T> : IRepository<T> where T : Entity, new()
{
    private readonly TaskAutomationContext _db;
    private readonly DbSet<T> _set;
    /// <summary>
    /// Автосохранение изменений
    /// </summary>
    public bool AutoSaveChanges { get; set; } = true;
    /// <summary>
    /// Конструктор репозитория
    /// </summary>
    /// <param name="db">Контекст БД</param>
    public DbRepository(TaskAutomationContext db)
    {
        _db = db;
        _set = db.Set<T>();
    }

    public virtual IQueryable<T> Items => _set;

    public T[] ItemsArray => _set.ToArray();

    public T Add(T item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        _db.Entry(item).State = EntityState.Added;
        if (AutoSaveChanges)
            _db.SaveChanges();
        return item;
    }

    public async Task<T> AddAsync(T item, CancellationToken cancel = default)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        _db.Entry(item).State = EntityState.Added;
        if (AutoSaveChanges)
            await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
        return item;
    }

    public T Get(int id) => Items.SingleOrDefault(x => x.Id == id);

    public async Task<T> GetAsync(int id, CancellationToken cancel = default) => await Items
        .SingleOrDefaultAsync(x => x.Id == id,cancel)
        .ConfigureAwait(false);

    public void Remove(int id)
    {
        _db.Remove(new T { Id = id });
        if (AutoSaveChanges)
            _db.SaveChanges();
    }

    public async Task RemoveAsync(int id, CancellationToken cancel = default)
    {
        _db.Remove(new T { Id = id });
        if (AutoSaveChanges)
            await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
    }

    public void Update(T item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        _db.Entry(item).State = EntityState.Modified;
        if (AutoSaveChanges)
            _db.SaveChanges();
    }

    public async Task UpdateAsync(T item, CancellationToken cancel = default)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        _db.Entry(item).State = EntityState.Modified;
        if (AutoSaveChanges)
            await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
    }
}