using TaskAutomation.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskAutomation.Infrastructure.Commands;
using System.ComponentModel;
using TaskAutomation.Services;
using TaskAutomationInterfaces;
using TaskAutomationDB.Entities;
using System.Linq;
using Class = TaskAutomationDB.Entities.Class;

namespace TaskAutomation.ViewModels
{
    public enum TypeSelectedItem
    {
        Task,
        Area,
        Object,
        Parameter,
        ParametersArea,
        None
    }
    public class MainWindowViewModel : Base.ViewModel
    { 
        private readonly ExcelCreator _ExcelCreator;

        #region Задание
        private static Models.Task _Task = new();
        public Models.Task Task
        {
            get => _Task;
            set => Set(ref _Task, value);
        }
        #endregion

        #region Источник для списков с заданиями
        private ObservableCollection<Models.Task> _Tasks = new() { _Task };

        public ObservableCollection<Models.Task> Tasks
        {
            get => _Tasks;
            set => Set(ref _Tasks, value);
        } 
        #endregion

        #region Окно для выбранного комплексного объекта (1 уровень дерева)
        private TaskTemplate _TaskTemplate;

        public TaskTemplate TaskTemplate
        {
            get => _TaskTemplate;
            set => Set(ref _TaskTemplate, value);
        }
        #endregion

        #region Окно для выбранной площадки (2 уровень дерева)
        private AreaTemplate _AreaTemplate = new();

        public AreaTemplate AreaTemplate
        {
            get => _AreaTemplate;
            set => Set(ref _AreaTemplate, value);
        }
        #endregion

        #region Окно для выбранного объекта (3 уровень дерева)
        private ObjectTemplate _ObjectTemplate = new();

        public ObjectTemplate ObjectTemplate
        {
            get => _ObjectTemplate;
            set => Set(ref _ObjectTemplate, value);
        }
        #endregion

        #region Окно для выбранных параметров площадки (3 уровень дерева) 
        private ParametersAreaTemplate _ParametersAreaTemplate = new();
        public ParametersAreaTemplate ParametersAreaTemplate
        {
            get => _ParametersAreaTemplate;
            set => Set<ParametersAreaTemplate>(ref _ParametersAreaTemplate, value);
        }
        #endregion

        #region Окно для выбранного параметра (4 уровень дерева)
        private ParameterTemplate _ParameterTemplate = new();

        public ParameterTemplate ParameterTemplate
        {
            get => _ParameterTemplate;
            set => Set(ref _ParameterTemplate, value);
        }
        #endregion

        #region Перечень классов автоматизации 
        private readonly IRepository<TaskAutomationDB.Entities.Class> _RepositoryClasses;
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
        private readonly IRepository<TaskAutomationDB.Entities.Mode> _RepositoryModes;
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
        private ObservableCollection<TreeItem> _FirstLevelTree = new() { new TreeItemTask(_Task)};
        public ObservableCollection<TreeItem> FirstLevelTree
        {
            get => _FirstLevelTree;
            set => Set<ObservableCollection<TreeItem>>(ref _FirstLevelTree, value);
        }
        #endregion


        #region Выбранный объект в дереве
        private TreeItem _SelectedTreeViewItem;

        public TreeItem SelectedTreeViewItem
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

        #region Выбранная площадка 
        private Area _SelectedArea;
        public Area SelectedArea
        {
            get => _SelectedArea;
            set => Set<Area>(ref _SelectedArea, value);
        }
        #endregion

        #region Выбранный параметр 
        private Parameter _SelectedParameter;
        public Parameter SelectedParameter
        {
            get => _SelectedParameter;
            set => Set<Parameter>(ref _SelectedParameter, value);
        }
        #endregion

        #region Выбранная сигнализация 
        private Signaling _SelectedSignaling;
        public Signaling SelectedSignaling
        {
            get => _SelectedSignaling;
            set => Set<Signaling>(ref _SelectedSignaling, value);
        }
        #endregion

        #region Выбранный алгоритм 
        private Algorithm _SelectedAlgorithm;
        public Algorithm SelectedAlgorithm
        {
            get => _SelectedAlgorithm;
            set => Set<Algorithm>(ref _SelectedAlgorithm, value);
        }
        #endregion

        public void SelectTemplate()
        {
            switch (SelectedTreeViewItem)
            {
                case TreeItemTask:
                    TaskTemplate.SetTemplate(this);
                    break;
                case TreeItemArea:
                    AreaTemplate.SetTemplate(this);
                    break;
                case TreeItemMainList:
                    ParametersAreaTemplate.SetTemplate(this);
                    break;
                case TreeItemObject:
                    ObjectTemplate.SetTemplate(this);
                    break;
                case TreeItemParameter:
                    ParameterTemplate.SetTemplate(this);
                    break;
                default:
                    TypeSelectedItem = TypeSelectedItem.None;
                    break;
            }
        }

        #region Command AddAreaCommand - Добавление площадки

        /// <summary>Добавление площадки</summary>

        public ICommand AddAreaCommand { get; }

        /// <summary>Проверка возможности выполнения - Добавление площадки</summary>
        private bool CanAddAreaCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Добавление площадки</summary>
        private void OnAddAreaCommandExecuted(object p)
        {
            Task.Areas.Add(new Area());
        }
        #endregion

        #region Command AddObjectCommand - Добавление объекта

        /// <summary>Добавление объекта</summary>

        public ICommand AddObjectCommand { get; }

        /// <summary>Проверка возможности выполнения - Добавление объекта</summary>
        private bool CanAddObjectCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Добавление объекта</summary>
        private void OnAddObjectCommandExecuted(object p)
        {
            Task.Objects.Add(new Object());
        }
        #endregion

        #region Command AddParameterCommand - Добавление параметра

        /// <summary>Добавление параметра</summary>

        public ICommand AddParameterCommand { get; }

        /// <summary>Проверка возможности выполнения - Добавление параметра</summary>
        private bool CanAddParameterCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Добавление параметра</summary>
        private void OnAddParameterCommandExecuted(object p)
        {
            Task.Parameters.Add(new Parameter());
        }
        #endregion

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

        public MainWindowViewModel(ExcelCreator creator,
            IRepository<TaskAutomationDB.Entities.Class> repositoryClasses,
            IRepository<TaskAutomationDB.Entities.Stage> repositoryStages, IRepository<TaskAutomationDB.Entities.Mode> repositoryModes,
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
            AddAreaCommand = new LambdaCommand(OnAddAreaCommandExecuted, CanAddAreaCommandExecute);
            AddObjectCommand = new LambdaCommand(OnAddObjectCommandExecuted, CanAddObjectCommandExecute);
            AddParameterCommand = new LambdaCommand(OnAddParameterCommandExecuted, CanAddParameterCommandExecute);
            TaskTemplate = new TaskTemplate(_FirstLevelTree[0], _Task.Areas);
            SetDefaultSettings();
        }
        private void SetDefaultSettings()
        {
            Task.Name = "Комплексный объект";
            Task.Code = "";
            Task.Object = "";
            Task.Class = "";
            Task.Stage = "";
            Task.Areas.Clear();
            Task.Parameters.Clear();
            Task.Objects.Clear();
            FirstLevelTree[0] = new TreeItemTask(Task);
            SelectedTreeViewItem = FirstLevelTree[0];
            FirstLevelTree[0].Update();
            SelectTemplate();
            //FirstLevelTree[0].Update();
        }
    }
}
