using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupBuy1.Models
{
    // 产品+颜色中文+库存+单位
    public class ProductQry
    {
        public v_product product { get; set; }
        public object colors { get; set; }
        public string cunit { get; set; }
        public decimal inventory { get; set; }
        public v_attrItem attrItem { get; set; }
    }

    // 单头+单身+客户
    public class vqOrderQry
    {
        public vq_m_corder corder { get; set; }
        public IQueryable<vq_m_corderc> cordercs { get; set; }
        public vq_m_customer customer { get; set; }

    }

    // 单头+单身+客户
    public class ErpOrderQry
    {
        public v_corder corder { get; set; }
        public IQueryable<v_corderc> cordercs { get; set; }
        public v_customer customer { get; set; }

    }

    // 单头+单身+客户
    public class OrderQry
    {
        public M_CORDER corder { get; set; }
        public IQueryable<vq_m_corderc> cordercs { get; set; }
        public M_CUSTOMER customer { get; set; }

    }

    // 采购需求计算返回值
    public class Perchase_Demand
    {
        public string cmteid { get; set; }
        public string ccname { get; set; }
        public string ccolorid { get; set; }
        public string cname { get; set; }
        public string cspec { get; set; }
        public string cunitid { get; set; }
        public string cnamea { get; set; }
        public decimal dcost { get; set; }
        public decimal dtotalqty { get; set; }
        public decimal dnetqty { get; set; }
        public decimal dorderqty { get; set; }
        public decimal dplanqty { get; set; }
        public decimal donwayqty { get; set; }
        public decimal dstkqty { get; set; }
        public decimal dsafeqty { get; set; }
        public decimal dreqnoinqty { get; set; }
        public decimal dreqnooutqty { get; set; }
        public decimal dsplitqty { get; set; }
        public decimal dcoalitionqty { get; set; }
        public string cwhid { get; set; }
        public string cwhname { get; set; }
        public string cattrid { get; set; }
        public string cattrname { get; set; }
        public decimal dfactorderqty { get; set; }
        public string cvendid { get; set; }
        public string cvendname { get; set; }
        public decimal dnoship { get; set; }
    }
}