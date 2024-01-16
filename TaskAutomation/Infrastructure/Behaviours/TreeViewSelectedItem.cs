using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using TaskAutomation.Models;
using TaskAutomation.ViewModels;

namespace TaskAutomation.Infrastructure.Behaviours
{
    public class TreeViewSelectedItem : Behavior<TreeView>
    {
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                nameof(SelectedItem),
                typeof(BaseModel),
                typeof(TreeViewSelectedItem),
                new FrameworkPropertyMetadata(null) { BindsTwoWayByDefault = true });

        public BaseModel SelectedItem
        {
            get => (BaseModel)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value); 
        }

        protected override void OnAttached() => AssociatedObject.SelectedItemChanged += OnTreeViewSelectedItemChanged;

        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
                AssociatedObject.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) 
        {
            SelectedItem = (BaseModel)e.NewValue;
            var vM = (MainWindowViewModel)((TreeView)sender).DataContext;
            vM.SelectTemplate();
        } 

        //private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        //{
        //    //find corresponding item in the TreeView and select it
        //    object dataItem = e.NewValue; //a FileExplorerItem object
        //    TreeViewItem tvi = GetTreeViewItem(_treeView, dataItem);
        //    if (tvi != null)
        //        tvi.SetValue(TreeViewItem.IsSelectedProperty, true);
        //}

        //private static TreeViewItem GetTreeViewItem(ItemsControl container, object item)
        //{
        //    /* Refer to http://msdn.microsoft.com/en-us/library/ff407130(v=vs.110).aspx 
        //     * for implementation details of this method */
        //}

        //private static T FindVisualChild<T>(Visual visual) where T : Visual
        //{
        //    /* Refer to http://msdn.microsoft.com/en-us/library/ff407130(v=vs.110).aspx 
        //     * for implementation details of this method */
        //}

    }
}
