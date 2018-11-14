using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Settings.VehicleRating
{
    [FwLogic(Id:"ypSS8kqrX6sk5")]
    public class VehicleRatingLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VehicleRatingRecord vehicleRating = new VehicleRatingRecord();
        VehicleRatingLoader vehicleRatingLoader = new VehicleRatingLoader();
        public VehicleRatingLogic()
        {
            dataRecords.Add(vehicleRating);
            dataLoader = vehicleRatingLoader;
            RowType = RwConstants.VEHICLE_TYPE_VEHICLE;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"hCIvn3jR8myO5", IsPrimaryKey:true)]
        public string VehicleRatingId { get { return vehicleRating.VehicleRatingId; } set { vehicleRating.VehicleRatingId = value; } }

        [FwLogicProperty(Id:"hCIvn3jR8myO5", IsRecordTitle:true)]
        public string VehicleRating { get { return vehicleRating.VehicleRating; } set { vehicleRating.VehicleRating = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"xeAJDgFuGiES")]
        public string RowType { get { return vehicleRating.RowType; } set { vehicleRating.RowType = value; } }

        [FwLogicProperty(Id:"iCiAE4oCbdxv")]
        public bool? Inactive { get { return vehicleRating.Inactive; } set { vehicleRating.Inactive = value; } }

        [FwLogicProperty(Id:"NUeNkZu2r2iy")]
        public string DateStamp { get { return vehicleRating.DateStamp; } set { vehicleRating.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
