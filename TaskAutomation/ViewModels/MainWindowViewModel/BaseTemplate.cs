using TaskAutomation.Models;
using TaskAutomation.ViewModels.Base;

namespace TaskAutomation.ViewModels
{
    public abstract class BaseTemplate: ViewModel
    {
        #region Поле наименование айтема
        private TreeItem _SelectedItem;
        public TreeItem SelectedItem
        {
            get => _SelectedItem;
            set => Set(ref _SelectedItem, value);
        }
        #endregion
        public abstract void SetTemplate(MainWindowViewModel vM);
    }
}
