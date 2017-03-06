using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupBuy1.Models
{
    public class Result
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
}