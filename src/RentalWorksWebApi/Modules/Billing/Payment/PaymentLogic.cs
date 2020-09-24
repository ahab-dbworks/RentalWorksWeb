using WebApi.Logic;
using FwStandard.AppManager;
using System.Collections.Generic;
using FwStandard.BusinessLogic;
using WebApi.Modules.Settings.SystemSettings.SystemSettings;
using WebApi.Modules.HomeControls.VendorInvoicePayment;
using FwStandard.Models;

namespace WebApi.Modules.Billing.Payment
{


    public class PaymentVendorInvoice
    {
        public string VendorInvoicePaymentId { get; set; }
        public string VendorInvoiceId { get; set; }
        public decimal Amount { get; set; }
    }


    [FwLogic(Id: "YAxLRif4rWXiK")]
    public class PaymentLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PaymentRecord payment = new PaymentRecord();
        PaymentLoader paymentLoader = new PaymentLoader();
        public PaymentLogic()
        {
            dataRecords.Add(payment);
            dataLoader = paymentLoader;

            AfterSave += OnAfterSave;
            BeforeDelete += OnBeforeDelete;
            ForceSave = true;
            UseTransactionToSave = true;

        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "yCrCqskyzZl0e", IsPrimaryKey: true)]
        public string PaymentId { get { return payment.PaymentId; } set { payment.PaymentId = value; } }
        [FwLogicProperty(Id: "YCsK3PiyvQWgC")]
        public string PaymentDate { get { return payment.PaymentDate; } set { payment.PaymentDate = value; } }
        [FwLogicProperty(Id: "YDDO3FCMvkhD8")]
        public string LocationId { get { return payment.LocationId; } set { payment.LocationId = value; } }
        [FwLogicProperty(Id: "YDfcZ3tuykY31")]
        public string DepartmentId { get { return payment.DepartmentId; } set { payment.DepartmentId = value; } }
        [FwLogicProperty(Id: "YDnknCM97UjHd", IsReadOnly: true)]
        public string LocationCode { get; set; }
        [FwLogicProperty(Id: "yDxDcyN8jMfa8", IsReadOnly: true)]
        public string Location { get; set; }
        [FwLogicProperty(Id: "YdXm4VICUzQKB")]
        public string VendorId { get { return payment.VendorId; } set { payment.VendorId = value; } }
        [FwLogicProperty(Id: "YEXjcF65Inc9k", IsReadOnly: true)]
        public string Vendor { get; set; }
        [FwLogicProperty(Id: "YeYAiaG7yIG0H")]
        public string PaymentTypeId { get { return payment.PaymentTypeId; } set { payment.PaymentTypeId = value; } }
        [FwLogicProperty(Id: "YFwa55mvcaFvR", IsReadOnly: true)]
        public string PaymentType { get; set; }
        [FwLogicProperty(Id: "YFzhaQ9kteU5Y")]
        public int? BankAccountId { get { return payment.BankAccountId; } set { payment.BankAccountId = value; } }
        [FwLogicProperty(Id: "yGOghdOViV5aK", IsReadOnly: true)]
        public string AccountName { get; set; }
        [FwLogicProperty(Id: "yGs9XcrH9KiTd", IsReadOnly: true)]
        public string OfficeLocationDefaultCurrencyId { get; set; }
        [FwLogicProperty(Id: "yh6sFlfKRP3rd")]
        public string CurrencyId { get { return payment.CurrencyId; } set { payment.CurrencyId = value; } }
        [FwLogicProperty(Id: "yH87lNko2L0vr", IsReadOnly: true)]
        public string Currency { get; set; }
        [FwLogicProperty(Id: "yHP8QjxTjE1Cn", IsReadOnly: true)]
        public string CurrencyCode { get; set; }
        [FwLogicProperty(Id: "yhr3OKDaRjTHK", IsReadOnly: true)]
        public string CurrencySymbol { get; set; }
        [FwLogicProperty(Id: "Yhtdr097snj6H", IsRecordTitle: true)]
        public string CheckNumber { get { return payment.CheckNumber; } set { payment.CheckNumber = value; } }
        [FwLogicProperty(Id: "yiS2CuKG1E0Ps")]
        public string PaymentDocumentNumber { get { return payment.PaymentDocumentNumber; } set { payment.PaymentDocumentNumber = value; } }
        [FwLogicProperty(Id: "yiuQU9Tm5XQT1")]
        public decimal? PaymentAmount { get { return payment.PaymentAmount; } set { payment.PaymentAmount = value; } }
        [FwLogicProperty(Id: "yjGOxisrRWI9r")]
        public string AppliedById { get { return payment.AppliedById; } set { payment.AppliedById = value; } }
        [FwLogicProperty(Id: "yjPQpg5eexOnS", IsReadOnly: true)]
        public string AppliedBy { get; set; }
        [FwLogicProperty(Id: "yKbg8dRpHx2Cg")]
        public string ModifiedById { get { return payment.ModifiedById; } set { payment.ModifiedById = value; } }
        [FwLogicProperty(Id: "yKjsz46xGmpnK", IsReadOnly: true)]
        public string ModifiedBy { get; set; }
        [FwLogicProperty(Id: "ykSeB0sWuztzW")]
        public string PaymentMemo { get { return payment.PaymentMemo; } set { payment.PaymentMemo = value; } }
        [FwLogicProperty(Id: "YKSMgdeGwa5H4")]
        public string RecType { get { return payment.RecType; } set { payment.RecType = value; } }
        [FwLogicProperty(Id: "YKu7te3KZAgDt", IsReadOnly: true)]
        public string ChargeBatchId { get; set; }
        [FwLogicProperty(Id: "yKxooboo3mWAw", IsReadOnly: true)]
        public string ChargeBatchNumber { get; set; }

        // this field accepts the requested Vendor Invoices and Amounts from the user when saving a new or modified Payment
        [FwLogicProperty(Id: "jNR5EbfK2dfR", IsNotAudited: true)]
        public List<PaymentVendorInvoice> VendorInvoiceDataList { get; set; }

        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            return isValid;
        }
        //------------------------------------------------------------------------------------ 
        //Here we want to first ask the database for the previous list of Vendor Invoices and Amounts related to this Payment.
        //We then compare that list with the list provided by the user and perform updates back to the database.
        //All of this is done within the same databas transaction as the insert/update of the Payment.  Any failures will rollback everything
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            PaymentLogic orig = null;
            decimal vendorInvoiceAmountTotal = 0;
            decimal paymentAmount = 0;

            if (e.Original != null)
            {
                orig = (PaymentLogic)e.Original;
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
                // here we are applying the payment amount to indicated Vendor Invoices
                List<VendorInvoicePaymentLogic> previousVipData = new List<VendorInvoicePaymentLogic>();

                if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
                {
                    // ask the database for the previous list of Invoices and Amounts related to this Payment
                    VendorInvoicePaymentLogic irLoader = new VendorInvoicePaymentLogic();
                    irLoader.SetDependencies(AppConfig, UserSession);
                    irLoader.PaymentId = PaymentId;
                    BrowseRequest request = new BrowseRequest();
                    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                    uniqueIds.Add("PaymentId", PaymentId);
                    request.uniqueids = uniqueIds;
                    previousVipData = irLoader.SelectAsync<VendorInvoicePaymentLogic>(request).Result;  // here is the list with the previous data
                }


                // iterate through the PREVIOUS list.  compare each entry with the ones provided by the user.  If the amount is changed, we need to save the modifications to the database
                if (VendorInvoiceDataList != null)
                {
                    foreach (VendorInvoicePaymentLogic vipPrev in previousVipData)
                    {
                        foreach (PaymentVendorInvoice pviNew in VendorInvoiceDataList)  // iterate through the list provided by the user
                        {
                            if (vipPrev.VendorInvoicePaymentId.ToString().Equals(pviNew.VendorInvoicePaymentId)) // find the record that matches this VendorInvoicePaymentId
                            {
                                if (!vipPrev.Amount.Equals(pviNew.Amount))
                                {
                                    VendorInvoicePaymentLogic irNew = vipPrev.MakeCopy<VendorInvoicePaymentLogic>();
                                    irNew.Amount = pviNew.Amount;
                                    irNew.SetDependencies(AppConfig, UserSession);
                                    int saveCount = irNew.SaveAsync(vipPrev, conn: e.SqlConnection).Result;
                                }
                            }
                        }
                    }
                }

                // iterate through the NEW list.  anything without an VendorInvoicePaymentId is new, we need to save these
                if (VendorInvoiceDataList != null)
                {
                    foreach (PaymentVendorInvoice pvi in VendorInvoiceDataList)
                    {
                        if ((string.IsNullOrEmpty(pvi.VendorInvoicePaymentId)) && (pvi.Amount != 0))
                        {
                            VendorInvoicePaymentLogic vipNew = new VendorInvoicePaymentLogic();
                            vipNew.SetDependencies(AppConfig, UserSession);
                            vipNew.PaymentId = PaymentId;
                            vipNew.VendorInvoiceId = pvi.VendorInvoiceId;
                            vipNew.Amount = pvi.Amount;
                            vipNew.SetDependencies(AppConfig, UserSession);
                            int saveCount = vipNew.SaveAsync(null, conn: e.SqlConnection).Result;
                            pvi.VendorInvoicePaymentId = vipNew.VendorInvoicePaymentId.ToString(); 
                        }
                        vendorInvoiceAmountTotal += pvi.Amount;
                    }
                }
            }


            //explicitly delete and insert any G/L transactions related to this Payment
            bool b = PaymentFunc.PostGlForPayment(AppConfig, UserSession, PaymentId, conn: e.SqlConnection).Result;

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
                if (s.AllowDeleteExportedPayments.GetValueOrDefault(false))
                {
                    e.PerformDelete = false;
                    e.ErrorMessage = $"Payment has already been exported.  Are you sure you want to delete?";
                }
                else
                {
                    e.PerformDelete = false;
                    e.ErrorMessage = $"Cannot delete this Payment because it has already been exported.";
                }

            }
        }
        //------------------------------------------------------------------------------------
    }
}
