using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using static WebApi.Modules.AccountServices.Account.AccountController;

namespace WebApi.Modules.Reports.ContractReports.ReturnListReport
{
    [FwSqlTable("funcreturnlist(@dealid,@departmentid,@sortby,@printbarcodemode,@includesales,@warehouseid,@contractid,@orderids)")]
    public class ReturnListReportItemsLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returnlistprint", modeltype: FwDataTypes.Text)]
        public string ReturnListPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string Barcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        //public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masternodisplay", modeltype: FwDataTypes.Text)]
        public string ICodeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Text)]
        public string OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string MasterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stillout", modeltype: FwDataTypes.Decimal)]
        public decimal StillOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "metered", modeltype: FwDataTypes.Text)]
        public string Metered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyin", modeltype: FwDataTypes.Decimal)]
        public string QuantityIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedbyweight", modeltype: FwDataTypes.Text)]
        public string TrackedByWeight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightunitid", modeltype: FwDataTypes.Text)]
        public string WeightUnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightunitqty", modeltype: FwDataTypes.Decimal)]
        public decimal? weightunitqty { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weight", modeltype: FwDataTypes.Decimal)]
        public decimal? Weight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightunit", modeltype: FwDataTypes.Text)]
        public string WeightUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedbylength", modeltype: FwDataTypes.Text)]
        public string TrackedByLength { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lengthunitid", modeltype: FwDataTypes.Text)]
        public string LengthUnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lengthunitqty", modeltype: FwDataTypes.Decimal)]
        public decimal LengthUnitQty { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "length", modeltype: FwDataTypes.Decimal)]
        public decimal Length { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lengthunit", modeltype: FwDataTypes.Text)]
        public string LengthUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "aisle", modeltype: FwDataTypes.Text)]
        public string Aisle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shelf", modeltype: FwDataTypes.Text)]
        public string Shelf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string MasterItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "completeid", modeltype: FwDataTypes.Text)]
        public string CompleteId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "packageitemid", modeltype: FwDataTypes.Text)]
        public string PackageItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nestedmasteritemid", modeltype: FwDataTypes.Text)]
        public string NestedMasterItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderunitid", modeltype: FwDataTypes.Text)]
        public string OrderUnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> GetItemsTable(ReturnListReportRequest request)
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
                    select.AddParameter("@dealid", request.DealId ?? "");
                    select.AddParameter("@departmentid", request.DepartmentId ?? "");
                    select.AddParameter("@sortby", request.SortBy ?? "");
                    select.AddParameter("@printbarcodemode", request.PrintBarcodeMode ?? "");
                    select.AddParameter("@includesales", request.IncludeSales ?? "");
                    select.AddParameter("@warehouseid", request.WarehouseId ?? "");
                    select.AddParameter("@contractid", request.ContractId ?? "");
                    select.AddParameter("@orderids", request.OrderIds ?? "");

                    if (request.IncludeTrackedByBarcode.GetValueOrDefault(false))
                    {
                        select.AddWhere("trackedby in ('QUANTITY)");
                    }

                    if (request.PaginateByInventoryType.GetValueOrDefault(false))
                    {
                        select.AddOrderBy("inventorydepartment, rectype, orderby, masterno, description, weight");
                    }
                    else
                    {
                        select.AddOrderBy("rectype, orderby, masterno, description, weight");
                    }

                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }

            return dt;
        }
        //------------------------------------------------------------------------------------ 
        public async Task<ReturnListReportLoader> SetWarehouseOptions(ReturnListReportRequest request)
        {
            ReturnListReportLoader ReturnListOptions = new ReturnListReportLoader();
            
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.Add("select top 1 returnlistprintin, returnlistprintout");
                    qry.Add("from warehouse w with (nolock)");
                    qry.Add("       join users u with (nolock) on (u.warehouseid = w.warehouseid)");
                    qry.Add("where u.usersid = @usersid");
                    qry.AddParameter("@usersid", UserSession.UsersId);

                    await qry.ExecuteAsync();
                    ReturnListOptions.PrintIn = qry.GetField("returnlistprintin").ToBoolean();
                    ReturnListOptions.PrintOut = qry.GetField("returnlistprintout").ToBoolean();
                }
            }
            return ReturnListOptions;
        }
    }
    public class ReturnListReportLoader : ReturnListReportItemsLoader
    {
        public FwJsonDataTable ItemsTable { get; set; }
        public bool PrintIn {get; set;}
        public bool PrintOut { get; set; }
        //-------------------------------------------------------------------------------------------------------        }
        public async Task<ReturnListReportLoader> RunReportAsync(ReturnListReportRequest request)
        {
            useWithNoLock = false;
            ReturnListReportLoader ReturnList = new ReturnListReportLoader();
            ReturnListReportLoader ReturnListOptions = await SetWarehouseOptions(request);
            ReturnList.ItemsTable = await GetItemsTable(request);
            ReturnList.PrintIn = ReturnListOptions.PrintIn;
            ReturnList.PrintOut = ReturnListOptions.PrintOut;

            return ReturnList;
        }

    }
}
