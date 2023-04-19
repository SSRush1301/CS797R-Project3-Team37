using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace calcAppT37_REST.ViewModels;

public class ExpressionHistoryViewModel: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public ExpressionHistoryViewModel()
    {
        ExpressionHistory = new ObservableCollection<string>();
    }

    ObservableCollection<string> _expressionHistory;
    public ObservableCollection<string> ExpressionHistory
    {
        get => _expressionHistory;
        set
        {
            _expressionHistory = value;
            OnPropertyChanged(nameof(ExpressionHistory));
        }
    }

    public void AddExpression(string newExpression)
    {
        if (string.IsNullOrEmpty(newExpression))
            return;
        ExpressionHistory.Add(newExpression);
    }




    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
