using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.PoSettings.VendorInvoiceApprover
{
    [FwSqlTable("vendorinvoiceapprover")]
    public class VendorInvoiceApproverRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvoiceapproverid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string VendorInvoiceApproverId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string UsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parts", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Parts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Misc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Labor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicle", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Vehicle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrent", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? SubRent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsale", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? SubSale { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repair", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Repair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submisc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? SubMisc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublabor", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? SubLabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subvehicle", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? SubVehicle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}