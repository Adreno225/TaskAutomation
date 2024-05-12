using Newtonsoft.Json;
using System.Collections.Generic;
using TaskAutomation.ViewModels.Lists;

namespace TaskAutomation.ViewModels.TreeItems;
/// <summary>
/// Интерфейс элемента площадки
/// </summary>
public interface IAreaTreeItem : ITreeItem
{
    /// <summary>
    /// Перечень объектов
    /// </summary>
    List<IObjectTreeItem> Objects { get; }
    /// <summary>
    /// Перечень общих параметров площадки
    /// </summary>
    List<IParameterTreeItem> Parameters { get; }
}
/// <summary>
/// Реализация площадки
/// </summary>
public class AreaTreeItem : TreeItem, IAreaTreeItem
{
    private const string DefaultName = "Площадка";
    [JsonIgnore]
    public List<IObjectTreeItem> Objects => DefineTypeObjects<IObjectTreeItem,ITreeItem>(ListGroup.Items);
    [JsonIgnore]
    public List<IParameterTreeItem> Parameters => DefineTypeObjects<IParameterTreeItem, ITreeItem>(ListGroup.Items[0].ListGroup.Items);
    /// <summary>
    /// Основной конструктор
    /// </summary>
    /// <param name="listGroup">Список вложенных объектов</param>
    public AreaTreeItem(IListGroup<IObjectTreeItem> listGroup) : this(DefaultName, listGroup) { }

    /// <summary>
    /// Дополнительный конструктор
    /// </summary>
    /// <param name="name">Наименование элемента дерева</param>
    /// <param name="listGroup">Список площадок/объектов</param>
    [JsonConstructor]
    public AreaTreeItem(string name, IListGroup listGroup): base(name, listGroup) { }

    public override ITreeItem Copy() =>
        new AreaTreeItem(Name,ListGroup?.Copy());
}