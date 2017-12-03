using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.VehicleMake
{
    public class VehicleMakeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VehicleMakeRecord vehicleMake = new VehicleMakeRecord();
        VehicleMakeLoader vehicleMakeLoader = new VehicleMakeLoader();
        public VehicleMakeLogic()
        {
            dataRecords.Add(vehicleMake);
            dataLoader = vehicleMakeLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string VehicleMakeId { get { return vehicleMake.VehicleMakeId; } set { vehicleMake.VehicleMakeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string VehicleMake { get { return vehicleMake.VehicleMake; } set { vehicleMake.VehicleMake = value; } }
        [JsonIgnore]
        public string RowType { get { return vehicleMake.RowType; } set { vehicleMake.RowType = value; } }
        public bool? Inactive { get { return vehicleMake.Inactive; } set { vehicleMake.Inactive = value; } }
        public string DateStamp { get { return vehicleMake.DateStamp; } set { vehicleMake.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            RowType = "VEHICLE";
        }
        //------------------------------------------------------------------------------------
    }

}
