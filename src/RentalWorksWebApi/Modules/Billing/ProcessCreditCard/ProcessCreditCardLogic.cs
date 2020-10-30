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
        ProcessCreditCardLoader processCreditCardLoader = new ProcessCreditCardLoader();
        public ProcessCreditCardLogic()
        {
            dataLoader = processCreditCardLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "DOo10PuNXp8p", IsReadOnly: true, IsRecordTitle: true)]
        public string RecordTitle { get { return "Process Credit Card: " + this.OrderNo;} set{ } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "ggWKjrdYT2dT", IsPrimaryKey: true, IsReadOnly: true)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "LLzut6Ua2fYL", IsReadOnly: true)]
        public string OrderNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "CvprXvdKccuR", IsReadOnly: true)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "CtERH19mN4fo", IsReadOnly: true)]
        public string CustomerNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "TYgFeAFq0cdB", IsReadOnly: true)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------
    }
}
