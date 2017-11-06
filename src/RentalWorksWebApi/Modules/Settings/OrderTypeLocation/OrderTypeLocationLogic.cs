using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.OrderTypeLocation
{
    public class OrderTypeLocationLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderTypeLocationRecord orderTypeLocation = new OrderTypeLocationRecord();
        OrderTypeLocationLoader orderTypeLocationLoader = new OrderTypeLocationLoader();
        public OrderTypeLocationLogic()
        {
            dataRecords.Add(orderTypeLocation);
            dataLoader = orderTypeLocationLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderTypeLocationId { get { return orderTypeLocation.OrderTypeLocationId; } set { orderTypeLocation.OrderTypeLocationId = value; } }
        public string OrderTypeId { get { return orderTypeLocation.OrderTypeId; } set { orderTypeLocation.OrderTypeId = value; } }
        public string LocationId { get { return orderTypeLocation.LocationId; } set { orderTypeLocation.LocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Location { get; set; }
        public string InvoiceClass { get { return orderTypeLocation.InvoiceClass; } set { orderTypeLocation.InvoiceClass = value; } }
        public string TermsConditionsId { get { return orderTypeLocation.TermsConditionsId; } set { orderTypeLocation.TermsConditionsId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TermsConditions { get; set; }
        public string CoverLetterId { get { return orderTypeLocation.CoverLetterId; } set { orderTypeLocation.CoverLetterId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CoverLetter { get; set; }
        public string DateStamp { get { return orderTypeLocation.DateStamp; } set { orderTypeLocation.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}