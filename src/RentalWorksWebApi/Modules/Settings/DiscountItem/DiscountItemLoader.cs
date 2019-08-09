using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
namespace WebApi.Modules.Settings.DiscountItem
{
    [FwSqlTable("discountitemview")]
    public class DiscountItemLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountitemid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string DiscountItemId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? DiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "daysinwk", modeltype: FwDataTypes.Decimal)]
        public decimal? DaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "marginpct", modeltype: FwDataTypes.Decimal)]
        public decimal? MarginPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markuppct", modeltype: FwDataTypes.Decimal)]
        public decimal? MarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week2rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week2rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week3rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week4rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week5rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week5Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeorderby", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderTypeOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentorderby", modeltype: FwDataTypes.Integer)]
        public int? InventoryTypeOrderBy { get; set; }
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
        public decimal? SubCategoryOrderBy { get; set; }
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
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whdailyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? WarehouseDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whweeklyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? WarehouseWeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whdefaultdailyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? WarehouseDefaultDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whdefaultweeklyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? WarehouseDefaultWeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discounttemplateid", modeltype: FwDataTypes.Text)]
        public string DiscountTemplateId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("DiscountTemplateId", "discounttemplateid", select, request);
            addFilterToSelect("RecType", "rectype", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
