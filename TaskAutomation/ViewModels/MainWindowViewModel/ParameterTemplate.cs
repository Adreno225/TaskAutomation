using System.Collections.ObjectModel;
using TaskAutomation.Models;

namespace TaskAutomation.ViewModels
{
    public class ParameterTemplate:BaseTemplate
    {
        #region Таблица параметра       
        private ObservableCollection<Parameter> _MainTableParameter = new ObservableCollection<Parameter> { new Parameter()};
        public ObservableCollection<Parameter>  MainTableParameter
        {
            get => _MainTableParameter;
            set => Set(ref _MainTableParameter, value);
        }
        #endregion
        #region Cписок алгоритмов        
        private ListGroup<Algorithm> _ListAlgorithms;
        public ListGroup<Algorithm> ListAlgorithms
        {
            get => _ListAlgorithms;
            set => Set(ref _ListAlgorithms, value);
        }
        #endregion
        #region Cписок сигнализаций        
        private ListGroup<Signaling> _ListSignalings;
        public ListGroup<Signaling> ListSignalings
        {
            get => _ListSignalings;
            set => Set(ref _ListSignalings, value);
        }
        #endregion
        public override void SetTemplate(MainWindowViewModel vM)
        {
            SelectedItem = vM.SelectedTreeViewItem;
            MainTableParameter[0] = (Parameter)SelectedItem;
            ListAlgorithms = new ListGroup<Algorithm>(((Parameter)SelectedItem).Algorithms, SelectedItem);
            ListSignalings = new ListGroup<Signaling>(((Parameter)SelectedItem).Signalings, SelectedItem);
            vM.TypeSelectedItem = TypeSelectedItem.Parameter;
        }
    }
}
