using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using System.Data;
using WebApi.Data;
using WebApi.Logic;
using WebApi.Modules.HomeControls.InventoryAvailability;

namespace WebApi.Modules.HomeControls.InventorySearch
{
    [FwSqlTable("tmpsearchsession")]
    public class InventorySearchRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        public InventorySearchRecord()
        {
            BeforeSave += OnBeforeSaveInventorySearch;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string SessionId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string InventoryId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string WarehouseId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "grandparentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string GrandParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Integer, sqltype: "numeric", precision: 12, scale: 2)]
        public float? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        // this property is only used to hold the request value for the availability check in overridden save method below
        public DateTime? FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        // this property is only used to hold the request value for the availability check in overridden save method below
        public DateTime? ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        // this property is only used to hold the return value in the overridden save method below
        public float? QuantityAvailable { get; set; }
        //------------------------------------------------------------------------------------ 
        // this property is only used to hold the return value in the overridden save method below
        public DateTime? ConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        // this property is only used to hold the return value in the overridden save method below
        public string AvailabilityState { get; set; }
        //------------------------------------------------------------------------------------ 
        // this property is only used to hold the return value in the overridden save method below
        public float? QuantityAvailableAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        // this property is only used to hold the return value in the overridden save method below
        public DateTime? ConflictDateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        // this property is only used to hold the return value in the overridden save method below
        public string AvailabilityStateAllWarehouses { get; set; }
        //------------------------------------------------------------------------------------ 
        // this property is only used to hold the return value in the overridden save method below
        public decimal? TotalQuantityInSession { get; set; }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveInventorySearch(object sender, BeforeSaveDataRecordEventArgs e)
        {
            if (Quantity == null)
            {
                Quantity = 0;
            }

            if (Quantity < 0)
            {
                throw new System.Exception("Quantity cannot be negative.");
            }
            else
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    TSpStatusResponse response = new TSpStatusResponse();

                    FwSqlCommand qry = new FwSqlCommand(conn, "savetmpsearchsession", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, SessionId);
                    qry.AddParameter("@parentid", SqlDbType.NVarChar, ParameterDirection.Input, ParentId);
                    qry.AddParameter("@grandparentid", SqlDbType.NVarChar, ParameterDirection.Input, GrandParentId);
                    qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, InventoryId);
                    qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, WarehouseId);
                    qry.AddParameter("@qty", SqlDbType.Float, ParameterDirection.Input, Quantity);
                    qry.AddParameter("@totalqtyinsession", SqlDbType.Float, ParameterDirection.Output);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    int i = qry.ExecuteNonQueryAsync().Result;
                    TotalQuantityInSession = qry.GetParameter("@totalqtyinsession").ToDecimal();
                    response.status = qry.GetParameter("@status").ToInt32();
                    response.success = (response.status == 0);
                    response.msg = qry.GetParameter("@msg").ToString();

                    if (!response.success)
                    {
                        throw new System.Exception("Cannot save search quantity: " + response.msg);
                    }


                    QuantityAvailable = 0;
                    ConflictDate = null;
                    AvailabilityState = RwConstants.AVAILABILITY_STATE_STALE;

                    QuantityAvailableAllWarehouses = 0;
                    ConflictDateAllWarehouses = null;
                    AvailabilityStateAllWarehouses = RwConstants.AVAILABILITY_STATE_STALE;

                    DateTime fromDateTime = DateTime.MinValue;
                    DateTime toDateTime = DateTime.MinValue;

                    if ((FromDate != null) && (FromDate > DateTime.MinValue))
                    {
                        fromDateTime = FromDate.GetValueOrDefault(DateTime.MinValue);
                    }
                    if ((ToDate != null) && (ToDate > DateTime.MinValue))
                    {
                        toDateTime = ToDate.GetValueOrDefault(DateTime.MinValue);
                    }


                    TInventoryWarehouseAvailabilityRequestItems availRequestItems = new TInventoryWarehouseAvailabilityRequestItems();
                    availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(InventoryId, WarehouseId, fromDateTime, toDateTime));
                    availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(InventoryId, RwConstants.WAREHOUSEID_ALL, fromDateTime, toDateTime));

                    TAvailabilityCache availCache = InventoryAvailabilityFunc.GetAvailability(AppConfig, UserSession, availRequestItems, refreshIfNeeded: true, forceRefresh: false).Result;
                    TInventoryWarehouseAvailability availData = null;
                    if (availCache.TryGetValue(new TInventoryWarehouseAvailabilityKey(InventoryId, WarehouseId), out availData))
                    {
                        TInventoryWarehouseAvailabilityMinimum minAvail = availData.GetMinimumAvailableQuantity(fromDateTime, toDateTime, Quantity.GetValueOrDefault(0));
                        QuantityAvailable = minAvail.MinimumAvailable.OwnedAndConsigned;
                        ConflictDate = minAvail.FirstConfict;
                        AvailabilityState = minAvail.AvailabilityState;
                    }


                    // all warehouses available
                    TInventoryWarehouseAvailability allWhAvailData = null;
                    if (availCache.TryGetValue(new TInventoryWarehouseAvailabilityKey(InventoryId, RwConstants.WAREHOUSEID_ALL), out allWhAvailData))
                    {
                        TInventoryWarehouseAvailabilityMinimum minAvail = allWhAvailData.GetMinimumAvailableQuantity(fromDateTime, toDateTime, Quantity.GetValueOrDefault(0));
                        QuantityAvailableAllWarehouses = minAvail.MinimumAvailable.OwnedAndConsigned;
                        ConflictDateAllWarehouses = minAvail.FirstConfict;
                        AvailabilityStateAllWarehouses = minAvail.AvailabilityState;
                    }

                }
            }
            e.PerformSave = false;
        }
        //-------------------------------------------------------------------------------------------------------   
    }
}