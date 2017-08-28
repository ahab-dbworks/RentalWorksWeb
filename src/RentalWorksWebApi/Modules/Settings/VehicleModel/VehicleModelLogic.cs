using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.VehicleModel
{
    public class VehicleModelLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VehicleModelRecord vehicleModel = new VehicleModelRecord();
        public VehicleModelLogic()
        {
            dataRecords.Add(vehicleModel);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string VehicleModelId { get { return vehicleModel.VehicleModelId; } set { vehicleModel.VehicleModelId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string VehicleModel { get { return vehicleModel.VehicleModel; } set { vehicleModel.VehicleModel = value; } }
        public string VehicleMakeId { get { return vehicleModel.VehicleMakeId; } set { vehicleModel.VehicleMakeId = value; } }
        public string DateStamp { get { return vehicleModel.DateStamp; } set { vehicleModel.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
