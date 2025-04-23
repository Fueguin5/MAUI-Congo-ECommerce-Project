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
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public Item? SelectedProduct { get; set; }
        public string? Query { get; set; }
        private ProductServiceProxy _svc = ProductServiceProxy.Current;
        private string SortProtocol = "Id";

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshProductList()
        {
            NotifyPropertyChanged(nameof(Products));
        }

        public ObservableCollection<Item?> Products
        {
            get
            {
                var filteredList = _svc.Products
                    .Where(p => p?.Product?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false);

                if (SortProtocol == "Name")
                {
                    filteredList = filteredList.OrderBy(p => p?.Product?.Name);
                }
                else if (SortProtocol == "Price")
                {
                    filteredList = filteredList.OrderBy(p => p?.Product?.Price);
                }
                else
                {
                    filteredList = filteredList.OrderBy(p => p?.Product?.Id);
                }

                return new ObservableCollection<Item?>(filteredList);
            }
        }

        public Item? Delete()
        {
            var item = _svc.Delete(SelectedProduct?.Id ?? 0);
            NotifyPropertyChanged("Products");
            return item;
        }

        public void SortById()
        {
            SortProtocol = "Id";
            RefreshProductList();
        }

        public void SortByName()
        {
            SortProtocol = "Name";
            RefreshProductList();
        }

        public void SortByPrice()
        {
            SortProtocol = "Price";
            RefreshProductList();
        }
    }
}
