using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.UtilitiesControls.BrowseActiveViewFields
{
    [FwSqlTable("webbrowseactiveviewfields")]
    public class BrowseActiveViewFieldsRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Integer, sqltype: "int", isPrimaryKey: true, identity: true)]
        public int? Id { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string WebUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modulename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string ModuleName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activeviewfields", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string ActiveViewFields { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("WebUserId", "webusersid", select, request);
            addFilterToSelect("OfficeLocationId", "locationid", select, request);
        }
        //------------------------------------------------------------------------------------    
        public void DeleteOthers(FwSqlConnection conn = null)
        {
            bool b = BrowseActiveViewFieldsFunc.DeleteOthers(AppConfig, UserSession, ModuleName, OfficeLocationId, WebUserId, conn).Result;
        }
        //------------------------------------------------------------------------------------    
    }
}
