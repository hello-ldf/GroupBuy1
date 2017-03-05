using GroupBuy1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GroupBuy1.Controllers
{
    public class GlobalController : Controller
    {
        // 获取7大区 JSON
        public JsonResult GetAreas()
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                var areas = from c in kw_m01.v_accountinfo
                            select new { value = c.cdbname, text = c.cname };

                string areasJson = JsonConvert.SerializeObject(areas);
                jsonResult.Data = areasJson;
            }

            return jsonResult;
        }

        public JsonResult GetGlobalParm(string db)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            jsonResult.Data = "NonData";
            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                var stations = from s in kw_m01.v_station
                               where s.a02 == db
                               select new { value = s.cstation, text = s.cname };
                var salers = from s in kw_m01.v_saler
                             where s.db == db
                             orderby s.CNAME
                             select new { value = s.CEMPID, text = s.CNAME + s.CEMPID };
                var custclas = from c in kw_m01.v_custclas
                               where c.db == db
                               select new { value = c.CCLASSID, text = c.CNAME };
                var singnstylists = from c in kw_m01.v_singnstylist
                                    where c.db == db
                                    select new { value = c.csignstylistid, text = c.cname };
                var payments = from c in kw_m01.v_payment
                               where c.db == db
                               select new { value = c.CPYID, text = c.CNAME };
                var settleModes = from c in kw_m01.v_SettleMode
                                  where c.db == db
                                  select new { value = c.FStlNo, text = c.FStlName };
                var stores = from c in kw_m01.v_warehous
                             where c.db == db
                             select new { value = c.CWHID, text = c.CNAME };

                object global = new { stations, salers, custclas, singnstylists, payments, settleModes, stores };

                string globalJson = JsonConvert.SerializeObject(global);
                jsonResult.Data = globalJson;
            }

            return jsonResult;
        }

        // 查询库存
        public JsonResult QryInventory(string pdNo, string db, string flag)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                jsonResult.Data = "无该产品库存信息。";
                if (flag == "1")
                {
                    var stockcs = from s in kw_m01.vq_stockc
                                  where s.db == db
                                  && s.cmteid == pdNo
                                  select s;
                    if (stockcs.Count() != 0)
                    {
                        jsonResult.Data = JsonConvert.SerializeObject(stockcs);
                    }
                }
                else
                {
                    var store = kw_m01.gb_Store.Find(pdNo);
                    if (store != null)
                    {
                        var inventory = store.Inventory;
                        jsonResult.Data = inventory;
                    }
                }

            }
            return jsonResult;
        }

        // 通过型号返回产品
        public JsonResult GetProduct(string db, string pNo, string flag)
        {
            var jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (String.IsNullOrEmpty(pNo))
            {// 
                return jsonResult;
            }
            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                v_product product = kw_m01.v_product.Find(db, pNo);
                if (product == null)
                {
                    return jsonResult;
                }

                var attrItem = kw_m01.v_attrItem.Find(db, product.cattrid);

                var colors = from s in kw_m01.v_prodcolor
                             where (s.db == db) && (s.CMTEID == pNo)
                             select new { text = s.cname, value = s.CCOLORID };

                decimal inventory = -99999;

                var store = kw_m01.gb_Store.Find(pNo);
                if (store != null)
                {
                    inventory = store.Inventory;
                }

                ProductQry productQry = new ProductQry();
                productQry.product = product;
                productQry.attrItem = attrItem;
                productQry.colors = colors;
                productQry.cunit = kw_m01.v_unit.Find(db, product.CUNITID).CNAME;
                productQry.inventory = inventory;

                string json = JsonConvert.SerializeObject(productQry);
                jsonResult.Data = json;
            }

            return jsonResult;
        }

        // 获取订单
        public JsonResult GetOrder(string db, string ccode, string flag)
        {
            Debug.Assert((db != null) && (db != ""), "db为空");
            Debug.Assert((ccode != null) && (ccode != ""), "ccode为空");

            var jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                if (flag == "1")
                {
                    var erp_corder = kw_m01.v_corder.Find(db, ccode);
                    if (erp_corder == null)
                    {
                        jsonResult.Data = 3;
                        return jsonResult;
                    }
                    var erp_corders = from s in kw_m01.v_corderc
                                      where s.db == db
                                      && s.CCODE == ccode
                                      orderby s.IORDER
                                      select s;
                    var erp_customer = kw_m01.v_customer.Find(db, erp_corder.CCUSTID);
                    ErpOrderQry orderQry = new ErpOrderQry();
                    orderQry.customer = erp_customer;
                    orderQry.corder = erp_corder;
                    if (erp_corders.Count() != 0)
                    {
                        orderQry.cordercs = erp_corders;
                    }
                    jsonResult.Data = JsonConvert.SerializeObject(orderQry);
                }
                else
                {
                    var corder = kw_m01.vq_m_corder.Find(ccode, db);
                    if (corder == null)
                    {
                        jsonResult.Data = 3;
                        return jsonResult;
                    }
                    var cordercs = from c in kw_m01.vq_m_corderc
                                   where (c.cquotid == db) && (c.CCODE == ccode)
                                   orderby c.IORDER
                                   select c;
                    var customer = kw_m01.vq_m_customer.Find(corder.CCUSTID, db);
                    Debug.Assert((customer != null), "customer为空");

                    vqOrderQry orderQry = new vqOrderQry();
                    orderQry.customer = customer;
                    orderQry.corder = corder;
                    if (cordercs.Count() != 0)
                    {
                        orderQry.cordercs = cordercs;
                    }
                    jsonResult.Data = JsonConvert.SerializeObject(orderQry);
                }
                
            }
            return jsonResult;
        }
    }
}