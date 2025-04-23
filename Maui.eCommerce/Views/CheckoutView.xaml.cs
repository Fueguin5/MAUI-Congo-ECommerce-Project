using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class CheckoutView : ContentPage
{
    public CheckoutView()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        (BindingContext as CheckoutViewModel)?.RefreshUX();
    }
    private void ConfirmClicked(object sender, EventArgs e)
    {
        (BindingContext as CheckoutViewModel)?.CheckoutCart();
        Shell.Current.GoToAsync("//MainPage");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ShoppingManagement");
    }
}
