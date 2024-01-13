namespace TaskAutomation.Models
{
    public enum Mode
    {
        Manual,
        Remote,
        Both
    }
    public abstract class BaseModel: ViewModels.Base.ViewModel
    {
        private string _Name;
        public string Name { get => _Name; set => Set<string>(ref _Name,value); }
    }
}
