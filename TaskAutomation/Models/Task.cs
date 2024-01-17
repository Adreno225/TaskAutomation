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

        #region Шифр 
        private string _Code;
        public string Code
        {
            get => _Code;
            set => Set<string>(ref _Code, value);
        }
        #endregion

        #region Объект 
        private string _Object;
        public string Object
        {
            get => _Object;
            set => Set<string>(ref _Object, value);
        }
        #endregion

        #region Стадия 
        private string _Stage;
        public string Stage
        {
            get => _Stage;
            set => Set<string>(ref _Stage, value);
        }
        #endregion

        #region Класс 
        private string _Class;
        public string Class
        {
            get => _Class;
            set => Set<string>(ref _Class, value);
        }
        #endregion

        #region Площадки 
        private ObservableCollection<Area> _Areas = new ObservableCollection<Area>();
        public ObservableCollection<Area> Areas
        {
            get => _Areas;
            set => Set<ObservableCollection<Area>>(ref _Areas, value);
        }
        #endregion

        public Task()
        {
            Name = "Комплексный объект";
            Code = "";
            Object = "";
            Stage = "";
            Class = "";
        }
        public void AddArea()
        {
            Areas.Add(new Area { });
        }  
    }
}
