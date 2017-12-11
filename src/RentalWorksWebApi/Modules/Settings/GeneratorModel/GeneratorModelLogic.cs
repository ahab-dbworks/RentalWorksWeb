using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.VehicleModel;

namespace WebApi.Modules.Settings.GeneratorModel
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
        [FwBusinessLogicField(isReadOnly: true)]
        public string GeneratorMake { get; set; }
        [JsonIgnore]
        public string RowType { get; set; }
        public string DateStamp { get { return vehicleModel.DateStamp; } set { vehicleModel.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
