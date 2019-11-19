using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.PoSettings.VendorInvoiceApprover
{
    [FwSqlTable("vendorinvoiceapproverview")]
    public class VendorInvoiceApproverLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvoiceapproverid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string VendorInvoiceApproverId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text)]
        public string UsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "username", modeltype: FwDataTypes.Text)]
        public string Username { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean)]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parts", modeltype: FwDataTypes.Boolean)]
        public bool? Parts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean)]
        public bool? Misc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean)]
        public bool? Labor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicle", modeltype: FwDataTypes.Boolean)]
        public bool? Vehicle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subrent", modeltype: FwDataTypes.Boolean)]
        public bool? SubRent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subsale", modeltype: FwDataTypes.Boolean)]
        public bool? SubSale { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repair", modeltype: FwDataTypes.Boolean)]
        public bool? Repair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submisc", modeltype: FwDataTypes.Boolean)]
        public bool? SubMisc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sublabor", modeltype: FwDataTypes.Boolean)]
        public bool? SubLabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subvehicle", modeltype: FwDataTypes.Boolean)]
        public bool? SubVehicle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}