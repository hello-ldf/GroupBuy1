using GroupBuy1.Models;
using Newtonsoft.Json;
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
        public JsonResult stock_qry(string cmteid)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                // 错误信息
                jsonResult.Data = 4;

                // 获取匹配的结果
                var stocks = (from s in kw_m01.M_STOCK
                              where s.CMTEID == cmteid
                              select s).ToList();

                if (stocks.Count != 0)
                {   // 有结果、序列化
                    jsonResult.Data = JsonConvert.SerializeObject(stocks);
                }
            }
            return jsonResult;
        }// storck_qry - End


        // 下拉刷新、上拉加载
        public JsonResult pull_refresh(string db, int skipNum, int takeNum, string who, string yM, string flag)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            int y = int.Parse(yM.Substring(0, 4));
            int M = int.Parse(yM.Substring(5, 2));
            DateTime date1 = new DateTime(y, M, 1);
            DateTime date2 = date1.AddMonths(1).AddDays(-1);

            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                Sale_GB_pull_refresh result = new Sale_GB_pull_refresh();

                // 初始化 三个选项卡
                if (flag == "0")
                {
                    var cordersSql1 = from s in kw_m01.M_CORDER
                                      where s.CMTFLAG == "0"
                                      && s.cpathid == db
                                      && s.BEGINMAN == who
                                      && date1 <= s.DDATE && s.DDATE <= date2
                                      orderby s.DDATE descending
                                      select s;

                    var cordersSql2 = from s in kw_m01.v_corder
                                      where s.CMTFLAG == "0"
                                      && s.db == db
                                      && s.BEGINMAN == who
                                      && date1 <= s.DDATE && s.DDATE <= date2
                                      orderby s.DDATE descending
                                      select s;

                    var cordersSql3 = from s in kw_m01.v_corder
                                      where s.CMTFLAG == "1"
                                      && s.db == db
                                      && s.BEGINMAN == who
                                      && date1 <= s.DDATE && s.DDATE <= date2
                                      orderby s.DDATE descending
                                      select s;

                    result.skipNum1 = cordersSql1.Count() - 2 * takeNum;
                    result.skipNum2 = cordersSql2.Count() - 2 * takeNum;
                    result.skipNum3 = cordersSql3.Count() - 2 * takeNum;
                    result.corders1 = cordersSql1.Take(takeNum).ToList();
                    result.corders2 = cordersSql2.Take(takeNum).ToList();
                    result.corders3 = cordersSql3.Take(takeNum).ToList();
                }

                // 未提交
                if (flag == "1")
                {
                    var cordersSql1 = from s in kw_m01.M_CORDER
                                      where s.CMTFLAG == "0"
                                      && s.cpathid == db
                                      && s.BEGINMAN == who
                                      && date1 <= s.DDATE && s.DDATE <= date2
                                      orderby s.DDATE
                                      select s;

                    result.corders1 = cordersSql1.Skip(skipNum).Take(takeNum).OrderByDescending(d => d.DDATE).ToList();
                    result.skipNum1 = skipNum - takeNum;
                }

                // 已提交 * 未审核
                if (flag == "2")
                {
                    var cordersSql2 = from s in kw_m01.v_corder
                                      where s.CMTFLAG == "0"
                                      && s.db == db
                                      && s.BEGINMAN == who
                                      && date1 <= s.DDATE && s.DDATE <= date2
                                      orderby s.DDATE
                                      select s;

                    result.corders2 = cordersSql2.Skip(skipNum).Take(takeNum).OrderByDescending(d => d.DDATE).ToList();
                    result.skipNum2 = skipNum - takeNum;
                }

                // 已审核
                if (flag == "3")
                {
                    var cordersSql3 = from s in kw_m01.v_corder
                                      where s.CMTFLAG == "1"
                                      && s.db == db
                                      && s.BEGINMAN == who
                                      && date1 <= s.DDATE && s.DDATE <= date2
                                      orderby s.DDATE
                                      select s;

                    result.corders3 = cordersSql3.Skip(skipNum).Take(takeNum).OrderByDescending(d => d.DDATE).ToList();
                    result.skipNum3 = skipNum - takeNum;
                }

                if ((result.skipNum1 < 0) && (result.corders1.Count == takeNum))
                {
                    result.skipNum1 = 0;
                }
                if ((result.skipNum2 < 0) && (result.corders2.Count == takeNum))
                {
                    result.skipNum2 = 0;
                }
                if ((result.skipNum3 < 0) && (result.corders3.Count == takeNum))
                {
                    result.skipNum3 = 0;
                }

                jsonResult.Data = JsonConvert.SerializeObject(result);
            }
            return jsonResult;
        }// pull_refresh - End



    }// Ctrl - End
}