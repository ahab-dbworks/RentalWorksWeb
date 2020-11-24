using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using System;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Billing.ProcessCreditCard.ProcessCreditCardService;
using WebApi.Modules.Billing.Receipt;

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
        public decimal Totals_Replacement_DepositDue { get { return (Totals_Replacement_ReplacementCost * Totals_Replacement_DepositPercentage) / 100.00m; } }

        // Payment Amount
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "D1yvp9JsUzCO", IsReadOnly: true)]
        public decimal Payment_TotalAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "1RHRnXKTzHCn", IsReadOnly: true)]
        public decimal Payment_AmountToPay { get; set; }

        [FwLogicProperty(Id: "HBOwtIcgc4b8", IsReadOnly: true)]
        public string PINPad_Code { get; set; } = string.Empty;

        [FwLogicProperty(Id: "dFUcOGXkkVa7", IsReadOnly: true)]
        public string PINPad_Description { get; set; } = string.Empty;


        [FwLogicProperty(Id: "dFUcOGXkkVa7", IsReadOnly: true)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "SWSReDvLhdGM", IsReadOnly: true)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "eSIJIjRBXpLt", IsReadOnly: true)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 

        //------------------------------------------------------------------------------------
        public async Task<ProcessCreditCardResponse> ProcessPaymentAsync(ProcessCreditCardRequest request)
        {
            ProcessCreditCardResponse response = await this.ProcessCreditCardService.ProcessPaymentAsync(this.AppConfig, request);
            if (response.Status == "APPROVED")
            {
                ReceiptLogic receipt = FwBusinessLogic.CreateBusinessLogic<ReceiptLogic>(this.AppConfig, this.UserSession);
                //receipt.OrderId = this.OrderId;
                receipt.AppliedById = this.UserSession.UsersId;
                receipt.ChargeBatchId = string.Empty;
                receipt.CheckNumber = "auth code";
                receipt.CreateDepletingDeposit = true;
                receipt.CreateOverpayment = false;
                //receipt.CurrencyId = this.CurrencyId;
                //receipt.CustomerDepositCheckNumber = this.CustomerDepositId;
                //receipt.CustomerDepositId = this.CustomerDepositId;
                //receipt.CustomerId = this.CustomerId;
                //receipt.DealDepositCheckNumber = this.DealDepositCheckNumber;
                //receipt.DealDepositId = this.DealDepositId;
                //receipt.DealId = this.DealId;
                //receipt.LocationId = this.LocationId;
                //receipt.PaymentAmount = request.Payment_AmountToPay;
                receipt.PaymentBy = "DEAL";
                receipt.PaymentMemo = "";
                receipt.PaymentTypeId = "";
                receipt.PaymentTypeType = "CREDIT CARD";
                receipt.RecType = "P";
                receipt.ReceiptDate = FwConvert.ToShortDate(DateTime.Now);
                receipt.ReceiptId = string.Empty;
                //await receipt.SaveAsync(null, null, TDataRecordSaveMode.smInsert);
            }
            return response;
        }
        //------------------------------------------------------------------------------------
    }
}
