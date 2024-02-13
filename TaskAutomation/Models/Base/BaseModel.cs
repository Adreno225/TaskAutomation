namespace TaskAutomation.Models;

public interface IItem
{
    string Name { get; set; }
}
public abstract class BaseModel : ViewModels.Base.ViewModel<object>, IItem
{
    private string _Name;
    public string Name 
    { 
        get => _Name; 
        set => Set(ref _Name, value); 
    }
    protected BaseModel(string name)
    {
        Name = name; 
    }
}