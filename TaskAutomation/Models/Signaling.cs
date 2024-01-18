using System;

namespace TaskAutomation.Models
{
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
        private string _Mode;
        public string Mode
        {
            get => _Mode;
            set => Set<string>(ref _Mode, value);
        }
        #endregion

        #region Уставка 
        private string _SetPoint;
        public string SetPoint
        {
            get => _SetPoint;
            set => Set<string>(ref _SetPoint, value);
        }
        #endregion

        #region Тип 
        private TypeSignaling _Type;
        public TypeSignaling Type
        {
            get => _Type;
            set => Set<TypeSignaling>(ref _Type, value);
        }
        #endregion

        public Signaling()
        {
            Mode = "";
            SetPoint = "";
            Type = TypeSignaling.H;
        }

    }
}
