using Microsoft.Extensions.DependencyInjection;
using TaskAutomationDB.Entities;
using TaskAutomationInterfaces;

namespace TaskAutomationDB;

public static class RegistratorRepository
{
    /// <summary>
    /// Рестрация репозиториев БД
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <returns>Коллекция сервисов</returns>
    public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) => services
        .AddTransient<IRepository<Class>, DbRepository<Class>>()
        .AddTransient<IRepository<Stage>, DbRepository<Stage>>()
        .AddTransient<IRepository<Mode>, DbRepository<Mode>>()
        .AddTransient<IRepository<Customer>, DbRepository<Customer>>()
        .AddTransient<IRepository<TypeCO>, DbRepository<TypeCO>>()
        .AddTransient<IRepository<FunctionParameter>, DbRepository<FunctionParameter>>()
        .AddTransient<IRepository<ObjectAutomation>, DbRepository<ObjectAutomation>>()
        .AddTransient<IRepository<Parameter>, DbRepository<Parameter>>()
        .AddTransient<IRepository<ParameterClassFunction>, DbRepository<ParameterClassFunction>>()
    ;
}