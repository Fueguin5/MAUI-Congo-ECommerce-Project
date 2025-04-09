using Library.eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        private ProductServiceProxy _prodSvc = ProductServiceProxy.Current;
        private List<CartItem> items;

        public List<CartItem> CartItems
        {
            get
            {
                CleanupOrphanedCartItems();
                return items;
            }
        }

        public static ShoppingCartService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShoppingCartService();
                }
                return instance;
            }
        }

        private static ShoppingCartService? instance;

        private ShoppingCartService()
        {
            items = new List<CartItem>();
        }

        public CartItem? AddOrUpdate(Item item)
        {
            var existingInvItem = _prodSvc.GetById(item.Id);
            if (existingInvItem == null || existingInvItem.Quantity == 0)
                return null;

            existingInvItem.Quantity--;

            var existingCartItem = items.FirstOrDefault(ci => ci.InventoryItem.Id == item.Id);
            if (existingCartItem == null)
            {
                var cartItem = new CartItem(existingInvItem);
                items.Add(cartItem);
                return cartItem;
            }
            else
            {
                existingCartItem.Quantity++;
                return existingCartItem;
            }
        }

        public CartItem? ReturnItem(Item item)
        {
            var cartItem = items.FirstOrDefault(ci => ci.InventoryItem.Id == item.Id);
            if (cartItem == null) return null;

            cartItem.Quantity--;
            if (cartItem.Quantity <= 0)
            {
                items.Remove(cartItem);
            }

            var invItem = _prodSvc.GetById(item.Id);
            if (invItem != null)
            {
                invItem.Quantity++;
            }

            return cartItem;
        }

        public void CleanupOrphanedCartItems()
        {
            items = items
                .Where(ci => _prodSvc.GetById(ci.InventoryItem.Id) != null)
                .ToList();
        }
    }
}
