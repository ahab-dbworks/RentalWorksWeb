using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.FuelType;
using WebLibrary;

namespace WebApi.Modules.Settings.VehicleFuelType
{
    public class VehicleFuelTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        FuelTypeRecord fuelType = new FuelTypeRecord();
        VehicleFuelTypeLoader fuelTypeLoader = new VehicleFuelTypeLoader();
        public VehicleFuelTypeLogic()
        {
            dataRecords.Add(fuelType);
            dataLoader = fuelTypeLoader;
            RowType = RwConstants.VEHICLE_TYPE_VEHICLE;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string VehicleFuelTypeId { get { return fuelType.FuelTypeId; } set { fuelType.FuelTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string VehicleFuelType { get { return fuelType.FuelType; } set { fuelType.FuelType = value; } }
        [JsonIgnore]
        public string RowType { get { return fuelType.RowType; } set { fuelType.RowType = value; } }
        public bool? Inactive { get { return fuelType.Inactive; } set { fuelType.Inactive = value; } }
        public string DateStamp { get { return fuelType.DateStamp; } set { fuelType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}
