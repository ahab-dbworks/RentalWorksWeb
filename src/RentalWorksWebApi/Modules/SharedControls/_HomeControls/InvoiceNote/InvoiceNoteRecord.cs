using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.InvoiceNote
{
    [FwSqlTable("invoicenote")]
    public class InvoiceNoteRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicenoteid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string InvoiceNoteId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string NotesById { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime", required: true)]
        public string NoteDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "printoninvoice", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? PrintOnInvoice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<bool> SaveNoteASync(string Note)
        {
            return await AppFunc.SaveNoteAsync(AppConfig, UserSession, InvoiceId, InvoiceNoteId, "", Note);
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
