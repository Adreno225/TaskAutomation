using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using TaskAutomation.Models;
using Newtonsoft.Json;

namespace TaskAutomation.ViewModels.Lists
{
    /// <summary>
    /// Базовый интерфейс списочных элементов
    /// </summary>
    /// <typeparam name="TypeItems">Тип элементов списка</typeparam>
    /// <typeparam name="TypeCopy">Выходной тип при копировании списка</typeparam>
    public interface IBaseGroup<TypeItems,TypeCopy>:ICopy<TypeCopy> 
        where TypeItems:ICopy<TypeItems> where TypeCopy : IBaseGroup<TypeItems,TypeCopy>
    {
        /// <summary>
        /// Подпись над списком
        /// </summary>
        string Text { get; }
        /// <summary>
        /// Выбранный элемент списка
        /// </summary>
        TypeItems SelectedItem { get; set; }
        /// <summary>
        /// Элементы списка
        /// </summary>
        ObservableCollection<TypeItems> Items { get; }
        /// <summary>
        /// Комманда на добавление элемента в список
        /// </summary>
        IRelayCommand AddItemCommand { get; }
        /// <summary>
        /// Комманда на удаление элемента из списка
        /// </summary>
        IRelayCommand RemoveSelectedItemCommand { get; }
        /// <summary>
        /// Команда копирования выбранного элемента из списка
        /// </summary>
        IRelayCommand CopySelectedItemCommand { get; }
    }

    /// <summary>
    /// Реализация базового интерфейса списков
    /// </summary>
    /// <typeparam name="TypeItems">Тип элементов списка</typeparam>
    /// <typeparam name="TypeCopy">Выходной тип при копировании списка</typeparam>
    public abstract partial class BaseGroup<TypeItems,TypeCopy> : ObservableObject, IBaseGroup<TypeItems,TypeCopy>
        where TypeItems : ICopy<TypeItems> where TypeCopy : IBaseGroup<TypeItems, TypeCopy>
    {
        #region Подпись в списке
        public string Text { get; }
        #endregion

        #region Выбранный айтем
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RemoveSelectedItemCommand))]
        [NotifyCanExecuteChangedFor(nameof(CopySelectedItemCommand))]
        private TypeItems _selectedItem;
        #endregion
        public ObservableCollection<TypeItems> Items { get; }

        #region Добавление айтема
        [RelayCommand]
        [property: JsonIgnore]
        protected virtual void AddItem() => AddItem<TypeItems>();
        #endregion

        #region Удаление выбранного айтема
        [RelayCommand(CanExecute = nameof(IsSelectedCanCommandExecute))]
        [property: JsonIgnore]
        protected virtual void RemoveSelectedItem() => Items.Remove(SelectedItem);
        #endregion

        #region Копирование выбранного айтема
        [RelayCommand(CanExecute = nameof(IsSelectedCanCommandExecute))]
        [property: JsonIgnore]
        protected void CopySelectedItem() => Items.Add(SelectedItem.Copy());
        #endregion
        
        /// <summary>
        /// Контруктор базового класса списков
        /// </summary>
        /// <param name="text">Подпись над списком</param>
        protected BaseGroup(string text)
        {
            Text = text;
            Items = new ObservableCollection<TypeItems>();
        }
        /// <summary>
        /// Дополнительный конструктор
        /// </summary>
        /// <param name="text">Подпись таблицы</param>
        /// <param name="selectedItem">Выбранный айтем таблицы</param>
        /// <param name="items">Элементы списка</param>
        public BaseGroup(string text, TypeItems selectedItem, ObservableCollection<TypeItems> items)
        {
            Text = text;
            SelectedItem = selectedItem;
            Items = items;
        }
        /// <summary>
        /// Универсальный метод добавления элемента в коллекцию
        /// </summary>
        /// <typeparam name="Y">Тип добавляемого элемента</typeparam>
        protected void AddItem<Y>() where Y : TypeItems
        {
            Items.Add(App.Services.GetRequiredService<Y>());
        }
        /// <summary>
        /// Метод определения, выбран ли объект в списке
        /// </summary>
        /// <returns>Результат метода</returns>
        protected virtual bool IsSelectedCanCommandExecute() => SelectedItem != null;

        /// <summary>
        /// Копирование объекта
        /// </summary>
        /// <returns></returns>
        public abstract TypeCopy Copy();
    }
}
