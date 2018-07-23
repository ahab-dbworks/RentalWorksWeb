using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Modules.Reports.ContractReport
{
    public class OutContractItem
    {
        public string RecordType { get; set; } = string.Empty;
        public string MasterItemId { get; set; } = string.Empty;
        public string ICode { get; set; } = string.Empty;
        public string ICodeColor { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DescriptionColor { get; set; } = string.Empty;
        public string QuantityOrdered { get; set; } = string.Empty;
        public string QuantityOut { get; set; } = string.Empty;
        public string TotalOut { get; set; } = string.Empty;
        public string ItemClass { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
    }
}
