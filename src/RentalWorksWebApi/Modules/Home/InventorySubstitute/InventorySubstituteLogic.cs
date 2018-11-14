using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.InventorySubstitute
{
    [FwLogic(Id:"6qtbmpyyZxAS")]
    public class InventorySubstituteLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventorySubstituteRecord inventorySubstitute = new InventorySubstituteRecord();
        InventorySubstituteLoader inventorySubstituteLoader = new InventorySubstituteLoader();
        public InventorySubstituteLogic()
        {
            dataRecords.Add(inventorySubstitute);
            dataLoader = inventorySubstituteLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"kIgc9N0v6BHv", IsPrimaryKey:true)]
        public string InventorySubstituteId { get { return inventorySubstitute.InventorySubstituteId; } set { inventorySubstitute.InventorySubstituteId = value; } }

        [FwLogicProperty(Id:"brwDf1xWbCvQ")]
        public string InventoryId { get { return inventorySubstitute.InventoryId; } set { inventorySubstitute.InventoryId = value; } }

        [FwLogicProperty(Id:"m6pY0JdiHVVy")]
        public string SubstituteInventoryId { get { return inventorySubstitute.SubstituteInventoryId; } set { inventorySubstitute.SubstituteInventoryId = value; } }

        [FwLogicProperty(Id:"vxsL8CkW9OSz", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"OI6pULGzP0wl", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"9GOs2j8LWlrJ", IsReadOnly:true)]
        public string ManufacturerId { get; set; }

        [FwLogicProperty(Id:"9GOs2j8LWlrJ", IsReadOnly:true)]
        public string Manufacturer { get; set; }

        [FwLogicProperty(Id:"gP4KHJ7GkXfb", IsReadOnly:true)]
        public decimal? Rate { get; set; }

        [FwLogicProperty(Id:"OCCrfHc1cPC7")]
        public string DateStamp { get { return inventorySubstitute.DateStamp; } set { inventorySubstitute.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
