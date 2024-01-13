using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskAutomation.Infrastructure.Commands;
using TaskAutomation.Models;
using TaskAutomation.ViewModels.Base;

namespace TaskAutomation.ViewModels
{
    public class ListGroup<T>: ViewModel
        where T:BaseModel,new()
    {
        #region Подпись в списке
        private string _Text;
        public string Text
        {
            get => _Text;
            set => Set(ref _Text, value);
        }
        #endregion

        #region Выбранный айтем
        private T _SelectedItem;
        public T SelectedItem
        {
            get => _SelectedItem;
            set => Set(ref _SelectedItem, value);
        } 
        #endregion

        #region Список айтемов
        private ObservableCollection<T> _Items;
        public ObservableCollection<T> Items
        {
            get => _Items;
            set => Set(ref _Items, value);
        } 
        #endregion

        #region Добавление айтема
        public ICommand AddItemCommand { get; }

        private bool CanAddItemCommandExecute(object p) => true;

        private void OnAddItemCommandExecuted(object p) => Items.Add(new T());
        #endregion

        #region Добавление удаление выбранного айтема
        public ICommand RemoveSelectedItemCommand { get; }

        private bool CanRemoveSelectedItemCommandExecute(object p) => SelectedItem != null;

        private void OnRemoveSelectedItemCommandExecuted(object p) => Items.Remove(SelectedItem);
        #endregion

        #region Копирование выбранного айтема
        public ICommand CopySelectedItemCommand { get; }

        private bool CanCopySelectedItemCommandExecute(object p) => SelectedItem!=null;

        private void OnCopySelectedItemCommandExecuted(object p) => Items.Add(SelectedItem);
        #endregion

        public ListGroup(ObservableCollection<T> items)
        {
            Items = items;
            DefineText(items);
            #region Команды
            AddItemCommand = new LambdaCommand(OnAddItemCommandExecuted, CanAddItemCommandExecute);
            RemoveSelectedItemCommand = new LambdaCommand(OnRemoveSelectedItemCommandExecuted, CanRemoveSelectedItemCommandExecute);
            CopySelectedItemCommand = new LambdaCommand(OnCopySelectedItemCommandExecuted, CanCopySelectedItemCommandExecute);
            #endregion
        }
        private void DefineText(ObservableCollection<T> items)
        {
            switch (items)
            {
                case ObservableCollection<Area>:
                    Text = "Перечень технологических площадок:";
                    break;
                case ObservableCollection<Object>:
                    Text = "Перечень объектов:";
                    break;
                case ObservableCollection<Parameter>:
                    Text = "Перечень параметров:";
                    break;
                case ObservableCollection<Signaling>:
                    Text = "Перечень сигнализаций:";
                    break;
                case ObservableCollection<Algorithm>:
                    Text = "Перечень алгоритмов:";
                    break;
                default:
                    break;
            }
        }
    }
}

