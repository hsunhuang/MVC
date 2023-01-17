using prjMVCdemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCdemo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult List()
        {
            IEnumerable<tProduct>datas=null;
            string keyword = Request.Form["txtKeyword"];
            dbDemoEntities db = new dbDemoEntities();
            if (string.IsNullOrEmpty(keyword))
                datas = from t in db.tProduct
                        select t;
            else
                datas = db.tProduct.Where(t => t.fName.Contains(keyword));
            return View(datas);

            //dbDemoEntities db=new dbDemoEntities();
            //var datas = from t in db.tProduct
            //            select t;
            //return View(datas);
        }



        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tProduct p)
        {
            dbDemoEntities db = new dbDemoEntities(); //紅框
            db.tProduct.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }


        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                dbDemoEntities db = new dbDemoEntities();
                tProduct delProduct=db.tProduct.FirstOrDefault(t=>t.fid==id);
                if(delProduct!=null)
                {
                    db.tProduct.Remove(delProduct);
                    db.SaveChangesAsync();
                }
              
            }
            return RedirectToAction("List"); //刪除後回傳給List
        }

        [HttpPost]
        //存入資料庫
        public ActionResult Edit(tProduct p)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct x = db.tProduct.FirstOrDefault(t => t.fid == p.fid);
            if (x != null)
            {
                if(p.photo != null)
                {
                    string photoName=Guid.NewGuid().ToString()+".jpg";
                    x.fImagePath= photoName;
                    p.photo.SaveAs(Server.MapPath(@"..\..\img\"+photoName));
                }
                x.fName = p.fName;
                x.fCost = p.fCost;
                x.fPrice = p.fPrice;
                x.fQty = p.fQty;
                db.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }

        //第一步找資料
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                dbDemoEntities db = new dbDemoEntities();
                tProduct x = db.tProduct.FirstOrDefault(t => t.fid == id);
                if (x != null)
              return View(x);

            }
            return RedirectToAction("List"); //刪除後回傳給List
        }


     

    }
}