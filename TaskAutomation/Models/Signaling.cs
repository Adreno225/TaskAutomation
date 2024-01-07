using System;

namespace TaskAutomation.Models
{
    public class Signaling
    {
        public enum TypeSignaling
        {
            LL,
            L,
            H,
            HH
        }
        public Mode Mode { get; set; }
        public string SetPoint { get; set; }
        public TypeSignaling Type { get; set; }

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
