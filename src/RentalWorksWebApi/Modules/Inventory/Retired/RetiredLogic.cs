using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.Retired
{
    [FwLogic(Id: "idPPk9q7Wgb14")]
    public class RetiredLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        RetiredRecord retired = new RetiredRecord();
        RetiredLoader retiredLoader = new RetiredLoader();
        public RetiredLogic()
        {
            dataRecords.Add(retired);
            dataLoader = retiredLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "iDqFFqqwU6IAw", IsPrimaryKey: true)]
        public string RetiredId { get { return retired.RetiredId; } set { retired.RetiredId = value; } }
        [FwLogicProperty(Id: "iDtnapb1HZbAy")]
        public string InventoryId { get { return retired.InventoryId; } set { retired.InventoryId = value; } }
        [FwLogicProperty(Id: "IDW5ODLXiXjKh", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "IEsjSxscGEaXM", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "IEtDiVnlumX7C")]
        public string WarehouseId { get { return retired.WarehouseId; } set { retired.WarehouseId = value; } }
        [FwLogicProperty(Id: "IeY74xSOjjczG", IsReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwLogicProperty(Id: "iFYTZZRuqcBTe", IsReadOnly: true)]
        public string Warehouse { get; set; }
        [FwLogicProperty(Id: "igoypdLTDQAzr")]
        public string ConsignorId { get { return retired.ConsignorId; } set { retired.ConsignorId = value; } }
        [FwLogicProperty(Id: "igUwrISTpPdRP", IsReadOnly: true)]
        public string Consignor { get; set; }
        [FwLogicProperty(Id: "iHY3UCw6ZFcjk")]
        public string ConsignorAgreementId { get { return retired.ConsignorAgreementId; } set { retired.ConsignorAgreementId = value; } }
        [FwLogicProperty(Id: "ii7fP8uUHmFIN", IsReadOnly: true)]
        public string AgreementNumber { get; set; }
        [FwLogicProperty(Id: "ii8lVmyTF5RHA")]
        public string RetiredDate { get { return retired.RetiredDate; } set { retired.RetiredDate = value; } }
        [FwLogicProperty(Id: "Iihke6Im8vOm0", IsReadOnly: true)]
        public string RetiredReason { get; set; }
        [FwLogicProperty(Id: "IiR5IRzZ6nXGi", IsReadOnly: true)]
        public string Retiredby { get; set; }
        [FwLogicProperty(Id: "Iiw3ZkMuGealW")]
        public string RetiredReasonId { get { return retired.RetiredReasonId; } set { retired.RetiredReasonId = value; } }
        [FwLogicProperty(Id: "IJ90V3dZZ9mXO")]
        public string RetiredByUserId { get { return retired.RetiredByUserId; } set { retired.RetiredByUserId = value; } }
        [FwLogicProperty(Id: "IjbVYbm80tcOt")]
        public string RetiredNotes { get { return retired.RetiredNotes; } set { retired.RetiredNotes = value; } }
        [FwLogicProperty(Id: "ijQNXtSvKJn5u", IsReadOnly: true)]
        public decimal? RetiredQuantity { get; set; }
        [FwLogicProperty(Id: "iJUGHebPuiHbt")]
        public decimal? UnretiredQuantity { get { return retired.UnretiredQuantity; } set { retired.UnretiredQuantity = value; } }
        [FwLogicProperty(Id: "Ikf3EbdlZUx8R")]
        public string LostOrderId { get { return retired.LostOrderId; } set { retired.LostOrderId = value; } }
        [FwLogicProperty(Id: "ikgzbayJbzI8o", IsReadOnly: true)]
        public string LostOrderNumber { get; set; }
        [FwLogicProperty(Id: "iknIo0xfDxKAB")]
        public string SoldToOrderId { get { return retired.SoldToOrderId; } set { retired.SoldToOrderId = value; } }
        [FwLogicProperty(Id: "IkTO4jXhmmMOo")]
        public string SoldToOrderItemId { get { return retired.SoldToOrderItemId; } set { retired.SoldToOrderItemId = value; } }
        [FwLogicProperty(Id: "ikUuciHIcJvDc", IsReadOnly: true)]
        public string SoldToOrderNumber { get; set; }
        [FwLogicProperty(Id: "iL0KkVc6DPRMU", IsReadOnly: true)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
