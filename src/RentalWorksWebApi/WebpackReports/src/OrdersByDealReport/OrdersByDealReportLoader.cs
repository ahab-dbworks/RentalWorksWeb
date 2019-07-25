using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using WebLibrary;
using System.Text;

namespace WebApi.Modules.Reports.OrdersByDealReport
{
    [FwSqlTable("dealorderrptwebview")]
    public class OrdersByDealReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text)]
        public string Address1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text)]
        public string Address2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text)]
        public string City { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text)]
        public string Zip { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "country", modeltype: FwDataTypes.Text)]
        public string Country { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealinputdate", modeltype: FwDataTypes.Date)]
        public string DealInputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealstatusasof", modeltype: FwDataTypes.Date)]
        public string DealStatusAsOf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealstatusid", modeltype: FwDataTypes.Text)]
        public string DealStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealstatus", modeltype: FwDataTypes.Text)]
        public string DealStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealtypeid", modeltype: FwDataTypes.Text)]
        public string DealTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealtype", modeltype: FwDataTypes.Text)]
        public string DealType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealprimarycontactname", modeltype: FwDataTypes.Text)]
        public string DealPrimaryContactName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealprimarycontactphone", modeltype: FwDataTypes.Text)]
        public string DealPrimaryContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealprimarycontactext", modeltype: FwDataTypes.Text)]
        public string DealPrimaryContactExt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditstatus", modeltype: FwDataTypes.Text)]
        public string CreditStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditthroughdate", modeltype: FwDataTypes.Date)]
        public string CreditThroughDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "insurancecertification", modeltype: FwDataTypes.Boolean)]
        public bool? InsuranceCertification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "insurancevalidthroughdate", modeltype: FwDataTypes.Date)]
        public string InsuranceValidThroughDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string OrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertotal", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentfrom", modeltype: FwDataTypes.Date)]
        public string EstimatedStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentto", modeltype: FwDataTypes.Date)]
        public string EstimatedStopDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "wano", modeltype: FwDataTypes.Text)]
        public string WarehouseNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderstatus", modeltype: FwDataTypes.Text)]
        public string OrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderstatusdate", modeltype: FwDataTypes.Date)]
        public string OrderStatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SalesExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LaborExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? MiscExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Boolean)]
        public bool? NoCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealpayterms", modeltype: FwDataTypes.Text)]
        public string DealPaymentTerms { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderpayterms", modeltype: FwDataTypes.Text)]
        public string OrderPaymentTerms { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(OrdersByDealReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();

                    if (request.FilterDatesOrderCreate.GetValueOrDefault(false))
                    {
                        addDateFilterToSelect("orderdate", request.OrderCreateFromDate, select, ">=", "orderdatefrom");
                        addDateFilterToSelect("orderdate", request.OrderCreateToDate, select, "<=", "orderdateto");
                    }
                    if (request.FilterDatesOrderStart.GetValueOrDefault(false))
                    {
                        addDateFilterToSelect("estrentfrom", request.OrderStartFromDate, select, ">=", "estrentfromfrom");
                        addDateFilterToSelect("estrentfrom", request.OrderStartToDate, select, "<=", "estrentfromto");
                    }
                    if (request.FilterDatesDealCredit.GetValueOrDefault(false))
                    {
                        addDateFilterToSelect("creditthroughdate", request.DealCreditFromDate, select, ">=", "creditthroughdatefrom");
                        addDateFilterToSelect("creditthroughdate", request.DealCreditToDate, select, "<=", "creditthroughdateto");
                    }
                    if (request.FilterDatesDealInsurance.GetValueOrDefault(false))
                    {
                        addDateFilterToSelect("insurancevalidthroughdate", request.DealInsuranceFromDate, select, ">=", "insurancevalidthroughdatefrom");
                        addDateFilterToSelect("insurancevalidthroughdate", request.DealInsuranceToDate, select, "<=", "insurancevalidthroughdateto");
                    }
                    select.AddWhereIn("locationid", request.OfficeLocationId);
                    select.AddWhereIn("departmentid", request.DepartmentId);
                    select.AddWhereIn("customerid", request.CustomerId);
                    select.AddWhereIn("dealtypeid", request.DealTypeId);
                    select.AddWhereIn("dealstatusid", request.DealStatusId);
                    select.AddWhereIn("dealid", request.DealId);

                    if (!string.IsNullOrEmpty(request.NoCharge))   // ALL, NoChargeOnly, ExcludeNoCharge
                    {
                        if (request.NoCharge.Equals("NoChargeOnly"))
                        {
                            select.AddWhere("nocharge = @nocharge");
                            select.AddParameter("@nocharge", "T");
                        }
                        else if (request.NoCharge.Equals("ExcludeNoCharge"))
                        {
                            select.AddWhere("nocharge <> @nocharge");
                            select.AddParameter("@nocharge", "T");
                        }
                    }

                    if (request.OrderType.Count > 0)
                    {
                        StringBuilder orderTypeWhere = new StringBuilder();

                        foreach (SelectedCheckBoxListItem item in request.OrderType)
                        {
                            if (item.value.Equals(RwConstants.RECTYPE_RENTAL))
                            {
                                if (orderTypeWhere.Length.Equals(0))
                                {
                                    orderTypeWhere.Append("(");
                                }
                                else
                                {
                                    orderTypeWhere.Append(" or ");
                                }
                                orderTypeWhere.Append("rentalextended <> 0");
                            }
                            if (item.value.Equals(RwConstants.RECTYPE_SALE))
                            {
                                if (orderTypeWhere.Length.Equals(0))
                                {
                                    orderTypeWhere.Append("(");
                                }
                                else
                                {
                                    orderTypeWhere.Append(" or ");
                                }
                                orderTypeWhere.Append("salesextended <> 0");
                            }
                            if (item.value.Equals(RwConstants.RECTYPE_LABOR))
                            {
                                if (orderTypeWhere.Length.Equals(0))
                                {
                                    orderTypeWhere.Append("(");
                                }
                                else
                                {
                                    orderTypeWhere.Append(" or ");
                                }
                                orderTypeWhere.Append("laborextended <> 0");
                            }
                            if (item.value.Equals(RwConstants.RECTYPE_MISCELLANEOUS))
                            {
                                if (orderTypeWhere.Length.Equals(0))
                                {
                                    orderTypeWhere.Append("(");
                                }
                                else
                                {
                                    orderTypeWhere.Append(" or ");
                                }
                                orderTypeWhere.Append("miscextended <> 0");
                            }
                        }

                        if (orderTypeWhere.Length > 0)
                        {
                            orderTypeWhere.Append(")");

                            select.AddWhere(orderTypeWhere.ToString());
                        }
                    }

                    request.OrderStatus.Add(new SelectedCheckBoxListItem(""));
                    select.AddWhereIn("orderstatus", request.OrderStatus);

                    select.AddOrderBy("location, department, customer, deal, orderno");

                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "OrderTotal" };
                string[] headerFieldsDeal = new string[] { "Customer", "DealNumber", "DealType", "DealPrimaryContactName", "DealPrimaryContactPhone", "DealPrimaryContactExt", "CreditStatus", "InsuranceCertification", "EstimatedStopDate", "EstimatedStartDate", "DealPaymentTerms", "Address1", "Address2", "City", "State", "Zip", "Country", "Phone" };
                dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
                dt.InsertSubTotalRows("Department", "RowType", totalFields);
                dt.InsertSubTotalRows("Customer", "RowType", totalFields);
                dt.InsertSubTotalRows("Deal", "RowType", totalFields, headerFieldsDeal, totalFor: "Total for Deal");
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
