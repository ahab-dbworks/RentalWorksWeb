using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi;

namespace WebApi.Modules.Settings.VehicleSettings.VehicleMake
{
    [FwLogic(Id:"MHET6iyz5fgcM")]
    public class VehicleMakeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VehicleMakeRecord vehicleMake = new VehicleMakeRecord();
        VehicleMakeLoader vehicleMakeLoader = new VehicleMakeLoader();
        public VehicleMakeLogic()
        {
            dataRecords.Add(vehicleMake);
            dataLoader = vehicleMakeLoader;
            RowType = RwConstants.VEHICLE_TYPE_VEHICLE;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"iNP6eCYTXtVMG", IsPrimaryKey:true)]
        public string VehicleMakeId { get { return vehicleMake.VehicleMakeId; } set { vehicleMake.VehicleMakeId = value; } }

        [FwLogicProperty(Id:"iNP6eCYTXtVMG", IsRecordTitle:true)]
        public string VehicleMake { get { return vehicleMake.VehicleMake; } set { vehicleMake.VehicleMake = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"Q86CWIozSXvY")]
        public string RowType { get { return vehicleMake.RowType; } set { vehicleMake.RowType = value; } }

        [FwLogicProperty(Id:"5CD4GAuiaLqX")]
        public bool? Inactive { get { return vehicleMake.Inactive; } set { vehicleMake.Inactive = value; } }

        [FwLogicProperty(Id:"tDaALNpyYcXr")]
        public string DateStamp { get { return vehicleMake.DateStamp; } set { vehicleMake.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
