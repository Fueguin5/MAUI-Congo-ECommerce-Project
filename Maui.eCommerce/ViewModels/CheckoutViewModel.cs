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

        public decimal SubtotalAmount => Math.Round(ShoppingCart.Sum(item => item?.InventoryItem?.Product?.Price * item?.Quantity ?? 0), 2);
        public decimal TaxAmount => Math.Round(SubtotalAmount * _cartSvc.TaxRate / 100, 2);
        public string Subtotal => $"Subtotal: {SubtotalAmount:C2}";
        public string Tax => $"Tax: {TaxAmount:C2}";
        public string Total => $"Total: {(SubtotalAmount + TaxAmount):C2}";



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
