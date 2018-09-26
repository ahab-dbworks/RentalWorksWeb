using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Settings.VehicleMake
{
    public class VehicleMakeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VehicleMakeRecord vehicleMake = new VehicleMakeRecord();
        VehicleMakeLoader vehicleMakeLoader = new VehicleMakeLoader();
        public VehicleMakeLogic()
        {
            dataRecords.Add(vehicleMake);
            dataLoader = vehicleMakeLoader;
            RowType = RwConstants.VEHICLE_TYPE_VEHICLE;
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
    }

}
