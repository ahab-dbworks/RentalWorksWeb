using WebApi.Logic;
using FwStandard.AppManager;


//justin hoffman 12/03/2019 not sure if we need this class
/*
namespace WebApi.Modules.Home.OrderInvoice
{
    [FwLogic(Id: "4slcZIsgJkvCa")]
    public class OrderInvoiceLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderInvoiceRecord orderInvoice = new OrderInvoiceRecord();
        public OrderInvoiceLogic()
        {
            dataRecords.Add(orderInvoice);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "4t5kyglxT8sDc", IsPrimaryKey: true)]
        public string OrderInvoiceId { get { return orderInvoice.OrderInvoiceId; } set { orderInvoice.OrderInvoiceId = value; } }
        [FwLogicProperty(Id: "4tUrnCUiqXPYK")]
        public string PurchaseOrderNumber { get { return orderInvoice.PurchaseOrderNumber; } set { orderInvoice.PurchaseOrderNumber = value; } }
        [FwLogicProperty(Id: "4TvnU0LXUEfIy")]
        public decimal? OrderBy { get { return orderInvoice.OrderBy; } set { orderInvoice.OrderBy = value; } }
        [FwLogicProperty(Id: "4UJpsqW0dt5u8")]
        public bool? IsBillableFlatPoInvoice { get { return orderInvoice.IsBillableFlatPoInvoice; } set { orderInvoice.IsBillableFlatPoInvoice = value; } }
        [FwLogicProperty(Id: "4UMAMmaxlLUJp")]
        public string SupplementalFlatPoId { get { return orderInvoice.SupplementalFlatPoId; } set { orderInvoice.SupplementalFlatPoId = value; } }
        [FwLogicProperty(Id: "4V7EWFivWac7W")]
        public string OrderDiscountId { get { return orderInvoice.OrderDiscountId; } set { orderInvoice.OrderDiscountId = value; } }
        [FwLogicProperty(Id: "4VleXb8vJ3oDf")]
        public string ProjectManagerId { get { return orderInvoice.ProjectManagerId; } set { orderInvoice.ProjectManagerId = value; } }
        [FwLogicProperty(Id: "4wMcRWXW6J7XZ")]
        public string WarehouseId { get { return orderInvoice.WarehouseId; } set { orderInvoice.WarehouseId = value; } }
        [FwLogicProperty(Id: "4Wu9URVsZcQOi")]
        public decimal? OrderInvoiceSubtotal { get { return orderInvoice.OrderInvoiceSubtotal; } set { orderInvoice.OrderInvoiceSubtotal = value; } }
        [FwLogicProperty(Id: "4xcLiHB8vsJgG")]
        public decimal? OrderInvoiceTax { get { return orderInvoice.OrderInvoiceTax; } set { orderInvoice.OrderInvoiceTax = value; } }
        [FwLogicProperty(Id: "4Y9kDMAfXKug3")]
        public decimal? OrderInvoiceTotal { get { return orderInvoice.OrderInvoiceTotal; } set { orderInvoice.OrderInvoiceTotal = value; } }
        [FwLogicProperty(Id: "4YH0e4EBgoKFQ")]
        public decimal? RentalSubTotal { get { return orderInvoice.RentalSubTotal; } set { orderInvoice.RentalSubTotal = value; } }
        [FwLogicProperty(Id: "4ynpZx7vEKTUR")]
        public decimal? SalesSubTotal { get { return orderInvoice.SalesSubTotal; } set { orderInvoice.SalesSubTotal = value; } }
        [FwLogicProperty(Id: "4z4Tn5tA6ELpn")]
        public decimal? FacilitiesSubTotal { get { return orderInvoice.FacilitiesSubTotal; } set { orderInvoice.FacilitiesSubTotal = value; } }
        [FwLogicProperty(Id: "4zO6TMvDIPgGf")]
        public decimal? LaborSubTotal { get { return orderInvoice.LaborSubTotal; } set { orderInvoice.LaborSubTotal = value; } }
        [FwLogicProperty(Id: "50QyBWprf1PhQ")]
        public decimal? MiscellaneousSubTotal { get { return orderInvoice.MiscellaneousSubTotal; } set { orderInvoice.MiscellaneousSubTotal = value; } }
        [FwLogicProperty(Id: "51FtvqLK2nSSW")]
        public decimal? TransportationSubTotal { get { return orderInvoice.TransportationSubTotal; } set { orderInvoice.TransportationSubTotal = value; } }
        [FwLogicProperty(Id: "52EMph9C5mHdH")]
        public decimal? LossAndDamageSubTotal { get { return orderInvoice.LossAndDamageSubTotal; } set { orderInvoice.LossAndDamageSubTotal = value; } }
        [FwLogicProperty(Id: "52mRTFp0N5Wai")]
        public decimal? RentalSaleSubTotal { get { return orderInvoice.RentalSaleSubTotal; } set { orderInvoice.RentalSaleSubTotal = value; } }
        [FwLogicProperty(Id: "53pFXLB7t8IB6")]
        public decimal? RentalTax { get { return orderInvoice.RentalTax; } set { orderInvoice.RentalTax = value; } }
        [FwLogicProperty(Id: "55drrWQ1jZvPh")]
        public decimal? SalesTax { get { return orderInvoice.SalesTax; } set { orderInvoice.SalesTax = value; } }
        [FwLogicProperty(Id: "57aM0ZClYg3XS")]
        public decimal? LaborTax { get { return orderInvoice.LaborTax; } set { orderInvoice.LaborTax = value; } }
        [FwLogicProperty(Id: "57eAxWu0qpyXY")]
        public decimal? MiscellaneousTax { get { return orderInvoice.MiscellaneousTax; } set { orderInvoice.MiscellaneousTax = value; } }
        [FwLogicProperty(Id: "5ADXVZ6aXtDxP")]
        public decimal? LossAndDamageTax { get { return orderInvoice.LossAndDamageTax; } set { orderInvoice.LossAndDamageTax = value; } }
        [FwLogicProperty(Id: "5bEoMdiQLLl08")]
        public decimal? RentalSaleTax { get { return orderInvoice.RentalSaleTax; } set { orderInvoice.RentalSaleTax = value; } }
        [FwLogicProperty(Id: "5BSu6JVrigqUi")]
        public decimal? FacilitiesTax { get { return orderInvoice.FacilitiesTax; } set { orderInvoice.FacilitiesTax = value; } }
        [FwLogicProperty(Id: "5cLnBIhAJNZg0")]
        public decimal? TransportationTax { get { return orderInvoice.TransportationTax; } set { orderInvoice.TransportationTax = value; } }
        [FwLogicProperty(Id: "5DmluwcDDqoeQ")]
        public string ContractId { get { return orderInvoice.ContractId; } set { orderInvoice.ContractId = value; } }
        [FwLogicProperty(Id: "5drolJ1rGFI9I")]
        public string BillingCycleId { get { return orderInvoice.BillingCycleId; } set { orderInvoice.BillingCycleId = value; } }
        [FwLogicProperty(Id: "5e6RlJHmsJwJF")]
        public decimal? PremiumPercent { get { return orderInvoice.PremiumPercent; } set { orderInvoice.PremiumPercent = value; } }
        [FwLogicProperty(Id: "5fN3zpPYgCGz6")]
        public string AgentId { get { return orderInvoice.AgentId; } set { orderInvoice.AgentId = value; } }
        [FwLogicProperty(Id: "5HdlJBbZTiAeF")]
        public bool? IsBilledHiatus { get { return orderInvoice.IsBilledHiatus; } set { orderInvoice.IsBilledHiatus = value; } }
        [FwLogicProperty(Id: "5hkiUQVHGILIq")]
        public string BillingEndDate { get { return orderInvoice.BillingEndDate; } set { orderInvoice.BillingEndDate = value; } }
        [FwLogicProperty(Id: "5IJ6RV5AFWGhD")]
        public string BillingStartDate { get { return orderInvoice.BillingStartDate; } set { orderInvoice.BillingStartDate = value; } }
        [FwLogicProperty(Id: "5JHi2vmCaupLD")]
        public string BillingCycleEvent { get { return orderInvoice.BillingCycleEvent; } set { orderInvoice.BillingCycleEvent = value; } }
        [FwLogicProperty(Id: "5l3HGR9tjVne5")]
        public string EpisodeId { get { return orderInvoice.EpisodeId; } set { orderInvoice.EpisodeId = value; } }
        [FwLogicProperty(Id: "5MbnVs7C5i9ld")]
        public string DealId { get { return orderInvoice.DealId; } set { orderInvoice.DealId = value; } }
        [FwLogicProperty(Id: "5n3ZieG92NdNg")]
        public bool? IsExcludeFromFlatPo { get { return orderInvoice.IsExcludeFromFlatPo; } set { orderInvoice.IsExcludeFromFlatPo = value; } }
        [FwLogicProperty(Id: "5nFSGluCRUXop")]
        public string FlatPoId { get { return orderInvoice.FlatPoId; } set { orderInvoice.FlatPoId = value; } }
        [FwLogicProperty(Id: "5njXosEejjVo5")]
        public decimal? HiatusDiscountPercent { get { return orderInvoice.HiatusDiscountPercent; } set { orderInvoice.HiatusDiscountPercent = value; } }
        [FwLogicProperty(Id: "5Qu5UmLWpKIbV")]
        public string DateStamp { get { return orderInvoice.DateStamp; } set { orderInvoice.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
*/
