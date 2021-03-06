﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class kw_m01Context : DbContext
    {
        public kw_m01Context()
            : base("name=kw_m01Context")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<v_userlist> v_userlist { get; set; }
        public virtual DbSet<v_colorset> v_colorset { get; set; }
        public virtual DbSet<v_unit> v_unit { get; set; }
        public virtual DbSet<gb_Store> gb_Store { get; set; }
        public virtual DbSet<v_station> v_station { get; set; }
        public virtual DbSet<v_saler> v_saler { get; set; }
        public virtual DbSet<v_custclas> v_custclas { get; set; }
        public virtual DbSet<v_SettleMode> v_SettleMode { get; set; }
        public virtual DbSet<v_singnstylist> v_singnstylist { get; set; }
        public virtual DbSet<v_warehous> v_warehous { get; set; }
        public virtual DbSet<v_payment> v_payment { get; set; }
        public virtual DbSet<v_attrItem> v_attrItem { get; set; }
        public virtual DbSet<M_CORDER> M_CORDER { get; set; }
        public virtual DbSet<M_CORDERC> M_CORDERC { get; set; }
        public virtual DbSet<v_customer> v_customer { get; set; }
        public virtual DbSet<M_CUSTOMER> M_CUSTOMER { get; set; }
        public virtual DbSet<v_product> v_product { get; set; }
        public virtual DbSet<v_accountinfo> v_accountinfo { get; set; }
        public virtual DbSet<v_prodcolor> v_prodcolor { get; set; }
        public virtual DbSet<vq_m_corderc> vq_m_corderc { get; set; }
        public virtual DbSet<mao_file> mao_file { get; set; }
        public virtual DbSet<map_file> map_file { get; set; }
        public virtual DbSet<vq_m_corder> vq_m_corder { get; set; }
        public virtual DbSet<vq_m_customer> vq_m_customer { get; set; }
        public virtual DbSet<M_StoreChange> M_StoreChange { get; set; }
        public virtual DbSet<v_prodclas> v_prodclas { get; set; }
        public virtual DbSet<v_product1> v_product1 { get; set; }
        public virtual DbSet<v_vendclas> v_vendclas { get; set; }
        public virtual DbSet<v_vendor> v_vendor { get; set; }
        public virtual DbSet<v_warehous1> v_warehous1 { get; set; }
        public virtual DbSet<v_warehousclas> v_warehousclas { get; set; }
        public virtual DbSet<M_POLIST> M_POLIST { get; set; }
        public virtual DbSet<M_POLISTC> M_POLISTC { get; set; }
        public virtual DbSet<vq_stockc> vq_stockc { get; set; }
        public virtual DbSet<err_01> err_01 { get; set; }
        public virtual DbSet<v_corder> v_corder { get; set; }
        public virtual DbSet<v_corderc> v_corderc { get; set; }
        public virtual DbSet<M_STOCK> M_STOCK { get; set; }
        public virtual DbSet<v_poman> v_poman { get; set; }
        public virtual DbSet<v_vorder> v_vorder { get; set; }
        public virtual DbSet<v_vorderc> v_vorderc { get; set; }
    
        public virtual ObjectResult<string> p_getmaxcode3(string as_code, string as_flag, string as_sysname, Nullable<System.DateTime> adt_workdate)
        {
            var as_codeParameter = as_code != null ?
                new ObjectParameter("as_code", as_code) :
                new ObjectParameter("as_code", typeof(string));
    
            var as_flagParameter = as_flag != null ?
                new ObjectParameter("as_flag", as_flag) :
                new ObjectParameter("as_flag", typeof(string));
    
            var as_sysnameParameter = as_sysname != null ?
                new ObjectParameter("as_sysname", as_sysname) :
                new ObjectParameter("as_sysname", typeof(string));
    
            var adt_workdateParameter = adt_workdate.HasValue ?
                new ObjectParameter("adt_workdate", adt_workdate) :
                new ObjectParameter("adt_workdate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("p_getmaxcode3", as_codeParameter, as_flagParameter, as_sysnameParameter, adt_workdateParameter);
        }
    
        public virtual ObjectResult<string> p_cre_custid_no(string g_db, string g_area, string g_phone)
        {
            var g_dbParameter = g_db != null ?
                new ObjectParameter("g_db", g_db) :
                new ObjectParameter("g_db", typeof(string));
    
            var g_areaParameter = g_area != null ?
                new ObjectParameter("g_area", g_area) :
                new ObjectParameter("g_area", typeof(string));
    
            var g_phoneParameter = g_phone != null ?
                new ObjectParameter("g_phone", g_phone) :
                new ObjectParameter("g_phone", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("p_cre_custid_no", g_dbParameter, g_areaParameter, g_phoneParameter);
        }
    
        public virtual int p_ins_custid_no(string g_db, string g_ccustid)
        {
            var g_dbParameter = g_db != null ?
                new ObjectParameter("g_db", g_db) :
                new ObjectParameter("g_db", typeof(string));
    
            var g_ccustidParameter = g_ccustid != null ?
                new ObjectParameter("g_ccustid", g_ccustid) :
                new ObjectParameter("g_ccustid", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("p_ins_custid_no", g_dbParameter, g_ccustidParameter);
        }
    
        public virtual int sp_ins_erp_corder(string g_db, string g_code)
        {
            var g_dbParameter = g_db != null ?
                new ObjectParameter("g_db", g_db) :
                new ObjectParameter("g_db", typeof(string));
    
            var g_codeParameter = g_code != null ?
                new ObjectParameter("g_code", g_code) :
                new ObjectParameter("g_code", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ins_erp_corder", g_dbParameter, g_codeParameter);
        }
    
        public virtual int sp_del_erp_corder(string g_db, string g_code)
        {
            var g_dbParameter = g_db != null ?
                new ObjectParameter("g_db", g_db) :
                new ObjectParameter("g_db", typeof(string));
    
            var g_codeParameter = g_code != null ?
                new ObjectParameter("g_code", g_code) :
                new ObjectParameter("g_code", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_del_erp_corder", g_dbParameter, g_codeParameter);
        }
    
        public virtual int sp_ins_erp_polist(string g_db, string g_code)
        {
            var g_dbParameter = g_db != null ?
                new ObjectParameter("g_db", g_db) :
                new ObjectParameter("g_db", typeof(string));
    
            var g_codeParameter = g_code != null ?
                new ObjectParameter("g_code", g_code) :
                new ObjectParameter("g_code", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ins_erp_polist", g_dbParameter, g_codeParameter);
        }
    
        public virtual int sp_erp_corder(Nullable<int> g_flag, string g_db, string g_code, string g_str1, string g_str2, string g_str3, string g_str4, string g_str5, string g_filter1, string g_filter2, string g_filter3)
        {
            var g_flagParameter = g_flag.HasValue ?
                new ObjectParameter("g_flag", g_flag) :
                new ObjectParameter("g_flag", typeof(int));
    
            var g_dbParameter = g_db != null ?
                new ObjectParameter("g_db", g_db) :
                new ObjectParameter("g_db", typeof(string));
    
            var g_codeParameter = g_code != null ?
                new ObjectParameter("g_code", g_code) :
                new ObjectParameter("g_code", typeof(string));
    
            var g_str1Parameter = g_str1 != null ?
                new ObjectParameter("g_str1", g_str1) :
                new ObjectParameter("g_str1", typeof(string));
    
            var g_str2Parameter = g_str2 != null ?
                new ObjectParameter("g_str2", g_str2) :
                new ObjectParameter("g_str2", typeof(string));
    
            var g_str3Parameter = g_str3 != null ?
                new ObjectParameter("g_str3", g_str3) :
                new ObjectParameter("g_str3", typeof(string));
    
            var g_str4Parameter = g_str4 != null ?
                new ObjectParameter("g_str4", g_str4) :
                new ObjectParameter("g_str4", typeof(string));
    
            var g_str5Parameter = g_str5 != null ?
                new ObjectParameter("g_str5", g_str5) :
                new ObjectParameter("g_str5", typeof(string));
    
            var g_filter1Parameter = g_filter1 != null ?
                new ObjectParameter("g_filter1", g_filter1) :
                new ObjectParameter("g_filter1", typeof(string));
    
            var g_filter2Parameter = g_filter2 != null ?
                new ObjectParameter("g_filter2", g_filter2) :
                new ObjectParameter("g_filter2", typeof(string));
    
            var g_filter3Parameter = g_filter3 != null ?
                new ObjectParameter("g_filter3", g_filter3) :
                new ObjectParameter("g_filter3", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_erp_corder", g_flagParameter, g_dbParameter, g_codeParameter, g_str1Parameter, g_str2Parameter, g_str3Parameter, g_str4Parameter, g_str5Parameter, g_filter1Parameter, g_filter2Parameter, g_filter3Parameter);
        }
    
        public virtual int sp_audit_erp_corder(string g_code, string g_db, string g_cmtMan)
        {
            var g_codeParameter = g_code != null ?
                new ObjectParameter("g_code", g_code) :
                new ObjectParameter("g_code", typeof(string));
    
            var g_dbParameter = g_db != null ?
                new ObjectParameter("g_db", g_db) :
                new ObjectParameter("g_db", typeof(string));
    
            var g_cmtManParameter = g_cmtMan != null ?
                new ObjectParameter("g_cmtMan", g_cmtMan) :
                new ObjectParameter("g_cmtMan", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_audit_erp_corder", g_codeParameter, g_dbParameter, g_cmtManParameter);
        }
    }
}
