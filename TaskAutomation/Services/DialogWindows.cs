using Microsoft.Win32;
using System.Windows;

namespace TaskAutomation.Services
{
    /// <summary>
    /// Интерфейс сервиса диалоговых окон
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Вывод сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="header">Заголовок</param>
        void ShowMessage(string message, string header);
        /// <summary>
        /// Путь к файлу
        /// </summary>
        string FilePath { get; }
        /// <summary>
        /// Открыть файл
        /// </summary>
        /// <param name="filter">Фильтр открываемых файлов</param>
        /// <returns>Выбран ли файл для открытия</returns>
        bool OpenFileDialog(string filter);
        /// <summary>
        /// Сохранить файл
        /// </summary>
        /// <param name="filter">Фильтр сохраняемых файлов</param>
        /// <returns>Выбран ли файл для сохранения</returns>
        bool SaveFileDialog(string filter);
    }
    /// <summary>
    /// Реализация сервиса диалоговых окон
    /// </summary>
    internal class DialogWindows:IDialogService
    {
        private const string MessageFail = "Процесс создания файла прерван!";
        private const string HeaderFail = "Отмена процесса создания";


        public string FilePath { get; private set; }

        public bool SaveFileDialog(string filter)
        { 
            return SaveLoadFileDialog(new SaveFileDialog(), filter);
        }

        public bool OpenFileDialog(string filter)
        {
            return SaveLoadFileDialog(new OpenFileDialog(), filter);
        }
        /// <summary>
        /// Универсальный метод открытия/сохранения файла
        /// </summary>
        /// <param name="fileDialog">Диалоговый объект</param>
        /// <param name="filter">Фильтр сохранения/открытия</param>
        /// <returns></returns>
        private bool SaveLoadFileDialog(FileDialog fileDialog, string filter)
        {
            fileDialog.Filter = filter;
            if (fileDialog.ShowDialog() == true)
            {
                FilePath=fileDialog.FileName;
                return true;
            }
            return false;
        }

        public void ShowMessage(string message, string header)
        {
            MessageBox.Show(message, header, MessageBoxButton.OK);
        }
    }
}
