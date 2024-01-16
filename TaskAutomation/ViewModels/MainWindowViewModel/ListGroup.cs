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

        #region Родитель
        private BaseModel _Parent;
        public BaseModel Parent
        {
            get => _Parent;
            set => Set(ref _Parent, value);
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

        private void OnAddItemCommandExecuted(object p) 
        {
            Items.Add(new T());
        } 
        #endregion

        #region Удаление выбранного айтема
        public ICommand RemoveSelectedItemCommand { get; }

        private bool CanRemoveSelectedItemCommandExecute(object p) => SelectedItem != null;

        private void OnRemoveSelectedItemCommandExecuted(object p) 
        {
            Items.Remove(SelectedItem);
        } 
        #endregion

        #region Копирование выбранного айтема
        public ICommand CopySelectedItemCommand { get; }

        private bool CanCopySelectedItemCommandExecute(object p) => SelectedItem!=null;

        private void OnCopySelectedItemCommandExecuted(object p) 
        {
            Items.Add(SelectedItem);
            if (Parent is Area)
                ((Area)Parent).Parameters = ((Area)Parent).Parameters;
        }
        #endregion

        #region Перемещение айтема вверх
        public ICommand UpSelectedItemCommand { get; }

        private bool CanUpSelectedItemExecute(object p) => SelectedItem!=null && Items.IndexOf(SelectedItem)!=0;

        private void OnUpSelectedItemExecuted(object p)
        {
            var index = Items.IndexOf(SelectedItem);
            Items.Move(index,index-1);
        }
        #endregion

        #region Перемещение айтема вниз
        public ICommand DownSelectedItemCommand { get; }

        private bool CanDownSelectedItemExecute(object p) => SelectedItem != null && Items.IndexOf(SelectedItem) != Items.Count-1;

        private void OnDownSelectedItemExecuted(object p)
        {
            var index = Items.IndexOf(SelectedItem);
            Items.Move(index, index + 1);
        }
        #endregion

        public ListGroup(ObservableCollection<T> items, BaseModel parent)
        {
            Items = items;
            Parent = parent;
            DefineText(items);
            #region Команды
            AddItemCommand = new LambdaCommand(OnAddItemCommandExecuted, CanAddItemCommandExecute);
            RemoveSelectedItemCommand = new LambdaCommand(OnRemoveSelectedItemCommandExecuted, CanRemoveSelectedItemCommandExecute);
            CopySelectedItemCommand = new LambdaCommand(OnCopySelectedItemCommandExecuted, CanCopySelectedItemCommandExecute);
            UpSelectedItemCommand = new LambdaCommand(OnUpSelectedItemExecuted, CanUpSelectedItemExecute);
            DownSelectedItemCommand = new LambdaCommand(OnDownSelectedItemExecuted, CanDownSelectedItemExecute);
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

