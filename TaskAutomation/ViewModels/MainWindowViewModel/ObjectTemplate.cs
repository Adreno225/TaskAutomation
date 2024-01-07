using System.Collections.ObjectModel;

namespace TaskAutomation.ViewModels
{
    public class ObjectTemplate:BaseTemplate
    {

        #region Cписок параметров        
        private ListGroup<Models.Parameter> _ListParameters;
        public ListGroup<Models.Parameter> ListParameters
        {
            get => _ListParameters;
            set => Set(ref _ListParameters, value);
        }
        #endregion

        #region Первая таблица       
        private ObservableCollection<MainDataObject> _MainTableObject;
        public ObservableCollection<MainDataObject> MainTableObject
        {
            get => _MainTableObject;
            set => Set(ref _MainTableObject, value);
        }
        #endregion
    }
}
