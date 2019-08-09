using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Home.OrderNote
{
    [FwSqlTable("ordernoteview")]
    public class OrderNoteLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordernoteid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderNoteId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; } 
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notedate", modeltype: FwDataTypes.Date)]
        public string NoteDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text)]
        public string UserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notesdesc", modeltype: FwDataTypes.Text)]
        public string NotesDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billing", modeltype: FwDataTypes.Boolean)]
        public bool? Billing { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "printonorder", modeltype: FwDataTypes.Boolean)]
        public bool? PrintOnOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "copytoinvoice", modeltype: FwDataTypes.Boolean)]
        public bool? CopyToInvoice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "schedulenote", modeltype: FwDataTypes.Boolean)]
        public bool? ScheduleNote { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "schedulenotedate", modeltype: FwDataTypes.Date)]
        public string ScheduleNoteDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picklist", modeltype: FwDataTypes.Boolean)]
        public bool? PickList { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "name", modeltype: FwDataTypes.Text)]
        public string UserName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
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