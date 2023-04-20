using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace calcAppT37_REST.ViewModels;

public class ExpressionHistoryViewModel: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private static List<string> _expressions = new List<string>();

    
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
        //AddExpression("Testing");
    }

    public void AddExpression(string _expression)
    {
        Expressions.Add(_expression);
        OnPropertyChanged(nameof(Expressions));
    }





    public void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
