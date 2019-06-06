using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
using WebLibrary;

namespace WebApi.Modules.Reports.BillingAnalysisReport
{
    [FwSqlTable("dbo.funcbillinganalysisweb(@includerental, @includesales, @includemisc, @includelabor, @includeld, @includers, @includetaxordertotal, @includetaxordercost, @includetaxinvoiced, @includetaxvendorinvoice, @includecreditsinvoiced, @includebilledintotal)")]
    public class BillingAnalysisReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string OrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
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
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanagerid", modeltype: FwDataTypes.Text)]
        public string ProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanager", modeltype: FwDataTypes.Text)]
        public string ProjectManager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentfrom", modeltype: FwDataTypes.Date)]
        public string EstimatedStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentto", modeltype: FwDataTypes.Date)]
        public string EstimatedStopDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectid", modeltype: FwDataTypes.Text)]
        public string ProjectId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "divisionid", modeltype: FwDataTypes.Text)]
        public string DivisionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderlocation", modeltype: FwDataTypes.Text)]
        public string OrderLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypedesc", modeltype: FwDataTypes.Text)]
        public string OrderTypeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prcontact", modeltype: FwDataTypes.Text)]
        public string PrimaryContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectno", modeltype: FwDataTypes.Text)]
        public string ProjectNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectdesc", modeltype: FwDataTypes.Text)]
        public string ProjectDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "drawingid", modeltype: FwDataTypes.Text)]
        public string DrawingId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "drawing", modeltype: FwDataTypes.Text)]
        public string Drawing { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorderedid", modeltype: FwDataTypes.Text)]
        public string ProjectItemsOrderedId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemordered", modeltype: FwDataTypes.Text)]
        public string ProjectItemsOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dropshipid", modeltype: FwDataTypes.Text)]
        public string ProjectDropShipItemsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dropship", modeltype: FwDataTypes.Text)]
        public string ProjectDropShipItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "asbuildid", modeltype: FwDataTypes.Text)]
        public string AsBuildId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "asbuild", modeltype: FwDataTypes.Text)]
        public string AsBuild { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "commissioningid", modeltype: FwDataTypes.Text)]
        public string CommissioningId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "commissioning", modeltype: FwDataTypes.Text)]
        public string Commissioning { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depositid", modeltype: FwDataTypes.Text)]
        public string DepositId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deposit", modeltype: FwDataTypes.Text)]
        public string Deposit { get; set; }
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
        [FwSqlDataField(column: "ldextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LossAndDamageExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaleextended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalSaleExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salescost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SalesCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LaborCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misccost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? MiscCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LossAndDamageCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsalecost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalSaleCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaltax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salestax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SalesTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labortax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LaborTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misctax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? MiscTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldtax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LossAndDamageTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaletax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalSaleTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalcosttax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalCostTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salescosttax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SalesCostTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborcosttax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LaborCostTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misccosttax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? MiscCostTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldcosttax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LossAndDamageCostTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsalecosttax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalSaleCostTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? OrderTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordercost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? OrderCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedtotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? InvoicedTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notyetinvoiced", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? NotYetInvoiced { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicespaid", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? InvoicesPaid { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicesunpaid", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? InvoicesUnpaid { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvoicetotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? VendorInvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(BillingAnalysisReportRequest request)
        {
            bool includeRental = false;
            bool includeSales = false;
            bool includeMisc = false;
            bool includeLabor = false;
            bool includeLd = false;
            bool includeRentalSale = false;

            bool includeTaxOrderTotal = false;
            bool includeTaxOrderCost = false;
            bool includeTaxInvoiced = false;
            bool includeTaxVendorInvoice = false;

            foreach (SelectedCheckBoxListItem item in request.IncludeFilter)
            {
                switch (item.value)
                {
                    case "Rental":
                        includeRental = true;
                        break;
                    case "Sales":
                        includeSales = true;
                        break;
                    case "Misc":
                        includeMisc = true;
                        break;
                    case "Labor":
                        includeLabor = true;
                        break;
                    case "Ld":
                        includeLd = true;
                        break;
                    case "RentalSale":
                        includeRentalSale = true;
                        break;
                }
            }

            foreach (SelectedCheckBoxListItem item in request.IncludeTaxFilter)
            {
                switch (item.value)
                {
                    case "OrderTotal":
                        includeTaxOrderTotal = true;
                        break;
                    case "Cost":
                        includeTaxOrderCost = true;
                        break;
                    case "ToBill":
                        includeTaxInvoiced = true;
                        break;
                    case "VendorInvoice":
                        includeTaxVendorInvoice = true;
                        break;
                }
            }

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
                    select.AddWhereIn("locationid", request.OfficeLocationId);
                    select.AddWhereIn("customerid", request.CustomerId);
                    select.AddWhereIn("dealid", request.DealId);
                    select.AddWhereIn("projectid", request.ProjectId);
                    select.AddWhereIn("agentid", request.AgentId);
                    select.AddWhereIn("status", request.Status.ToString());
                    addDateFilterToSelect("orderdate", request.FromDate, select, ">=", "fromdate");
                    addDateFilterToSelect("orderdate", request.ToDate, select, "<=", "todate");

                    if (request.ExcludeOrdersBilledInTotal.GetValueOrDefault(false))
                    {
                        select.AddWhere("notyetinvoiced <> 0");
                    }

                    select.AddParameter("@includerental", includeRental);
                    select.AddParameter("@includesales", includeSales);
                    select.AddParameter("@includemisc", includeMisc);
                    select.AddParameter("@includelabor", includeLabor);
                    select.AddParameter("@includeld", includeLd);
                    select.AddParameter("@includers", includeRentalSale);
                    select.AddParameter("@includetaxordertotal", includeTaxOrderTotal);
                    select.AddParameter("@includetaxordercost", includeTaxOrderCost);
                    select.AddParameter("@includetaxinvoiced", includeTaxInvoiced);
                    select.AddParameter("@includetaxvendorinvoice", includeTaxVendorInvoice);
                    select.AddParameter("@includecreditsinvoiced", request.IncludeCreditsInvoiced);
                    select.AddParameter("@includebilledintotal", !request.ExcludeOrdersBilledInTotal);
                    select.AddOrderBy("location,agent,orderno");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "OrderTotal", "OrderCost", "InvoicedTotal", "VendorInvoiceTotal"};
                dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
                dt.InsertSubTotalRows("Agent", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
