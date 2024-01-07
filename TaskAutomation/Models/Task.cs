using System.Collections.ObjectModel;

namespace TaskAutomation.Models
{
    public enum Stage
    {
        ОПР,
        ПД,
        РД,
        ПиР
    }
    public enum Class
    {
        Минимальный,
        Базовый,
        Перспективный
    }
    public class Task: BaseModel
    {
        public string Code { get; set; }
        public string Object { get; set; }
        public Stage Stage { get; set; }
        public Class Class { get; set; }
        public ObservableCollection<Area> Areas { get; set; }

        public Task()
        {
            Code = "";
            Object = "";
            Stage = Stage.РД;
            Class = Class.Базовый;
            Areas = new ObservableCollection<Area>();
        }
        public void AddArea()
        {
            Areas.Add(new Area { });
        }
       
    }
}
