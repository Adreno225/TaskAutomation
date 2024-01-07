using System.Collections.ObjectModel;

namespace TaskAutomation.Models
{
    public class Area: BaseModel
    {
        public ObservableCollection<Object> Objects { get; set; }
        public ObservableCollection<Parameter> Parameters { get; set; }
        public Area()
        {
            Name = "Площадка";
            Objects = new ObservableCollection<Object>();
            Parameters = new ObservableCollection<Parameter>();
        }
    }
}
