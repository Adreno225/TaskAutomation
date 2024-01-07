using System.Windows;
using System.Windows.Controls;
using TaskAutomation.ViewModels;
using TaskAutomation.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace TaskAutomation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var vM = (MainWindowViewModel)((TreeView)sender).DataContext;
            vM.SelectedObject = (e.NewValue is TreeViewItem)
                ? ((MainWindowViewModel)((TreeView)sender).DataContext).Task
                : (BaseModel)((TreeView)sender).SelectedItem;
            vM.SelectTemplate();
        }       
    }
}
