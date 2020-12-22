﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VistekPaymentCaptureService
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", ConfigurationName="VistekPaymentCaptureService.PaymentCapture_Port")]
    public interface PaymentCapture_Port
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/codeunit/PaymentCapture:ProcessCardPayment", ReplyAction="*")]
        System.Threading.Tasks.Task<VistekPaymentCaptureService.ProcessCardPayment_Result> ProcessCardPaymentAsync(VistekPaymentCaptureService.ProcessCardPayment request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/codeunit/PaymentCapture:GetApiKey", ReplyAction="*")]
        System.Threading.Tasks.Task<VistekPaymentCaptureService.GetApiKey_Result> GetApiKeyAsync(VistekPaymentCaptureService.GetApiKey request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ProcessCardPayment
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ProcessCardPayment", Namespace="urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order=0)]
        public VistekPaymentCaptureService.ProcessCardPaymentBody Body;
        
        public ProcessCardPayment()
        {
        }
        
        public ProcessCardPayment(VistekPaymentCaptureService.ProcessCardPaymentBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/PaymentCapture")]
    public partial class ProcessCardPaymentBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string pINPadNo;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int transactionTypeOpt;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string amount;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string docRefNo;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string storeCode;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string salespersonCode;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string billToCustomerNo;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string pPaymentRefNo;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public string pCardType;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=9)]
        public string pCardNo;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=10)]
        public string pAuthCode;
        
        public ProcessCardPaymentBody()
        {
        }
        
        public ProcessCardPaymentBody(string pINPadNo, int transactionTypeOpt, string amount, string docRefNo, string storeCode, string salespersonCode, string billToCustomerNo, string pPaymentRefNo, string pCardType, string pCardNo, string pAuthCode)
        {
            this.pINPadNo = pINPadNo;
            this.transactionTypeOpt = transactionTypeOpt;
            this.amount = amount;
            this.docRefNo = docRefNo;
            this.storeCode = storeCode;
            this.salespersonCode = salespersonCode;
            this.billToCustomerNo = billToCustomerNo;
            this.pPaymentRefNo = pPaymentRefNo;
            this.pCardType = pCardType;
            this.pCardNo = pCardNo;
            this.pAuthCode = pAuthCode;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ProcessCardPayment_Result
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ProcessCardPayment_Result", Namespace="urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order=0)]
        public VistekPaymentCaptureService.ProcessCardPayment_ResultBody Body;
        
        public ProcessCardPayment_Result()
        {
        }
        
        public ProcessCardPayment_Result(VistekPaymentCaptureService.ProcessCardPayment_ResultBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/PaymentCapture")]
    public partial class ProcessCardPayment_ResultBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string return_value;
        
        public ProcessCardPayment_ResultBody()
        {
        }
        
        public ProcessCardPayment_ResultBody(string return_value)
        {
            this.return_value = return_value;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetApiKey
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetApiKey", Namespace="urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order=0)]
        public VistekPaymentCaptureService.GetApiKeyBody Body;
        
        public GetApiKey()
        {
        }
        
        public GetApiKey(VistekPaymentCaptureService.GetApiKeyBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GetApiKeyBody
    {
        
        public GetApiKeyBody()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetApiKey_Result
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetApiKey_Result", Namespace="urn:microsoft-dynamics-schemas/codeunit/PaymentCapture", Order=0)]
        public VistekPaymentCaptureService.GetApiKey_ResultBody Body;
        
        public GetApiKey_Result()
        {
        }
        
        public GetApiKey_Result(VistekPaymentCaptureService.GetApiKey_ResultBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/PaymentCapture")]
    public partial class GetApiKey_ResultBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string return_value;
        
        public GetApiKey_ResultBody()
        {
        }
        
        public GetApiKey_ResultBody(string return_value)
        {
            this.return_value = return_value;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface PaymentCapture_PortChannel : VistekPaymentCaptureService.PaymentCapture_Port, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class PaymentCapture_PortClient : System.ServiceModel.ClientBase<VistekPaymentCaptureService.PaymentCapture_Port>, VistekPaymentCaptureService.PaymentCapture_Port
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public PaymentCapture_PortClient() : 
                base(PaymentCapture_PortClient.GetDefaultBinding(), PaymentCapture_PortClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.PaymentCapture_Port.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PaymentCapture_PortClient(EndpointConfiguration endpointConfiguration) : 
                base(PaymentCapture_PortClient.GetBindingForEndpoint(endpointConfiguration), PaymentCapture_PortClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PaymentCapture_PortClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(PaymentCapture_PortClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PaymentCapture_PortClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(PaymentCapture_PortClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PaymentCapture_PortClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<VistekPaymentCaptureService.ProcessCardPayment_Result> ProcessCardPaymentAsync(VistekPaymentCaptureService.ProcessCardPayment request)
        {
            return base.Channel.ProcessCardPaymentAsync(request);
        }
        
        public System.Threading.Tasks.Task<VistekPaymentCaptureService.GetApiKey_Result> GetApiKeyAsync(VistekPaymentCaptureService.GetApiKey request)
        {
            return base.Channel.GetApiKeyAsync(request);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.PaymentCapture_Port))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.PaymentCapture_Port))
            {
                return new System.ServiceModel.EndpointAddress("https://10.1.8.103:8047/TEST_NAV80/WS/Vistek Live/Codeunit/PaymentCapture");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return PaymentCapture_PortClient.GetBindingForEndpoint(EndpointConfiguration.PaymentCapture_Port);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return PaymentCapture_PortClient.GetEndpointAddress(EndpointConfiguration.PaymentCapture_Port);
        }
        
        public enum EndpointConfiguration
        {
            
            PaymentCapture_Port,
        }
    }
}