using Library.eCommerce.DTO;
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
    public class CartItem
    {
        public Item InventoryItem { get; private set; }
        public int Quantity { get; set; }

        public CartItem(Item inventoryItem)
        {
            InventoryItem = inventoryItem;
            Quantity = 1;
        }
    }
}
