using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Settings.GeneratorSettings.GeneratorWatts
{
    [FwLogic(Id:"P4vLftwZDYW2")]
    public class GeneratorWattsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        GeneratorWattsRecord generatorWatts = new GeneratorWattsRecord();
        public GeneratorWattsLogic()
        {
            dataRecords.Add(generatorWatts);
            RowType = RwConstants.VEHICLE_TYPE_GENERATOR;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"SFznv5Ws7huz", IsPrimaryKey:true)]
        public string GeneratorWattsId { get { return generatorWatts.GeneratorWattsId; } set { generatorWatts.GeneratorWattsId = value; } }

        [FwLogicProperty(Id:"SFznv5Ws7huz", IsRecordTitle:true)]
        public string GeneratorWatts { get { return generatorWatts.GeneratorWatts; } set { generatorWatts.GeneratorWatts = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"EFhJGXnm24H")]
        public string RowType { get { return generatorWatts.RowType; } set { generatorWatts.RowType = value; } }

        [FwLogicProperty(Id:"ClBe26IhK9F")]
        public bool? Inactive { get { return generatorWatts.Inactive; } set { generatorWatts.Inactive = value; } }

        [FwLogicProperty(Id:"3yPo97qp3lO")]
        public string DateStamp { get { return generatorWatts.DateStamp; } set { generatorWatts.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }
}
