using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class ConfigView : ContentPage
{
	public ConfigView()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        (BindingContext as ConfigViewModel)?.RefreshUX();
    }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as ConfigViewModel)?.UpdateTax();
        Shell.Current.GoToAsync("//MainPage");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        (BindingContext as ConfigViewModel)?.ResetTax();
        Shell.Current.GoToAsync("//MainPage");
    }
}