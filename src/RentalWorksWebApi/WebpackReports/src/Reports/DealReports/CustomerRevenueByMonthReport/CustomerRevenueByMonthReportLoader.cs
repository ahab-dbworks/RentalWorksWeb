using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using FwStandard.Models;
using WebLibrary;
using System.Collections.Generic;

namespace WebApi.Modules.Reports.DealReports.CustomerRevenueByMonthReport
{
    public class CustomerRevenueByMonthReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custtypeid", modeltype: FwDataTypes.Text)]
        public string CustomerTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custtype", modeltype: FwDataTypes.Text)]
        public string CustomerType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryorder", modeltype: FwDataTypes.Decimal)]
        public decimal? CategoryOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month01name", modeltype: FwDataTypes.Text)]
        public string Month01Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month01dec", modeltype: FwDataTypes.Decimal)]
        public decimal? Month01RevenueAsDecimal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month01str", modeltype: FwDataTypes.Text)]
        public string Month01RevenueAsText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month02name", modeltype: FwDataTypes.Text)]
        public string Month02Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month02dec", modeltype: FwDataTypes.Decimal)]
        public decimal? Month02RevenueAsDecimal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month02str", modeltype: FwDataTypes.Text)]
        public string Month02RevenueAsText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month03name", modeltype: FwDataTypes.Text)]
        public string Month03Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month03dec", modeltype: FwDataTypes.Decimal)]
        public decimal? Month03RevenueAsDecimal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month03str", modeltype: FwDataTypes.Text)]
        public string Month03RevenueAsText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month04name", modeltype: FwDataTypes.Text)]
        public string Month04Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month04dec", modeltype: FwDataTypes.Decimal)]
        public decimal? Month04RevenueAsDecimal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month04str", modeltype: FwDataTypes.Text)]
        public string Month04RevenueAsText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month05name", modeltype: FwDataTypes.Text)]
        public string Month05Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month05dec", modeltype: FwDataTypes.Decimal)]
        public decimal? Month05RevenueAsDecimal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month05str", modeltype: FwDataTypes.Text)]
        public string Month05RevenueAsText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month06name", modeltype: FwDataTypes.Text)]
        public string Month06Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month06dec", modeltype: FwDataTypes.Decimal)]
        public decimal? Month06RevenueAsDecimal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month06str", modeltype: FwDataTypes.Text)]
        public string Month06RevenueAsText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month07name", modeltype: FwDataTypes.Text)]
        public string Month07Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month07dec", modeltype: FwDataTypes.Decimal)]
        public decimal? Month07RevenueAsDecimal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month07str", modeltype: FwDataTypes.Text)]
        public string Month07RevenueAsText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month08name", modeltype: FwDataTypes.Text)]
        public string Month08Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month08dec", modeltype: FwDataTypes.Decimal)]
        public decimal? Month08RevenueAsDecimal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month08str", modeltype: FwDataTypes.Text)]
        public string Month08RevenueAsText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month09name", modeltype: FwDataTypes.Text)]
        public string Month09Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month09dec", modeltype: FwDataTypes.Decimal)]
        public decimal? Month09RevenueAsDecimal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month09str", modeltype: FwDataTypes.Text)]
        public string Month09RevenueAsText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month10name", modeltype: FwDataTypes.Text)]
        public string Month10Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month10dec", modeltype: FwDataTypes.Decimal)]
        public decimal? Month10RevenueAsDecimal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month10str", modeltype: FwDataTypes.Text)]
        public string Month10RevenueAsText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month11name", modeltype: FwDataTypes.Text)]
        public string Month11Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month11dec", modeltype: FwDataTypes.Decimal)]
        public decimal? Month11RevenueAsDecimal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month11str", modeltype: FwDataTypes.Text)]
        public string Month11RevenueAsText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month12name", modeltype: FwDataTypes.Text)]
        public string Month12Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month12dec", modeltype: FwDataTypes.Decimal)]
        public decimal? Month12RevenueAsDecimal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month12str", modeltype: FwDataTypes.Text)]
        public string Month12RevenueAsText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allmonthsdec", modeltype: FwDataTypes.Decimal)]
        public decimal? AllMonthsRevenueAsDecimal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allmonthsstr", modeltype: FwDataTypes.Text)]
        public string AllMonthsRevenueAsText { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(CustomerRevenueByMonthReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getcustomerrevenuebymonthrptweb", this.AppConfig.DatabaseSettings.ReportTimeout))
                {

                    bool Rental = false;
                    bool Sales = false;
                    bool Parts = false;
                    bool Misc = false;
                    bool Labor = false;
                    bool FinalLd = false;
                    bool RentalSale = false;

                    foreach (SelectedCheckBoxListItem rt in request.RevenueTypes)
                    {
                        if (rt.value.Equals(RwConstants.RECTYPE_RENTAL))
                        {
                            Rental = true;
                        }
                        else if (rt.value.Equals(RwConstants.RECTYPE_SALE))
                        {
                            Sales = true;
                        }
                        else if (rt.value.Equals(RwConstants.RECTYPE_PARTS))
                        {
                            Parts = true;
                        }
                        else if (rt.value.Equals(RwConstants.RECTYPE_MISCELLANEOUS))
                        {
                            Misc = true;
                        }
                        else if (rt.value.Equals(RwConstants.RECTYPE_LABOR))
                        {
                            Labor = true;
                        }
                        else if (rt.value.Equals(RwConstants.RECTYPE_LOSS_AND_DAMAGE))
                        {
                            FinalLd = true;
                        }
                        else if (rt.value.Equals(RwConstants.RECTYPE_USED_SALE))
                        {
                            RentalSale = true;
                        }
                    }

                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, request.FromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, request.ToDate);
                    qry.AddParameter("@summary", SqlDbType.Text, ParameterDirection.Input, request.IsSummary);
                    qry.AddParameter("@locationid", SqlDbType.Text, ParameterDirection.Input, request.OfficeLocationId);
                    qry.AddParameter("@departmentid", SqlDbType.Text, ParameterDirection.Input, request.DepartmentId);
                    qry.AddParameter("@customertypeid", SqlDbType.Text, ParameterDirection.Input, request.CustomerTypeId);
                    qry.AddParameter("@customerid", SqlDbType.Text, ParameterDirection.Input, request.CustomerId);
                    qry.AddParameter("@dealtypeid", SqlDbType.Text, ParameterDirection.Input, request.DealTypeId);
                    qry.AddParameter("@dealid", SqlDbType.Text, ParameterDirection.Input, request.DealId);
                    qry.AddParameter("@inventorydepartmentid", SqlDbType.Text, ParameterDirection.Input, request.InventoryTypeId);
                    qry.AddParameter("@rental", SqlDbType.Text, ParameterDirection.Input, Rental);
                    qry.AddParameter("@sales", SqlDbType.Text, ParameterDirection.Input, Sales);
                    qry.AddParameter("@parts", SqlDbType.Text, ParameterDirection.Input, Parts);
                    qry.AddParameter("@misc", SqlDbType.Text, ParameterDirection.Input, Misc);
                    qry.AddParameter("@labor", SqlDbType.Text, ParameterDirection.Input, Labor);
                    qry.AddParameter("@finalld", SqlDbType.Text, ParameterDirection.Input, FinalLd);
                    qry.AddParameter("@rentalsale", SqlDbType.Text, ParameterDirection.Input, RentalSale);

                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
                //--------------------------------------------------------------------------------- 
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "Month01RevenueAsDecimal", "Month02RevenueAsDecimal", "Month03RevenueAsDecimal", "Month04RevenueAsDecimal", "Month05RevenueAsDecimal", "Month06RevenueAsDecimal", "Month07RevenueAsDecimal", "Month08RevenueAsDecimal", "Month09RevenueAsDecimal", "Month10RevenueAsDecimal", "Month11RevenueAsDecimal", "Month12RevenueAsDecimal", "AllMonthsRevenueAsDecimal" };
                dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
                dt.InsertSubTotalRows("Department", "RowType", totalFields);
                dt.InsertSubTotalRows("Customer", "RowType", totalFields);
                if (!request.IsSummary.GetValueOrDefault(false))
                {
                    dt.InsertSubTotalRows("Deal", "RowType", totalFields);
                }
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);

                foreach (List<object> row in dt.Rows)
                {
                    string revenueAsText = string.Empty;
                    object revenueObj = null;
                    decimal? revenueAsDecimal = 0;
                    for (int x = 1; x <= 12; x++)  // months 1 through 12
                    {
                        revenueAsText = string.Empty;
                        revenueObj = row[dt.GetColumnNo("Month" + x.ToString().PadLeft(2, '0') + "RevenueAsDecimal")];
                        if (revenueObj != null)
                        {
                            revenueAsDecimal = FwConvert.ToDecimal(revenueObj.ToString());
                            if (revenueAsDecimal != 0)
                            {
                                revenueAsText = FwConvert.ToCurrencyStringNoDollarSign(revenueAsDecimal.GetValueOrDefault(0));
                            }
                        }
                        row[dt.GetColumnNo("Month" + x.ToString().PadLeft(2, '0') + "RevenueAsText")] = revenueAsText;
                    }

                    // "allmonths"
                    revenueAsText = string.Empty;
                    revenueObj = row[dt.GetColumnNo("AllMonthsRevenueAsDecimal")];
                    if (revenueObj != null)
                    {
                        revenueAsDecimal = FwConvert.ToDecimal(revenueObj.ToString());
                        if (revenueAsDecimal != 0)
                        {
                            revenueAsText = FwConvert.ToCurrencyStringNoDollarSign(revenueAsDecimal.GetValueOrDefault(0));
                        }
                    }
                    row[dt.GetColumnNo("AllMonthsRevenueAsText")] = revenueAsText;
                }
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}