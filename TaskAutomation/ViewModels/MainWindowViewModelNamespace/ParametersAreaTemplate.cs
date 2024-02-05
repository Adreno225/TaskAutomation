using System.Collections.ObjectModel;
using TaskAutomation.Models;

namespace TaskAutomation.ViewModels
{
    public class ParametersAreaTemplate : BaseTemplate
    {
        #region Cписок параметров        
        //private ListGroup<Parameter> _ListParameters;
        //public ListGroup<Parameter> ListParameters
        //{
        //    get => _ListParameters;
        //    set => Set(ref _ListParameters, value);
        //}
        #endregion
        public override void SetTemplate(MainWindowViewModel vM)
        {
            SelectedItem = vM.SelectedTreeViewItem;
            //ListParameters = new ListGroup<Parameter>(((ObservableCollection<Parameter>)SelectedItem.Object));
            vM.TypeSelectedItem = TypeSelectedItem.ParametersArea;
        }
    }
}
