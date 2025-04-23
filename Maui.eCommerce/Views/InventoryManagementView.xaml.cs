using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class InventoryManagementView : ContentPage
{
	public InventoryManagementView()
	{
		InitializeComponent();
		BindingContext = new InventoryManagementViewModel();
	}

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.RefreshProductList();
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.RefreshProductList();
    }

    private void IdClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.SortById();
    }

    private void NameClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.SortByName();
    }

    private void PriceClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.SortByPrice();
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Product");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        if ((BindingContext as InventoryManagementViewModel)?.SelectedProduct != null)
        {
            var productId = (BindingContext as InventoryManagementViewModel)?.SelectedProduct?.Id;
            Shell.Current.GoToAsync($"//Product?productId={productId}");
        }
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.Delete();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }
}