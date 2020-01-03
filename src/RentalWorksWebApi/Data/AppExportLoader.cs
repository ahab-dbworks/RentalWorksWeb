using FwStandard.Data;
using System;

namespace WebApi.Data
{
    public class AppExportLoader : FwDataReadWriteRecord
    {
        public string BatchId { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? BatchDateTime { get; set; }
    }
}
