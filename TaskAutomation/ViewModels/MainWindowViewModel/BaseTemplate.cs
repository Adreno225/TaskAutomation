using TaskAutomation.Models;
using TaskAutomation.ViewModels.Base;

namespace TaskAutomation.ViewModels
{
    public abstract class BaseTemplate: ViewModel
    {
        #region Поле наименование айтема
        private BaseModel _SelectedItem;
        public BaseModel SelectedItem
        {
            get => _SelectedItem;
            set => Set(ref _SelectedItem, value);
        }
        #endregion
        public abstract void SetTemplate(MainWindowViewModel vM);
    }
}
