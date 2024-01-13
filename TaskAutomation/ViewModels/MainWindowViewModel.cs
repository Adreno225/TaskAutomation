using TaskAutomation.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskAutomation.Infrastructure.Commands;
using System.ComponentModel;

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
        #region Задание
        private static Models.Task _Task = new Models.Task();
        public Models.Task Task
        {
            get => _Task;
            set => Set(ref _Task, value);
        }
        #endregion

        #region Источник для списков с заданиями
        private ObservableCollection<Models.Task> _Tasks = new ObservableCollection<Models.Task> { _Task };

        public ObservableCollection<Models.Task> Tasks
        {
            get => _Tasks;
            set => Set(ref _Tasks, value);
        } 
        #endregion

        #region Окно для выбранного комплексного объекта (1 уровень дерева)
        private TaskTemplate _TaskTemplate = new TaskTemplate(_Task, _Task.Areas);

        public TaskTemplate TaskTemplate
        {
            get => _TaskTemplate;
            set => Set(ref _TaskTemplate, value);
        }
        #endregion

        #region Окно для выбранной площадки (2 уровень дерева)
        private AreaTemplate _AreaTemplate = new AreaTemplate();

        public AreaTemplate AreaTemplate
        {
            get => _AreaTemplate;
            set => Set(ref _AreaTemplate, value);
        }
        #endregion

        #region Окно для выбранного объекта (3 уровень дерева)
        private ObjectTemplate _ObjectTemplate = new ObjectTemplate();

        public ObjectTemplate ObjectTemplate
        {
            get => _ObjectTemplate;
            set => Set(ref _ObjectTemplate, value);
        }
        #endregion

        #region Окно для выбранного параметра (4 уровень дерева)
        private ParameterTemplate _ParameterTemplate = new ParameterTemplate();

        public ParameterTemplate ParameterTemplate
        {
            get => _ParameterTemplate;
            set => Set(ref _ParameterTemplate, value);
        }
        #endregion

        #region Выбранный объект в дереве
        private BaseModel _SelectedTreeViewItem;

        public BaseModel SelectedTreeViewItem
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

        #region Выбранный объект 
        private Models.Object _SelectedObject;
        public Models.Object SelectedObject
        {
            get => _SelectedObject;
            set => Set<Models.Object>(ref _SelectedObject, value);
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
                case Models.Task:
                    TaskTemplate.SetTemplate(this);
                    break;
                case Area:
                    AreaTemplate.SetTemplate(this);
                    break;
                case Models.Object:
                    ObjectTemplate.SetTemplate(this);
                    break;
                case Parameter:
                    ParameterTemplate.SetTemplate(this);
                    break;
                default:
                    TypeSelectedItem = TypeSelectedItem.None;
                    break;
            }
        }

        #region Настройки по умолчанию
        public ICommand SetDefaultSettingsCommand { get; }

        private bool CanSetDefaultSettingsCommandExecute(object p) => true;

        private void OnSetDefaultSettingsCommandExecuted(object p) => SetDefaultSettings(); 
        #endregion

        public MainWindowViewModel()
        {
            SetDefaultSettingsCommand = new LambdaCommand(OnSetDefaultSettingsCommandExecuted, CanSetDefaultSettingsCommandExecute);
            SetDefaultSettings();
        }
        private void SetDefaultSettings()
        {
            Task.Name = "Комплексный объект";
            Task.Code = "";
            Task.Object = "";
            Task.Class = Class.Базовый;
            Task.Stage = Stage.РД;
            Task.Areas.Clear();
            SelectTemplate();
        }
    }
}
