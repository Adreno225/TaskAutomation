using System.Collections.ObjectModel;

namespace TaskAutomation.Models;

public class Subobject:BaseModel
{
    private const string Text = "Подобъект";
    public string Position { get; set; }
    public string ParametersEquipment { get; set; }
    public ObservableCollection <Product> Products { get; set; }
    public ObservableCollection<Parameter> Parameters { get; set; }

    public Subobject():base(Text)
    {
        Position = "";
        ParametersEquipment = "";
        Products = new ObservableCollection<Product> { };
        Parameters = new ObservableCollection<Parameter> { }; 
    }
}