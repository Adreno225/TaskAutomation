namespace TaskAutomation.Models;

public class Product:BaseModel
{
    private const string Text = "Продукт";

    #region Параметры среды 
    private string _Parameters = "";
    public string Parameters
    {
        get => _Parameters;
        set => Set(ref _Parameters, value);
    }
    #endregion

    public Product():base(Text) { }
}