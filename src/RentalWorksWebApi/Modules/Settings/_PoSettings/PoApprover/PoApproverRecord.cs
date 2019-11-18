using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.PoApprover
{
    [FwSqlTable("poapprover")]
    public class PoApproverRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poapproverid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string PoApproverId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string UsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "backupflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsBackup { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? HasLimit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approleid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string AppRoleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitlabor", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? LimitLabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitmisc", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? LimitMisc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitparts", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? LimitParts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitrental", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? LimitRental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitrepair", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? LimitRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitsales", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? LimitSales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitsublabor", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? LimitSubLabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitsubmisc", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? LimitSubMisc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitsubrent", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? LimitSubRent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitsubsale", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? LimitSubSale { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitsubvehicle", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? LimitSubVehicle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitvehicle", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? LimitVehicle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ProjectId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}