using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Modules.Reports.ContractReport
{
    public class OutContractReport
    {
        public string ContractNumber { get; set; } = string.Empty;
        public string ContractDate { get; set; } = string.Empty;
        public string ContractTime { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
        public string LocationAddress1 { get; set; } = string.Empty;
        public string LocationAddress2 { get; set; } = string.Empty;
        public string LocationCity { get; set; } = string.Empty;
        public string LocationStateOrProvince { get; set; } = string.Empty;
        public string LocationZipCode { get; set; } = string.Empty;
        public string LocationCountry { get; set; } = string.Empty;
        public string LocationPhone  { get; set; } = string.Empty;
        public string LocationFax { get; set; } = string.Empty;
        public string LocationEmail { get; set; } = string.Empty;
        public string LocationUrl { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        public string OrderDescription { get; set; } = string.Empty;
        public string Deal { get; set; } = string.Empty;
        public string PoNumber { get; set; } = string.Empty;
        public string PaymentTerms { get; set; } = string.Empty;
        public string BillingCycle { get; set; } = string.Empty;
        public string BillingStartDate { get; set; } = string.Empty;
        public string BillingStopDate { get; set; } = string.Empty;
        public string DealAddress1 { get; set; } = string.Empty;
        public string DealAddress2 { get; set; } = string.Empty;
        public string DealCity { get; set; } = string.Empty;
        public string DealStateOrProvince { get; set; } = string.Empty;
        public string DealZipCode { get; set; } = string.Empty;
        public string DealCountry { get; set; } = string.Empty;
        public string DealPhone { get; set; } = string.Empty;
        public string DealFax { get; set; } = string.Empty;
        public string EstimatedStartDate { get; set; } = string.Empty;
        public string EstimatedEndDate { get; set; } = string.Empty;
        public string Agent { get; set; } = string.Empty;
        public string AgentEmail { get; set; } = string.Empty;
        public string ReceivedByPrintName { get; set; } = string.Empty;
        public string ReceivedBySignature { get; set; } = string.Empty;
        public string ReceivedByDate { get; set; } = string.Empty;

        public List<OutContractItem> RentalItems { get; set; }
        public List<OutContractItem> SalesItems { get; set; }
    }
}
