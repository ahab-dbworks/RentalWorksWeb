using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 

namespace WebApi.Modules.Settings.OrderTypeLocation
{
    [FwSqlTable("ordertypelocationview")]
    public class OrderTypeLocationLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypelocationid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderTypeLocationId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceclass", modeltype: FwDataTypes.Text)]
        public string InvoiceClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "termsconditionsid", modeltype: FwDataTypes.Text)]
        public string TermsConditionsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "termsconditions", modeltype: FwDataTypes.Text)]
        public string TermsConditions { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "coverletterid", modeltype: FwDataTypes.Text)]
        public string CoverLetterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "coverletter", modeltype: FwDataTypes.Text)]
        public string CoverLetter { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "presentationlayerid", modeltype: FwDataTypes.Text)]
        public string PresentationLayerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "presentationlayer", modeltype: FwDataTypes.Text)]
        public string PresentationLayer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("OrderTypeId", "ordertypeid", select, request); 
            addFilterToSelect("LocationId", "locationid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}