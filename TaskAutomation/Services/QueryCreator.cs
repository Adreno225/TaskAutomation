using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TaskAutomation.Models;
using TaskAutomation.ViewModels;
using TaskAutomation.ViewModels.MainWindowViewModelNamespace;
using TaskAutomationDB.Entities;
using TaskAutomationInterfaces;

namespace TaskAutomation.Services
{
    public interface IQueryCreator
    {
        void SetParameters(ObjectTreeItem objectTreeItem);
        void SetParametersAllObjects();
        MainWindowViewModel MainWindowViewModel { get; set; }
    }

    public class QueryCreator : IQueryCreator
    {
        private const string MainText = "Изменить параметры всех добавленных в проекте объектов?";
        private const string HeaderText = "Изменение класса автоматизации";
        private readonly IRepository<ParameterClassFunction> _RepositoryParameterClassFunction;
        public MainWindowViewModel MainWindowViewModel { get; set; }

        public QueryCreator(IRepository<ParameterClassFunction> repositoryParameterClassFunction)
        {
            _RepositoryParameterClassFunction = repositoryParameterClassFunction;
        }

        public void SetParametersAllObjects()
        {
            var tree = MainWindowViewModel.FirstLevelTree;
            if (tree == null) return;
            if (!(GetObjects(tree.Item[0].Items).Any() || tree.Item[0].Items.Where(x => x is AreaTreeItem).Where(x => GetObjects(x.Items).Any()).Any())) return;
            var result = MessageBox.Show(MainText, HeaderText, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            foreach (var taskTreeItem in tree.Item)
            {
                foreach (var treeItem in taskTreeItem.Items)
                    if (treeItem is AreaTreeItem areaTreeItem)
                        SetParametersObjects(areaTreeItem);
                SetParametersObjects(taskTreeItem);
            }
        }

        private void SetParametersObjects(ITreeItem treeItem)
        {
            var objectsTreeItem = GetObjects(treeItem.Items);
            foreach (var obj in objectsTreeItem)
                SetParameters(obj);
        }

        private static IEnumerable<ObjectTreeItem> GetObjects(IEnumerable<ITreeItem> treeItems) => treeItems.Where(x => x is ObjectTreeItem).Cast<ObjectTreeItem>();

        public void SetParameters(ObjectTreeItem objectTreeItem)
        {
            var result = _RepositoryParameterClassFunction.Items
                .Where(x => x.Class == MainWindowViewModel.Task.Class && x.Parameter.ObjectAutomation == objectTreeItem.SelectedTypeObject)
                .Include(x => x.Parameter)
                .Include(x => x.Parameter.ObjectAutomation).Include(x => x.FunctionParameter);
            var dictionary = new Dictionary<TaskAutomationDB.Entities.Parameter, List<FunctionParameter>>();
            foreach (var item in result)
            {
                if (dictionary.TryGetValue(item.Parameter, out List<FunctionParameter> value))
                    value.Add(item.FunctionParameter);
                else
                    dictionary.Add(item.Parameter, new List<FunctionParameter>() { item.FunctionParameter });
            }
            var parameters = ((ObjectInf)objectTreeItem.Object).MainItems;
            if (objectTreeItem.SelectedTypeObject is not null)
            {
                parameters.Clear();
                objectTreeItem.Items.Clear();
                objectTreeItem.Name = objectTreeItem.SelectedTypeObject.Name;
                foreach (var item in dictionary)
                {
                    var isManualMeasure = item.Value.FirstOrDefault(x => x.Name == "Им") is not null;
                    var isRemoteMeasure = item.Value.FirstOrDefault(x => x.Name == "Ид") is not null;
                    var newParameter = new Models.Parameter() { Name = item.Key.Name, IsManualMeasure = isManualMeasure, IsRemoteMeasure = isRemoteMeasure};
                    parameters.Add(newParameter);
                    objectTreeItem.Items.Add(new ParameterTreeItem(newParameter));
                }
            }
        }
    }
}
