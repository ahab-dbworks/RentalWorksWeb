using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.OrderStatusSummary
{
    [FwLogic(Id:"ZT9sq7pUOn8s")]
    public class OrderStatusSummaryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderStatusSummaryLoader orderStatusSummaryLoader = new OrderStatusSummaryLoader();
        public OrderStatusSummaryLogic()
        {
            dataLoader = orderStatusSummaryLoader;
        }
        [FwLogicProperty(Id:"ReaH0Yt97g0u")]
        public string OrderId { get; set; }

        [FwLogicProperty(Id:"OqmiBJsnGDgF")]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"cjowkvzNeULc")]
        public string ICodeDisplay { get; set; }

        [FwLogicProperty(Id:"vdgN2rXSLMIV")]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id:"oHlxO6HxAGKY")]
        public string InventoryTypeId { get; set; }

        [FwLogicProperty(Id:"7J5CIuMPiBPg")]
        public string CategoryId { get; set; }

        [FwLogicProperty(Id:"PRsdhjRXpj58")]
        public string SubCategoryId { get; set; }

        [FwLogicProperty(Id:"G3yNaAgMdOHY")]
        public string Description { get; set; }

        [FwLogicProperty(Id:"EuIDjj8oS0d0")]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id:"XS32S16d5MUY")]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id:"MAdRjrjp7DZF")]
        public string MasterItemId { get; set; }

        [FwLogicProperty(Id:"YiK8faWBvlRm")]
        public string NestedMasterItemId { get; set; }

        [FwLogicProperty(Id:"95YGDVPMlKvB")]
        public string ParentId { get; set; }

        [FwLogicProperty(Id:"5wjqBTIjpeDI")]
        public string OutWarehouseId { get; set; }

        [FwLogicProperty(Id:"a6N6rVnYB2HZ")]
        public string OutWarehouseCode { get; set; }

        [FwLogicProperty(Id:"UMZfokhqn58v")]
        public string OutWarehouse { get; set; }

        [FwLogicProperty(Id:"hFEeE4HLfnjZ")]
        public string InWarehouseId { get; set; }

        [FwLogicProperty(Id:"iUoYn5nn8HSr")]
        public string InWarehouseCode { get; set; }

        [FwLogicProperty(Id:"0UtATNu9wtcO")]
        public string InWarehouse { get; set; }

        [FwLogicProperty(Id:"5QEATDBt1JRH")]
        public decimal? QuantityOrdered { get; set; }

        [FwLogicProperty(Id:"AjVUJeiZeIBD")]
        public string QuantityOrderedColor { get; set; }

        [FwLogicProperty(Id:"MFseTErOtbQu")]
        public decimal? SubQuantity { get; set; }

        [FwLogicProperty(Id:"fdkEOBkgODzh")]
        public decimal? StagedQuantity { get; set; }

        [FwLogicProperty(Id:"b0Pkp64PdkJD")]
        public decimal? StagedQuantityFilter { get; set; }

        [FwLogicProperty(Id:"gwHjeMNVkvOH")]
        public string StagedQuantityColor { get; set; }

        [FwLogicProperty(Id:"M7GpuBhj2187")]
        public decimal? OutQuantity { get; set; }

        [FwLogicProperty(Id:"fS5ygHEb5s8A")]
        public decimal? OutQuantityfilter { get; set; }

        [FwLogicProperty(Id:"FDJhoXVma0OO")]
        public string OutQuantityColor { get; set; }

        [FwLogicProperty(Id:"mbXiY4MrSnuH")]
        public bool? IsSuspendOut { get; set; }

        [FwLogicProperty(Id:"E6YNtJUX7VEF")]
        public decimal? InQuantity { get; set; }

        [FwLogicProperty(Id:"MHkGS0Vms0m2")]
        public decimal? InQuantityFilter { get; set; }

        [FwLogicProperty(Id:"Q2krPJUdgYUd")]
        public string InQuantityColor { get; set; }

        [FwLogicProperty(Id:"WjSOzparvHgL")]
        public bool? IsSuspendIn { get; set; }

        [FwLogicProperty(Id:"HcOjijY0nbQD")]
        public decimal? ReturnedQuantity { get; set; }

        [FwLogicProperty(Id:"vy6m6rXfp1r4")]
        public decimal? ActivityQuantity { get; set; }

        [FwLogicProperty(Id:"r0QiLIPMc0Dr")]
        public decimal? SubActivityQuantity { get; set; }

        [FwLogicProperty(Id:"gJcYH55C435j")]
        public decimal? QuantityReceived { get; set; }

        [FwLogicProperty(Id:"vTcHZxdEc56F")]
        public decimal? QuantityReturned { get; set; }

        [FwLogicProperty(Id:"y1niotUhP1Cu")]
        public decimal? NotYetStagedQuantity { get; set; }

        [FwLogicProperty(Id:"9hluF5GofUSU")]
        public bool? TooManyStagedOut { get; set; }

        [FwLogicProperty(Id:"YiX3Usi5Q9VS")]
        public decimal? NotYetStagedQuantityFilter { get; set; }

        [FwLogicProperty(Id:"lsD7v8Yeyn5i")]
        public decimal? StillOutQuantity { get; set; }

        [FwLogicProperty(Id:"X7MKA2V2aYUj")]
        public string StillOutQuantityColor { get; set; }

        [FwLogicProperty(Id:"Wu8LU4D2NBHf")]
        public string ItemOrder { get; set; }

        [FwLogicProperty(Id:"L0vERJ9Mfgw9")]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id:"H5Hsz5tV2HLs")]
        public string RecType { get; set; }

        [FwLogicProperty(Id:"MQH9gziDoW7r")]
        public string IsReturn { get; set; }

        [FwLogicProperty(Id:"49mPuil8IUPk")]
        public string PoOrderId { get; set; }

        [FwLogicProperty(Id:"lD1tkrHZkmWn")]
        public string PoMasteritemId { get; set; }

        [FwLogicProperty(Id:"C5BwQH4gKlXt")]
        public string RecTypeDisplay { get; set; }

        [FwLogicProperty(Id:"vWKx5hLBISzn")]
        public string RecTypeColor { get; set; }

        [FwLogicProperty(Id:"e0PGppMHJ9A2")]
        public string OptionColor { get; set; }

        [FwLogicProperty(Id:"MCK6FDEZ0MGg")]
        public bool? Bold { get; set; }

        [FwLogicProperty(Id:"tJZgFiIKGZN1")]
        public bool? HasPoItem { get; set; }

        [FwLogicProperty(Id:"sTv3fkFeksCh")]
        public string VendorId { get; set; }

        [FwLogicProperty(Id:"IXraljDtJlq0")]
        public string Notes { get; set; }

        [FwLogicProperty(Id:"ZrbpL9mfwqqS")]
        public string ManufacturerPartNumber { get; set; }

        [FwLogicProperty(Id:"MvpmPCrggOQz")]
        public string OrderBy { get; set; }

        [FwLogicProperty(Id:"O1WMIEfognZt")]
        public bool? IsWardrobe { get; set; }

        [FwLogicProperty(Id:"lE9NIvWAbk6U")]
        public bool? IsProps { get; set; }

        [FwLogicProperty(Id:"hkvEM8QJFDe2")]
        public decimal? UnitCost { get; set; }

        [FwLogicProperty(Id:"5NENadKUEgIw")]
        public decimal? StagedOutExtendedCost { get; set; }

        [FwLogicProperty(Id:"gzcGtRoB0B77")]
        public decimal? UnitPrice { get; set; }

        [FwLogicProperty(Id:"f8YRG40R1DCr")]
        public decimal? StagedOutExtendedPrice { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
