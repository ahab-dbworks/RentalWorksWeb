using WebApi.Logic;
using FwStandard.AppManager;
using WebApi.Modules.Billing.Invoice;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WebApi.Modules.HomeControls.OrderInvoice;
using FwStandard.BusinessLogic;
using WebApi.Modules.Billing.Billing;
using WebApi.Modules.HomeControls.Tax;

namespace WebApi.Modules.Billing.BillingWorksheet
{
    [FwLogic(Id: "2hvbkSzqL6z7x")]
    public class BillingWorksheetLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InvoiceRecord billingWorksheet = new InvoiceRecord();
        OrderInvoiceRecord orderInvoice = new OrderInvoiceRecord();
        TaxRecord tax = new TaxRecord();
        BillingWorksheetLoader billingWorksheetLoader = new BillingWorksheetLoader();
        public BillingWorksheetLogic()
        {
            dataRecords.Add(billingWorksheet);
            dataRecords.Add(orderInvoice);
            dataLoader = billingWorksheetLoader;
            InvoiceType = RwConstants.INVOICE_TYPE_WORKSHEET;

            billingWorksheet.BeforeSave += OnBeforeSaveBillingWorksheet;
            billingWorksheet.AfterSave += OnAfteSaveBillingWorksheet;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "2ijwJxkJTB2tW", IsPrimaryKey: true)]
        public string BillingWorksheetId { get { return billingWorksheet.InvoiceId; } set { billingWorksheet.InvoiceId = value; orderInvoice.InvoiceId = value; } }
        [FwLogicProperty(Id: "2joDnffYLhfLE", IsRecordTitle: true)]
        public string WorksheetNumber { get { return billingWorksheet.InvoiceNumber; } set { billingWorksheet.InvoiceNumber = value; } }
        [FwLogicProperty(Id: "2k9NFU18jBITg")]
        public string WorksheetDate { get { return billingWorksheet.InvoiceDate; } set { billingWorksheet.InvoiceDate = value; } }
        [JsonIgnore]
        [FwLogicProperty(Id: "2Kzfx4Th2EA8R")]
        public string InvoiceType { get { return billingWorksheet.InvoiceType; } set { billingWorksheet.InvoiceType = value; } }
        [FwLogicProperty(Id: "2l4sM7D6Y7WZU")]
        public string BillingStartDate { get { return billingWorksheet.BillingStartDate; } set { billingWorksheet.BillingStartDate = value; } }
        [FwLogicProperty(Id: "2LADbGK9NjOP5")]
        public string BillingEndDate { get { return billingWorksheet.BillingEndDate; } set { billingWorksheet.BillingEndDate = value; } }
        [FwLogicProperty(Id: "5QPKY4RJ0HC7V")]
        public string OrderId { get { return orderInvoice.OrderId; } set { orderInvoice.OrderId = value; } }
        [FwLogicProperty(Id: "2lrujFC2e8Xdr", IsReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwLogicProperty(Id: "2MhOETpM1SSyK", IsReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwLogicProperty(Id: "2mi27Luav5zlE", IsReadOnly: true)]
        public string OrderDate { get; set; }
        [FwLogicProperty(Id: "2O50ugJsg5Rqf")]
        public string WorksheetDescription { get { return billingWorksheet.InvoiceDescription; } set { billingWorksheet.InvoiceDescription = value; } }
        [FwLogicProperty(Id: "2Onl5ca6mPd6H", IsReadOnly: true)]
        public string CustomerId { get; set; }
        [FwLogicProperty(Id: "2qOsvFG7COToT")]
        public string DealId { get { return billingWorksheet.DealId; } set { billingWorksheet.DealId = value; } }
        [FwLogicProperty(Id: "2qQYzER3ZGmFP", IsReadOnly: true)]
        public string Deal { get; set; }
        [FwLogicProperty(Id: "2QUXQxjUf2C9F", IsReadOnly: true)]
        public string DealNumber { get; set; }
        [FwLogicProperty(Id: "2rdDWsKp2mxW1")]
        public string DepartmentId { get { return billingWorksheet.DepartmentId; } set { billingWorksheet.DepartmentId = value; } }
        [FwLogicProperty(Id: "2rkEhKeRBWAoK", IsReadOnly: true)]
        public string Department { get; set; }


        [FwLogicProperty(Id: "ivYTIKIYKlxX")]
        public string TaxOptionId { get { return tax.TaxOptionId; } set { tax.TaxOptionId = value; } }

        [FwLogicProperty(Id: "tQ7f6Bnx4UIU", IsReadOnly: true)]
        public string TaxOption { get; set; }

        [FwLogicProperty(Id: "aq5QGMEu0uFi", IsReadOnly: true)]
        public string Tax1Name { get; set; }

        [FwLogicProperty(Id: "4bCpSWTxzHQd", IsReadOnly: true)]
        public string Tax2Name { get; set; }


        //[FwLogicProperty(Id: "oeMnnCMIYYgJ", DisableDirectAssign: true, DisableDirectModify: true)]
        //public string TaxId { get { return dealOrder.TaxId; } set { dealOrder.TaxId = value; tax.TaxId = value; } }

        [FwLogicProperty(Id: "CGVnuUzvOwtI")]
        public decimal? RentalTaxRate1 { get { return tax.RentalTaxRate1; } set { tax.RentalTaxRate1 = value; } }

        [FwLogicProperty(Id: "U8G2t76FgMeI")]
        public decimal? SalesTaxRate1 { get { return tax.SalesTaxRate1; } set { tax.SalesTaxRate1 = value; } }

        [FwLogicProperty(Id: "IbLHuddi2hvX")]
        public decimal? LaborTaxRate1 { get { return tax.LaborTaxRate1; } set { tax.LaborTaxRate1 = value; } }

        [FwLogicProperty(Id: "jm0Awq6pXxE0")]
        public decimal? RentalTaxRate2 { get { return tax.RentalTaxRate2; } set { tax.RentalTaxRate2 = value; } }

        [FwLogicProperty(Id: "LTEDTCv9H2IK")]
        public decimal? SalesTaxRate2 { get { return tax.SalesTaxRate2; } set { tax.SalesTaxRate2 = value; } }

        [FwLogicProperty(Id: "HkyhKAntclj3")]
        public decimal? LaborTaxRate2 { get { return tax.LaborTaxRate2; } set { tax.LaborTaxRate2 = value; } }



        [FwLogicProperty(Id: "2RQpXvymzs1Tx", IsReadOnly: true)]
        public string PurchaseOrderNumber { get; set; }
        [FwLogicProperty(Id: "2shhvpikconAb")]
        public string Status { get { return billingWorksheet.Status; } set { billingWorksheet.Status = value; } }
        [FwLogicProperty(Id: "2tRQffjOFDtlC")]
        public string StatusDate { get { return billingWorksheet.StatusDate; } set { billingWorksheet.StatusDate = value; } }
        [FwLogicProperty(Id: "2TXl1R1aPqFzz")]
        public bool? IsNoCharge { get { return billingWorksheet.IsNoCharge; } set { billingWorksheet.IsNoCharge = value; } }
        [FwLogicProperty(Id: "2uOgzNUjleGcX")]
        public bool? IsAdjusted { get { return billingWorksheet.IsAdjusted; } set { billingWorksheet.IsAdjusted = value; } }
        [FwLogicProperty(Id: "2vg7DaW2hMRd1", IsReadOnly: true)]
        public bool? IsBilledHiatus { get; set; }
        [FwLogicProperty(Id: "2womWuKOugLQr", IsReadOnly: true)]
        public bool? HasLockedTotal { get; set; }
        [FwLogicProperty(Id: "2XarGP2POReez")]
        public bool? IsAlteredDates { get { return billingWorksheet.IsAlteredDates; } set { billingWorksheet.IsAlteredDates = value; } }
        [FwLogicProperty(Id: "2xNuMRucjpGnR")]
        public string OfficeLocationId { get { return billingWorksheet.OfficeLocationId; } set { billingWorksheet.OfficeLocationId = value; } }
        [FwLogicProperty(Id: "ipYGtYeWQ2IVW")]
        public string OfficeLocation { get; set; }
        [FwLogicProperty(Id: "33MLFWm5fZBHC", IsReadOnly: true)]
        public string InputByUserId { get; set; }
        [FwLogicProperty(Id: "37amUdeP2WuNr", IsReadOnly: true)]
        public string FlatPoId { get; set; }
        [FwLogicProperty(Id: "381QSWpungVkP", IsReadOnly: true)]
        public string OrderType { get; set; }
        [FwLogicProperty(Id: "3iaLuKQGrlajH")]
        public decimal? WorksheetTotal { get { return billingWorksheet.InvoiceTotal; } set { billingWorksheet.InvoiceTotal = value; } }
        [FwLogicProperty(Id: "3J9N0UzClX8cB", IsReadOnly: true)]
        public decimal? ResultingInvoiceTotal { get; set; }
        [FwLogicProperty(Id: "3JUqaBMXeqeLL")]
        public string ReferenceNumber { get { return billingWorksheet.ReferenceNumber; } set { billingWorksheet.ReferenceNumber = value; } }
        [FwLogicProperty(Id: "3jZ3dFlaU0FvX")]
        public string AgentId { get { return billingWorksheet.AgentId; } set { billingWorksheet.AgentId = value; } }
        [FwLogicProperty(Id: "3l9uD7C5AmhNM")]
        public string CurrencyId { get { return billingWorksheet.CurrencyId; } set { billingWorksheet.CurrencyId = value; } }
        [FwLogicProperty(Id: "3m3rEOA194dRP", IsReadOnly: true)]
        public string CurrencyCode { get; set; }
        [FwLogicProperty(Id: "3m5s0QgwBvLI6", IsReadOnly: true)]
        public string OfficeLocationDefaultCurrencyId { get; set; }
        [FwLogicProperty(Id: "3M8PWdI1kIIef")]
        public string OutsideSalesRepresentativeId { get { return billingWorksheet.OutsideSalesRepresentativeId; } set { billingWorksheet.OutsideSalesRepresentativeId = value; } }
        [FwLogicProperty(Id: "3mO8aQgYGUM7Y", IsReadOnly: true)]
        public string UsageStartDate { get; set; }
        [FwLogicProperty(Id: "3MweuKDyTXGkD", IsReadOnly: true)]
        public string UsageEndDate { get; set; }
        [FwLogicProperty(Id: "3naUcchoQ3xRx", IsReadOnly: true)]
        public string ResultingInvoiceNumber { get; set; }
        [FwLogicProperty(Id: "3Nv9T095QKTFp", IsReadOnly: true)]
        public string ResultingInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveBillingWorksheet(object sender, BeforeSaveDataRecordEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                WorksheetNumber = BillingFunc.GetNextBillingWorksheetNumberAsync(AppConfig, UserSession, OrderId, e.SqlConnection).Result;

                //if ((TaxOptionId == null) || (TaxOptionId.Equals(string.Empty)))
                //{
                //    TaxOptionId = AppFunc.GetLocationAsync(AppConfig, UserSession, OfficeLocationId, "taxoptionid", e.SqlConnection).Result;
                //}
            }
        }
        //------------------------------------------------------------------------------------
        public void OnAfteSaveBillingWorksheet(object sender, AfterSaveDataRecordEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                orderInvoice.InvoiceId = ((InvoiceRecord)sender).InvoiceId;
            }
        }
        //------------------------------------------------------------------------------------    
        public async Task<ToggleInvoiceApprovedResponse> Approve()
        {
            return await billingWorksheet.Approve();
        }
        //------------------------------------------------------------------------------------    
        public async Task<ToggleInvoiceApprovedResponse> Unapprove()
        {
            return await billingWorksheet.Unapprove();
        }
        //------------------------------------------------------------------------------------    
    }
}
