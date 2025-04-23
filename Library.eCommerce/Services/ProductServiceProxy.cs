using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.eCommerce.Services
{
    public class ProductServiceProxy
    {
        private ProductServiceProxy()
        {
            Products = new List<Item?>
            {
                new Item{ Product = new ProductDTO{Id = 1, Name ="Product1", Price = 1}, Id = 1, Quantity = 1 },
                new Item{ Product = new ProductDTO{Id = 2, Name ="Product2", Price = 2}, Id = 2, Quantity = 2 },
                new Item{ Product = new ProductDTO{Id = 3, Name ="Product3", Price = 3}, Id = 3, Quantity = 3 },
                new Item{ Product = new ProductDTO{Id = 4, Name ="Coffee Mug", Price = 5.99m}, Id = 4, Quantity = 10 },
                new Item{ Product = new ProductDTO{Id = 5, Name ="Notebook", Price = 3.49m}, Id = 5, Quantity = 20 },
                new Item{ Product = new ProductDTO{Id = 6, Name ="Wireless Mouse", Price = 15.99m}, Id = 6, Quantity = 5 },
                new Item{ Product = new ProductDTO{Id = 7, Name ="Bluetooth Speaker", Price = 29.99m}, Id = 7, Quantity = 7 },
                new Item{ Product = new ProductDTO{Id = 8, Name ="Desk Lamp", Price = 12.89m}, Id = 8, Quantity = 8 },
                new Item{ Product = new ProductDTO{Id = 9, Name ="USB Cable", Price = 2.50m}, Id = 9, Quantity = 50 },
                new Item{ Product = new ProductDTO{Id = 10, Name ="Water Bottle", Price = 8.25m}, Id = 10, Quantity = 12 },
                new Item{ Product = new ProductDTO{Id = 11, Name ="Phone Stand", Price = 6.75m}, Id = 11, Quantity = 18 },
                new Item{ Product = new ProductDTO{Id = 12, Name ="LED Strip", Price = 11.49m}, Id = 12, Quantity = 9 },
                new Item{ Product = new ProductDTO{Id = 13, Name ="Gaming Keyboard", Price = 49.99m}, Id = 13, Quantity = 4 }
            };
        }


        private int LastKey
        {
            get
            {
                if(!Products.Any())
                {
                    return 0;
                }

                return Products.Select(p => p?.Id ?? 0).Max();
            }
        }

        private static ProductServiceProxy? instance;
        private static object instanceLock = new object();
        public static ProductServiceProxy Current
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductServiceProxy();
                    }
                }

                return instance;
            }
        }

        public List<Item?> Products { get; private set; }


        public Item AddOrUpdate(Item item)
        {
            if (item == null)
            {
                return null;
            } else if(item.Id == 0)
            {
                item.Id = LastKey + 1;
                item.Product.Id = item.Id;
                Products.Add(item);
            } else
            {
                var existingItem = Products.FirstOrDefault(p => p.Id == item.Id);
                var index = Products.IndexOf(existingItem);
                Products.RemoveAt(index);
                Products.Insert(index,new Item(item));
            }

            return item;
        }

        public Item? Delete(int id)
        {
            if(id == 0)
            {
                return null;
            }

            Item? product = Products.FirstOrDefault(p => p.Id == id);
            Products.Remove(product);

            return product;
        }

        public Item? GetById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }
    }
}
