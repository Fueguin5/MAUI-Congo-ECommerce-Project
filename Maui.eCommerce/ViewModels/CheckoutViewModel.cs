using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Spring2025_Samples.Models;
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
    public class CheckoutViewModel
    {
        private ShoppingCartService _cartSvc = ShoppingCartService.Current;
        public ObservableCollection<Item?> ShoppingCart
        {
            get
            {
                return new ObservableCollection<Item?>(_cartSvc.CartItems
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
            NotifyPropertyChanged(nameof(ShoppingCart));
        }
    }
}
