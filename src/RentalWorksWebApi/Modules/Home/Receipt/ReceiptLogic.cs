using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using WebApi.Logic;
using WebApi.Modules.Home.InvoiceReceipt;
using WebLibrary;

namespace WebApi.Modules.Home.Receipt
{


    public class ReceiptInvoice
    {
        public string InvoiceReceiptId { get; set; }
        public string InvoiceId { get; set; }
        public decimal Amount { get; set; }
    }


    [FwLogic(Id: "5XIpJJ8C7Ywx")]
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
            AfterSave += OnAfterSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "hcdYkju05r4e", IsPrimaryKey: true)]
        public string ReceiptId { get { return receipt.ReceiptId; } set { receipt.ReceiptId = value; } }

        [FwLogicProperty(Id: "7ucLS5wlvWpI", IsRecordTitle: true)]
        public string ReceiptDate { get { return receipt.ReceiptDate; } set { receipt.ReceiptDate = value; } }

        [FwLogicProperty(Id: "Bf2y9PcBnUwj")]
        public string LocationId { get { return receipt.LocationId; } set { receipt.LocationId = value; } }

        [FwLogicProperty(Id: "RUgIsdJa9FQn", IsReadOnly: true)]
        public string LocationCode { get; set; }

        [FwLogicProperty(Id: "NSh9WWPHVJDa", IsReadOnly: true)]
        public string Location { get; set; }

        [FwLogicProperty(Id: "DN9elEbrZ1dr")]
        public string CustomerId { get { return receipt.CustomerId; } set { receipt.CustomerId = value; } }

        [FwLogicProperty(Id: "MedLHg9WuGie", IsReadOnly: true)]
        public string Customer { get; set; }

        [FwLogicProperty(Id: "dhYQSaHyHNiW")]
        public string DealId { get { return receipt.DealId; } set { receipt.DealId = value; } }

        [FwLogicProperty(Id: "eeTo238ihgT9", IsReadOnly: true)]
        public string Deal { get; set; }

        [FwLogicProperty(Id: "Hq9VCTVD1PKv")]
        public string PaymentBy { get { return receipt.PaymentBy; } set { receipt.PaymentBy = value; } }

        [FwLogicProperty(Id: "TaJJBwxOCui6", IsReadOnly: true)]
        public string CustomerDeal { get; set; }

        [FwLogicProperty(Id: "cAyFNYEDmOWD")]
        public string PaymentTypeId { get { return receipt.PaymentTypeId; } set { receipt.PaymentTypeId = value; } }

        [FwLogicProperty(Id: "kXK17zt5j8sU", IsReadOnly: true)]
        public string PaymentType { get; set; }

        [FwLogicProperty(Id: "ME2oFmB4u32U", IsReadOnly: true)]
        public string PaymentTypeExportPaymentMethod { get; set; }

        [FwLogicProperty(Id: "myzLVs0a6vfu", IsRecordTitle: true)]
        public string CheckNumber { get { return receipt.CheckNumber; } set { receipt.CheckNumber = value; } }

        [FwLogicProperty(Id: "KwEgI2DXXnXD")]
        public decimal? PaymentAmount { get { return receipt.PaymentAmount; } set { receipt.PaymentAmount = value; } }

        [FwLogicProperty(Id: "XfMr9D8sR6DZ")]
        public string AppliedById { get { return receipt.AppliedById; } set { receipt.AppliedById = value; } }

        [FwLogicProperty(Id: "L8bFfvReiecw", IsReadOnly: true)]
        public string AppliedBy { get; set; }

        [FwLogicProperty(Id: "A3RNw0AYVNSC")]
        public string ModifiedById { get { return receipt.ModifiedById; } set { receipt.ModifiedById = value; } }

        [FwLogicProperty(Id: "qupXWj36ne07", IsReadOnly: true)]
        public string ModifiedBy { get; set; }

        [FwLogicProperty(Id: "mat3c7KQ0iNn")]
        public string PaymentMemo { get { return receipt.PaymentMemo; } set { receipt.PaymentMemo = value; } }

        [FwLogicProperty(Id: "TpuozxIakrza")]
        public bool? RecType { get { return receipt.RecType; } set { receipt.RecType = value; } }

        [FwLogicProperty(Id: "0uxt3Kj2zTdS", IsReadOnly: true)]
        public string ChargeBatchId { get; set; }

        [FwLogicProperty(Id: "3uY1f5SblA5d", IsReadOnly: true)]
        public string ChargeBatchNumber { get; set; }

        [FwLogicProperty(Id: "HL5mbYr6nUYx")]
        public string CurrencyId { get { return receipt.CurrencyId; } set { receipt.CurrencyId = value; } }

        [FwLogicProperty(Id: "D7LZaFqbzDWI", IsReadOnly: true)]
        public string CurrencyCode { get; set; }

        [FwLogicProperty(Id: "DFYUuxYiQR5I", IsReadOnly: true)]
        public string LocationDefaultCurrencyId { get; set; }


        [FwLogicProperty(Id: "BD8n6SDR8Rn6y")]
        public List<ReceiptInvoice> InvoiceDataList { get; set; }  // this field accepts the requested Invoices and Amounts from the user when saving a new or modified Receipt


        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            ReceiptLogic orig = null;
            bool isValid = true;
            decimal invoiceAmountTotal = 0;
            decimal paymentAmount = 0;

            if (original != null)
            {
                orig = (ReceiptLogic)original;
            }

            if (saveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                paymentAmount = PaymentAmount.GetValueOrDefault(0);
            }
            else
            {
                if (PaymentAmount == null)
                {
                    paymentAmount = orig.PaymentAmount.GetValueOrDefault(0);
                }
                else
                {
                    paymentAmount = PaymentAmount.GetValueOrDefault(0);
                }
            }

            if (isValid)
            {
                PropertyInfo property = typeof(ReceiptLogic).GetProperty(nameof(ReceiptLogic.PaymentBy));
                string[] acceptableValues = { RwConstants.RECEIPT_PAYMENT_BY_CUSTOMER, RwConstants.RECEIPT_PAYMENT_BY_DEAL };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }
            if (isValid)
            {
                foreach (ReceiptInvoice i in InvoiceDataList)
                {
                    invoiceAmountTotal += i.Amount;
                }
                if (invoiceAmountTotal != paymentAmount)
                {
                    isValid = false;
                    validateMsg = "Amount to Apply does not match Invoice Amounts provided.";
                }
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
        //------------------------------------------------------------------------------------
        public void OnAfterSave(object sender, AfterSaveEventArgs e) // Josh, here we want to first ask the database for the previous list of Invoices and Amounts related to this Receipt.  We then compare that list with the list provided by the user and perform updates back to the database.
        {
            List<InvoiceReceiptLogic> previousIrData = new List<InvoiceReceiptLogic>();

            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                // ask the database for the previous list of Invoices and Amounts related to this Receipt
                InvoiceReceiptLogic irLoader = new InvoiceReceiptLogic();
                irLoader.SetDependencies(AppConfig, UserSession);
                irLoader.ReceiptId = ReceiptId;
                BrowseRequest request = new BrowseRequest();
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                uniqueIds.Add("ReceiptId", ReceiptId);
                request.uniqueids = uniqueIds;
                previousIrData = irLoader.SelectAsync<InvoiceReceiptLogic>(request).Result;  // here is the list with the previous data
            }


            // iterate through the PREVIOUS list.  compare each entry with the ones provided by the user.  If the amount is changed, we need to save the modifications to the database
            foreach (InvoiceReceiptLogic irPrev in previousIrData)
            {
                foreach (ReceiptInvoice riNew in InvoiceDataList)  // iterate through the list provided by the user
                {
                    if (irPrev.InvoiceReceiptId.Equals(riNew.InvoiceReceiptId)) // find the record that matches this InvoiceReceiptId
                    {
                        if (!irPrev.Amount.Equals(riNew.Amount))
                        {
                            irPrev.Amount = riNew.Amount;
                            irPrev.SetDependencies(AppConfig, UserSession);
                            int saveCount = irPrev.SaveAsync(null).Result;
                        }
                    }
                }
            }

            // iterate through the NEW list.  anyhing without an InvoiceReeiptId is new, we need to save these
            foreach (ReceiptInvoice ri in InvoiceDataList)
            {
                if ((string.IsNullOrEmpty(ri.InvoiceReceiptId)) && (ri.Amount != 0))
                {
                    InvoiceReceiptLogic irNew = new InvoiceReceiptLogic();
                    irNew.SetDependencies(AppConfig, UserSession);
                    irNew.ReceiptId = ReceiptId;
                    irNew.InvoiceId = ri.InvoiceId;
                    irNew.Amount = ri.Amount;
                    irNew.SetDependencies(AppConfig, UserSession);
                    int saveCount = irNew.SaveAsync(null).Result;
                }
            }


        }
    }
}