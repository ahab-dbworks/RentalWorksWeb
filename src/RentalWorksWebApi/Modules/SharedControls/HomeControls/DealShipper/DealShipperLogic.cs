using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.DealShipper
{
    [FwLogic(Id: "Fto5kQYOglEN")]
    public class DealShipperLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DealShipperRecord dealShipper = new DealShipperRecord();
        DealShipperLoader dealShipperLoader = new DealShipperLoader();
        public DealShipperLogic()
        {
            dataRecords.Add(dealShipper);
            dataLoader = dealShipperLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "a1GpNe3LmHZo", IsPrimaryKey: true)]
        public string DealShipperId { get { return dealShipper.DealShipperId; } set { dealShipper.DealShipperId = value; } }

        [FwLogicProperty(Id: "tJLHZgzYyrXS")]
        public string DealId { get { return dealShipper.DealId; } set { dealShipper.DealId = value; } }

        [FwLogicProperty(Id: "NrQB01Iy7IiW")]
        public string VendorId { get { return dealShipper.VendorId; } set { dealShipper.VendorId = value; } }

        [FwLogicProperty(Id: "etXOei1dFetX")]
        public string CarrierId { get { return VendorId; } set { this.VendorId = value; } }

        [FwLogicProperty(Id: "etXOei1dFetX", IsReadOnly: true)]
        public string Carrier { get; set; }

        [FwLogicProperty(Id: "XsfG2fvN0d8G")]
        public string ShipperAcct { get { return dealShipper.ShipperAcct; } set { dealShipper.ShipperAcct = value; } }

        [FwLogicProperty(Id: "wZzLQvhWRtpW")]
        public bool? IsPrimary { get { return dealShipper.IsPrimary; } set { dealShipper.IsPrimary = value; } }

        [FwLogicProperty(Id: "fQKWbapmJ51c")]
        public string ShipViaId { get { return dealShipper.ShipViaId; } set { dealShipper.ShipViaId = value; } }

        [FwLogicProperty(Id: "3y6AIQyyyJqI", IsReadOnly: true)]
        public string ShipVia { get; set; }

        [FwLogicProperty(Id: "zNqbxgrXcKP6")]
        public string DateStamp { get { return dealShipper.DateStamp; } set { dealShipper.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
