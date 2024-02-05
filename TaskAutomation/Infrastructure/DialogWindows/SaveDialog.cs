using Microsoft.Win32;
using System;
using System.Windows;

namespace TaskAutomation.Infrastructure.DialogWindows;

public class SaveDialog:BaseDialog
{
    const string MessageFail = "Процесс создания файла прерван!";
    const string HeaderFail = "Отмена процесса создания";
    const string Filter = "Excel Worksheets|*.xlsx";

    public SaveDialog()
    {
        _FileDialog = new SaveFileDialog();  
    }
    public void Save(Action<string> action, string messageFail = MessageFail, string filter = Filter, string initialDirectory = null)
    {
        _FileDialog.Filter = filter;
        _FileDialog.InitialDirectory = initialDirectory == null ? Environment.CurrentDirectory : initialDirectory;
        if (_FileDialog.ShowDialog() == true)
            action(_FileDialog.FileName);
        else
            MessageBox.Show(messageFail, HeaderFail, MessageBoxButton.OK, MessageBoxImage.Stop);
    }
}