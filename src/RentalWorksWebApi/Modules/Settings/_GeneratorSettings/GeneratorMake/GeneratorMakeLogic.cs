using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.VehicleSettings.VehicleMake;
using WebLibrary;

namespace WebApi.Modules.Settings.GeneratorSettings.GeneratorMake
{
    [FwLogic(Id:"xTZqu0tXcxTA")]
    public class GeneratorMakeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VehicleMakeRecord generatorMake = new VehicleMakeRecord();
        GeneratorMakeLoader generatorMakeLoader = new GeneratorMakeLoader();
        public GeneratorMakeLogic()
        {
            dataRecords.Add(generatorMake);
            dataLoader = generatorMakeLoader;
            RowType = RwConstants.VEHICLE_TYPE_GENERATOR;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"EvSabhttxYNQ", IsPrimaryKey:true)]
        public string GeneratorMakeId { get { return generatorMake.VehicleMakeId; } set { generatorMake.VehicleMakeId = value; } }

        [FwLogicProperty(Id:"EvSabhttxYNQ", IsRecordTitle:true)]
        public string GeneratorMake { get { return generatorMake.VehicleMake; } set { generatorMake.VehicleMake = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"jD5xtSw6ipc")]
        public string RowType { get { return generatorMake.RowType; } set { generatorMake.RowType = value; } }

        [FwLogicProperty(Id:"vc3XRBnvBrV")]
        public bool? Inactive { get { return generatorMake.Inactive; } set { generatorMake.Inactive = value; } }

        [FwLogicProperty(Id:"Of6B74DI1Y0")]
        public string DateStamp { get { return generatorMake.DateStamp; } set { generatorMake.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }
}
