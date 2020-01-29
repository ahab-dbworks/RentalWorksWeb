using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Utilities.QuikActivity
{
    [FwLogic(Id: "u8LEDq5KKwNY0")]
    public class QuikActivityLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        QuikActivityLoader quikActivityLoader = new QuikActivityLoader();
        public QuikActivityLogic()
        {
            dataLoader = quikActivityLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "SGrnWiB0jbk")]
        public string ActivityDate { get; set; }

        [FwLogicProperty(Id: "jgDEOKQa2Dxst")]
        public string ActivityTime { get; set; }

        [FwLogicProperty(Id: "rveIxP0YgE4XB")]
        public string ActivityTypeId { get; set; }

        [FwLogicProperty(Id: "32SGrnWiB0jbk")]
        public string ActivityTypeDescription { get; set; }

        [FwLogicProperty(Id: "nBSClOUSyGeJm")]
        public string ActivityTypeColor { get; set; }

        [FwLogicProperty(Id: "IP12sBYs3oMqo")]
        public string ActivityTypeTextColor { get; set; }

        [FwLogicProperty(Id: "WhxNW1CxoDVci")]
        public string ActivityStatusId { get; set; }

        [FwLogicProperty(Id: "Z7VeqzzJ2TywZ")]
        public string ActivityStatus { get; set; }

        [FwLogicProperty(Id: "LEQnoOHeRKI85")]
        public string ActivityStatusColor { get; set; }

        [FwLogicProperty(Id: "Oa6EMnuN9i9re")]
        public string ActivityStatusTextColor { get; set; }

        [FwLogicProperty(Id: "ppcWLsnx6rXtG")]
        public string OrderId { get; set; }

        [FwLogicProperty(Id: "WLkc5TblFXg8U")]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id: "WuIniVBRJR6IT")]
        public string OrderType { get; set; }

        [FwLogicProperty(Id: "0lhZ515LxrDGl")]
        public string OrderTypeController { get; set; }

        [FwLogicProperty(Id: "DKYiaPmxnoahF")]
        public string OrderDescription { get; set; }

        [FwLogicProperty(Id: "uSWgAkcqC8s2n")]
        public string DealId { get; set; }

        [FwLogicProperty(Id: "TPfRpUvb5HH5r")]
        public string Deal { get; set; }

        [FwLogicProperty(Id: "gIwtkn5K0vFax")]
        public string VendorId { get; set; }

        [FwLogicProperty(Id: "Qtxe7PJA7sYK9")]
        public string Vendor { get; set; }

        [FwLogicProperty(Id: "InxHNuWcFEprd")]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id: "yeBNfnVdmT6IS")]
        public string ICode { get; set; }

        [FwLogicProperty(Id: "UlZIuzD8wTj30")]
        public string Description { get; set; }

        [FwLogicProperty(Id: "GUScPBh7s8htg")]
        public decimal? Quantity { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
