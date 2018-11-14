using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.DiscountItem
{
    [FwLogic(Id:"pO8cVGf6Dd9W")]
    public class DiscountItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DiscountItemRecord discountItem = new DiscountItemRecord();
        DiscountItemLoader discountItemLoader = new DiscountItemLoader();
        public DiscountItemLogic()
        {
            dataRecords.Add(discountItem);
            dataLoader = discountItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"C0VUf9LvjaKD", IsPrimaryKey:true)]
        public string DiscountItemId { get { return discountItem.DiscountItemId; } set { discountItem.DiscountItemId = value; } }

        [FwLogicProperty(Id:"wXejnuXAJHSV")]
        public decimal? DiscountPercent { get { return discountItem.DiscountPercent; } set { discountItem.DiscountPercent = value; } }

        [FwLogicProperty(Id:"4TIkGptQkk7e")]
        public decimal? DaysPerWeek { get { return discountItem.DaysPerWeek; } set { discountItem.DaysPerWeek = value; } }

        [FwLogicProperty(Id:"ofBCkDUqo9wt")]
        public decimal? MarginPercent { get { return discountItem.MarginPercent; } set { discountItem.MarginPercent = value; } }

        [FwLogicProperty(Id:"zL5XlqRlXHp6")]
        public decimal? MarkupPercent { get { return discountItem.MarkupPercent; } set { discountItem.MarkupPercent = value; } }

        [FwLogicProperty(Id:"dPi9EkAOROp1")]
        public decimal? DailyRate { get { return discountItem.DailyRate; } set { discountItem.DailyRate = value; } }

        [FwLogicProperty(Id:"xYBghgZgFGRi")]
        public decimal? WeeklyRate { get { return discountItem.WeeklyRate; } set { discountItem.WeeklyRate = value; } }

        [FwLogicProperty(Id:"HVonFcMosZte")]
        public decimal? Week2Rate { get { return discountItem.Week2Rate; } set { discountItem.Week2Rate = value; } }

        [FwLogicProperty(Id:"IBBW4kKnDFG4")]
        public decimal? Week3Rate { get { return discountItem.Week3Rate; } set { discountItem.Week3Rate = value; } }

        [FwLogicProperty(Id:"7tZSXXyPXisb")]
        public decimal? Week4Rate { get { return discountItem.Week4Rate; } set { discountItem.Week4Rate = value; } }

        [FwLogicProperty(Id:"NkdlQPOC9OyK")]
        public decimal? Week5Rate { get { return discountItem.Week5Rate; } set { discountItem.Week5Rate = value; } }

        [FwLogicProperty(Id:"AgSOZKaPUfK0")]
        public decimal? MonthlyRate { get { return discountItem.MonthlyRate; } set { discountItem.MonthlyRate = value; } }

        [FwLogicProperty(Id:"8s8e0qKHEVmw")]
        public string OrderTypeId { get { return discountItem.OrderTypeId; } set { discountItem.OrderTypeId = value; } }

        [FwLogicProperty(Id:"bydTfIzQUKbr", IsReadOnly:true)]
        public string OrderType { get; set; }

        [FwLogicProperty(Id:"bydTfIzQUKbr", IsReadOnly:true)]
        public decimal? OrderTypeOrderBy { get; set; }

        [FwLogicProperty(Id:"lLWkVOV0dMS6")]
        public string InventoryTypeId { get { return discountItem.InventoryTypeId; } set { discountItem.InventoryTypeId = value; } }

        [FwLogicProperty(Id:"4FYJxbuNIfDf", IsReadOnly:true)]
        public string InventoryType { get; set; }

        [FwLogicProperty(Id:"4FYJxbuNIfDf", IsReadOnly:true)]
        public int? InventoryTypeOrderBy { get; set; }

        [FwLogicProperty(Id:"3R1n4d7ZJFtc")]
        public string CategoryId { get { return discountItem.CategoryId; } set { discountItem.CategoryId = value; } }

        [FwLogicProperty(Id:"yYqec5A76PuU", IsReadOnly:true)]
        public string Category { get; set; }

        [FwLogicProperty(Id:"yYqec5A76PuU", IsReadOnly:true)]
        public decimal? CategoryOrderBy { get; set; }

        [FwLogicProperty(Id:"RlIxqsnAxYOH")]
        public string SubCategoryId { get { return discountItem.SubCategoryId; } set { discountItem.SubCategoryId = value; } }

        [FwLogicProperty(Id:"yYqec5A76PuU", IsReadOnly:true)]
        public string SubCategory { get; set; }

        [FwLogicProperty(Id:"yYqec5A76PuU", IsReadOnly:true)]
        public decimal? SubCategoryOrderBy { get; set; }

        [FwLogicProperty(Id:"5BrjKqKuE8yr")]
        public string InventoryId  { get { return discountItem.InventoryId; } set { discountItem.InventoryId = value; } }

        [FwLogicProperty(Id:"uVLuzcYF5eLD", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"oHJEbpjLKv6Z", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"no0Jwp6Clmqh")]
        public string RecType { get { return discountItem.RecType; } set { discountItem.RecType = value; } }

        [FwLogicProperty(Id:"9d9iH2fH2xRW", IsReadOnly:true)]
        public string Classification { get; set; }

        [FwLogicProperty(Id:"PwSouxmAbNi8", IsReadOnly:true)]
        public decimal? WarehouseDailyRate { get; set; }

        [FwLogicProperty(Id:"wcX76HRP6pyK", IsReadOnly:true)]
        public decimal? WarehouseWeeklyRate { get; set; }

        [FwLogicProperty(Id:"PwSouxmAbNi8", IsReadOnly:true)]
        public decimal? WarehouseDefaultDailyRate { get; set; }

        [FwLogicProperty(Id:"7utkpcedDKP7", IsReadOnly:true)]
        public decimal? WarehouseDefaultWeeklyRate { get; set; }

        [FwLogicProperty(Id:"No4o43v8Fyyk")]
        public string DiscountTemplateId { get { return discountItem.DiscountTemplateId; } set { discountItem.DiscountTemplateId = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
