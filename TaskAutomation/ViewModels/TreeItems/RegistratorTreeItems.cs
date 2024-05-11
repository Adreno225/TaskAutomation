using Microsoft.Extensions.DependencyInjection;

namespace TaskAutomation.ViewModels.TreeItems
{
    /// <summary>
    /// Класс для регистрации элементов дерева
    /// </summary>
    public static class RegistratorTreeItems
    {
        /// <summary>
        /// Регистрация элементов дерева
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <returns>Коллекция сервисов</returns>
        public static IServiceCollection RegisterTreeItems(this IServiceCollection services)
        {
            services.
                AddTransient<IParameterTreeItem, ParameterTreeItem>().
                AddTransient<IObjectTreeItem, ObjectTreeItem>().
                AddTransient<IAreaTreeItem, AreaTreeItem>().
                AddTransient<IComplexObjectTreeItem, ComplexObjectTreeItem>().
                AddTransient<ISubTreeItem<IParameterTreeItem, IComplexObjectTreeItem>, SubTreeItemCOParameters>().
                AddTransient<ISubTreeItem<IParameterTreeItem, IAreaTreeItem>, SubTreeItemAreaParameters>()
                ;
            return services;
        }
    }
}
