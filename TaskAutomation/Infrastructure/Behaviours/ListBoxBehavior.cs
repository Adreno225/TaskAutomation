﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xaml.Behaviors;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TaskAutomation.Models;
using TaskAutomation.ViewModels;

namespace TaskAutomation.Infrastructure.Behaviours
{
    public class ListBoxBehavior: Behavior<ListBox>
    {
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                nameof(SelectedItem),
                typeof(object),
                typeof(ListBoxBehavior),
                new FrameworkPropertyMetadata(null) { BindsTwoWayByDefault = true });

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        protected override void OnAttached() => AssociatedObject.MouseDoubleClick += AssociatedObject_MouseDoubleClick;
        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
                AssociatedObject.MouseDoubleClick -= AssociatedObject_MouseDoubleClick;
        }

        private void AssociatedObject_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selItem = ((ListBox)sender).SelectedItem;
            if (selItem != null)
            {
                var vM = App.Host.Services.GetRequiredService<MainWindowViewModel>();
                var actTreeItem = vM.SelectedTreeViewItem;
                if ((selItem is Parameter)&&((actTreeItem.Object is Area)||(actTreeItem.Object is Models.Task))) 
                {
                    var temp = actTreeItem .Items.Single(x => x is TreeItemMainList);
                    vM.SelectedTreeViewItem = temp.Items.Single(x => x.Object == selItem);
                }
                else
                    vM.SelectedTreeViewItem = actTreeItem.Items.Single(x => x.Object == selItem);
            } 
        }
    }
}
