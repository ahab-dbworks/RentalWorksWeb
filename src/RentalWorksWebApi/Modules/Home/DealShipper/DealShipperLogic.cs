using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Home.DealShipper
{
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DealShipperId { get { return dealShipper.DealShipperId; } set { dealShipper.DealShipperId = value; } }
        public string DealId { get { return dealShipper.DealId; } set { dealShipper.DealId = value; } }
        public string VendorId { get { return dealShipper.VendorId; } set { dealShipper.VendorId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CarrierId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Carrier { get; set; }
        public string ShipperAcct { get { return dealShipper.ShipperAcct; } set { dealShipper.ShipperAcct = value; } }
        public bool? IsPrimary { get { return dealShipper.IsPrimary; } set { dealShipper.IsPrimary = value; } }
        public string ShipViaId { get { return dealShipper.ShipViaId; } set { dealShipper.ShipViaId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ShipVia { get; set; }
        public string DateStamp { get { return dealShipper.DateStamp; } set { dealShipper.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}