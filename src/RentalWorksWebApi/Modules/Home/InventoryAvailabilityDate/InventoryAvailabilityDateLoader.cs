using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
using System.Threading.Tasks;
using System.Data;

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
        [FwSqlDataField(column: "availdate", modeltype: FwDataTypes.Date)]
        public string AvailabilityDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyavailable", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityAvailable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availablecolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string AvailableColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtylate", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityLate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "latecolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string LateColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyreserved", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityReserved { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reservedcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ReservedColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyreturning", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityReturning { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returningcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ReturningColor { get; set; }
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
