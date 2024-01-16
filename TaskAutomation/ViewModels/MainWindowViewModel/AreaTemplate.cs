using TaskAutomation.Models;
using Object = TaskAutomation.Models.Object;

namespace TaskAutomation.ViewModels
{
    public class AreaTemplate:BaseTemplate
    {
        #region Cписок объектов        
        private ListGroup<Object> _ListObjects;
        public ListGroup<Object> ListObjects
        {
            get => _ListObjects;
            set
            {
                Set(ref _ListObjects, value);
                ListObjects.Items = ListObjects.Items;
            } 
        }
        #endregion

        #region Cписок параметров        
        private ListGroup<Models.Parameter> _ListParameters;
        public ListGroup<Models.Parameter> ListParameters
        {
            get => _ListParameters;
            set => Set(ref _ListParameters, value);
        }
        #endregion

        public override void SetTemplate(MainWindowViewModel vM)
        {
            SelectedItem = vM.SelectedTreeViewItem;
            ListParameters = new ListGroup<Parameter>(((Area)SelectedItem).Parameters, SelectedItem);
            ListObjects = new ListGroup<Object>(((Area)SelectedItem).Objects, SelectedItem);
            vM.TypeSelectedItem = TypeSelectedItem.Area;
        }
    }
}
