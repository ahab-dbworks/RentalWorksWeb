using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using System;
using WebApi.Logic;

namespace WebApi.Modules.Utilities.QuikActivity
{
    public class QuikActivityLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitydate", modeltype: FwDataTypes.Date)]
        public string ActivityDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytime", modeltype: FwDataTypes.Text)]
        public string ActivityTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytypedesc", modeltype: FwDataTypes.Text)]
        public string ActivityDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypedesc", modeltype: FwDataTypes.Text)]
        public string OrderTypeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "", modeltype: FwDataTypes.Text)]
        public string OrderTypeController { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemcount", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        public override async Task<FwJsonDataTable> BrowseAsync(BrowseRequest request, FwCustomFields customFields = null)
        {
            DateTime fromDate = GetUniqueIdAsDate("FromDate", request).GetValueOrDefault(DateTime.Today);
            DateTime toDate = GetUniqueIdAsDate("ToDate", request).GetValueOrDefault(DateTime.Today);
            string officeLocationId = GetUniqueIdAsString("OfficeLocationId", request) ?? "";
            string warehouseId = GetUniqueIdAsString("WarehouseId", request) ?? "";
            string departmentId = GetUniqueIdAsString("DepartmentId", request) ?? "";
            string activityTypeId = GetUniqueIdAsString("ActivityTypeId", request) ?? "";
            bool summary = GetUniqueIdAsBoolean("Summary", request).GetValueOrDefault(false);

            FwJsonDataTable dt = null;

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getquikactivitydatadetail2", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@fromdate", SqlDbType.DateTime, ParameterDirection.Input, fromDate);
                    qry.AddParameter("@todate", SqlDbType.DateTime, ParameterDirection.Input, toDate);
                    qry.AddParameter("@locationid", SqlDbType.NVarChar, ParameterDirection.Input, officeLocationId);
                    qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, warehouseId);
                    qry.AddParameter("@departmentid", SqlDbType.NVarChar, ParameterDirection.Input, departmentId);
                    qry.AddParameter("@activitytypeid", SqlDbType.NVarChar, ParameterDirection.Input, activityTypeId);
                    qry.AddParameter("@summarizeorders", SqlDbType.NVarChar, ParameterDirection.Input, summary);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }

            //maybe this should just be done on the front-end?
            if (dt.Rows.Count > 0)
            {
                foreach (List<object> row in dt.Rows)
                {
                    string orderType = row[dt.GetColumnNo("OrderType")].ToString();
                    row[dt.GetColumnNo("OrderTypeController")] = AppFunc.GetOrderTypeDescriptionFronEndControllerName(orderType);
                }
            }

            return dt;
        }
        //------------------------------------------------------------------------------------
    }
}