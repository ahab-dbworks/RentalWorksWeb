using WebApi.Logic;
using FwStandard.AppManager;
using System.Collections.Generic;
using FwStandard.BusinessLogic;
using WebApi.Modules.Settings.SystemSettings.SystemSettings;
using WebApi.Modules.HomeControls.VendorInvoicePayment;
using FwStandard.Models;
using FwStandard.SqlServer;
using WebApi.Modules.Billing.VendorInvoice;
using WebApi.Modules.Home.BankAccount;

namespace WebApi.Modules.Billing.ProcessCreditCard
{

    [FwLogic(Id: "naVthxJ08Q9V")]
    public class ProcessCreditCardLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        //PaymentRecord payment = new PaymentRecord();
        //PaymentLoader paymentLoader = new PaymentLoader();
        public ProcessCreditCardLogic()
        {
            /*
            dataRecords.Add(payment);
            dataLoader = paymentLoader;

            AfterSave += OnAfterSave;
            BeforeDelete += OnBeforeDelete;
            ForceSave = true;
            UseTransactionToSave = true;
            */
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "kqLYO5NRyVV4", IsPrimaryKey: true)]
        public string OrderId { get; set; }

        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            return isValid;
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeDelete(object sender, BeforeDeleteEventArgs e)
        {
        }
        //------------------------------------------------------------------------------------
    }
}
