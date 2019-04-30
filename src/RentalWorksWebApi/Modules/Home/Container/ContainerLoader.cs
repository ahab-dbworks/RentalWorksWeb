using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.Container
{
    [FwSqlTable("webcontainerview")]
    public class ContainerLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string ContainerId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string Type { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "scannablemasterid", modeltype: FwDataTypes.Text)]
        public string ScannableInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "scannablemasterno", modeltype: FwDataTypes.Text)]
        public string ScannableICode{ get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("ScannableInventoryId", "scannablemasterid", select, request);
            addFilterToSelect("ContainerId", "orderid", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
