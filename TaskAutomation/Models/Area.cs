using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;

namespace TaskAutomation.Models
{
    public class Area: BaseModel
    {

        #region Объекты 
        private ObservableCollection<Models.Object> _Objects;
        public ObservableCollection<Models.Object> Objects
        {
            get => _Objects;
            set => Set<ObservableCollection<Models.Object>>(ref _Objects, value);
        }
        #endregion

        #region Параметры 
        private ObservableCollection<Parameter> _Parameters = new ObservableCollection<Parameter>();
        public ObservableCollection<Parameter> Parameters
        {
            get => _Parameters;
            set => Set<ObservableCollection<Parameter>>(ref _Parameters, value);
        }
        #endregion

        private ParametersArea _ParametersArea;
            
        #region Объекты 
        private ObservableCollection<Object> _ObjectsParameters = new ObservableCollection<Object>();
        public IEnumerable ObjectsParameters
        {
            get => SetObjParams();
        }
        #endregion
        public Area()
        {
            Name = "Площадка";
            Objects = new ObservableCollection<Object>();
            _ParametersArea= new ParametersArea() { Parameters=_Parameters};
            ((ObservableCollection<Object>)_ObjectsParameters).Add(_ParametersArea);
            Parameters.CollectionChanged += _ObjectsParameters_CollectionChanged;
            Objects.CollectionChanged += _ObjectsParameters_CollectionChanged;
        }

        private void _ObjectsParameters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(ObjectsParameters));
        }

        private IEnumerable SetObjParams()
        {
            _ObjectsParameters.Clear();
            if (Parameters.Count != 0)
            {
                _ObjectsParameters.Add(_ParametersArea);
                return _ObjectsParameters.Concat(Objects);
            }
            else
                return Objects;
        }
    }
}
