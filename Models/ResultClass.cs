using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupBuy1.Models
{
    public class ResultClass
    {
    }

    public class Sale_GB_pull_refresh
    {
        public int skipNum1 { get; set; }
        public int skipNum2 { get; set; }
        public int skipNum3 { get; set; }

        public List<M_CORDER> corders1 { get; set; }
        public List<v_corder> corders2 { get; set; }
        public List<v_corder> corders3 { get; set; }
    }

    // zcscdb4 - sp_me01 - 采购单 转 工厂销售单
    public class zcscdb4_sp_me01_21
    {
        public string a01 { get; set; }
        public string a02 { get; set; }
        public string a03 { get; set; }
        public string a04 { get; set; }
        public string a05 { get; set; }
        public string a06 { get; set; }
        public string a07 { get; set; }
        public string a08 { get; set; }
        public string a09 { get; set; }
        public string b02 { get; set; }
        public string b03 { get; set; }
        public string b04 { get; set; }
        public decimal b05 { get; set; }
        public string b06 { get; set; }
        public string b07 { get; set; }
        public de b08 { get; set; }
    }
}