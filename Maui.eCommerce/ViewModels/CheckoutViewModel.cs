using Library.eCommerce.Models;
using Library.eCommerce.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Maui.eCommerce.ViewModels
{
    public class CheckoutViewModel : INotifyPropertyChanged
    {
        private readonly ShoppingCartService _cartSvc = ShoppingCartService.Current;

        public ObservableCollection<CartItem?> ShoppingCart =>
            new ObservableCollection<CartItem?>(_cartSvc.CartItems.Where(i => i?.Quantity > 0));

        private const decimal TaxRate = 0.07m; // 7% tax

        public decimal Subtotal => ShoppingCart.Sum(item => item?.InventoryItem?.Product?.Price * item?.Quantity ?? 0);
        public decimal Tax => Subtotal * TaxRate;
        public decimal Total => Subtotal + Tax;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshUX()
        {
            NotifyPropertyChanged(nameof(ShoppingCart));
            NotifyPropertyChanged(nameof(Subtotal));
            NotifyPropertyChanged(nameof(Tax));
            NotifyPropertyChanged(nameof(Total));
        }
    }
}
