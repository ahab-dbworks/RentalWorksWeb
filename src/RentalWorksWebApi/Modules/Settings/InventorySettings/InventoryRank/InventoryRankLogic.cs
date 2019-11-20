using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.InventorySettings.InventoryRank
{
    [FwLogic(Id:"lrM957aDB56f")]
    public class InventoryRankLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"IR4QTJFFv6Kx", IsPrimaryKey:true)]
        public string InventoryRankId { get { return inventoryRank.InventoryRankId; } set { inventoryRank.InventoryRankId = value; } }

        [FwLogicProperty(Id:"E7th3EBs7c5")]
        public string InventoryTypeId { get { return inventoryRank.InventoryTypeId; } set { inventoryRank.InventoryTypeId = value; } }

        [FwLogicProperty(Id:"ocgaIJB9ZgQT", IsRecordTitle:true, IsReadOnly:true)]
        public string InventoryType { get; set; }

        [FwLogicProperty(Id:"zFG0eq1SsQp")]
        public string WarehouseId { get { return inventoryRank.WarehouseId; } set { inventoryRank.WarehouseId = value; } }

        [FwLogicProperty(Id:"wubbFYK6iBUP", IsReadOnly:true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"wubbFYK6iBUP", IsRecordTitle:true, IsReadOnly:true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"zoe5oseLWWl")]
        public string Type { get { return inventoryRank.Type; } set { inventoryRank.Type = value; } }

        [FwLogicProperty(Id:"TSQEJRtf7Uky", IsReadOnly:true)]
        public string TypeDisplay { get; set; }

        [FwLogicProperty(Id:"yf47ZUiIKen")]
        public decimal? AFromValue { get { return inventoryRank.AFromValue; } set { inventoryRank.AFromValue = value; } }

        [FwLogicProperty(Id:"L1sK5cSbW3R")]
        public decimal? AToValue { get { return inventoryRank.AToValue; } set { inventoryRank.AToValue = value; } }

        [FwLogicProperty(Id:"n3sA2OORMJc")]
        public decimal? BFromValue { get { return inventoryRank.BFromValue; } set { inventoryRank.BFromValue = value; } }

        [FwLogicProperty(Id:"YRPokcMb2VF")]
        public decimal? BToValue { get { return inventoryRank.BToValue; } set { inventoryRank.BToValue = value; } }

        [FwLogicProperty(Id:"9Ozrs3mgGLp")]
        public decimal? CFromValue { get { return inventoryRank.CFromValue; } set { inventoryRank.CFromValue = value; } }

        [FwLogicProperty(Id:"3byw8krAqF0")]
        public decimal? CToValue { get { return inventoryRank.CToValue; } set { inventoryRank.CToValue = value; } }

        [FwLogicProperty(Id:"VuMiMYCpTK5")]
        public decimal? DFromValue { get { return inventoryRank.DFromValue; } set { inventoryRank.DFromValue = value; } }

        [FwLogicProperty(Id:"QkxSZ9xELxZ")]
        public decimal? DToValue { get { return inventoryRank.DToValue; } set { inventoryRank.DToValue = value; } }

        [FwLogicProperty(Id:"jJbFGbXaqAS")]
        public decimal? EFromValue { get { return inventoryRank.EFromValue; } set { inventoryRank.EFromValue = value; } }

        [FwLogicProperty(Id:"GWGtYBaBZgj")]
        public decimal? EToValue { get { return inventoryRank.EToValue; } set { inventoryRank.EToValue = value; } }

        [FwLogicProperty(Id:"b5o1SamjHjq")]
        public decimal? FFromValue { get { return inventoryRank.FFromValue; } set { inventoryRank.FFromValue = value; } }

        [FwLogicProperty(Id:"K6tgmZX4PuQ")]
        public decimal? FToValue { get { return inventoryRank.FToValue; } set { inventoryRank.FToValue = value; } }

        [FwLogicProperty(Id:"wMEQrQQva80")]
        public decimal? GFromValue { get { return inventoryRank.GFromValue; } set { inventoryRank.GFromValue = value; } }

        [FwLogicProperty(Id:"mOiG4Z2bsaW")]
        public decimal? GToValue { get { return inventoryRank.GToValue; } set { inventoryRank.GToValue = value; } }

        [FwLogicProperty(Id:"DlyWInnG2T2")]
        public string RankUpdated { get { return inventoryRank.RankUpdated; } set { inventoryRank.RankUpdated = value; } }

        [FwLogicProperty(Id:"Dv9G16vuF76")]
        public string UsersId { get { return inventoryRank.UsersId; } set { inventoryRank.UsersId = value; } }

        [FwLogicProperty(Id:"508UQlCLJqF")]
        public string DateStamp { get { return inventoryRank.DateStamp; } set { inventoryRank.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
