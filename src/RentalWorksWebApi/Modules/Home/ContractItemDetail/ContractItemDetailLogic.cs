using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.ContractItemDetail
{
    [FwLogic(Id:"h3uewaeFBSqh")]
    public class ContractItemDetailLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ContractItemDetailLoader contractItemDetailLoader = new ContractItemDetailLoader();
        public ContractItemDetailLogic()
        {
            dataLoader = contractItemDetailLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"sLUTWx3sABKt")]
        public string ContractId { get; set; }

        [FwLogicProperty(Id:"1Rc8RAwrXexD")]
        public string OrderId { get; set; }

        [FwLogicProperty(Id:"xjSVxCZLnxxE")]
        public string OrderItemId { get; set; }

        [FwLogicProperty(Id:"76s85CfnYo10")]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id:"y2L5rdKpXUrb")]
        public string OrderDescription { get; set; }

        [FwLogicProperty(Id:"ijWDhaYorMjR")]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"JQ4DWGjzVoi5")]
        public string ICodeDisplay { get; set; }

        [FwLogicProperty(Id:"VH5LWH2bEksC")]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id:"Q82P1EFlBgno")]
        public string Description { get; set; }

        [FwLogicProperty(Id:"5PZevaNueSlJ")]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id:"EGa1DUt1kFwh")]
        public decimal? Quantity { get; set; }

        [FwLogicProperty(Id:"LE6RT7DljfFl")]
        public string Barcode { get; set; }

        [FwLogicProperty(Id:"SUqwhtLVNWmj")]
        public string ManufacturerPartNumber { get; set; }

        [FwLogicProperty(Id:"lDAqywUimGKm")]
        public string TrackedBy { get; set; }

        [FwLogicProperty(Id:"aFEXxvmV7oYH")]
        public string CategoryId { get; set; }

        [FwLogicProperty(Id:"bwZQ2LktVWKz")]
        public string VendorId { get; set; }

        [FwLogicProperty(Id:"5OsSTnZmWvDs")]
        public string Vendor { get; set; }

        [FwLogicProperty(Id:"endzej7ssFry")]
        public string VendorColor { get; set; }

        [FwLogicProperty(Id:"lKBIK1HSPKmW")]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id:"Uac1HEcHxnLG")]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"Ht6zorYcnbHh")]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"J7mL9w2rpX8v")]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"LTyWKVAXBdwE")]
        public string PrimaryOrderItemId { get; set; }

        [FwLogicProperty(Id:"Ityd97yJxcQt")]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id:"dIHvx6Q7eG0g")]
        public string ItemOrder { get; set; }

        [FwLogicProperty(Id:"gDvJMTdNHKO0")]
        public string OrderBy { get; set; }

        [FwLogicProperty(Id:"HzlR9ZybGbME")]
        public string Notes { get; set; }

        [FwLogicProperty(Id:"ZJJwndVgFBLk")]
        public string OrderType { get; set; }

        [FwLogicProperty(Id:"gi4rqTqCY2e2")]
        public string RecType { get; set; }

        [FwLogicProperty(Id:"yxcv9EhRizFF")]
        public string RecTypeDisplay { get; set; }

        [FwLogicProperty(Id:"Ui02ZI7AQtG1")]
        public string OptionColor { get; set; }

        [FwLogicProperty(Id:"aaXfdCcRPybA")]
        public string UsersId { get; set; }

        [FwLogicProperty(Id:"65hAvo4z2W6N")]
        public string UserName { get; set; }

        [FwLogicProperty(Id:"Nz1OrAlXHshI")]
        public string TransactionDateTime { get; set; }

        [FwLogicProperty(Id:"pgAS1I3f9Yjx")]
        public string ParentId { get; set; }

        [FwLogicProperty(Id:"IY1Ysv3gh7PR")]
        public decimal? AccessoryRatio { get; set; }

        [FwLogicProperty(Id:"JTGFojpHj1Pj")]
        public string NestedOrderItemId { get; set; }

        [FwLogicProperty(Id:"BNmQKw6zjMBy")]
        public string ContainerItemId { get; set; }

        [FwLogicProperty(Id:"z9Nrhn6SusZd")]
        public string ContainerBarCode { get; set; }

        [FwLogicProperty(Id:"3lrOOnb9BHpK")]
        public bool? IsConsignment { get; set; }

        [FwLogicProperty(Id:"b4LOTks7b05f")]
        public string ConsignorId { get; set; }

        [FwLogicProperty(Id:"ZxbFFiYZRgvX")]
        public string ConsignorAgreementId { get; set; }

        [FwLogicProperty(Id: "fmpBihxoTwq")]
        public bool? IsVoid { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
