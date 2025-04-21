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
                    NotifyPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        private CartItem? _selectedCartItem;
        public CartItem? SelectedCartItem
        {
            get => _selectedCartItem;
            set
            {
                if (_selectedCartItem != value)
                {
                    _selectedCartItem = value;
                    NotifyPropertyChanged(nameof(SelectedCartItem));
                }
            }
        }

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

        public void ReturnQuantity()
        {
            if (SelectedCartItem != null)
            {
                var shouldRefresh = SelectedCartItem.InventoryItem.RemoveQuantity >= 1;
                CartItem? updatedItem = null;
                var tempQuantity = SelectedCartItem.Quantity;
                for (int i = 0; i < SelectedCartItem.InventoryItem.RemoveQuantity && i < tempQuantity; i++)
                {
                    CartItem? temp = _cartSvc.ReturnItem(SelectedCartItem.InventoryItem);

                    if (temp != null)
                    {
                        updatedItem = temp;
                    }
                }

                SelectedCartItem.InventoryItem.RemoveQuantity = null;

                if (updatedItem != null && shouldRefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }
            }
        }

        public void InventoryEntryClicked(Item item)
        {
            SelectedItem = item;
        }

        public void ShoppingEntryClicked(CartItem cartitem)
        {
            SelectedCartItem = cartitem;
        }
    }
}