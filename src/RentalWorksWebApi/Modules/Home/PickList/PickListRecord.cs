using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Logic;

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
        public async Task<string> LoadFromSession(BrowseRequest request)
        {
            string id = "";
            string sessionId = "";
            string orderIds = "";
            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueIds.ContainsKey("SessionId"))
                {
                    sessionId = uniqueIds["SessionId"].ToString();
                }
                if (uniqueIds.ContainsKey("OrderId"))
                {
                    orderIds = uniqueIds["OrderId"].ToString();
                }
            }

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "gettmppicklistitem", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, sessionId);
                    qry.AddParameter("@orderids", SqlDbType.NVarChar, ParameterDirection.Input, orderIds);
                    AddMiscFieldToQueryAsBoolean("ItemsNotYetStaged", "@itemsnotyetstaged", qry, request);
                    AddMiscFieldToQueryAsBoolean("ItemsStaged", "@itemsstaged", qry, request);
                    AddMiscFieldToQueryAsBoolean("ItemsOut", "@itemsout", qry, request);
                    AddMiscFieldToQueryAsDate("PickDateFrom", "@pickdatefrom", qry, request);
                    AddMiscFieldToQueryAsDate("PickDateTo", "@pickdateto", qry, request);
                    AddMiscFieldToQueryAsBoolean("RentalItems", "@rentalitems", qry, request);
                    AddMiscFieldToQueryAsBoolean("SaleItems", "@saleitems", qry, request);
                    AddMiscFieldToQueryAsBoolean("VendorItems", "@vendoritems", qry, request);
                    AddMiscFieldToQueryAsBoolean("LaborItems", "@laboritems", qry, request);
                    AddMiscFieldToQueryAsString("WarehouseId", "@warehouseid", qry, request);
                    AddMiscFieldToQueryAsBoolean("CompleteKitMain", "@completekitmains", qry, request);
                    AddMiscFieldToQueryAsBoolean("CompleteKitAccessories", "@completekitaccessories", qry, request);
                    AddMiscFieldToQueryAsBoolean("CompleteKitOptions", "@completekitoptions", qry, request);
                    AddMiscFieldToQueryAsBoolean("StandAloneItems", "@standaloneitems", qry, request);
                    AddMiscFieldToQueryAsBoolean("ItemsOnOtherPickLists", "@itemsonotherpicklists", qry, request);
                    AddMiscFieldToQueryAsBoolean("ReduceQuantityAlreadyPicked", "@reduceqtyalreadypicked", qry, request);
                    AddMiscFieldToQueryAsBoolean("SummarizeByICode", "@summarizebymaster", qry, request);
                    AddMiscFieldToQueryAsBoolean("SummarizeCompleteKitItems", "@summarizeacc", qry, request);
                    AddMiscFieldToQueryAsBoolean("HonorCompleteKitItemTypes", "@honorcompletekititemtypes", qry, request);
                    qry.AddParameter("@createpicklist", SqlDbType.NVarChar, ParameterDirection.Input, 'T');
                    qry.AddParameter("@picklistid", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    id = qry.GetParameter("@picklistid").ToString().TrimEnd();
                }
            }

            return id;

        }
        //------------------------------------------------------------------------------------ 
        public async Task<bool> SaveNoteASync(string Note)
        {
            return await AppFunc.SaveNoteAsync(AppConfig, UserSession, PickListId, "", "", Note);
        }
        //-------------------------------------------------------------------------------------------------------
        public override async Task<bool> DeleteAsync()
        {
            bool success = false;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "deletepicklist", this.AppConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@picklistid", SqlDbType.NVarChar, ParameterDirection.Input, PickListId);
                await qry.ExecuteNonQueryAsync();
            }
            return success;
        }
        //------------------------------------------------------------------------------------    }
    }
}