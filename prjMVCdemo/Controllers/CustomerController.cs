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