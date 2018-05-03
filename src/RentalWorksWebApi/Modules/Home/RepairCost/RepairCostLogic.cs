using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.RepairCost
{
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string RepairCostId { get { return repairCost.RepairCostId; } set { repairCost.RepairCostId = value; } }
        public string RepairId { get { return repairCost.RepairId; } set { repairCost.RepairId = value; } }
        public string RateId { get { return repairCost.RateId; } set { repairCost.RateId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        public string Description { get { return repairCost.Description; } set { repairCost.Description = value; } }
        public decimal? Quantity { get { return repairCost.Quantity; } set { repairCost.Quantity = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Unit { get; set; }
        public decimal? Rate { get { return repairCost.Rate; } set { repairCost.Rate = value; } }
        public decimal? DiscountAmount { get { return repairCost.DiscountAmount; } set { repairCost.DiscountAmount = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Extended { get; set; }
        public bool? Taxable { get { return repairCost.Taxable; } set { repairCost.Taxable = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Tax { get; set; }
        public bool? Billable { get { return repairCost.Billable; } set { repairCost.Billable = value; } }
        public string Note { get { return repairCost.Note; } set { repairCost.Note = value; } }
        public string DateStamp { get { return repairCost.DateStamp; } set { repairCost.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
