using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Reflection;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Home.Receipt
{


    public class ReceiptInvoice
    {
        public string InvoiceId { get; set; }
        public decimal Amount { get; set; }
        //int? someValue;
    }


    [FwLogic(Id:"5XIpJJ8C7Ywx")]
    public class ReceiptLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ReceiptRecord receipt = new ReceiptRecord();
        ReceiptLoader receiptLoader = new ReceiptLoader();
        public ReceiptLogic()
        {
            dataRecords.Add(receipt);
            dataLoader = receiptLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"hcdYkju05r4e", IsPrimaryKey:true)]
        public string ReceiptId { get { return receipt.ReceiptId; } set { receipt.ReceiptId = value; } }

        [FwLogicProperty(Id:"7ucLS5wlvWpI", IsRecordTitle:true)]
        public string ReceiptDate { get { return receipt.ReceiptDate; } set { receipt.ReceiptDate = value; } }

        [FwLogicProperty(Id:"Bf2y9PcBnUwj")]
        public string LocationId { get { return receipt.LocationId; } set { receipt.LocationId = value; } }

        [FwLogicProperty(Id:"RUgIsdJa9FQn", IsReadOnly:true)]
        public string LocationCode { get; set; }

        [FwLogicProperty(Id:"NSh9WWPHVJDa", IsReadOnly:true)]
        public string Location { get; set; }

        [FwLogicProperty(Id:"DN9elEbrZ1dr")]
        public string CustomerId { get { return receipt.CustomerId; } set { receipt.CustomerId = value; } }

        [FwLogicProperty(Id:"MedLHg9WuGie", IsReadOnly:true)]
        public string Customer { get; set; }

        [FwLogicProperty(Id:"dhYQSaHyHNiW")]
        public string DealId { get { return receipt.DealId; } set { receipt.DealId = value; } }

        [FwLogicProperty(Id:"eeTo238ihgT9", IsReadOnly:true)]
        public string Deal { get; set; }

        [FwLogicProperty(Id:"Hq9VCTVD1PKv")]
        public string PaymentBy { get { return receipt.PaymentBy; } set { receipt.PaymentBy = value; } }

        [FwLogicProperty(Id:"TaJJBwxOCui6", IsReadOnly:true)]
        public string CustomerDeal { get; set; }

        [FwLogicProperty(Id:"cAyFNYEDmOWD")]
        public string PaymentTypeId { get { return receipt.PaymentTypeId; } set { receipt.PaymentTypeId = value; } }

        [FwLogicProperty(Id:"kXK17zt5j8sU", IsReadOnly:true)]
        public string PaymentType { get; set; }

        [FwLogicProperty(Id:"ME2oFmB4u32U", IsReadOnly:true)]
        public string PaymentTypeExportPaymentMethod { get; set; }

        [FwLogicProperty(Id:"myzLVs0a6vfu", IsRecordTitle:true)]
        public string CheckNumber { get { return receipt.CheckNumber; } set { receipt.CheckNumber = value; } }

        [FwLogicProperty(Id:"KwEgI2DXXnXD")]
        public decimal? PaymentAmount { get { return receipt.PaymentAmount; } set { receipt.PaymentAmount = value; } }

        [FwLogicProperty(Id:"XfMr9D8sR6DZ")]
        public string AppliedById { get { return receipt.AppliedById; } set { receipt.AppliedById = value; } }

        [FwLogicProperty(Id:"L8bFfvReiecw", IsReadOnly:true)]
        public string AppliedBy { get; set; }

        [FwLogicProperty(Id:"A3RNw0AYVNSC")]
        public string ModifiedById { get { return receipt.ModifiedById; } set { receipt.ModifiedById = value; } }

        [FwLogicProperty(Id:"qupXWj36ne07", IsReadOnly:true)]
        public string ModifiedBy { get; set; }

        [FwLogicProperty(Id:"mat3c7KQ0iNn")]
        public string PaymentMemo { get { return receipt.PaymentMemo; } set { receipt.PaymentMemo = value; } }

        [FwLogicProperty(Id:"TpuozxIakrza")]
        public bool? RecType { get { return receipt.RecType; } set { receipt.RecType = value; } }

        [FwLogicProperty(Id:"0uxt3Kj2zTdS", IsReadOnly:true)]
        public string ChargeBatchId { get; set; }

        [FwLogicProperty(Id:"3uY1f5SblA5d", IsReadOnly:true)]
        public string ChargeBatchNumber { get; set; }

        [FwLogicProperty(Id:"HL5mbYr6nUYx")]
        public string CurrencyId { get { return receipt.CurrencyId; } set { receipt.CurrencyId = value; } }

        [FwLogicProperty(Id:"D7LZaFqbzDWI", IsReadOnly:true)]
        public string CurrencyCode { get; set; }

        [FwLogicProperty(Id:"DFYUuxYiQR5I", IsReadOnly:true)]
        public string LocationDefaultCurrencyId { get; set; }


        [FwLogicProperty(Id: "XXXXX")]
        public List<ReceiptInvoice> InvoiceDataList { get; set; }


        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            decimal invoiceAmountTotal = 0;
            if (isValid)
            {
                PropertyInfo property = typeof(ReceiptLogic).GetProperty(nameof(ReceiptLogic.PaymentBy));
                string[] acceptableValues = { RwConstants.RECEIPT_PAYMENT_BY_CUSTOMER, RwConstants.RECEIPT_PAYMENT_BY_DEAL };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }
            foreach (var invoice in InvoiceDataList)
            {
                invoiceAmountTotal += invoice.Amount;
            }
            if (invoiceAmountTotal != PaymentAmount)
            {
                isValid = false;
                validateMsg = "Amount to Apply does not match Invoice Amounts provided.";
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (PaymentBy != null)
                {
                    if (PaymentBy.Equals(RwConstants.RECEIPT_PAYMENT_BY_CUSTOMER))
                    {
                        DealId = "";
                    }
                    else if (PaymentBy.Equals(RwConstants.RECEIPT_PAYMENT_BY_DEAL))
                    {
                        CustomerId = "";
                    }
                }
            }
        }
    }
}
