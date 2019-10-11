using FwStandard.Models;

namespace WebApi.Modules.Agent.Customer
{
    //------------------------------------------------------------------------------------
    public class GetManyCustomerRequest : GetManyRequest
    {
        /// <summary>
        /// Filter Expression
        /// </summary>
        [GetManyRequestProperty(true, false)]
        public string CustomerId { get; set; } = null;
        
        /// <summary>
        /// Filter Expression
        /// </summary>
        [GetManyRequestProperty(true, true)]
        public string CustomerNumber { get; set; } = null;
        
        /// <summary>
        /// Filter Expression
        /// </summary>
        [GetManyRequestProperty(true, true)]
        public string Customer { get; set; } = null;
        
        /// <summary>
        /// Filter Expression
        /// </summary>
        [GetManyRequestProperty(true, true)]
        public string CustomerType { get; set; } = null;
        
        /// <summary>
        /// Filter Expression
        /// </summary>
        [GetManyRequestProperty(true, true)]
        public string CustomerStatus { get; set; } = null;
    }
    //------------------------------------------------------------------------------------
    public class GetManyCustomerResponse
    {
        public string CustomerId { get; set; } = null;
        public string Customer { get; set; } = null;
        public string CustomerType { get; set; } = null;
        public string CustomerStatus { get; set; } = null;
    }
    //------------------------------------------------------------------------------------
}
