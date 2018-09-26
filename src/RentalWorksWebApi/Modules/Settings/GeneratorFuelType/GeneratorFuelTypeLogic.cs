using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.FuelType;
using WebLibrary;

namespace WebApi.Modules.Settings.GeneratorFuelType
{
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string GeneratorFuelTypeId { get { return fuelType.FuelTypeId; } set { fuelType.FuelTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string GeneratorFuelType { get { return fuelType.FuelType; } set { fuelType.FuelType = value; } }
        [JsonIgnore]
        public string RowType { get { return fuelType.RowType; } set { fuelType.RowType = value; } }
        public bool? Inactive { get { return fuelType.Inactive; } set { fuelType.Inactive = value; } }
        public string DateStamp { get { return fuelType.DateStamp; } set { fuelType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}
