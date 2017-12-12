using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Home.ItemQc
{
    public class ItemQcLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ItemQcRecord itemQc = new ItemQcRecord();
        ItemQcLoader itemQcLoader = new ItemQcLoader();
        public ItemQcLogic()
        {
            dataRecords.Add(itemQc);
            dataLoader = itemQcLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ItemQcId { get { return itemQc.ItemQcId; } set { itemQc.ItemQcId = value; } }
        public string ItemId { get { return itemQc.ItemId; } set { itemQc.ItemId = value; } }
        public string QcRequiredAsOf { get { return itemQc.QcRequiredAsOf; } set { itemQc.QcRequiredAsOf = value; } }
        public string QcByUsersId { get { return itemQc.QcByUsersId; } set { itemQc.QcByUsersId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string QcByUser { get; set; }
        public string QcDateTime { get { return itemQc.QcDateTime; } set { itemQc.QcDateTime = value; } }
        public string LastOrderId { get { return itemQc.LastOrderId; } set { itemQc.LastOrderId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LastOrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LastOrderDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LastDealId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LastDealNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LastDeal { get; set; }
        public string ConditionId { get { return itemQc.ConditionId; } set { itemQc.ConditionId = value; } }
        public string InContractId { get { return itemQc.InContractId; } set { itemQc.InContractId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Condition { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Note { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasImage { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Datstamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}