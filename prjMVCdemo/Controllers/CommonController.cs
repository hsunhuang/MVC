using prjMVCdemo.Models;
using prjMVCdemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCdemo.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Home()
        {
            if (Session[CDictionary.SK_LOINGED_USER]==null)
                return RedirectToAction("Login");
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(CLoginViewModel vm)
        {
            CCustomer user = (new CCustomerFactory()).queryByEmail(vm.txtAccount);
            if(user != null)
            {
                if(user.fPassword.Equals(vm.txtPassword))
                {
                    Session[CDictionary.SK_LOINGED_USER] = user;
                    return RedirectToAction("Home");
                }
            }
            return View();
        }

    }
}
