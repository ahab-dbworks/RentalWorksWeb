using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.RepairCost
{
    [FwLogic(Id:"fyLsarwsFgRh1")]
    public class RepairCostLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        RepairCostRecord repairCost = new RepairCostRecord();
        RepairCostLoader repairCostLoader = new RepairCostLoader();
        public RepairCostLogic()
        {
            dataRecords.Add(repairCost);
            dataLoader = repairCostLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"UqBEEmkuoldzS", IsPrimaryKey:true)]
        public string RepairCostId { get { return repairCost.RepairCostId; } set { repairCost.RepairCostId = value; } }

        [FwLogicProperty(Id:"ylOjXPFxxBc9")]
        public string RepairId { get { return repairCost.RepairId; } set { repairCost.RepairId = value; } }

        [FwLogicProperty(Id:"00xte7FlFdEw")]
        public string RateId { get { return repairCost.RateId; } set { repairCost.RateId = value; } }

        [FwLogicProperty(Id:"2TsQ4Nxz5GU2m", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"aemsntzuPSrM")]
        public string Description { get { return repairCost.Description; } set { repairCost.Description = value; } }

        [FwLogicProperty(Id:"ng9TYWqC6LXP")]
        public decimal? Quantity { get { return repairCost.Quantity; } set { repairCost.Quantity = value; } }

        [FwLogicProperty(Id:"E8jHYOT8gGfl0", IsReadOnly:true)]
        public string Unit { get; set; }

        [FwLogicProperty(Id:"zLLBO214AnRO")]
        public decimal? Rate { get { return repairCost.Rate; } set { repairCost.Rate = value; } }

        [FwLogicProperty(Id: "yKFpkf6eXliaa", IsReadOnly: true)]
        public decimal? GrossTotal { get; set; }

        [FwLogicProperty(Id:"DojGTe1SuP0p")]
        public decimal? DiscountAmount { get { return repairCost.DiscountAmount; } set { repairCost.DiscountAmount = value; } }

        [FwLogicProperty(Id:"KtbdlqinOzs8k", IsReadOnly:true)]
        public decimal? Extended { get; set; }

        [FwLogicProperty(Id:"RVlxtx9sbT02")]
        public bool? Taxable { get { return repairCost.Taxable; } set { repairCost.Taxable = value; } }

        [FwLogicProperty(Id:"9CxEjLMiVFKAQ", IsReadOnly:true)]
        public decimal? Tax { get; set; }

        [FwLogicProperty(Id: "AKtSlerPXj2Tq", IsReadOnly: true)]
        public decimal? Total { get; set; }

        [FwLogicProperty(Id:"qITg35IMtxPz")]
        public bool? Billable { get { return repairCost.Billable; } set { repairCost.Billable = value; } }

        [FwLogicProperty(Id:"tH8VOGTmCkIL")]
        public string Note { get { return repairCost.Note; } set { repairCost.Note = value; } }

        [FwLogicProperty(Id: "jBAsNeMKbsq9", IsReadOnly: true)]
        public string CurrencySymbol { get; set; }

        [FwLogicProperty(Id:"9Nznde1Vsifs")]
        public string DateStamp { get { return repairCost.DateStamp; } set { repairCost.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
