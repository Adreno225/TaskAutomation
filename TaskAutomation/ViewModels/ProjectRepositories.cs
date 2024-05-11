using TaskAutomationDB.Entities;
using TaskAutomationInterfaces;

namespace TaskAutomation.ViewModels;
/// <summary>
/// Интерфейс репозиториев проекта
/// </summary>
public interface IProjectRepositories
{
    /// <summary>
    /// Репозиторий классов автоматизации
    /// </summary>
    IRepository<Class> RepositoryClasses { get; }
    /// <summary>
    /// Репозиторий стадий
    /// </summary>
    IRepository<Stage> RepositoryStages { get; }
    /// <summary>
    /// Репозиторий режимов работы
    /// </summary>
    IRepository<Mode> RepositoryModes { get; }
    /// <summary>
    /// Репозиторий Заказчиков
    /// </summary>
    IRepository<Customer> RepositoryCustomers { get; }
    /// <summary>
    /// Репозиторий типов КО
    /// </summary>
    IRepository<TypeCO> RepositoryTypesCO { get; }
    /// <summary>
    /// Репозиторий функций параметров
    /// </summary>
    IRepository<FunctionParameter> RepositoryFunctionsParameter { get; }
    /// <summary>
    /// Репозитоий объектов автоматизации
    /// </summary>
    IRepository<ObjectAutomation> RepositoryObjectsAutomation { get; }
    /// <summary>
    /// Репозиторий параметров
    /// </summary>
    IRepository<Parameter> RepositoryParameters { get; }
}
/// <summary>
/// Реализация репозиториев проекта
/// </summary>
public class ProjectRepositories : IProjectRepositories
{
    #region Перечень классов автоматизации 
    public IRepository<Class> RepositoryClasses { get; }
    #endregion

    #region Перечень стадий 
    public IRepository<Stage> RepositoryStages { get; }
    #endregion

    #region Перечень режимов 
    public IRepository<Mode> RepositoryModes { get; }
    #endregion

    #region Перечень Заказчиков 
    public IRepository<Customer> RepositoryCustomers { get; }
    #endregion

    #region Перечень типов КО 
    public IRepository<TypeCO> RepositoryTypesCO { get; }
    #endregion

    #region Функции параметра
    public IRepository<FunctionParameter> RepositoryFunctionsParameter { get; }
    #endregion

    #region Объекты автоматизации из ЛНД
    public IRepository<ObjectAutomation> RepositoryObjectsAutomation { get; }
    #endregion

    #region Параметры из ЛНД
    public IRepository<Parameter> RepositoryParameters { get; }
    #endregion

    public ProjectRepositories(IRepository<Class> repositoryClasses,
    IRepository<Stage> repositoryStages, IRepository<Mode> repositoryModes,
    IRepository<Customer> repositoryCustomers, IRepository<TypeCO> repositoryTypesCO,
    IRepository<FunctionParameter> repositoryFunctionsParameter,
    IRepository<ObjectAutomation> repositoryObjectsAutomation,
    IRepository<Parameter> repositoryParameters)
    {
        RepositoryClasses = repositoryClasses;
        RepositoryStages = repositoryStages;
        RepositoryModes = repositoryModes;
        RepositoryCustomers = repositoryCustomers;
        RepositoryTypesCO = repositoryTypesCO;
        RepositoryFunctionsParameter = repositoryFunctionsParameter;
        RepositoryObjectsAutomation = repositoryObjectsAutomation;
        RepositoryParameters = repositoryParameters;
    }
}