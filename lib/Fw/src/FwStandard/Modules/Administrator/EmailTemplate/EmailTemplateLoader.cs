using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;


namespace FwStandard.Modules.Administrator.EmailTemplate
{
    [FwSqlTable("appemail")]
    public class EmailTemplateLoader : FwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appemailid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string AppEmailId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "filterid", modeltype: FwDataTypes.Text)]
        public string FilterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subject", modeltype: FwDataTypes.Text)]
        public string Subject { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "emailtext", modeltype: FwDataTypes.Text)]
        public string EmailText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bodyformat", modeltype: FwDataTypes.Text)]
        public string BodyFormat { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "emailtype", modeltype: FwDataTypes.Text)]
        public string EmailType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddOrderBy("Description asc");
            //addFilterToSelect("FromUserId", "fromusersid", select, request);
            //addFilterToSelect("RelatedToId", "uniqueid", select, request);

            //AddActiveViewFieldToSelect("FromUserId", "fromwebusersid", select, request);

        }
        //------------------------------------------------------------------------------------ 
    }
}
