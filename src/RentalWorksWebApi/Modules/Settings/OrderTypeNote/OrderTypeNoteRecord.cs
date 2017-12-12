using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
using System.Data;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.OrderTypeNote
{
    [FwSqlTable("ordertypenotes")]
    public class OrderTypeNoteRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypenotesid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string OrderTypeNoteId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billing", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Billing { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "printonorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? PrintOnOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
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
                    qry.AddParameter("@uniqueid1", SqlDbType.NVarChar, ParameterDirection.Input, OrderTypeId);
                    qry.AddParameter("@uniqueid2", SqlDbType.NVarChar, ParameterDirection.Input, OrderTypeNoteId);
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