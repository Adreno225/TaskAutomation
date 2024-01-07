using System.Collections.ObjectModel;

namespace TaskAutomation.Models
{
    public class Subobject:BaseModel
    {
        public string Position { get; set; }
        public string ParametersEquipment { get; set; }
        public ObservableCollection <Product> Products { get; set; }
        public ObservableCollection<Parameter> Parameters { get; set; }

        public Subobject()
        {
            Name = "Подобъект";
            Position = "";
            ParametersEquipment = "";
            Products = new ObservableCollection<Product> { };
            Parameters = new ObservableCollection<Parameter> { }; 
        }
    }
}
