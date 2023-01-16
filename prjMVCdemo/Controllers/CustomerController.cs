using prjMVCdemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace prjMVCdemo.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        //兩個方法名稱一樣會模稜兩可 無法辨識(在MVC裡面方法是代表網址)
        [HttpPost]
        public ActionResult Edit(CCustomer p)
        {
            //快速法
            (new CCustomerFactory()).update(p);
            return RedirectToAction("List");


            //腳踏實地法
            //CCustomer x = new CCustomer();
            //x.fid = Convert.ToInt32( Request.Form["txtFid"]);
            //x.fName = Request.Form["txtName"];
            //x.fPhone = Request.Form["txtPhone"];
            //x.fEmail = Request.Form["txtEmail"];
            //x.fAddress = Request.Form["txtAdress"];
            //x.fPassword = Request.Form["txtPassword"];
            //(new CCustomerFactory()).update(x);
            //return RedirectToAction("List");
        }


        //修改
        public ActionResult Edit(int? id)
        {
            if (id == null)
            
                return RedirectToAction("List");
                CCustomer x=(new CCustomerFactory()).queryById((int)id);
                return View(x);
            
        }


        //刪除
        public ActionResult Delete(int? id)
        {
            if(id != null)
            {
                (new CCustomerFactory()).delete((int)id);

            }
          
            return RedirectToAction("List"); //刪除後回傳給List
        }



        //條列客戶清單
        public ActionResult List()
        {
            //撈取所有客戶資料
            List<CCustomer> datas=(new CCustomerFactory()).queryAll();
            string keyword = Request.Form["txtKeyword"];
            if(string.IsNullOrEmpty(keyword))
                datas = (new CCustomerFactory()).queryAll();
            else
                datas=(new CCustomerFactory()).queryByKeyword(keyword);
            return View(datas);

        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Save()
        {
            CCustomer x = new CCustomer();
            x.fName = Request.Form["txtName"];
            x.fPhone = Request.Form["txtPhone"];
            x.fEmail = Request.Form["txtEmail"];
            x.fAddress = Request.Form["txtAdress"];
            x.fPassword = Request.Form["txtPassword"];
            (new CCustomerFactory()).create(x);
            return RedirectToAction("List");
        }



    }
}