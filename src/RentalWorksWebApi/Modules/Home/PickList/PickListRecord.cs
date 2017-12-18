using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
namespace WebApi.Modules.Home.PickList
{
    [FwSqlTable("picklist")]
    public class PickListRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picklistid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string PickListId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statustime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string StatusTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "multipleresources", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? MultipleResources { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DeliveryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string UsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InputByUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ModifiedByUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ModifiedDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldpickno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OldPickListNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picktype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string PickType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "summarizebymaster", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? SummarizeByInventory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "summarizeacc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? SummarizeAccessories { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "duedate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string DueDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "duetime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string DueTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string PickListNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputtime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string InputTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "completed", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Completed { get; set; }
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
                    qry.AddParameter("@uniqueid1", SqlDbType.NVarChar, ParameterDirection.Input, PickListId);
                    qry.AddParameter("@uniqueid2", SqlDbType.NVarChar, ParameterDirection.Input, "");
                    qry.AddParameter("@uniqueid3", SqlDbType.NVarChar, ParameterDirection.Input, "");
                    qry.AddParameter("@note", SqlDbType.NVarChar, ParameterDirection.Input, Note);
                    await qry.ExecuteNonQueryAsync(true);
                    saved = true;
                }
            }
            return saved;
        }
        //-------------------------------------------------------------------------------------------------------
        public override async Task<bool> DeleteAsync()
        {
            bool success = false;
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "deletepicklist", _dbConfig.QueryTimeout);
                qry.AddParameter("@picklistid", SqlDbType.NVarChar, ParameterDirection.Input, PickListId);
                await qry.ExecuteNonQueryAsync(true);
            }
            return success;
        }
        //------------------------------------------------------------------------------------    }
    }
}