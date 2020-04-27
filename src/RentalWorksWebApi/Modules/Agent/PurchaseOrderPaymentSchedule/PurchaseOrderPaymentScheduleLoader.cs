using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
namespace WebApi.Modules.Agent.PurchaseOrderPaymentSchedule
{
    [FwSqlTable("billschedrptview")]
    public class PurchaseOrderPaymentScheduleLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billschedid", modeltype: FwDataTypes.Text)]
        public string EpisodeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "epno", modeltype: FwDataTypes.Integer)]
        public int? EpisodeNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromdate", modeltype: FwDataTypes.Date)]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "todate", modeltype: FwDataTypes.Date)]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billweekend", modeltype: FwDataTypes.Boolean)]
        public bool? BillWeekends { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billholiday", modeltype: FwDataTypes.Boolean)]
        public bool? BillHolidays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billabledays", modeltype: FwDataTypes.Decimal)]
        public decimal? BillableDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hiatus", modeltype: FwDataTypes.Boolean)]
        public bool? IsHiatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hiatusdiscount", modeltype: FwDataTypes.Decimal)]
        public decimal? HiatusDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "eporder", modeltype: FwDataTypes.Integer)]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalbeforeoverride", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalBeforeOverride { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? GrossTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountamt", modeltype: FwDataTypes.Decimal)]
        public decimal? DiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pretaxtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? SubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax1", modeltype: FwDataTypes.Decimal)]
        public decimal? Tax1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax2", modeltype: FwDataTypes.Decimal)]
        public decimal? Tax2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax", modeltype: FwDataTypes.Decimal)]
        public decimal? TaxTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "total", modeltype: FwDataTypes.Decimal)]
        public decimal? Total { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "metersubtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MeterSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "meterdiscountamt", modeltype: FwDataTypes.Decimal)]
        public decimal? MeterDiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "metertax", modeltype: FwDataTypes.Decimal)]
        public decimal? MeterTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "metertotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MeterTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "periodno", modeltype: FwDataTypes.Integer)]
        public int? PeriodNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? BillPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjbilltotal", modeltype: FwDataTypes.Decimal)]
        public decimal? AdjustedBillTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "createrebate", modeltype: FwDataTypes.Boolean)]
        public bool? CreateRebateInvoice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rebatepercent", modeltype: FwDataTypes.Decimal)]
        public decimal? RebatePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rebateamount", modeltype: FwDataTypes.Decimal)]
        public decimal? RebateAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "duedate", modeltype: FwDataTypes.Date)]
        public string DueDate { get; set; }
        //------------------------------------------------------------------------------------ 
        //protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        //{
        //    //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
        //    //DateTime paramDate = GetUniqueIdAsDate("ParamDate", request) ?? DateTime.MinValue; 
        //    //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
        //    base.SetBaseSelectQuery(select, qry, customFields, request);
        //    select.Parse();
        //    //select.AddWhere("(xxxtype = 'ABCDEF')"); 
        //    //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        //    //select.AddParameter("@paramstring", paramString); 
        //    //select.AddParameter("@paramdate", paramDate); 
        //    //select.AddParameter("@paramboolean", paramBoolean); 
        //}
        //------------------------------------------------------------------------------------ 
        public override async Task<FwJsonDataTable> BrowseAsync(BrowseRequest request, FwCustomFields customFields = null)
        {
            string purchaseOrderId = GetUniqueIdAsString("PurchaseOrderId", request) ?? "~x~x~x~x";

            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getpopaymentscheduleweb", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }

            //foreach (List<object> row in dt.Rows)
            //{
            //    row[dt.GetColumnNo("ICodeColor")] = getICodeColor(row[dt.GetColumnNo("ItemClass")].ToString());
            //    row[dt.GetColumnNo("DescriptionColor")] = getDescriptionColor(row[dt.GetColumnNo("ItemClass")].ToString());
            //    row[dt.GetColumnNo("RecTypeColor")] = determineRecTypeColor(row[dt.GetColumnNo("RecType")].ToString());
            //}

            return dt;
        }
        //------------------------------------------------------------------------------------
    }
}
