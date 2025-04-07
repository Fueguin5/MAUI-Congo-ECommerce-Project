﻿using Library.eCommerce.DTO;
using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.eCommerce.Models
{
    public class Item
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public int? Quantity { get; set; }
        public ICommand? AddCommand { get; set; }

        public override string ToString()
        {
            return $"{Product}\tQuantity: {Quantity}";
        }

        public string Display {
            get
            {
                return ToString();
            }
        }

        public Item()
        {
            Product = new ProductDTO();
            Quantity = 0;

            AddCommand = new Command(DoAdd);
        }

        private void DoAdd()
        {
            ShoppingCartService.Current.AddOrUpdate(this);
        }

        public Item(Item i)
        {
            Product = new ProductDTO(i.Product);
            Quantity = i.Quantity;
            Id = i.Id;

            AddCommand = new Command(DoAdd);
        }
    }
}
