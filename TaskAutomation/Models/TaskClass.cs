using System.Collections.Generic;
using TaskAutomation.Models.Base;
using TaskAutomation.ViewModels;
using TaskAutomationDB.Entities;

namespace TaskAutomation.Models;

public class TaskClass : ComplexModel
{
    private const string Text = "Комплексный объект";
    private readonly MainWindowViewModel _MainModel;




    #region Площадки 
    public IEnumerable<Area> Areas => DefineTypeObjects<Area>(MainItems);
    #endregion

    #region Объекты 
    public IEnumerable<ObjectInf> Objects => DefineTypeObjects<ObjectInf>(MainItems);
    #endregion

    #region Параметры 
    public IEnumerable<Parameter> Parameters => DefineTypeObjects<Parameter>(SubItems);
    #endregion

    public TaskClass(MainWindowViewModel mainWindowViewModel) : base(Text) { _MainModel = mainWindowViewModel; }
}