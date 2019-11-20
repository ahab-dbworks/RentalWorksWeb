using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.VendorSettings.SapVendorInvoiceStatus
{
    [FwSqlTable("sapvendorinvoicestatusview")]
    public class SapVendorInvoiceStatusLoader : AppDataLoadRecord
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
        [FwSqlDataField(column: "sapstatus", modeltype: FwDataTypes.Text)]
        public string SapStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sapstatusdisplay", modeltype: FwDataTypes.Text)]
        public string SapStatusDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------    } 
    }
}