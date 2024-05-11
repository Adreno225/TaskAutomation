using TaskAutomation.Models;
using Object = TaskAutomation.Models.ObjectInf;

namespace TaskAutomation.ViewModels
{
    public class AreaTemplate:BaseTemplate
    {
        //#region Cписок объектов        
        //private ListGroup<Object> _ListObjects;
        //public ListGroup<Object> ListObjects
        //{
        //    get => _ListObjects;
        //    set => Set(ref _ListObjects, value);
        //}
        //#endregion

        //#region Cписок параметров        
        //private ListGroup<Parameter> _ListParameters;
        //public ListGroup<Parameter> ListParameters
        //{
        //    get => _ListParameters;
        //    set => Set(ref _ListParameters, value);
        //}
        //#endregion

        public override void SetTemplate(MainWindowViewModel vM)
        {
            SelectedItem = vM.SelectedTreeViewItem;
            var area = (Area)SelectedItem.Object;
            //ListParameters = new ListGroup<Parameter> (SelectedItem, area.Parameters);
            //ListObjects = new ListGroup<Object>(area.Objects);
            vM.TypeSelectedItem = TypeSelectedItem.Area;
        }
    }
}
