using Newtonsoft.Json;
using System.Collections.ObjectModel;
using TaskAutomation.Models;
using TaskAutomation.ViewModels.TreeItems;

namespace TaskAutomation.ViewModels.Lists
{
    /// <summary>
    /// Список объектов и площадок для КО
    /// </summary>
    public class ListGroupAreaObjects : SubListGroup<IAreaTreeItem, IObjectTreeItem, IParameterTreeItem, IComplexObjectTreeItem>
    {
        private const string TextAreasObjects = "Перечень технологических площадок, ОИ и параметров КО:";
        /// <summary>
        /// Основной конструктор
        /// </summary>
        public ListGroupAreaObjects() : base(TextAreasObjects) { }

        /// <summary>
        /// Дополнительный конструктор
        /// </summary>
        /// <param name="text">Подпись таблицы</param>
        /// <param name="selectedItem">Выбранный айтем таблицы</param>
        /// <param name="items">Элементы списка</param>
        [JsonConstructor]
        public ListGroupAreaObjects(string text, ITreeItem selectedItem, ObservableCollection<ITreeItem> items):base(text, selectedItem, items) { }

        public override IListGroup Copy() => 
            new ListGroupAreaObjects(Text, SelectedItem?.Copy(), Items.Copy<ObservableCollection<ITreeItem>, ITreeItem>());
    }

    /// <summary>
    /// Список объектов для площадки
    /// </summary>
    public class ListGroupObjects : SubListGroup<IObjectTreeItem, IParameterTreeItem, IAreaTreeItem>
    {
        private const string TextObjects = "Перечень ОИ:";
        /// <summary>
        /// Основной конструктор
        /// </summary>
        public ListGroupObjects() : base(TextObjects) { }

        /// <summary>
        /// Дополнительный конструктор
        /// </summary>
        /// <param name="text">Подпись таблицы</param>
        /// <param name="selectedItem">Выбранный айтем таблицы</param>
        /// <param name="items">Элементы списка</param>
        [JsonConstructor]
        public ListGroupObjects(string text, ITreeItem selectedItem, ObservableCollection<ITreeItem> items) : base(text, selectedItem, items) { }
        public override IListGroup Copy() =>
            new ListGroupObjects(Text, SelectedItem?.Copy(), Items.Copy<ObservableCollection<ITreeItem>, ITreeItem>());
    }

    /// <summary>
    /// Список параметров
    /// </summary>
    public class LisGroupParameters : ListGroup<IParameterTreeItem>
    {
        private const string TextParameters = "Перечень параметров";
        /// <summary>
        /// Основной конструктор
        /// </summary>
        public LisGroupParameters() : base(TextParameters) { }

        /// <summary>
        /// Дополнительный конструктор
        /// </summary>
        /// <param name="text">Подпись таблицы</param>
        /// <param name="selectedItem">Выбранный айтем таблицы</param>
        /// <param name="items">Элементы списка</param>
        [JsonConstructor]
        public LisGroupParameters(string text, ITreeItem selectedItem, ObservableCollection<ITreeItem> items) : base(text, selectedItem, items) { }
        public override IListGroup Copy() =>
            new LisGroupParameters(Text,SelectedItem?.Copy(),Items.Copy<ObservableCollection<ITreeItem>,ITreeItem>());
    }
}
