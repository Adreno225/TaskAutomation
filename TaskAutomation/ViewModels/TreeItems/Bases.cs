using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaskAutomation.Models;
using TaskAutomation.ViewModels.Lists;


namespace TaskAutomation.ViewModels.TreeItems;

/// <summary>
/// Интерфейс элементов дерева
/// </summary>
public interface ITreeItem: ICopy<ITreeItem>
{
    /// <summary>
    /// Наименование элемента дерева
    /// </summary>
    string Name { get; set; }
    /// <summary>
    /// Список вложенных элементов
    /// </summary>
    IListGroup ListGroup { get; }
}
/// <summary>
/// Реализация базового интерфейса элемента дерева
/// </summary>
public abstract partial class TreeItem : ObservableObject, ITreeItem
{
    [ObservableProperty]
    protected string name;

    #region Вложенные объекты 
    public IListGroup ListGroup { get; }
    #endregion
    /// <summary>
    /// Базовый конструктор
    /// </summary>
    /// <param name="defaultName">Наименование элемента по умолчанию</param>
    /// <param name="listGroup">Cписок вложенных элементов</param>
    protected TreeItem(string defaultName, IListGroup listGroup)
    {
        Name = defaultName;
        ListGroup = listGroup;
    }
    /// <summary>
    /// Метод выделения из входной коллекции с элементами IN новой коллекции с элементами типа OUT
    /// </summary>
    /// <typeparam name="Out">Выходной тип элементов коллекции</typeparam>
    /// <typeparam name="In">Входной тип элементов коллекции</typeparam>
    /// <param name="collection">Входная коллекция</param>
    /// <returns>Выходная коллекция</returns>
    protected static List<Out> DefineTypeObjects<Out,In>(ObservableCollection<In> collection)
    {
        var result = new List<Out>();
        foreach (var item in collection)
            if (item is Out t)
                result.Add(t);
        return result;
    }

    /// <summary>
    /// Копирование элемента дерева
    /// </summary>
    /// <returns>Новый скопированный элемент</returns>
    public abstract ITreeItem Copy();
}