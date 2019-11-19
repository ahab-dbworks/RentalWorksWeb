using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.ItemQc
{
    [FwLogic(Id:"tcgJS4ex1aKW")]
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
        [FwLogicProperty(Id:"sQ6qQBvPoKq2", IsPrimaryKey:true)]
        public string ItemQcId { get { return itemQc.ItemQcId; } set { itemQc.ItemQcId = value; } }

        [FwLogicProperty(Id:"kPHOj2sxYzOF")]
        public string ItemId { get { return itemQc.ItemId; } set { itemQc.ItemId = value; } }

        [FwLogicProperty(Id:"HVKS7DefHth5")]
        public string QcRequiredAsOf { get { return itemQc.QcRequiredAsOf; } set { itemQc.QcRequiredAsOf = value; } }

        [FwLogicProperty(Id:"HTzIbGsLVYtW")]
        public string QcByUsersId { get { return itemQc.QcByUsersId; } set { itemQc.QcByUsersId = value; } }

        [FwLogicProperty(Id:"kZzlwuL39SUL", IsReadOnly:true)]
        public string QcByUser { get; set; }

        [FwLogicProperty(Id:"yZOnbnZI9LSO")]
        public string QcDateTime { get { return itemQc.QcDateTime; } set { itemQc.QcDateTime = value; } }

        [FwLogicProperty(Id:"kMdh7XQVmj1K")]
        public string LastOrderId { get { return itemQc.LastOrderId; } set { itemQc.LastOrderId = value; } }

        [FwLogicProperty(Id:"P15og1we7155", IsReadOnly:true)]
        public string LastOrderNumber { get; set; }

        [FwLogicProperty(Id:"IldXZzkWmGGf", IsReadOnly:true)]
        public string LastOrderDescription { get; set; }

        [FwLogicProperty(Id:"Ha2gtN6m39N2", IsReadOnly:true)]
        public string LastDealId { get; set; }

        [FwLogicProperty(Id:"Ha2gtN6m39N2", IsReadOnly:true)]
        public string LastDealNumber { get; set; }

        [FwLogicProperty(Id:"Ha2gtN6m39N2", IsReadOnly:true)]
        public string LastDeal { get; set; }

        [FwLogicProperty(Id:"OPmqmvzhgXS7")]
        public string ConditionId { get { return itemQc.ConditionId; } set { itemQc.ConditionId = value; } }

        [FwLogicProperty(Id:"t3QeEbMh9o2p")]
        public string InContractId { get { return itemQc.InContractId; } set { itemQc.InContractId = value; } }

        [FwLogicProperty(Id:"qFP7fqj0Ik0i", IsReadOnly:true)]
        public string Condition { get; set; }

        [FwLogicProperty(Id:"cMQ8wcsKa3FJ", IsReadOnly:true)]
        public string Note { get; set; }

        [FwLogicProperty(Id:"LhM3v7hesjuO", IsReadOnly:true)]
        public bool? HasImage { get; set; }

        [FwLogicProperty(Id:"bBt0lDIjW2VM", IsReadOnly:true)]
        public string Datstamp { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
