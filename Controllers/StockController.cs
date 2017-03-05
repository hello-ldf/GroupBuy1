using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using GroupBuy1.Models;
using System.Data;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace GroupBuy1.Controllers
{
    public class StockController : Controller
    {
        private List<v_prodclas> prodclas = null;

        // GET: Stock
        public ViewResult Page()
        {
            return View("test");
        }
        public JsonResult Index()
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return jsonResult;
        }
        // ****************************************
        // ***          初始化数据    开始

        // 厂家编号 - 厂家分类
        public string Init_vendclas_tree(string db)
        {
            string json = "";
            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                var vendclas = from s in kw_m01.v_vendclas
                               where s.db == db
                               orderby s.cclassid
                               select s;

                node root = new node();
                root.id = "root";
                root.text = "厂家分类";
                root.clevel = "0";
                root.state = "open";
                root.children = new List<node>();
                foreach (v_vendclas vdc in vendclas)
                {
                    node temp = new node(vdc);
                    temp.state = "open";
                    root.children.Add(temp);
                }
                json = "[" + JsonConvert.SerializeObject(root) + "]";
            }
            return json;
        }
        // 厂家编号 - 厂家资料
        public string Init_vender_grid(string db, string CCLASSID)
        {
            string grid_json = "";
            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                var venders = from s in kw_m01.v_vendor
                              where s.db == db
                              && s.CCLASSID == CCLASSID
                              orderby s.CVENDID
                              select s;
                grid_json = JsonConvert.SerializeObject(venders);
            }
            return grid_json;
        }

        // 颜色编号
        public string Init_color_grid(string db)
        {
            string grid_json = "";
            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                var colors = from s in kw_m01.v_colorset
                             where s.db == db
                             orderby s.CCOLORID
                             select s;
                grid_json = JsonConvert.SerializeObject(colors);
            }
            return grid_json;
        }

        // 商品分类
        public string Init_prodclas_grid(string db)
        {
            string grid_json = "";
            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                var prodclas = from s in kw_m01.v_prodclas
                               where s.db == db
                               orderby s.classid
                               select s;
                grid_json = JsonConvert.SerializeObject(prodclas);
            }
            return grid_json;
        }

        // 厂家分类
        public string Init_vendclas_grid(string db)
        {
            string grid_json = "";
            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                var vendclas = from s in kw_m01.v_vendclas
                               where s.db == db
                               select s;
                grid_json = JsonConvert.SerializeObject(vendclas);
            }
            return grid_json;
        }
        // 采购计划仓库 - 仓库分类
        public string Init_warehouseCLas_tree(string db)
        {
            string json = "";
            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                var warehousclas = from s in kw_m01.v_warehousclas
                                   where s.db == db
                                   orderby s.cla
                                   select s;

                node root = new node();
                root.id = "root";
                root.text = "仓库分类";
                root.clevel = "0";
                root.state = "open";
                root.children = new List<node>();
                foreach (var vdc in warehousclas)
                {
                    node temp = new node(vdc);
                    temp.state = "open";
                    root.children.Add(temp);
                }
                json = "[" + JsonConvert.SerializeObject(root) + "]";
            }
            return json;
        }
        // 采购计划仓库 - 仓库资料
        public string Init_warehouse_grid(string db, string CCLASSID)
        {
            string grid_json = "";
            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                var warehous1 = from s in kw_m01.v_warehous1
                                where s.db == db
                                && s.CCLASSID == CCLASSID
                                orderby s.CWHID
                                select s;
                grid_json = JsonConvert.SerializeObject(warehous1);
            }
            return grid_json;
        }
        // 前端仓库选择
        public string Init_warehouse0_grid(string db)
        {
            string grid_json = "";
            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                var warehous1 = from s in kw_m01.v_warehous1
                                where s.db == db
                                && s.ccalcflag == "1"
                                orderby s.CWHID
                                select s;
                grid_json = JsonConvert.SerializeObject(warehous1);
            }
            return grid_json;
        }

        // ***          初始化数据   结束
        // ****************************************


        // 采购需求计算
        public string Calculate_Demand(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string f1, string db)
        {
            string json = "";

            using (zcscdb4Entities zcscdb4 = new zcscdb4Entities())
            {
                string sql = "select dbo.s_me007('1', '" + s1 + "', '" + s2 + "', '" + s3 + "', '" + s4 + "', '" + s5 + "', '" + s6 + "', '" +
                                         s7 + "', '" + s8 + "', '', '', '', '', '', '', '', '', '', '', '', '', ' ''0101'',''0102'' ', '', '', '', '" + db + "')";

                var sql1 = zcscdb4.Database.SqlQuery<string>(sql).FirstOrDefault();
                if (sql1 != null)
                {
                    var rs = zcscdb4.Database.SqlQuery<Perchase_Demand>(sql1);
                    json = JsonConvert.SerializeObject(rs);
                }
            }

            return json;
        }

        // 生成采购计划单
        public string Create_Polist(string plJson, string plcJson)
        {
            string msg = "success";

            using (kw_m01Context kw_m01 = new kw_m01Context())
            {
                var polist = JsonConvert.DeserializeObject<M_POLIST>(plJson);
                var polists = JsonConvert.DeserializeObject<List<M_POLISTC>>(plcJson);
                var db = polist.CMTMAN;

                var ccode = kw_m01.p_getmaxcode3("204", "1", db, DateTime.Now.Date).First();
                polist.CCODE = ccode;
                for (var i = 0; i < polists.Count; i++)
                {
                    polists[i].CCODE = ccode;
                }

                kw_m01.M_POLIST.Add(polist);
                kw_m01.M_POLISTC.AddRange(polists);
                kw_m01.SaveChanges();
                kw_m01.sp_ins_erp_polist(db, ccode);
            }

            return msg;
        }


        private TreeResult checkNext(node n1, node n2)
        {
            while (n1.clevel.Length < n2.clevel.Length)
            {
                if (prodclas.Count != 0)
                {
                    node n3 = new node(prodclas.First());
                    prodclas.Remove(prodclas.First());
                    TreeResult treeResult = checkNext(n2, n3);
                    n1.children.Add(treeResult.child);
                    n2 = treeResult.next;
                    if (n2 == null)
                    {
                        break;
                    }
                }
                else
                {
                    n2.state = "open";
                    n1.children.Add(n2);
                    n2 = null;
                    break;
                }
            }
            if (n1.children.Count == 0)
            {
                n1.state = "open";
            }

            TreeResult tr = new TreeResult();
            tr.child = n1;
            tr.next = n2;
            return tr;
        }


    }

    public class TreeResult
    {
        public node child { get; set; }

        public node next { get; set; }
    }

    public class node
    {
        public string state = "closed";
        private v_warehousclas vdc;

        public string id { get; set; }
        public string text { get; set; }
        public string clevel { get; set; }
        public List<node> children { get; set; }

        public node(v_prodclas prodclas)
        {
            id = prodclas.classid;
            text = prodclas.cname;
            clevel = prodclas.clevel;
            children = new List<node>();
        }

        public node(v_vendclas vendclas)
        {
            id = vendclas.cclassid;
            text = vendclas.new_name;
            clevel = vendclas.CLEVEL;
            children = new List<node>();
        }

        public node() { }

        public node(v_warehousclas vdc)
        {
            id = vdc.cla;
            text = vdc.new_name;
            clevel = vdc.CLEVEL;
            children = new List<node>();
        }
    }

}