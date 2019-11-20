using FwStandard.AppManager;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.FuelType;
using WebApi.Modules.Settings.GeneratorSettings.GeneratorFuelType;
using WebApi;

namespace WebApi.Modules.Settings.GeneratorFuelType
{
    [FwLogic(Id:"1cUb9tSvxnwU")]
    public class GeneratorFuelTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        FuelTypeRecord fuelType = new FuelTypeRecord();
        GeneratorFuelTypeLoader fuelTypeLoader = new GeneratorFuelTypeLoader();
        public GeneratorFuelTypeLogic()
        {
            dataRecords.Add(fuelType);
            dataLoader = fuelTypeLoader;
            RowType = RwConstants.VEHICLE_TYPE_GENERATOR;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"Y6onKswpB9V4", IsPrimaryKey:true)]
        public string GeneratorFuelTypeId { get { return fuelType.FuelTypeId; } set { fuelType.FuelTypeId = value; } }

        [FwLogicProperty(Id:"Y6onKswpB9V4", IsRecordTitle:true)]
        public string GeneratorFuelType { get { return fuelType.FuelType; } set { fuelType.FuelType = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"eT2wZb2EYaW")]
        public string RowType { get { return fuelType.RowType; } set { fuelType.RowType = value; } }

        [FwLogicProperty(Id:"stltqKHw6Z2")]
        public bool? Inactive { get { return fuelType.Inactive; } set { fuelType.Inactive = value; } }

        [FwLogicProperty(Id:"4xYiku7JWPX")]
        public string DateStamp { get { return fuelType.DateStamp; } set { fuelType.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }
}
