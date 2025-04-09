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

        public string Subtotal => $"Subtotal: {ShoppingCart.Sum(item => item?.InventoryItem?.Product?.Price * item?.Quantity ?? 0):C2}";
        public string Tax => $"Tax: {ShoppingCart.Sum(item => item?.InventoryItem?.Product?.Price * item?.Quantity ?? 0) * TaxRate:C2}";
        public string Total => $"Total: {ShoppingCart.Sum(item => item?.InventoryItem?.Product?.Price * item?.Quantity ?? 0) * (1 + TaxRate):C2}";

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CheckoutCart()
        {
            _cartSvc.CheckoutCart();
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
