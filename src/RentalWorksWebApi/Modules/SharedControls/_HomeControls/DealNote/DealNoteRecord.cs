using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.DealNote
{
    [FwSqlTable("dealnote")]
    public class DealNoteRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealnoteid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string DealNoteId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, maxlength: 8, required: true)]
        public string DealId { get; set; }
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
            return await AppFunc.SaveNoteAsync(AppConfig, UserSession, DealId, DealNoteId, "", Note);
        }
        //-------------------------------------------------------------------------------------------------------    
    }
}