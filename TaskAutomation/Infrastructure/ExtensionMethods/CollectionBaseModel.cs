using System.Collections.ObjectModel;

namespace TaskAutomation.Infrastructure.ExtensionMethods
{
    public static class CollectionBaseModel
    {
        public static void AddItem<T>(this ObservableCollection<T> baseModels) where T : class, new()
        { 
            baseModels.Add(new T());
        }
    }
}
