using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
namespace WebApi.Modules.Settings.Department
{
    [FwSqlTable("departmentview")]
    public class DepartmentLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string DepartmentId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deptcode", modeltype: FwDataTypes.Text)]
        public string DepartmentCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "divisioncode", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string DivisionCode { get; set; }
        //------------------------------------------------------------------------------------
        //[FwSqlDataField(column: "chgsub", modeltype: FwDataTypes.Text)]
        //public string Chgsub { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "sets", modeltype: FwDataTypes.Boolean)]
        //public bool? Sets { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "parts", modeltype: FwDataTypes.Boolean)]
        //public bool? Parts { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        //public bool? Rental { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean)]
        //public bool? Sales { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "space", modeltype: FwDataTypes.Boolean)]
        //public bool? Space { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean)]
        //public bool? Labor { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean)]
        //public bool? Misc { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "vehicle", modeltype: FwDataTypes.Boolean)]
        //public bool? Vehicle { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "props", modeltype: FwDataTypes.Boolean)]
        //public bool? Props { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "wardrobe", modeltype: FwDataTypes.Boolean)]
        //public bool? Wardrobe { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "divisionid", modeltype: FwDataTypes.Text)]
        //public string DivisionId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "division", modeltype: FwDataTypes.Text)]
        //public string Division { get; set; }
        ////------------------------------------------------------------------------------------ 


        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityrental", modeltype: FwDataTypes.Boolean)]
        public bool? DefaultActivityRental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitysales", modeltype: FwDataTypes.Boolean)]
        public bool? DefaultActivitySales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitylabor", modeltype: FwDataTypes.Boolean)]
        public bool? DefaultActivityLabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitymisc", modeltype: FwDataTypes.Boolean)]
        public bool? DefaultActivityMiscellaneous { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityspace", modeltype: FwDataTypes.Boolean)]
        public bool? DefaultActivityFacilities { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityvehicle", modeltype: FwDataTypes.Boolean)]
        public bool? DefaultActivityTransportation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityrentalsale", modeltype: FwDataTypes.Boolean)]
        public bool? DefaultActivityUsedSale { get; set; }
        //------------------------------------------------------------------------------------ 


        [FwSqlDataField(column: "disableeditraterental", modeltype: FwDataTypes.Boolean)]
        public bool? DisableEditingRentalRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disableeditratesales", modeltype: FwDataTypes.Boolean)]
        public bool? DisableEditingSalesRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disableeditratemisc", modeltype: FwDataTypes.Boolean)]
        public bool? DisableEditingMiscellaneousRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disableeditratelabor", modeltype: FwDataTypes.Boolean)]
        public bool? DisableEditingLaborRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disableeditrateusedsale", modeltype: FwDataTypes.Boolean)]
        public bool? DisableEditingUsedSaleRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "disableeditrateld", modeltype: FwDataTypes.Boolean)]
        public bool? DisableEditingLossAndDamageRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesbillingmode", modeltype: FwDataTypes.Text)]
        public string SalesBillingRule { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lockwhencustomdiscount", modeltype: FwDataTypes.Boolean)]
        public bool? LockLineItemsWhenCustomDiscountUsed { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exportcode", modeltype: FwDataTypes.Text)]
        public string ExportCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDateTime("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
