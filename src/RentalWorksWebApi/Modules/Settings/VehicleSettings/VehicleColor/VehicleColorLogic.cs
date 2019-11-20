using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Settings.Color;
using WebApi;

namespace WebApi.Modules.Settings.VehicleSettings.VehicleColor
{
    [FwLogic(Id:"7joFkxCytT54u")]
    public class VehicleColorLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        ColorRecord vehicleColor = new ColorRecord();
        VehicleColorLoader vehicleColorLoader = new VehicleColorLoader();
        public VehicleColorLogic()
        {
            dataRecords.Add(vehicleColor);
            dataLoader = vehicleColorLoader;
            ColorType = RwConstants.COLOR_TYPE_VEHICLE;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"fd5cOy9IK62sP", IsPrimaryKey:true)]
        public string VehicleColorId { get { return vehicleColor.ColorId; } set { vehicleColor.ColorId = value; } }

        [FwLogicProperty(Id:"fd5cOy9IK62sP", IsRecordTitle:true)]
        public string VehicleColor { get { return vehicleColor.Color; } set { vehicleColor.Color = value; } }

        [FwLogicProperty(Id:"eMTWvDBRUf1X")]
        public string ColorType { get { return vehicleColor.ColorType; } set { vehicleColor.ColorType = value; } }

        [FwLogicProperty(Id:"G6RH5qbqRYxx")]
        public bool? Inactive { get { return vehicleColor.Inactive; } set { vehicleColor.Inactive = value; } }

        [FwLogicProperty(Id:"zRlrRsD6UgIk")]
        public string DateStamp { get { return vehicleColor.DateStamp; } set { vehicleColor.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }
}
