using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class CartController : Controller
    {
        ShopEntities2 _db = new ShopEntities2();
        // GET: Cart
        public ActionResult Index()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
                return View(cart.cartItems);
            return View();
        }

        public ActionResult CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.cartItems.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }

        public ActionResult CheckOutSuccess(string paymentType)
        {
            if (paymentType == "COD")
            {
                return View("CheckOutSuccessCOD");
            }
            else
            {
                return View("CheckOutSuccessTransfer");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderViewModel req)
        {
            var code = new { Success = false, Code = -1 };
            if (ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart != null)
                {
                    // Thêm đơn hàng vào cơ sở dữ liệu
                    order order = new order();
                    order.customname = req.customname;
                    order.phone = req.phone;
                    order.address = req.address;
                    cart.cartItems.ForEach(x => order.orderdetails.Add(new orderdetail
                    {
                        productid = x.productid,
                        quantity = x.quantity,
                        price = (int?)x.price
                    }));
                    order.total = (int?)cart.cartItems.Sum(x => (x.price * x.quantity));
                    order.description = req.description;
                    order.typepayment = req.typepayment;
                    order.datebegin = DateTime.Now;
                    Random rd = new Random();
                    order.code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    _db.orders.Add(order);
                    _db.SaveChanges();

                    // Giảm số lượng sản phẩm trong kho
                    foreach (var item in cart.cartItems)
                    {
                        var product = _db.products.FirstOrDefault(p => p.id == item.productid);
                        if (product != null)
                        {
                            product.quantity -= item.quantity;
                        }
                    }
                    _db.SaveChanges();

                    // Xóa các mục trong giỏ hàng
                    cart.clearItem();

                    // Chuyển hướng đến trang thành công
                    if (req.typepayment == 1)
                    {
                        return RedirectToAction("CheckOutSuccess", new { paymentType = "COD" });
                    }
                    else if (req.typepayment == 2)
                    {
                        return RedirectToAction("CheckOutSuccess", new { paymentType = "Transfer" });
                    }
                    else
                    {
                        return RedirectToAction("CheckOutSuccess");
                    }
                }
            }
            return Json(code);
        }

        public ActionResult Partial_CheckOut()
        {
            return PartialView();
        }


        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            var checkProduct = _db.products.FirstOrDefault(x => x.id == id);
            if (checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart == null)
                {
                    cart = new ShoppingCart();
                }
                    cartItem item = new cartItem
                    {
                        productid = checkProduct.id,
                        productname = checkProduct.name,
                        categoryid = (int)checkProduct.categoryid,
                        img = checkProduct.img,
                        price = (double)checkProduct.price,
                        quantity = quantity
                    };
                    item.totalprice = item.price * item.quantity;
                    cart.AddToCart(item, quantity);
                    Session["Cart"] = cart;
                    code = new { Success = true, msg = "Successfully added " + checkProduct.name, code = 1, Count = cart.cartItems.Count };
            }
            return Json(code);
        }

        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if(cart != null)
            {
                var checkProduct = cart.cartItems.FirstOrDefault(x => x.productid == id);
                if(checkProduct != null)
                {
                    cart.removeItem(id);
                    code = new { Success = true, msg = "", code = 1, Count = cart.cartItems.Count };
                }
            }
            return Json(code);
        }
        

        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.clearItem();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        public ActionResult Partial_Item_Cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.cartItems.Any())
                return View(cart.cartItems);
            return PartialView();
        }

        public ActionResult Partial_Item_ThanhToan()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.cartItems.Any())
                return View(cart.cartItems);
            return PartialView();
        }

        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return Json(new { Count = cart.cartItems.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.updateQuantity(id, quantity);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
    }
 }