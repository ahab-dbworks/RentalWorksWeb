using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.GeneratorWatts
{
    public class GeneratorWattsLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        GeneratorWattsRecord generatorWatts = new GeneratorWattsRecord();
        public GeneratorWattsLogic()
        {
            dataRecords.Add(generatorWatts);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string GeneratorWattsId { get { return generatorWatts.GeneratorWattsId; } set { generatorWatts.GeneratorWattsId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string GeneratorWatts { get { return generatorWatts.GeneratorWatts; } set { generatorWatts.GeneratorWatts = value; } }
        public string RowType { get { return generatorWatts.RowType; } set { generatorWatts.RowType = value; } }
        public bool Inactive { get { return generatorWatts.Inactive; } set { generatorWatts.Inactive = value; } }
        public string DateStamp { get { return generatorWatts.DateStamp; } set { generatorWatts.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            RowType = "GENERATOR";
        }
        //------------------------------------------------------------------------------------
    }

}
