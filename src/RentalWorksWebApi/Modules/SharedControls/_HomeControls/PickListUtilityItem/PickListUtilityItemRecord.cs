using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.PickListUtilityItem
{
    [FwSqlTable("tmppicklistitem")]
    public class PickListUtilityItemRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        public PickListUtilityItemRecord()
        {
            BeforeSave += BeforeSavePickListUtilityItem;
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string SessionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickqty", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? PickQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stagedqty", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? StagedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outqty", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? OutQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inlocationqty", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? InlocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        public void BeforeSavePickListUtilityItem(object sender, BeforeSaveDataRecordEventArgs e)
        {
            if (PickQuantity == 0)
            {
                bool deleted = DeleteAsync().Result;
                if (deleted)
                {
                    e.PerformSave = false;
                }
            }
            else
            {
                using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "inserttemppicklistitem", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, SessionId);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                    qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, OrderItemId);
                    qry.AddParameter("@qtyordered", SqlDbType.Int, ParameterDirection.Input, QuantityOrdered);
                    qry.AddParameter("@stagedqty", SqlDbType.Int, ParameterDirection.Input, StagedQuantity);
                    qry.AddParameter("@outqty", SqlDbType.Int, ParameterDirection.Input, OutQuantity);
                    qry.AddParameter("@inlocationqty", SqlDbType.Int, ParameterDirection.Input, InlocationQuantity);
                    int i = qry.ExecuteNonQueryAsync().Result;
                }
            }

        }
        //------------------------------------------------------------------------------------ 
    }
}
