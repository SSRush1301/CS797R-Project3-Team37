using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace calcAppT37_REST.ViewModels;

public class ExpressionHistoryViewModel: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private List<string> _expressions;

    public List<string> Expressions
    {
        get => _expressions;
        set
        {
            if (_expressions != value)
            {
                _expressions = value;
                OnPropertyChanged();
            }
        }
    }

    public ExpressionHistoryViewModel()
    {
        Expressions = new List<string>();
        AddExpression("Testing");
    }

    public void AddExpression(string _expression)
    {
        Expressions.Add(_expression);
    }
   


    

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
