using FwStandard.AppManager;
using WebApi.Data.Settings;
using WebApi.Logic;

namespace WebApi.Modules.Settings.ShipVia
{
    [FwLogic(Id:"tc6S14B8Je4yD")]
    public class ShipViaLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ShipViaRecord shipVia = new ShipViaRecord();
        ShipViaLoader shipViaLoader = new ShipViaLoader();

        public ShipViaLogic()
        {
            dataRecords.Add(shipVia);
            dataLoader = shipViaLoader;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"ZuCcdnv4qA7Jq", IsPrimaryKey:true)]
        public string ShipViaId { get { return shipVia.ShipViaId; } set { shipVia.ShipViaId = value; } }

        [FwLogicProperty(Id:"ZuCcdnv4qA7Jq", IsRecordTitle:true)]
        public string ShipVia { get { return shipVia.ShipVia; } set { shipVia.ShipVia = value; } }

        [FwLogicProperty(Id:"wJYCRHkZXIAn")]
        public string VendorId { get { return shipVia.VendorId; } set { shipVia.VendorId = value; } }

        [FwLogicProperty(Id:"7xal7WqOGeyaU", IsReadOnly:true)]
        public string Vendor { get; set; }

        [FwLogicProperty(Id:"5wMTlYpJ7JsX")]
        public bool? Inactive { get { return shipVia.Inactive; } set { shipVia.Inactive = value; } }

        [FwLogicProperty(Id:"UhyvhAvy6Lup")]
        public string DateStamp { get { return shipVia.DateStamp; } set { shipVia.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
