using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Home.MasterWarehouse
{
    public abstract class MasterWarehouseLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        protected MasterWarehouseRecord masterWarehouse = new MasterWarehouseRecord();
        //MasterWarehouseLoader masterWarehouseLoader = new MasterWarehouseLoader();
        public MasterWarehouseLogic()
        {
            dataRecords.Add(masterWarehouse);
            //dataLoader = masterWarehouseLoader;
        }
        //------------------------------------------------------------------------------------ 
        //[FwBusinessLogicField(isPrimaryKey: true)]
        //public string MasterId { get { return masterWarehouse.MasterId; } set { masterWarehouse.MasterId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemDescription { get; set; }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WarehouseId { get { return masterWarehouse.WarehouseId; } set { masterWarehouse.WarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }

        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? Qty { get; set; }
        //public decimal? AverageCost { get { return masterWarehouse.AverageCost; } set { masterWarehouse.AverageCost = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? HourlyRate { get { return masterWarehouse.HourlyRate; } set { masterWarehouse.HourlyRate = value; } }
        //public decimal? HourlyCost { get { return masterWarehouse.HourlyCost; } set { masterWarehouse.HourlyCost = value; } }
        //public decimal? DailyRate { get { return masterWarehouse.DailyRate; } set { masterWarehouse.DailyRate = value; } }
        //public decimal? DailyCost { get { return masterWarehouse.DailyCost; } set { masterWarehouse.DailyCost = value; } }
        //public decimal? WeeklyRate { get { return masterWarehouse.WeeklyRate; } set { masterWarehouse.WeeklyRate = value; } }
        //public decimal? Week2Rate { get { return masterWarehouse.Week2Rate; } set { masterWarehouse.Week2Rate = value; } }
        //public decimal? Week3Rate { get { return masterWarehouse.Week3Rate; } set { masterWarehouse.Week3Rate = value; } }
        //public decimal? Week4Rate { get { return masterWarehouse.Week4Rate; } set { masterWarehouse.Week4Rate = value; } }
        //public decimal? Week5Rate { get { return masterWarehouse.Week5Rate; } set { masterWarehouse.Week5Rate = value; } }
        //public decimal? WeeklyCost { get { return masterWarehouse.WeeklyCost; } set { masterWarehouse.WeeklyCost = value; } }
        //public decimal? MonthlyRate { get { return masterWarehouse.MonthlyRate; } set { masterWarehouse.MonthlyRate = value; } }
        //public decimal? MonthlyCost { get { return masterWarehouse.MonthlyCost; } set { masterWarehouse.MonthlyCost = value; } }

        //public decimal? DefaultCost { get { return masterWarehouse.DefaultCost; } set { masterWarehouse.DefaultCost = value; } }
        //public decimal? Price { get { return masterWarehouse.Price; } set { masterWarehouse.Price = value; } }
        //public decimal? Retail { get { return masterWarehouse.Retail; } set { masterWarehouse.Retail = value; } }
        //public int? ReorderPoint { get { return masterWarehouse.ReorderPoint; } set { masterWarehouse.ReorderPoint = value; } }
        //public int? Reorderqty { get { return masterWarehouse.Reorderqty; } set { masterWarehouse.Reorderqty = value; } }
        //public decimal? Maxdiscount { get { return masterWarehouse.Maxdiscount; } set { masterWarehouse.Maxdiscount = value; } }
        //public string Aisleloc { get { return masterWarehouse.Aisleloc; } set { masterWarehouse.Aisleloc = value; } }
        //public string Shelfloc { get { return masterWarehouse.Shelfloc; } set { masterWarehouse.Shelfloc = value; } }
        //public bool? Availbyhour { get { return masterWarehouse.Availbyhour; } set { masterWarehouse.Availbyhour = value; } }
        //public bool? Availbydeal { get { return masterWarehouse.Availbydeal; } set { masterWarehouse.Availbydeal = value; } }
        //------------------------------------------------------------------------------------ 
    }
}