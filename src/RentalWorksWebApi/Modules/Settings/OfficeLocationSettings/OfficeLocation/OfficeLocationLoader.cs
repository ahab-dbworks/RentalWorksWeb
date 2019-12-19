using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation
{
    [FwSqlTable("locationview")]
    public class OfficeLocationLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string LocationId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccode", modeltype: FwDataTypes.Text)]
        public string LocationCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "company", modeltype: FwDataTypes.Text)]
        public string CompanyName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text)]
        public string Email { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webaddress", modeltype: FwDataTypes.Text)]
        public string WebAddress { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text)]
        public string RateType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultpoordertypeid", modeltype: FwDataTypes.Text)]
        public string DefaultPurchasePoTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultpoordertype", modeltype: FwDataTypes.Text)]
        public string DefaultPurchasePoType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glprefix", modeltype: FwDataTypes.Text)]
        public string GlPrefix { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "glsuffix", modeltype: FwDataTypes.Text)]
        public string GlSuffix { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "useprefix", modeltype: FwDataTypes.Boolean)]
        public bool? UseNumberPrefix { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prefix", modeltype: FwDataTypes.Text)]
        public string NumberPrefix { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "usereq", modeltype: FwDataTypes.Boolean)]
        public bool? UseRequisitionNumbers { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "samequoteno", modeltype: FwDataTypes.Boolean)]
        public bool? UseSameNumberForQuoteAndOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "samebatchno", modeltype: FwDataTypes.Boolean)]
        public bool? UseSameNumberForAllExportBatches { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicesnumberedbyorder", modeltype: FwDataTypes.Boolean)]
        public bool? UserOrderNumberAndSuffixForInvoice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicesnumberedforhiatus", modeltype: FwDataTypes.Boolean)]
        public bool? UseHInHiatusInvoiceNumbers { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
