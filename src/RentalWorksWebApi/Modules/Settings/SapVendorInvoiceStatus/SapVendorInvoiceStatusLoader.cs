using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic;
namespace RentalWorksWebApi.Modules.Settings.SapVendorInvoiceStatus
{
    [FwSqlTable("sapvendorinvoicestatusview")]
    public class SapVendorInvoiceStatusLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sapvendorinvoicestatusid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string SapVendorInvoiceStatusId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sapvendorinvoicestatus", modeltype: FwDataTypes.Text)]
        public string SapVendorInvoiceStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvoicestatus", modeltype: FwDataTypes.Text)]
        public string VendorInvoiceStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sapstatuscode", modeltype: FwDataTypes.Text)]
        public string SapStatusCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sapstatus", modeltype: FwDataTypes.Text)]
        public string SapStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequestDto request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------    } 
    }
}