using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace WebApi.Modules.Warehouse.PickList
{
    [FwSqlTable("picklistview")]
    public class PickListLoader : PickListBrowseLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picktype", modeltype: FwDataTypes.Text)]
        public string PickType { get; set; }
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
        [FwSqlDataField(column: "delivertype", modeltype: FwDataTypes.Text)]
        public string DeliverType { get; set; }
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
        [FwSqlDataField(column: "deliverdate", modeltype: FwDataTypes.Date)]
        public string DeliverDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "delivertime", modeltype: FwDataTypes.Text)]
        public string DeliverTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "note", modeltype: FwDataTypes.Text)]
        public string Note { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}