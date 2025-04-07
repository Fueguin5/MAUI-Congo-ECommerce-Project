using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class CheckoutView : ContentPage
{
	public CheckoutView()
	{
		InitializeComponent();
		BindingContext = new CheckoutViewModel();
	}
}