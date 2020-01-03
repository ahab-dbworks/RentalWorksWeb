using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Modules.Exports.VendorInvoiceBatchExport
{
    public class VendorInvoiceBatchExportRequest : AppExportRequest
    {
        public string BatchId { get; set; }
    }

    public class VendorInvoiceBatchExportResponse : AppExportResponse
    {
    }

    public class VendorInvoiceBatchExportLoader : AppExportLoader  
    {

        public async Task<VendorInvoiceBatchExportLoader> DoLoad<VendorInvoiceBatchExportLoader>(VendorInvoiceBatchExportRequest request)
        {
            VendorInvoiceBatchExportLoader batchLoader;

            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                using (FwSqlCommand qry = new FwSqlCommand(conn, "webgetreceiptexportbatch", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@invoiceid", SqlDbType.Text, ParameterDirection.Input, request.BatchId);
                    AddPropertiesAsQueryColumns(qry);
                    Task<VendorInvoiceBatchExportLoader> taskBatch = qry.QueryToTypedObjectAsync<VendorInvoiceBatchExportLoader>();

                    await Task.WhenAll(new Task[] { taskBatch });

                    batchLoader = taskBatch.Result;

                }
            }

            return batchLoader;
        }
        //------------------------------------------------------------------------------------ 
    }
}
