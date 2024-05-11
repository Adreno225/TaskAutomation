using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
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
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        private readonly IMainData _mainData;


        public Serializer(IMainData mainData)
        {
            _mainData = mainData;
            _jsonSerializerSettings = new JsonSerializerSettings()
            { 
                TypeNameHandling = TypeNameHandling.Auto,
                
                //NullValueHandling = NullValueHandling.Ignore,
                //ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
        }

        public void LoadData()
        {
            var json = Load();
            if (json == null) return;
            var mainData = JsonConvert.DeserializeObject<MainData>(json, _jsonSerializerSettings);
            PrisvData(mainData);
        }

        private void PrisvData(IMainData mainData)
        {
            _mainData.Code = mainData.Code;
            _mainData.Name = mainData.Name;
            _mainData.Object = mainData.Object;
            _mainData.Stage = mainData.Stage;
            _mainData.Class = mainData.Class;
            _mainData.Customer = mainData.Customer;
            _mainData.TypeCO = mainData.TypeCO;
            var CO = (IComplexObjectTreeItem)mainData.ComplexObject.Copy();
            PrisvCO(_mainData.ComplexObject, CO);
            //_mainData.ComplexObject = mainData.ComplexObject;
        }

        private static void PrisvCO(ITreeItem outCO, ITreeItem inCO)
        {
            outCO.Name = inCO.Name;
            if (inCO.ListGroup != null)
            {
                outCO.ListGroup.SelectedItem = inCO.ListGroup.SelectedItem;
                outCO.ListGroup.Items.Clear();
            }
            foreach (var item in inCO.ListGroup.Items)
            {
                outCO.ListGroup.Items.Add(item);
            }             
        }

        public void SaveData()
        {
            var json = JsonConvert.SerializeObject(_mainData, _jsonSerializerSettings);
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON Files (*.json)|*.json"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                var filePath = saveFileDialog.FileName;
                File.WriteAllText(filePath, json);
            }
        }

        private static string Load()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var json = File.ReadAllText(openFileDialog.FileName);
                    return json;
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Ошибка при загрузке файла: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return null;
        }
    }
}
