﻿using TaskAutomation.Models;

namespace TaskAutomation.ViewModels
{
    public class ParametersAreaTemplate : BaseTemplate
    {
        #region Cписок параметров        
        private ListGroup<Models.Parameter> _ListParameters;
        public ListGroup<Models.Parameter> ListParameters
        {
            get => _ListParameters;
            set => Set(ref _ListParameters, value);
        }
        #endregion
        public override void SetTemplate(MainWindowViewModel vM)
        {
            SelectedItem = vM.SelectedTreeViewItem;
            ListParameters = new ListGroup<Parameter>(((Models.Object)SelectedItem).Parameters, SelectedItem);
            vM.TypeSelectedItem = TypeSelectedItem.ParametersArea;
        }
    }
}
