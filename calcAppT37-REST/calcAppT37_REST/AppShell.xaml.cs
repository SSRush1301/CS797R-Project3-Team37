using calcAppT37_REST.Color_Themes;
namespace calcAppT37_REST;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}

    void OnDark(object sender, EventArgs e)
    {
        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            mergedDictionaries.Clear();

            mergedDictionaries.Add(new DarkTheme());

        }
    }

    void OnLight(object sender, EventArgs e)
    {
        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            mergedDictionaries.Clear();

            mergedDictionaries.Add(new LightTheme());

        }
    }

    void OnRed(object sender, EventArgs e)
    {
        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            mergedDictionaries.Clear();

            mergedDictionaries.Add(new RedTheme());

        }
    }
    void OnPink(object sender, EventArgs e)
    {
        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            mergedDictionaries.Clear();

            mergedDictionaries.Add(new PinkTheme());

        }
    }
}
