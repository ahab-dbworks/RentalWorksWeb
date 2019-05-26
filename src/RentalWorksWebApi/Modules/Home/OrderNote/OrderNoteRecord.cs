using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Logic;

namespace WebApi.Modules.Home.OrderNote
{
    [FwSqlTable("ordernote")]
    public class OrderNoteRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordernoteid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string OrderNoteId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string OrderId { get; set; } 
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string NoteDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string UserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notesdesc", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string NotesDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billing", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Billing { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "printonorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? PrintOnOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "copytoinvoice", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CopyToInvoice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "schedulenote", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ScheduleNote { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "schedulenotedate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string ScheduleNoteDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picklist", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? PickList { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<bool> SaveNoteASync(string Note)
        {
            return await AppFunc.SaveNoteAsync(AppConfig, UserSession, OrderId, OrderNoteId, "", Note);
        }
        //-------------------------------------------------------------------------------------------------------
    }
}