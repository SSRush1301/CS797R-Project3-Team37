using calcAppT37_REST.ViewModels;

namespace calcAppT37_REST;

public partial class ExpressionHistoryPage : ContentPage
{
	public ExpressionHistoryPage(ExpressionHistoryViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}