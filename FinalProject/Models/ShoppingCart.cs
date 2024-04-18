using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class ShoppingCart
    {
        public List<cartItem> cartItems { get; set; }
        public ShoppingCart()
        {
            this.cartItems = new List<cartItem>();
        }
        public void AddToCart(cartItem item, int quantity) { 
            var CheckIfExist = cartItems.FirstOrDefault(x => x.productid == item.productid);
            if (CheckIfExist != null)
            {
                CheckIfExist.quantity += quantity;
                CheckIfExist.totalprice = CheckIfExist.price * CheckIfExist.quantity;
            }
            else
            {
                cartItems.Add(item);
            }
        }
        public void removeItem(int productid) {
            var CheckIfExist = cartItems.FirstOrDefault(x => x.productid == productid);
            if(CheckIfExist != null)
            {
                cartItems.Remove(CheckIfExist);
            }
        }
        public void updateQuantity(int productid, int quantity) {
            var CheckIfExist = cartItems.FirstOrDefault(x => x.productid == productid);
            if(CheckIfExist != null)
            {
                CheckIfExist.quantity = quantity;
                CheckIfExist.totalprice = CheckIfExist.quantity* CheckIfExist.price;   
            }
        }
        public void clearItem()
        {
            cartItems.Clear();
        }
        public double getTotalPrice()
        {
            double totalPrice = 0;
            foreach (var item in cartItems)
            {
                totalPrice += item.totalprice;
            }
            return totalPrice;
        }

        public int getTotalQuantity()
        {
            int totalQuantity = 0;
            foreach (var item in cartItems)
            {
                totalQuantity += item.quantity;
            }
            return totalQuantity;
        }

    }
    public class cartItem
    {
        public int productid { get; set; }
        public String productname { get; set; }
        public int categoryid { get; set; }
        public string img { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public double totalprice { get; set; }

    }
}
