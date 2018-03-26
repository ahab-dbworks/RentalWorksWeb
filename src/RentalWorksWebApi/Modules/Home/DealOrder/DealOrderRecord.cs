using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Modules.Home.Order;
using WebLibrary;

namespace WebApi.Modules.Home.DealOrder
{
    [FwSqlTable("dealorder")]
    public class DealOrderRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string OrderId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 16)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50, required: true)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string OrderDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15, required: true)]
        public string Type { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Miscellaneous { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Labor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "space", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Facilities { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicle", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Transportation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickdate", modeltype: FwDataTypes.Date)]
        public string PickDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "picktime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string PickTime { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "estrentfrom", modeltype: FwDataTypes.Date)]
        public string EstimatedStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "estfromtime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string EstimatedStartTime { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "estrentto", modeltype: FwDataTypes.Date)]
        public string EstimatedStopDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "esttotime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string EstimatedStopTime { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RateType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "flatpo", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? FlatPo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pending", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? PendingPo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "refno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string ReferenceNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "versionno", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int VersionNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodstart", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodend", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string BillingEndDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billingdates", modeltype: FwDataTypes.Text , sqltype: "billingdates", maxlength: 10)]
        public string DetermineQuantitiesToBillBasedOn { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string BillingCycleId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string PaymentTermsId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string PaymentTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string TaxId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 01)]
        public bool NoCharge { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "nochargereason", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string NoChargeReason { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuedtoadd", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string PrintIssuedToAddressFrom { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billname", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 100)]
        public string IssuedToName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attention", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string IssuedToAttention { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attention2", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string IssuedToAttention2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billadd1", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string IssuedToAddress1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billadd2", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string IssuedToAddress2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billcity", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string IssuedToCity { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billstate", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string IssuedToState { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billzip", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string IssuedToZipCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billcountryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 08)]
        public string IssuedToCountryId { get; set; }
        //------------------------------------------------------------------------------------

        [FwSqlDataField(column: "includeinbillinganalysis", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 1)]
        public bool IncludeInBillingAnalysis { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "hiatusdiscfrom", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 07)]
        public string HiatusDiscountFrom { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "summaryinvoice", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 01)]
        public bool InGroup { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "summaryinvoicegroup", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int GroupNumber { get; set; }
        //------------------------------------------------------------------------------------








        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        public async Task<bool> SavePoASync(string PoNumber, decimal? PoAmount)
        {
            bool saved = false;
            if ((PoNumber != null) && (PoAmount != null))  // temporary: actual solution is to force the PO number and Amount with the post
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "setorderpo", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                    qry.AddParameter("@orgpono", SqlDbType.NVarChar, ParameterDirection.Input, "");
                    qry.AddParameter("@newpono", SqlDbType.NVarChar, ParameterDirection.Input, PoNumber);
                    qry.AddParameter("@poamount", SqlDbType.Decimal, ParameterDirection.Input, PoAmount);
                    qry.AddParameter("@insertnew", SqlDbType.NVarChar, ParameterDirection.Input, false);
                    await qry.ExecuteNonQueryAsync(true);
                    saved = true;
                }
            }
            return saved;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<bool> SetNumber()
        {
            bool saved = false;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "getnextcounter", this.AppConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@module", SqlDbType.NVarChar, ParameterDirection.Input, "QUOTE");
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, "A000001Q");  // temporary: todo: get logged-in usersid
                qry.AddParameter("@newcounter", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                OrderNumber = qry.GetParameter("@newcounter").ToString().TrimEnd();
                saved = true;
            }
            return saved;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<string> Copy(QuoteOrderCopyRequest copyRequest)
        {
            string newId = "";
            if (OrderId != null)
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "copyquoteorder", this.AppConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@fromorderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                    qry.AddParameter("@newordertype", SqlDbType.NVarChar, ParameterDirection.Input, copyRequest.CopyToType);
                    qry.AddParameter("@ratesfrominventory", SqlDbType.NVarChar, ParameterDirection.Input, copyRequest.CopyRatesFromInventory);
                    qry.AddParameter("@combinesubs", SqlDbType.NVarChar, ParameterDirection.Input, copyRequest.CombineSubs);
                    qry.AddParameter("@copydates", SqlDbType.NVarChar, ParameterDirection.Input, copyRequest.CopyDates);
                    qry.AddParameter("@copyitemnotes", SqlDbType.NVarChar, ParameterDirection.Input, copyRequest.CopyLineItemNotes);
                    qry.AddParameter("@copydocuments", SqlDbType.NVarChar, ParameterDirection.Input, copyRequest.CopyDocuments);
                    qry.AddParameter("@copytodealid", SqlDbType.NVarChar, ParameterDirection.Input, copyRequest.CopyToDealId);
                    qry.AddParameter("@neworderid", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync(true);
                    newId = qry.GetParameter("@neworderid").ToString();
                }
            }
            return newId;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
