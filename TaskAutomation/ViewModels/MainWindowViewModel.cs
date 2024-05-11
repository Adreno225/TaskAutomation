using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TaskAutomation.Services;
using TaskAutomation.ViewModels.TreeItems;

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
/// <summary>
/// Главная ViewModel
/// </summary>
public partial class MainWindowViewModel : ObservableObject
{
    private readonly ICreatorTask _ExcelCreator;

    private readonly IQueryCreator _QueryCreator;

    #region Основные данные проекта
    /// <summary>
    /// Основные данные проекта
    /// </summary>
    public IMainData MainDataProject { get; set; }
    #endregion

    #region Репозитории БД
    /// <summary>
    /// Репозитории БД
    /// </summary>
    public IProjectRepositories Repositories { get; }
    #endregion

    #region Сериализатор/десериализатор
    /// <summary>
    ///  Сериализатор/десериализатор
    /// </summary>
    public ISerializer Serializer { get; }
    #endregion

    public bool IsChangedTreeItem { get; set; } = false;

    #region Источник для списка КО
    /// <summary>
    /// Список КО
    /// </summary>
    public ObservableCollection<IComplexObjectTreeItem> Tasks { get; } = new ObservableCollection<IComplexObjectTreeItem>();
    #endregion

    #region Выбранный объект в дереве
    [ObservableProperty]
    private ITreeItem _selectedTreeViewItem;
    #endregion

    #region Тип выбранного объекта в дереве
    [ObservableProperty]
    private TypeSelectedItem _typeSelectedItem = TypeSelectedItem.None;
    #endregion

    /// <summary>
    /// Переключение шаблона представлния элемента
    /// </summary>
    public void SelectTemplate()
    {
        TypeSelectedItem = SelectedTreeViewItem switch
        {
            IComplexObjectTreeItem => TypeSelectedItem.Task,
            IAreaTreeItem => TypeSelectedItem.Area,
            ISubTreeItem => TypeSelectedItem.ParametersArea,
            IObjectTreeItem => TypeSelectedItem.Object,
            IParameterTreeItem => TypeSelectedItem.Parameter,
            _ => TypeSelectedItem.None,
        };
    }

    #region Настройки по умолчанию
    [RelayCommand]
    private void SetDefaultSettings()
    {
        MainDataProject.SetDefaultData(true);
        Initialize();
    }
    #endregion

    private void Initialize()
    {
        if (Tasks.Count == 0)
            Tasks.Add(MainDataProject.ComplexObject);
        else
            Tasks[0] = MainDataProject.ComplexObject;
        SelectedTreeViewItem = Tasks[0];
        SelectTemplate();
    }

    #region Создание задания
    [RelayCommand]
    private void CreateExcel() => _ExcelCreator.Create();
    #endregion

    #region Сохранение данных
    [RelayCommand]
    private void SaveData() => Serializer.SaveData();
    #endregion

    #region Загрузка данных
    [RelayCommand]
    private void LoadData() { Serializer.LoadData(); }
    #endregion

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="mainDataProject">Основные данные проекта</param>
    /// <param name="projectRepositories">Репозитории БД</param>
    /// <param name="creator">Сервис создания задания</param>
    /// <param name="queryCreator">Сервис запросов</param>
    /// <param name="serializer">Сервис сериализации/десериализации</param>
    public MainWindowViewModel(IMainData mainDataProject, IProjectRepositories projectRepositories,
        ICreatorTask creator, IQueryCreator queryCreator, ISerializer serializer)
    {
        Repositories = projectRepositories;
        MainDataProject = mainDataProject;
        _ExcelCreator = creator;
        _QueryCreator = queryCreator;
        Serializer = serializer;
        Initialize(); 
    }
}