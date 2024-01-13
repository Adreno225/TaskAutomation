using System.Collections.ObjectModel;

namespace TaskAutomation.Models
{
    public class Object:BaseModel
    {
        //public ObservableCollection<Subobject> Subobjects { get; set; }

        #region Подобъекты 
        private ObservableCollection<string> _Subobjects;
        public ObservableCollection<string> Subobjects
        {
            get => _Subobjects;
            set => Set<ObservableCollection<string>>(ref _Subobjects, value);
        }
        #endregion

        #region Позиция по ГП 
        private string _Position;
        public string Position
        {
            get => _Position;
            set => Set<string>(ref _Position, value);
        }
        #endregion

        #region Параметры оборудования 
        private string _ParametersEquipment;
        public string ParametersEquipment
        {
            get => _ParametersEquipment;
            set => Set<string>(ref _ParametersEquipment, value);
        }
        #endregion

        #region Продукт 
        private Product _Product;
        public Product Product
        {
            get => _Product;
            set => Set<Product>(ref _Product, value);
        }
        #endregion
        //public ObservableCollection<Product> Products { get; set; }

        #region Параметры 
        private ObservableCollection<Parameter> _Parameters;
        public ObservableCollection<Parameter> Parameters
        {
            get => _Parameters;
            set => Set<ObservableCollection<Parameter>>(ref _Parameters, value);
        }
        #endregion

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
