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
        private Mode _Mode;
        public Mode Mode
        {
            get => _Mode;
            set => Set<Mode>(ref _Mode, value);
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
            Mode = Mode.Both;
            SetPoint = "";
            Type = TypeSignaling.H;
        }

        public Signaling(Mode mode, string setpoint, TypeSignaling type)
        {
            Mode = mode;
            SetPoint = setpoint;
            Type = type;
        }

    }
}
