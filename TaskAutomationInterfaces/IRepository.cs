﻿namespace TaskAutomationInterfaces;

public interface IRepository<T> where T : IEntity, new()
{
    IQueryable<T> Items { get; }
    T Get(int id);
    Task<T>  GetAsync(int id, CancellationToken cancel = default);
    T Add(T item);
    Task<T> AddAsync(T item, CancellationToken cancel = default);
    void Update(T item);
    Task UpdateAsync(T item, CancellationToken cancel = default);
    void Remove(int id);
    Task RemoveAsync(int id, CancellationToken cancel = default);
}