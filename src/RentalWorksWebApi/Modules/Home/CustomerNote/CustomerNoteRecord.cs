using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;
using System.Data;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Home.CustomerNote
{
    [FwSqlTable("custnote")]
    public class CustomerNoteRecord : RwDataReadWriteRecord
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
            bool saved = false;
            if (Note != null)
            {
                using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "updateappnote", _dbConfig.QueryTimeout);
                    qry.AddParameter("@uniqueid1", SqlDbType.NVarChar, ParameterDirection.Input, CustomerId);
                    qry.AddParameter("@uniqueid2", SqlDbType.NVarChar, ParameterDirection.Input, CustomerNoteId);
                    qry.AddParameter("@uniqueid3", SqlDbType.NVarChar, ParameterDirection.Input, "");
                    qry.AddParameter("@note", SqlDbType.NVarChar, ParameterDirection.Input, Note);
                    await qry.ExecuteNonQueryAsync(true);
                    saved = true;
                }
            }
            return saved;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}