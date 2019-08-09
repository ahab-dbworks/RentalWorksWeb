using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
namespace WebApi.Modules.Home.Project
{
    [FwSqlTable("projectview")]
    public class ProjectLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string ProjectId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "project", modeltype: FwDataTypes.Text)]
        public string Project { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanagerid", modeltype: FwDataTypes.Text)]
        public string ProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanager", modeltype: FwDataTypes.Text)]
        public string ProjectManager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prcontact", modeltype: FwDataTypes.Text)]
        public string PrimaryContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requestedby", modeltype: FwDataTypes.Text)]
        public string RequestedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesrepresentativecontactid", modeltype: FwDataTypes.Text)]
        public string OutsideSalesRepresentativeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesrepresentative", modeltype: FwDataTypes.Text)]
        public string OutsideSalesRepresentative { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectdescription", modeltype: FwDataTypes.Text)]
        public string ProjectDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean)]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "space", modeltype: FwDataTypes.Boolean)]
        public bool? Facilities { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean)]
        public bool? Labor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean)]
        public bool? Miscellaneous { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicle", modeltype: FwDataTypes.Boolean)]
        public bool? Transportation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsale", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSale { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickdate", modeltype: FwDataTypes.Date)]
        public string PickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picktime", modeltype: FwDataTypes.Text)]
        public string PickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentfrom", modeltype: FwDataTypes.Date)]
        public string EstimatedStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estfromtime", modeltype: FwDataTypes.Text)]
        public string EstimatedStartTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentto", modeltype: FwDataTypes.Date)]
        public string EstimatedStopDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "esttotime", modeltype: FwDataTypes.Text)]
        public string EstimatedStopTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectno", modeltype: FwDataTypes.Text)]
        public string ProjectNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poccprimarywhenemailbackup", modeltype: FwDataTypes.Boolean)]
        public bool? CcPrimaryApproverWhenEmailingBackupApprover { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("OfficeLocationId", "locationid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
            addFilterToSelect("DealId", "dealid", select, request);
            addFilterToSelect("CustomerId", "customerid", select, request);

            AddActiveViewFieldToSelect("WarehouseId", "warehouseid", select, request);
            AddActiveViewFieldToSelect("LocationId", "locationid", select, request);

        }
        //------------------------------------------------------------------------------------ 
    }
}
