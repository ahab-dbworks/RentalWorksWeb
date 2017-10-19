using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.InventoryRank
{
    public class InventoryRankLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryRankRecord inventoryRank = new InventoryRankRecord();
        InventoryRankLoader inventoryRankLoader = new InventoryRankLoader();
        public InventoryRankLogic()
        {
            dataRecords.Add(inventoryRank);
            dataLoader = inventoryRankLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryRankId { get { return inventoryRank.InventoryRankId; } set { inventoryRank.InventoryRankId = value; } }
        public string InventoryTypeId { get { return inventoryRank.InventoryTypeId; } set { inventoryRank.InventoryTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string InventoryType { get; set; }
        public string WarehouseId { get { return inventoryRank.WarehouseId; } set { inventoryRank.WarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string Warehouse { get; set; }
        public string Type { get { return inventoryRank.Type; } set { inventoryRank.Type = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TypeDisplay { get; set; }
        public decimal? AFromValue { get { return inventoryRank.AFromValue; } set { inventoryRank.AFromValue = value; } }
        public decimal? AToValue { get { return inventoryRank.AToValue; } set { inventoryRank.AToValue = value; } }
        public decimal? BFromValue { get { return inventoryRank.BFromValue; } set { inventoryRank.BFromValue = value; } }
        public decimal? BToValue { get { return inventoryRank.BToValue; } set { inventoryRank.BToValue = value; } }
        public decimal? CFromValue { get { return inventoryRank.CFromValue; } set { inventoryRank.CFromValue = value; } }
        public decimal? CToValue { get { return inventoryRank.CToValue; } set { inventoryRank.CToValue = value; } }
        public decimal? DFromValue { get { return inventoryRank.DFromValue; } set { inventoryRank.DFromValue = value; } }
        public decimal? DToValue { get { return inventoryRank.DToValue; } set { inventoryRank.DToValue = value; } }
        public decimal? EFromValue { get { return inventoryRank.EFromValue; } set { inventoryRank.EFromValue = value; } }
        public decimal? EToValue { get { return inventoryRank.EToValue; } set { inventoryRank.EToValue = value; } }
        public decimal? FFromValue { get { return inventoryRank.FFromValue; } set { inventoryRank.FFromValue = value; } }
        public decimal? FToValue { get { return inventoryRank.FToValue; } set { inventoryRank.FToValue = value; } }
        public decimal? GFromValue { get { return inventoryRank.GFromValue; } set { inventoryRank.GFromValue = value; } }
        public decimal? GToValue { get { return inventoryRank.GToValue; } set { inventoryRank.GToValue = value; } }
        public string RankUpdated { get { return inventoryRank.RankUpdated; } set { inventoryRank.RankUpdated = value; } }
        public string UsersId { get { return inventoryRank.UsersId; } set { inventoryRank.UsersId = value; } }
        public string DateStamp { get { return inventoryRank.DateStamp; } set { inventoryRank.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}