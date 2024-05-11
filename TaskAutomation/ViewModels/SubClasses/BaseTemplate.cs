using TaskAutomation.Models;
using TaskAutomation.ViewModels.Base;

namespace TaskAutomation.ViewModels
{
    public abstract class BaseTemplate: ViewModel<object>
    {
        #region Поле наименование айтема
        private ITreeItem _SelectedItem;
        public ITreeItem SelectedItem
        {
            get => _SelectedItem;
            set => Set(ref _SelectedItem, value);
        }
        #endregion
        public abstract void SetTemplate(MainWindowViewModel vM);
    }
}
