using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Settings.VehicleMake;

namespace WebApi.Modules.Settings.GeneratorMake
{
    public class GeneratorMakeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VehicleMakeRecord generatorMake = new VehicleMakeRecord();
        GeneratorMakeLoader generatorMakeLoader = new GeneratorMakeLoader();
        public GeneratorMakeLogic()
        {
            dataRecords.Add(generatorMake);
            dataLoader = generatorMakeLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string GeneratorMakeId { get { return generatorMake.VehicleMakeId; } set { generatorMake.VehicleMakeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string GeneratorMake { get { return generatorMake.VehicleMake; } set { generatorMake.VehicleMake = value; } }
        [JsonIgnore]
        public string RowType { get { return generatorMake.RowType; } set { generatorMake.RowType = value; } }
        public bool? Inactive { get { return generatorMake.Inactive; } set { generatorMake.Inactive = value; } }
        public string DateStamp { get { return generatorMake.DateStamp; } set { generatorMake.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            RowType = "GENERATOR";
        }
        //------------------------------------------------------------------------------------
    }

}
