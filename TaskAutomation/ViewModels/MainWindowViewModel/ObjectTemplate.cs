using System.Collections.ObjectModel;
using TaskAutomation.Models;

namespace TaskAutomation.ViewModels
{
    public class ObjectTemplate:BaseTemplate
    {

        #region Cписок параметров        
        private ListGroup<Models.Parameter> _ListParameters;
        public ListGroup<Models.Parameter> ListParameters
        {
            get => _ListParameters;
            set => Set(ref _ListParameters, value);
        }
        #endregion

        //#region Первая таблица       
        //private ObservableCollection<MainDataObject> _MainTableObject;
        //public ObservableCollection<MainDataObject> MainTableObject
        //{
        //    get => _MainTableObject;
        //    set => Set(ref _MainTableObject, value);
        //}
        //#endregion

        #region Первая таблица       
        private ObservableCollection<Object> _MainTableObject = new ObservableCollection<Object> { new Object() };
        public ObservableCollection<Object> MainTableObject
        {
            get => _MainTableObject;
            set => Set(ref _MainTableObject, value);
        }
        #endregion
        public override void SetTemplate(MainWindowViewModel vM)
        {
            SelectedItem = vM.SelectedTreeViewItem;
            ListParameters = new ListGroup<Parameter>(((Object)SelectedItem).Parameters, SelectedItem);
            MainTableObject[0] = (Object)SelectedItem;
            vM.TypeSelectedItem = TypeSelectedItem.Object;
        }
    }
}
