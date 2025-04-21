using Library.eCommerce.Models;
using Library.eCommerce.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    public class ShoppingManagementViewModel : INotifyPropertyChanged
    {
        private ProductServiceProxy _invSvc = ProductServiceProxy.Current;
        private ShoppingCartService _cartSvc = ShoppingCartService.Current;
        
        private Item? _selectedItem;
        public Item? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    NotifyPropertyChanged(nameof(SelectedItem));  // Notify UI about the change
                }
            }
        }
        public CartItem? SelectedCartItem { get; set; }

        public ObservableCollection<Item?> Inventory
        {
            get
            {
                return new ObservableCollection<Item?>(_invSvc.Products
                    .Where(i => i?.Quantity > 0)
                    );
            }
        }

        public ObservableCollection<CartItem?> ShoppingCart
        {
            get
            {
                return new ObservableCollection<CartItem?>(_cartSvc.CartItems
                    .Where(i => i?.Quantity > 0)
                    );
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshUX()
        {
            NotifyPropertyChanged(nameof(Inventory));
            NotifyPropertyChanged(nameof(ShoppingCart));
        }

        public void PurchaseItem()
        {
            if (SelectedItem != null)
            {
                var shouldRefresh = SelectedItem.Quantity >= 1;
                var updatedItem = _cartSvc.AddOrUpdate(SelectedItem);

                if(updatedItem != null && shouldRefresh) {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }

            }
        }

        public void ReturnItem()
        {
            if (SelectedCartItem != null) 
            {
                var shouldRefresh = SelectedCartItem.Quantity >= 1;
                var updatedItem = _cartSvc.ReturnItem(SelectedCartItem.InventoryItem);

                if (updatedItem != null && shouldRefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }
            }
        }

        public void PurchaseQuantity()
        {
            if (SelectedItem != null)
            {
                var shouldRefresh = SelectedItem.AddQuantity >= 1;
                CartItem? updatedItem = null;
                var tempQuantity = SelectedItem.Quantity;
                for (int i = 0; i < SelectedItem.AddQuantity && i < tempQuantity; i++)
                {
                    CartItem? temp = _cartSvc.AddOrUpdate(SelectedItem);

                    if (temp != null)
                    {
                        updatedItem = temp;
                    }
                }

                SelectedItem.AddQuantity = null;

                if (updatedItem != null && shouldRefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }
            }
        }

        public void EntryClicked(Item item)
        {
            SelectedItem = item;
        }
    }
}
