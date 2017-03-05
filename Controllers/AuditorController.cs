using GroupBuy1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Security.Cryptography;

namespace GroupBuy1.Controllers
{
    public class AuditorController : Controller
    {
        private string o = "";

        // 提交登录信息 
        public JsonResult Login(string db, string account, string password)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jsonResult.Data = 0;

            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                var user = kw_m01.v_userlist.Find(account, db);

                if (user != null)
                {// 账户匹配
                    if (password == user.cdesc)
                    {// 密码匹配
                        if (user.cstop == "0" && user.cempid == "Y")
                        {// 未停用，可登陆
                            Session["db"] = db;
                            Session["cid"] = user.cid;
                            Session["cname"] = user.cname;
                            jsonResult.Data = "success";
                        }
                    }
                }
            }
            return jsonResult;
        }
        // 跳转到Create视图
        public ActionResult OpenCreate()
        {
            return View("Test");
        }

        // 跳转到Create视图
        public ActionResult Test()
        {
            return View("Test");
        }

        // 提交订单到erp 减库存
        public JsonResult CMT(string ccode, string db, string ccustid)
        {
            var jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jsonResult.Data = 4;

            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                var corder = kw_m01.M_CORDER.Find(ccode, db);
                if (corder.CMTFLAG == "1")
                {
                    jsonResult.Data = 5;
                    return jsonResult;
                }

                // 转到erp
                kw_m01.p_ins_custid_no(db, ccustid);
                kw_m01.sp_ins_erp_corder(db, ccode);

                // 让订单处于已提交状态
                corder.CMTFLAG = "1";
                kw_m01.M_CORDER.AddOrUpdate(corder);

                var cordercs = from c in kw_m01.M_CORDERC
                               where c.cquotid == db
                               && c.CCODE == ccode
                               select c;

                var date = DateTime.Now;

                foreach (var corderc in cordercs)
                {
                    // 每个项的库存异动
                    M_StoreChange storeChange = new M_StoreChange();
                    storeChange.db = db;
                    storeChange.ccode = ccode;
                    storeChange.iorder = corderc.IORDER;
                    storeChange.cmteid = corderc.CMTEID;
                    storeChange.changeFlag = -1;
                    storeChange.quantity = corderc.DQTY;
                    storeChange.changeDate = date.Date;
                    kw_m01.M_StoreChange.Add(storeChange);

                    // 每个项的库存更改kw
                    var store = kw_m01.gb_Store.Find(corderc.CMTEID);
                    store.Inventory -= corderc.DQTY;
                    kw_m01.gb_Store.AddOrUpdate(store);
                }

                // 保存数据库更改
                kw_m01.SaveChanges();

                jsonResult.Data = "success";
            }

            return jsonResult;
        }

        // 从erp撤回订单
        public JsonResult CancelOrder(string ccode, string db)
        {
            var jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            //jsonResult.Data = 4;

            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                int flag = kw_m01.sp_del_erp_corder(db,ccode);
                if (flag == 1)
                {
                    jsonResult.Data = "success";
                }
            }
            return jsonResult;
        }
        // 通过条件查询订单列表
        public String OrderList(string passed, DateTime date1, DateTime date2, string areaQry, string storeQry, string salerQry, string inIDQry)

        {
            var flag = true;
            if (date1.Date.ToString() != "9999/12/31 0:00:00")
            {
                flag = false;
                date2 = date2.AddDays(1);
            }
            String json = "";

            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                var corders = from s in kw_m01.M_CORDER
                              where (s.CMTFLAG == passed)
                              //&& (areaQry == "" ? true : s == areaQry)
                              //&& (storeQry == "" ? true : s.Store == storeQry)
                              //&& (salerQry == "" ? true : s.Saler == salerQry)
                              && (inIDQry == "" ? true : s.CCODE == inIDQry)
                              && (flag ? true : (date1 <= s.DDATE && s.DDATE <= date2))
                              orderby s.DDATE
                              select s;

                //var totalPage = (storageIns.Count() / 10.0); // 这里要设置每行
                json = JsonConvert.SerializeObject(corders);
                //if (storageIns.Count() > 0)
                //{
                //    json = "{\"total\": \"" + totalPage + "\", \"page\": \"1\", \"records\": \"" + storageIns.Count() + "\", \"rows\": [";
                //    foreach (var st in storageIns)
                //    {
                //        json += "{\"InID\":\"" + st.InID + "\", \"StoreArea\":\"" + st.StoreArea + "\", \"Store\":\"" + st.Store + "\" , \"Saler\":\""
                //            + st.Saler + "\" , \"CustomName\":\"" + st.CustomName + "\" , \"Phone\":\"" + st.Phone + "\"}";
                //        json += ",";
                //    }
                //    json = json.Substring(0, json.Length - 1);
                //    json += "]}";
                //}
            }
            return json;

        }

        // 获取某个订单全部信息
        public JsonResult GetOrder(string InID, string db)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                var corder = kw_m01.M_CORDER.Find(db, InID);

                var cordercs = from s in kw_m01.vq_m_corderc
                               where s.CCODE == InID
                               && s.cquotid == db
                               select s;

                foreach (M_CORDERC corderc in corder.M_CORDERC)
                {
                    var store = kw_m01.gb_Store.Find(corderc.CMTEID);

                    // ctype 临时用于存放库存
                    if (store != null)
                    {
                        corderc.ctype = store.Inventory.ToString();
                    }
                    else
                    {
                        corderc.ctype = "-999999";
                    }
                }
                OrderQry orderQry = new OrderQry();
                orderQry.corder = corder;
                orderQry.cordercs = cordercs;
                jsonResult.Data = JsonConvert.SerializeObject(orderQry);
            }

            return jsonResult;
        }

        // 审核ERP 中的订单
        public JsonResult Audit(string db, string ccode, string cmtMan)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                // 获取订单 检查是否已审核
                var corder = kw_m01.v_corder.Find(db, ccode);
                if(corder.CMTFLAG == "0")
                {
                    // 执行存储过程 审核订单
                    try
                    {
                        kw_m01.sp_audit_erp_corder(ccode, db, cmtMan);
                        jsonResult.Data = "success";
                    }
                    catch (Exception ex)
                    {
                        new SaveError(ex, o, o, o, o, o, db, ccode);
                        jsonResult.Data = ex.InnerException.Message;
                    }
                }

            }
            return jsonResult;
        }
    }

}

