using prjMVCdemo.Models;
using prjMVCdemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCdemo.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult CartView()
        {
            List<CShoppingCartItem> cart = Session[CDictionary.Sk_PURCHASED_PRODUCTS_LIST] as List<CShoppingCartItem>;
            if (cart == null)
          
                return RedirectToAction("List");
                return View(cart);
            

        }

            public ActionResult List()
        {
            
            dbDemoEntities db = new dbDemoEntities();        
             var   datas = from t in db.tProduct
                        select t;
            return View(datas);


        }

        public ActionResult AddToSession(int? id)
        {

            ViewBag.fid = id;
            return View();

        }

        [HttpPost]
        public ActionResult AddToSession(CAddToCartViewModel vm)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct p = db.tProduct.FirstOrDefault(t => t.fid == vm.txtFid);
            if (p == null)
                return RedirectToAction("List");
           
            List<CShoppingCartItem> cart =Session[CDictionary.Sk_PURCHASED_PRODUCTS_LIST] as List<CShoppingCartItem>;
            if(cart == null)
            {
                cart = new List<CShoppingCartItem>();
                Session[CDictionary.Sk_PURCHASED_PRODUCTS_LIST] = cart;
            }
            CShoppingCartItem item = new CShoppingCartItem();
            item.price = (decimal) p.fPrice;
            item.productId=vm.txtFid;
            item.count = vm.txtCount;
            item.product = p;
            cart.Add(item);

            return RedirectToAction("List");

        }


        public ActionResult AddToCart(int? id)
        {

           ViewBag.fid = id;
            return View();

        }

        [HttpPost]
        public ActionResult AddToCart(CAddToCartViewModel vm)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct p = db.tProduct.FirstOrDefault(t => t.fid == vm.txtFid);
            if (p== null)
                return RedirectToAction("List");
            tShoppingCart item=new tShoppingCart();
            item.fDate = DateTime.Now.ToString("yyyMMddHHmmss");
            item.fPrice=p.fPrice;
            item.fProductid=vm.txtFid;
            item.fCustomerid = 1;
            item.fCount=vm.txtCount;
            db.tShoppingCart.Add(item);
            db.SaveChangesAsync();
            return RedirectToAction("List");


        }

       

    }
}