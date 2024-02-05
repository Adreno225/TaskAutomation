﻿using System.Linq;
using System.Windows.Input;
using TaskAutomation.Infrastructure.Commands;
using TaskAutomation.Models;
using TaskAutomation.Services;
using TaskAutomation.ViewModels.MainWindowViewModelNamespace;
using TaskAutomationDB.Entities;
using TaskAutomationInterfaces;
using Class = TaskAutomationDB.Entities.Class;

namespace TaskAutomation.ViewModels;

public enum TypeSelectedItem
{
    Task,
    Area,
    Object,
    Parameter,
    ParametersArea,
    None
}
public class MainWindowViewModel : Base.ViewModel<object>
{ 
    private readonly ICreatorTask _ExcelCreator;

    #region Задание
    private TaskClass _Task;
    public TaskClass Task
    {
        get => _Task;
        set => Set(ref _Task, value);
    }
    #endregion

    #region Источник для списков с заданиями
    private TableOneRow<TaskClass> _Tasks;

    public TableOneRow<TaskClass> Tasks
    {
        get => _Tasks;
        set => Set(ref _Tasks, value);
    } 
    #endregion

    #region Перечень классов автоматизации 
    private readonly IRepository<Class> _RepositoryClasses;
    public string[] Classes
    {
        get => _RepositoryClasses.Items.Select(x=>x.Name).ToArray();
    }
    #endregion

    #region Перечень стадий 
    private readonly IRepository<TaskAutomationDB.Entities.Stage> _RepositoryStages;
    public string[] Stages
    {
        get => _RepositoryStages.Items.Select(x => x.Name).ToArray();
    }
    #endregion

    #region Перечень режимов 
    private readonly IRepository<Mode> _RepositoryModes;
    public string[] Modes
    {
        get => _RepositoryModes.Items.Select(x => x.Name).ToArray();
    }
    #endregion

    #region Перечень Заказчиков 
    private readonly IRepository<Customer> _RepositoryCustomers;
    public string[] Customers
    {
        get => _RepositoryCustomers.Items.Select(x => x.Name).ToArray();
    }
    #endregion

    #region Перечень типов КО 
    private readonly IRepository<TypeCO> _RepositoryTypesCO;
    public string[] TypesCO
    {
        get => _RepositoryTypesCO.Items.Select(x => x.Name).ToArray();
    }
    #endregion

    #region Первый уровень дерева 
    private TableOneRow<TaskTreeItem> _FirstLevelTree;
    public TableOneRow<TaskTreeItem> FirstLevelTree
    { 
        get => _FirstLevelTree;
        set => Set(ref _FirstLevelTree, value);
    }
    #endregion

    #region Выбранный объект в дереве
    private ITreeItem _SelectedTreeViewItem;

    public ITreeItem SelectedTreeViewItem
    {
        get => _SelectedTreeViewItem;
        set => Set(ref _SelectedTreeViewItem, value);
    }
    #endregion

    #region Тип выбранного объекта в дереве
    private TypeSelectedItem _TypeSelectedItem = TypeSelectedItem.None;
    public TypeSelectedItem TypeSelectedItem
    {
        get => _TypeSelectedItem;
        set => Set(ref _TypeSelectedItem, value);
    }
    #endregion


    public void SelectTemplate()
    {
        TypeSelectedItem = SelectedTreeViewItem switch
        {
            TaskTreeItem => TypeSelectedItem.Task,
            AreaTreeItem => TypeSelectedItem.Area,
            SubTreeItem => TypeSelectedItem.ParametersArea,
            ObjectTreeItem => TypeSelectedItem.Object,
            ParameterTreeItem => TypeSelectedItem.Parameter,
            _ => TypeSelectedItem.None,
        };
    }



    #region Настройки по умолчанию
    public ICommand SetDefaultSettingsCommand { get; }

    private bool CanSetDefaultSettingsCommandExecute(object p) => true;

    private void OnSetDefaultSettingsCommandExecuted(object p) => SetDefaultSettings();
    #endregion

    #region Создание задания
    public ICommand CreateExcelCommand { get; }

    private bool CanCreateExcelCommandExecute(object p) => true;

    private void OnCreateExcelExecuted(object p) => _ExcelCreator.Create();
    #endregion

    public MainWindowViewModel(ICreatorTask creator,
        IRepository<Class> repositoryClasses,
        IRepository<TaskAutomationDB.Entities.Stage> repositoryStages, IRepository<Mode> repositoryModes,
        IRepository<Customer> repositoryCustomers, IRepository<TypeCO> repositoryTypesCO)
    {
        _ExcelCreator = creator;
        _RepositoryClasses = repositoryClasses;
        _RepositoryStages = repositoryStages;
        _RepositoryModes = repositoryModes;
        _RepositoryCustomers = repositoryCustomers;
        _RepositoryTypesCO = repositoryTypesCO;
        _ExcelCreator.MainModel = this;
            


        SetDefaultSettingsCommand = new LambdaCommand(OnSetDefaultSettingsCommandExecuted, CanSetDefaultSettingsCommandExecute);
        CreateExcelCommand = new LambdaCommand(OnCreateExcelExecuted, CanCreateExcelCommandExecute);
        SetDefaultSettings();
    }
    private void SetDefaultSettings()
    {
        Task = new ();
        Tasks = new (Task);
        FirstLevelTree = new (new TaskTreeItem(_Task));
        SelectedTreeViewItem = FirstLevelTree.Item[0];
        SelectTemplate();
    }
}