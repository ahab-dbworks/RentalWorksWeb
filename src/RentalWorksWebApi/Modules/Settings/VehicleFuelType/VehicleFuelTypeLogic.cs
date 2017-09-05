using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Settings.FuelType;

namespace RentalWorksWebApi.Modules.Settings.VehicleFuelType
{
    public class VehicleFuelTypeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        FuelTypeRecord fuelType = new FuelTypeRecord();
        VehicleFuelTypeLoader fuelTypeLoader = new VehicleFuelTypeLoader();
        public VehicleFuelTypeLogic()
        {
            dataRecords.Add(fuelType);
            dataLoader = fuelTypeLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string VehicleFuelTypeId { get { return fuelType.FuelTypeId; } set { fuelType.FuelTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string VehicleFuelType { get { return fuelType.FuelType; } set { fuelType.FuelType = value; } }
        [JsonIgnore]
        public string RowType { get { return fuelType.RowType; } set { fuelType.RowType = value; } }
        public bool Inactive { get { return fuelType.Inactive; } set { fuelType.Inactive = value; } }
        public string DateStamp { get { return fuelType.DateStamp; } set { fuelType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            RowType = "VEHICLE";
        }
        //------------------------------------------------------------------------------------

    }

}
