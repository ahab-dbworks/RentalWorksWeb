using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.PhysicalInventoryQuantityInventory
{
    [FwLogic(Id: "gF1JGE2jYR3hb")]
    public class PhysicalInventoryQuantityInventoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PhysicalInventoryQuantityInventoryRecord physicalInventoryQuantityInventory = new PhysicalInventoryQuantityInventoryRecord();
        PhysicalInventoryQuantityInventoryLoader physicalInventoryQuantityInventoryLoader = new PhysicalInventoryQuantityInventoryLoader();
        public PhysicalInventoryQuantityInventoryLogic()
        {
            dataRecords.Add(physicalInventoryQuantityInventory);
            dataLoader = physicalInventoryQuantityInventoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "ipB7LOnAnwUJK", IsPrimaryKey: true)]
        public int? PhysicalInventoryItemId { get { return physicalInventoryQuantityInventory.PhysicalInventoryItemId; } set { physicalInventoryQuantityInventory.PhysicalInventoryItemId = value; } }
        [FwLogicProperty(Id: "zrP17kmbH0z2d")]
        public string InventoryId { get { return physicalInventoryQuantityInventory.InventoryId; } set { physicalInventoryQuantityInventory.InventoryId = value; } }
        [FwLogicProperty(Id: "O7bke46QE8daq", IsReadOnly: true)]
        public string InventoryType { get; set; }
        [FwLogicProperty(Id: "27QAaVp5IDbcG", IsReadOnly: true)]
        public string Category { get; set; }
        [FwLogicProperty(Id: "LcBdpMbTZKpNb", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "dS5wyC6ZKo0Ht", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "qb80A9dnGGfq2", IsReadOnly: true)]
        public string AisleLocation { get; set; }
        [FwLogicProperty(Id: "8KFQJVxkNjxFV", IsReadOnly: true)]
        public string ShelfLocation { get; set; }
        [FwLogicProperty(Id: "R8Jm7QAFnDk1r", IsReadOnly: true)]
        public string CategoryId { get; set; }
        [FwLogicProperty(Id: "HT4zFR6b12yo6", IsReadOnly: true)]
        public decimal? CategoryOrderBy { get; set; }
        [FwLogicProperty(Id: "2OefVlAR9qckg", IsReadOnly: true)]
        public int? InventoryTypeOrderBy { get; set; }
        [FwLogicProperty(Id: "NRIqdWwTMfZ0p", IsReadOnly: true)]
        public string InventoryTypeId { get; set; }
        [FwLogicProperty(Id: "nNs4MFBCaI3eW", IsReadOnly: true)]
        public string UnitId { get; set; }
        [FwLogicProperty(Id: "A3cdUGw2G77kx", IsReadOnly: true)]
        public string WeightUnitId { get; set; }
        [FwLogicProperty(Id: "FzSGHJuNOPxrJ", IsReadOnly: true)]
        public string LengthUnitId { get; set; }
        [FwLogicProperty(Id: "JSzXILqeGEaGU")]
        public int? SessionQuantity { get { return physicalInventoryQuantityInventory.SessionQuantity; } set { physicalInventoryQuantityInventory.SessionQuantity = value; } }
        [FwLogicProperty(Id: "k4fSdQ6pYpbRx")]
        public int? Quantity { get { return physicalInventoryQuantityInventory.Quantity; } set { physicalInventoryQuantityInventory.Quantity = value; } }
        [FwLogicProperty(Id: "h7QjHMAROt8fg", IsReadOnly: true)]
        public int? CurrentQuantity { get; set; }
        [FwLogicProperty(Id: "RCFsDKT77MZQx")]
        public int? Weight { get { return physicalInventoryQuantityInventory.Weight; } set { physicalInventoryQuantityInventory.Weight = value; } }
        [FwLogicProperty(Id: "ks7OxMLfkbaOp")]
        public int? Length { get { return physicalInventoryQuantityInventory.Length; } set { physicalInventoryQuantityInventory.Length = value; } }
        [FwLogicProperty(Id: "W1qTQ8K2O5Ii8", IsReadOnly: true)]
        public string Unit { get; set; }
        [FwLogicProperty(Id: "YkCaZEMtr0FN3", IsReadOnly: true)]
        public string WeightUnit { get; set; }
        [FwLogicProperty(Id: "UVDdwM9HLtojC", IsReadOnly: true)]
        public string LengthUnit { get; set; }
        [FwLogicProperty(Id: "QWZCghkpnhfxG")]
        public string CurrentSpaceId { get { return physicalInventoryQuantityInventory.CurrentSpaceId; } set { physicalInventoryQuantityInventory.CurrentSpaceId = value; } }
        [FwLogicProperty(Id: "IugcbP5YbZIJv", IsReadOnly: true)]
        public string CurrentSpace { get; set; }
        [FwLogicProperty(Id: "V5Uxrl1LLiSjd", IsReadOnly: true)]
        public bool? IsRecount { get; set; }
        [FwLogicProperty(Id: "IdhVsKlhwlecb")]
        public string ConsignorId { get { return physicalInventoryQuantityInventory.ConsignorId; } set { physicalInventoryQuantityInventory.ConsignorId = value; } }
        [FwLogicProperty(Id: "ytgnfnaSMSslM", IsReadOnly: true)]
        public string Consignor { get; set; }
        [FwLogicProperty(Id: "1UZiD8YiyNWtU")]
        public string PhysicalInventoryId { get { return physicalInventoryQuantityInventory.PhysicalInventoryId; } set { physicalInventoryQuantityInventory.PhysicalInventoryId = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
