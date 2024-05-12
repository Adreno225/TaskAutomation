using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TaskAutomation.ViewModels;
using TaskAutomation.ViewModels.SubClasses;
using TaskAutomation.ViewModels.TreeItems;
using TaskAutomationDB.Entities;
using TaskAutomationInterfaces;

namespace TaskAutomation.Services
{
    /// <summary>
    /// Интерфейс сервиса создания запросов к БД
    /// </summary>
    public interface IQueryCreator
    {
        /// <summary>
        /// Установка параметров объекта/сооружения автоматизации
        /// </summary>
        /// <param name="objectTreeItem">Объект/cооружение автоматизации</param>
        void SetParameters(IObjectTreeItem objectTreeItem);
        /// <summary>
        /// Установка параметров всех объектов/сооружений автоматизации
        /// </summary>
        void SetParametersAllObjects(); 
    }
    /// <summary>
    /// Реализация сервиса создания запросов к БД
    /// </summary>
    public class QueryCreator : IQueryCreator
    {
        private const string MainText = "Изменить параметры всех добавленных в проекте объектов?";
        private const string HeaderText = "Изменение класса автоматизации";
        private readonly IRepository<ParameterClassFunction> _RepositoryParameterClassFunction;
        private readonly IMainData _mainData;

        public QueryCreator(IRepository<ParameterClassFunction> repositoryParameterClassFunction, IMainData mainData)
        {
            _RepositoryParameterClassFunction = repositoryParameterClassFunction;
            _mainData = mainData;
        }

        public void SetParametersAllObjects()
        {
            var complexObj = _mainData.ComplexObject;
            if (complexObj == null) return;
            if (!(GetObjects(complexObj.ListGroup.Items).Any() || complexObj.ListGroup.Items.Where(x => x is AreaTreeItem).Where(x => GetObjects(x.ListGroup.Items).Any()).Any())) return;
            var result = MessageBox.Show(MainText, HeaderText, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            foreach (var treeItem in complexObj.ListGroup.Items)
                if (treeItem is AreaTreeItem areaTreeItem)
                    SetParametersObjects(areaTreeItem);
            SetParametersObjects(complexObj);
        }

        private void SetParametersObjects(ITreeItem treeItem)
        {
            var objectsTreeItem = GetObjects(treeItem.ListGroup.Items);
            foreach (var obj in objectsTreeItem)
                SetParameters(obj);
        }

        private static IEnumerable<ObjectTreeItem> GetObjects(IEnumerable<ITreeItem> treeItems) => treeItems.Where(x => x is ObjectTreeItem).Cast<ObjectTreeItem>();

        public void SetParameters(IObjectTreeItem objectTreeItem)
        {
            var result = _RepositoryParameterClassFunction.Items
                .Where(x => x.Class == _mainData.Class && x.Parameter.ObjectAutomation == objectTreeItem.SelectedTypeObject)
                .Include(x => x.Parameter)
                .Include(x => x.Parameter.ObjectAutomation).Include(x => x.FunctionParameter);
            var dictionary = new Dictionary<Parameter, List<FunctionParameter>>();
            foreach (var item in result)
            {
                if (dictionary.TryGetValue(item.Parameter, out List<FunctionParameter> value))
                    value.Add(item.FunctionParameter);
                else
                    dictionary.Add(item.Parameter, new List<FunctionParameter>() { item.FunctionParameter });
            }
            if (objectTreeItem.SelectedTypeObject is not null)
            {
                objectTreeItem.ListGroup.Items.Clear();
                objectTreeItem.Name = objectTreeItem.SelectedTypeObject.Name;
                foreach (var item in dictionary)
                {
                    var isManualMeasure = item.Value.FirstOrDefault(x => x.Name == "Им") is not null;
                    var isRemoteMeasure = item.Value.FirstOrDefault(x => x.Name == "Ид") is not null;
                    var newParameter = App.Services.GetRequiredService<IParameterTreeItem>();
                    newParameter.Name = item.Key.Name;
                    newParameter.ManualMeasure = App.Services.GetRequiredService<IMeasure>();
                    newParameter.ManualMeasure.IsMeasurable = isManualMeasure;
                    newParameter.RemoteMeasure = App.Services.GetRequiredService<IMeasure>();
                    newParameter.RemoteMeasure.IsMeasurable = isRemoteMeasure;
                    objectTreeItem.ListGroup.Items.Add(newParameter);
                }
            }
        }
    }
}
