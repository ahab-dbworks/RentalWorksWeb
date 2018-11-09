using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace WebApi.Modules.Home.InventoryAvailabilityDate
{
    public class InventoryAvailabilityDateLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromdate", modeltype: FwDataTypes.DateTime)]
        public string start { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "todate", modeltype: FwDataTypes.DateTime)]
        public string end { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string html { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.OleToHtmlColor)]
        public string backColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "textcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string textColor { get; set; }
        //------------------------------------------------------------------------------------ 
        public string id { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Text)]
        public string resource { get; set; }
        //------------------------------------------------------------------------------------ 
        //protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        //{
        //    //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
        //    //DateTime paramDate = GetUniqueIdAsDateTime("ParamDate", request) ?? DateTime.MinValue; 
        //    //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
        //    base.SetBaseSelectQuery(select, qry, customFields, request);
        //    select.Parse();
        //    //select.AddWhere("(xxxtype = 'ABCDEF')"); 
        //    //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        //    //select.AddParameter("@paramstring", paramString); 
        //    //select.AddParameter("@paramdate", paramDate); 
        //    //select.AddParameter("@paramboolean", paramBoolean); 
        //}
        ////------------------------------------------------------------------------------------ 

        public override async Task<List<T>> SelectAsync<T>(BrowseRequest request, FwCustomFields customFields = null)
        {
            string inventoryId = GetUniqueIdAsString("InventoryId", request) ?? "";
            string warehouseId = GetUniqueIdAsString("WarehouseId", request) ?? "";
            DateTime fromDate = GetUniqueIdAsDate("FromDate", request) ?? DateTime.MinValue;
            DateTime toDate = GetUniqueIdAsDate("ToDate", request) ?? DateTime.MinValue;

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getavaildata", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, inventoryId);
                    qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, warehouseId);
                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, fromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, toDate);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                    AddPropertiesAsQueryColumns(qry);

                    bool openAndCloseConnection = true;
                    MethodInfo method = typeof(FwSqlCommand).GetMethod("SelectAsync");
                    MethodInfo generic = method.MakeGenericMethod(this.GetType());
                    dynamic result = generic.Invoke(qry, new object[] { openAndCloseConnection, customFields });
                    dynamic records = await result;
                    return records;
                }
            }
        }
        //------------------------------------------------------------------------------------


        public override async Task<FwJsonDataTable> BrowseAsync(BrowseRequest request, FwCustomFields customFields = null)
        {
            FwJsonDataTable dt = null;

            string inventoryId = GetUniqueIdAsString("InventoryId", request) ?? "";
            string warehouseId = GetUniqueIdAsString("WarehouseId", request) ?? "";
            DateTime fromDate = GetUniqueIdAsDate("FromDate", request) ?? DateTime.MinValue; 
            DateTime toDate = GetUniqueIdAsDate("ToDate", request) ?? DateTime.MinValue; 


            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getavaildata", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, inventoryId);
                    qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, warehouseId);
                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, fromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, toDate);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }
            return dt;
        }
        //------------------------------------------------------------------------------------



    }
}
