using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.DepartmentSettings.Department
{
    [FwSqlTable("department")]
    public class DepartmentRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string DepartmentId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text, maxlength: 30, required: true)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "deptcode", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string DepartmentCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "divisioncode", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string DivisionCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "activityrental", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? DefaultActivityRental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitysales", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? DefaultActivitySales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitylabor", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? DefaultActivityLabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitymisc", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? DefaultActivityMiscellaneous { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityspace", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? DefaultActivityFacilities { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityvehicle", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? DefaultActivityTransportation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityrentalsale", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? DefaultActivityUsedSale { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disableeditraterental", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? DisableEditingRentalRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disableeditratesales", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? DisableEditingSalesRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disableeditratemisc", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? DisableEditingMiscellaneousRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disableeditratelabor", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? DisableEditingLaborRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disableeditrateusedsale", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? DisableEditingUsedSaleRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disableeditrateld", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? DisableEditingLossAndDamageRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesbillingmode", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string SalesBillingMode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lockwhencustomdiscount", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? LockWhenCustomDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dw", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 3)]
        public decimal? DefaultDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exportcode", modeltype: FwDataTypes.Text)]
        public string ExportCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackproject", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? EnableProjects { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}