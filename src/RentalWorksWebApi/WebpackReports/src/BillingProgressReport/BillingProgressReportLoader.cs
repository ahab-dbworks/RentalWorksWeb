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
using System.Reflection;

namespace WebApi.Modules.Reports.BillingProgressReport
{
    public class BillingProgressReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "officelocation", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? OrderTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billed", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Billed { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remaining", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Remaining { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedpercent", modeltype: FwDataTypes.Percentage)]
        public decimal? BilledPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        public override async Task<FwJsonDataTable> BrowseAsync(BrowseRequest request, FwCustomFields customFields = null)
        {
            FwJsonDataTable dt = null;
            DateTime asOfDate = DateTime.Today;
            string locationId = "";
            string departmentId = "";
            string dealCsrId = "";
            string customerId = "";
            string dealTypeId = "";
            string dealId = "";
            string agentId = "";
            bool includeConfirmed = false;
            bool includeHold = false;
            bool includeActive = false;
            bool includeComplete = false;
            bool includeClosed = false;
            bool includeCredits = false;
            bool excludeBilled100 = false;
            dynamic statusList = null;

            if (request != null)
            {
                if (request.uniqueids != null)
                {
                    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                    if (uniqueIds.ContainsKey("AsOfDate"))
                    {
                        asOfDate = FwConvert.ToDateTime(uniqueIds["AsOfDate"].ToString());
                    }
                    if (uniqueIds.ContainsKey("OfficeLocationId"))
                    {
                        locationId = uniqueIds["OfficeLocationId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("DepartmentId"))
                    {
                        departmentId = uniqueIds["DepartmentId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("CustomerId"))
                    {
                        customerId = uniqueIds["CustomerId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("DealId"))
                    {
                        dealId = uniqueIds["DealId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("DealTypeId"))
                    {
                        dealTypeId = uniqueIds["DealTypeId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("CsrId"))
                    {
                        dealCsrId = uniqueIds["CsrId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("AgentId"))
                    {
                        agentId = uniqueIds["AgentId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("IncludeCreditInvoices"))
                    {
                        includeCredits = FwConvert.ToBoolean(uniqueIds["IncludeCreditInvoices"].ToString());
                    }
                    if (uniqueIds.ContainsKey("ExcludeOrdersBilled100"))
                    {
                        excludeBilled100 = FwConvert.ToBoolean(uniqueIds["ExcludeOrdersBilled100"].ToString());
                    }
                    if (uniqueIds.ContainsKey("statuslist"))
                    {
                        statusList = uniqueIds["statuslist"];
                    }
                }
            }

            foreach (dynamic parameter in statusList)
            {
                if (parameter.value == RwConstants.ORDER_STATUS_CONFIRMED)
                {
                    includeConfirmed = true;
                }
                if (parameter.value == RwConstants.ORDER_STATUS_HOLD)
                {
                    includeHold = true;
                }
                if (parameter.value == RwConstants.ORDER_STATUS_ACTIVE)
                {
                    includeActive = true;
                }
                if (parameter.value == RwConstants.ORDER_STATUS_COMPLETE)
                {
                    includeComplete = true;
                }
                if (parameter.value == RwConstants.ORDER_STATUS_CLOSED)
                {
                    includeClosed = true;
                }
            }

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getbillingprogressrpt", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@asofdate", SqlDbType.Date, ParameterDirection.Input, asOfDate);
                    qry.AddParameter("@includeconfirmed", SqlDbType.Text, ParameterDirection.Input, includeConfirmed ? "T" : "F");
                    qry.AddParameter("@includehold", SqlDbType.Text, ParameterDirection.Input, includeHold? "T" : "F");
                    qry.AddParameter("@includeactive", SqlDbType.Text, ParameterDirection.Input, includeActive? "T" : "F");
                    qry.AddParameter("@includecomplete", SqlDbType.Text, ParameterDirection.Input, includeComplete? "T" : "F");
                    qry.AddParameter("@includeclosed", SqlDbType.Text, ParameterDirection.Input, includeClosed ? "T" : "F");
                    qry.AddParameter("@includecredits", SqlDbType.Text, ParameterDirection.Input, includeCredits ? "T" : "F");
                    qry.AddParameter("@excludebilled100", SqlDbType.Text, ParameterDirection.Input, excludeBilled100 ? "T" : "F");
                    qry.AddParameter("@locationid", SqlDbType.Text, ParameterDirection.Input, locationId);
                    qry.AddParameter("@departmentid", SqlDbType.Text, ParameterDirection.Input, departmentId);
                    qry.AddParameter("@dealcsrid", SqlDbType.Text, ParameterDirection.Input, dealCsrId);
                    qry.AddParameter("@customerid", SqlDbType.Text, ParameterDirection.Input, customerId);
                    qry.AddParameter("@dealtypeid", SqlDbType.Text, ParameterDirection.Input, dealTypeId);
                    qry.AddParameter("@dealid", SqlDbType.Text, ParameterDirection.Input, dealId);
                    qry.AddParameter("@agentid", SqlDbType.Text, ParameterDirection.Input, agentId);

                    PropertyInfo[] propertyInfos = typeof(BillingProgressReportLoader).GetProperties();
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        FwSqlDataFieldAttribute sqlDataFieldAttribute = propertyInfo.GetCustomAttribute<FwSqlDataFieldAttribute>();
                        if (sqlDataFieldAttribute != null)
                        {
                            qry.AddColumn(sqlDataFieldAttribute.ColumnName, propertyInfo.Name, sqlDataFieldAttribute.ModelType, sqlDataFieldAttribute.IsVisible, sqlDataFieldAttribute.IsPrimaryKey, false);
                        }
                    }
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }

            string[] totalFields = new string[] { "OrderTotal", "Billed", "Remaining" };
            dt.InsertSubTotalRows("Deal", "RowType", totalFields);
            dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
            dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);

            return dt;
        }
        //------------------------------------------------------------------------------------
    }
}
