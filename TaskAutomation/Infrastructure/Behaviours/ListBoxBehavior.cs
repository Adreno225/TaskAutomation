using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var selItem = (BaseModel)((ListBox)sender).SelectedItem;
            if (selItem != null)
            {
                var vM = App.Host.Services.GetRequiredService<MainWindowViewModel>();
                vM.SelectedTreeViewItem = selItem;
            } 
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SelectedItem = e.NewValue;
            var vM = (MainWindowViewModel)((TreeView)sender).DataContext;
            vM.SelectTemplate();
        }
    }
}
