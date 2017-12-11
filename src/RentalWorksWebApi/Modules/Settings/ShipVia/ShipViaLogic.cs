using FwStandard.BusinessLogic.Attributes;
using WebApi.Data.Settings;
using WebApi.Logic;

namespace WebApi.Modules.Settings.ShipVia
{
    public class ShipViaLogic : RwBusinessLogic
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ShipViaId { get { return shipVia.ShipViaId; } set { shipVia.ShipViaId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ShipVia { get { return shipVia.ShipVia; } set { shipVia.ShipVia = value; } }
        public string VendorId { get { return shipVia.VendorId; } set { shipVia.VendorId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Vendor { get; set; }
        public bool? Inactive { get { return shipVia.Inactive; } set { shipVia.Inactive = value; } }
        public string DateStamp { get { return shipVia.DateStamp; } set { shipVia.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
