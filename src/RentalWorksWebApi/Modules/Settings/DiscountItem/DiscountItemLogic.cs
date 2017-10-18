using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic; 
namespace RentalWorksWebApi.Modules.Settings.DiscountItem 
{ 
public class DiscountItemLogic : RwBusinessLogic 
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
[FwBusinessLogicField(isPrimaryKey: true)] 
public string DiscountItemId { get { return discountItem.DiscountItemId; } set { discountItem.DiscountItemId = value; } } 
public decimal? DiscountPercent { get { return discountItem.DiscountPercent; } set { discountItem.DiscountPercent = value; } } 
public decimal? DaysInWeek { get { return discountItem.DaysInWeek; } set { discountItem.Daysinwk = value; } } 
public decimal? MarginPercent { get { return discountItem.MarginPercent; } set { discountItem.MarginPercent = value; } } 
public decimal? MarkupPercent { get { return discountItem.MarkupPercent; } set { discountItem.MarkupPercent = value; } } 
public decimal? DailyRate { get { return discountItem.DailyRate; } set { discountItem.DailyRate = value; } } 
public decimal? WeeklyRate { get { return discountItem.WeeklyRate; } set { discountItem.WeeklyRate = value; } } 
public decimal? Week2Rate { get { return discountItem.Week2Rate; } set { discountItem.Week2Rate = value; } } 
public decimal? Week3Rate { get { return discountItem.Week3Rate; } set { discountItem.Week3Rate = value; } } 
public decimal? Week4Rate { get { return discountItem.Week4Rate; } set { discountItem.Week4Rate = value; } } 
public decimal? Week5Rate { get { return discountItem.Week5Rate; } set { discountItem.Week5Rate = value; } } 
public decimal? MonthlyRate { get { return discountItem.MonthlyRate; } set { discountItem.MonthlyRate = value; } } 
public string OrderTypeId { get { return discountItem.OrderTypeId; } set { discountItem.OrderTypeId = value; } } 
[FwBusinessLogicField(isReadOnly: true)] 
public string Ordertype { get; set; } 
[FwBusinessLogicField(isReadOnly: true)] 
public decimal? OrderTypeOrderBy { get; set; } 
public string InventoryDepartmentId { get { return discountItem.InventoryDepartmentId; } set { discountItem.InventoryDepartmentId = value; } } 
[FwBusinessLogicField(isReadOnly: true)] 
public string InventoryDepartment { get; set; } 
[FwBusinessLogicField(isReadOnly: true)] 
public int? DepartmentOrderBy { get; set; } 
public string CategoryId { get { return discountItem.CategoryId; } set { discountItem.CategoryId = value; } } 
[FwBusinessLogicField(isReadOnly: true)] 
public string Category { get; set; } 
[FwBusinessLogicField(isReadOnly: true)] 
public decimal? CategoryOrderBy { get; set; } 
public string SubCategoryId { get { return discountItem.SubCategoryId; } set { discountItem.SubCategoryId = value; } } 
[FwBusinessLogicField(isReadOnly: true)] 
public string SubCategory { get; set; } 
[FwBusinessLogicField(isReadOnly: true)] 
public decimal? SubCategoryOrderBy { get; set; } 
public string MasterId { get { return discountItem.MasterId; } set { discountItem.MasterId = value; } } 
[FwBusinessLogicField(isReadOnly: true)] 
public string Masterno { get; set; } 
[FwBusinessLogicField(isReadOnly: true)] 
public string Description { get; set; } 
public string RecType { get { return discountItem.RecType; } set { discountItem.RecType = value; } } 
[FwBusinessLogicField(isReadOnly: true)] 
public string Class { get; set; } 
[FwBusinessLogicField(isReadOnly: true)] 
public decimal? WhDailyRate { get; set; } 
[FwBusinessLogicField(isReadOnly: true)] 
public decimal? WhWeeklyRate { get; set; } 
[FwBusinessLogicField(isReadOnly: true)] 
public decimal? WhDefaultDailyRate { get; set; } 
[FwBusinessLogicField(isReadOnly: true)] 
public decimal? WhDefaultWeeklyRate { get; set; } 
public string DiscountTemplateId { get { return discountItem.DiscountTemplateId; } set { discountItem.DiscountTemplateId = value; } } 
//------------------------------------------------------------------------------------ 
} 
} 
