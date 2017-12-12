using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Home.InventoryConsignor
{
    public class InventoryConsignorLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryConsignorLoader inventoryConsignorLoader = new InventoryConsignorLoader();
        public InventoryConsignorLogic()
        {
            dataLoader = inventoryConsignorLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string ConsignorId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ConsignorAgreementId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Consignor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AgreementNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AgreementDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TrackedBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? TreatConsignedQtyAsOwned { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? QtyConsigned { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? ConsignorPercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? HousePercent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? FlatRate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? FlatRateAmount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BarCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SerialNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryStatusId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryStatus { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryStatusType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? TextColor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? Color { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}