using Newtonsoft.Json;
using System.Collections.Generic;
using TaskAutomation.ViewModels.Lists;

namespace TaskAutomation.ViewModels.TreeItems;
/// <summary>
/// Интерфейс комплексного объекта
/// </summary>
public interface IComplexObjectTreeItem : ITreeItem
{
    /// <summary>
    /// Перечень площадок и объектов
    /// </summary>
    List<ITreeItem> AreasObjects { get; }
    /// <summary>
    /// Перечень общих параметров КО
    /// </summary>
    List<IParameterTreeItem> Parameters { get; }
}
/// <summary>
/// Реализация комлексного объекта
/// </summary>
public class ComplexObjectTreeItem : TreeItem, IComplexObjectTreeItem
{
    private const string DefaultName = "Комплексный объект";

    [JsonIgnore]
    public List<ITreeItem> AreasObjects => DefineTypeObjects<ITreeItem, ITreeItem>(ListGroup.Items);
    [JsonIgnore]
    public List<IParameterTreeItem> Parameters => DefineTypeObjects<IParameterTreeItem, ITreeItem>(ListGroup.Items[0].ListGroup.Items);
    /// <summary>
    /// Основной конструктор
    /// </summary>
    /// <param name="listGroup">Список площадок/объектов</param>
    public ComplexObjectTreeItem(IListGroup<IAreaTreeItem, IObjectTreeItem> listGroup) : this(DefaultName, listGroup) { }
    
    /// <summary>
    /// Дополнительный конструктор
    /// </summary>
    /// <param name="name">Наименование элемента дерева</param>
    /// <param name="listGroup">Список площадок/объектов</param>
    [JsonConstructor]
    public ComplexObjectTreeItem(string name, IListGroup listGroup) : base(name, listGroup) { }

    public override ITreeItem Copy() =>
        new ComplexObjectTreeItem(Name,ListGroup.Copy());
}