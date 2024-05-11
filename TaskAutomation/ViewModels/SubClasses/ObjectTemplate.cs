using System.Collections.ObjectModel;
using TaskAutomation.Models;

namespace TaskAutomation.ViewModels
{
    public class ObjectTemplate:BaseTemplate
    {

        #region Cписок параметров        
        //private ListGroup<Parameter> _ListParameters;
        //public ListGroup<Parameter> ListParameters
        //{
        //    get => _ListParameters;
        //    set => Set(ref _ListParameters, value);
        //}
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
        private ObservableCollection<ObjectInf> _MainTableObject = new ObservableCollection<ObjectInf> { new ObjectInf() };
        public ObservableCollection<ObjectInf> MainTableObject
        {
            get => _MainTableObject;
            set => Set(ref _MainTableObject, value);
        }
        #endregion
        public override void SetTemplate(MainWindowViewModel vM)
        {
            SelectedItem = vM.SelectedTreeViewItem;
            //ListParameters = new ListGroup<Parameter>(((Object)SelectedItem.Object).Parameters);
            MainTableObject[0] = (ObjectInf)SelectedItem.Object;
            vM.TypeSelectedItem = TypeSelectedItem.Object;
        }

    }
}
