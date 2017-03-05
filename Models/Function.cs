using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GroupBuy1.Models
{
    public class Function
    {
    }

    public class SaveError
    {
        public SaveError( Exception ex, string a03, string a04, string a05, string a06, string a07, string a08, string a09)
        {
            err_01 err = new err_01();
            err.a02 = DateTime.Now;
            err.a08 = a08;  // 数据库
            err.a09 = a09;  // 单号
            while (ex != null)
            {
                err.a10 += ex.Message;
                ex = ex.InnerException;
            }
            kw_m01Context kw_m01 = new kw_m01Context();
            kw_m01.err_01.Add(err);
            kw_m01.SaveChanges();
        }
    }
}