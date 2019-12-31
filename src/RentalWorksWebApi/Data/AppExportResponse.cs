using FwStandard.Data;

namespace WebApi.Data
{
    public class AppExportResponse : FwExportResponse
    {
        public string BatchId { get; set; }
        public string BatchNumber { get; set; }
    }
}
