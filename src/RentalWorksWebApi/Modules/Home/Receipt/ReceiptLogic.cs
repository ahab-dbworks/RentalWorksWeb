using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.Receipt
{
    public class ReceiptLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ReceiptRecord receipt = new ReceiptRecord();
        ReceiptLoader receiptLoader = new ReceiptLoader();
        public ReceiptLogic()
        {
            dataRecords.Add(receipt);
            dataLoader = receiptLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ReceiptId { get { return receipt.ReceiptId; } set { receipt.ReceiptId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ReceiptDate { get { return receipt.ReceiptDate; } set { receipt.ReceiptDate = value; } }
        public string LocationId { get { return receipt.LocationId; } set { receipt.LocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LocationCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Location { get; set; }
        public string CustomerId { get { return receipt.CustomerId; } set { receipt.CustomerId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Customer { get; set; }
        public string DealId { get { return receipt.DealId; } set { receipt.DealId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; }
        public string PaymentBy { get { return receipt.PaymentBy; } set { receipt.PaymentBy = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CustomerDeal { get; set; }
        public string PaymentTypeId { get { return receipt.PaymentTypeId; } set { receipt.PaymentTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PaymentType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PaymentTypeExportPaymentMethod { get; set; }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string CheckNumber { get { return receipt.CheckNumber; } set { receipt.CheckNumber = value; } }
        public decimal? PaymentAmount { get { return receipt.PaymentAmount; } set { receipt.PaymentAmount = value; } }
        public string AppliedById { get { return receipt.AppliedById; } set { receipt.AppliedById = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AppliedBy { get; set; }
        public string ModifiedById { get { return receipt.ModifiedById; } set { receipt.ModifiedById = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ModifiedBy { get; set; }
        public string PaymentMemo { get { return receipt.PaymentMemo; } set { receipt.PaymentMemo = value; } }
        public bool? RecType { get { return receipt.RecType; } set { receipt.RecType = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ChargeBatchId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ChargeBatchNumber { get; set; }
        public string CurrencyId { get { return receipt.CurrencyId; } set { receipt.CurrencyId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CurrencyCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LocationDefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
