using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.VehicleRating;
using WebLibrary;

namespace WebApi.Modules.Settings.GeneratorRating
{
    [FwLogic(Id:"6Xt8fc9bXFCo")]
    public class GeneratorRatingLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VehicleRatingRecord generatorRating = new VehicleRatingRecord();
        GeneratorRatingLoader generatorRatingLoader = new GeneratorRatingLoader();
        public GeneratorRatingLogic()
        {
            dataRecords.Add(generatorRating);
            dataLoader = generatorRatingLoader;
            RowType = RwConstants.VEHICLE_TYPE_GENERATOR;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"IsgksEfhMLlz", IsPrimaryKey:true)]
        public string GeneratorRatingId { get { return generatorRating.VehicleRatingId; } set { generatorRating.VehicleRatingId = value; } }

        [FwLogicProperty(Id:"IsgksEfhMLlz", IsRecordTitle:true)]
        public string GeneratorRating { get { return generatorRating.VehicleRating; } set { generatorRating.VehicleRating = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"LKGVjqOFfxs")]
        public string RowType { get { return generatorRating.RowType; } set { generatorRating.RowType = value; } }

        [FwLogicProperty(Id:"J3Rf19yl2cE")]
        public bool? Inactive { get { return generatorRating.Inactive; } set { generatorRating.Inactive = value; } }

        [FwLogicProperty(Id:"5J0caNdMgRT")]
        public string DateStamp { get { return generatorRating.DateStamp; } set { generatorRating.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
