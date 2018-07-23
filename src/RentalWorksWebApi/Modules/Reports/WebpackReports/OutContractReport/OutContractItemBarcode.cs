using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Modules.Reports.ContractReport
{
    public class OutContractItemBarcode
    {
        public string RecordType { get; set; } = string.Empty;
        public string MasterItemId { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
    }
}
