using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.VehicleModel
{
    public class VehicleModelLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VehicleModelRecord vehicleModel = new VehicleModelRecord();
        VehicleModelLoader vehicleModelLoader = new VehicleModelLoader();
        public VehicleModelLogic()
        {
            dataRecords.Add(vehicleModel);
            dataLoader = vehicleModelLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string VehicleModelId { get { return vehicleModel.VehicleModelId; } set { vehicleModel.VehicleModelId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string VehicleModel { get { return vehicleModel.VehicleModel; } set { vehicleModel.VehicleModel = value; } }
        public string VehicleMakeId { get { return vehicleModel.VehicleMakeId; } set { vehicleModel.VehicleMakeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string VehicleMake { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        [JsonIgnore]
        public string RowType { get; set; }
        public string DateStamp { get { return vehicleModel.DateStamp; } set { vehicleModel.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
