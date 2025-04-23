using Library.eCommerce.Models;
using Library.eCommerce.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Maui.eCommerce.ViewModels
{
    public class ConfigViewModel : INotifyPropertyChanged
    {
        private readonly ShoppingCartService _cartSvc = ShoppingCartService.Current;

        private decimal _taxRate;
        public decimal TaxRate
        {
            get => _taxRate;
            set
            {
                if (_taxRate != value)
                {
                    _taxRate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private decimal _tempTaxRate;
        public decimal TempTaxRate
        {
            get => _tempTaxRate;
            set
            {
                if (_tempTaxRate != value)
                {
                    _tempTaxRate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ConfigViewModel()
        {
            TaxRate = 0.07m;
            TempTaxRate = 0.07m;
        }

        public void UpdateTax()
        {
            TaxRate = TempTaxRate;
            _cartSvc.TaxRate = TaxRate;
        }

        public void ResetTax()
        {
            TempTaxRate = TaxRate;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
                throw new ArgumentNullException(nameof(propertyName));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshUX()
        {
            NotifyPropertyChanged(nameof(TaxRate));
        }
    }
}
