using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Home.InventorySubstitute
{
    public class InventorySubstituteLogic : RwBusinessLogic
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryId { get { return inventorySubstitute.InventoryId; } set { inventorySubstitute.InventoryId = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string SubstituteInventoryId { get { return inventorySubstitute.SubstituteInventoryId; } set { inventorySubstitute.SubstituteInventoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ManufacturerId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Manufacturer { get; set; }
        public string DateStamp { get { return inventorySubstitute.DateStamp; } set { inventorySubstitute.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}