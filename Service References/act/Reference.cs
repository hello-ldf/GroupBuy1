﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace GroupBuy1.act {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="act.IAdd_Tiptop_cxmt410")]
    public interface IAdd_Tiptop_cxmt410 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdd_Tiptop_cxmt410/Add", ReplyAction="http://tempuri.org/IAdd_Tiptop_cxmt410/AddResponse")]
        string Add(System.Data.DataTable p_dt1, System.Data.DataTable p_dt2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdd_Tiptop_cxmt410/Add", ReplyAction="http://tempuri.org/IAdd_Tiptop_cxmt410/AddResponse")]
        System.Threading.Tasks.Task<string> AddAsync(System.Data.DataTable p_dt1, System.Data.DataTable p_dt2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdd_Tiptop_cxmt410/Del", ReplyAction="http://tempuri.org/IAdd_Tiptop_cxmt410/DelResponse")]
        string Del(System.Data.DataTable p_dt1, System.Data.DataTable p_dt2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdd_Tiptop_cxmt410/Del", ReplyAction="http://tempuri.org/IAdd_Tiptop_cxmt410/DelResponse")]
        System.Threading.Tasks.Task<string> DelAsync(System.Data.DataTable p_dt1, System.Data.DataTable p_dt2);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAdd_Tiptop_cxmt410Channel : GroupBuy1.act.IAdd_Tiptop_cxmt410, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Add_Tiptop_cxmt410Client : System.ServiceModel.ClientBase<GroupBuy1.act.IAdd_Tiptop_cxmt410>, GroupBuy1.act.IAdd_Tiptop_cxmt410 {
        
        public Add_Tiptop_cxmt410Client() {
        }
        
        public Add_Tiptop_cxmt410Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Add_Tiptop_cxmt410Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Add_Tiptop_cxmt410Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Add_Tiptop_cxmt410Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Add(System.Data.DataTable p_dt1, System.Data.DataTable p_dt2) {
            return base.Channel.Add(p_dt1, p_dt2);
        }
        
        public System.Threading.Tasks.Task<string> AddAsync(System.Data.DataTable p_dt1, System.Data.DataTable p_dt2) {
            return base.Channel.AddAsync(p_dt1, p_dt2);
        }
        
        public string Del(System.Data.DataTable p_dt1, System.Data.DataTable p_dt2) {
            return base.Channel.Del(p_dt1, p_dt2);
        }
        
        public System.Threading.Tasks.Task<string> DelAsync(System.Data.DataTable p_dt1, System.Data.DataTable p_dt2) {
            return base.Channel.DelAsync(p_dt1, p_dt2);
        }
    }
}
