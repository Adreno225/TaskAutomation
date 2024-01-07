using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TaskAutomation.Models
{
    public class Parameter:BaseModel
    {
        public bool IsControl { get; set; }
        public string Unit { get; set; }
        public bool ESD { get; set; }
        public Mode Mode { get; set; }
        public string RangeMeasure { get; set; }
        public string CalculatedValue { get; set; }
        public string RangeControl { get; set; }
        public ObservableCollection<Signaling> Signalings { get; set; }
        public ObservableCollection<Algorithm> Algorithms { get; set; }
        public string Note { get; set; }
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
        }
    }
}
