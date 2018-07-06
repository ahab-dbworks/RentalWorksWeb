using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.StagedItem
{
    public class StagedItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        StagedItemLoader stagedItemLoader = new StagedItemLoader();
        public StagedItemLogic()
        {
            dataLoader = stagedItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        public string OrderId;
        public string BarCode;
        public string ICode;
        public string Description;
        public decimal? Quantity;
        public decimal? QuantityOrdered;
        public string InventoryId;
        public string WarehouseId;
        public string OrderItemId;
        public string ParentId;
        public string StagedDateTime;
        public string ItemClass;
        public string Notes;
        public string RecType;
        public string RecTypeDisplay;
        public string OptionColor;
        public bool? QuotePrint;
        public bool? OrderPrint;
        public bool? PickListPrint;
        public bool? ContractOutPrint;
        public bool? ReturnListPrint;
        public bool? InvoicePrint;
        public bool? ContractInPrint;
        public bool? PurchaseOrderPrint;
        public bool? ContractReceivePrint;
        public bool? ContractReturnPrint;
        public bool? PurchaseOrderReceivelistPrint;
        public bool? PurchaseOrderReturnlistPrint;
        public string StagedUsersId;
        public string StagedUser;
        public string VendorId;
        public string NestedOrderItemId;
        public string TrackedBy;
        public int? ICodeColor;
        public int? DescriptionColor;
        public bool? Bold;
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
    }
}
