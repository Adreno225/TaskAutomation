using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TaskAutomation.ViewModels;
using TaskAutomation.ViewModels.MainWindowViewModelNamespace;

namespace TaskAutomation.Infrastructure.Behaviours;

public class TreeViewSelectedItem : Behavior<TreeView>
{
    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(
            nameof(SelectedItem),
            typeof(ITreeItem),
            typeof(TreeViewSelectedItem),
            new FrameworkPropertyMetadata(null, OnSelectedItemChanged) { BindsTwoWayByDefault = true });

    public ITreeItem SelectedItem
    {
        get => (ITreeItem)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value); 
    }

    private static TreeView _treeView;
    protected override void OnAttached() 
    {
        AssociatedObject.SelectedItemChanged += OnTreeViewSelectedItemChanged;
        _treeView = AssociatedObject;
    } 

    protected override void OnDetaching()
    {
        if (AssociatedObject != null)
            AssociatedObject.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
    }

    private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) 
    {
        SelectedItem = (ITreeItem)e.NewValue;
        var vM = (MainWindowViewModel)((TreeView)sender).DataContext;
        vM.SelectTemplate();
    }

    private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        object dataItem = e.NewValue; //a FileExplorerItem object
        TreeViewItem tvi = GetTreeViewItem(_treeView, dataItem);
        tvi?.SetValue(TreeViewItem.IsSelectedProperty, true);
    }

    private static TreeViewItem GetTreeViewItem(ItemsControl container, object item)
    {
        if (container != null)
        {
            if (container.DataContext == item)
            {
                return container as TreeViewItem;
            }

            if (container is TreeViewItem && !((TreeViewItem)container).IsExpanded)
            {
                container.SetValue(TreeViewItem.IsExpandedProperty, true);
            }

            container.ApplyTemplate();
            ItemsPresenter itemsPresenter =
                (ItemsPresenter)container.Template.FindName("ItemsHost", container);
            if (itemsPresenter != null)
            {
                itemsPresenter.ApplyTemplate();
            }
            else
            {
                itemsPresenter = FindVisualChild<ItemsPresenter>(container);
                if (itemsPresenter == null)
                {
                    container.UpdateLayout();

                    itemsPresenter = FindVisualChild<ItemsPresenter>(container);
                }
            }

            Panel itemsHostPanel = (Panel)VisualTreeHelper.GetChild(itemsPresenter, 0);

            UIElementCollection children = itemsHostPanel.Children;

            MyVirtualizingStackPanel virtualizingPanel =
                itemsHostPanel as MyVirtualizingStackPanel;

            for (int i = 0, count = container.Items.Count; i < count; i++)
            {
                TreeViewItem subContainer;
                if (virtualizingPanel != null)
                {
                    virtualizingPanel.BringIntoView(i);

                    subContainer =
                        (TreeViewItem)container.ItemContainerGenerator.
                            ContainerFromIndex(i);
                }
                else
                {
                    subContainer =
                        (TreeViewItem)container.ItemContainerGenerator.
                            ContainerFromIndex(i);
                    subContainer.BringIntoView();
                }

                if (subContainer != null)
                {
                    TreeViewItem resultContainer = GetTreeViewItem(subContainer, item);
                    if (resultContainer != null)
                    {
                        return resultContainer;
                    }
                    else
                    {
                        subContainer.IsExpanded = false;
                    }
                }
            }
        }

        return null;
    }

    private static T FindVisualChild<T>(Visual visual) where T : Visual
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
        {
            Visual child = (Visual)VisualTreeHelper.GetChild(visual, i);
            if (child != null)
            {
                if (child is T correctlyTyped)
                {
                    return correctlyTyped;
                }

                T descendent = FindVisualChild<T>(child);
                if (descendent != null)
                {
                    return descendent;
                }
            }
        }

        return null;
    }

    public class MyVirtualizingStackPanel : VirtualizingStackPanel
    {
        /// <summary>
        /// Publically expose BringIndexIntoView.
        /// </summary>
        public void BringIntoView(int index)
        {

            BringIndexIntoView(index);
        }
    }

}