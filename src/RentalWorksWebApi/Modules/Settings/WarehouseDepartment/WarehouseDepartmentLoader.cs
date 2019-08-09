using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.WarehouseDepartment
{
    [FwSqlTable("departmentwarehouseview")]
    public class WarehouseDepartmentLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalbarcoderangeid", modeltype: FwDataTypes.Text)]
        public string RentalBarCodeRangeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalbarcoderange", modeltype: FwDataTypes.Text)]
        public string RentalBarCodeRange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesbarcoderangeid", modeltype: FwDataTypes.Text)]
        public string SalesBarCodeRangeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesbarcoderange", modeltype: FwDataTypes.Text)]
        public string SalesBarCodeRange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Text)]
        public string OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requesttoid", modeltype: FwDataTypes.Text)]
        public string RequestToId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requesttoname", modeltype: FwDataTypes.Text)]
        public string RequestTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(inventorydepartmentid = '')");
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
}