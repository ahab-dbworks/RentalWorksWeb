using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.VehicleRating
{
    public class VehicleRatingLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VehicleRatingRecord vehicleRating = new VehicleRatingRecord();
        VehicleRatingLoader vehicleRatingLoader = new VehicleRatingLoader();
        public VehicleRatingLogic()
        {
            dataRecords.Add(vehicleRating);
            dataLoader = vehicleRatingLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string VehicleRatingId { get { return vehicleRating.VehicleRatingId; } set { vehicleRating.VehicleRatingId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string VehicleRating { get { return vehicleRating.VehicleRating; } set { vehicleRating.VehicleRating = value; } }
        [JsonIgnore]
        public string RowType { get { return vehicleRating.RowType; } set { vehicleRating.RowType = value; } }
        public bool? Inactive { get { return vehicleRating.Inactive; } set { vehicleRating.Inactive = value; } }
        public string DateStamp { get { return vehicleRating.DateStamp; } set { vehicleRating.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            RowType = "VEHICLE";
        }
        //------------------------------------------------------------------------------------
    }

}
