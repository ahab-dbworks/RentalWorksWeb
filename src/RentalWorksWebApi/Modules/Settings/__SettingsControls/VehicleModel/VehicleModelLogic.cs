using FwStandard.AppManager;
using Newtonsoft.Json;
using WebApi.Logic;

namespace WebApi.Modules.Settings.VehicleModel
{
    [FwLogic(Id:"Snh7H8GxGoTER")]
    public class VehicleModelLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"d8ycQfLTyNRQZ", IsPrimaryKey:true)]
        public string VehicleModelId { get { return vehicleModel.VehicleModelId; } set { vehicleModel.VehicleModelId = value; } }

        [FwLogicProperty(Id:"d8ycQfLTyNRQZ", IsRecordTitle:true)]
        public string VehicleModel { get { return vehicleModel.VehicleModel; } set { vehicleModel.VehicleModel = value; } }

        [FwLogicProperty(Id:"BDtYeQ4TSpCD")]
        public string VehicleMakeId { get { return vehicleModel.VehicleMakeId; } set { vehicleModel.VehicleMakeId = value; } }

        [FwLogicProperty(Id:"fWoJxyK96DM6l", IsReadOnly:true)]
        public string VehicleMake { get; set; }

        [JsonIgnore]
        [FwLogicProperty(Id:"3Ao1yJYgIrnU", IsReadOnly:true)]
        public string RowType { get; set; }

        [FwLogicProperty(Id:"xQvEIsgdVrF6")]
        public string DateStamp { get { return vehicleModel.DateStamp; } set { vehicleModel.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
