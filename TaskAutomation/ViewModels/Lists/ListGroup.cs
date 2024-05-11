using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using TaskAutomation.ViewModels.TreeItems;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.ObjectModel;

namespace TaskAutomation.ViewModels.Lists;

/// <summary>
/// Универсальный интерфейс списков
/// </summary>
public interface IListGroup : IBaseGroup<ITreeItem,IListGroup> { }
/// <summary>
/// Типизированный интерфейс списков
/// </summary>
public interface IListGroup<T>: IListGroup where T : ITreeItem { }
/// <summary>
/// Универсальный интерфейс списков с 2 типами вложенных объектов
/// </summary>
public interface IListTwoGroup:IListGroup
{
    /// <summary>
    /// Команда добавления элемента второго типа в коллекцию
    /// </summary>
    IRelayCommand AddItem2Command { get; }
}

/// <summary>
/// Типизированный интерфейс списков с 2 типами вложенных объектов
/// </summary>
public interface IListGroup<T1,T2>: IListTwoGroup where T1 : ITreeItem where T2: ITreeItem { }

/// <summary>
/// Реализация типизированного интерфейса с 1 типом вложенных элементов
/// </summary>
/// <typeparam name="T">Тип элементов списка</typeparam>
public abstract partial class ListGroup<T>:BaseGroup<ITreeItem, IListGroup>,IListGroup<T>  where T : ITreeItem
{
    /// <summary>
    /// Метод добавления элента типа T в коллекцию
    /// </summary>
    protected override void AddItem() => AddItem<T>();
    protected override bool IsSelectedCanCommandExecute() => base.IsSelectedCanCommandExecute() && SelectedItem is not ISubTreeItem;

    /// <summary>
    /// Контруктор класса с инициализацией подписи над списком
    /// </summary>
    /// <param name="text">Подпись списка</param>
    protected ListGroup(string text):base(text) { }
    /// <summary>
    /// Дополнительный контруктор класса с инициализацией подписи над списком
    /// </summary>
    /// <param name="text">Подпись таблицы</param>
    /// <param name="selectedItem">Выбранный айтем таблицы</param>
    /// <param name="items">Элементы списка</param>
    protected ListGroup(string text, ITreeItem selectedItem, ObservableCollection<ITreeItem> items):base(text,selectedItem,items) { }

    /// <summary>
    /// Добавление 1 элемента по дефолту в коллекцию элементов (например, элемента "Параметры КО" в списко элементов "Комплексного объекта")
    /// </summary>
    /// <typeparam name="SubType">Тип вложенных элементов (например, параметры)</typeparam>
    /// <typeparam name="ParentType">Тип родительского элмента (например, комплексный объект)</typeparam>
    protected void AddSubTreeItem<SubType, ParentType>()
        where SubType : ITreeItem where ParentType : ITreeItem
    {
        if (Items.Count ==0 || Items.Any() && Items[0] is not ISubTreeItem)
            Items.Insert(0, App.Services.GetRequiredService<ISubTreeItem<SubType, ParentType>>());
    }
}

/// <summary>
/// Базовый класс универсальных списков с одним типов вложенных объектов и имеющих
/// подвложенные списки (пример -> Площадки имеют список объектов и вложенный список - перечень параметров)
/// </summary>
public abstract class SubListGroup<MainItemType, SubType, ParentType> : ListGroup<MainItemType>, IListGroup<MainItemType>
    where MainItemType : ITreeItem where SubType : ITreeItem where ParentType : ITreeItem
{
    protected SubListGroup(string text) : base(text)
    {
        AddSubTreeItem<SubType, ParentType>();
    }
    protected SubListGroup(string text, ITreeItem selectedItem, ObservableCollection<ITreeItem> items): base(text,selectedItem,items) { }
}

/// <summary>
/// Базовый класс универсальных списков с 2 типами вложенных объектов
/// </summary>
/// <typeparam name="Type1">Тип объекта 1</typeparam>
/// <typeparam name="Type2">Тип объекта 2</typeparam>
public abstract partial class ListGroup<Type1, Type2> : ListGroup<Type1>, IListGroup<Type1, Type2>
    where Type1 : ITreeItem where Type2 : ITreeItem
{
    #region Добавить айтем (тип 2)
    [RelayCommand]
    [property: JsonIgnore]
    private void AddItem2() => AddItem<Type2>();
    #endregion
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="text">Подпись списка</param>
    protected ListGroup(string text) : base(text) { }

    /// <summary>
    /// Дополнительный конструктор
    /// </summary>
    /// <param name="text">Подпись таблицы</param>
    /// <param name="selectedItem">Выбранный айтем таблицы</param>
    /// <param name="items">Элементы списка</param>
    protected ListGroup(string text, ITreeItem selectedItem, ObservableCollection<ITreeItem> items):base(text,selectedItem,items) { }
}

/// <summary>
/// Базовый класс универсальных списков с 2 типами вложенных объектов и имеющих подвложенные списки
/// (пример -> КО имеет список из объектов/площадок и подвложенный список - перечень параметров)
/// </summary>
public abstract class SubListGroup<MainItemType1, MainItemType2, SubType, ParentType> :
    ListGroup<MainItemType1,MainItemType2>, IListGroup<MainItemType1, MainItemType2>
    where MainItemType1 : ITreeItem where MainItemType2 : ITreeItem
    where SubType : ITreeItem where ParentType : ITreeItem
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="text">подпись списка</param>
    protected SubListGroup(string text) : base(text)
    {
        AddSubTreeItem<SubType, ParentType>();
    }

    /// <summary>
    /// Дополнительный конструктор
    /// </summary>
    /// <param name="text">Подпись таблицы</param>
    /// <param name="selectedItem">Выбранный айтем таблицы</param>
    /// <param name="items">Элементы списка</param>>
    protected SubListGroup(string text, ITreeItem selectedItem, ObservableCollection<ITreeItem> items) : base(text, selectedItem, items) { }
}
