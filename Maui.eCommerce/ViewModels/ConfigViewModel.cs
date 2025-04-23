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

        private string _tempTaxRate;

        public string TempTaxRate
        {
            get => _tempTaxRate;
            set
            {
                if (_tempTaxRate != value)
                {
                    _tempTaxRate = value;
                    NotifyPropertyChanged(nameof(TempTaxRate));
                }
            }
        }

        public void ValidateTaxRate(string newTextValue)
        {
            string validText = new string(newTextValue.Where(c => char.IsDigit(c) || c == '.').ToArray());

            if (validText.Count(c => c == '.') > 1)
            {
                validText = validText.Substring(0, validText.LastIndexOf('.'));
            }

            if (validText == ".")
            {
                validText = string.Empty;
            }

            if (TempTaxRate != validText)
            {
                TempTaxRate = validText;
            }
        }


        public ConfigViewModel()
        {
            _taxRate = 7;
            _tempTaxRate = "7";
        }

        public void UpdateTax()
        {
            if (!string.IsNullOrWhiteSpace(TempTaxRate))
            {
                TaxRate = decimal.Parse(TempTaxRate);
                TempTaxRate = TaxRate.ToString();
                _cartSvc.TaxRate = TaxRate;
            }
            else ResetTax();
        }


        public void ResetTax()
        {
            TempTaxRate = TaxRate.ToString();
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
