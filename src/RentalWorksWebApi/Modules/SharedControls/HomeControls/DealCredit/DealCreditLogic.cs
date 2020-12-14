using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.DealCredit
{
    [FwLogic(Id: "WripDzAneJek")]
    public class DealCreditLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DealCreditLoader dealCreditLoader = new DealCreditLoader();
        public DealCreditLogic()
        {
            dataLoader = dealCreditLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "HVj9PabFHJJB", IsReadOnly: true)]
        public string ReceiptId { get; set; }
        [FwLogicProperty(Id: "gg9NN28stf5j", IsReadOnly: true)]
        public string CustomerId { get; set; }
        [FwLogicProperty(Id: "gWA3n1TKZvBr", IsReadOnly: true)]
        public string Customer { get; set; }
        [FwLogicProperty(Id: "5zSKHplydiqk", IsReadOnly: true)]
        public string DealId { get; set; }
        [FwLogicProperty(Id: "sJwIq7diMhZM9", IsReadOnly: true)]
        public string Deal { get; set; }
        [FwLogicProperty(Id: "LBvTFF7dcF2K", IsReadOnly: true)]
        public string OfficeLocationId { get; set; }
        [FwLogicProperty(Id: "k86inHUAzzMm", IsReadOnly: true)]
        public string PaymentBy { get; set; }
        [FwLogicProperty(Id: "EeINVIzUnqgt", IsReadOnly: true)]
        public string RecType { get; set; }
        [FwLogicProperty(Id: "y8ohSHagJ0G7", IsReadOnly: true)]
        public string RecTypeDisplay { get; set; }
        [FwLogicProperty(Id: "BV639n04HTMtN", IsReadOnly: true)]
        public string RecTypeColor { get; set; }
        [FwLogicProperty(Id: "pOhOCSgTMnCK", IsReadOnly: true)]
        public string ReceiptDate { get; set; }
        [FwLogicProperty(Id: "Fhw6oVtJXAYx", IsReadOnly: true)]
        public string PaymentTypeId { get; set; }
        [FwLogicProperty(Id: "mP8QykbDGBAk", IsReadOnly: true)]
        public string PaymentType { get; set; }
        [FwLogicProperty(Id: "JuHoud6X5FTfk", IsReadOnly: true)]
        public string CheckNumber { get; set; }
        [FwLogicProperty(Id: "ReYGibzDwM2G", IsReadOnly: true)]
        public decimal? Amount { get; set; }
        [FwLogicProperty(Id: "6y5k0SIHO1do", IsReadOnly: true)]
        public decimal? Applied { get; set; }
        [FwLogicProperty(Id: "CZn69JbNjmQo", IsReadOnly: true)]
        public decimal? Refunded { get; set; }
        [FwLogicProperty(Id: "vrAKkqvs5S6K", IsReadOnly: true)]
        public decimal? Remaining { get; set; }
        [FwLogicProperty(Id: "MGMEgsJZMrVm", IsReadOnly: true)]
        public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
