﻿using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Settings.VehicleModel;

namespace RentalWorksWebApi.Modules.Settings.GeneratorModel
{
    public class GeneratorModelLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VehicleModelRecord vehicleModel = new VehicleModelRecord();
        GeneratorModelLoader generatorModelLoader = new GeneratorModelLoader();
        public GeneratorModelLogic()
        {
            dataRecords.Add(vehicleModel);
            dataLoader = generatorModelLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string GeneratorModelId { get { return vehicleModel.VehicleModelId; } set { vehicleModel.VehicleModelId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string GeneratorModel { get { return vehicleModel.VehicleModel; } set { vehicleModel.VehicleModel = value; } }
        public string GeneratorMakeId { get { return vehicleModel.VehicleMakeId; } set { vehicleModel.VehicleMakeId = value; } }
        public string DateStamp { get { return vehicleModel.DateStamp; } set { vehicleModel.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
