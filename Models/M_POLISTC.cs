//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace GroupBuy1.Models
{
    using System;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    
    public partial class M_POLISTC
    {
        public string CCODE { get; set; }
        public int IORDER { get; set; }
        public string CMTEID { get; set; }
        public Nullable<System.DateTime> DPLANDT { get; set; }
        public string CVENDID { get; set; }
        public Nullable<decimal> DQTY { get; set; }
        public Nullable<decimal> DPRICE { get; set; }
        public Nullable<decimal> DPOQTY { get; set; }
        public string CENDFLAG { get; set; }
        public string CDESC { get; set; }
        public Nullable<decimal> DCYPRICE { get; set; }
        public Nullable<decimal> DCYCOST { get; set; }
        public Nullable<decimal> DUTQTY { get; set; }
        public byte INETFLAG { get; set; }
        public string ccolorid { get; set; }
        public Nullable<System.DateTime> ddedt { get; set; }
        public Nullable<decimal> dagio { get; set; }
        public string corderid { get; set; }
        public Nullable<int> iporder { get; set; }
        public string cpodesc { get; set; }
        public Nullable<System.DateTime> plango_date { get; set; }
        public string cplanwhid { get; set; }
        public Nullable<decimal> dpack_qty { get; set; }
        public string cattrid { get; set; }
        public Nullable<decimal> dstandardcost { get; set; }
    
        public virtual M_POLIST M_POLIST { get; set; }
    }
}
