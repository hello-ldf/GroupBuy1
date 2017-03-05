using GroupBuy1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GroupBuy1.Controllers
{
    public class SalerController : Controller
    {
        private string o = "";

        // 销售员 登录
        public JsonResult Login(string loginInfoJson)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jsonResult.Data = "NoMatch";

            LoginInfo loginInfo = JsonConvert.DeserializeObject<LoginInfo>(loginInfoJson);
            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                var user = kw_m01.v_userlist.Find(loginInfo.account, loginInfo.db);

                if (user != null)
                {// 账户匹配
                    if (loginInfo.password == user.cdesc)
                    {// 密码匹配
                        if (user.cstop == "0" && user.cempid == "Y")
                        {// 未停用，可登陆

                            // 店长号拥有权限
                            var maos = from m in kw_m01.mao_file
                                       where m.mao05 == loginInfo.account
                                       && m.mao01 == loginInfo.db
                                       select m;

                            // 测试期间 存放mac地址
                            map_file map = new map_file();
                            map.map01 = loginInfo.db;
                            map.map02 = loginInfo.dbName;
                            map.map03 = loginInfo.account;
                            map.map04 = user.cname;
                            map.map05 = loginInfo.macAddr;
                            map.map06 = "Y";
                            map.mapacti = "Y";
                            kw_m01.map_file.AddOrUpdate(map);
                            kw_m01.SaveChanges();

                            var temp = new { cid = user.cid, cname = user.cname, maos = maos };
                            jsonResult.Data = JsonConvert.SerializeObject(temp);
                        }
                    }
                }
            }
            return jsonResult;
        }

        // 添加订单
        public JsonResult AddOrder(string customerJson, string corderJson, string cordercsJson, string flag)
        {
            var jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;


            // 反序列化 单头单身
            JavaScriptSerializer js = new JavaScriptSerializer();
            var customer = js.Deserialize<M_CUSTOMER>(customerJson);
            var corder = js.Deserialize<M_CORDER>(corderJson);
            var cordercs = js.Deserialize<List<M_CORDERC>>(cordercsJson);
            Debug.Assert(customer != null, "customer 为null！");
            Debug.Assert(corder != null, "corder 为null！");


            // 访问数据库
            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                // 获取客户号 填入customer&corder
                var custmResult = kw_m01.p_cre_custid_no(customer.cdeareaid, customer.CCLASSID, customer.cconmante).ToList();
                Debug.Assert(custmResult.First() != null && custmResult.First() != "", "客户ID获取失败");
                customer.CCUSTID = custmResult.First();
                corder.CCUSTID = customer.CCUSTID;


                // 获取单号 填入customer&corder
                var orderResult = kw_m01.p_getmaxcode3("305", "1", corder.cpathid, DateTime.Now.Date).ToList();
                Debug.Assert(orderResult.First() != null && orderResult.First() != "", "订单ID获取失败");
                corder.CCODE = orderResult.First();
                customer.CREMARK = corder.CCODE;
                for (var i = 0; i < cordercs.Count; i++)
                {
                    cordercs[i].CCODE = corder.CCODE;
                }


                // 操作数据库
                try
                {
                    kw_m01.M_CUSTOMER.AddOrUpdate(customer);
                    kw_m01.M_CORDER.Add(corder);
                    if (cordercs.Count != 0)
                    {
                        kw_m01.M_CORDERC.AddRange(cordercs);
                    }
                    kw_m01.SaveChanges();


                    if (flag == "1")
                    {
                        kw_m01.p_ins_custid_no(customer.cdeareaid, customer.CCUSTID);
                        kw_m01.sp_ins_erp_corder(corder.cpathid, corder.CCODE);
                        corder.CMTFLAG = "1";
                        kw_m01.M_CORDER.AddOrUpdate(corder);
                        kw_m01.SaveChanges();
                    }
                    jsonResult.Data = "success";
                }
                catch (Exception ex)
                {
                    string o = "";
                    new SaveError( ex, o, o, o, o, o, "区域：" + corder.cpathid, "订单号：" + corder.CCODE);
                    jsonResult.Data = 3;
                }
            }
            return jsonResult;
        }


        // 保存订单
        public JsonResult SaveOrder(string customerJson, string corderJson, string cordercsJson, string flag)
        {
            var jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            // 反序列化 单头单身
            JavaScriptSerializer js = new JavaScriptSerializer();
            var customer = js.Deserialize<M_CUSTOMER>(customerJson);
            var corder = js.Deserialize<M_CORDER>(corderJson);
            var cordercs = js.Deserialize<List<M_CORDERC>>(cordercsJson);
            Debug.Assert(customer != null, "customer 为null！");
            Debug.Assert(corder != null, "corder 为null！");


            // 访问数据库
            //TransactionScope scope = new TransactionScope()
            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                var dels = from s in kw_m01.M_CORDERC
                           where s.CCODE == corder.CCODE
                           && s.cquotid == corder.cpathid
                           select s;

                // 操作数据库
                kw_m01.M_CORDER.AddOrUpdate(corder);
                kw_m01.M_CUSTOMER.AddOrUpdate(customer);
                kw_m01.M_CORDERC.RemoveRange(dels);

                if (cordercs.Count != 0)
                {
                    kw_m01.M_CORDERC.AddRange(cordercs);
                }

                if (flag == "1")
                {
                    var erp_order = kw_m01.v_corder.Find(corder.cpathid, corder.CCODE);
                    if (erp_order.CMTFLAG == "0")
                    {
                        try
                        {
                            kw_m01.SaveChanges();
                            kw_m01.sp_erp_corder(1, corder.cpathid, corder.CCODE, o, o, o, o, o, o, o, o);
                            kw_m01.p_ins_custid_no(customer.cdeareaid, customer.CCUSTID);
                            kw_m01.sp_ins_erp_corder(corder.cpathid, corder.CCODE);
                            jsonResult.Data = "success";
                        }
                        catch (Exception ex)
                        {
                            new SaveError( ex, o, o, o, o, o, "db:" + corder.cpathid, "客户订单号:" + corder.CCODE);
                        }
                    }
                }
                else
                {
                    var tempOrder = kw_m01.M_CORDER.Find(corder.CCODE, corder.cpathid);

                    if (tempOrder.CMTFLAG == "0")
                    {
                        kw_m01.SaveChanges();
                        jsonResult.Data = "success";
                    }
                }
            }
            return jsonResult;
        }

        // 删除订单
        public JsonResult DelOrder(string db, string ccode, string flag)
        {
            var jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jsonResult.Data = 6;

            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                if (flag == "1")
                {
                    var erp_corder = kw_m01.v_corder.Find(db, ccode);
                    if (erp_corder.CMTFLAG == "0")
                    {
                        try
                        {

                            int rs = kw_m01.sp_erp_corder(1, db, ccode, "clear", o, o, o, o, o, o, o);
                            jsonResult.Data = "success";
                        }
                        catch (Exception ex)
                        {
                            new SaveError( ex, o, o, o, o, o, o, o);
                        }
                    }
                }
                else
                {
                    var cordercs = from c in kw_m01.M_CORDERC
                                   where (c.cquotid == db) && (c.CCODE == ccode)
                                   select c;
                    var tempOrder = kw_m01.M_CORDER.Find(ccode, db);
                    var customer = kw_m01.M_CUSTOMER.Find(tempOrder.CCUSTID, db);

                    if (tempOrder.CMTFLAG == "0")
                    {
                        kw_m01.M_CORDER.Remove(tempOrder);
                        kw_m01.M_CORDERC.RemoveRange(cordercs);
                        kw_m01.M_CUSTOMER.Remove(customer);
                        kw_m01.SaveChanges();
                        jsonResult.Data = "success";
                    }
                }
            }

            return jsonResult;
        }


        // 获取某个月的订单
        public JsonResult GetOrderByMonth(int cNum, int rNum, string saler, string yM, int cmtFlag, string flag)
        {
            int y = int.Parse(yM.Substring(0, 4));
            int M = int.Parse(yM.Substring(5, 2));
            DateTime date1 = new DateTime(y, M, 1);
            DateTime date2 = date1.AddMonths(1).AddDays(-1);

            JsonResult jsonResult = new JsonResult();

            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                Debug.Assert(cmtFlag == 1 || cmtFlag == 0);
                if (flag == "1")
                {
                    IOrderedQueryable<v_corder> orders = null;
                    if (cmtFlag == 0)
                    {
                        orders = from n in kw_m01.v_corder
                                 where (n.BEGINMAN == saler)
                                 && (date1 <= n.DDATE && n.DDATE <= date2)
                                 && (n.CMTFLAG == "0")
                                 orderby n.DDATE descending
                                 select n;
                    }

                    if (cmtFlag == 1)
                    {
                        orders = from n in kw_m01.v_corder
                                 where (n.BEGINMAN == saler)
                                 && (date1 <= n.DDATE && n.DDATE <= date2)
                                 && (n.CMTFLAG == "1")
                                 orderby n.DDATE descending
                                 select n;
                    }
                    var newOrders = orders.Skip(cNum).Take(rNum);// 每次获取10条数据
                    string json = JsonConvert.SerializeObject(newOrders);
                    jsonResult.Data = json;
                }
                else
                {
                    IOrderedQueryable<M_CORDER> orders = null;
                    if (cmtFlag == 0)
                    {
                        orders = from n in kw_m01.M_CORDER
                                 where (n.BEGINMAN == saler)
                                 && (date1 <= n.DDATE && n.DDATE <= date2)
                                 && (n.CMTFLAG == "0")
                                 orderby n.DDATE descending
                                 select n;
                    }

                    if (cmtFlag == 1)
                    {
                        orders = from n in kw_m01.M_CORDER
                                 where (n.BEGINMAN == saler)
                                 && (date1 <= n.DDATE && n.DDATE <= date2)
                                 && (n.CMTFLAG == "1")
                                 orderby n.DDATE descending
                                 select n;
                    }
                    var newOrders = orders.Skip(cNum).Take(rNum);// 每次获取10条数据
                    string json = JsonConvert.SerializeObject(newOrders);
                    jsonResult.Data = json;
                }
            }

            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        }
    }
}