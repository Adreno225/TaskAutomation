using System.Collections.Generic;

namespace TaskAutomation.Models
{
    /// <summary>
    /// Класс методов расширения ObservableCollection
    /// </summary>
    internal static class ExtensionMethodsCollections
    {
        /// <summary>
        /// Создание копии коллекции
        /// </summary>
        /// <typeparam name="TypeCollection">Тип коллекции</typeparam>
        /// <typeparam name="TypeItem">Тип элементов коллекции</typeparam>
        /// <param name="collection">Входная коллекция</param>
        /// <returns>Выходная скопированная коллекция</returns>
        public static TypeCollection Copy<TypeCollection,TypeItem>(this TypeCollection collection)
            where TypeItem : ICopy<TypeItem>
            where TypeCollection: ICollection<TypeItem>, new()
        {
            var resultCollection = new TypeCollection();
            foreach (var item in collection)
                resultCollection.Add(item.Copy());
            return resultCollection;
        }
    }
}
