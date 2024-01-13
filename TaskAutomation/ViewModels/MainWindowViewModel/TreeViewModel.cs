using TaskAutomation.ViewModels.Base;

namespace TaskAutomation.ViewModels
{
    public class TreeViewModel:ViewModel
    {
        private static object _selectedItem = null;
        
        public static object SelectedItem
        {
            get { return _selectedItem; }
            private set 
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnSelectedItemChanged();
                }
            }
        }

        private static void OnSelectedItemChanged()
        {
            // Raise event / do other things
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                Set<bool>(ref _isSelected, value);
                if (_isSelected) 
                    SelectedItem = this;
            }
        }
    }
}
