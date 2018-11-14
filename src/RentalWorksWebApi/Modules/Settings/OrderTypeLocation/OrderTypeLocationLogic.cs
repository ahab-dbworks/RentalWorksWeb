using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.OrderTypeLocation
{
    [FwLogic(Id:"weKvHzy4G1kd")]
    public class OrderTypeLocationLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"dj6YtvxNWKJf", IsPrimaryKey:true)]
        public string OrderTypeLocationId { get { return orderTypeLocation.OrderTypeLocationId; } set { orderTypeLocation.OrderTypeLocationId = value; } }

        [FwLogicProperty(Id:"vrx5Dsfkkwrq")]
        public string OrderTypeId { get { return orderTypeLocation.OrderTypeId; } set { orderTypeLocation.OrderTypeId = value; } }

        [FwLogicProperty(Id:"R1Y9PbhhLu8j")]
        public string LocationId { get { return orderTypeLocation.LocationId; } set { orderTypeLocation.LocationId = value; } }

        [FwLogicProperty(Id:"dj6YtvxNWKJf", IsReadOnly:true)]
        public string Location { get; set; }

        [FwLogicProperty(Id:"FBE90yUJpkuf")]
        public string InvoiceClass { get { return orderTypeLocation.InvoiceClass; } set { orderTypeLocation.InvoiceClass = value; } }

        [FwLogicProperty(Id:"pJok2J9Xc6tD")]
        public string TermsConditionsId { get { return orderTypeLocation.TermsConditionsId; } set { orderTypeLocation.TermsConditionsId = value; } }

        [FwLogicProperty(Id:"RPGjIQyi1y6P", IsReadOnly:true)]
        public string TermsConditions { get; set; }

        [FwLogicProperty(Id:"joQBJfbNeuGL")]
        public string CoverLetterId { get { return orderTypeLocation.CoverLetterId; } set { orderTypeLocation.CoverLetterId = value; } }

        [FwLogicProperty(Id:"ypuh9hLC3MbZ", IsReadOnly:true)]
        public string CoverLetter { get; set; }

        [FwLogicProperty(Id:"sp92wxiF6uOu")]
        public string DateStamp { get { return orderTypeLocation.DateStamp; } set { orderTypeLocation.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
