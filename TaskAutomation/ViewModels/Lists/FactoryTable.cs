using Newtonsoft.Json;
using System.Collections.ObjectModel;
using TaskAutomation.Models;
using TaskAutomation.ViewModels.SubClasses;

namespace TaskAutomation.ViewModels.Lists
{
    /// <summary>
    /// Таблица сигнализаций
    /// </summary>
    public class TableSignalings : TableGroup<ISignaling>
    {
        private const string TextSignalings = "Перечень сигнализаций:";
        public TableSignalings() : base(TextSignalings) { }
        
        [JsonConstructor]
        public TableSignalings(string text, ISignaling selectedItem, ObservableCollection<ISignaling> items) : base(text, selectedItem, items) { }

        public override ITableGroup<ISignaling> Copy() => 
            new TableSignalings(Text,SelectedItem?.Copy(), Items.Copy<ObservableCollection<ISignaling>,ISignaling>());
    }

    /// <summary>
    /// Таблица алгоритмов
    /// </summary>
    public class TableAlgorythmes : TableGroup<IAlgorithm>
    {
        private const string TextAlg = "Перечень алгоритмов:";
        public TableAlgorythmes() : base(TextAlg) { }

        [JsonConstructor]
        public TableAlgorythmes(string text, IAlgorithm selectedItem, ObservableCollection<IAlgorithm> items) : base(text, selectedItem, items) { }
        public override ITableGroup<IAlgorithm> Copy() =>
            new TableAlgorythmes(Text, SelectedItem?.Copy(), Items.Copy<ObservableCollection<IAlgorithm>, IAlgorithm>());
    }
}
