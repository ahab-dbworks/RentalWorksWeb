using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Settings.GeneratorWatts
{
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string GeneratorWattsId { get { return generatorWatts.GeneratorWattsId; } set { generatorWatts.GeneratorWattsId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string GeneratorWatts { get { return generatorWatts.GeneratorWatts; } set { generatorWatts.GeneratorWatts = value; } }
        [JsonIgnore]
        public string RowType { get { return generatorWatts.RowType; } set { generatorWatts.RowType = value; } }
        public bool? Inactive { get { return generatorWatts.Inactive; } set { generatorWatts.Inactive = value; } }
        public string DateStamp { get { return generatorWatts.DateStamp; } set { generatorWatts.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}
