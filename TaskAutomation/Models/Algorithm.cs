using System;

namespace TaskAutomation.Models
{
    public class Algorithm:BaseModel
    {
        public string SetPoint { get; set; }
        public string Action { get; set; }

        public Algorithm()
        {
            Action = "";
            SetPoint = "";
        }

        public Algorithm(string action, string setpoint)
        {
            Action = action;
            SetPoint = setpoint;
        }

        public Algorithm(Algorithm algorithm)
        {

        }
    }
}
