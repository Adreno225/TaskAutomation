using System.Collections.ObjectModel;
using TaskAutomationInterfaces;

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

        #region Заказчик 
        private string _Customer;
        public string Customer
        {
            get => _Customer;
            set => Set<string>(ref _Customer, value);
        }
        #endregion

        #region Тип КО 
        private string _TypeCO;
        public string TypeCO
        {
            get => _TypeCO;
            set => Set<string>(ref _TypeCO, value);
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

        #region Объекты 
        private ObservableCollection<Object> _Objects = new ObservableCollection<Object>();
        public ObservableCollection<Object> Objects
        {
            get => _Objects;
            set => Set<ObservableCollection<Object>>(ref _Objects, value);
        }
        #endregion

        #region Параметры 
        private ObservableCollection<Parameter> _Parameters = new ObservableCollection<Parameter>();
        public ObservableCollection<Parameter> Parameters
        {
            get => _Parameters;
            set => Set<ObservableCollection<Parameter>>(ref _Parameters, value);
        }
        #endregion


        public Task()
        {
            Name = "Комплексный объект";
            Code = "";
            Object = "";
            Stage = null;
            Class = null;
        }
    }
}
