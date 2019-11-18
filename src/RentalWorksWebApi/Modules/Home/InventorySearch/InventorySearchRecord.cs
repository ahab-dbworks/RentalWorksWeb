using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Logic;
using WebApi.Modules.Home.InventoryPackageInventory;
using WebLibrary;

namespace WebApi.Modules.Home.InventorySearch
{
    [FwSqlTable("tmpsearchsession")]
    public class InventorySearchRecord : AppDataReadWriteRecord
    {
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
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        // this property is only used to hold the return value in the overridden save method below
        public decimal? TotalQuantityInSession { get; set; }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveInventorySearch(object sender, BeforeSaveDataRecordEventArgs e)
        {
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
                }
            }
            e.PerformSave = false;
        }
        //-------------------------------------------------------------------------------------------------------   
        //public async Task<InventorySearchGetTotalResponse> GetTotalAsync(InventorySearchGetTotalRequest request)
        //{
        //    InventorySearchGetTotalResponse response = new InventorySearchGetTotalResponse();
        //    using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
        //    {
        //        FwSqlCommand qry = new FwSqlCommand(conn, "tmpsearchsessiongettotal", this.AppConfig.DatabaseSettings.QueryTimeout);
        //        qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
        //        qry.AddParameter("@totalqtyinsession", SqlDbType.Float, ParameterDirection.Output);
        //        qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
        //        qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
        //        int i = await qry.ExecuteNonQueryAsync();
        //        response.TotalQuantityInSession = qry.GetParameter("@totalqtyinsession").ToDecimal();
        //        response.status = qry.GetParameter("@status").ToInt32();
        //        response.success = (response.status == 0);
        //        response.msg = qry.GetParameter("@msg").ToString();
        //    }
        //    return response;
        //}
        ////-------------------------------------------------------------------------------------------------------   
        //public async Task<bool> AddToAsync(InventorySearchAddToRequest request)
        //{
        //    bool b = false;
        //    using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
        //    {
        //        if (string.IsNullOrEmpty(request.InventoryId))
        //        {
        //            // adding to Quote, Order, PO, Transfer, etc.
        //            FwSqlCommand qry = new FwSqlCommand(conn, "tmpsearchsessionaddtoorder", this.AppConfig.DatabaseSettings.QueryTimeout);
        //            qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
        //            qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
        //            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
        //            int i = await qry.ExecuteNonQueryAsync();
        //        }
        //        else
        //        {
        //            //adding to Complete, Kit, Container

        //            string invClassification = FwSqlCommand.GetStringDataAsync(conn, this.AppConfig.DatabaseSettings.QueryTimeout, "master", "masterid", request.InventoryId, "class").Result;
        //            if (invClassification.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE) || invClassification.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT))
        //            {
        //                try
        //                {
        //                    await conn.GetConnection().OpenAsync();
        //                    conn.BeginTransaction();

        //                    FwSqlCommand qrySessionItems = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout);
        //                    qrySessionItems.Add("select t.masteritemid, t.masterid, t.warehouseid, t.qty,        ");
        //                    qrySessionItems.Add("       m.master                                                 ");
        //                    qrySessionItems.Add(" from  tmpsearchsession t                                       ");
        //                    qrySessionItems.Add("            join master m on (t.masterid = m.masterid)          ");
        //                    qrySessionItems.Add(" where t.sessionid = @sessionid                                 ");
        //                    qrySessionItems.AddParameter("@sessionid", request.SessionId);
        //                    FwJsonDataTable dt = await qrySessionItems.QueryToFwJsonTableAsync();

        //                    //#jhtodo: need to handle Containers special here #788
        //                    foreach (List<object> row in dt.Rows)
        //                    {
        //                        InventoryPackageInventoryLogic item = new InventoryPackageInventoryLogic();
        //                        item.SetDependencies(AppConfig, UserSession);
        //                        item.PackageId = request.InventoryId;
        //                        item.InventoryId = row[dt.GetColumnNo("masterid")].ToString();
        //                        item.Description = row[dt.GetColumnNo("master")].ToString();
        //                        item.DefaultQuantity = FwConvert.ToDecimal(row[dt.GetColumnNo("qty")].ToString());
        //                        //item.WarehouseId = row[dt.GetColumnNo("warehouseid")].ToString();  // (only specify when the Complete/Kit is "warehouse-specific"
        //                        int saveCount = item.SaveAsync(null, conn: conn).Result;
        //                    }

        //                    qrySessionItems = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout);
        //                    qrySessionItems.Add("delete t                                                        ");
        //                    qrySessionItems.Add(" from  tmpsearchsession t                                       ");
        //                    qrySessionItems.Add(" where t.sessionid = @sessionid                                 ");
        //                    qrySessionItems.AddParameter("@sessionid", request.SessionId);
        //                    await qrySessionItems.ExecuteAsync();

        //                    conn.CommitTransaction();
        //                }

        //                catch (Exception ex)
        //                {
        //                    conn.RollbackTransaction();
        //                    throw ex;
        //                }
        //                finally
        //                {
        //                    conn.Close();
        //                }
        //            }
        //            else if (invClassification.Equals(RwConstants.INVENTORY_CLASSIFICATION_CONTAINER))
        //            {
        //                string containerId = FwSqlCommand.GetStringDataAsync(conn, this.AppConfig.DatabaseSettings.QueryTimeout, "master", "masterid", request.InventoryId, "containerid").Result;

        //                FwSqlCommand qry = new FwSqlCommand(conn, "tmpsearchsessionaddtoorder", this.AppConfig.DatabaseSettings.QueryTimeout);
        //                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
        //                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, containerId);
        //                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
        //                int i = await qry.ExecuteNonQueryAsync();

        //            }
        //            else
        //            {
        //                throw new Exception($"Cannot copy from QuikSearch.  Invalid Inventory Classification: \"{invClassification}\"");
        //            }

        //        }
        //    }
        //    return b;
        //}
        ////-------------------------------------------------------------------------------------------------------   
    }
}