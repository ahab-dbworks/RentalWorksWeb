using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using WebApi;

namespace WebApi.Modules.Utilities.VendorInvoiceProcessBatch
{
    [FwSqlTable("chgbatchview")]
    public class VendorInvoiceProcessBatchLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chgbatchid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string BatchId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "batchtype", modeltype: FwDataTypes.Text)]
        public string BatchType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "divisioncode", modeltype: FwDataTypes.Text)]
        public string DivisionCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chgbatchno", modeltype: FwDataTypes.Text)]
        public string BatchNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chgbatchdate", modeltype: FwDataTypes.Date)]
        public string BatchDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chgbatchtime", modeltype: FwDataTypes.Text)]
        public string BatchTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chgbatchdatetime", modeltype: FwDataTypes.Date)]
        public string BatchDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exportdate", modeltype: FwDataTypes.Date)]
        public string ExportDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exported", modeltype: FwDataTypes.Boolean)]
        public bool? Exported { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reccount", modeltype: FwDataTypes.Integer)]
        public int? RecordCount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDateTime("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(batchtype='" + RwConstants.BATCH_TYPE_VENDOR_INVOICE + "')");
            addFilterToSelect("LocationId", "locationid", select, request);
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
