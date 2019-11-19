using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.HomeControls.InventoryVendor
{
    [FwSqlTable("mastervendorgridview")]
    public class InventoryVendorLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mastervendorid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventoryVendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "person", modeltype: FwDataTypes.Text)]
        public string ContactName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "officephone", modeltype: FwDataTypes.Text)]
        public string ContactOfficePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text)]
        public string ContactEmail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("InventoryId", "masterid", select, request); 
            addFilterToSelect("VendorId", "vendorid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}