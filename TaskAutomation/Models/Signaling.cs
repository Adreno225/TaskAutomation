namespace TaskAutomation.Models;

public enum TypeSignaling
{
    LL,
    L,
    H,
    HH,
    Other
}
public class Signaling:BaseModel
{

    #region Режим 
    private string _Mode ="";
    public string Mode
    {
        get => _Mode;
        set => Set(ref _Mode, value);
    }
    #endregion

    #region Уставка 
    private string _SetPoint="";
    public string SetPoint
    {
        get => _SetPoint;
        set => Set(ref _SetPoint, value);
    }
    #endregion

    #region Тип 
    private TypeSignaling _Type= TypeSignaling.H;
    public TypeSignaling Type
    {
        get => _Type;
        set => Set(ref _Type, value);
    }
    #endregion

    public Signaling(): base("") { }
}