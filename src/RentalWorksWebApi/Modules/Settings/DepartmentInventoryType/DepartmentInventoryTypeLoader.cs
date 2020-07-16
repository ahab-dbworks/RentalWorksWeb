using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.DepartmentInventoryType
{
    [FwSqlTable("departmentinventorydepartmentaccessview")]
    public class DepartmentInventoryTypeLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentaccessid", modeltype: FwDataTypes.Integer, isPrimaryKey: true, identity: true)]
        public int? Id { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isinventory", modeltype: FwDataTypes.Boolean)]
        public bool? IsInventory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "israte", modeltype: FwDataTypes.Boolean)]
        public bool? IsRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isspace", modeltype: FwDataTypes.Boolean)]
        public bool? IsFacilities { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Integer)]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("DepartmentId", "departmentid", select, request); 
            addFilterToSelect("InventoryTypeId", "inventorydepartmentid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
