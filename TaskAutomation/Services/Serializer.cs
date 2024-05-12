using Newtonsoft.Json;
using System.IO;
using TaskAutomation.ViewModels;
using TaskAutomation.ViewModels.TreeItems;

namespace TaskAutomation.Services
{
    /// <summary>
    /// Интерфейс сервиса сериализации/десериализации
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Сохрание данных/сериализация
        /// </summary>
        void SaveData();
        /// <summary>
        /// Загрузка данных/десериализация
        /// </summary>
        void LoadData();
    }

    /// <summary>
    /// Реализация сервиса сериализации/десериализации
    /// </summary>
    public class Serializer : ISerializer
    {
        private const string JsonFilter = "JSON Files (*.json)|*.json";
        private const string MessageSuccesLoad = "Проект успешно загружен";
        private const string HeaderMessageSuccesLoad = "Успешная загрузка";
        private const string MessageSuccesSave = "Проект успешно сохранен!";
        private const string HeaderMessageSuccesSave = "Успешное сохранение";
        private const string MessageErrorSave = "Ошибка при сохранении файла: ";
        private const string HeaderMessageErrorSave = "Ошибка сохранения";
        private const string MessageErrorLoad = "Ошибка при загрузке файла: ";
        private const string HeaderMessageErrorLoad = "Ошибка загрузки";
        private const string MessageErrorLoadJson = "Не удается восстановить данные из файла: ";
        private const string HeaderMessageErrorLoadJson = "Ошибка в восстановлении данных проекта";
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        private readonly IMainData _mainData;
        private readonly IDialogService _dialogService;

        public Serializer(IMainData mainData, IDialogService dialogService)
        {
            _mainData = mainData;
            _dialogService = dialogService;
            _jsonSerializerSettings = new JsonSerializerSettings()
            { 
                TypeNameHandling = TypeNameHandling.Auto,
                //NullValueHandling = NullValueHandling.Ignore,
                //ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
        }

        public void LoadData()
        {
            var json = GetJsonString();
            if (json == null) return;
            var mainData = JsonConvert.DeserializeObject<MainData>(json, _jsonSerializerSettings);
            ProcessData(mainData);
        }

        private void ProcessData(IMainData mainData)
        {
            _mainData.Code = mainData.Code;
            _mainData.Name = mainData.Name;
            _mainData.Object = mainData.Object;
            _mainData.Stage = mainData.Stage;
            _mainData.Class = mainData.Class;
            _mainData.Customer = mainData.Customer;
            _mainData.TypeCO = mainData.TypeCO;
            if (mainData.ComplexObject != null)
            {
                var CO = (IComplexObjectTreeItem)mainData.ComplexObject.Copy();
                ProcessDataCO(_mainData.ComplexObject, CO);
                _dialogService.ShowMessage(MessageSuccesLoad, HeaderMessageSuccesLoad);
            }
            else
                _dialogService.ShowMessage(MessageErrorLoadJson + _dialogService.FilePath, HeaderMessageErrorLoadJson);
        }

        private static void ProcessDataCO(ITreeItem outCO, ITreeItem inCO)
        {
            outCO.Name = inCO.Name;
            outCO.ListGroup.SelectedItem = inCO.ListGroup.SelectedItem;
            outCO.ListGroup.Items.Clear();
            foreach (var item in inCO.ListGroup.Items)
                outCO.ListGroup.Items.Add(item);          
        }

        public void SaveData()
        {
            var json = JsonConvert.SerializeObject(_mainData, _jsonSerializerSettings);
            if (_dialogService.SaveFileDialog(JsonFilter))
            {
                try
                {
                    File.WriteAllText(_dialogService.FilePath, json);
                    _dialogService.ShowMessage(MessageSuccesSave, HeaderMessageSuccesSave);
                }
                catch (IOException ex)
                {
                    _dialogService.ShowMessage(MessageErrorSave + ex.Message, HeaderMessageErrorSave);
                }
                
            }
        }

        private string GetJsonString()
        {
            if (_dialogService.OpenFileDialog(JsonFilter))
            {
                try
                {
                    return File.ReadAllText(_dialogService.FilePath);
                }
                catch (IOException ex)
                {
                    _dialogService.ShowMessage(MessageErrorLoad + ex.Message, HeaderMessageErrorLoad);
                }
            }
            return null;
        }
    }
}
