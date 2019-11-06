using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Reports.Shared.InventoryChangeTransactionType
{
    [FwLogic(Id: "TnVI2E0MuBgcP")]
    public class InventoryChangeTransactionTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryChangeTransactionTypeLoader inventoryChangeTransactionTypeLoader = new InventoryChangeTransactionTypeLoader();
        public InventoryChangeTransactionTypeLogic()
        {
            dataLoader = inventoryChangeTransactionTypeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "PJIo4Qw7pAQVZ", IsReadOnly: true)]
        public string TransactionType { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
