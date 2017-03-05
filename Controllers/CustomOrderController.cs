using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

using GroupBuy1.Models;
//using GroupBuy1.QueryResult;
using System.Net;
using System.Web.Script.Serialization;


using System.Data.SqlClient;
using System.Configuration;

using Newtonsoft.Json;
using System.Diagnostics;

namespace GroupBuy1.Controllers
{
    public class CustomOrderController : Controller
    {
        public JsonResult GetOrderByMonth(int cNum, int rNum, string saler, string yM)
        {
            int y = int.Parse(yM.Substring(0, 4));
            int M = int.Parse(yM.Substring(5, 2));
            DateTime date1 = new DateTime(y, M, 1);
            DateTime date2 = date1.AddMonths(1).AddDays(-1);

            JsonResult jsonResult = new JsonResult();

            //var orders = from n in gbdb.StorageIns
            //             where (n.Saler == saler)
            //             && (date1 <= n.CreateDate && n.CreateDate <= date2)
            //             orderby n.CreateDate descending
            //             select n;
            //var newOrders = orders.Skip(cNum).Take(rNum);// 每次获取10条数据

            //string json = JsonConvert.SerializeObject(newOrders);
            //jsonResult.Data = json;
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        }

        public JsonResult QryInventory(string pdNo)
        {
            JsonResult jsonResult = new JsonResult();
            decimal inventory = -999999;
            //var store = gbdb.Stores.Find(pdNo);
            //if (store != null)
            //{
            //    inventory = store.Inventory;
            //}
            jsonResult.Data = inventory;
            return jsonResult;
        }

        public JsonResult HelloH5(string customerJson)
        {//zcscdb2 W999-11470032, zcscdb K002-KW215-001,  zcscdb 21440004
            var jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            JavaScriptSerializer js = new JavaScriptSerializer();
            //var customersJson = js.Deserialize<object>(customerJson);
            var customersJson = js.Deserialize<List<M_CORDERC>>(customerJson);
            kw_m01Context kw_m01 = new kw_m01Context();
            kw_m01.M_CORDERC.AddRange(customersJson);
            try
            {
                kw_m01.SaveChanges();
            }
            catch (DbUpdateException devExc1) { }
            catch (DbEntityValidationException devExc2) { }
            return jsonResult;
        }

        public ActionResult Barcode_Scan()
        {
            return View();
        }

        // 保存订单
        public JsonResult SaveOrder(string lisJson, string storageInJson)
        {
            var jsonResult = new JsonResult();
            JavaScriptSerializer js = new JavaScriptSerializer();
            //StorageIn storageIn = js.Deserialize<StorageIn>(storageInJson);
            //List<string> liJsons = js.Deserialize<List<string>>(lisJson); //反序列化
            //List<StorageInC> storageInCs = new List<StorageInC>();
            //foreach (string liJson in liJsons)
            //{
            //    storageInCs.Add(js.Deserialize<StorageInC>(liJson));
            //}

            //storageIn.CreateDate = DateTime.Now;

            ///* begin 生成InId  */
            //var storageIns = from p in gbdb.StorageIns
            //                 orderby p.CreateDate descending
            //                 select p;

            string lastID = "";
            //lastID = storageIns.First().InID;
            DateTime date = DateTime.Now;
            int nowYear = date.Year % 100;
            int nowMonth = date.Month;

            int idYear = int.Parse(lastID.Substring(4, 2));
            int idMonth = int.Parse(lastID.Substring(6, 2));
            int idNo = int.Parse(lastID.Substring(8, 4));
            //if (nowMonth == idMonth)
            //{
            //    idNo += 1;
            //}
            //else
            //{
            //    idYear = nowYear;
            //    idMonth = nowMonth;
            //    idNo = 1;
            //}
            //string InID = "395-" + String.Format("{0:D2}", idYear) + String.Format("{0:D2}", idMonth) + String.Format("{0:D4}", idNo);
            /* end 生成InId  */
            //string InID = "395-00000000";

            //storageIn.InID = InID;
            //storageIn.Passed = 0;
            //gbdb.StorageIns.Add(storageIn);
            //foreach (StorageInC stoInC in storageInCs)
            //{
            //    stoInC.InID = InID;
            //    gbdb.StorageInCs.Add(stoInC);
            //}
            //try
            //{
            //    gbdb.SaveChanges();
            //    jsonResult.Data = "success";
            //}
            //catch (System.Data.Entity.Infrastructure.DbUpdateException devExc1) { }
            //catch (DbEntityValidationException devExc2) { }
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        }

        // 通过型号返回产品
        public JsonResult GetProduct(string ProductNo)
        {
            var jsonResult = new JsonResult();

            if (String.IsNullOrEmpty(ProductNo))
            {// 
                return jsonResult;
            }
            //PRODUCT product = zcscdb.PRODUCTs.Find(ProductNo);
            //if (product == null)
            //{
            //    return jsonResult;
            //}

            //List<string> cnames = new List<string>();
            //foreach (var pdcl in product.PRODCOLORs)
            //{
            //    cnames.Add(pdcl.COLORSET.CNAME);
            //}

            //decimal inventory = -99999;
            //Store store = gbdb.Stores.Find(ProductNo);
            //if (store != null)
            //{
            //    inventory = store.Inventory;
            //}

            //ProductQry productQry = new ProductQry();
            ////productQry.product = product;
            ////productQry.cnames = cnames;
            //productQry.cunit = product.UNIT.CNAME;
            //productQry.inventory = inventory;

            //string json = JsonConvert.SerializeObject(productQry);
            //jsonResult.Data = json;
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        }


        // GET: CustomOrder
        // 获取产品列表并返回添加订单的页面
        public ActionResult Index()
        {
            return View();
        }

        // GET: CustomOrder/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomOrder/Create
        public ActionResult Create()
        {
            return View();
        }

        // 获取产品列表
        //public JsonResult ProductNos()
        //{
        //    JsonResult jsonResult = new JsonResult();
        //    var products = from p in zcscdb.PRODUCTs
        //                   select p.CMTEID;

        //    JavaScriptSerializer json = new JavaScriptSerializer();
        //    string s = json.Serialize(products);
        //    jsonResult.Data = products;
        //    jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        //    return jsonResult;
        //}

        // POST: CustomOrder/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomOrder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomOrder/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomOrder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomOrder/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }

}
