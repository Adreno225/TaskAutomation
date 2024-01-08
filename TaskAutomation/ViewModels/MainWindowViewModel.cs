using TaskAutomation.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace TaskAutomation.ViewModels
{
    public enum TypeSelectedItem
    {
        Task,
        Area,
        Object,
        Parameter,
        None
    }
    public class MainWindowViewModel: Base.ViewModel
    {
        #region Задание
        private static Models.Task _Task = new Models.Task
        {
            Code = "",
            Object = "",
            Name = "Комплексный объект",
            Class = Class.Базовый,
            Stage = Stage.РД,
            Areas = new ObservableCollection<Area>()
        };
        public Models.Task Task
        {
            get => _Task;
            set => Set(ref _Task, value);
        } 
        #endregion

        private ObservableCollection<Models.Task> _MainTable = new ObservableCollection<Models.Task> { _Task};

        public ObservableCollection<Models.Task> MainTable
        {
            get => _MainTable;
            set => Set(ref _MainTable, value);
        }

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
        private BaseModel _SelectedObject;

        public BaseModel SelectedObject
        {
            get => _SelectedObject;
            set => Set(ref _SelectedObject, value);
        }
        #endregion

        private TypeSelectedItem _TypeSelectedItem = TypeSelectedItem.None;
        public TypeSelectedItem TypeSelectedItem
        {
            get => _TypeSelectedItem;
            set => Set(ref _TypeSelectedItem, value);
        }

        public void SelectTemplate()
        {
            switch (SelectedObject)
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


        public MainWindowViewModel()
        {

        }
    }
}
