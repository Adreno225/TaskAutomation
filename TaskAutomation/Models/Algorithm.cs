using System;

namespace TaskAutomation.Models
{
    public class Algorithm:BaseModel
    {
        #region Уставка 
        private string _SetPoint;
        public string SetPoint
        {
            get => _SetPoint;
            set => Set<string>(ref _SetPoint, value);
        }
        #endregion

        #region Действия 
        private string _Action;
        public string Action
        {
            get => _Action;
            set => Set<string>(ref _Action, value);
        }
        #endregion

    }
}
