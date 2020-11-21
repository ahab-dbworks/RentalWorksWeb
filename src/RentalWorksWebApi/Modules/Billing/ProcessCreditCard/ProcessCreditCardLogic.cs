using FwStandard.AppManager;
using System;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Billing.ProcessCreditCard.ProcessCreditCardService;

namespace WebApi.Modules.Billing.ProcessCreditCard
{

    [FwLogic(Id: "naVthxJ08Q9V")]
    public class ProcessCreditCardLogic : AppBusinessLogic
    {
        public IProcessCreditCardService ProcessCreditCardService;
        //------------------------------------------------------------------------------------ 
        ProcessCreditCardLoader processCreditCardLoader = new ProcessCreditCardLoader();
        public ProcessCreditCardLogic()
        {
            dataLoader = processCreditCardLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "DOo10PuNXp8p", IsReadOnly: true, IsRecordTitle: true)]
        override public string RecordTitle { get { return "Process Credit Card: " + this.OrderNo;} }
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


        // PIN Pad
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "j1m70WLfcPxq", IsReadOnly: true)]
        public string PINPad_Type { get { return "SALES"; } }

        // Weekly Totals
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "IWRGxYILCM4P", IsReadOnly: true)]
        public decimal Totals_Weekly_GrossTotal { get; set; }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "pCuKUlp4lSMT", IsReadOnly: true)]
        public decimal Totals_Weekly_Discount { get; set; }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "b5dbVblvbyCL", IsReadOnly: true)]
        public decimal Totals_Weekly_SubTotal { get; set; }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "vsq9rUJtVoxp", IsReadOnly: true)]
        public decimal Totals_Weekly_Tax { get; set; }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "mebV3K40l8FC", IsReadOnly: true)]
        public decimal Totals_Weekly_GrandTotal { get; set; }

        // Period Totals
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "VH0BCIO1aSpD", IsReadOnly: true)]
        public decimal Totals_Period_GrossTotal { get; set; }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "3pMylt7XIX0R", IsReadOnly: true)]
        public decimal Totals_Period_Discount { get; set; }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "9yCADMCxVXoT", IsReadOnly: true)]
        public decimal Totals_Period_SubTotal { get; set; }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "5j1nJGpDYI2N", IsReadOnly: true)]
        public decimal Totals_Period_Tax { get; set; }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "8FQ0aR37RhBe", IsReadOnly: true)]
        public decimal Totals_Period_GrandTotal { get; set; }

        // Replacement Totals
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "8FQ0aR37RhBe", IsReadOnly: true)]
        public decimal Totals_Replacement_ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "8FQ0aR37RhBe", IsReadOnly: true)]
        public decimal Totals_Replacement_DepositPercentage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "8FQ0aR37RhBe", IsReadOnly: true)]
        public decimal Totals_Replacement_DepositDue { get { return Totals_Replacement_ReplacementCost * Totals_Replacement_DepositPercentage; } }

        // Payment Amount
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "D1yvp9JsUzCO", IsReadOnly: true)]
        public decimal Payment_TotalAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwLogicProperty(Id: "FNmV0PuQN23R", IsReadOnly: true)]
        //public decimal Payment_Deposit { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwLogicProperty(Id: "k5LSmx5qpgKc", IsReadOnly: true)]
        //public decimal Payment_RemainingAmount { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "1RHRnXKTzHCn", IsReadOnly: true)]
        public decimal Payment_AmountToPay { get; set; }

        //------------------------------------------------------------------------------------
        public async Task<ProcessCreditCardResponse> ProcessPaymentAsync(ProcessCreditCardRequest request)
        {
            Console.WriteLine(request);
            //ProcessPaymentResponse response = await InventoryFunc.RetireInventory(AppConfig, UserSession, request);
            ProcessCreditCardResponse response = await this.ProcessCreditCardService.ProcessPaymentAsync(this.AppConfig, request);
            if (true)
            {
                // ProcessDepetingDeposit
            }
            return response;
        }
        //------------------------------------------------------------------------------------
    }
}
