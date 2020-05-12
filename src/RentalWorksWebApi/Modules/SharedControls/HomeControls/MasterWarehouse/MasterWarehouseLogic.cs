using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.MasterWarehouse
{
    [FwLogic(Id:"xeta3vkEWo3O")]
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
        //[FwLogicProperty(Id:"tF3S2Wcp8NMR", IsPrimaryKey:true)]
        //public string MasterId { get { return masterWarehouse.MasterId; } set { masterWarehouse.MasterId = value; } }

        //[FwLogicProperty(Id:"c1PUrSxKjHoK", IsReadOnly:true)]
        //public string ICode { get; set; }

        [FwLogicProperty(Id:"uc4hQpO7ZTZq", IsReadOnly:true)]
        public string ItemDescription { get; set; }

        [FwLogicProperty(Id:"hrnHCLcmqHiY", IsPrimaryKey:true)]
        public string WarehouseId { get { return masterWarehouse.WarehouseId; } set { masterWarehouse.WarehouseId = value; } }

        [FwLogicProperty(Id:"RVFuutK3vUg3", IsReadOnly:true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"1YIBbxvSTfm0", IsReadOnly:true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id: "guLzUpMJ60Dcb", IsReadOnly: true, IsNotAudited: true)]
        public int? WarehouseOrderBy { get; set; }


        //[FwLogicProperty(Id:"pVWKGqtT6FD4", IsReadOnly:true)]
        //public decimal? Qty { get; set; }

        //[FwLogicProperty(Id:"DKP6aYauBAym")]
        //public decimal? AverageCost { get { return masterWarehouse.AverageCost; } set { masterWarehouse.AverageCost = value; } }

        //[FwLogicProperty(Id:"Zj5UFSc097Nv", IsReadOnly:true)]
        //public decimal? HourlyRate { get { return masterWarehouse.HourlyRate; } set { masterWarehouse.HourlyRate = value; } }

        //[FwLogicProperty(Id:"GzzE8be8CE71")]
        //public decimal? HourlyCost { get { return masterWarehouse.HourlyCost; } set { masterWarehouse.HourlyCost = value; } }

        //[FwLogicProperty(Id:"VRH7UWQnsftu")]
        //public decimal? DailyRate { get { return masterWarehouse.DailyRate; } set { masterWarehouse.DailyRate = value; } }

        //[FwLogicProperty(Id:"OPRnBFRu24Yd")]
        //public decimal? DailyCost { get { return masterWarehouse.DailyCost; } set { masterWarehouse.DailyCost = value; } }

        //[FwLogicProperty(Id:"sA0wwdNWVDbX")]
        //public decimal? WeeklyRate { get { return masterWarehouse.WeeklyRate; } set { masterWarehouse.WeeklyRate = value; } }

        //[FwLogicProperty(Id:"oM35O69R0YC0")]
        //public decimal? Week2Rate { get { return masterWarehouse.Week2Rate; } set { masterWarehouse.Week2Rate = value; } }

        //[FwLogicProperty(Id:"5JsGUV6Ck76e")]
        //public decimal? Week3Rate { get { return masterWarehouse.Week3Rate; } set { masterWarehouse.Week3Rate = value; } }

        //[FwLogicProperty(Id:"Guk00HL0Rh6X")]
        //public decimal? Week4Rate { get { return masterWarehouse.Week4Rate; } set { masterWarehouse.Week4Rate = value; } }

        //[FwLogicProperty(Id:"X3aPlKeZzwTM")]
        //public decimal? Week5Rate { get { return masterWarehouse.Week5Rate; } set { masterWarehouse.Week5Rate = value; } }

        //[FwLogicProperty(Id:"dc1Bt4ijVnXr")]
        //public decimal? WeeklyCost { get { return masterWarehouse.WeeklyCost; } set { masterWarehouse.WeeklyCost = value; } }

        //[FwLogicProperty(Id:"96GQSRfumgU9")]
        //public decimal? MonthlyRate { get { return masterWarehouse.MonthlyRate; } set { masterWarehouse.MonthlyRate = value; } }

        //[FwLogicProperty(Id:"hKMGm3DHAn93")]
        //public decimal? MonthlyCost { get { return masterWarehouse.MonthlyCost; } set { masterWarehouse.MonthlyCost = value; } }


        //[FwLogicProperty(Id:"5jvQ8mNxinX5")]
        //public decimal? DefaultCost { get { return masterWarehouse.DefaultCost; } set { masterWarehouse.DefaultCost = value; } }

        //[FwLogicProperty(Id:"mCvl2XoyDa2f")]
        //public decimal? Price { get { return masterWarehouse.Price; } set { masterWarehouse.Price = value; } }

        //[FwLogicProperty(Id:"41qAQ7dINVVx")]
        //public decimal? Retail { get { return masterWarehouse.Retail; } set { masterWarehouse.Retail = value; } }

        //[FwLogicProperty(Id:"uTfzIlLik3CE")]
        //public int? ReorderPoint { get { return masterWarehouse.ReorderPoint; } set { masterWarehouse.ReorderPoint = value; } }

        //[FwLogicProperty(Id:"dJDNCHuIaJEo")]
        //public int? Reorderqty { get { return masterWarehouse.Reorderqty; } set { masterWarehouse.Reorderqty = value; } }

        //[FwLogicProperty(Id:"FLODDDCEAm3n")]
        //public decimal? Maxdiscount { get { return masterWarehouse.Maxdiscount; } set { masterWarehouse.Maxdiscount = value; } }

        //[FwLogicProperty(Id:"uiWbfYM4Wyzm")]
        //public string Aisleloc { get { return masterWarehouse.Aisleloc; } set { masterWarehouse.Aisleloc = value; } }

        //[FwLogicProperty(Id:"sEfdXxC7Y8n8")]
        //public string Shelfloc { get { return masterWarehouse.Shelfloc; } set { masterWarehouse.Shelfloc = value; } }

        //[FwLogicProperty(Id:"6Rm9gA272GJf")]
        //public bool? Availbyhour { get { return masterWarehouse.Availbyhour; } set { masterWarehouse.Availbyhour = value; } }

        //[FwLogicProperty(Id:"1Wj6J0VU1Bgi")]
        //public bool? Availbydeal { get { return masterWarehouse.Availbydeal; } set { masterWarehouse.Availbydeal = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
