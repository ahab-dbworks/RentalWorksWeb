using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
namespace WebApi.Modules.Settings.OrderTypeDateType
{
    [FwSqlTable("ordertypeactivityview")]
    public class OrderTypeDateTypeLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypedatetypeid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderTypeDateTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytypeid", modeltype: FwDataTypes.Integer)]
        public int? ActivityTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytype", modeltype: FwDataTypes.Text)]
        public string ActivityType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rename", modeltype: FwDataTypes.Text)]
        public string Rename { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "descriptiondisplay", modeltype: FwDataTypes.Text)]
        public string DescriptionDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "systemtype", modeltype: FwDataTypes.Boolean)]
        public bool? SystemType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enabled", modeltype: FwDataTypes.Boolean)]
        public bool? Enabled { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "milestone", modeltype: FwDataTypes.Boolean)]
        //public bool? Milestone { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "productionactivity", modeltype: FwDataTypes.Boolean)]
        //public bool? ProductionActivity { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "requiredquote", modeltype: FwDataTypes.Boolean)]
        //public bool? RequiredOnQuote { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "requiredorder", modeltype: FwDataTypes.Boolean)]
        //public bool? RequiredOnOrder { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.OleToHtmlColor)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "textcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string TextColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("OrderTypeId", "ordertypeid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}