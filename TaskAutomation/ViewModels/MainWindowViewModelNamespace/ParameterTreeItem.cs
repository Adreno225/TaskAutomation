using TaskAutomation.Models;

namespace TaskAutomation.ViewModels.MainWindowViewModelNamespace;

public class ParameterTreeItem : MainTreeItem
{
    private const string TextSignalings = "Перечень сигнализаций:";
    private const string TextAlgorithms = "Перечень алгоритмов:";

    public TableGroup<Signaling> Signalings { get; }
    public TableGroup<Algorithm> Algorithms { get; }
    public TableOneRow<Parameter> TableParameter { get; }

    public ParameterTreeItem(Parameter parameter) : base(parameter)
    {
        Signalings = new (TextSignalings,parameter.MainItems);
        Algorithms = new (TextAlgorithms, parameter.MainItems2);
        TableParameter = new (parameter);
    }
}