using Newtonsoft.Json;
using TaskAutomation.ViewModels.Lists;

namespace TaskAutomation.ViewModels.TreeItems;
/// <summary>
/// Интерфейс элемента дерева представляющая только перечни чего-либо (например, элмент дерева -> "Параметры КО")
/// </summary>
public interface ISubTreeItem:ITreeItem { }
/// <summary>
/// Типизированный интерфейс элемента дерева представляющая только перечни чего-либо (например, элмент дерева -> "Параметры КО")
/// </summary>
/// <typeparam name="Children">Тип элементов детей</typeparam>
/// <typeparam name="Parent">Тип родителя</typeparam>
public interface ISubTreeItem<Children,Parent>: ISubTreeItem where Children : ITreeItem where Parent : ITreeItem { }
/// <summary>
/// Реализация базового типизированного элемента дерева представляющая только перечни чего-либо (например, элмент дерева -> "Параметры КО")
/// </summary>
/// <typeparam name="Children"></typeparam>
/// <typeparam name="Parent"></typeparam>
public abstract class SubTreeItem<Children, Parent> : TreeItem, ISubTreeItem<Children, Parent> where Children : ITreeItem where Parent : ITreeItem
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="listGroup">Список элементов детей</param>
    /// <param name="name">Подпись наименования элемента</param>
    public SubTreeItem(string name, IListGroup listGroup) : base(name, listGroup) { }
}
/// <summary>
/// Элемент дерева, представляющий параметры КО
/// </summary>
public class SubTreeItemCOParameters : SubTreeItem<IParameterTreeItem, IComplexObjectTreeItem>
{
    private const string DefaultName = "Параметры КО";
    /// <summary>
    /// Основной конструктор
    /// </summary>
    /// <param name="listGroup">Список параметров</param>
    public SubTreeItemCOParameters(IListGroup<IParameterTreeItem> listGroup) : this(DefaultName, listGroup) { }

    /// <summary>
    /// Дополнительный конструктор
    /// </summary>
    /// <param name="name">Наименование подписи элемента дерева</param>
    /// <param name="listGroup">Список элементов детей</param>
    [JsonConstructor]
    public SubTreeItemCOParameters(string name, IListGroup listGroup) : base(name, listGroup) { }

    public override ITreeItem Copy() =>
        new SubTreeItemCOParameters(Name,ListGroup?.Copy());
}
/// <summary>
/// Элемент дерева, представляющий параметры площадки
/// </summary>
public class SubTreeItemAreaParameters : SubTreeItem<IParameterTreeItem, IAreaTreeItem>
{
    private const string DefaultName = "Параметры площадки";
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="listGroup">Список параметров</param>
    public SubTreeItemAreaParameters(IListGroup<IParameterTreeItem> listGroup) : base(DefaultName, listGroup) { }

    /// <summary>
    /// Дополнительный конструктор
    /// </summary>
    /// <param name="name">Наименование подписи элемента дерева</param>
    /// <param name="listGroup">Список элементов детей</param>
    [JsonConstructor]
    public SubTreeItemAreaParameters(string name, IListGroup listGroup) : base(name, listGroup) { }

    public override ITreeItem Copy() =>
        new SubTreeItemAreaParameters(Name, ListGroup?.Copy());
}