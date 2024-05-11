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
    /// <param name="nameSubItem">Подпись наименования элемента</param>
    public SubTreeItem(IListGroup<Children> listGroup, string nameSubItem) : base(nameSubItem, listGroup) { }
}
/// <summary>
/// Элемент дерева, представляющий параметры КО
/// </summary>
public class SubTreeItemCOParameters : SubTreeItem<IParameterTreeItem, IComplexObjectTreeItem>
{
    private const string NameSubItems = "Параметры КО";
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="listGroup">Список параметров</param>
    public SubTreeItemCOParameters(IListGroup<IParameterTreeItem> listGroup) : base(listGroup, NameSubItems) { }

    public override ITreeItem Copy() => 
        new SubTreeItemCOParameters((IListGroup<IParameterTreeItem>)ListGroup?.Copy()) 
        { 
            Name=Name
        };
}
/// <summary>
/// Элемент дерева, представляющий параметры площадки
/// </summary>
public class SubTreeItemAreaParameters : SubTreeItem<IParameterTreeItem, IAreaTreeItem>
{
    private const string NameSubItems = "Параметры площадки";
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="listGroup">Список параметров</param>
    public SubTreeItemAreaParameters(IListGroup<IParameterTreeItem> listGroup) : base(listGroup, NameSubItems) { }

    public override ITreeItem Copy() =>
        new SubTreeItemAreaParameters((IListGroup<IParameterTreeItem>)ListGroup?.Copy())
        {
            Name = Name
        };
}