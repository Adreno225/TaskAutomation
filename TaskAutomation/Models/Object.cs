using System.Collections.ObjectModel;

namespace TaskAutomation.Models
{
    public class Object:BaseModel
    {
        //public ObservableCollection<Subobject> Subobjects { get; set; }
        public ObservableCollection<string> Subobjects { get; set; }
        public string Position { get; set; }
        public string ParametersEquipment { get; set; }
        public Product Product { get; set; }
        //public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Parameter> Parameters { get; set; }

        public Object()
        {
            Name = "Объект";
            Subobjects = new ObservableCollection<string>() { string.Empty};
            Position = "";
            ParametersEquipment = "";
            Product = new Product();
            Parameters = new ObservableCollection<Parameter>();
        }
    }
}
