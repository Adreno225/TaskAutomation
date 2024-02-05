using System.Collections.ObjectModel;
using TaskAutomation.Models;

namespace TaskAutomation.ViewModels
{
    public class TaskTemplate : BaseTemplate
    {
        #region Cписок Площадок        
        //private ListGroup<Area> _ListAreas;
        //public ListGroup<Area> ListAreas
        //{
        //    get => _ListAreas;
        //    set => Set(ref _ListAreas, value);
        //}
        #endregion
        public TaskTemplate(ITreeItem treeItem, ObservableCollection<IItem> listAreas )
        {
            SelectedItem = treeItem;
            //ListAreas = new ListGroup<Area>(listAreas);
        }

        public override void SetTemplate(MainWindowViewModel vM)
        {
            vM.TypeSelectedItem = TypeSelectedItem.Task;
        }
    }
}
