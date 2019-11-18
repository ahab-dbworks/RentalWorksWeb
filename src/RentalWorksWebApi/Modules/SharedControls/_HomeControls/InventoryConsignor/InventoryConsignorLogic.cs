using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.InventoryConsignor
{
    [FwLogic(Id:"KkFURWqnufeB")]
    public class InventoryConsignorLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryConsignorLoader inventoryConsignorLoader = new InventoryConsignorLoader();
        public InventoryConsignorLogic()
        {
            dataLoader = inventoryConsignorLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"pXxTfesodx4G", IsReadOnly:true)]
        public string ConsignorId { get; set; }

        [FwLogicProperty(Id:"pXxTfesodx4G", IsReadOnly:true)]
        public string ConsignorAgreementId { get; set; }

        [FwLogicProperty(Id:"pXxTfesodx4G", IsReadOnly:true)]
        public string Consignor { get; set; }

        [FwLogicProperty(Id:"Jaoaj8NRCEjA", IsReadOnly:true)]
        public string AgreementNumber { get; set; }

        [FwLogicProperty(Id:"2CvbZyR4Hjjc", IsReadOnly:true)]
        public string AgreementDescription { get; set; }

        [FwLogicProperty(Id:"8MNlu9xJpBCu", IsReadOnly:true)]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id:"7JjEng9n2rJs", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"I8tOeGZcvvzj", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"wkAIvxpAHGEe", IsReadOnly:true)]
        public string TrackedBy { get; set; }

        [FwLogicProperty(Id:"0d982cBGwVmA", IsReadOnly:true)]
        public bool? TreatConsignedQtyAsOwned { get; set; }

        [FwLogicProperty(Id:"Pr1u4pANxs7p", IsReadOnly:true)]
        public int? QtyConsigned { get; set; }

        [FwLogicProperty(Id:"pXxTfesodx4G", IsReadOnly:true)]
        public int? ConsignorPercent { get; set; }

        [FwLogicProperty(Id:"cmiKCeAZRIhb", IsReadOnly:true)]
        public int? HousePercent { get; set; }

        [FwLogicProperty(Id:"NRxQjHexvVaw", IsReadOnly:true)]
        public bool? FlatRate { get; set; }

        [FwLogicProperty(Id:"NRxQjHexvVaw", IsReadOnly:true)]
        public decimal? FlatRateAmount { get; set; }

        [FwLogicProperty(Id:"VBJ46Ps4x1sA", IsReadOnly:true)]
        public string ItemId { get; set; }

        [FwLogicProperty(Id:"INtnWP4QKt7C", IsReadOnly:true)]
        public string BarCode { get; set; }

        [FwLogicProperty(Id:"O5sNc3Dk2NC5", IsReadOnly:true)]
        public string SerialNumber { get; set; }

        [FwLogicProperty(Id:"funcDVB6faLD", IsReadOnly:true)]
        public string InventoryStatusId { get; set; }

        [FwLogicProperty(Id:"funcDVB6faLD", IsReadOnly:true)]
        public string InventoryStatus { get; set; }

        [FwLogicProperty(Id:"funcDVB6faLD", IsReadOnly:true)]
        public string InventoryStatusType { get; set; }

        [FwLogicProperty(Id:"i5avGonwc8K5", IsReadOnly:true)]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"i5avGonwc8K5", IsReadOnly:true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"i5avGonwc8K5", IsReadOnly:true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"ZShdz65wUQ7M", IsReadOnly:true)]
        public string TextColor { get; set; }

        [FwLogicProperty(Id:"ZShdz65wUQ7M", IsReadOnly:true)]
        public int? Color { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
