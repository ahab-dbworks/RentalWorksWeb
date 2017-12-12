﻿using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.VehicleRating;

namespace WebApi.Modules.Settings.GeneratorRating
{
    public class GeneratorRatingLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VehicleRatingRecord generatorRating = new VehicleRatingRecord();
        GeneratorRatingLoader generatorRatingLoader = new GeneratorRatingLoader();
        public GeneratorRatingLogic()
        {
            dataRecords.Add(generatorRating);
            dataLoader = generatorRatingLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string GeneratorRatingId { get { return generatorRating.VehicleRatingId; } set { generatorRating.VehicleRatingId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string GeneratorRating { get { return generatorRating.VehicleRating; } set { generatorRating.VehicleRating = value; } }
        [JsonIgnore]
        public string RowType { get { return generatorRating.RowType; } set { generatorRating.RowType = value; } }
        public bool? Inactive { get { return generatorRating.Inactive; } set { generatorRating.Inactive = value; } }
        public string DateStamp { get { return generatorRating.DateStamp; } set { generatorRating.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            RowType = "GENERATOR";
        }
        //------------------------------------------------------------------------------------
    }

}
