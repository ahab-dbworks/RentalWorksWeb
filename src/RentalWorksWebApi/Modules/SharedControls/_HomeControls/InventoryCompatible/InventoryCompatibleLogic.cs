using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.InventoryCompatible
{
    [FwLogic(Id:"qDPKspOKwKal")]
    public class InventoryCompatibleLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryCompatibleRecord inventoryCompatible = new InventoryCompatibleRecord();
        InventoryCompatibleLoader inventoryCompatibleLoader = new InventoryCompatibleLoader();
        public InventoryCompatibleLogic()
        {
            dataRecords.Add(inventoryCompatible);
            dataLoader = inventoryCompatibleLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"5SqyfprKuJo5", IsPrimaryKey:true)]
        public string InventoryCompatibleId { get { return inventoryCompatible.InventoryCompatibleId; } set { inventoryCompatible.InventoryCompatibleId = value; } }

        [FwLogicProperty(Id:"cyMTo3i9DaIa")]
        public string InventoryId { get { return inventoryCompatible.InventoryId; } set { inventoryCompatible.InventoryId = value; } }

        [FwLogicProperty(Id:"xOn3wWM6ZXKt", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"jaXJ6OkPgIjc", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"wW8Fn10loUZu")]
        public string CompatibleWithInventoryId { get { return inventoryCompatible.CompatibleWithInventoryId; } set { inventoryCompatible.CompatibleWithInventoryId = value; } }

        [FwLogicProperty(Id:"eppnqCBjhqn9", IsReadOnly:true)]
        public string CompatibleWithICode { get; set; }

        [FwLogicProperty(Id:"ufbJKG4apzdU", IsReadOnly:true)]
        public string CompatibleWithDescription { get; set; }

        [FwLogicProperty(Id:"fnR3VAFdT53L", IsReadOnly:true)]
        public string CompatibleWithClassification { get; set; }

        [FwLogicProperty(Id:"zRRZTRLucoPX")]
        public string DateStamp { get { return inventoryCompatible.DateStamp; } set { inventoryCompatible.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
