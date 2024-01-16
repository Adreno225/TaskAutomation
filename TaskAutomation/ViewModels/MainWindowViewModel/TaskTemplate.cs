using System.Collections.ObjectModel;
using TaskAutomation.Models;

namespace TaskAutomation.ViewModels
{
    public class TaskTemplate : BaseTemplate
    {
        #region Cписок Площадок        
        private ListGroup<Area> _ListAreas;
        public ListGroup<Area> ListAreas
        {
            get => _ListAreas;
            set => Set(ref _ListAreas, value);
        }
        #endregion
        public TaskTemplate(BaseModel item, ObservableCollection<Area> listAreas )
        {
            SelectedItem = item;
            ListAreas = new ListGroup<Area>(listAreas, null);
        }

        public override void SetTemplate(MainWindowViewModel vM)
        {
            vM.TypeSelectedItem = TypeSelectedItem.Task;
        }
    }
}
