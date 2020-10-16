using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System;

namespace WebApi.Modules.Reports.InventoryChangeReport
{

    public class InventoryChangeReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string WarehouseId { get; set; }
        public string InventoryTypeId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string InventoryId { get; set; }
        public string TransactionType { get; set; }
        public IncludeExcludeAll FixedAsset { get; set; }
        public SelectedCheckBoxListItems Ranks { get; set; } = new SelectedCheckBoxListItems();
        public SelectedCheckBoxListItems TrackedBys { get; set; } = new SelectedCheckBoxListItems();
    }


    public class InventoryChangeReportLoader : AppReportLoader
    {
        protected string AvailableForFilter = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorychangeid", modeltype: FwDataTypes.Integer)]
        public int? InventoryChangeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "region", modeltype: FwDataTypes.Text)]
        public string Region { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryorderby", modeltype: FwDataTypes.Decimal)]
        public decimal? CategoryOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryorderby", modeltype: FwDataTypes.Decimal)]
        public decimal? SubCategOryorderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Text)]
        public string Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailableFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentqty", modeltype: FwDataTypes.Decimal)]
        public decimal? CurrentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currenttotalvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? CurrentTotalValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "changedate", modeltype: FwDataTypes.Date)]
        public string ChangeDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "changetype", modeltype: FwDataTypes.Text)]
        public string ChangeType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "changedesc", modeltype: FwDataTypes.Text)]
        public string ChangeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transactionsequence", modeltype: FwDataTypes.Integer)]
        public int? TransactionSequence { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "additionqty", modeltype: FwDataTypes.Decimal)]
        public decimal? AdditionQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitvalueaddition", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitValueAddition { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extendedadditionvalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ExtendedAdditionValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subtractionqty", modeltype: FwDataTypes.Decimal)]
        public decimal? SubtractionQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitvaluesubtraction", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? UnitValueSubtraction { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extendedsubtractionvalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? ExtendedSubtractionValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "runningtotalqty", modeltype: FwDataTypes.Decimal)]
        public decimal? RunningTotalQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "runningtotalvalue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RunningTotalValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transferindate", modeltype: FwDataTypes.Date)]
        public string TransferInDate { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(InventoryChangeReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getinventorychangerptweb", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@availfor", SqlDbType.Text, ParameterDirection.Input, AvailableForFilter);
                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, request.FromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, request.ToDate);
                    qry.AddParameter("@warehouseid", SqlDbType.Text, ParameterDirection.Input, request.WarehouseId);
                    qry.AddParameter("@inventorydepartmentid", SqlDbType.Text, ParameterDirection.Input, request.InventoryTypeId);
                    qry.AddParameter("@categoryid", SqlDbType.Text, ParameterDirection.Input, request.CategoryId);
                    qry.AddParameter("@subcategoryid", SqlDbType.Text, ParameterDirection.Input, request.SubCategoryId);
                    qry.AddParameter("@masterid", SqlDbType.Text, ParameterDirection.Input, request.InventoryId);
                    if (AvailableForFilter.Equals(RwConstants.INVENTORY_AVAILABLE_FOR_RENT))
                    {
                        if (request.FixedAsset.Equals(IncludeExcludeAll.IncludeOnly))
                        {
                            qry.AddParameter("@fixedassets", SqlDbType.Text, ParameterDirection.Input, RwConstants.INCLUDE);
                        }
                        else if (request.FixedAsset.Equals(IncludeExcludeAll.Exclude))
                        {
                            qry.AddParameter("@fixedassets", SqlDbType.Text, ParameterDirection.Input, RwConstants.EXCLUDE);
                        }
                    }
                    qry.AddParameter("@rank", SqlDbType.Text, ParameterDirection.Input, request.Ranks.ToString());
                    qry.AddParameter("@trackedby", SqlDbType.Text, ParameterDirection.Input, request.TrackedBys.ToString());

                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }

            if (request.IncludeSubHeadingsAndSubTotals)
            {
                dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
                string[] totalFields = new string[] { "AdditionQuantity", "ExtendedAdditionValue", "SubtractionQuantity", "ExtendedSubtractionValue" };
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                dt.InsertSubTotalRows("InventoryType", "RowType", totalFields);
                dt.InsertSubTotalRows("Category", "RowType", totalFields);
                dt.InsertSubTotalRows("SubCategory", "RowType", totalFields);
                dt.InsertSubTotalRows("Description", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }

            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
