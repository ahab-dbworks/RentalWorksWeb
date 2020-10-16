using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Text;
using WebApi.Controllers;

namespace WebApi.Modules.Reports.Shared.InventoryAttributesReport
{

    public class InventoryAttributesReportRequest : AppReportRequest
    {
        public CheckBoxListItems SortBy { get; set; } = new CheckBoxListItems();
        public string InventoryTypeId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string InventoryId { get; set; }
        public string AttributeId { get; set; }
        public IncludeExcludeAll FixedAsset { get; set; }  // only applies to Rentals
        public SelectedCheckBoxListItems Ranks { get; set; } = new SelectedCheckBoxListItems();
    }


    [FwSqlTable("dbo.funcinventoryattributerpt()")]
    public abstract class InventoryAttributesReportLoader : AppReportLoader
    {
        protected string AvailableForFilter = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterorderby", modeltype: FwDataTypes.Integer)]
        public int? InventoryOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailableFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentorderby", modeltype: FwDataTypes.Integer)]
        public int? InventoryTypeOrderby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryorderby", modeltype: FwDataTypes.Integer)]
        public int? CategoryOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryorderby", modeltype: FwDataTypes.Integer)]
        public int? SubCategoryOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attributeid", modeltype: FwDataTypes.Text)]
        public string AttributeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attribute", modeltype: FwDataTypes.Text)]
        public string Attribute { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attributevalueid", modeltype: FwDataTypes.Text)]
        public string AttributevalueId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attributevalue", modeltype: FwDataTypes.Text)]
        public string AttributeValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "numericvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? NumericValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "numericonly", modeltype: FwDataTypes.Boolean)]
        public bool? NumericOnly { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fixedasset", modeltype: FwDataTypes.Boolean)]
        public bool? IsFixedAsset { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Text)]
        public string Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unit", modeltype: FwDataTypes.Text)]
        public string Unit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manufacturer", modeltype: FwDataTypes.Text)]
        public string Manufacturer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partnumber", modeltype: FwDataTypes.Text)]
        public string ManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgurl", modeltype: FwDataTypes.Text)]
        public string ManufacturerUrl { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? IsInactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "classification", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nondiscountable", modeltype: FwDataTypes.Boolean)]
        public bool? IsNonDiscountable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hazardousmaterial", modeltype: FwDataTypes.Boolean)]
        public bool? IsHazardousMaterial { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(InventoryAttributesReportRequest request)
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
                    select.AddWhere("(availfor = '" + AvailableForFilter + "')");
                    select.AddWhereIn("inventorydepartmentid", request.InventoryTypeId);
                    select.AddWhereIn("categoryid", request.CategoryId);
                    select.AddWhereIn("subcategoryid", request.SubCategoryId);
                    select.AddWhereIn("masterid", request.InventoryId);
                    select.AddWhereIn("attributeid", request.AttributeId);
                    select.AddWhereIn("rank", request.Ranks.ToString());
                    if (AvailableForFilter.Equals(RwConstants.INVENTORY_AVAILABLE_FOR_RENT))
                    {
                        if (request.FixedAsset.Equals(IncludeExcludeAll.IncludeOnly))
                        {
                            select.AddWhere("fixedasset = 'T'");
                        }
                        else if (request.FixedAsset.Equals(IncludeExcludeAll.Exclude))
                        {
                            select.AddWhere("fixedasset <> 'T'");
                        }
                    }

                    StringBuilder orderBy = new StringBuilder();
                    if ((request.SortBy == null) || (request.SortBy.Count.Equals(0)))
                    {
                        orderBy.Append("inventorydepartmentorderby, categoryorderby, subcategoryorderby, masterorderby, masterno");
                    }
                    else
                    {
                        foreach (CheckBoxListItem item in request.SortBy)
                        {
                            if (item.selected.GetValueOrDefault(false))
                            {
                                if (!orderBy.ToString().Equals(string.Empty))
                                {
                                    orderBy.Append(",");
                                }
                                orderBy.Append(item.value.Equals("INVENTORYTYPE") ? "inventorydepartmentorderby" : "");
                                orderBy.Append(item.value.Equals("CATEGORY") ? "categoryorderby" : "");
                                orderBy.Append(item.value.Equals("SUBCATEGORY") ? "subcategoryorderby" : "");
                                orderBy.Append(item.value.Equals("ICODE") ? "masterno" : "");
                                orderBy.Append(item.value.Equals("ATTRIBUTE") ? "attribute,attributevalue" : "");
                            }
                        }

                    }
                    select.AddOrderBy(orderBy.ToString());

                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }

            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
