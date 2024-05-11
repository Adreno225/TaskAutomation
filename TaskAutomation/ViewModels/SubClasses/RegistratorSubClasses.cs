using Microsoft.Extensions.DependencyInjection;

namespace TaskAutomation.ViewModels.SubClasses
{
    public static class RegistratorSubClasses
    {
        /// <summary>
        /// Регистрация подклассов ViewModels
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <returns>Коллекция сервисов</returns>
        public static IServiceCollection RegisterSubClasses(this IServiceCollection services)
        {
            services.
                AddTransient<IMeasure, Measure>().
                AddTransient<IProduct, Product>().
                AddTransient<ISignaling, Signaling>().
                AddTransient<IAlgorithm, Algorithm>()
                ;
            return services;
        }
    }
}
