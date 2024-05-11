using System.Collections.ObjectModel;
using TaskAutomation.Models;

namespace TaskAutomation.ViewModels.Lists;

/// <summary>
/// Интерфейс таблиц
/// </summary>
/// <typeparam name="TypeItems">Тип элементов таблицы</typeparam>
public interface ITableGroup<TypeItems>: IBaseGroup<TypeItems, ITableGroup<TypeItems>>
 where TypeItems : ICopy<TypeItems>
{

}
/// <summary>
/// Реализация интерфейса таблиц
/// </summary>
/// <typeparam name="TypeItems">Тип элементов таблицы</typeparam>
public abstract partial class TableGroup<TypeItems> :BaseGroup<TypeItems,ITableGroup<TypeItems>>, ITableGroup<TypeItems>
    where TypeItems : ICopy<TypeItems>
{
    /// <summary>
    /// Основной конструктор таблицы
    /// </summary>
    /// <param name="text">Подпись таблицы</param>
    protected TableGroup(string text):base(text) { }
    /// <summary>
    /// Дополнительный конструктор таблицы
    /// </summary>
    /// <param name="text">Подпись таблицы</param>
    /// <param name="selectedItem">Выбранный айтем таблицы</param>
    /// <param name="items">Элементы списка</param>
    protected TableGroup(string text, TypeItems selectedItem, ObservableCollection<TypeItems> items):base(text,selectedItem,items) { }
}
