using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.CheckOutPendingItem
{
    [FwLogic(Id:"JWGuz56ob7O")]
    public class CheckOutPendingItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CheckOutPendingItemLoader checkOutPendingItemLoader = new CheckOutPendingItemLoader();
        public CheckOutPendingItemLogic()
        {
            dataLoader = checkOutPendingItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"sccyaDcKCq4", IsReadOnly:true)]
        public string OrderId { get; set; }

        [FwLogicProperty(Id:"2Rb9PNivjCp", IsReadOnly:true)]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id:"2k92dEPUFqC", IsReadOnly:true)]
        public string ParentId { get; set; }

        [FwLogicProperty(Id:"swpls1qebDv", IsReadOnly:true)]
        public string OrderItemId { get; set; }

        [FwLogicProperty(Id:"BYJuRUlnzmk", IsReadOnly:true)]
        public string NestedOrderItemId { get; set; }

        [FwLogicProperty(Id:"OiaDNARx1dC", IsReadOnly:true)]
        public bool? IsException { get; set; }

        [FwLogicProperty(Id:"FmnU766rhea", IsReadOnly:true)]
        public bool? SomeOut { get; set; }

        [FwLogicProperty(Id:"lmAJep1Zc9q", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"LJD7baFFsIt", IsReadOnly:true)]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id:"JIjZptM6vpB", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"UmwA3xfvXOU", IsReadOnly:true)]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id:"vLZmNN189b2", IsReadOnly:true)]
        public string SubVendorId { get; set; }

        [FwLogicProperty(Id:"8NrFSokKVaV", IsReadOnly:true)]
        public string ConsignorId { get; set; }

        [FwLogicProperty(Id:"iDDQWvtdg9X", IsReadOnly:true)]
        public string ConsignorAgreementId { get; set; }

        [FwLogicProperty(Id:"z7Y7IKzRSOw", IsReadOnly:true)]
        public string Vendor { get; set; }

        [FwLogicProperty(Id:"uQP3u1LHGY6", IsReadOnly:true)]
        public string VendorColor { get; set; }

        [FwLogicProperty(Id:"WcUoUWnaJhP", IsReadOnly:true)]
        public decimal? QuantityOrdered { get; set; }

        [FwLogicProperty(Id:"stqLARFDkEs", IsReadOnly:true)]
        public decimal? ReservedItems { get; set; }

        [FwLogicProperty(Id:"leqMCpe7GQT", IsReadOnly:true)]
        public decimal? QuantityStagedAndOut { get; set; }

        [FwLogicProperty(Id:"9ruRwGdK6yH", IsReadOnly:true)]
        public decimal? QuantityOut { get; set; }

        [FwLogicProperty(Id:"hLxSSe5wFO2", IsReadOnly:true)]
        public decimal? QuantitySub { get; set; }

        [FwLogicProperty(Id:"Sc8LPwMRhWT", IsReadOnly:true)]
        public decimal? QuantitySubStagedAndOut { get; set; }

        [FwLogicProperty(Id:"UUpT8eHXm6l", IsReadOnly:true)]
        public decimal? QuantitySubOut { get; set; }

        [FwLogicProperty(Id:"1pu2c2hGrjg", IsReadOnly:true)]
        public bool? IsMissing { get; set; }

        [FwLogicProperty(Id:"DIvUF4hiX8c", IsReadOnly:true)]
        public decimal? MissingQuantity { get; set; }

        [FwLogicProperty(Id:"jdbbwlZeqwI", IsReadOnly:true)]
        public string MissingColor { get; set; }

        [FwLogicProperty(Id:"lvrGmtjwoNJ", IsReadOnly:true)]
        public string TrackedBy { get; set; }

        [FwLogicProperty(Id:"kHQqsaa2e2F", IsReadOnly:true)]
        public string RecType { get; set; }

        [FwLogicProperty(Id:"VFezJjpUKLD", IsReadOnly:true)]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id:"TP8nXf2dT3N", IsReadOnly:true)]
        public string ItemOrder { get; set; }

        [FwLogicProperty(Id:"eSzXnFKNdztl", IsReadOnly:true)]
        public string OrderBy { get; set; }

        [FwLogicProperty(Id:"pzInxv6NZ7q4", IsReadOnly:true)]
        public bool? OptionColor { get; set; }

        [FwLogicProperty(Id:"DYCvGtMP1Q6I", IsReadOnly:true)]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"r7UWlgDfFXq5", IsReadOnly:true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"I6HPsWxC9sDZ", IsReadOnly:true)]
        public string ContainerInventoryId { get; set; }

        [FwLogicProperty(Id:"nEfFJBefwhoj", IsReadOnly:true)]
        public string ContainerId { get; set; }

        [FwLogicProperty(Id:"NWmiHhC4ncwR", IsReadOnly:true)]
        public string ScannableInventoryId { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
