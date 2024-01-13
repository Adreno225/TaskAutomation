using System.Collections.ObjectModel;

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
        private ObservableCollection<Models.Object> _ObjectsParameters = new ObservableCollection<Object>();
        public ObservableCollection<Models.Object> ObjectsParameters
        {
            get
            {
                _ObjectsParameters.Clear();
                _ObjectsParameters.Add(_ParametersArea);
                var count = Objects.Count;
                for (int i = 0; i < count; i++)
                    _ObjectsParameters.Add(Objects[i]);
                return _ObjectsParameters;
                //if (_Parameters.Count != 0)
                //{
                //    _ObjectsParameters.Clear();
                //    _ObjectsParameters[0] = new Object() { Name = "Параметры площадки", Parameters = _Parameters };
                //    var count = _Objects.Count;
                //    for (int i = 0; i < count; i++)
                //        _ObjectsParameters[i+1] = Objects[i];
                //    return _ObjectsParameters;
                //}
                //else { return _Objects; }
            }
            set => Set<ObservableCollection<Models.Object>>(ref _ObjectsParameters, value);
        }
        #endregion
        public Area()
        {
            Name = "Площадка";
            Objects = new ObservableCollection<Object>();
            _ParametersArea= new ParametersArea() { Parameters=_Parameters};
        }
    }
}
