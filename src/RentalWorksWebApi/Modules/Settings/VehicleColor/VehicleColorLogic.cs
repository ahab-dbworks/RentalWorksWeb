using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
using WebApi.Modules.Settings.Color;

namespace WebApi.Modules.Settings.VehicleColor
{
    public class VehicleColorLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ColorRecord vehicleColor = new ColorRecord();
        VehicleColorLoader vehicleColorLoader = new VehicleColorLoader();
        public VehicleColorLogic()
        {
            dataRecords.Add(vehicleColor);
            dataLoader = vehicleColorLoader;
            BeforeSave += OnBeforeSave;
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
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            ColorType = "V";
        }
        //------------------------------------------------------------------------------------

    }

}
