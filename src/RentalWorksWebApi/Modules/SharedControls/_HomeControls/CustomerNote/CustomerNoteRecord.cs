using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.CustomerNote
{
    [FwSqlTable("custnote")]
    public class CustomerNoteRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custnoteid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string CustomerNoteId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text, maxlength: 8, required: true)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notedate", modeltype: FwDataTypes.Date)]
        public string NoteDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notesbyid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string NotesById { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notesdesc", modeltype: FwDataTypes.Text, maxlength: 30)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notify", modeltype: FwDataTypes.Boolean)]
        public bool? Notify { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        public async Task<bool> SaveNoteASync(string Note)
        {
            return await AppFunc.SaveNoteAsync(AppConfig, UserSession, CustomerId, CustomerNoteId, "", Note);
        }
        //-------------------------------------------------------------------------------------------------------
    }
}