using FwStandard.AppManager;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.VehicleModel;

namespace WebApi.Modules.Settings.GeneratorSettings.GeneratorModel
{
    [FwLogic(Id:"vEXoCFBfuq7j")]
    public class GeneratorModelLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"XW6CwzyD2UT0", IsPrimaryKey:true)]
        public string GeneratorModelId { get { return vehicleModel.VehicleModelId; } set { vehicleModel.VehicleModelId = value; } }

        [FwLogicProperty(Id:"XW6CwzyD2UT0", IsRecordTitle:true)]
        public string GeneratorModel { get { return vehicleModel.VehicleModel; } set { vehicleModel.VehicleModel = value; } }

        [FwLogicProperty(Id:"Z5RL4RFxqAL")]
        public string GeneratorMakeId { get { return vehicleModel.VehicleMakeId; } set { vehicleModel.VehicleMakeId = value; } }

        [FwLogicProperty(Id:"s4qFDuaDN2gQ", IsReadOnly:true)]
        public string GeneratorMake { get; set; }

        [JsonIgnore]
        [FwLogicProperty(Id:"wPkdEHjK313")]
        public string RowType { get; set; }

        [FwLogicProperty(Id:"0f0MtB7ytxt")]
        public string DateStamp { get { return vehicleModel.DateStamp; } set { vehicleModel.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
