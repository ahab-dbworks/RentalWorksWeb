using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.FuelType;
using WebApi;

namespace WebApi.Modules.Settings.VehicleSettings.VehicleFuelType
{
    [FwLogic(Id:"stcY3m37vMxwA")]
    public class VehicleFuelTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        FuelTypeRecord fuelType = new FuelTypeRecord();
        VehicleFuelTypeLoader fuelTypeLoader = new VehicleFuelTypeLoader();
        public VehicleFuelTypeLogic()
        {
            dataRecords.Add(fuelType);
            dataLoader = fuelTypeLoader;
            RowType = RwConstants.VEHICLE_TYPE_VEHICLE;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"dD4ZZ2b8SV9QL", IsPrimaryKey:true)]
        public string VehicleFuelTypeId { get { return fuelType.FuelTypeId; } set { fuelType.FuelTypeId = value; } }

        [FwLogicProperty(Id:"dD4ZZ2b8SV9QL", IsRecordTitle:true)]
        public string VehicleFuelType { get { return fuelType.FuelType; } set { fuelType.FuelType = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"M9leRT0yztCv")]
        public string RowType { get { return fuelType.RowType; } set { fuelType.RowType = value; } }

        [FwLogicProperty(Id:"Yx9GA65Ah2ot")]
        public bool? Inactive { get { return fuelType.Inactive; } set { fuelType.Inactive = value; } }

        [FwLogicProperty(Id:"vfGslAFzAKYO")]
        public string DateStamp { get { return fuelType.DateStamp; } set { fuelType.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }
}
