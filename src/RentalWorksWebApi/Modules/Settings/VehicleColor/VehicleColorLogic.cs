using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Settings.Color;

namespace RentalWorksWebApi.Modules.Settings.VehicleColor
{
    public class VehicleColorLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ColorRecord vehicleColor = new ColorRecord();
        VehicleColorLoader vehicleColorLoader = new VehicleColorLoader();
        public VehicleColorLogic()
        {
            dataRecords.Add(vehicleColor);
            dataLoader = vehicleColorLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string VehicleColorId { get { return vehicleColor.ColorId; } set { vehicleColor.ColorId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string VehicleColor { get { return vehicleColor.Color; } set { vehicleColor.Color = value; } }
        public string ColorType { get { return vehicleColor.ColorType; } set { vehicleColor.ColorType = value; } }
        public bool? Inactive { get { return vehicleColor.Inactive; } set { vehicleColor.Inactive = value; } }
        public string DateStamp { get { return vehicleColor.DateStamp; } set { vehicleColor.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            ColorType = "V";
        }
        //------------------------------------------------------------------------------------

    }

}
