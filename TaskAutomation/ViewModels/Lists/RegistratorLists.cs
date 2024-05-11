using Microsoft.Extensions.DependencyInjection;
using TaskAutomation.ViewModels.SubClasses;
using TaskAutomation.ViewModels.TreeItems;

namespace TaskAutomation.ViewModels.Lists
{
    public static class RegistratorLists
    {
        /// <summary>
        /// Регистрация списков/таблиц
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <returns>Коллекция сервисов</returns>
        public static IServiceCollection RegisterLists(this IServiceCollection services)
        {
            services.
                AddTransient<ITableGroup<ISignaling>, TableSignalings>().
                AddTransient<ITableGroup<IAlgorithm>, TableAlgorythmes>().
                AddTransient<IListGroup<IParameterTreeItem>, LisGroupParameters>().
                AddTransient<IListGroup<IAreaTreeItem, IObjectTreeItem>, ListGroupAreaObjects>().
                AddTransient<IListGroup<IObjectTreeItem>, ListGroupObjects>()
                ;
            return services;
        }
    }
}
