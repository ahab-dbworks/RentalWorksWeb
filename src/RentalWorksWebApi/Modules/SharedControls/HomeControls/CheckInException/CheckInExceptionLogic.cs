using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.CheckInException
{
    [FwLogic(Id:"C9kbK8ewnVnj")]
    public class CheckInExceptionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CheckInExceptionLoader checkInExceptionLoader = new CheckInExceptionLoader();
        public CheckInExceptionLogic()
        {
            dataLoader = checkInExceptionLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"aJf1G9IJZwnR", IsReadOnly:true)]
        public string OrderId { get; set; }

        [FwLogicProperty(Id:"zVeWXB8rnUg5", IsReadOnly:true)]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id:"1Li6kNlMR3IQ", IsReadOnly:true)]
        public string ParentId { get; set; }

        [FwLogicProperty(Id:"Sw2HOtHX9k7K", IsReadOnly:true)]
        public string OrderItemId { get; set; }

        [FwLogicProperty(Id:"M02ALLSxsPOa", IsReadOnly:true)]
        public bool? IsException { get; set; }

        [FwLogicProperty(Id:"AVo4VPQ5q86F", IsReadOnly:true)]
        public bool? SomeItemsIn { get; set; }

        [FwLogicProperty(Id:"66KT6Ig9lDlK", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"IaLq9Uah441M", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"iaFFQbkgOfhX", IsReadOnly:true)]
        public string Vendor { get; set; }

        [FwLogicProperty(Id:"iaFFQbkgOfhX", IsReadOnly:true)]
        public string VendorId { get; set; }

        [FwLogicProperty(Id:"7zewecFuPvs4", IsReadOnly:true)]
        public decimal? QuantityOrdered { get; set; }

        [FwLogicProperty(Id:"2KE6hQJcNt6E", IsReadOnly:true)]
        public decimal? QuantityStagedAndOut { get; set; }

        [FwLogicProperty(Id:"A49l3bpcZI68", IsReadOnly:true)]
        public decimal? QuantityOut { get; set; }

        [FwLogicProperty(Id:"BPQ3D7KhAbSm", IsReadOnly:true)]
        public decimal? SubQuantity { get; set; }

        [FwLogicProperty(Id:"2KE6hQJcNt6E", IsReadOnly:true)]
        public decimal? SubQuantityStagedAndOut { get; set; }

        [FwLogicProperty(Id:"A49l3bpcZI68", IsReadOnly:true)]
        public decimal? SubQuantityOut { get; set; }

        [FwLogicProperty(Id:"60r501hZivHr", IsReadOnly:true)]
        public decimal? QuantityIn { get; set; }

        [FwLogicProperty(Id:"kUxCQ6KlqIfO", IsReadOnly:true)]
        public decimal? QuantityStillOut { get; set; }

        [FwLogicProperty(Id:"BDlOxcgFVm5d", IsReadOnly:true)]
        public bool? IsMissing { get; set; }

        [FwLogicProperty(Id:"a5srn6SHCmxY", IsReadOnly:true)]
        public decimal? MissingQuantity { get; set; }

        [FwLogicProperty(Id:"RrajHtE1k9A6", IsReadOnly:true)]
        public string TrackedBy { get; set; }

        [FwLogicProperty(Id:"PgzTIPYUwdC2", IsReadOnly:true)]
        public string RecType { get; set; }

        [FwLogicProperty(Id:"2RkL3D937Nho", IsReadOnly:true)]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id:"kTGxMYMpcpOF", IsReadOnly:true)]
        public string ItemOrder { get; set; }

        [FwLogicProperty(Id:"PyUrPl6c164M", IsReadOnly:true)]
        public string OrderBy { get; set; }

        [FwLogicProperty(Id:"ZeuapNS9Vdwh", IsReadOnly:true)]
        public bool? OptionColor { get; set; }

        [FwLogicProperty(Id:"WsXQ5OJUYfZr", IsReadOnly:true)]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"m6goIx4brh2u", IsReadOnly:true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"Zpe1wyYwkHtQ", IsReadOnly:true)]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id:"TllxbuixXnZf", IsReadOnly:true)]
        public bool? IsBarCode { get; set; }

        [FwLogicProperty(Id:"tbmXxAAtrnHz", IsReadOnly:true)]
        public string ContractId { get; set; }

        [FwLogicProperty(Id:"I4pUDTcAejdm", IsReadOnly:true)]
        public bool? IsSub { get; set; }

        [FwLogicProperty(Id:"ClNXqBp5AePD", IsReadOnly:true)]
        public string NestedOrderItemId { get; set; }

        [FwLogicProperty(Id:"t1ygWA4dbJOh", IsReadOnly:true)]
        public bool? IsConsignor { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
