using System.Collections.ObjectModel;
using TaskAutomation.Models;
using TaskAutomation.ViewModels.Base;

namespace TaskAutomation.ViewModels
{
    public class MainDataObject: ViewModel
    {
        #region Параметр оборудования        
        private string _ParameterEquipment;
        public string ParameterEquipment
        {
            get => _ParameterEquipment;
            set => Set(ref _ParameterEquipment, value);
        }
        #endregion

        #region Поз.по ГП        
        private string _NumGP;
        public string NumGP
        {
            get => _NumGP;
            set => Set(ref _NumGP, value);
        }
        #endregion

        #region Поз.по схеме        
        private ObservableCollection<string> _Positions;
        public ObservableCollection<string> Positions
        {
            get => _Positions;
            set => Set(ref _Positions, value);
        }
        #endregion

        #region Наименование продукта       
        private string _NameProduct;
        public string NameProduct
        {
            get => _NameProduct;
            set => Set(ref _NameProduct, value);
        }
        #endregion

        #region Параметры рабочей среды      
        private string _ParameterProduct;
        public string ParameterProduct
        {
            get => _ParameterProduct;
            set => Set(ref _ParameterProduct, value);
        }
        #endregion
        public MainDataObject(Object @object)
        {
            ParameterEquipment = @object.ParametersEquipment;
            NumGP = @object.Position;
            Positions = @object.Subobjects;
            NameProduct = @object.Product.Name;
            ParameterProduct = @object.Product.Parameters;
        }
    }
}
