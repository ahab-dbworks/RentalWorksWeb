using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic;
namespace RentalWorksWebApi.Modules.Home.PickList
{
    [FwSqlTable("picklistview")]
    public class PickListLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picklistid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string PickListId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickno", modeltype: FwDataTypes.Text)]
        public string PickListNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "completed", modeltype: FwDataTypes.Boolean)]
        public bool? Completed { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submitteddate", modeltype: FwDataTypes.Date)]
        public string SubmittedDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submittedtime", modeltype: FwDataTypes.Text)]
        public string SubmittedTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "submitteddatetime", modeltype: FwDataTypes.Date)]
        public string SubmittedDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderedby", modeltype: FwDataTypes.Text)]
        public string OrderedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderedbyid", modeltype: FwDataTypes.Text)]
        public string OrderedById { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requestedby", modeltype: FwDataTypes.Text)]
        public string RequestedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date)]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputtime", modeltype: FwDataTypes.Text)]
        public string InputTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdatetime", modeltype: FwDataTypes.Date)]
        public string InputDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalitemqty", modeltype: FwDataTypes.Integer)]
        public int? TotalItemQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assignedtoid", modeltype: FwDataTypes.Text)]
        public string AssignedToId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assignedto", modeltype: FwDataTypes.Text)]
        public string AssignedTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "duedate", modeltype: FwDataTypes.Date)]
        public string DueDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "duetime", modeltype: FwDataTypes.Text)]
        public string DueTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("OrderId", "orderid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}