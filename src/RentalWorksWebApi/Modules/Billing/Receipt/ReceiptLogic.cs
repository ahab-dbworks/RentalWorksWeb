using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System.Reflection;
using WebApi.Logic;
using WebApi.Modules.Billing.Invoice;
using WebApi.Modules.Home.DepositPayment;
using WebApi.Modules.Home.InvoiceReceipt;
using WebApi.Modules.Home.ReceiptCredit;
using WebApi.Modules.Settings.PaymentType;
using WebApi.Modules.Settings.SystemSettings;
using WebLibrary;

namespace WebApi.Modules.Billing.Receipt
{
    public class ReceiptInvoice
    {
        public string InvoiceReceiptId { get; set; }
        public string InvoiceId { get; set; }
        public decimal Amount { get; set; }
    }

    public class ReceiptCredit
    {
        public string CreditReceiptId { get; set; }
        public string CreditId { get; set; }
        public decimal Amount { get; set; }
    }

    [FwLogic(Id: "5XIpJJ8C7Ywx")]
    public class ReceiptLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ReceiptRecord receipt = new ReceiptRecord();
        ReceiptLoader receiptLoader = new ReceiptLoader();
        ReceiptBrowseLoader receiptBrowseLoader = new ReceiptBrowseLoader();

        public ReceiptLogic()
        {
            dataRecords.Add(receipt);
            dataLoader = receiptLoader;
            browseLoader = receiptBrowseLoader;
            BeforeValidate += OnBeforeValidate;
            BeforeSave += OnBeforeSave;
            AfterSave += OnAfterSave;
            BeforeDelete += OnBeforeDelete;
            ForceSave = true;
            UseTransactionToSave = true;
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

        [FwLogicProperty(Id: "coXGwDfx5Zqv4", IsReadOnly: true)]
        public string PaymentTypeType { get; set; }

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
        public string RecType { get { return receipt.RecType; } set { receipt.RecType = value; } }

        [FwLogicProperty(Id: "0uxt3Kj2zTdS", IsReadOnly: true)]
        public string ChargeBatchId { get; set; }

        [FwLogicProperty(Id: "3uY1f5SblA5d", IsReadOnly: true)]
        public string ChargeBatchNumber { get; set; }

        [FwLogicProperty(Id: "HL5mbYr6nUYx")]
        public string CurrencyId { get { return receipt.CurrencyId; } set { receipt.CurrencyId = value; } }

        [FwLogicProperty(Id: "D7LZaFqbzDWI", IsReadOnly: true)]
        public string CurrencyCode { get; set; }

        [FwLogicProperty(Id: "DFYUuxYiQR5I", IsReadOnly: true)]
        public string OfficeLocationDefaultCurrencyId { get; set; }

        [FwLogicProperty(Id: "gZIf5MIdFQlmZ")]
        public string OverPaymentId { get { return receipt.OverPaymentId; } set { receipt.OverPaymentId = value; } }

        // this field accepts the requested Invoices and Amounts from the user when saving a new or modified Receipt
        [FwLogicProperty(Id: "BD8n6SDR8Rn6y", IsNotAudited: true)]
        public List<ReceiptInvoice> InvoiceDataList { get; set; }

        // this field accepts the requested Credits and Amounts from the user when saving a new Refund Check
        [FwLogicProperty(Id: "DyE4LuDuuJghH", IsNotAudited: true)]
        public List<ReceiptCredit> CreditDataList { get; set; }

        // if saving a New Receipt, and this value is true, then any amounts over the Invoice Amounts should be saved as a separate Overpayment Receipt
        [FwLogicProperty(Id: "2wv5LlhqpmDdU")]
        public bool? CreateOverpayment { get; set; }

        // if saving a New Receipt, and this value is true, then this Receipt should be saved as a Depleting Deposit
        [FwLogicProperty(Id: "frwPI795LU3yb")]
        public bool? CreateDepletingDeposit { get; set; }

        [FwLogicProperty(Id: "tTMXircHBVgvy")]
        public string DealDepositId { get; set; }

        [FwLogicProperty(Id: "iNueJbp4otiSs")]
        public string DealDepositCheckNumber { get; set; }

        [FwLogicProperty(Id: "JeLwt2Wdz7hc6")]
        public string CustomerDepositId { get; set; }

        [FwLogicProperty(Id: "3gacIXqCngz3T")]
        public string CustomerDepositCheckNumber { get; set; }


        //------------------------------------------------------------------------------------ 
        private void OnBeforeValidate(object sender, BeforeValidateEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                if ((!string.IsNullOrEmpty(DealDepositId)) && (string.IsNullOrEmpty(CheckNumber)))
                {
                    CheckNumber = DealDepositCheckNumber;
                }
                else if ((!string.IsNullOrEmpty(CustomerDepositId)) && (string.IsNullOrEmpty(CheckNumber)))
                {
                    CheckNumber = CustomerDepositCheckNumber;
                }
            }
        }
        //------------------------------------------------------------------------------------
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            ReceiptLogic orig = null;
            bool isValid = true;
            decimal invoiceAmountTotal = 0;
            decimal paymentAmount = 0;
            string recType = RecType;

            if (original != null)
            {
                orig = (ReceiptLogic)original;

                if (PaymentBy != null)
                {
                    if (orig.PaymentBy == null)
                    {
                        orig.PaymentBy = "";
                    }
                    if (!PaymentBy.Equals(orig.PaymentBy))
                    {
                        isValid = false;
                        validateMsg = $"Cannot change the Payment By on this {BusinessLogicModuleName}.";
                    }
                }

                if (CustomerId != null)
                {
                    if (orig.CustomerId == null)
                    {
                        orig.CustomerId = "";
                    }
                    if (!CustomerId.Equals(orig.CustomerId))
                    {
                        isValid = false;
                        validateMsg = $"Cannot change the Customer on this {BusinessLogicModuleName}.";
                    }
                }

                if (DealId != null)
                {
                    if (orig.DealId == null)
                    {
                        orig.DealId = "";
                    }
                    if (!DealId.Equals(orig.DealId))
                    {
                        isValid = false;
                        validateMsg = $"Cannot change the Deal on this {BusinessLogicModuleName}.";
                    }
                }

                if (RecType != null)
                {
                    if (orig.RecType == null)
                    {
                        orig.RecType = "";
                    }
                    if (!RecType.Equals(orig.RecType))
                    {
                        isValid = false;
                        validateMsg = $"Cannot change the RecType on this {BusinessLogicModuleName}.";
                    }
                }

                if (PaymentTypeId != null)
                {
                    if (orig.PaymentTypeId == null)
                    {
                        orig.PaymentTypeId = "";
                        orig.PaymentTypeType = "";
                    }

                    PaymentTypeLogic ptl = new PaymentTypeLogic();
                    ptl.SetDependencies(AppConfig, UserSession);
                    ptl.PaymentTypeId = PaymentTypeId;
                    bool b = ptl.LoadAsync<PaymentTypeLogic>().Result;

                    if (!ptl.PaymentTypeType.Equals(orig.PaymentTypeType))
                    {
                        isValid = false;
                        validateMsg = $"Cannot change the Payment Type on this {BusinessLogicModuleName}.";
                    }
                }
            }


            if (isValid)
            {
                if (saveMode.Equals(TDataRecordSaveMode.smInsert))
                {
                    PaymentTypeLogic ptl = new PaymentTypeLogic();
                    ptl.SetDependencies(AppConfig, UserSession);
                    ptl.PaymentTypeId = PaymentTypeId;
                    bool b = ptl.LoadAsync<PaymentTypeLogic>().Result;
                    if (ptl.PaymentTypeType.Equals(RwConstants.PAYMENT_TYPE_TYPE_WRITE_OFF))
                    {
                        RecType = RwConstants.RECEIPT_RECTYPE_WRITE_OFF;
                    }
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
                PropertyInfo property = typeof(ReceiptLogic).GetProperty(nameof(ReceiptLogic.RecType));
                string[] acceptableValues = { RwConstants.RECEIPT_RECTYPE_PAYMENT, RwConstants.RECEIPT_RECTYPE_OVERPAYMENT, RwConstants.RECEIPT_RECTYPE_DEPLETING_DEPOSIT, RwConstants.RECEIPT_RECTYPE_REFUND, RwConstants.RECEIPT_RECTYPE_NSF_ADJUSTMENT, RwConstants.RECEIPT_RECTYPE_WRITE_OFF, RwConstants.RECEIPT_RECTYPE_CREDIT_MEMO };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }

            if (isValid)
            {
                if (RecType == null)
                {
                    if (saveMode.Equals(TDataRecordSaveMode.smUpdate))
                    {
                        recType = orig.RecType;
                    }
                }
            }

            if (isValid)
            {
                if (saveMode.Equals(TDataRecordSaveMode.smUpdate))
                {
                    if (
                        recType.Equals(RwConstants.RECEIPT_RECTYPE_OVERPAYMENT) ||
                        recType.Equals(RwConstants.RECEIPT_RECTYPE_DEPLETING_DEPOSIT) ||
                        recType.Equals(RwConstants.RECEIPT_RECTYPE_CREDIT_MEMO) ||
                        recType.Equals(RwConstants.RECEIPT_RECTYPE_REFUND)
                       )
                    {
                        isValid = false;
                        validateMsg = $"Cannot modify this {BusinessLogicModuleName} because of its RecType.";
                    }
                }
            }

            if (isValid)
            {
                if (saveMode.Equals(TDataRecordSaveMode.smUpdate))
                {
                    if (((DealDepositId != null) && (!DealDepositId.Equals(orig.DealDepositId))) ||
                        ((CustomerDepositId != null) && (!CustomerDepositId.Equals(orig.CustomerDepositId))))
                    {
                        isValid = false;
                        validateMsg = $"Cannot modify the Deposit related to this {BusinessLogicModuleName}.";
                    }
                }
            }


            if (isValid)
            {
                if (saveMode.Equals(TDataRecordSaveMode.smUpdate))
                {
                    string depId = DealDepositId ?? orig.DealDepositId ?? CustomerDepositId ?? orig.CustomerDepositId;
                    if (!string.IsNullOrEmpty(depId))
                    {
                        if (PaymentAmount != null)
                        {
                            if (!PaymentAmount.Equals(orig.PaymentAmount))
                            {
                                isValid = false;
                                validateMsg = $"Cannot modify the Amount of this {BusinessLogicModuleName} because it is related to a Deposit/Overpayment/Credit.";
                            }
                        }
                    }
                }
            }

            if (isValid)
            {
                if (saveMode.Equals(TDataRecordSaveMode.smUpdate))
                {
                    if (!string.IsNullOrEmpty(orig.OverPaymentId))
                    {
                        if (PaymentAmount != null)
                        {
                            if (!PaymentAmount.Equals(orig.PaymentAmount))
                            {
                                isValid = false;
                                validateMsg = $"Cannot modify the Amount of this {BusinessLogicModuleName} because an Overpayment was created.";
                            }
                        }
                    }
                }
            }

            if (isValid)
            {
                if (saveMode.Equals(TDataRecordSaveMode.smUpdate))
                {
                    if (!string.IsNullOrEmpty(orig.ChargeBatchId))
                    {
                        isValid = false;
                        validateMsg = $"Cannot modify this Receipt because it has already been exported.";
                    }
                }
            }

            if (isValid)
            {
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

                if (paymentAmount < 0)
                {
                    isValid = false;
                    validateMsg = "Payment amount cannot be negative.";
                }
            }

            if (isValid)
            {
                if (InvoiceDataList != null)
                {
                    foreach (ReceiptInvoice i in InvoiceDataList)
                    {
                        if (i.Amount < 0)
                        {
                            isValid = false;
                            validateMsg = "Amount to Apply cannot be negative.";
                        }
                    }
                }
            }

            if (isValid)
            {
                if (CreditDataList == null)
                {
                    if (InvoiceDataList != null)
                    {
                        foreach (ReceiptInvoice i in InvoiceDataList)
                        {
                            invoiceAmountTotal += i.Amount;
                        }
                    }

                    if (recType.Equals(RwConstants.RECEIPT_RECTYPE_PAYMENT))
                    {
                        if ((paymentAmount != 0) && (invoiceAmountTotal == 0))
                        {
                            if (paymentAmount > invoiceAmountTotal)
                            {
                                if ((saveMode.Equals(TDataRecordSaveMode.smInsert)) && CreateDepletingDeposit.GetValueOrDefault(false))
                                {
                                    // user is creating a New Receipt and has indicated to create a Depleting Deposit with this amount
                                }
                                else
                                {
                                    isValid = false;
                                    validateMsg = "No Invoice Amounts have been provided.";
                                }
                            }
                        }
                        else
                        {
                            if ((paymentAmount > invoiceAmountTotal) && (string.IsNullOrEmpty(DealDepositId)) && (string.IsNullOrEmpty(CustomerDepositId)))
                            {
                                if ((saveMode.Equals(TDataRecordSaveMode.smInsert)) && CreateOverpayment.GetValueOrDefault(false))
                                {
                                    // user is creating a New Receipt and has indicated to create an Overpayment with the left-over amount
                                }
                                else
                                {
                                    isValid = false;
                                    validateMsg = "Amount to Apply exceeds the Invoice Amounts provided.";
                                }
                            }
                            else if (paymentAmount < invoiceAmountTotal)
                            {
                                isValid = false;
                                validateMsg = "Amount to Apply is less than the Invoice Amounts provided.";
                            }
                        }
                    }
                }
            }
            if (isValid)
            {
                if (saveMode.Equals(TDataRecordSaveMode.smInsert))
                {
                    if ((!string.IsNullOrEmpty(DealDepositId)) || (!string.IsNullOrEmpty(CustomerDepositId)))
                    {
                        PaymentAmount = invoiceAmountTotal;
                    }
                }
            }


            if (isValid)
            {
                if (InvoiceDataList != null)
                {
                    foreach (ReceiptInvoice i in InvoiceDataList)
                    {
                        decimal invoiceTotal = 0;
                        InvoiceLogic iL = new InvoiceLogic();
                        iL.SetDependencies(AppConfig, UserSession);
                        iL.InvoiceId = i.InvoiceId;
                        bool b = iL.LoadAsync<InvoiceLogic>().Result;
                        invoiceTotal = iL.InvoiceTotal.GetValueOrDefault(0);

                        BrowseRequest br = new BrowseRequest();
                        br.uniqueids = new Dictionary<string, object>();
                        br.uniqueids.Add("InvoiceId", i.InvoiceId);
                        InvoiceReceiptLogic irl = new InvoiceReceiptLogic();
                        irl.SetDependencies(AppConfig, UserSession);
                        FwJsonDataTable dt = irl.BrowseAsync(br).Result;

                        //determine the total receipts applied against this invoice so far, not counting this current Receipt
                        decimal totalReceipts = 0;
                        foreach (List<object> row in dt.Rows)
                        {
                            string recId = row[dt.GetColumnNo("ReceiptId")].ToString();
                            decimal amount = FwConvert.ToDecimal(row[dt.GetColumnNo("Amount")].ToString());
                            if (!recId.Equals(ReceiptId))  // exclude this current Receipt
                            {
                                totalReceipts += amount;
                            }
                        }

                        //add the amount of this current Receipt
                        totalReceipts += i.Amount;

                        if (totalReceipts > invoiceTotal)
                        {
                            isValid = false;
                            validateMsg = "Cannot apply more than the Invoice Total for Invoice " + iL.InvoiceNumber + " (" + iL.InvoiceDescription + ").";
                        }
                    }
                }
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                if (string.IsNullOrEmpty(RecType))
                {
                    RecType = RwConstants.RECEIPT_RECTYPE_PAYMENT;
                }
                if (PaymentBy != null)
                {
                    if (PaymentBy.Equals(RwConstants.RECEIPT_PAYMENT_BY_CUSTOMER))
                    {
                        Deal = "";
                        DealId = "";
                    }
                    else if (PaymentBy.Equals(RwConstants.RECEIPT_PAYMENT_BY_DEAL))
                    {
                        Customer = "";
                        CustomerId = "";
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------

        //Here we want to first ask the database for the previous list of Invoices and Amounts related to this Receipt.  
        //We then compare that list with the list provided by the user and perform updates back to the database.
        //If inserting a new record with DepositId supplied, then we insert the ardepositpmt record
        //All of this is done within the same databas transaction as the insert/update of the Receipt.  Any failures will rollback everything
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            ReceiptLogic orig = null;
            decimal invoiceAmountTotal = 0;
            decimal paymentAmount = 0;

            if (e.Original != null)
            {
                orig = (ReceiptLogic)e.Original;
            }

            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
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


            {
                // here we are applying the receipt amount to indicated Invoices
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
                if (InvoiceDataList != null)
                {
                    foreach (InvoiceReceiptLogic irPrev in previousIrData)
                    {
                        foreach (ReceiptInvoice riNew in InvoiceDataList)  // iterate through the list provided by the user
                        {
                            if (irPrev.InvoiceReceiptId.ToString().Equals(riNew.InvoiceReceiptId)) // find the record that matches this InvoiceReceiptId
                            {
                                if (!irPrev.Amount.Equals(riNew.Amount))
                                {
                                    InvoiceReceiptLogic irNew = irPrev.MakeCopy<InvoiceReceiptLogic>();
                                    irNew.Amount = riNew.Amount;
                                    irNew.SetDependencies(AppConfig, UserSession);
                                    int saveCount = irNew.SaveAsync(irPrev, conn: e.SqlConnection).Result;
                                }
                            }
                        }
                    }
                }

                // iterate through the NEW list.  anything without an InvoiceReceiptId is new, we need to save these
                if (InvoiceDataList != null)
                {
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
                            int saveCount = irNew.SaveAsync(null, conn: e.SqlConnection).Result;
                            ri.InvoiceReceiptId = irNew.InvoiceReceiptId.ToString(); //jh 03/19/2019 provide the ID of the new payment record back with the response
                        }
                        invoiceAmountTotal += ri.Amount;
                    }
                }
            }

            {
                // here we are applying the refund amount to indicated Credits
                List<DepositPaymentLogic> previousDpData = new List<DepositPaymentLogic>();

                if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
                {
                    // ask the database for the previous list of Credits and Amounts related to this Receipt
                    DepositPaymentLogic dpLoader = new DepositPaymentLogic();
                    dpLoader.SetDependencies(AppConfig, UserSession);
                    dpLoader.PaymentId = ReceiptId;
                    BrowseRequest request = new BrowseRequest();
                    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                    uniqueIds.Add("Payment", ReceiptId);
                    request.uniqueids = uniqueIds;
                    previousDpData = dpLoader.SelectAsync<DepositPaymentLogic>(request).Result;  // here is the list with the previous data
                }

                // iterate through the PREVIOUS list.compare each entry with the ones provided by the user.  If the amount is changed, we need to save the modifications to the database
                if (CreditDataList != null)
                {
                    foreach (DepositPaymentLogic dpPrev in previousDpData)
                    {
                        foreach (ReceiptCredit rcNew in CreditDataList)  // iterate through the list provided by the user
                        {
                            if (dpPrev.Id.ToString().Equals(rcNew.CreditReceiptId)) // find the record that matches this CreditReceiptId
                            {
                                if (!dpPrev.Applied.Equals(rcNew.Amount))
                                {
                                    DepositPaymentLogic dpNew = dpPrev.MakeCopy<DepositPaymentLogic>();
                                    dpNew.Applied = rcNew.Amount;
                                    dpNew.SetDependencies(AppConfig, UserSession);
                                    int saveCount = dpNew.SaveAsync(dpPrev, conn: e.SqlConnection).Result;
                                }
                            }
                        }
                    }
                }

                //   iterate through the NEW list.anything without an CreditReceiptId is new, we need to save these
                if (CreditDataList != null)
                {
                    foreach (ReceiptCredit rcNew in CreditDataList)
                    {
                        if ((string.IsNullOrEmpty(rcNew.CreditReceiptId)) && (rcNew.Amount != 0))
                        {
                            DepositPaymentLogic dpNew = new DepositPaymentLogic();
                            dpNew.SetDependencies(AppConfig, UserSession);
                            dpNew.PaymentId = ReceiptId;
                            dpNew.DepositId = rcNew.CreditId;
                            dpNew.Applied = rcNew.Amount;
                            dpNew.SetDependencies(AppConfig, UserSession);
                            int saveCount = dpNew.SaveAsync(null, conn: e.SqlConnection).Result;
                            rcNew.CreditReceiptId = dpNew.Id.ToString();
                        }
                        invoiceAmountTotal += (decimal)rcNew.Amount;
                    }
                }
            }

            if ((e.SaveMode.Equals(TDataRecordSaveMode.smInsert)) && (paymentAmount > invoiceAmountTotal) && (string.IsNullOrEmpty(DealDepositId)) && (string.IsNullOrEmpty(CustomerDepositId)))
            {
                if ((invoiceAmountTotal == 0) && CreateDepletingDeposit.GetValueOrDefault(false))
                {
                    //change the RecType of this Receipt record to "D" (Depleting Deposit)
                    this.RecType = RwConstants.RECEIPT_RECTYPE_DEPLETING_DEPOSIT;
                    int i = this.SaveAsync(this, e.SqlConnection).Result;
                }
                else if ((invoiceAmountTotal > 0) && CreateOverpayment.GetValueOrDefault(false))
                {
                    //create a new Receipt record with the overage, using "O" as the RecType (Overpayment)
                    ReceiptLogic o = new ReceiptLogic();
                    o.SetDependencies(AppConfig, UserSession);
                    o.ReceiptId = this.ReceiptId;
                    bool overpaymentLoaded = o.LoadAsync<ReceiptLogic>(conn: e.SqlConnection).Result;
                    o.ReceiptId = "";
                    o.PaymentAmount = (paymentAmount - invoiceAmountTotal);
                    o.RecType = RwConstants.RECEIPT_RECTYPE_OVERPAYMENT;
                    int i1 = o.SaveAsync(null, e.SqlConnection).Result;
                    this.OverPaymentId = o.ReceiptId;
                    int i2 = this.SaveAsync(this, e.SqlConnection).Result;
                }
                else
                {
                    // should never get here
                }
            }


            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                string depId = DealDepositId ?? CustomerDepositId;
                if (!string.IsNullOrEmpty(depId))
                {
                    DepositPaymentLogic dp = new DepositPaymentLogic();
                    dp.SetDependencies(AppConfig, UserSession);
                    dp.DepositId = depId;
                    dp.PaymentId = ReceiptId;
                    dp.Applied = paymentAmount;
                    int i1 = dp.SaveAsync(null, conn: e.SqlConnection).Result;
                }
                paymentAmount = PaymentAmount.GetValueOrDefault(0);
            }


            //explicitly delete and insert any G/L transactions related to this Receipt
            bool b = ReceiptFunc.PostGlForReceipt(AppConfig, UserSession, ReceiptId, conn: e.SqlConnection).Result;

        }
        //------------------------------------------------------------------------------------
        public void OnBeforeDelete(object sender, BeforeDeleteEventArgs e)
        {
            if (!string.IsNullOrEmpty(ChargeBatchId))
            {
                SystemSettingsLogic s = new SystemSettingsLogic();
                s.SetDependencies(AppConfig, UserSession);
                s.SystemSettingsId = RwConstants.CONTROL_ID;
                bool b = s.LoadAsync<SystemSettingsLogic>().Result;
                if (s.AllowDeleteExportedBeceipts.GetValueOrDefault(false))
                {
                    e.PerformDelete = false;
                    e.ErrorMessage = $"Receipt has already been exported.  Are you sure you want to delete?";
                }
                else
                {
                    e.PerformDelete = false;
                    e.ErrorMessage = $"Cannot delete this Receipt because it has already been exported.";
                }

            }
        }
        //------------------------------------------------------------------------------------
    }
}