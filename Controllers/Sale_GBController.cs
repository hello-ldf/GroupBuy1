using GroupBuy1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupBuy1.Controllers
{
    /*
     * 团购专用 Ctrl Sale_GB
     */
    public class Sale_GBController : Controller
    {
        // 查询库存
        public JsonResult storck_qry(string cmteid)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                jsonResult.Data = "无该产品库存信息。";

                var store = kw_m01.gb_Store.Find(cmteid);
                if (store != null)
                {
                    var inventory = store.Inventory;
                    jsonResult.Data = inventory;
                }
            }
            return jsonResult;
        }// end

    }
}