using Maui.eCommerce.ViewModels;
using Library.eCommerce.Models;

namespace Maui.eCommerce.Views;

public partial class ShoppingManagementView : ContentPage
{
	public ShoppingManagementView()
	{
		InitializeComponent();
		BindingContext = new ShoppingManagementViewModel();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        (BindingContext as ShoppingManagementViewModel)?.RefreshUX();
    }

    private void RemoveFromCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).ReturnItem();
    }

    private void AddToCartClicked(object sender, EventArgs e)
    {
		(BindingContext as ShoppingManagementViewModel).PurchaseItem();
    }

    private void InlineAddClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).PurchaseQuantity();
    }

    private void InlineRemoveClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).ReturnQuantity();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void CheckoutClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Checkout");
    }

    private void InventoryEntryFocused(object sender, FocusEventArgs e)
    {
        if (sender is Entry entry && entry.BindingContext is Item item)
        {
            (BindingContext as ShoppingManagementViewModel).InventoryEntryClicked(item);
        }
    }

    private void ShoppingEntryFocused(object sender, FocusEventArgs e)
    {
        if (sender is Entry entry && entry.BindingContext is CartItem cartitem)
        {
            (BindingContext as ShoppingManagementViewModel).ShoppingEntryClicked(cartitem);
        }
    }
}