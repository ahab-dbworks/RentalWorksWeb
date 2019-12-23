using FwStandard.AppManager;
using System;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.PickListUtilityItem
{
    [FwLogic(Id:"NEchiPmHN3Eg")]
    public class PickListUtilityItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PickListUtilityItemRecord pickListUtilityItem = new PickListUtilityItemRecord();
        PickListUtilityItemLoader pickListUtilityItemLoader = new PickListUtilityItemLoader();
        public PickListUtilityItemLogic()
        {
            dataRecords.Add(pickListUtilityItem);
            dataLoader = pickListUtilityItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"fTraDuJD1WUF", IsPrimaryKey:true)]
        public string SessionId { get { return pickListUtilityItem.SessionId; } set { pickListUtilityItem.SessionId = value; } }

        [FwLogicProperty(Id:"GSn642Pw6srE", IsPrimaryKey:true)]
        public string OrderId { get { return pickListUtilityItem.OrderId; } set { pickListUtilityItem.OrderId = value; } }

        [FwLogicProperty(Id:"Xvt06xmRpyCg", IsPrimaryKey:true)]
        public string OrderItemId { get { return pickListUtilityItem.OrderItemId; } set { pickListUtilityItem.OrderItemId = value; } }

        [FwLogicProperty(Id:"CZosd66xxbmL", IsReadOnly:true)]
        public string ParentId { get; set; }

        [FwLogicProperty(Id:"CZosd66xxbmL", IsReadOnly:true)]
        public string ParentParentId { get; set; }

        [FwLogicProperty(Id:"Fs9LiHc9PPG2", IsReadOnly:true)]
        public decimal? AccessoryRatio { get; set; }

        [FwLogicProperty(Id:"NoARhLrT5bH5", IsReadOnly:true)]
        public string InventoryTypeId { get; set; }

        [FwLogicProperty(Id: "JXXazq96xPK0V", IsReadOnly: true)]
        public string InventoryType { get; set; }

        [FwLogicProperty(Id:"NoARhLrT5bH5", IsReadOnly:true)]
        public string InventoryTypeIdNoParent { get; set; }

        [FwLogicProperty(Id:"wEJfCXDy4Fgi", IsReadOnly:true)]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id:"4tXigWUAZNhS", IsReadOnly:true)]
        public string LocationId { get; set; }

        [FwLogicProperty(Id:"LQmya0gpDQLy", IsReadOnly:true)]
        public string DepartmentId { get; set; }

        [FwLogicProperty(Id:"8iERnklkv3Ad", IsReadOnly:true)]
        public string DealId { get; set; }

        [FwLogicProperty(Id:"UtYTXlyCz6sl", IsReadOnly:true)]
        public string OrderType { get; set; }

        [FwLogicProperty(Id:"cYXaFiFFYjFC", IsReadOnly:true)]
        public string OrderStatus { get; set; }

        [FwLogicProperty(Id:"yh736JCXV1qX", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"Q8K0yEQAxqpY", IsReadOnly:true)]
        public string Description { get; set; }

        public decimal? QuantityOrdered { get { return pickListUtilityItem.QuantityOrdered; } set { pickListUtilityItem.QuantityOrdered = Convert.ToInt32(value); } }
        [FwLogicProperty(Id:"XcBlx9diNvr4", IsReadOnly:true)]
        public decimal? SubQuantity { get; set; }

        [FwLogicProperty(Id:"oV6PV5EuN8ex", IsReadOnly:true)]
        public decimal? ConsignQuantity { get; set; }

        [FwLogicProperty(Id:"zHwBsyGOQxFH", IsReadOnly:true)]
        public decimal? QuantityInLocation { get; set; }

        [FwLogicProperty(Id:"P6Z3yRPgvLPU", IsReadOnly:true)]
        public string PickDate { get; set; }

        public decimal? PickQuantity { get { return pickListUtilityItem.PickQuantity; } set { pickListUtilityItem.PickQuantity = Convert.ToInt32(value); } }
        [FwLogicProperty(Id:"oLjrIbtrn57h", IsReadOnly:true)]
        public decimal? StagedQuantity { get; set; }

        [FwLogicProperty(Id:"MaccQs9xMuVs", IsReadOnly:true)]
        public decimal? OutQuantity { get; set; }

        [FwLogicProperty(Id:"2V9sTbbwqdEg", IsReadOnly:true)]
        public decimal? RemainingQuantity { get; set; }

        [FwLogicProperty(Id:"Tku91YWJwZSB", IsReadOnly:true)]
        public decimal? PickedQuantity { get; set; }

        [FwLogicProperty(Id:"FB0yZ2VLAIpM", IsReadOnly:true)]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id:"97dFdAXc83Na", IsReadOnly:true)]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"97dFdAXc83Na", IsReadOnly:true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"97dFdAXc83Na", IsReadOnly:true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"SHMA3ft3gLZH", IsReadOnly:true)]
        public string RecType { get; set; }

        [FwLogicProperty(Id:"SHMA3ft3gLZH", IsReadOnly:true)]
        public string RecTypeDisplay { get; set; }

        [FwLogicProperty(Id:"qEt1Xvb102NZ", IsReadOnly:true)]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id:"xAvduVwdd2hM", IsReadOnly:true)]
        public string ItemOrder { get; set; }

        [FwLogicProperty(Id:"lMeatMFDq0Z6", IsReadOnly:true)]
        public bool? OptionColor { get; set; }

        [FwLogicProperty(Id:"Xvt06xmRpyCg", IsReadOnly:true)]
        public string ItemId { get; set; }

        [FwLogicProperty(Id:"G2rrSjC4N2dz", IsReadOnly:true)]
        public string BarCode { get; set; }

        [FwLogicProperty(Id:"cFBmdAnvYkSQ", IsReadOnly:true)]
        public string SerialNumber { get; set; }

        [FwLogicProperty(Id:"4rCfMFBqar1p", IsReadOnly:true)]
        public string SubVendorId { get; set; }

        [FwLogicProperty(Id:"67jtFOgZVPWG", IsReadOnly:true)]
        public string ConsignorId { get; set; }

        [FwLogicProperty(Id:"wYKa0CskkyQX", IsReadOnly:true)]
        public string Vendor { get; set; }

        [FwLogicProperty(Id:"Xvt06xmRpyCg", IsReadOnly:true)]
        public string NestedOrderItemId { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
