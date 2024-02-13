using System.Collections.Generic;
using TaskAutomation.Models.Base;

namespace TaskAutomation.Models;

public class Area: ComplexModel
{
    private const string Text = "Площадка";
    #region Объекты 
    public IEnumerable<ObjectInf> Objects => DefineTypeObjects<ObjectInf>(MainItems);
    #endregion

    #region Параметры 
    public IEnumerable<Parameter> Parameters => DefineTypeObjects<Parameter>(SubItems);
    #endregion

    public Area():base(Text) { }
}