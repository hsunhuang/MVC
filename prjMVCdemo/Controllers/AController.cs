using prjMVCdemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;

namespace prjMVCdemo.Controllers
{

    public class AController : Controller
    {
        public ActionResult demoForm()
        {
            //相加的公式
            ViewBag.ANS = "?";
            if (!string.IsNullOrEmpty(Request.Form["txtA"]))
            {
                double a = Convert.ToDouble(Request.Form["txtA"]);
                double b = Convert.ToDouble(Request.Form["txtB"]);
                ViewBag.ANS = a + b;
            }

            return View();

        }

        public ActionResult demoForm2()
        {
            //開根號的公式
            ViewBag.ANS = "?";
            if (!string.IsNullOrEmpty(Request.Form["txtA"]))
            {

                double a = Convert.ToDouble(Request.Form["txtA"]);
                double b = Convert.ToDouble(Request.Form["txtB"]);
                double c = Convert.ToDouble(Request.Form["txtC"]);
                //保留數字用viewbag
                ViewBag.a = a;
                ViewBag.b = b;
                ViewBag.c = c;

                double r = b * b - 4 * a * c;
                r = Math.Sqrt(r);
              

                ViewBag.ANS = ((-b + r) / (2 * a)).ToString("0.0#") + " Or X=" + ((-b - r) / (2 * a)).ToString();

            }
            return View();
        }
        
        
        //測試程式-查詢Query
        public string testingQuery()
        {
           
            return "目前客戶數:"+ (new CCustomerFactory()).queryAll().Count.ToString();
        }


        //測試程式-Update
        public string testingUpdate(int? id)
        {
            if (id == null)
                return "請指定ID";
            CCustomer x = new CCustomer();
            x.fid = (int)id;
            //x.fName = "Jason";
            x.fPhone = "0978945612";
            //x.fEmail = "jason@gmail.com";
            x.fAddress = "NewTaipei";
            x.fPassword = "4321";
            (new CCustomerFactory()).update(x);
            return "修改資料成功";
        }

        //測試程式-Delete
        public string testingDelete(int? id)
        {
            if (id == null)
                return "請指定ID";
            (new CCustomerFactory()).delete((int)id);
            return "刪除資料成功";
        }

        //測試程式-Insert
        public string testingInsert()
        {
            CCustomer x = new CCustomer();
            x.fName = "Jason";
            x.fPhone = "0978945612";
            x.fEmail = "jason@gmail.com";
            x.fAddress = "Tainan";
            x.fPassword = "1234";
            (new CCustomerFactory()).create(x);
            return "新增資料成功";
        }

       

        public ActionResult bindingById(int? id)
        {
            CCustomer x = null;
            if (id != null)
           
            {
                SqlConnection con = new SqlConnection(); //程式化語法物件
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open(); //連線開啟資料庫
                SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fid=" + id.ToString(), con); //建立SQL命令物件,參數1:SQL語法/參數2:SQL連線(con)
                SqlDataReader reader = cmd.ExecuteReader();  //SQL資料讀取器
                if (reader.Read())
                {
                    x = new CCustomer()
                    {
                        fEmail = reader["fEmail"].ToString(),
                        fPhone = reader["fPhone"].ToString(),
                        fName = reader["fName"].ToString(),
                        fid = (int)reader["fid"]

                    };
                   
                }
                con.Close();
            }

            return View(x);
        }


        //弱型別傳法
        public ActionResult showById(int? id)
        {
            //ViewBag.kk = "查無資料";
            if (id != null)
            //    ViewBag.kk = "請指定查詢ID";
            
            //else
            {
                SqlConnection con = new SqlConnection(); //程式化語法物件
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open(); //連線開啟資料庫
                SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fid=" + id.ToString(), con); //建立SQL命令物件,參數1:SQL語法/參數2:SQL連線(con)
                SqlDataReader reader = cmd.ExecuteReader();  //SQL資料讀取器
                if (reader.Read())
                {
                    CCustomer x = new CCustomer()
                    {
                        fEmail = reader["fEmail"].ToString(),
                        fPhone = reader["fPhone"].ToString(),
                        fName = reader["fName"].ToString(),
                        fid = (int)reader["fid"]

                    };
                   ViewBag.kk = x;
                }
                con.Close();
            }

                return View();
        }

        public string queryById(int? id)
        {
            if (id == null)
                return "請指定要查詢ID";
            SqlConnection con = new SqlConnection(); //程式化語法物件
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open(); //連線開啟資料庫
            SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fid=" + id.ToString(), con); //建立SQL命令物件,參數1:SQL語法/參數2:SQL連線(con)
            SqlDataReader reader = cmd.ExecuteReader();  //SQL資料讀取器
            string s = "查無改筆資料";
            if (reader.Read())
                s = reader["fName"].ToString() + "<br/>" + reader["fPhone"].ToString();
            con.Close();
            return s;
        }
        
        public string demoServer()
        {
            return "目前伺服器上實體位置:" + Server.MapPath(".");
        }

            //用request抓資料
            public string demoRequest()
        {
            string id = Request.QueryString["pid"];
            if (id == "0")
                return "XBox 加入購物車成功";
            else if (id == "1")
                return "PS5 加入購物車成功";
            else if (id == "2")
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        //用參數抓資料,但參數不可是null
        public string demoParaneter(int p)
        {
            if (p == 0)
                return "XBox 加入購物車成功";
            else if (p == 1)
                return "PS5 加入購物車成功";
            else if (p == 2)
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        //參數int?不等於int,使用「?」運算子讓參數接受 null 
        public string demoParaneter1(int? p)
        {
            if (p == 0)
                return "XBox 加入購物車成功";
            else if (p == 1)
                return "PS5 加入購物車成功";
            else if (p == 2)
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        //參數id 可以不加?=直接打數字
        public string demoParaneter2(int id)
        {
            if (id == 0)
                return "XBox 加入購物車成功";
            else if (id == 1)
                return "PS5 加入購物車成功";
            else if (id == 2)
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        public string sayHello()
        {
            return "Hello world!!!";
        }
        public string lotto()
        {

          ClottoGen x=new ClottoGen();
            return x.getNumbers();
        }

        // GET: A
        public ActionResult Index()
        {
            return View();
        }
    }
}