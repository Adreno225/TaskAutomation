using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TaskAutomation.Models
{
    public class Parameter:BaseModel
    {
        #region Измеримость 
        private bool _IsControl;
        public bool IsControl
        {
            get => _IsControl;
            set => Set<bool>(ref _IsControl, value);
        }
        #endregion

        #region Ед.изм. 
        private string _Unit;
        public string Unit
        {
            get => _Unit;
            set => Set<string>(ref _Unit, value);
        }
        #endregion

        #region ПАЗ 
        private bool _ESD;
        public bool ESD
        {
            get => _ESD;
            set => Set<bool>(ref _ESD, value);
        }
        #endregion

        #region Режим 
        private Mode _Mode;
        public Mode Mode
        {
            get => _Mode;
            set => Set<Mode>(ref _Mode, value);
        }
        #endregion

        #region Диапазон измерения 
        private string _RangeMeasure;
        public string RangeMeasure
        {
            get => _RangeMeasure;
            set => Set<string>(ref _RangeMeasure, value);
        }
        #endregion

        #region Расчетное значение 
        private string _CalculatedValue;
        public string CalculatedValue
        {
            get => _CalculatedValue;
            set => Set<string>(ref _CalculatedValue, value);
        }
        #endregion

        #region Диапазон управления/регулирования 
        private string _RangeControl;
        public string RangeControl
        {
            get => _RangeControl;
            set => Set<string>(ref _RangeControl, value);
        }
        #endregion

        #region Сигнализации 
        private ObservableCollection<Signaling> _Signalings;
        public ObservableCollection<Signaling> Signalings
        {
            get => _Signalings;
            set => Set<ObservableCollection<Signaling>>(ref _Signalings, value);
        }
        #endregion

        #region Алгоритмы 
        private ObservableCollection<Algorithm> _Algorithms;
        public ObservableCollection<Algorithm> Algorithms
        {
            get => _Algorithms;
            set => Set<ObservableCollection<Algorithm>>(ref _Algorithms, value);
        }
        #endregion

        #region Примечание 
        private string _Note;
        public string Note
        {
            get => _Note;
            set => Set<string>(ref _Note, value);
        }
        #endregion

        public Parameter()
        {
            Name = "Параметр";
            IsControl = true;
            Unit = "";
            ESD = false;
            Mode = Mode.Both;
            RangeMeasure = "";
            CalculatedValue = "";
            RangeControl = "";
            Signalings = new ObservableCollection<Signaling>();
            Algorithms = new ObservableCollection<Algorithm>();
            Note = "";
        }
    }
}
